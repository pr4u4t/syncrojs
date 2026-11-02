#include "classestato.h"
#include "formui.h"
#include "miscpages.h"
#include "uihooks.h"
#include "stagglobals.h"
#include "stagapp.h"
#include "inistore.h"

#include <QCommandLineParser>
#include <cstdio>

#include <QDir>
#include <QTimer>
#include <QFile>
#include <QFileInfo>
#include <QFont>
#include <QSqlDatabase>
#include <QSqlError>
#include <QGuiApplication>
#include <QImage>
#include <QQmlApplicationEngine>
#include <QQmlComponent>
#include <QQmlContext>
#include <QQuickItem>
#include <QQuickWindow>
#include <QTest>

// Like every other key of the ini, a missing one is written with its default: there it can be changed.
static QString guiStyleOfIni()
{
    return stag::getFileINIString(QStringLiteral("Gui"), QStringLiteral("Style"), QStringLiteral("legacy"));
}

// [Gui] Style of stag.ini: "normal" is the new look; no key at all, or anything else, keeps the VB one.
static bool isModernGui(const QString &style)
{
    const QString wanted = style.trimmed().toLower();
    if (!wanted.isEmpty() && wanted != QLatin1String("legacy") && wanted != QLatin1String("normal"))
        qWarning().noquote() << "Unknown GUI style, using legacy:" << style;
    return wanted == QLatin1String("normal");
}

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

