#include "MainWindow.h"

#include <QAction>
#include <QApplication>
#include <QClipboard>
#include <QCloseEvent>
#include <QDateTime>
#include <QDesktopServices>
#include <QDir>
#include <QFileInfo>
#include <QFont>
#include <QFontDatabase>
#include <QFormLayout>
#include <QGroupBox>
#include <QHBoxLayout>
#include <QIcon>
#include <QLabel>
#include <QListWidget>
#include <QMenu>
#include <QMessageBox>
#include <QPlainTextEdit>
#include <QProgressBar>
#include <QPushButton>
#include <QScrollBar>
#include <QStandardPaths>
#include <QStatusBar>
#include <QTime>
#include <QTimer>
#include <QUrl>
#include <QVBoxLayout>

namespace {

const QString kPreviousRef = QStringLiteral("refs/syncro-production/previous");
const QString kRollbackFromRef = QStringLiteral("refs/syncro-production/rollback-from");
const QChar kFieldSeparator(0x1f);
constexpr int kMaxLogFiles = 30;

QString logDirectory()
{
    return QStandardPaths::writableLocation(QStandardPaths::AppLocalDataLocation) + QStringLiteral("/logs");
}

QString operationName(MainWindow::Operation operation)
{
    switch (operation) {
    case MainWindow::Operation::Update:
        return QStringLiteral("update");
    case MainWindow::Operation::Rollback:
        return QStringLiteral("rollback");
    case MainWindow::Operation::Restart:
        return QStringLiteral("restart");
    default:
        return QStringLiteral("check");
    }
}

QString commandError(const QString &prefix, const StepResult &result)
{
    const QString detail = (result.standardError.trimmed().isEmpty() ? result.standardOutput : result.standardError).trimmed();
    return detail.isEmpty() ? prefix : prefix + QLatin1Char('\n') + detail.section(QLatin1Char('\n'), -4);
}

} // namespace

MainWindow::MainWindow(const QString &repoPath, int checkIntervalMinutes, QWidget *parent)
    : QMainWindow(parent)
    , m_repoPath(QDir(repoPath).absolutePath())
{
    setWindowTitle(tr("Aktualizator Syncro"));
    setWindowIcon(QIcon(QStringLiteral(":/syncro-update.svg")));
    resize(900, 680);

    m_runner.setWorkingDirectory(m_repoPath);
    buildUi();
    buildTray();

    connect(&m_runner, &CommandRunner::stepStarted, this, [this](int index, int count, const QString &title) {
        m_progress->setRange(0, count);
        m_progress->setValue(index);
        m_stepLabel->setText(tr("Krok %1 z %2: %3").arg(index + 1).arg(count).arg(title));
        if (!m_silentCheck)
            appendLog(QStringLiteral("▶ %1").arg(title), false);
    });
    connect(&m_runner, &CommandRunner::outputLine, this, &MainWindow::appendLog);
    connect(&m_runner, &CommandRunner::cancellableChanged, this, [this](bool cancellable) {
        m_cancelButton->setEnabled(cancellable);
    });
    connect(&m_runner, &CommandRunner::finished, this, [this](bool success, const QString &message) {
        const Operation finished = m_operation;
        const bool silent = m_silentCheck;
        m_operation = Operation::None;
        m_silentCheck = false;
        m_clockTimer->stop();
        m_progress->setValue(m_progress->maximum());

        const QString duration = tr("%1 s").arg(m_elapsed.elapsed() / 1000);
        if (success) {
            m_stepLabel->setText(finished == Operation::Check ? tr("Sprawdzanie zakończone.") : tr("Operacja zakończona powodzeniem (%1).").arg(duration));
            appendLog(finished == Operation::Check ? tr("✔ Stan odświeżony.") : tr("✔ Operacja zakończona powodzeniem."), false);
        } else {
            m_stepLabel->setText(tr("Operacja nieudana."));
            appendLog(QStringLiteral("✖ %1").arg(message), true);
        }
        if (m_logFile.isOpen())
            m_logFile.close();

        setBusy(false);
        refreshView();
        if (m_info.behind == 0)
            m_lastNotifiedBehind = 0;

        if (finished == Operation::Check && silent) {
            if (success && m_tray && m_info.behind > 0 && m_info.behind != m_lastNotifiedBehind) {
                m_lastNotifiedBehind = m_info.behind;
                m_tray->showMessage(tr("Dostępna aktualizacja Syncro"),
                                    tr("Nowe zmiany: %1. Kliknij ikonę, aby otworzyć aktualizator.").arg(m_info.behind),
                                    QSystemTrayIcon::Information, 15000);
            }
        } else if (finished == Operation::Update || finished == Operation::Rollback || finished == Operation::Restart) {
            const QString title = finished == Operation::Update ? tr("Aktualizacja")
                                  : finished == Operation::Rollback ? tr("Przywracanie")
                                                                    : tr("Restart");
            if (success) {
                if (m_tray && isHidden())
                    m_tray->showMessage(title, tr("Operacja zakończona powodzeniem."), QSystemTrayIcon::Information, 10000);
                else
                    QMessageBox::information(this, title, tr("Operacja zakończona powodzeniem w %1.").arg(duration));
            } else {
                showAndRaise();
                QString text = message;
                if (finished == Operation::Update && !m_beforeUpdate.isEmpty() && m_info.head != m_beforeUpdate)
                    text += tr("\n\nKod został już zaktualizowany, ale wdrożenie się nie powiodło. "
                               "Możesz spróbować ponownie albo przywrócić poprzednią wersję.");
                QMessageBox::critical(this, tr("%1 nie powiodła się").arg(title), text);
            }
        } else if (!success && !silent) {
            QMessageBox::warning(this, tr("Sprawdzanie aktualizacji"), message);
        }

        if (m_quitRequested) {
            qApp->quit();
            return;
        }
        const Operation queued = m_queued;
        m_queued = Operation::None;
        if (queued == Operation::Update)
            startUpdate();
        else if (queued == Operation::Rollback)
            startRollback();
        else if (queued == Operation::Restart)
            startRestart();
    });

    m_clockTimer = new QTimer(this);
    m_clockTimer->setInterval(1000);
    connect(m_clockTimer, &QTimer::timeout, this, [this]() {
        m_elapsedLabel->setText(tr("Czas: %1 s").arg(m_elapsed.elapsed() / 1000));
    });

    m_checkTimer = new QTimer(this);
    if (checkIntervalMinutes > 0) {
        m_checkTimer->setInterval(checkIntervalMinutes * 60 * 1000);
        connect(m_checkTimer, &QTimer::timeout, this, [this]() {
            if (m_runner.isRunning())
                return;
            m_silentCheck = true;
            checkForUpdates();
        });
        m_checkTimer->start();
    }

    refreshView();
    QTimer::singleShot(0, this, [this]() {
        m_silentCheck = isHidden();
        checkForUpdates();
    });
}

