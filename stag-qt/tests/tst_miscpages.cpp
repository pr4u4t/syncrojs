#include "abilitazionimodel.h"
#include "classestato.h"
#include "stabarmodel.h"
#include "collezioneordini.h"
#include "finsclient.h"
#include "finssimulator.h"
#include "inibas.h"
#include "inifile.h"
#include "inistore.h"
#include "messages.h"
#include "parcfgschtag.h"
#include "parcurtag.h"
#include "parmac.h"
#include "plclink.h"
#include "schtagpages.h"
#include "setparmodels.h"
#include "miscpages.h"
#include "stabarform.h"
#include "spdelink.h"
#include "pagpromodel.h"
#include "stagglobals.h"
#include "fixedrecord.h"
#include "stagrecords.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QFileInfo>
#include <QRegularExpression>
#include <QSignalSpy>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>
#include <QUdpSocket>

using namespace stag;

static constexpr char STX_ST_SPDE = 2;
static constexpr char ETX_ST_SPDE = 3;

class TestMiscPages : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    FinsSimulator *m_plc = nullptr;
    FinsClient *m_fins = nullptr;
    ClasseStato *m_state = nullptr;
    CollezioneOrdini *m_orders = nullptr;
    PlcLink *m_link = nullptr;
    SchTagConfig *m_config = nullptr;
    QUdpSocket *m_board = nullptr;
    QSqlDatabase m_db;
    QStringList m_frames;
    QStringList m_written;
    QString m_message;
    QByteArray m_reply = QByteArray(1, ACK_ST);

    // Runs the queues as tim_tag_plc does and lets the answers arrive.
    void tick()
    {
        m_link->tick();
        m_link->tick();
    }
    void machineReady()
    {
        QVector<quint16> dm{0x0071, 0x0000};
        const int bit = ClasseStato::SCH_TAG_LIV_1_PRONTA;
        dm[bit / 16] |= quint16(1 << (bit % 16));
        m_plc->setDm(3600, dm);
        m_plc->setDm(3602, m_orders->item(1)->stag_id().toUShort());
        m_plc->setDm(3620, m_orders->item(2)->stag_id().toUShort());
        m_state->setComOK(true);
        m_state->aggiorna(*m_fins);
        com_ini_sch_tag(1) = true;
        pro_set.ute_wrk = true;
    }
    int configCount(const QString &descr)
    {
        QSqlQuery q(m_db);
        q.prepare(QStringLiteral("SELECT COUNT(*) FROM PAR_CFG WHERE trim(DESCR) = ?"));
        q.addBindValue(descr);
        q.exec();
        q.next();
        return q.value(0).toInt();
    }
    // Every control a model sets must exist on the generated form.
    void controlsExist(const FormModel &model, const QString &form)
    {
        QFile file(QFileInfo(QStringLiteral(STAG_SQLITE)).path() + QStringLiteral("/src/app/qml/forms/") + form + QStringLiteral(".qml"));
        QVERIFY(file.open(QIODevice::ReadOnly));
        const QString qml = QString::fromUtf8(file.readAll());
        const QStringList keys = model.state().keys();
        for (const QString &key : keys)
            QVERIFY2(qml.contains(u'"' + key + QStringLiteral("\":")), qPrintable(form + u' ' + key));
    }

