#include "MainWindow.h"

#include <QApplication>
#include <QCommandLineParser>
#include <QDir>
#include <QFileInfo>
#include <QIcon>
#include <QLocalServer>
#include <QLocalSocket>
#include <QMessageBox>
#include <QSettings>
#include <QTimer>

#include <unistd.h>

namespace {

QString serverName()
{
    return QStringLiteral("syncro-updater-%1").arg(getuid());
}

bool isRepository(const QString &path)
{
    return !path.isEmpty() && QFileInfo::exists(QDir(path).filePath(QStringLiteral(".git")));
}

QString resolveRepository(const QString &argument, QSettings &settings)
{
    if (!argument.isEmpty()) {
        const QString path = QDir(argument).absolutePath();
        settings.setValue(QStringLiteral("repoPath"), path);
        return path;
    }

    const QStringList candidates{
        qEnvironmentVariable("SYNCRO_REPO"),
        settings.value(QStringLiteral("repoPath")).toString(),
        QStringLiteral(SYNCRO_DEFAULT_REPO),
        // Running straight from tools/syncro-updater/build inside the checkout.
        QDir(QCoreApplication::applicationDirPath()).absoluteFilePath(QStringLiteral("../../..")),
    };
    for (const QString &candidate : candidates) {
        if (isRepository(candidate))
            return QDir(candidate).absolutePath();
    }
    return {};
}

// Forwards the command to an already running instance; returns true if one answered.
bool forwardToRunningInstance(const QString &command)
{
    QLocalSocket socket;
    socket.connectToServer(serverName());
    if (!socket.waitForConnected(500))
        return false;
    socket.write(command.toUtf8());
    socket.flush();
    socket.waitForBytesWritten(1000);
    socket.disconnectFromServer();
    return true;
}

} // namespace

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    QApplication::setApplicationName(QStringLiteral("syncro-updater"));
    QApplication::setOrganizationName(QStringLiteral("Syncro"));
    QApplication::setApplicationDisplayName(QStringLiteral("Aktualizator Syncro"));
    QApplication::setApplicationVersion(QStringLiteral(SYNCRO_UPDATER_VERSION));
    QApplication::setWindowIcon(QIcon(QStringLiteral(":/syncro-update.svg")));
    QApplication::setDesktopFileName(QStringLiteral("syncro-updater"));

    QCommandLineParser parser;
    parser.setApplicationDescription(QStringLiteral("Aktualizacja i przywracanie produkcyjnej instalacji Syncro."));
    parser.addHelpOption();
    parser.addVersionOption();
    const QCommandLineOption repoOption(QStringLiteral("repo"), QStringLiteral("Katalog repozytorium Syncro."), QStringLiteral("path"));
    const QCommandLineOption trayOption(QStringLiteral("tray"), QStringLiteral("Uruchom zminimalizowany do zasobnika."));
    const QCommandLineOption updateOption(QStringLiteral("update"), QStringLiteral("Od razu rozpocznij aktualizację."));
    const QCommandLineOption rollbackOption(QStringLiteral("rollback"), QStringLiteral("Od razu rozpocznij przywracanie poprzedniej wersji."));
    const QCommandLineOption restartOption(QStringLiteral("restart"), QStringLiteral("Od razu zrestartuj kontenery produkcyjne."));
    const QCommandLineOption intervalOption(QStringLiteral("check-interval"),
                                            QStringLiteral("Co ile minut sprawdzać aktualizacje w tle (0 wyłącza, domyślnie 60)."),
                                            QStringLiteral("minutes"));
    parser.addOptions({repoOption, trayOption, updateOption, rollbackOption, restartOption, intervalOption});
    parser.process(app);

    const QString command = parser.isSet(updateOption)   ? QStringLiteral("update")
                            : parser.isSet(rollbackOption) ? QStringLiteral("rollback")
                            : parser.isSet(restartOption)  ? QStringLiteral("restart")
                            : parser.isSet(trayOption)     ? QStringLiteral("check")
                                                           : QStringLiteral("show");
    if (forwardToRunningInstance(command))
        return 0;

    QSettings settings;
    const QString repo = resolveRepository(parser.value(repoOption), settings);
    if (repo.isEmpty()) {
        QMessageBox::critical(nullptr, QStringLiteral("Aktualizator Syncro"),
                              QStringLiteral("Nie znaleziono repozytorium Syncro.\n\nUruchom z opcją --repo <katalog> albo ustaw zmienną SYNCRO_REPO."));
        return 1;
    }

    bool intervalOk = false;
    int interval = parser.value(intervalOption).toInt(&intervalOk);
    if (!intervalOk)
        interval = settings.value(QStringLiteral("checkIntervalMinutes"), 60).toInt();
    else
        settings.setValue(QStringLiteral("checkIntervalMinutes"), interval);

    MainWindow window(repo, qMax(0, interval));
    QApplication::setQuitOnLastWindowClosed(!window.hasTray());

    QLocalServer server;
    QLocalServer::removeServer(serverName());
    server.setSocketOptions(QLocalServer::UserAccessOption);
    if (server.listen(serverName())) {
        QObject::connect(&server, &QLocalServer::newConnection, &window, [&server, &window]() {
            while (QLocalSocket *socket = server.nextPendingConnection()) {
                QObject::connect(socket, &QLocalSocket::readyRead, &window, [socket, &window]() {
                    const QString received = QString::fromUtf8(socket->readAll()).trimmed();
                    if (received == QLatin1String("show") || received == QLatin1String("update") ||
                        received == QLatin1String("rollback") || received == QLatin1String("restart") || received == QLatin1String("check"))
                        window.handleCommand(received);
                });
                QObject::connect(socket, &QLocalSocket::disconnected, socket, &QObject::deleteLater);
            }
        });
    }

    if (!parser.isSet(trayOption) || !window.hasTray())
        window.show();
    if (command == QLatin1String("update") || command == QLatin1String("rollback") || command == QLatin1String("restart")) {
        // Queued behind the initial state check so the confirmation shows real versions.
        QTimer::singleShot(0, &window, [&window, command]() { window.handleCommand(command); });
    }

    return app.exec();
}