bool MainWindow::hasTray() const
{
    return m_tray != nullptr;
}

void MainWindow::buildUi()
{
    auto *central = new QWidget(this);
    auto *layout = new QVBoxLayout(central);

    auto *stateBox = new QGroupBox(tr("Stan instalacji"), central);
    auto *form = new QFormLayout(stateBox);
    form->setLabelAlignment(Qt::AlignRight);
    auto makeValue = [stateBox]() {
        auto *label = new QLabel(stateBox);
        label->setTextInteractionFlags(Qt::TextSelectableByMouse);
        label->setWordWrap(true);
        return label;
    };
    m_repoLabel = makeValue();
    m_branchLabel = makeValue();
    m_currentLabel = makeValue();
    m_availableLabel = makeValue();
    m_previousLabel = makeValue();
    m_cleanLabel = makeValue();
    form->addRow(tr("Katalog:"), m_repoLabel);
    form->addRow(tr("Gałąź:"), m_branchLabel);
    form->addRow(tr("Bieżąca wersja:"), m_currentLabel);
    form->addRow(tr("Aktualizacje:"), m_availableLabel);
    form->addRow(tr("Wersja do przywrócenia:"), m_previousLabel);
    form->addRow(tr("Kopia robocza:"), m_cleanLabel);
    layout->addWidget(stateBox);

    auto *pendingBox = new QGroupBox(tr("Nowe zmiany do pobrania"), central);
    auto *pendingLayout = new QVBoxLayout(pendingBox);
    m_pendingList = new QListWidget(pendingBox);
    m_pendingList->setMaximumHeight(140);
    m_pendingList->setSelectionMode(QAbstractItemView::NoSelection);
    pendingLayout->addWidget(m_pendingList);
    layout->addWidget(pendingBox);

    auto *buttons = new QHBoxLayout();
    m_checkButton = new QPushButton(QIcon::fromTheme(QStringLiteral("view-refresh")), tr("Sprawdź aktualizacje"), central);
    m_updateButton = new QPushButton(QIcon::fromTheme(QStringLiteral("system-software-update")), tr("Aktualizuj"), central);
    m_rollbackButton = new QPushButton(QIcon::fromTheme(QStringLiteral("edit-undo")), tr("Przywróć poprzednią wersję"), central);
    m_restartButton = new QPushButton(QIcon::fromTheme(QStringLiteral("system-reboot")), tr("Uruchom ponownie"), central);
    m_restartButton->setToolTip(tr("Restartuje kontenery produkcyjne bez pobierania i przebudowy (make restart-production)."));
    m_cancelButton = new QPushButton(QIcon::fromTheme(QStringLiteral("process-stop")), tr("Anuluj"), central);
    m_updateButton->setDefault(true);
    m_cancelButton->setEnabled(false);
    m_cancelButton->setToolTip(tr("Anulować można tylko pobieranie i sprawdzanie; wdrożenia nie przerywa się w połowie."));
    buttons->addWidget(m_checkButton);
    buttons->addStretch();
    buttons->addWidget(m_restartButton);
    buttons->addWidget(m_rollbackButton);
    buttons->addWidget(m_updateButton);
    buttons->addWidget(m_cancelButton);
    layout->addLayout(buttons);

    auto *progressRow = new QHBoxLayout();
    m_stepLabel = new QLabel(tr("Gotowy."), central);
    m_elapsedLabel = new QLabel(central);
    progressRow->addWidget(m_stepLabel, 1);
    progressRow->addWidget(m_elapsedLabel);
    layout->addLayout(progressRow);

    m_progress = new QProgressBar(central);
    m_progress->setRange(0, 1);
    m_progress->setValue(0);
    m_progress->setTextVisible(false);
    layout->addWidget(m_progress);

    auto *logBox = new QGroupBox(tr("Dziennik"), central);
    auto *logLayout = new QVBoxLayout(logBox);
    m_log = new QPlainTextEdit(logBox);
    m_log->setReadOnly(true);
    m_log->setMaximumBlockCount(5000);
    m_log->setFont(QFontDatabase::systemFont(QFontDatabase::FixedFont));
    m_log->setLineWrapMode(QPlainTextEdit::NoWrap);
    logLayout->addWidget(m_log);
    auto *logButtons = new QHBoxLayout();
    auto *copyLog = new QPushButton(QIcon::fromTheme(QStringLiteral("edit-copy")), tr("Kopiuj dziennik"), logBox);
    auto *openLogs = new QPushButton(QIcon::fromTheme(QStringLiteral("folder-open")), tr("Otwórz folder dzienników"), logBox);
    logButtons->addStretch();
    logButtons->addWidget(copyLog);
    logButtons->addWidget(openLogs);
    logLayout->addLayout(logButtons);
    layout->addWidget(logBox, 1);

    setCentralWidget(central);
    statusBar()->showMessage(tr("Wersja aktualizatora %1").arg(QStringLiteral(SYNCRO_UPDATER_VERSION)));

    connect(m_checkButton, &QPushButton::clicked, this, &MainWindow::checkForUpdates);
    connect(m_updateButton, &QPushButton::clicked, this, &MainWindow::startUpdate);
    connect(m_rollbackButton, &QPushButton::clicked, this, &MainWindow::startRollback);
    connect(m_restartButton, &QPushButton::clicked, this, &MainWindow::startRestart);
    connect(m_cancelButton, &QPushButton::clicked, &m_runner, &CommandRunner::cancel);
    connect(copyLog, &QPushButton::clicked, this, [this]() { QApplication::clipboard()->setText(m_log->toPlainText()); });
    connect(openLogs, &QPushButton::clicked, this, []() {
        QDir().mkpath(logDirectory());
        QDesktopServices::openUrl(QUrl::fromLocalFile(logDirectory()));
    });
}

