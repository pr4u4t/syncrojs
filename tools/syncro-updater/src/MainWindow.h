#pragma once

#include "CommandRunner.h"

#include <QElapsedTimer>
#include <QFile>
#include <QMainWindow>
#include <QStringList>
#include <QSystemTrayIcon>

class QAction;
class QLabel;
class QListWidget;
class QPlainTextEdit;
class QProgressBar;
class QPushButton;
class QTimer;

struct RepoInfo
{
    QString branch;
    QString upstream;
    QString head;
    QString headShort;
    QString headSubject;
    QString headDate;
    QString previous;
    QString previousShort;
    QString previousSubject;
    QString previousDate;
    int behind = -1;
    int ahead = 0;
    QStringList pendingCommits;
    QStringList dirtyFiles;
};

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    enum class Operation { None, Check, Update, Rollback, Restart };

    MainWindow(const QString &repoPath, int checkIntervalMinutes, QWidget *parent = nullptr);

    bool hasTray() const;

public slots:
    void handleCommand(const QString &command);
    void showAndRaise();
    void checkForUpdates();
    void startUpdate();
    void startRollback();
    void startRestart();

protected:
    void closeEvent(QCloseEvent *event) override;

private:
    void buildUi();
    void buildTray();
    void refreshView();
    void setBusy(bool busy);
    void startOperation(Operation operation, const QVector<Step> &steps);
    void appendLog(const QString &line, bool isError);
    void openLogFile(Operation operation);
    void pruneLogs();
    void requestQuit();

    QString repoError() const;
    Step gitStep(const QString &title, const QStringList &arguments, std::function<QString(const StepResult &)> onFinished = {}, bool echo = false);
    QVector<Step> preflightSteps(bool requireClean, bool requireMake);
    QVector<Step> fetchSteps();
    QVector<Step> stateSteps();
    QVector<Step> deploySteps();
    QVector<Step> selfUpdateSteps();
    void restartSelf();

    QString m_repoPath;
    RepoInfo m_info;
    QString m_beforeUpdate;
    Operation m_operation = Operation::None;
    Operation m_queued = Operation::None;
    bool m_silentCheck = false;
    bool m_quitRequested = false;
    bool m_updaterReinstalled = false;
    int m_lastNotifiedBehind = 0;

    CommandRunner m_runner;
    QElapsedTimer m_elapsed;
    QFile m_logFile;
    QTimer *m_checkTimer = nullptr;
    QTimer *m_clockTimer = nullptr;

    QLabel *m_repoLabel = nullptr;
    QLabel *m_branchLabel = nullptr;
    QLabel *m_currentLabel = nullptr;
    QLabel *m_availableLabel = nullptr;
    QLabel *m_previousLabel = nullptr;
    QLabel *m_cleanLabel = nullptr;
    QLabel *m_stepLabel = nullptr;
    QLabel *m_elapsedLabel = nullptr;
    QListWidget *m_pendingList = nullptr;
    QPlainTextEdit *m_log = nullptr;
    QProgressBar *m_progress = nullptr;
    QPushButton *m_checkButton = nullptr;
    QPushButton *m_updateButton = nullptr;
    QPushButton *m_rollbackButton = nullptr;
    QPushButton *m_restartButton = nullptr;
    QPushButton *m_cancelButton = nullptr;

    QSystemTrayIcon *m_tray = nullptr;
    QAction *m_trayUpdate = nullptr;
    QAction *m_trayRollback = nullptr;
    QAction *m_trayRestart = nullptr;
    QAction *m_trayCheck = nullptr;
};