private slots:
    void initTestCase()
    {
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        messaggioHook = [this](const QString &text, bool, bool) { m_message = text; };
    }

    void init()
    {
        delete m_dir;
        m_dir = new QTemporaryDir;
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            const QString target = m_dir->filePath(QString::fromLatin1(name));
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), target);
            QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(m_dir->path());
        legParSup(iniFile(), QString(), m_dir->path() + u'/');
        const QString database = m_dir->filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), database));
        QFile::setPermissions(database, QFile::ReadOwner | QFile::WriteOwner);
        if (!m_db.isValid())
            m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("misc"));
        m_db.close();
        m_db.setDatabaseName(database);
        QVERIFY(m_db.open());
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        leggiCfg(m_db, iniFile());
        leggiParCurTag(m_db);
        iniLiv();
        par_mac_ute = par_mac;
        par_mac.indirizzo_plc.TIMER_SCANS_PLC = 0;
        Livello = 1;
        num_com_sch_tag = 0;

        delete m_config;
        delete m_link;
        delete m_orders;
        delete m_state;
        delete m_fins;
        delete m_plc;
        delete m_board;
        m_plc = new FinsSimulator;
        QVERIFY(m_plc->listen(QHostAddress::LocalHost, 0));
        m_written.clear();
        connect(m_plc, &FinsSimulator::dmWritten, this, [this](int address, const QVector<quint16> &values) {
            if (address != 3500)
                return;
            QString text;
            for (const quint16 value : values)
                text += QString::number(value, 16).toUpper().rightJustified(4, u'0');
            m_written << text;
        });
        m_fins = new FinsClient;
        m_fins->setRemoteHost(QStringLiteral("127.0.0.1"));
        m_fins->setRemotePort(m_plc->port());
        m_fins->setTimeOut(200);
        m_fins->setEnabled(true);
        QTRY_VERIFY_WITH_TIMEOUT(!m_fins->error(), 5000);
        m_state = new ClasseStato;
        stag::sta_tag = m_state;
        m_orders = new CollezioneOrdini;
        QCOMPARE(m_orders->Init(false), int(RET_OK));
        m_orders->Leg_dat_ord();
        stag::ord_mac = m_orders;
        m_link = new PlcLink(m_fins, m_state, m_orders);
        m_link->setDatabase(&m_db);
        m_link->setWritesEnabled(true);
        m_config = new SchTagConfig(m_db, m_link, m_orders);

        m_frames.clear();
        m_reply = QByteArray(1, ACK_ST);
        m_board = new QUdpSocket;
        QVERIFY(m_board->bind(QHostAddress::LocalHost, 0));
        connect(m_board, &QUdpSocket::readyRead, this, [this] {
            while (m_board->hasPendingDatagrams()) {
                QByteArray data(int(m_board->pendingDatagramSize()), Qt::Uninitialized);
                QHostAddress host;
                quint16 port = 0;
                m_board->readDatagram(data.data(), data.size(), &host, &port);
                if (data.size() == 1)
                    continue;
                m_frames << QString::fromLatin1(data);
                m_board->writeDatagram(m_reply, host, port);
            }
        });
        for (int i = 1; i <= 3; ++i) {
            com_ini_sch_tag(i) = false;
            par_mac.opt.cut_pat(i) = "0";
            par_mac.opt.cut_pat_auto(i) = "0";
        }
        par_mac.tip_com_sch_tag(1) = "1";
        par_mac.lan_sch_tag_UDP(1) = true;
        par_mac.lan_sch_tag(1).por_loc = "0";
        par_mac.lan_sch_tag(1).por_rem = QString::number(m_board->localPort());
        par_mac.lan_sch_tag(1).ind_rem = QStringLiteral("127.0.0.1");
        par_mac.com_sch_tag(1).TimeOut = "60";
        m_message.clear();
    }

    void cleanupTestCase()
    {
        stag::sta_tag = nullptr;
        stag::ord_mac = nullptr;
    }

    void everyControlExistsOnItsForm()
    {
        AdjPatConModel adj(m_db, m_link, m_state);
        adj.activate(QString());
        controlsExist(adj, QStringLiteral("Adj_pat_con"));
        SelLinModel sel(m_db, m_state);
        sel.activate(QString());
        controlsExist(sel, QStringLiteral("Sel_lin"));
        PagProDayModel day(m_state);
        day.activate(QString());
        controlsExist(day, QStringLiteral("Pag_pro_day"));
        ShiftDialogModel cam(ShiftDialogModel::CamTur, m_state);
        cam.activate(QString());
        controlsExist(cam, QStringLiteral("Cam_tur"));
        ShiftDialogModel con(ShiftDialogModel::ConAzz, m_state);
        con.activate(QString());
        controlsExist(con, QStringLiteral("Con_azz"));
        DiaSchTagModel dia(m_link);
        dia.activate(QString());
        dia.colLabMsgAll(QStringLiteral("0000000000000001"), QStringLiteral("0000000000000011"));
        controlsExist(dia, QStringLiteral("Dia_sch_tag"));
        FrmDebugModel debug(false, m_fins, m_state);
        debug.activate(QString());
        controlsExist(debug, QStringLiteral("Frm_debug"));
        FrmDebugModel des(true, m_fins, m_state);
        des.activate(QString());
        controlsExist(des, QStringLiteral("Frm_debug_des"));
        FrmDataModel data;
        data.activate(QString());
        controlsExist(data, QStringLiteral("Frm_data"));
        ImpAllModel imp(m_db);
        imp.activate(QString());
        controlsExist(imp, QStringLiteral("Imp_all"));
    }

    // The full list carries the contrast itself, the free one the contrast - 1.
    void theContrastMacroSendsTheItemData()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        AdjPatConModel page(m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("a"), ChannelMacro, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F1"), 0); // all the values
        QCOMPARE(page.value(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("items")).toStringList().size(), 4);
        page.controlEvent(QStringLiteral("cmb_pat_con_sel"), -1, QStringLiteral("Click"), {{QStringLiteral("index"), 2}});
        QVERIFY(page.functionKey(QStringLiteral("F3"), 0));
        QVERIFY(m_link->boards().ftcRun());
        QVERIFY(!page.value(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("enabled")).toBool());
        tick();
        QCOMPARE(m_frames.size(), 1);
        QCOMPARE(m_frames[0].mid(1 + 5 + 10 * 4, 4), QStringLiteral("0008")); // contrast 3 = BKG_B
        QCOMPARE(m_written.last().left(4), QStringLiteral("0403"));
        QVERIFY(page.value(QStringLiteral("lab_proc_acq"), QStringLiteral("visible")).toBool());

        page.functionKey(QStringLiteral("F2"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("err_pro_acq_cor")));
        page.functionKey(QStringLiteral("F6"), 0);
        QVERIFY(!m_link->boards().ftcRun());
        QVERIFY(page.value(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("enabled")).toBool());
    }

    void changingTheLanguageNeedsThePasswordAndIsSaved()
    {
        machineReady();
        pro_set.lin_att = 1;
        SelLinModel page(m_db, m_state);
        page.activate(QString());
        page.controlEvent(QStringLiteral("cmb_lin"), -1, QStringLiteral("Click"), {{QStringLiteral("index"), 0}});
        pro_set.ute_wrk = false;
        QVERIFY(page.functionKey(QStringLiteral("F5"), 0));
        QCOMPARE(pro_set.lin_att, qint16(1));
        pro_set.ute_wrk = true;
        QVERIFY(!page.functionKey(QStringLiteral("F5"), 0));
        QCOMPARE(pro_set.lin_att, qint16(0));
        QCOMPARE(getFileINIString(QStringLiteral("pro_set"), QStringLiteral("Lin_att"), QString()), QStringLiteral("0"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    void shiftProductionIsFormattedLikeVb()
    {
        pro_set.tur = 2;
        FixedRecord record(shiftProductionLayout(), m_state->get_pro_tg());
        record.setText(QStringLiteral("tur(2).dat_ini"), QStringLiteral("20261030"));
        record.setText(QStringLiteral("tur(2).ora_ini"), QStringLiteral("143005"));
        record.setText(QStringLiteral("tur(2).tem_fer"), QStringLiteral("3725"));
        m_state->set_pro_tg(record.raw());
        PagProDayModel page(m_state);
        page.activate(QString());
        const QVariantList cells = page.value(QStringLiteral("lis_pro"), QStringLiteral("cells")).toList();
        QCOMPARE(cells.size(), 5);
        const QStringList row = cells.at(2).toStringList();
        QCOMPARE(row.at(1), QStringLiteral("30.10.2026"));
        QCOMPARE(row.at(2), QStringLiteral("14.30.05"));
        QCOMPARE(row.at(8), QStringLiteral("01.02.05"));
    }

    void aShiftOutsideOneToThreeIsRefused()
    {
        ShiftDialogModel page(ShiftDialogModel::ConAzz, m_state);
        page.activate(QString());
        page.controlEvent(QStringLiteral("txt_tur"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("4")}});
        QVERIFY(page.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("err_val")));
        page.controlEvent(QStringLiteral("txt_tur"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("3")}});
        QVERIFY(!page.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(pro_set.tur, qint16(3));
        QCOMPARE(getFileINIString(QStringLiteral("pro_set"), QStringLiteral("tur"), QString()), QStringLiteral("3"));
    }

    void boardDiagnosticsPaintTheLines()
    {
        machineReady();
        DiaSchTagModel page(m_link);
        page.setCommand(new Rmt(QStringLiteral("d"), ChannelCards, m_link, &page));
        page.activate(QStringLiteral("All_qe1"));
        QCOMPARE(page.value(QStringLiteral("lab_liv")).toString(), QStringLiteral("1"));
        // Answer 0001 0000: bit 0 of the first word, the first line.
        m_reply = QByteArray("\x02") + "00010000" + boardChecksum(QStringLiteral("\x02") + QStringLiteral("00010000")).toLatin1() + "\x04";
        QVERIFY(page.functionKey(QStringLiteral("F3"), 0));
        tick();
        QCOMPARE(page.value(QStringLiteral("lab_ris")).toString(), QStringLiteral("ERROR"));
        QCOMPARE(page.value(QStringLiteral("lab_msg_all(1)"), QStringLiteral("foreColor")).toString(), StaBarModel::vbColor(StaBarModel::vbRed));
        QCOMPARE(page.value(QStringLiteral("lab_msg_all(2)"), QStringLiteral("foreColor")).toString(), QStringLiteral("#000000"));
    }

    void theDebugPageReadsAndNeverWrites()
    {
        machineReady();
        m_plc->setDm(3500, {0x1234});
        FrmDebugModel page(false, m_fins, m_state);
        page.activate(QString());
        QVERIFY(page.running());
        QCOMPARE(page.value(QStringLiteral("lab_dm(1)")).toString(), QStringLiteral("3501"));
        page.controlEvent(QStringLiteral("tim_debug"), -1, QStringLiteral("Timer"), {});
        QCOMPARE(page.value(QStringLiteral("txt_dm(0)")).toString(), QString::number(0x1234));
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("tim_debug"), -1, QStringLiteral("Timer"), {});
        QCOMPARE(page.value(QStringLiteral("txt_dm(0)")).toString(), QStringLiteral("1234"));
        page.functionKey(QStringLiteral("F3"), 0);
        QVERIFY(!page.running());
        QVERIFY(m_written.isEmpty());
    }

    void theDescribedDebugPageShowsVbTexts()
    {
        machineReady();
        FrmDebugModel page(true, m_fins, m_state);
        page.activate(QString());
        QCOMPARE(page.value(QStringLiteral("lab_dm(0)")).toString(), QStringLiteral("Comando"));
        QCOMPARE(page.value(QStringLiteral("lab_int_dm")).toString(), QStringLiteral("3500 - 3549"));
        page.functionKey(QStringLiteral("F1"), 0);
        QCOMPARE(page.value(QStringLiteral("lab_int_dm")).toString(), QStringLiteral("3550 - 3599"));
    }

    void anAlarmAddressGoesToTheTableAndClearing()
    {
        machineReady();
        ImpAllModel page(m_db);
        page.activate(QString());
        QSqlQuery q(m_db);
        QVERIFY(q.exec(QStringLiteral("SELECT DM FROM ALL_MAC ORDER BY rec_no LIMIT 1")));
        QVERIFY(q.next());
        QCOMPARE(page.value(QStringLiteral("griglia"), QStringLiteral("cells")).toList().at(0).toStringList().at(0), q.value(0).toString());
        page.functionKey(QStringLiteral("F1"), 0); // on the address list
        page.keyUp(Qt::Key_Down);
        page.functionKey(QStringLiteral("F1"), 0);
        QSqlQuery after(m_db);
        QVERIFY(after.exec(QStringLiteral("SELECT DM FROM ALL_MAC ORDER BY rec_no LIMIT 1")));
        QVERIFY(after.next());
        QCOMPARE(after.value(0).toString(), QStringLiteral("001")); // row 2 of the list: word 0, bit 01
        page.functionKey(QStringLiteral("F3"), 0);
        QSqlQuery cleared(m_db);
        QVERIFY(cleared.exec(QStringLiteral("SELECT DM FROM ALL_MAC ORDER BY rec_no LIMIT 1")));
        QVERIFY(cleared.next());
        QCOMPARE(cleared.value(0).toString(), QStringLiteral("   "));
    }

    void alarmTextsFollowTheLanguageColumn()
    {
        machineReady();
        pro_set.lin_att = 1;
        ImpAllModel page(m_db);
        page.activate(QString());
        const QString address = page.value(QStringLiteral("griglia"), QStringLiteral("cells")).toList().at(0).toStringList().at(0);
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        page.controlEvent(QStringLiteral("all"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("NUOVO")}});
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        QCOMPARE(Messages::instance().find(QStringLiteral("all_mac_") + address), QStringLiteral("NUOVO"));
        QSqlQuery q(m_db);
        q.prepare(QStringLiteral("SELECT INGLESE FROM MESSAGGI WHERE CONTROLLO = ?"));
        q.addBindValue(QStringLiteral("all_mac_") + address);
        QVERIFY(q.exec() && q.next());
        QCOMPARE(q.value(0).toString(), QStringLiteral("NUOVO"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    void f7StartsAndEndsTheProductionReport()
    {
        machineReady();
        m_plc->setDm(3600, {0x0031, 0x0000}); // not running: bit MAC_IN_MARCIA off
        m_state->aggiorna(*m_fins);
        PagProModel page(m_orders);
        pro_set.abi_reg_rep_pro = QStringLiteral("0");
        QVERIFY(page.functionKey(QStringLiteral("F7"), 0));
        QCOMPARE(pro_set.abi_reg_rep_pro, QStringLiteral("1"));
        QCOMPARE(pro_set.ini_prod, QStringLiteral("1"));
        QCOMPARE(m_message, msg(QStringLiteral("reg_pro_ini")));

        // VB's ora_fin is minutes.seconds and DateDiff raises from minute 24 on: see theRuntimeErrorOfAFormIsLogged.
        m_orders->item(1)->setText(QStringLiteral("rep_pro.ora_ini"), QString());
        CollezioneOrdini executed;
        QCOMPARE(executed.Init(true), int(RET_OK));
        executed.Leg_dat_ord();
        const int before = executed.Count();
        stag::ord_mac_ese = &executed;
        const int count = m_orders->Count();
        const QString id = m_orders->item(1)->stag_id();
        QVERIFY(page.functionKey(QStringLiteral("F7"), 0));
        stag::ord_mac_ese = nullptr;
        QCOMPARE(pro_set.abi_reg_rep_pro, QStringLiteral("0"));
        QCOMPARE(m_message, msg(QStringLiteral("reg_pro_int")));
        QCOMPARE(m_orders->Count(), count); // no shift of the list
        QCOMPARE(executed.Count(), qMin(before + 1, 100));
        QCOMPARE(executed.item(1)->text(QStringLiteral("old_stag_id")).trimmed(), id.trimmed());
    }

    // ---- SPDE: frames of the machine's own Spde.log

    static QList<QByteArray> loggedStatus()
    {
        QFile file(QStringLiteral(STAG_REFERENCE_DATA "/Spde.log"));
        if (!file.open(QIODevice::ReadOnly))
            return {};
        QList<QByteArray> frames;
        const QByteArray data = file.readAll().replace('\0', "");
        for (const QByteArray &line : data.split('\n')) {
            const int at = line.indexOf("Inviato: ");
            if (at >= 0 && line.contains('\x03'))
                frames << line.mid(at + 9, line.indexOf('\x03') - at - 8);
        }
        return frames;
    }

    void everyLoggedStatusFrameHasOurChecksumAndShape()
    {
        const QList<QByteArray> frames = loggedStatus();
        QVERIFY(frames.size() > 5000);
        for (const QByteArray &raw : frames) {
            const QString text = QString::fromLatin1(raw);
            QCOMPARE(text.size(), 61);
            QCOMPARE(SpdeLink::frame(text.mid(1, 57)), text);
            QCOMPARE(text.mid(52, 6), QString(6, u' ')); // lib
        }
    }

    // A status frame logged with one order, rebuilt from the machine state it describes.
    void aLoggedStatusFrameIsRebuilt()
    {
        const QString logged = QStringLiteral("C1800000");
        machineReady();
        m_plc->setDm(3600, {0x0031, 0x0000});
        m_state->aggiorna(*m_fins);
        m_state->setComOK(true);
        par_abi.liv(1) = true;
        par_abi.liv(2) = true;
        com_ini_sch_tag(2) = true;
        m_plc->setDm(3602, m_orders->item(1)->stag_id().toUShort());
        m_plc->setDm(3620, m_orders->item(2)->stag_id().toUShort());
        m_state->aggiorna(*m_fins);
        CollezioneOrdini executed;
        QCOMPARE(executed.Init(true), int(RET_OK));
        executed.Leg_dat_ord();
        SpdeLink link(m_orders, &executed, m_state, m_db);
        const QString status = link.statusRecord();
        QCOMPARE(status.size(), 56);
        QCOMPARE(status.left(8), logged);
        for (int i = 1; i <= qMin(5, m_orders->Count()); ++i) {
            const OrdinePtr order = m_orders->item(i);
            QCOMPARE(status.mid(8 + (i - 1) * 7, 7), QLatin1Char('1') + order->text(QStringLiteral("spde_id")).trimmed() + order->text(QStringLiteral("spde_ModID")).trimmed());
        }
    }

    // Commands received by the machine: the seven orders of Spde_old.log.
    void loggedOrdersAreAcceptedAndReadBack()
    {
        machineReady();
        m_plc->setDm(3600, {0x0031, 0x0000});
        m_state->aggiorna(*m_fins);
        QFile file(QStringLiteral(STAG_REFERENCE_DATA "/Spde_old.log"));
        QVERIFY(file.open(QIODevice::ReadOnly));
        QList<QByteArray> commands;
        for (const QByteArray &line : file.readAll().replace('\0', "").split('\n')) {
            const int at = line.indexOf("ricevuto comando ");
            if (at >= 0)
                commands << line.mid(at + 17).chopped(line.endsWith('\r') ? 1 : 0);
        }
        QCOMPARE(commands.size(), 7);
        CollezioneOrdini executed;
        QCOMPARE(executed.Init(true), int(RET_OK));
        executed.Leg_dat_ord();
        SpdeLink link(m_orders, &executed, m_state, m_db);
        // They were sent at position 5 of the list of that moment.
        while (m_orders->Count() < 4)
            QCOMPARE(m_orders->add(m_orders->item(1)), int(RET_OK));
        QStringList answers;
        connect(&link, &SpdeLink::sent, this, [&answers](const QString &text) { answers << text; });
        for (const QByteArray &command : commands) {
            const QString text = QString::fromLatin1(command);
            QCOMPARE(text.size(), 744);
            answers.clear();
            link.rxDat(SpdeLink::frame(text));
            QCOMPARE(answers.size(), 1);
            QVERIFY(!answers.isEmpty());
            QCOMPARE(answers.first(), SpdeLink::frame(QStringLiteral("dA    ")));
            const int position = int(vb::val(text.mid(7, 2))) + 1;
            const OrdinePtr order = m_orders->item(position);
            QCOMPARE(order->text(QStringLiteral("spde_id")).trimmed(), text.mid(1, 4));
            QCOMPARE(order->text(QStringLiteral("cod_ord_abb")).trimmed(), text.mid(9, 16).trimmed());
            QCOMPARE(order->text(QStringLiteral("tip_car.Cod_ond")).trimmed(), text.mid(30, 3).trimmed());
        }
    }

    void aBadChecksumOrManualModeIsRefused()
    {
        machineReady();
        m_plc->setDm(3600, {0x0031, 0x0000});
        m_state->aggiorna(*m_fins);
        CollezioneOrdini executed;
        QCOMPARE(executed.Init(true), int(RET_OK));
        SpdeLink link(m_orders, &executed, m_state, m_db);
        QStringList answers;
        connect(&link, &SpdeLink::sent, this, [&answers](const QString &text) { answers << text; });
        const int before = m_orders->Count();
        link.rxDat(QLatin1Char(STX_ST_SPDE) + QStringLiteral("S00") + QLatin1Char(ETX_ST_SPDE));
        QCOMPARE(answers.size(), 1);
        QCOMPARE(answers.first().mid(1, 2), QStringLiteral(" N"));
        answers.clear();
        link.rxDat(SpdeLink::frame(QStringLiteral("S")));
        QVERIFY(!answers.isEmpty());
        QCOMPARE(answers.first().mid(1, 1), QStringLiteral("s"));
        answers.clear();
        link.rxDat(SpdeLink::frame(QStringLiteral("E0")));
        QVERIFY(!answers.isEmpty());
        QVERIFY(answers.first().mid(1, 2) == QLatin1String("eA") || answers.first().mid(1, 2) == QLatin1String("eN"));
        Q_UNUSED(before)
    }

    void theStatusBarFormFillsItsControls()
    {
        machineReady();
        StaBarModel bar;
        StaBarFormModel form(&bar);
        form.activate(QString());
        bar.setLedPlc(StaBarModel::vbGreen, StaBarModel::vbRed);
        form.messagWin(QStringLiteral("x"), false);
        form.ledSpdeRx();
        form.eveTagErr(1, QStringLiteral("5"));
        form.eveTagNum(1, QStringLiteral("10"));
        form.eveTagNumErr(1, QStringLiteral("2"));
        controlsExist(form, QStringLiteral("Sta_bar"));
        QCOMPARE(form.value(QStringLiteral("txt_err(1)")).toString(), QStringLiteral("5"));
        QCOMPARE(form.value(QStringLiteral("txt_num_sca(1)")).toString(), QStringLiteral("10"));
        QCOMPARE(form.value(QStringLiteral("txt_sca(1)")).toString(), QStringLiteral("2"));
        QCOMPARE(form.value(QStringLiteral("led_plc_rx"), QStringLiteral("fillColor")).toString(), StaBarModel::vbColor(StaBarModel::vbGreen));
        QVERIFY(form.value(QStringLiteral("ssp_mes_all"), QStringLiteral("visible")).toBool());
        form.messagWin(QString(), false);
        QVERIFY(!form.value(QStringLiteral("ssp_mes_all"), QStringLiteral("visible")).toBool());
        form.clearCounters();
        QCOMPARE(form.value(QStringLiteral("txt_err(1)")).toString(), QString());
    }

    void aCutErrorOverTheToleranceTurnsTheFrameRed()
    {
        StaBarModel bar;
        StaBarFormModel form(&bar);
        const int levels = int(par_mac.car_tec.num_liv_tag.val());
        const int index = levels == 3 ? 1 : 3;
        const int level = levels == 3 ? 1 : LIV_ALT;
        const qint64 limit = qint64(qMax(par_mac.sch_tag.sch_tag_liv(level).lim_inf_tol_tag.val(), par_mac.sch_tag.sch_tag_liv(level).lim_sup_tol_tag.val()));
        form.eveTagErr(index, QString::number(limit + 1));
        QCOMPARE(form.value(QStringLiteral("frm_err"), QStringLiteral("backColor")).toString(), StaBarModel::vbColor(StaBarModel::COL_ALL_Red));
        form.eveTagErr(index, QString());
        QCOMPARE(form.value(QStringLiteral("txt_err(%1)").arg(index)).toString(), QString());
    }

    void boardAnswersFillPagMsgByLevel()
    {
        PagMsgModel page;
        QSignalSpy opened(&page, &PagMsgModel::opened);
        page.show(QStringLiteral("a"), false, 1);
        page.show(QStringLiteral("b"), true, 2);
        QCOMPARE(opened.count(), 1);
        controlsExist(page, QStringLiteral("Pag_msg"));
        QCOMPARE(page.value(QStringLiteral("lab_msg(1)")).toString(), QStringLiteral("b"));
        QCOMPARE(page.value(QStringLiteral("lab_msg(1)"), QStringLiteral("foreColor")).toString(), StaBarModel::vbColor(StaBarModel::COL_ALL_Red));
        QVERIFY(!page.functionKey(QStringLiteral("ESC"), 0));
        page.show(QStringLiteral("c"), false, 1);
        QCOMPARE(opened.count(), 2);
        QCOMPARE(page.value(QStringLiteral("lab_msg(1)")).toString(), QString());
        page.show(QStringLiteral("z"), false, 3 + 1); // level out of range
        QCOMPARE(page.value(QStringLiteral("lab_msg(0)")).toString(), QStringLiteral("c"));
    }

    void theClockGetsTheTypedDate()
    {
        FrmDataModel page;
        QDateTime asked;
        bool answer = true;
        page.setClockSetter([&](const QDateTime &when) {
            asked = when;
            return answer;
        });
        page.activate(QString());
        for (const auto &[control, text] : {std::pair("txt_day", "30"), std::pair("txt_month", "10"), std::pair("txt_year", "2026"), std::pair("txt_ore", "12"), std::pair("txt_min", "5"), std::pair("txt_sec", "7")})
            page.controlEvent(QLatin1String(control), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QLatin1String(text)}});
        QVERIFY(!page.value(QStringLiteral("tim_ora"), QStringLiteral("running")).toBool());
        m_message.clear();
        QVERIFY(!page.functionKey(QStringLiteral("F3"), 0));
        QCOMPARE(asked, QDateTime(QDate(2026, 10, 30), QTime(12, 5, 7)));
        QVERIFY(m_message.isEmpty());
        answer = false;
        QVERIFY(!page.functionKey(QStringLiteral("F3"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("imp_ese_ope")));
    }

    void theRuntimeErrorOfAFormIsLogged()
    {
        bool reached = false;
        const bool answer = guardedFormCall(QStringLiteral("test"), [&]() -> bool {
            reached = true;
            vb::raise(13);
        }, true);
        QVERIFY(reached);
        QVERIFY(answer);
        QVERIFY_THROWS_EXCEPTION(vb::Error, vb::dateDiffSeconds(QStringLiteral("10/22/20 16.35 PM"), QStringLiteral("10/30/26 59.47 PM")));
    }

    // Every text the program asks for exists in the four languages, and none is left in Italian by accident.
    void theFourLanguagesAreComplete()
    {
        const QStringList keys{QStringLiteral("ptg_tur"), QStringLiteral("sta_e_stop"), QStringLiteral("ope_incor"), QStringLiteral("cfg_attiva"), QStringLiteral("vis_dec"),
                               QStringLiteral("abi_un_mot"), QStringLiteral("ptg_dat_ini"), QStringLiteral("ptg_gio")};
        const struct { int lin; const char *turno; } languages[] = {{0, "Turno"}, {1, "Shift"}, {2, "Schicht"}, {8, "Zmiana"}};
        for (const auto &language : languages) {
            QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), language.lin));
            for (const QString &key : keys)
                QVERIFY2(!Messages::instance().find(key).trimmed().isEmpty(), qPrintable(QString::number(language.lin) + u' ' + key));
            QCOMPARE(Messages::instance().find(QStringLiteral("ptg_tur")).trimmed(), QLatin1String(language.turno));
        }
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 2));
        QCOMPARE(Messages::instance().captionText(QStringLiteral("Filtro Kp")), QStringLiteral("Kp-Filter"));
        QCOMPARE(Messages::instance().captionText(QStringLiteral("msec")), QStringLiteral("msec"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 0));
        QCOMPARE(Messages::instance().captionText(QStringLiteral("Filtro Kp")), QStringLiteral("Filtro Kp"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        QCOMPARE(Messages::instance().find(QStringLiteral("pag_pro_day_0 2")).trimmed(), QStringLiteral("Shift change")); // "_Cambio turno" in the column
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 8));
        // A numbered placeholder keeps its number; what the machine has translated stays as it is.
        QCOMPARE(Messages::instance().find(QStringLiteral("det_all_con_1500")).trimmed(), QStringLiteral("Szczegoly alarmu nr 1500"));
        QCOMPARE(Messages::instance().find(QStringLiteral("all_con_1500")).trimmed(), QStringLiteral("Wystepuje 24 VDC"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    void theLanguagePickerOffersFourAndSavesTheColumn()
    {
        machineReady();
        pro_set.lin_att = 8;
        SelLinModel page(m_db, m_state);
        page.activate(QString());
        QCOMPARE(page.value(QStringLiteral("cmb_lin"), QStringLiteral("items")).toStringList().size(), 4);
        QCOMPARE(page.value(QStringLiteral("cmb_lin"), QStringLiteral("listIndex")).toInt(), 3);
        page.controlEvent(QStringLiteral("cmb_lin"), -1, QStringLiteral("Click"), {{QStringLiteral("index"), 2}});
        QVERIFY(!page.functionKey(QStringLiteral("F5"), 0));
        QCOMPARE(pro_set.lin_att, qint16(2));
        QCOMPARE(getFileINIString(QStringLiteral("pro_set"), QStringLiteral("Lin_att"), QString()), QStringLiteral("2"));
        QCOMPARE(Messages::instance().find(QStringLiteral("ptg_tur")).trimmed(), QStringLiteral("Schicht"));
        pro_set.lin_att = 1;
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    void theLinkOpensNothingWithoutTheFlag()
    {
        machineReady();
        CollezioneOrdini executed;
        QCOMPARE(executed.Init(true), int(RET_OK));
        SpdeLink link(m_orders, &executed, m_state, m_db);
        par_mac.com_spde.ver_spde = Fosber30;
        par_mac.com_spde.tip_com = "1";
        par_mac.com_spde.lan.por_loc = "0";
        par_mac.com_spde.lan.por_rem = QString::number(m_board->localPort());
        par_mac.com_spde.lan.ind_rem = QStringLiteral("127.0.0.1");
        link.tick();
        QTest::qWait(30);
        QVERIFY(m_frames.isEmpty());
    }
};

QTEST_GUILESS_MAIN(TestMiscPages)
#include "tst_miscpages.moc"