void MainWindow::buildTray()
{
    if (!QSystemTrayIcon::isSystemTrayAvailable())
        return;

    m_tray = new QSystemTrayIcon(windowIcon(), this);
    auto *menu = new QMenu(this);
    QAction *show = menu->addAction(tr("Pokaż okno"));
    m_trayCheck = menu->addAction(tr("Sprawdź aktualizacje"));
    m_trayUpdate = menu->addAction(tr("Aktualizuj"));
    m_trayRollback = menu->addAction(tr("Przywróć poprzednią wersję"));
    m_trayRestart = menu->addAction(tr("Uruchom ponownie"));
    menu->addSeparator();
    QAction *quit = menu->addAction(tr("Zakończ"));

    connect(show, &QAction::triggered, this, &MainWindow::showAndRaise);
    connect(m_trayCheck, &QAction::triggered, this, &MainWindow::checkForUpdates);
    connect(m_trayUpdate, &QAction::triggered, this, [this]() { showAndRaise(); startUpdate(); });
    connect(m_trayRollback, &QAction::triggered, this, [this]() { showAndRaise(); startRollback(); });
    connect(m_trayRestart, &QAction::triggered, this, [this]() { showAndRaise(); startRestart(); });
    connect(quit, &QAction::triggered, this, &MainWindow::requestQuit);
    connect(m_tray, &QSystemTrayIcon::activated, this, [this](QSystemTrayIcon::ActivationReason reason) {
        if (reason != QSystemTrayIcon::Trigger)
            return;
        if (isVisible() && !isMinimized())
            hide();
        else
            showAndRaise();
    });
    connect(m_tray, &QSystemTrayIcon::messageClicked, this, &MainWindow::showAndRaise);

    m_tray->setContextMenu(menu);
    m_tray->setToolTip(tr("Aktualizator Syncro"));
    m_tray->show();
}

