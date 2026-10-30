#include "abilitazionimodel.h"
#include "classestato.h"
#include "collezioneordini.h"
#include "finsclient.h"
#include "finssimulator.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "parcfgschtag.h"
#include "parcurtag.h"
#include "parmac.h"
#include "pagpromodel.h"
#include "plclink.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QRegularExpression>
#include <QSignalSpy>
#include <QSqlDatabase>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

static QString hexOf(const QVector<quint16> &values)
{
    QString text;
    for (const quint16 value : values)
        text += QString::number(value, 16).toUpper().rightJustified(4, u'0');
    return text;
}

// snd_str_PLC lines of the last start in the machine's Plc.log, up to "Inizializzazione PLC".
static QStringList loggedStartup()
{
    QFile file(QStringLiteral(STAG_REFERENCE_DATA "/Plc.log"));
    if (!file.open(QIODevice::ReadOnly))
        return {};
    const QStringList lines = QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts);
    int start = -1;
    for (int i = 0; i < lines.size(); ++i) {
        if (lines[i].contains(QStringLiteral("Main: avvio")))
            start = i;
    }
    QStringList frames;
    static const QRegularExpression frame(QStringLiteral(R"(snd_str_PLC: \(DM3500-DM\d+\) = '(\w+)')"));
    for (int i = start + 1; i < lines.size() && !lines[i].contains(QStringLiteral("Inizializzazione")); ++i) {
        const QRegularExpressionMatch match = frame.match(lines[i]);
        if (match.hasMatch())
            frames << match.captured(1);
    }
    return frames;
}

