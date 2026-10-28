#include "stagapp.h"

#include <QCommandLineParser>
#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include <QQmlContext>

int main(int argc, char *argv[])
{
    QGuiApplication app(argc, argv);
    QGuiApplication::setApplicationName(QStringLiteral("stag"));
    QGuiApplication::setOrganizationName(QStringLiteral("Fosber"));

    QCommandLineParser parser;
    parser.addHelpOption();
    QCommandLineOption dataOption(QStringLiteral("data"), QStringLiteral("Directory with stag.ini and the .dat files"), QStringLiteral("dir"), QStringLiteral("."));
    QCommandLineOption dbOption(QStringLiteral("db"), QStringLiteral("SQLite database made by stag-migrate-dbf"), QStringLiteral("file"), QStringLiteral("stag.sqlite"));
    QCommandLineOption plcOption(QStringLiteral("plc"), QStringLiteral("Override com_plc_lan address"), QStringLiteral("host:port"));
    parser.addOptions({dataOption, dbOption, plcOption});
    parser.process(app);

    StagApp::Options options;
    options.dataDir = parser.value(dataOption);
    options.sqlitePath = parser.value(dbOption);
    if (parser.isSet(plcOption)) {
        const QString plc = parser.value(plcOption);
        const int colon = plc.lastIndexOf(u':');
        options.plcHostOverride = colon < 0 ? plc : plc.left(colon);
        if (colon >= 0)
            options.plcPortOverride = quint16(plc.mid(colon + 1).toUInt());
    }

    StagApp stag(options);
    QQmlApplicationEngine engine;
    engine.rootContext()->setContextProperty(QStringLiteral("staBar"), stag.staBar());
    QObject::connect(&engine, &QQmlApplicationEngine::objectCreationFailed, &app, [] { QCoreApplication::exit(1); }, Qt::QueuedConnection);
    engine.loadFromModule("Stag", "Main");
    stag.start();
    QObject::connect(&app, &QCoreApplication::aboutToQuit, &stag, &StagApp::shutdown);
    return app.exec();
}