void MainWindow::handleCommand(const QString &command)
{
    if (command == QLatin1String("update")) {
        showAndRaise();
        startUpdate();
    } else if (command == QLatin1String("rollback")) {
        showAndRaise();
        startRollback();
    } else if (command == QLatin1String("restart")) {
        showAndRaise();
        startRestart();
    } else if (command == QLatin1String("check")) {
        checkForUpdates();
    } else {
        showAndRaise();
    }
}

void MainWindow::showAndRaise()
{
    show();
    setWindowState((windowState() & ~Qt::WindowMinimized) | Qt::WindowActive);
    raise();
    activateWindow();
}

QString MainWindow::repoError() const
{
    const QFileInfo repo(m_repoPath);
    if (!repo.isDir())
        return tr("Katalog repozytorium nie istnieje: %1").arg(m_repoPath);
    if (!QFileInfo::exists(m_repoPath + QStringLiteral("/.git")))
        return tr("Katalog nie jest repozytorium Git: %1").arg(m_repoPath);
    return {};
}

Step MainWindow::gitStep(const QString &title, const QStringList &arguments, std::function<QString(const StepResult &)> onFinished, bool echo)
{
    Step step;
    step.title = title;
    step.program = QStringLiteral("git");
    step.arguments = [arguments]() { return arguments; };
    step.onFinished = std::move(onFinished);
    step.echoOutput = echo;
    return step;
}

QVector<Step> MainWindow::preflightSteps(bool requireClean, bool requireMake)
{
    QVector<Step> steps;

    Step environment;
    environment.title = tr("Sprawdzanie środowiska");
    environment.action = [this, requireMake]() -> QString {
        const QString error = repoError();
        if (!error.isEmpty())
            return error;
        if (QStandardPaths::findExecutable(QStringLiteral("git")).isEmpty())
            return tr("Git nie jest zainstalowany.");
        if (requireMake) {
            if (QStandardPaths::findExecutable(QStringLiteral("make")).isEmpty())
                return tr("make nie jest zainstalowany.");
            if (QStandardPaths::findExecutable(QStringLiteral("docker")).isEmpty())
                return tr("Docker nie jest zainstalowany albo nie ma go w PATH.");
            if (!QFileInfo::exists(m_repoPath + QStringLiteral("/.env.production")))
                return tr("Brak pliku .env.production. Skopiuj .env.production.example i uzupełnij konfigurację.");
        }
        return {};
    };
    steps << environment;

    steps << gitStep(tr("Odczyt gałęzi"), {QStringLiteral("rev-parse"), QStringLiteral("--abbrev-ref"), QStringLiteral("HEAD")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się odczytać gałęzi."), result);
                         m_info.branch = result.standardOutput.trimmed();
                         if (m_info.branch == QLatin1String("HEAD"))
                             return tr("Repozytorium jest w stanie „detached HEAD”. Przełącz się na gałąź produkcyjną.");
                         return {};
                     });

    steps << gitStep(tr("Odczyt upstream"), {QStringLiteral("rev-parse"), QStringLiteral("--abbrev-ref"), QStringLiteral("--symbolic-full-name"), QStringLiteral("@{upstream}")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0) {
                             m_info.upstream.clear();
                             return tr("Bieżąca gałąź nie ma skonfigurowanego upstream (git branch --set-upstream-to=…).");
                         }
                         m_info.upstream = result.standardOutput.trimmed();
                         return {};
                     });

    steps << gitStep(tr("Sprawdzanie lokalnych zmian"), {QStringLiteral("status"), QStringLiteral("--porcelain"), QStringLiteral("--untracked-files=normal")},
                     [this, requireClean](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się odczytać stanu repozytorium."), result);
                         m_info.dirtyFiles = result.standardOutput.split(QLatin1Char('\n'), Qt::SkipEmptyParts);
                         if (requireClean && !m_info.dirtyFiles.isEmpty())
                             return tr("Kopia robocza zawiera lokalne zmiany. Zatwierdź je albo odłóż (git stash) przed operacją:\n%1")
                                 .arg(m_info.dirtyFiles.mid(0, 15).join(QLatin1Char('\n')));
                         return {};
                     });
    return steps;
}

