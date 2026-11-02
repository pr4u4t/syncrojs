#include "abilitazionimodel.h"
#include "classestato.h"
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

class TestSchTagPages : public QObject
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
            m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("pages"));
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
        for (const int number : {1, 2, 4, 5}) {
            SetParSchTagModel page(number, m_db, m_link, m_state);
            page.activate(QString());
            controlsExist(page, QStringLiteral("Set_par_sch_tag%1").arg(number));
        }
        SetParSchTag3Model page3(m_db, m_link, m_state);
        page3.activate(QString());
        controlsExist(page3, QStringLiteral("Set_par_sch_tag3"));
        SalCfgModel sal(m_db, m_link);
        sal.activate(QString());
        controlsExist(sal, QStringLiteral("Sal_cfg"));
        DelCfgModel del(m_db);
        del.activate(QString());
        controlsExist(del, QStringLiteral("Del_cfg"));
        LegCfgModel leg(m_db, m_link);
        leg.activate(QString());
        controlsExist(leg, QStringLiteral("Leg_cfg"));
    }

    // Values of the machine's own stag.ini reach the page, and saving unchanged writes the same ini back.
    void thePageShowsAndWritesBackTheMachineValues()
    {
        machineReady();
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        QCOMPARE(page.value(QStringLiteral("txt_sov_lam_att")).toString(), par_mac.sch_tag.sch_tag_liv(1).sov_lam.trimmed());

        QFile before(QStringLiteral(STAG_REFERENCE_DATA "/stag.ini"));
        QVERIFY(before.open(QIODevice::ReadOnly));
        const QString original = QString::fromLatin1(before.readAll());
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        QVERIFY(page.functionKey(QStringLiteral("F3"), 0));
        tick();
        const auto sectionOf = [](const QString &text) {
            const int start = text.indexOf(QStringLiteral("[sch_tag_liv_1]"));
            const int end = text.indexOf(u'[', start + 1);
            QStringList lines = text.mid(start, end - start).split(QRegularExpression(QStringLiteral("[\\r\\n]+")), Qt::SkipEmptyParts);
            for (QString &line : lines)
                line = line.remove(u' ');
            return lines;
        };
        QFile after(m_dir->filePath(QStringLiteral("stag.ini")));
        QVERIFY(after.open(QIODevice::ReadOnly));
        const QStringList saved = sectionOf(QString::fromLatin1(after.readAll()));
        QVERIFY(saved.size() > 60);
        for (const QString &line : sectionOf(original)) {
            if (line.contains(QStringLiteral("config_")))
                continue;
            QVERIFY2(saved.contains(line), qPrintable(line));
        }
    }

    void withoutThePasswordNothingStarts()
    {
        machineReady();
        pro_set.ute_wrk = false;
        SetParSchTagModel page(1, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("acc_pwd")));
        QVERIFY(page.value(QStringLiteral("txt_dia_ruo_mis_att"), QStringLiteral("readOnly")).toBool());
    }

    void aSavedValueGoesToTheBoardThenTheIniThenThePlc()
    {
        machineReady();
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        QVERIFY(!page.value(QStringLiteral("txt_sov_lam_att"), QStringLiteral("readOnly")).toBool());
        page.controlEvent(QStringLiteral("txt_sov_lam_att"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("123")}});
        QCOMPARE(page.value(QStringLiteral("lab_sov_lam")).toString(), QStringLiteral("12.3"));
        page.functionKey(QStringLiteral("F3"), 0);
        tick();

        QCOMPARE(m_frames.size(), 2);
        QCOMPARE(m_frames[0].mid(1 + 5 + 25 * 4, 4), QStringLiteral("007B"));
        QCOMPARE(par_mac.sch_tag.sch_tag_liv(1).sov_lam.trimmed(), QStringLiteral("123"));
        QCOMPARE(getFileINIString(QStringLiteral("sch_tag_liv_1"), QStringLiteral("sov_lam"), QString()), QStringLiteral("123"));
        QCOMPARE(m_written.size(), 2); // PARAM_CFG_MAC, PARAM_CUR_TAG
        QVERIFY(!m_message.isEmpty());
    }

    void aRefusedValueRollsBack()
    {
        machineReady();
        const QString before = par_mac.sch_tag.sch_tag_liv(1).sov_lam.trimmed();
        m_reply = QByteArray(1, NAK_ST);
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("txt_sov_lam_att"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("123")}});
        page.functionKey(QStringLiteral("F3"), 0);
        tick();
        QCOMPARE(par_mac.sch_tag.sch_tag_liv(1).sov_lam.trimmed(), before);
        QCOMPARE(par_mac_ute.sch_tag.sch_tag_liv(1).sov_lam.trimmed(), before);
        QVERIFY(m_written.isEmpty());
    }

    void escapeRestoresTheField()
    {
        machineReady();
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        const QString before = page.value(QStringLiteral("txt_sov_lam_att")).toString();
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("txt_sov_lam_att"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("9")}});
        QVERIFY(page.functionKey(QStringLiteral("F9"), 0)); // no page change during an edit
        page.functionKey(QStringLiteral("ESC"), 0);
        QCOMPARE(page.value(QStringLiteral("txt_sov_lam_att")).toString(), before);
        QVERIFY(!page.functionKey(QStringLiteral("F9"), 0));
        QVERIFY(m_frames.isEmpty());
    }

    // set_par_sch_tag2 lists lun_tag_tes among its fields, but def_in_att copies only its own group.
    void partialDefaultCopyTakesOnlyThePageGroup()
    {
        machineReady();
        par_mac.sch_tag_def.sch_tag_liv(1).sov_lam = "77";
        par_mac.sch_tag_def.sch_tag_liv(1).lun_tag_tes = "88";
        const QString tes = par_mac_ute.sch_tag.sch_tag_liv(1).lun_tag_tes.trimmed();
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(par_mac_ute.sch_tag.sch_tag_liv(1).sov_lam.trimmed(), QStringLiteral("77"));
        QCOMPARE(par_mac_ute.sch_tag.sch_tag_liv(1).lun_tag_tes.trimmed(), tes);
    }

    void copyToDefaultGoesToTheIni()
    {
        machineReady();
        SetParSchTagModel page(2, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        page.controlEvent(QStringLiteral("txt_sov_lam_att"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("55")}});
        page.functionKey(QStringLiteral("F5"), 0);
        QCOMPARE(par_mac.sch_tag_def.sch_tag_liv(1).sov_lam.trimmed(), QStringLiteral("55"));
        QCOMPARE(getFileINIString(QStringLiteral("sch_tag_liv_1"), QStringLiteral("sov_lam_def"), QString()), QStringLiteral("55"));
        QVERIFY(m_frames.isEmpty());
    }

    void aCurveIsSavedOnlyWhenThePlcTookIt()
    {
        machineReady();
        SetParSchTagModel page(1, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &page));
        page.setPlcCommand(new Rmt(QStringLiteral("p"), ChannelPlc, m_link, &page));
        page.activate(QString());
        QVERIFY(page.functionKey(QStringLiteral("F2"), 2));
        page.controlEvent(QStringLiteral("txt_lun_tag"), 0, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("999")}});
        QCOMPARE(page.value(QStringLiteral("txt_lun_tag(0)")).toString(), QStringLiteral("999"));
        QVERIFY(page.functionKey(QStringLiteral("F3"), 2));
        tick();
        QCOMPARE(cur_tag(1).Lun_tag(1), 999);
        QVERIFY(!m_written.isEmpty());
        QSqlQuery q(m_db);
        QVERIFY(q.exec(QStringLiteral("SELECT LUN_TAG0 FROM CUR_TAG WHERE CAST(LIVELLO1 AS INTEGER) = 1")));
        QVERIFY(q.next());
        QCOMPARE(q.value(0).toInt(), 999);
    }

    void aCurveThePlcRefusedIsRolledBack()
    {
        machineReady();
        const qint32 before = cur_tag(1).Lun_tag(1);
        SetParSchTagModel page(1, m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &page));
        page.setPlcCommand(new Rmt(QStringLiteral("p"), ChannelPlc, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 2);
        page.controlEvent(QStringLiteral("txt_lun_tag"), 0, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("999")}});
        page.functionKey(QStringLiteral("F3"), 2);
        m_state->setComOK(false);
        tick();
        QCOMPARE(cur_tag(1).Lun_tag(1), before);
    }

    void aNewConfigurationGoesThroughSalCfg()
    {
        machineReady();
        SetParSchTag3Model page(m_db, m_link, m_state);
        page.setCommand(new Rmt(QStringLiteral("t"), ChannelCards, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        for (const char *name : {"vel_max_tag", "tem_acc_tgl_lun", "tem_dec_tgl_lun", "tem_acc_tgl_cor", "tem_dec_tgl_cor", "kp_nor", "ki_nor", "fil_kp", "kv_nor", "lim_lav_kv", "gir_max_mot_lam"})
            page.controlEvent(QStringLiteral("txt_%1_att").arg(QLatin1String(name)), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("111")}});
        QVERIFY(!page.functionKey(QStringLiteral("F5"), 0)); // a configuration is active: on to sal_cfg

        SalCfgModel sal(m_db, m_link);
        sal.setCommand(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &sal));
        sal.activate(QString());
        QVERIFY(sal.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("imm_des")));
        sal.controlEvent(QStringLiteral("txt_des"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("NUOVA")}});
        QVERIFY(!sal.functionKey(QStringLiteral("F11"), 0));
        tick();

        QCOMPARE(configCount(QStringLiteral("NUOVA")), 1);
        QCOMPARE(par_mac.config_sch_tag_att(1).descr.trimmed(), QStringLiteral("NUOVA"));
        QCOMPARE(par_mac.par_cfg_att(1).vel_max_tag.trimmed(), QStringLiteral("111"));
        QCOMPARE(m_frames[0].mid(1 + 5 + 23 * 4, 4), QStringLiteral("006F"));
        QCOMPARE(getFileINIString(QStringLiteral("sch_tag_liv_1"), QStringLiteral("config_att"), QString()), QStringLiteral("NUOVA"));

        // res_cfg_att runs after the insert in salva_cfg_att and clears the new record's flag as well.
        QSqlQuery q(m_db);
        QVERIFY(q.exec(QStringLiteral("SELECT LIVELLO1 FROM PAR_CFG WHERE trim(DESCR) = 'NUOVA'")));
        QVERIFY(q.next());
        QCOMPARE(q.value(0).toInt(), 0);
    }

    void aNameAlreadyThereIsRefused()
    {
        machineReady();
        SalCfgModel sal(m_db, m_link);
        sal.setCommand(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &sal));
        sal.activate(QString());
        sal.controlEvent(QStringLiteral("txt_des"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("DEFAULT")}});
        QVERIFY(!sal.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("cfg_pre")));
        tick();
        QVERIFY(m_frames.isEmpty());
    }

    void defaultCannotBeDeletedNorAnActiveOne()
    {
        DelCfgModel del(m_db);
        del.activate(QString());
        const int before = configCount(par_mac.config_sch_tag_att(1).descr.trimmed());
        QVERIFY(!del.functionKey(QStringLiteral("F11"), 0));
        QVERIFY(m_message == msg(QStringLiteral("del_cfg_def")) || m_message == msg(QStringLiteral("del_cfg_att")));
        QCOMPARE(configCount(par_mac.config_sch_tag_att(1).descr.trimmed()), before);
    }

    void aSpareConfigurationIsDeleted()
    {
        QSqlQuery q(m_db);
        QVERIFY(q.exec(QStringLiteral("INSERT INTO PAR_CFG (DESCR, LIVELLO1, LIVELLO2, LIVELLO3, FOR_MAC, GRA_CAR, N_MOT_L, N_MOT_AB) VALUES ('ZZZ', '0', '0', '0', '1800', '1800', '1', '1')")));
        DelCfgModel del(m_db);
        del.activate(QString());
        for (int i = 0; i < 100; ++i)
            del.keyUp(Qt::Key_Right);
        QCOMPARE(del.value(QStringLiteral("lab_cfg")).toString(), QStringLiteral("ZZZ"));
        QVERIFY(!del.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(configCount(QStringLiteral("ZZZ")), 0);
    }

    void legCfgRefusesAnotherMachineShape()
    {
        machineReady();
        LegCfgModel leg(m_db, m_link);
        leg.setCommand(new Rmt(QStringLiteral("l"), ChannelCards, m_link, &leg));
        leg.activate(QString());
        for (int i = 0; i < 100; ++i)
            leg.keyUp(Qt::Key_Right);
        QVERIFY(leg.value(QStringLiteral("lab_for_mac")).toString() != leg.value(QStringLiteral("l_for_mac")).toString()
                || leg.value(QStringLiteral("lab_num_mot_liv")).toString() != leg.value(QStringLiteral("l_num_mot_liv")).toString()
                || leg.value(QStringLiteral("lab_gra_car")).toString() != leg.value(QStringLiteral("l_gra_car")).toString());
        QVERIFY(!leg.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("imp_car_cfg")));
        tick();
        QVERIFY(m_frames.isEmpty());
    }

    // Every key scr_par_mac writes reads back as the value in the machine's stag.ini.
    void scrParMacWritesWhatTheMachineHas()
    {
        const auto read = [](const QString &path) {
            QFile file(path);
            file.open(QIODevice::ReadOnly);
            QHash<QString, QString> values;
            QString section;
            for (const QString &raw : QString::fromLatin1(file.readAll()).split(QRegularExpression(QStringLiteral("[\\r\\n]+")), Qt::SkipEmptyParts)) {
                const QString line = raw.trimmed();
                if (line.startsWith(u'[')) {
                    section = line.toLower();
                } else {
                    const int equal = line.indexOf(u'=');
                    if (equal > 0)
                        values.insert(section + line.left(equal).trimmed().toLower(), line.mid(equal + 1).trimmed());
                }
            }
            return values;
        };
        const auto original = read(QStringLiteral(STAG_REFERENCE_DATA "/stag.ini"));
        legParMac(iniFile()); // init() points the link at the simulators
        par_mac.opt.cut_pat(3) = ""; // and legParMac reads only the fitted levels, so undo init() on the third
        par_mac.opt.cut_pat_auto(3) = "";
        scrParMac();
        scrParAbi();
        iniFile().save();
        const auto saved = read(m_dir->filePath(QStringLiteral("stag.ini")));
        QStringList differences;
        for (auto it = original.cbegin(); it != original.cend(); ++it) {
            if (saved.value(it.key()) != it.value())
                differences << it.key() + QStringLiteral(": '") + it.value() + QStringLiteral("' -> '") + saved.value(it.key()) + u'\'';
        }
        qInfo() << saved.size() << "keys;" << differences;
        QVERIFY2(differences.isEmpty(), qPrintable(differences.join(QStringLiteral("; "))));
    }

    void setParMacValuesAndChecks()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        QVERIFY2(m_state->bit(ClasseStato::MAC_F_STOP, 1), "F stop bit in DM3600");
        SetParMacModel page(m_db, m_state);
        page.setCommands(new Rmt(QStringLiteral("p"), ChannelPlc, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page));
        page.activate(QString());
        controlsExist(page, QStringLiteral("Set_par_mac"));
        QVERIFY(!page.value(QStringLiteral("txt_pre_all"), QStringLiteral("enabled")).toBool());
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        QVERIFY(page.value(QStringLiteral("txt_pre_all"), QStringLiteral("enabled")).toBool());
        QVERIFY(page.functionKey(QStringLiteral("F9"), 0));

        page.controlEvent(QStringLiteral("txt_sgl_abi_lin_dc_bus"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("100")}});
        page.functionKey(QStringLiteral("F3"), 0);
        QVERIFY(m_message.contains(msg(QStringLiteral("par_err"))));
        QVERIFY(!page.value(QStringLiteral("txt_pre_all"), QStringLiteral("enabled")).toBool());
        QVERIFY(m_written.isEmpty());

        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("txt_pre_all"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("42")}});
        page.functionKey(QStringLiteral("F3"), 0);
        tick();
        QCOMPARE(par_mac.car_tec.pre_all.trimmed(), QStringLiteral("42"));
        QCOMPARE(getFileINIString(QStringLiteral("car_tec"), QStringLiteral("pre_all"), QString()), QStringLiteral("42"));
        QCOMPARE(m_written.size(), 2); // PARAM_MAC, PARAM_CFG_MAC
    }

    void aRulletiExitForcesTheSandwichLength()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        SetParMacModel page(m_db, m_state);
        page.setCommands(new Rmt(QStringLiteral("p"), ChannelPlc, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("cmb_usc_liv"), 0, QStringLiteral("Click"), {{QStringLiteral("index"), 1}});
        page.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(par_mac.rul_usc.rul_usc_liv(1).lun_san.trimmed(), QStringLiteral("470"));
    }

    void aPatternOptionSwitchesCutToMarkOff()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        SetParMacModel page(m_db, m_state);
        page.setCommands(new Rmt(QStringLiteral("p"), ChannelPlc, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page));
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("chk_cut_to_mar"), -1, QStringLiteral("Click"), {{QStringLiteral("checked"), true}});
        page.controlEvent(QStringLiteral("chk_cut_to_pat"), 0, QStringLiteral("Click"), {{QStringLiteral("checked"), true}});
        page.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(par_mac.opt.cut_mar.trimmed(), QStringLiteral("0"));
        QCOMPARE(par_mac.opt.cut_pat(1).trimmed(), QStringLiteral("1"));
    }

    // A motor goes to its board with the configuration of the new motor count, then to ini and PLC.
    void aMotorSwitchGoesThroughItsBoard()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        par_mac.car_tec.num_mot_liv_tag = "2";
        par_mac_ute = par_mac;
        par_abi.liv(1) = true;
        par_abi.fun_mot_liv(1).fun_mot_mot(1) = true;
        par_abi.fun_mot_liv(1).fun_mot_mot(2) = true;
        par_abi.num_mot_abi(1) = 2;
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("a"), ChannelPlc, m_link, &page));
        page.setMotorCommands(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page), m_link, m_db);
        page.activate(QString());
        page.controlEvent(QStringLiteral("fun_mot"), 12, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        tick();
        QCOMPARE(m_frames.size(), 2);
        QVERIFY(!par_abi.fun_mot_liv(1).fun_mot_mot(2));
        QCOMPARE(par_abi.num_mot_abi(1), qint16(1));
        QCOMPARE(getFileINIString(QStringLiteral("abi_liv_1"), QStringLiteral("fun_mot2"), QString()), QStringLiteral("0"));
        QCOMPARE(m_written.size(), 2);
    }

    void theLastMotorCannotBeSwitchedOff()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        par_mac.car_tec.num_mot_liv_tag = "1";
        par_abi.liv(1) = true;
        par_abi.fun_mot_liv(1).fun_mot_mot(1) = true;
        par_abi.num_mot_abi(1) = 1;
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("a"), ChannelPlc, m_link, &page));
        page.setMotorCommands(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page), m_link, m_db);
        page.activate(QString());
        page.controlEvent(QStringLiteral("fun_mot"), 11, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        QCOMPARE(m_message, msg(QStringLiteral("imp_dis_all_mot")));
        QVERIFY(page.isOn(QStringLiteral("fun_mot(11)")));
        tick();
        QVERIFY(m_frames.isEmpty());
    }

    void aRefusedMotorRollsBack()
    {
        machineReady();
        m_plc->setDm(3600, {0x0075, 0x0000});
        m_state->aggiorna(*m_fins);
        m_reply = QByteArray(1, NAK_ST);
        par_mac.car_tec.num_mot_liv_tag = "2";
        par_mac_ute = par_mac;
        par_abi.liv(1) = true;
        par_abi.fun_mot_liv(1).fun_mot_mot(1) = true;
        par_abi.fun_mot_liv(1).fun_mot_mot(2) = true;
        par_abi.num_mot_abi(1) = 2;
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("a"), ChannelPlc, m_link, &page));
        page.setMotorCommands(new Rmt(QStringLiteral("s"), ChannelCards, m_link, &page), new Rmt(QStringLiteral("m"), ChannelMacro, m_link, &page), m_link, m_db);
        page.activate(QString());
        page.controlEvent(QStringLiteral("fun_mot"), 12, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        tick();
        QVERIFY(par_abi.fun_mot_liv(1).fun_mot_mot(2));
        QVERIFY(page.isOn(QStringLiteral("fun_mot(12)")));
        QVERIFY(m_written.isEmpty());
    }

    // No CAN here: nothing can be confirmed by the drives, so nothing is saved.
    void withoutCanTheRollerValuesStayAsTheyWere()
    {
        machineReady();
        const QString before = par_mac.rul_ing.vel_max_lin.trimmed();
        SetParRulModel page(m_state);
        page.setCommand(new Rmt(QStringLiteral("r"), ChannelPlc, m_link, &page));
        page.activate(QString());
        controlsExist(page, QStringLiteral("Set_par_rul"));
        QVERIFY(page.functionKey(QStringLiteral("F2"), 0));
        page.controlEvent(QStringLiteral("txt_vel_max_lin"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("123")}});
        page.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(page.value(QStringLiteral("lab_tra_ing(0)")).toString(), QStringLiteral("Error"));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff_par")));
        QCOMPARE(par_mac.rul_ing.vel_max_lin.trimmed(), before);
        QCOMPARE(page.value(QStringLiteral("txt_vel_max_lin")).toString(), before);
        tick();
        QVERIFY(m_written.isEmpty());
    }

    void rollerValuesAreSavedWhenTheDrivesTookThem()
    {
        machineReady();
        SetParRulModel page(m_state);
        page.setCommand(new Rmt(QStringLiteral("r"), ChannelPlc, m_link, &page));
        QList<std::pair<bool, int>> sent;
        page.setDriveSender([&sent](bool entry, int index) {
            sent << std::pair(entry, index);
            return true;
        });
        page.activate(QString());
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("txt_tem_chi_fla"), 0, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("900")}});
        page.functionKey(QStringLiteral("F3"), 0);
        tick();
        const int levels = int(par_mac.car_tec.num_liv_tag.val());
        QCOMPARE(sent.size(), (levels == 3 ? 2 : 1) + levels);
        QCOMPARE(par_mac.rul_usc.rul_usc_liv(1).tem_chi_fla.trimmed(), QStringLiteral("500")); // MAX_TIM_ANT_CHI_FLAP
        QCOMPARE(getFileINIString(QStringLiteral("rul_usc_liv_1"), QStringLiteral("tem_chi_fla"), QString()), QStringLiteral("500"));
        QCOMPARE(m_written.size(), 1);
    }

    void communicationSettingsAreSavedAndChecked()
    {
        machineReady();
        SetParComModel page(m_link, m_state);
        page.activate(QString());
        controlsExist(page, QStringLiteral("Set_par_com"));
        QVERIFY(!page.value(QStringLiteral("txt_timeout"), QStringLiteral("enabled")).toBool());
        QVERIFY(!page.functionKey(QStringLiteral("F9"), 0));
        page.functionKey(QStringLiteral("F2"), 0);
        QVERIFY(page.functionKey(QStringLiteral("F9"), 0));
        page.controlEvent(QStringLiteral("txt_timeout"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("1234")}});
        page.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(par_mac.com_spde.TimeOut.trimmed(), QStringLiteral("1234"));
        QCOMPARE(getFileINIString(QStringLiteral("com_spde"), QStringLiteral("timeout"), QString()), QStringLiteral("1234"));

        const QString port = par_mac.lan_sch_tag(1).por_loc.trimmed();
        page.functionKey(QStringLiteral("F2"), 0);
        page.controlEvent(QStringLiteral("txt_por_loc_tag"), 0, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("4005")}});
        page.controlEvent(QStringLiteral("txt_porta_sch_tag"), 0, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("9")}});
        page.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("pro_imp_por_ser")));
        QCOMPARE(par_mac.lan_sch_tag(1).por_loc.trimmed(), port);
    }

    void theCurveLoaderFindsTheMachineRecord()
    {
        QVERIFY(caricaNewParCurTag(m_db, 1));
        QVERIFY(!cur_tag(1).descr.isEmpty());
        QSqlQuery q(m_db);
        QVERIFY(q.exec(QStringLiteral("SELECT COUNT(*) FROM CUR_TAG WHERE CAST(LIVELLO1 AS INTEGER) = 1")));
        QVERIFY(q.next());
        QCOMPARE(q.value(0).toInt(), 1);
    }
};

QTEST_GUILESS_MAIN(TestSchTagPages)
#include "tst_schtagpages.moc"
