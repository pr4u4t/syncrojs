#include "classestato.h"
#include "formui.h"
#include "stagglobals.h"
#include "stagapp.h"

#include <QCommandLineParser>
#include <QDir>
#include <QGuiApplication>
#include <QImage>
#include <QQmlApplicationEngine>
#include <QQmlComponent>
#include <QQmlContext>
#include <QQuickItem>
#include <QQuickWindow>
#include <QTest>

// Fails the render runs below on any QML error or warning.
static void watchWarnings(QQmlEngine &engine, int &failures)
{
    QObject::connect(&engine, &QQmlEngine::warnings, &engine, [&failures](const QList<QQmlError> &warnings) {
        for (const QQmlError &warning : warnings) {
            qCritical().noquote() << warning.toString();
            ++failures;
        }
    });
}

// Loads every generated form and writes it to <dir>/<form>.png.
static int renderForms(const QString &outputDir, FormUi *ui)
{
    QDir().mkpath(outputDir);
    const QDir forms(QStringLiteral(":/qt/qml/Stag/qml/forms"));
    int failures = 0;
    for (const QString &file : forms.entryList({QStringLiteral("*.qml")})) {
        QQmlEngine engine;
        watchWarnings(engine, failures);
        engine.rootContext()->setContextProperty(QStringLiteral("stagUi"), ui);
        QQmlComponent component(&engine, QUrl(QStringLiteral("qrc") + forms.filePath(file)));
        QScopedPointer<QObject> object(component.create());
        auto *item = qobject_cast<QQuickItem *>(object.data());
        if (!item) {
            qCritical().noquote() << file << component.errorString();
            ++failures;
            continue;
        }
        QQuickWindow window;
        window.resize(int(item->width()), int(item->height()));
        item->setParentItem(window.contentItem());
        window.show();
        QTest::qWait(30);
        const QString name = file.chopped(4) + QStringLiteral(".png");
        if (!window.grabWindow().save(QDir(outputDir).filePath(name))) {
            qCritical().noquote() << "cannot save" << name;
            ++failures;
        }
    }
    return failures == 0 ? 0 : 1;
}