QVector<Step> MainWindow::fetchSteps()
{
    return {gitStep(tr("Pobieranie zmian z serwera"), {QStringLiteral("fetch"), QStringLiteral("--prune"), QStringLiteral("--progress")},
                    [](const StepResult &result) -> QString {
                        return result.exitCode == 0 ? QString() : commandError(tr("Nie udało się pobrać zmian (git fetch). Sprawdź połączenie i dostęp do repozytorium."), result);
                    },
                    true)};
}

QVector<Step> MainWindow::stateSteps()
{
    const QString format = QStringLiteral("--format=%H%x1f%h%x1f%s%x1f%ci");
    QVector<Step> steps;

    steps << gitStep(tr("Odczyt bieżącej wersji"), {QStringLiteral("log"), QStringLiteral("-1"), format, QStringLiteral("HEAD")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się odczytać bieżącej wersji."), result);
                         const QStringList fields = result.standardOutput.trimmed().split(kFieldSeparator);
                         m_info.head = fields.value(0);
                         m_info.headShort = fields.value(1);
                         m_info.headSubject = fields.value(2);
                         m_info.headDate = fields.value(3);
                         return {};
                     });

    steps << gitStep(tr("Odczyt wersji do przywrócenia"), {QStringLiteral("log"), QStringLiteral("-1"), format, kPreviousRef, QStringLiteral("--")},
                     [this](const StepResult &result) -> QString {
                         const QStringList fields = result.exitCode == 0 ? result.standardOutput.trimmed().split(kFieldSeparator) : QStringList();
                         m_info.previous = fields.value(0);
                         m_info.previousShort = fields.value(1);
                         m_info.previousSubject = fields.value(2);
                         m_info.previousDate = fields.value(3);
                         return {};
                     });

    steps << gitStep(tr("Porównanie z serwerem"), {QStringLiteral("rev-list"), QStringLiteral("--left-right"), QStringLiteral("--count"), QStringLiteral("HEAD...@{upstream}")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się porównać wersji z serwerem."), result);
                         const QStringList counts = result.standardOutput.simplified().split(QLatin1Char(' '));
                         m_info.ahead = counts.value(0).toInt();
                         m_info.behind = counts.value(1).toInt();
                         return {};
                     });

    steps << gitStep(tr("Lista nowych zmian"), {QStringLiteral("log"), QStringLiteral("--max-count=200"), QStringLiteral("--format=%h%x1f%s%x1f%an%x1f%cr"), QStringLiteral("HEAD..@{upstream}")},
                     [this](const StepResult &result) -> QString {
                         m_info.pendingCommits.clear();
                         if (result.exitCode != 0)
                             return {};
                         for (const QString &line : result.standardOutput.split(QLatin1Char('\n'), Qt::SkipEmptyParts)) {
                             const QStringList fields = line.split(kFieldSeparator);
                             m_info.pendingCommits << tr("%1  %2  (%3, %4)").arg(fields.value(0), fields.value(1), fields.value(2), fields.value(3));
                         }
                         return {};
                     });
    return steps;
}

QVector<Step> MainWindow::deploySteps()
{
    Step build;
    build.title = tr("Budowanie i wdrażanie produkcji (make update-production)");
    build.program = QStringLiteral("make");
    build.arguments = []() { return QStringList{QStringLiteral("update-production")}; };
    build.cancellable = false;
    build.onFinished = [](const StepResult &result) -> QString {
        return result.exitCode == 0 ? QString() : commandError(tr("Budowanie lub wdrożenie nie powiodło się (kod %1).").arg(result.exitCode), result);
    };
    return {build};
}

void MainWindow::checkForUpdates()
{
    if (m_runner.isRunning())
        return;

    QVector<Step> steps = preflightSteps(false, false);
    steps << fetchSteps() << stateSteps();
    startOperation(Operation::Check, steps);
}