// What is missing to start for real, empty when nothing is.
static QString startProblem(const QString &dataDir, const QString &sqlitePath)
{
    if (!QFile::exists(QDir(dataDir).filePath(QStringLiteral("stag.ini"))))
        return QStringLiteral("No stag.ini in the data directory: ") + QDir(dataDir).absolutePath();
    if (!QFile::exists(sqlitePath))
        return QStringLiteral("Database not found: ") + QFileInfo(sqlitePath).absoluteFilePath();
    QString problem;
    {
        QSqlDatabase db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("stag-check"));
        db.setDatabaseName(sqlitePath);
        db.setConnectOptions(QStringLiteral("QSQLITE_OPEN_READONLY"));
        if (!db.open())
            problem = QStringLiteral("Cannot open the database: ") + db.lastError().text();
        else if (!db.tables().contains(QStringLiteral("MESSAGGI")))
            problem = QStringLiteral("Not a STAG database, it has no MESSAGGI table: ") + QFileInfo(sqlitePath).absoluteFilePath();
        db.close();
    }
    QSqlDatabase::removeDatabase(QStringLiteral("stag-check"));
    return problem;
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

    // The forms ask for the font of Windows 9x; Qt searches for it at every start and warns.
    QFont::insertSubstitution(QStringLiteral("MS Sans Serif"), QStringLiteral("Arial"));

    QCommandLineParser parser;
    parser.addHelpOption();
    QCommandLineOption dataOption(QStringLiteral("data"), QStringLiteral("Directory with stag.ini and the .dat files"), QStringLiteral("dir"), QStringLiteral("."));
    QCommandLineOption dbOption(QStringLiteral("db"), QStringLiteral("SQLite database made by stag-migrate-dbf"), QStringLiteral("file"), QStringLiteral("stag.sqlite"));
    QCommandLineOption plcOption(QStringLiteral("plc"), QStringLiteral("Override com_plc_lan address"), QStringLiteral("host:port"));
    QCommandLineOption renderOption(QStringLiteral("render-forms"), QStringLiteral("Write every form to a PNG in this directory and exit"), QStringLiteral("dir"));
    QCommandLineOption shellOption(QStringLiteral("render-shell"), QStringLiteral("Walk the function keys without the PLC, write PNGs to this directory and exit"), QStringLiteral("dir"));
    QCommandLineOption languageOption(QStringLiteral("language"), QStringLiteral("With a render option: MESSAGGI language number"), QStringLiteral("number"), QStringLiteral("1"));
    QCommandLineOption writesOption(QStringLiteral("enable-plc-writes"), QStringLiteral("Allow writing to the PLC; without it the program only reads"));
    QCommandLineOption screenshotOption(QStringLiteral("screenshot"), QStringLiteral("Normal start: write the window to this PNG after 2 s and quit"), QStringLiteral("file"));
    QCommandLineOption sizeOption(QStringLiteral("window-size"), QStringLiteral("Normal start: size of the window"), QStringLiteral("WxH"));
    QCommandLineOption guiOption(QStringLiteral("gui"), QStringLiteral("Look of the program instead of [Gui] Style of stag.ini"), QStringLiteral("legacy|normal"));
    parser.addOption(guiOption);
    parser.addOptions({dataOption, dbOption, plcOption, renderOption, shellOption, languageOption, writesOption, screenshotOption, sizeOption});
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
        // After load(), which sets the data directory the ini is read from.
        ui.setModernGui(isModernGui(parser.isSet(guiOption) ? parser.value(guiOption) : parser.isSet(dataOption) ? guiStyleOfIni() : QString()));
        if (parser.isSet(dbOption)) {
            QString error;
            if (!Messages::instance().load(parser.value(dbOption), parser.value(languageOption).toInt(), &error))
                qWarning().noquote() << error;
        }
        return parser.isSet(renderOption) ? renderForms(parser.value(renderOption), &ui) : renderShell(parser.value(shellOption), &ui, &staBar);
    }

    StagApp::Options options;
    // What is not on the command line comes from next to the program, where the build puts it, or else from
    // the current directory.
    const QDir programDir(QCoreApplication::applicationDirPath());
    if (parser.isSet(dataOption))
        options.dataDir = parser.value(dataOption);
    else if (QFile::exists(programDir.filePath(QStringLiteral("dati/stag.ini"))))
        options.dataDir = programDir.filePath(QStringLiteral("dati"));
    else
        options.dataDir = QDir::currentPath();
    if (parser.isSet(dbOption) || !QFile::exists(programDir.filePath(QStringLiteral("stag.sqlite"))))
        options.sqlitePath = parser.value(dbOption);
    else
        options.sqlitePath = programDir.filePath(QStringLiteral("stag.sqlite"));
    options.plcWrites = parser.isSet(writesOption);
    if (parser.isSet(plcOption)) {
        const QString plc = parser.value(plcOption);
        const int colon = plc.lastIndexOf(u':');
        options.plcHostOverride = colon < 0 ? plc : plc.left(colon);
        if (colon >= 0)
            options.plcPortOverride = quint16(plc.mid(colon + 1).toUInt());
    }

    // Without these the program would create an empty database and ini in the current directory and start
    // with no texts, no orders and language 0.
    if (const QString problem = startProblem(options.dataDir, options.sqlitePath); !problem.isEmpty()) {
        fprintf(stderr, "%s\n\nUsage: stag --data <directory with stag.ini> --db <stag.sqlite> [--plc host:port] [--enable-plc-writes]\n", qPrintable(problem));
        return 2;
    }

    StagApp stag(options);
    // ui before the engine: QML still evaluates bindings on stagUi while the engine is being destroyed.
    FormUi ui(stag.staBar());
    // Models are registered after load(), which start() runs.
    QQmlApplicationEngine engine;
    ui.addModel(QStringLiteral("Pag_pro"), stag.pagPro());
    ui.addModel(QStringLiteral("Pag_lis_ord"), stag.pagLisOrd());
    ui.addModel(QStringLiteral("Pag_lis_ese"), stag.pagLisEse());
    engine.rootContext()->setContextProperty(QStringLiteral("staBar"), stag.staBar());
    engine.rootContext()->setContextProperty(QStringLiteral("stagUi"), &ui);
    QObject::connect(&engine, &QQmlApplicationEngine::objectCreationFailed, &app, [] { QCoreApplication::exit(1); }, Qt::QueuedConnection);
    stag.start();
    // After start(), which sets the data directory the ini is read from.
    ui.setModernGui(isModernGui(parser.isSet(guiOption) ? parser.value(guiOption) : guiStyleOfIni()));
    for (auto model = stag.pageModels().cbegin(); model != stag.pageModels().cend(); ++model)
        ui.addModel(model.key(), model.value());
    QObject::connect(stag.pagMsg(), &PagMsgModel::opened, &ui, [&ui] { emit ui.overlayRequested(QStringLiteral("Pag_msg")); });
    stag::languageChangedHook = [&ui] { emit ui.languageChanged(); };
    engine.loadFromModule("Stag", "Main");
    QObject::connect(&app, &QCoreApplication::aboutToQuit, &stag, &StagApp::shutdown);
    if (parser.isSet(sizeOption)) {
        const QStringList size = parser.value(sizeOption).split(u'x');
        if (auto *window = qobject_cast<QQuickWindow *>(engine.rootObjects().value(0)); window && size.size() == 2)
            window->resize(size[0].toInt(), size[1].toInt());
    }
    if (parser.isSet(screenshotOption)) {
        QTimer::singleShot(2000, &app, [&] {
            if (auto *window = qobject_cast<QQuickWindow *>(engine.rootObjects().value(0)))
                window->grabWindow().save(parser.value(screenshotOption));
            QCoreApplication::quit();
        });
    }
    const int code = app.exec();
    // These capture ui and the models, which end with this function.
    stag::languageChangedHook = nullptr;
    stag::messagWinHook = nullptr;
    stag::messagWinTextHook = nullptr;
    stag::messaggioLivHook = nullptr;
    return code;
}
