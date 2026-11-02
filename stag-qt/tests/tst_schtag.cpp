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
#include "plclink.h"
#include "schtagframes.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QRegularExpression>
#include <QSignalSpy>
#include <QSqlDatabase>
#include <QTemporaryDir>
#include <QTest>
#include <QUdpSocket>

using namespace stag;

static QString body(const QString &frame)
{
    return frame.mid(1, frame.size() - 2);
}

static QStringList logLines(const char *name)
{
    QFile file(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name));
    if (!file.open(QIODevice::ReadOnly))
        return {};
    return QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts);
}

// The far end of the wire: answers what the test says and remembers what came.
class BoardSimulator : public QObject
{
    Q_OBJECT
public:
    QUdpSocket socket;
    QStringList frames;
    int syn = 0;
    QByteArray stateReply;
    QByteArray writeReply = QByteArray(1, ACK_ST);

    explicit BoardSimulator()
    {
        socket.bind(QHostAddress::LocalHost, 0);
        connect(&socket, &QUdpSocket::readyRead, this, [this] {
            while (socket.hasPendingDatagrams()) {
                QByteArray data(int(socket.pendingDatagramSize()), Qt::Uninitialized);
                QHostAddress host;
                quint16 port = 0;
                socket.readDatagram(data.data(), data.size(), &host, &port);
                if (data.size() == 1 && data[0] == SYN_ST) {
                    ++syn;
                    continue;
                }
                frames << QString::fromLatin1(data);
                const QByteArray &reply = data[1] == '3' ? stateReply : writeReply;
                if (!reply.isEmpty())
                    socket.writeDatagram(reply, host, port);
            }
        });
    }

    static QByteArray state(const QString &next, const QString &current)
    {
        const QString text = QLatin1Char(STX_ST) + next + current;
        return (text + boardChecksum(text) + QLatin1Char(EOT_ST)).toLatin1();
    }
};

class TestSchTag : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    FinsSimulator *m_plc = nullptr;
    FinsClient *m_fins = nullptr;
    ClasseStato *m_state = nullptr;
    CollezioneOrdini *m_orders = nullptr;
    PlcLink *m_link = nullptr;
    BoardSimulator *m_board = nullptr;
    QSqlDatabase m_db;
    QStringList m_written;

    void wire(int liv = 1)
    {
        par_mac.tip_com_sch_tag(liv) = "1";
        par_mac.lan_sch_tag_UDP(liv) = true;
        par_mac.lan_sch_tag(liv).por_loc = "0";
        par_mac.lan_sch_tag(liv).por_rem = QString::number(m_board->socket.localPort());
        par_mac.lan_sch_tag(liv).ind_rem = QStringLiteral("127.0.0.1");
        par_mac.com_sch_tag(liv).TimeOut = "60";
        par_mac.com_sch_tag(liv).com_att = false;
    }

    // DM3600 on, change of order completed, board of level 1 up as the option asks.
    void machine(bool ready = false)
    {
        QVector<quint16> dm{0x0071, 0x0000};
        if (ready) {
            const int bit = ClasseStato::SCH_TAG_LIV_1_PRONTA;
            dm[bit / 16] |= quint16(1 << (bit % 16));
        }
        m_plc->setDm(3600, dm);
        m_state->setComOK(true);
        m_state->aggiorna(*m_fins);
    }