void MainWindow::startUpdate()
{
    if (m_runner.isRunning()) {
        if (m_operation == Operation::Check) {
            m_queued = Operation::Update;
            m_stepLabel->setText(tr("Aktualizacja rozpocznie się po zakończeniu sprawdzania…"));
        }
        return;
    }

    QString question = tr("Pobrać najnowszą wersję i wdrożyć produkcję?\n\n"
                          "Aplikacja zostanie przebudowana i uruchomiona ponownie; przez kilka minut może być niedostępna.");
    if (m_info.behind == 0)
        question = tr("Brak nowych zmian na serwerze.\n\nCzy mimo to przebudować i ponownie wdrożyć bieżącą wersję?");
    else if (m_info.behind > 0)
        question = tr("Dostępne nowe zmiany: %1.\n\n").arg(m_info.behind) + question;
    if (QMessageBox::question(this, tr("Aktualizacja Syncro"), question, QMessageBox::Yes | QMessageBox::No, QMessageBox::No) != QMessageBox::Yes)
        return;

    QVector<Step> steps = preflightSteps(true, true);
    steps << fetchSteps();

    steps << gitStep(tr("Zapamiętanie bieżącej wersji"), {QStringLiteral("rev-parse"), QStringLiteral("HEAD")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się odczytać bieżącej wersji."), result);
                         m_beforeUpdate = result.standardOutput.trimmed();
                         m_info.head = m_beforeUpdate;
                         return {};
                     });

    steps << gitStep(tr("Aktualizacja kodu (git pull --ff-only)"), {QStringLiteral("pull"), QStringLiteral("--ff-only"), QStringLiteral("--no-rebase")},
                     [](const StepResult &result) -> QString {
                         return result.exitCode == 0 ? QString()
                                                     : commandError(tr("Nie można zaktualizować przez fast-forward: lokalna gałąź rozeszła się z serwerem."), result);
                     },
                     true);

    Step record = gitStep(tr("Zapis wersji do przywrócenia"), {QStringLiteral("rev-parse"), QStringLiteral("HEAD")});
    record.onFinished = [this](const StepResult &result) -> QString {
        if (result.exitCode != 0)
            return commandError(tr("Nie udało się odczytać nowej wersji."), result);
        const QString after = result.standardOutput.trimmed();
        m_info.head = after;
        if (after == m_beforeUpdate) {
            appendLog(tr("Kod jest aktualny (%1). Przebudowa i wdrożenie mimo to.").arg(after.left(8)), false);
            return {};
        }
        QProcess updateRef;
        updateRef.setWorkingDirectory(m_repoPath);
        updateRef.start(QStringLiteral("git"), {QStringLiteral("update-ref"), kPreviousRef, m_beforeUpdate});
        if (!updateRef.waitForFinished(15000) || updateRef.exitCode() != 0)
            return tr("Nie udało się zapisać wersji do przywrócenia: %1").arg(QString::fromLocal8Bit(updateRef.readAllStandardError()).trimmed());
        appendLog(tr("Zaktualizowano %1 → %2. Poprzednia wersja zapisana do przywrócenia.").arg(m_beforeUpdate.left(8), after.left(8)), false);
        return {};
    };
    steps << record;

    steps << deploySteps() << stateSteps();
    startOperation(Operation::Update, steps);
}

void MainWindow::startRollback()
{
    if (m_runner.isRunning()) {
        if (m_operation == Operation::Check) {
            m_queued = Operation::Rollback;
            m_stepLabel->setText(tr("Przywracanie rozpocznie się po zakończeniu sprawdzania…"));
        }
        return;
    }

    if (m_info.previous.isEmpty()) {
        QMessageBox::information(this, tr("Przywracanie"),
                                 tr("Brak zapisanej poprzedniej wersji. Pojawi się po pierwszej udanej aktualizacji tym aktualizatorem."));
        return;
    }
    if (m_info.previous == m_info.head) {
        QMessageBox::information(this, tr("Przywracanie"), tr("Bieżąca wersja jest już wersją zapisaną do przywrócenia (%1).").arg(m_info.previousShort));
        return;
    }

    QMessageBox box(QMessageBox::Warning, tr("Przywróć poprzednią wersję"),
                    tr("Przywrócić wersję %1 („%2”, %3)?\n\nBieżąca wersja: %4 („%5”).")
                        .arg(m_info.previousShort, m_info.previousSubject, m_info.previousDate, m_info.headShort, m_info.headSubject),
                    QMessageBox::NoButton, this);
    box.setInformativeText(tr("Przywracany jest tylko kod aplikacji. Migracje i dane PostgreSQL NIE zostaną cofnięte."));
    QPushButton *confirm = box.addButton(tr("Przywróć"), QMessageBox::DestructiveRole);
    QPushButton *cancel = box.addButton(tr("Anuluj"), QMessageBox::RejectRole);
    box.setDefaultButton(cancel);
    box.exec();
    if (box.clickedButton() != confirm)
        return;

    const QString target = m_info.previous;
    QVector<Step> steps = preflightSteps(true, true);

    steps << gitStep(tr("Weryfikacja wersji do przywrócenia"), {QStringLiteral("rev-parse"), QStringLiteral("--verify"), QStringLiteral("-q"), kPreviousRef},
                     [target](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return tr("Zapisana wersja do przywrócenia zniknęła z repozytorium.");
                         if (result.standardOutput.trimmed() != target)
                             return tr("Wersja do przywrócenia zmieniła się w międzyczasie. Odśwież stan i spróbuj ponownie.");
                         return {};
                     });

    steps << gitStep(tr("Zapamiętanie bieżącej wersji"), {QStringLiteral("rev-parse"), QStringLiteral("HEAD")},
                     [this](const StepResult &result) -> QString {
                         if (result.exitCode != 0)
                             return commandError(tr("Nie udało się odczytać bieżącej wersji."), result);
                         m_beforeUpdate = result.standardOutput.trimmed();
                         return {};
                     });

    Step remember;
    remember.title = tr("Zapis wersji, z której wracamy");
    remember.program = QStringLiteral("git");
    remember.arguments = [this]() { return QStringList{QStringLiteral("update-ref"), kRollbackFromRef, m_beforeUpdate}; };
    steps << remember;

    steps << gitStep(tr("Przywracanie kodu"), {QStringLiteral("reset"), QStringLiteral("--hard"), target}, {}, true);
    steps << deploySteps() << stateSteps();
    startOperation(Operation::Rollback, steps);
}