// Starts the shell without the PLC loop, presses each function key from the main page and
// writes what it shows to <dir>/shell_<key>.png; the page must change where nav.js says so.
static int renderShell(const QString &outputDir, FormUi *ui, StaBarModel *staBar)
{
    QDir().mkpath(outputDir);
    QQmlApplicationEngine engine;
    int failures = 0;
    watchWarnings(engine, failures);
    engine.rootContext()->setContextProperty(QStringLiteral("staBar"), staBar);
    engine.rootContext()->setContextProperty(QStringLiteral("stagUi"), ui);
    engine.loadFromModule("Stag", "Main");
    if (engine.rootObjects().isEmpty())
        return 1;
    auto *window = qobject_cast<QQuickWindow *>(engine.rootObjects().first());
    QTest::qWait(100);
    const auto capture = [&](const QString &name) {
        QTest::qWait(60);
        engine.collectGarbage();
        window->grabWindow().save(QDir(outputDir).filePath(QStringLiteral("shell_%1.png").arg(name)));
    };
    const auto typeText = [&](const char *text) {
        for (; *text; ++text)
            QTest::keyClick(window, *text);
    };
    capture(QStringLiteral("start"));
    for (int key = 1; key <= 9; ++key) {
        QTest::keyClick(window, Qt::Key(Qt::Key_F1 + key - 1));
        capture(QStringLiteral("F%1_%2").arg(key).arg(window->property("overlayForm").toString() + window->property("currentForm").toString()));
        QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Pag_pro")));
        QTest::qWait(20);
    }
    // Report panel of the executed orders.
    QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Pag_lis_ese")));
    QTest::qWait(60);
    QTest::keyClick(window, Qt::Key_F7);
    capture(QStringLiteral("Pag_lis_ese_report"));
    for (const char *form : {"Pag_all", "All_mac", "Fre_mac", "Sto_all", "Onde", "Pattern"}) {
        QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QString::fromLatin1(form)));
        capture(QString::fromLatin1(form));
    }
    // A real edit on Onde: new record, typing and Return, then Escape so nothing is saved.
    QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Onde")));
    QTest::qWait(60);
    QTest::keyClick(window, Qt::Key_F1);
    QTest::qWait(30);
    typeText("bc");
    QTest::keyClick(window, Qt::Key_Return);
    QTest::qWait(30);
    typeText("abc");
    QTest::keyClick(window, Qt::Key_Return);
    QTest::qWait(30);
    typeText("x12y");
    capture(QStringLiteral("Onde_edit"));
    QTest::keyClick(window, Qt::Key_Escape);
    capture(QStringLiteral("Onde_after_escape"));
    QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Abilitazioni")));
    capture(QStringLiteral("Abilitazioni"));
    // The order form on real data: showing an order, then F1 refused in automatic.
    QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Ins_ord")));
    capture(QStringLiteral("Ins_ord"));
    QTest::keyClick(window, Qt::Key_F6);
    capture(QStringLiteral("Ins_ord_next"));
    QTest::keyClick(window, Qt::Key_F1);
    capture(QStringLiteral("Ins_ord_refused"));
    // A real edit by keyboard, with the machine out of automatic; Escape so nothing is saved.
    if (stag::sta_tag)
        stag::sta_tag->setSpdeOK(false);
    QTest::keyClick(window, Qt::Key_F2);
    QTest::qWait(30);
    typeText("abc");
    for (int field = 0; field < 3; ++field) {
        QTest::keyClick(window, Qt::Key_Return);
        QTest::qWait(30);
    }
    typeText("2");
    QTest::keyClick(window, Qt::Key_Return);
    QTest::qWait(30);
    QTest::keyClick(window, Qt::Key_Right);
    capture(QStringLiteral("Ins_ord_edit"));
    QTest::keyClick(window, Qt::Key_Escape);
    capture(QStringLiteral("Ins_ord_cancelled"));
    if (stag::sta_tag)
        stag::sta_tag->setSpdeOK(true);
    // A real dialog over the order list: F3 fills in the selected order, wrong numbers keep it open.
    QMetaObject::invokeMethod(window, "showForm", Q_ARG(QVariant, QStringLiteral("Pag_lis_ord")));
    QTest::qWait(60);
    QTest::keyClick(window, Qt::Key_F3);
    capture(QStringLiteral("Del_ord_open"));
    typeText("9");
    QTest::keyClick(window, Qt::Key_F11);
    capture(QStringLiteral("Del_ord_refused"));
    QTest::keyClick(window, Qt::Key_Escape);
    capture(QStringLiteral("Del_ord_closed"));
    if (window->property("overlayForm").toString() != QString()) {
        qCritical("Escape did not close the dialog");
        ++failures;
    }
    QTest::keyClick(window, Qt::Key_Down);
    capture(QStringLiteral("list_after_dialog"));
    return failures == 0 ? 0 : 1;
}

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
    QCommandLineOption renderOption(QStringLiteral("render-forms"), QStringLiteral("Write every form to a PNG in this directory and exit"), QStringLiteral("dir"));
    QCommandLineOption shellOption(QStringLiteral("render-shell"), QStringLiteral("Walk the function keys without the PLC, write PNGs to this directory and exit"), QStringLiteral("dir"));
    QCommandLineOption languageOption(QStringLiteral("language"), QStringLiteral("With a render option: MESSAGGI language number"), QStringLiteral("number"), QStringLiteral("1"));
    QCommandLineOption writesOption(QStringLiteral("enable-plc-writes"), QStringLiteral("Allow writing to the PLC; without it the program only reads"));
    parser.addOptions({dataOption, dbOption, plcOption, renderOption, shellOption, languageOption, writesOption});
    parser.process(app);
    if (parser.isSet(renderOption) || parser.isSet(shellOption)) {
        // With --data the shell shows those orders; the PLC loop is never started here.
        StagApp::Options renderOptions;
        renderOptions.dataDir = parser.value(dataOption);
        renderOptions.sqlitePath = parser.value(dbOption);
        StagApp data(renderOptions);
        StaBarModel &staBar = *data.staBar();
        FormUi ui(&staBar);
        if (parser.isSet(dataOption)) {
            data.load();
            ui.addModel(QStringLiteral("Pag_pro"), data.pagPro());
            ui.addModel(QStringLiteral("Pag_lis_ord"), data.pagLisOrd());
            ui.addModel(QStringLiteral("Pag_lis_ese"), data.pagLisEse());
            for (auto model = data.pageModels().cbegin(); model != data.pageModels().cend(); ++model)
                ui.addModel(model.key(), model.value());
        }
        if (parser.isSet(dbOption)) {
            QString error;
            if (!Messages::instance().load(parser.value(dbOption), parser.value(languageOption).toInt(), &error))
                qWarning().noquote() << error;
        }
        return parser.isSet(renderOption) ? renderForms(parser.value(renderOption), &ui) : renderShell(parser.value(shellOption), &ui, &staBar);
    }

    StagApp::Options options;
    options.dataDir = parser.value(dataOption);
    options.sqlitePath = parser.value(dbOption);
    options.plcWrites = parser.isSet(writesOption);
    if (parser.isSet(plcOption)) {
        const QString plc = parser.value(plcOption);
        const int colon = plc.lastIndexOf(u':');
        options.plcHostOverride = colon < 0 ? plc : plc.left(colon);
        if (colon >= 0)
            options.plcPortOverride = quint16(plc.mid(colon + 1).toUInt());
    }

    StagApp stag(options);
    // Models are registered after load(), which start() runs.
    QQmlApplicationEngine engine;
    FormUi ui(stag.staBar());
    ui.addModel(QStringLiteral("Pag_pro"), stag.pagPro());
    ui.addModel(QStringLiteral("Pag_lis_ord"), stag.pagLisOrd());
    ui.addModel(QStringLiteral("Pag_lis_ese"), stag.pagLisEse());
    engine.rootContext()->setContextProperty(QStringLiteral("staBar"), stag.staBar());
    engine.rootContext()->setContextProperty(QStringLiteral("stagUi"), &ui);
    QObject::connect(&engine, &QQmlApplicationEngine::objectCreationFailed, &app, [] { QCoreApplication::exit(1); }, Qt::QueuedConnection);
    stag.start();
    for (auto model = stag.pageModels().cbegin(); model != stag.pageModels().cend(); ++model)
        ui.addModel(model.key(), model.value());
    engine.loadFromModule("Stag", "Main");
    QObject::connect(&app, &QCoreApplication::aboutToQuit, &stag, &StagApp::shutdown);
    return app.exec();
}