private slots:
    void initTestCase()
    {
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        messaggioHook = [](const QString &, bool, bool) {};
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
            m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("schtag"));
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
        m_board = new BoardSimulator;
        for (int i = 1; i <= 3; ++i) {
            com_ini_sch_tag(i) = false;
            par_mac.opt.cut_pat(i) = "0";
            par_mac.opt.cut_pat_auto(i) = "0";
        }
        inv_ord_sch_tag(1) = false;
        inv_ord_sch_tag(2) = false;
        wire();
    }

    void cleanupTestCase()
    {
        stag::sta_tag = nullptr;
        stag::ord_mac = nullptr;
    }

    // Every frame VB put on the wire in both logs, rebuilt byte for byte.
    void everyLoggedFrameIsRebuilt()
    {
        static const QRegularExpression frame(QStringLiteral("\\| (\\x02[^\\x04]*\\x04)$"));
        int reads = 0;
        int orders = 0;
        for (const char *name : {"Sch_tag.log", "Sch_tag_old.log"}) {
            for (const QString &line : logLines(name)) {
                const QRegularExpressionMatch match = frame.match(line);
                if (!match.hasMatch())
                    continue;
                const QString logged = match.captured(1);
                if (logged[1] == u'3') {
                    QCOMPARE(logged, boardReadVector(IND_INI_LET_ID_ORD, NUM_WRD_ID_ORD));
                    ++reads;
                } else {
                    const QString id = body(logged).mid(5 + 12 * 4, 4);
                    QCOMPARE(logged, boardWriteVector(IND_INI_SCR_DAT_ORD, boardOrderDummy(id)));
                    ++orders;
                }
            }
        }
        qInfo() << reads << "reads," << orders << "orders";
        QVERIFY(reads > 9000);
        QVERIFY(orders >= 47);
    }

    // Every distinct answer in the logs gives the ids of the next "Nuovo stato" line.
    void loggedAnswersGiveTheLoggedIds()
    {
        static const QRegularExpression rx(QStringLiteral("rx = '\\x02(\\d{4})(\\d{4})(\\w\\w)\\x04'"));
        static const QRegularExpression state(QStringLiteral("Nuovo stato su livello 1 ID Corrente = (\\d{4}) ID Successivo = (\\d{4})"));
        m_link->setWritesEnabled(true);
        SchTagLink &boards = m_link->boards();
        int checked = 0;
        QString last;
        const QStringList lines = logLines("Sch_tag.log");
        for (int i = 0; i < lines.size(); ++i) {
            const QRegularExpressionMatch match = rx.match(lines[i]);
            if (!match.hasMatch())
                continue;
            const QString text = QLatin1Char(STX_ST) + match.captured(1) + match.captured(2);
            QCOMPARE(match.captured(3), boardChecksum(text));
            if (match.captured(1) + match.captured(2) == last)
                continue;
            last = match.captured(1) + match.captured(2);
            m_board->stateReply = BoardSimulator::state(match.captured(1), match.captured(2));
            QCOMPARE(boards.snd(1, LETTURA_STATO_ORDINE), int(RET_OK));
            QCOMPARE(boards.idOrd(1, SUCCESSIVO), match.captured(1));
            QCOMPARE(boards.idOrd(1, CORRENTE), match.captured(2));
            ++checked;
            if (i + 1 < lines.size()) {
                const QRegularExpressionMatch logged = state.match(lines[i + 1]);
                if (logged.hasMatch()) {
                    QCOMPARE(logged.captured(1), match.captured(2));
                    QCOMPARE(logged.captured(2), match.captured(1));
                }
            }
        }
        qInfo() << checked << "distinct answers";
        QVERIFY(checked > 10);
    }

    // SYN resets the slave: without the flag not even a read goes out.
    void nothingLeavesWithoutTheFlag()
    {
        par_mac.opt.cut_pat(1) = "1";
        com_ini_sch_tag(1) = true;
        machine(true);
        QCOMPARE(m_link->boards().snd(1, LETTURA_STATO_ORDINE), int(RET_BAD));
        m_link->tick();
        Rmt command(QStringLiteral("test"), ChannelCards, m_link);
        QVERIFY(command.esegui(SELEZIONE_CONTRASTO, 1, 0, 2));
        QSignalSpy bad(&command, &Rmt::retbad);
        m_link->tick();
        QCOMPARE(bad.count(), 1);
        QTest::qWait(50);
        QCOMPARE(m_board->syn, 0);
        QVERIFY(m_board->frames.isEmpty());
        QVERIFY(m_written.isEmpty());
    }

    // The sequence of Sch_tag.log: read, mismatch, order, ACK, confirmation 0403 to the PLC.
    void aMisalignedBoardGetsTheOrderAndThePlcTheConfirmation()
    {
        m_link->setWritesEnabled(true);
        par_mac.opt.cut_pat(1) = "1";
        com_ini_sch_tag(1) = true;
        const QString current = m_orders->item(1)->stag_id();
        const QString next = m_orders->item(2)->stag_id();
        m_board->stateReply = BoardSimulator::state(next, QStringLiteral("0001"));
        m_plc->setDm(3602, current.toUShort());
        m_plc->setDm(3620, next.toUShort());
        machine();
        m_link->tick();

        QVERIFY(m_board->frames.size() >= 2);
        QCOMPARE(m_board->frames[0], boardReadVector(IND_INI_LET_ID_ORD, NUM_WRD_ID_ORD));
        QVERIFY(m_board->frames[1].startsWith(QLatin1Char(STX_ST) + QStringLiteral("434E0")));
        QCOMPARE(body(m_board->frames[1]).mid(5 + 12 * 4, 4), current);
        QCOMPARE(m_board->frames.size(), 2);
        QCOMPARE(m_written.last().left(4), QStringLiteral("0403"));
    }

    void anAlignedBoardIsLeftAlone()
    {
        m_link->setWritesEnabled(true);
        par_mac.opt.cut_pat(1) = "1";
        com_ini_sch_tag(1) = true;
        const QString current = m_orders->item(1)->stag_id();
        const QString next = m_orders->item(2)->stag_id();
        m_board->stateReply = BoardSimulator::state(next, current);
        m_plc->setDm(3602, current.toUShort());
        m_plc->setDm(3620, next.toUShort());
        machine();
        m_link->tick();
        QCOMPARE(m_board->frames.size(), 1);
        QVERIFY(m_written.isEmpty());
    }

    void aSilentBoardCostsThreeAttemptsAndNoConfirmation()
    {
        m_link->setWritesEnabled(true);
        par_mac.opt.cut_pat(1) = "1";
        com_ini_sch_tag(1) = true;
        m_plc->setDm(3602, m_orders->item(1)->stag_id().toUShort());
        m_plc->setDm(3620, m_orders->item(2)->stag_id().toUShort());
        machine();
        m_link->tick();
        QCOMPARE(m_board->frames.size(), 3);
        QCOMPARE(m_board->syn, 1);
        QVERIFY(!m_link->boards().comOK(1));
        QVERIFY(m_written.isEmpty());
    }

    void aNakOrABadChecksumIsRetried()
    {
        m_link->setWritesEnabled(true);
        m_board->writeReply = QByteArray(1, NAK_ST);
        QCOMPARE(m_link->boards().snd(1, SELEZIONE_CONTRASTO, 1, 2), int(RET_BAD));
        QCOMPARE(m_board->frames.size(), 3);

        m_board->frames.clear();
        m_board->stateReply = QByteArray("\x02") + "98079805" + "00" + "\x04";
        QCOMPARE(m_link->boards().snd(1, LETTURA_STATO_ORDINE), int(RET_BAD));
        QCOMPARE(m_board->frames.size(), 3);
    }

    // No CONFIGURA in the logs: checked against the VB source only. Up on the first part's ACK, as in VB.
    void theFirstConfigurationGoesOutInTwoParts()
    {
        m_link->setWritesEnabled(true);
        par_mac_ute.sch_tag.sch_tag_liv(1).lun_tag_tes = "1234";
        par_mac_ute.par_cfg_att(1).vel_max_tag = "300";
        machine(true);
        m_link->tick();
        QVERIFY(com_ini_sch_tag(1));
        QCOMPARE(m_board->frames.size(), 2);
        const QString first = body(m_board->frames[0]);
        const QString second = body(m_board->frames[1]);
        QVERIFY(first.startsWith(QStringLiteral("43400")));
        QVERIFY(second.startsWith(QStringLiteral("43500")));
        QCOMPARE(first.size(), 5 + 80 * 4 + 2);
        QCOMPARE(second.size(), 5 + 23 * 4 + 2);
        QCOMPARE(first.mid(5 + 17 * 4, 4), QStringLiteral("04D2"));
        QCOMPARE(first.mid(5 + 23 * 4, 4), QStringLiteral("012C"));
    }

    void theConfigurationFollowsThePatternOrder()
    {
        par_mac.opt.cut_pat(1) = "1";
        par_mac_ute.sch_tag.sch_tag_liv(1).off_tag_reg = "11";
        const auto plain = boardConfig(1, nullptr);
        QCOMPARE(plain[0].mid(64 * 4, 4), QStringLiteral("000B"));
        par_mac.opt.cut_pat(1) = "0";
        par_mac.opt.cut_mar = "0";
        QCOMPARE(boardConfig(1, nullptr)[0].mid(64 * 4, 4), QStringLiteral("0000"));
    }

    // Worked by hand from snd_sch_tag: no real pattern order was sent in the logs.
    void aPatternOrderSetsItsBits()
    {
        const OrdinePtr order = m_orders->item(1);
        order->setText(QStringLiteral("ord(1).L1"), QStringLiteral("100"));
        order->setText(QStringLiteral("ord(1).L2"), QStringLiteral("200"));
        order->setText(QStringLiteral("ord(1).cut_to_mark"), QStringLiteral("2"));
        order->setText(QStringLiteral("ord(1).fst_FTC"), QStringLiteral("1"));
        order->setText(QStringLiteral("ord(1).pat_con"), QStringLiteral("4"));
        order->setText(QStringLiteral("ord(1).Ofs_Cut"), QStringLiteral("5"));
        order->setText(QStringLiteral("ord(1).fin_pri"), QStringLiteral("6"));
        order->setText(QStringLiteral("ord(1).fin_dop"), QStringLiteral("7"));
        const QString text = boardOrder(*order, 1);
        QCOMPARE(text.left(8), QStringLiteral("006400C8"));
        QCOMPARE(text.mid(10 * 4, 4), QStringLiteral("000F")); // 1 + 2 + 4 + 8
        QCOMPARE(text.mid(11 * 4, 4), QStringLiteral("0005"));
        QCOMPARE(text.mid(12 * 4, 4), order->stag_id().leftJustified(4, u' ', true));
        QCOMPARE(text.mid(14 * 4), QStringLiteral("00060007"));
    }

    void contrastSelectionUsesTheDummyWithFfff()
    {
        const QString text = boardContrast(3);
        QCOMPARE(text.mid(10 * 4, 4), QStringLiteral("0008"));
        QCOMPARE(text.mid(12 * 4, 4), QStringLiteral("FFFF"));
        QCOMPARE(text.left(4), QStringLiteral("001E"));
    }

    void theCardQueueAnswersThePage()
    {
        m_link->setWritesEnabled(true);
        machine();
        Rmt command(QStringLiteral("test"), ChannelCards, m_link);
        QVERIFY(command.esegui(SELEZIONE_CONTRASTO, 1, 0, 2));
        QSignalSpy ok(&command, &Rmt::retok);
        m_link->tick();
        QCOMPARE(ok.count(), 1);
        QCOMPARE(body(m_board->frames.last()).mid(5 + 10 * 4, 4), QStringLiteral("0004"));
    }

    // The macro of adj_pat_con: contrast to the board, then the confirmation to the PLC.
    void theContrastMacroConfirmsToThePlc()
    {
        m_link->setWritesEnabled(true);
        machine();
        Rmt command(QStringLiteral("test"), ChannelMacro, m_link);
        QVERIFY(command.esegui(ACQ_CON_SCH_PLC, 1, 0, 1));
        QSignalSpy ok(&command, &Rmt::retok);
        m_link->tick();
        QCOMPARE(ok.count(), 1);
        QCOMPARE(m_written.last().left(4), QStringLiteral("0403"));
    }
};

QTEST_GUILESS_MAIN(TestSchTag)
#include "tst_schtag.moc"