void MainWindow::startRestart()
{
    if (m_runner.isRunning()) {
        if (m_operation == Operation::Check) {
            m_queued = Operation::Restart;
            m_stepLabel->setText(tr("Restart rozpocznie się po zakończeniu sprawdzania…"));
        }
        return;
    }

    if (QMessageBox::question(this, tr("Uruchom ponownie Syncro"),
                              tr("Zrestartować kontenery produkcyjne?\n\nKod nie jest pobierany ani przebudowywany; aplikacja będzie niedostępna przez kilkanaście sekund."),
                              QMessageBox::Yes | QMessageBox::No, QMessageBox::No) != QMessageBox::Yes)
        return;

    QVector<Step> steps = preflightSteps(false, true);
    Step restart;
    restart.title = tr("Restart kontenerów produkcyjnych (make restart-production)");
    restart.program = QStringLiteral("make");
    restart.arguments = []() { return QStringList{QStringLiteral("restart-production")}; };
    restart.cancellable = false;
    restart.onFinished = [](const StepResult &result) -> QString {
        return result.exitCode == 0 ? QString() : commandError(tr("Restart nie powiódł się (kod %1).").arg(result.exitCode), result);
    };
    steps << restart << stateSteps();
    startOperation(Operation::Restart, steps);
}

void MainWindow::startOperation(Operation operation, const QVector<Step> &steps)
{
    m_operation = operation;
    if (operation != Operation::Check)
        m_beforeUpdate.clear();

    if (!m_silentCheck) {
        m_log->appendPlainText(QString());
        appendLog(tr("=== %1: %2 ===").arg(operationName(operation), QDateTime::currentDateTime().toString(Qt::ISODate)), false);
    }
    if (operation != Operation::Check)
        openLogFile(operation);

    m_elapsed.start();
    m_elapsedLabel->clear();
    m_clockTimer->start();
    setBusy(true);
    m_runner.run(steps);
}

void MainWindow::appendLog(const QString &line, bool isError)
{
    if (m_silentCheck && !isError)
        return;

    const QString stamped = QStringLiteral("[%1] %2").arg(QTime::currentTime().toString(QStringLiteral("HH:mm:ss")), line);
    QScrollBar *scroll = m_log->verticalScrollBar();
    const bool atBottom = scroll->value() >= scroll->maximum() - 4;
    m_log->appendPlainText(stamped);
    if (atBottom)
        scroll->setValue(scroll->maximum());

    if (m_logFile.isOpen()) {
        m_logFile.write(stamped.toUtf8());
        m_logFile.write("\n");
        m_logFile.flush();
    }
}

void MainWindow::openLogFile(Operation operation)
{
    if (m_logFile.isOpen())
        m_logFile.close();
    QDir().mkpath(logDirectory());
    m_logFile.setFileName(QStringLiteral("%1/%2-%3.log")
                              .arg(logDirectory(), operationName(operation), QDateTime::currentDateTime().toString(QStringLiteral("yyyyMMdd-HHmmss"))));
    if (!m_logFile.open(QIODevice::WriteOnly | QIODevice::Text))
        appendLog(tr("Nie można zapisać dziennika do pliku %1").arg(m_logFile.fileName()), true);
    pruneLogs();
}

void MainWindow::pruneLogs()
{
    QDir directory(logDirectory());
    const QFileInfoList logs = directory.entryInfoList({QStringLiteral("*.log")}, QDir::Files, QDir::Time);
    for (int i = kMaxLogFiles; i < logs.size(); ++i)
        QFile::remove(logs.at(i).absoluteFilePath());
}

void MainWindow::setBusy(bool busy)
{
    const bool deploying = busy && m_operation != Operation::Check;
    m_checkButton->setEnabled(!busy);
    m_updateButton->setEnabled(!deploying);
    m_rollbackButton->setEnabled(!deploying && !m_info.previous.isEmpty());
    m_restartButton->setEnabled(!deploying);
    if (!busy)
        m_cancelButton->setEnabled(false);
    if (m_trayUpdate) {
        m_trayCheck->setEnabled(!busy);
        m_trayUpdate->setEnabled(!deploying);
        m_trayRollback->setEnabled(!deploying && !m_info.previous.isEmpty());
        m_trayRestart->setEnabled(!deploying);
    }
    if (busy && !m_silentCheck)
        m_progress->setRange(0, 0);
}