class TestPlcLink : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    FinsSimulator *m_plc = nullptr;
    FinsClient *m_fins = nullptr;
    ClasseStato *m_state = nullptr;
    CollezioneOrdini *m_orders = nullptr;
    PlcLink *m_link = nullptr;
    QSqlDatabase m_db;
    QStringList m_written;
    QString m_message;

    void online()
    {
        m_plc->setDm(3600, {0x0071, 0x0000});
        m_state->setComOK(true);
    }
    void setPlcOrders(int current, int next)
    {
        m_plc->setDm(3602, quint16(current));
        m_plc->setDm(3620, quint16(next));
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
            m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("link"));
        m_db.close();
        m_db.setDatabaseName(database);
        QVERIFY(m_db.open());
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        leggiCfg(m_db, iniFile());
        leggiParCurTag(m_db);
        iniLiv();
        abi_mis_ang = false;
        par_mac.indirizzo_plc.TIMER_SCANS_PLC = 0; // VB waits 5 x this before each write; not what is tested here

        delete m_link;
        delete m_orders;
        delete m_state;
        delete m_fins;
        delete m_plc;
        m_plc = new FinsSimulator;
        QVERIFY(m_plc->listen(QHostAddress::LocalHost, 0));
        m_written.clear();
        connect(m_plc, &FinsSimulator::dmWritten, this, [this](int address, const QVector<quint16> &values) {
            if (address == 3500)
                m_written << hexOf(values);
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
        m_message.clear();
    }

    void cleanupTestCase()
    {
        stag::sta_tag = nullptr;
        stag::ord_mac = nullptr;
    }

    // The safety rule of this port: without the flag not a single word reaches the PLC.
    void nothingIsWrittenWithoutTheFlag()
    {
        QVERIFY(!m_link->writesEnabled());
        online();
        setPlcOrders(1, 2);
        m_state->aggiorna(*m_fins);
        m_link->sendStartup();
        m_link->tick();
        Rmt command(QStringLiteral("test"), ChannelPlc, m_link);
        QVERIFY(command.esegui(AZZERAMENTO_CONTATORI, 0, 0));
        QSignalSpy bad(&command, &Rmt::retbad);
        m_link->tick();
        QCOMPARE(bad.count(), 1);
        QVERIFY(m_written.isEmpty());
    }

    // Same frames in the same order as the last start of the VB program on the machine.
    void startupSendsWhatTheMachineLogged()
    {
        m_link->setWritesEnabled(true);
        online();
        m_link->sendStartup();
        const QStringList logged = loggedStartup();
        QCOMPARE(logged.size(), 4);
        QCOMPARE(m_written, logged);
        QCOMPARE(m_plc->mode(), quint8(2));
    }

    void alignedOrdersSendNothing()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 0);
        m_state->aggiorna(*m_fins);
        m_link->tick();
        m_link->tick();
        QVERIFY(m_written.isEmpty());
    }

    // Lis.log 22/10/26 15.16.35: "Invio cancellazione ordine successivo. ID Ordine PLC: 9815".
    void aNextOrderLeftInThePlcIsDeleted()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 9815);
        m_state->aggiorna(*m_fins);
        QCOMPARE(m_state->cod_ord_suc(), QStringLiteral("9815"));
        m_link->tick();
        QCOMPARE(m_written, QStringList{QStringLiteral("0015")});
    }

    void aDifferentCurrentOrderIsSentWithCounterReset()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(1, 0);
        m_state->aggiorna(*m_fins);
        m_link->tick();
        const auto frame = frameOrder(OrderOperation::Send, CORRENTE, *m_orders->item(1), nullptr);
        QVERIFY(frame.has_value());
        QCOMPARE(m_written, (QStringList{frame->text(), QStringLiteral("0201")}));
    }

    void duringAnOrderChangeNothingIsAligned()
    {
        m_link->setWritesEnabled(true);
        m_plc->setDm(3600, {0x0171, 0x0000}); // MAC_CAM_ORD_IN_CORSO
        m_state->setComOK(true);
        setPlcOrders(1, 2);
        m_state->aggiorna(*m_fins);
        m_link->tick();
        QVERIFY(m_written.isEmpty());
    }

    void aChangedOrderIsSentAsAModification()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 0);
        m_state->aggiorna(*m_fins);
        m_link->tick();
        m_orders->item(1)->set_mod_id(QStringLiteral("07"));
        m_link->tick();
        QCOMPARE(m_written.size(), 1);
        QVERIFY2(m_written.first().startsWith(QStringLiteral("0023")), qPrintable(m_written.first()));
        m_link->tick();
        QCOMPARE(m_written.size(), 1);
    }

    void aQueueTakesOneRequestAtATime()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 0);
        m_state->aggiorna(*m_fins);
        Rmt first(QStringLiteral("first"), ChannelPlc, m_link);
        Rmt second(QStringLiteral("second"), ChannelPlc, m_link);
        QVERIFY(first.esegui(AZZERAMENTO_CONTATORI, 0, 0));
        QVERIFY(!second.esegui(PARAM_MAC, 0, 0));
        QSignalSpy ok(&first, &Rmt::retok);
        m_link->tick();
        QCOMPARE(ok.count(), 1);
        QCOMPARE(m_written, QStringList{QStringLiteral("0201")});
        first.resetta();
        QVERIFY(second.esegui(PARAM_MAC, 0, 0));
    }

    void cutBoardRequestsFailInsteadOfHanging()
    {
        online();
        Rmt command(QStringLiteral("sch"), ChannelCards, m_link);
        QSignalSpy bad(&command, &Rmt::retbad);
        QVERIFY(command.esegui(1, 1, 0));
        m_link->tick();
        QCOMPARE(bad.count(), 1);
    }

    void operatorKeysGoThroughTheQueue()
    {
        m_link->setWritesEnabled(true);
        online();
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 0);
        m_state->aggiorna(*m_fins);
        PagProModel page(m_orders);
        page.setCommand(new Rmt(QStringLiteral("rmt_plc_pag_pro"), ChannelPlc, m_link, &page));
        QVERIFY(page.functionKey(QStringLiteral("F4"), 2));
        QCOMPARE(m_message, msg(QStringLiteral("ope_in_cor")));
        m_link->tick();
        QCOMPARE(m_written, QStringList{QStringLiteral("0201")});
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));

        // Only one order in the file: no next one to send.
        QVERIFY(page.functionKey(QStringLiteral("F2"), 2));
        QCOMPARE(m_message, msg(QStringLiteral("msg_ord_ass")));
    }

    void aLevelSwitchIsRefusedUnlessTheMachineIsStopped()
    {
        m_link->setWritesEnabled(true);
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("abilitazioni_plc"), ChannelPlc, m_link, &page));
        pro_set.ute_wrk = true;
        par_abi.liv(2) = true;
        m_state->setComOK(true);
        page.activate(QString());
        page.controlEvent(QStringLiteral("liv"), 2, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        QCOMPARE(m_message, msg(QStringLiteral("mac_non_fas_sto")));
        QVERIFY(par_abi.liv(2));
        QVERIFY(m_written.isEmpty());
    }

    // VB fills the frame from par_abi, which only takes the new value in retok: the PLC gets the old one.
    void aLevelSwitchIsSavedAfterTheSendWhichStillCarriesTheOldValues()
    {
        m_link->setWritesEnabled(true);
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("abilitazioni_plc"), ChannelPlc, m_link, &page));
        pro_set.ute_wrk = true;
        par_abi.liv(2) = true;
        m_plc->setDm(3600, {0x0074, 0x0001}); // F stop and manual
        m_state->setComOK(true);
        m_state->aggiorna(*m_fins);
        QVERIFY(m_state->bit(ClasseStato::MAC_F_STOP, 1));
        QVERIFY(m_state->bit(ClasseStato::MAC_IN_MANUALE, 1));
        setPlcOrders(m_orders->item(1)->stag_id().toInt(), 0);
        m_state->aggiorna(*m_fins);
        page.activate(QString());
        const QString before = frameParCfgMac().text();

        page.controlEvent(QStringLiteral("liv"), 2, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        QVERIFY(par_abi.liv(2));
        QCOMPARE(getFileINIString(QStringLiteral("abi_liv_2"), QStringLiteral("liv"), QString()), QStringLiteral("0"));
        m_link->tick();
        QCOMPARE(m_written, QStringList{before});
        QVERIFY(!par_abi.liv(2));
        QCOMPARE(page.value(QStringLiteral("liv(2)")).toString(), QStringLiteral("OFF"));
        QVERIFY(frameParCfgMac().text() != before);
    }

    void aFailedLevelSwitchRollsBackThePageAndTheFile()
    {
        CollezioneOrdini empty;
        AbilitazioniModel page(&empty);
        page.setCommand(new Rmt(QStringLiteral("abilitazioni_plc"), ChannelPlc, m_link, &page));
        pro_set.ute_wrk = true;
        par_abi.liv(2) = true;
        m_plc->setDm(3600, {0x0074, 0x0001});
        m_state->setComOK(true);
        m_state->aggiorna(*m_fins);
        page.activate(QString());

        page.controlEvent(QStringLiteral("liv"), 2, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        m_link->tick(); // writes are off, so the request fails
        QVERIFY(par_abi.liv(2));
        QVERIFY(page.isOn(QStringLiteral("liv(2)")));
        QCOMPARE(getFileINIString(QStringLiteral("abi_liv_2"), QStringLiteral("liv"), QString()), QStringLiteral("1"));
        QVERIFY(m_written.isEmpty());
    }
};

QTEST_GUILESS_MAIN(TestPlcLink)
#include "tst_plclink.moc"