void MainWindow::refreshView()
{
    m_repoLabel->setText(m_repoPath);
    m_branchLabel->setText(m_info.branch.isEmpty() ? tr("—") : tr("%1 → %2").arg(m_info.branch, m_info.upstream.isEmpty() ? tr("(brak upstream)") : m_info.upstream));
    m_currentLabel->setText(m_info.headShort.isEmpty() ? tr("—") : QStringLiteral("<b>%1</b> %2<br><small>%3</small>")
                                                                         .arg(m_info.headShort, m_info.headSubject.toHtmlEscaped(), m_info.headDate));
    m_currentLabel->setTextFormat(Qt::RichText);

    if (m_info.behind < 0) {
        m_availableLabel->setText(tr("nie sprawdzono"));
        m_availableLabel->setStyleSheet(QString());
    } else if (m_info.behind == 0) {
        m_availableLabel->setText(m_info.ahead > 0 ? tr("aktualna (lokalnie %1 zmian niewysłanych na serwer)").arg(m_info.ahead) : tr("aktualna"));
        m_availableLabel->setStyleSheet(QStringLiteral("color: #1b7f3b; font-weight: 600;"));
    } else {
        m_availableLabel->setText(m_info.ahead > 0 ? tr("%1 nowych zmian, ale gałąź się rozeszła (%2 lokalnych) — aktualizacja fast-forward nie będzie możliwa")
                                                         .arg(m_info.behind)
                                                         .arg(m_info.ahead)
                                                   : tr("dostępnych nowych zmian: %1").arg(m_info.behind));
        m_availableLabel->setStyleSheet(QStringLiteral("color: #b35c00; font-weight: 600;"));
    }

    m_previousLabel->setTextFormat(Qt::RichText);
    m_previousLabel->setText(m_info.previous.isEmpty() ? tr("brak (pojawi się po pierwszej aktualizacji)")
                                                       : QStringLiteral("<b>%1</b> %2<br><small>%3</small>")
                                                             .arg(m_info.previousShort, m_info.previousSubject.toHtmlEscaped(), m_info.previousDate));

    if (m_info.dirtyFiles.isEmpty()) {
        m_cleanLabel->setText(m_info.head.isEmpty() ? tr("—") : tr("czysta"));
        m_cleanLabel->setStyleSheet(QString());
        m_cleanLabel->setToolTip(QString());
    } else {
        m_cleanLabel->setText(tr("lokalne zmiany w %1 plikach — aktualizacja zablokowana").arg(m_info.dirtyFiles.size()));
        m_cleanLabel->setStyleSheet(QStringLiteral("color: #b00020; font-weight: 600;"));
        m_cleanLabel->setToolTip(m_info.dirtyFiles.mid(0, 30).join(QLatin1Char('\n')));
    }

    m_pendingList->clear();
    if (m_info.pendingCommits.isEmpty())
        m_pendingList->addItem(m_info.behind < 0 ? tr("Kliknij „Sprawdź aktualizacje”.") : tr("Brak nowych zmian."));
    else
        m_pendingList->addItems(m_info.pendingCommits);

    m_updateButton->setText(m_info.behind > 0 ? tr("Aktualizuj (%1)").arg(m_info.behind) : tr("Aktualizuj"));
    if (!m_runner.isRunning())
        setBusy(false);

    if (m_tray)
        m_tray->setToolTip(m_info.behind > 0 ? tr("Syncro: dostępnych zmian %1").arg(m_info.behind) : tr("Syncro: wersja %1").arg(m_info.headShort));
}

void MainWindow::requestQuit()
{
    if (m_runner.isRunning() && !m_runner.canCancel()) {
        showAndRaise();
        QMessageBox::warning(this, tr("Trwa wdrażanie"), tr("Nie można zamknąć aktualizatora w trakcie wdrażania. Poczekaj na zakończenie."));
        return;
    }
    if (m_runner.isRunning()) {
        m_quitRequested = true;
        m_runner.cancel();
        return;
    }
    qApp->quit();
}

void MainWindow::closeEvent(QCloseEvent *event)
{
    if (m_tray) {
        hide();
        event->ignore();
        return;
    }
    if (m_runner.isRunning() && !m_runner.canCancel()) {
        QMessageBox::warning(this, tr("Trwa wdrażanie"), tr("Nie można zamknąć aktualizatora w trakcie wdrażania. Poczekaj na zakończenie."));
        event->ignore();
        return;
    }
    QMainWindow::closeEvent(event);
}
