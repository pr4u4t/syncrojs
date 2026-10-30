#include "collezioneordini.h"
#include "gesord.h"
#include "inibas.h"
#include "inistore.h"
#include "parcfgschtag.h"
#include "parcurtag.h"
#include "parmac.h"
#include "plcframes.h"

#include <QDate>
#include <QFile>
#include <QHash>
#include <QRegularExpression>
#include <QSqlDatabase>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

// Every snd_str_PLC frame the VB program wrote to the machine's logs, oldest first.
static QStringList loggedFrames(int words)
{
    QStringList frames;
    const QString wanted = QStringLiteral("(DM3500-DM35%1)").arg(words, 2, 10, QLatin1Char('0'));
    for (const char *name : {"Plc_old.log", "Plc.log"}) {
        QFile file(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name));
        if (!file.open(QIODevice::ReadOnly))
            continue;
        for (const QString &line : QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts)) {
            const int at = line.indexOf(wanted);
            if (at >= 0)
                frames << line.mid(line.indexOf(u'\'', at) + 1).chopped(1);
        }
    }
    return frames;
}

// The 28-word order frames with the date of their log line.
static QList<QPair<QDate, QString>> loggedOrderFrames()
{
    QList<QPair<QDate, QString>> frames;
    static const QRegularExpression line(QStringLiteral(R"(^(\d\d)/(\d\d)/(\d\d) \| \S+ \| snd_str_PLC: \(DM3500-DM3528\) = '(\w+)')"));
    for (const char *name : {"Plc_old.log", "Plc.log"}) {
        QFile file(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name));
        if (!file.open(QIODevice::ReadOnly))
            continue;
        for (const QString &text : QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts)) {
            const QRegularExpressionMatch match = line.match(text);
            if (match.hasMatch())
                frames.append({QDate(2000 + match.captured(3).toInt(), match.captured(2).toInt(), match.captured(1).toInt()), match.captured(4)});
        }
    }
    return frames;
}

class TestPlcFrames : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    QSqlDatabase m_db;

private slots:
    void initTestCase()
    {
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            const QString target = m_dir.filePath(QString::fromLatin1(name));
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), target);
            QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(m_dir.path());
        legParSup(iniFile(), QString(), m_dir.path() + u'/');
        m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("frames"));
        // leggiCfg marks the active record, so it must not run on the committed database.
        const QString database = m_dir.filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), database));
        QFile::setPermissions(database, QFile::ReadOwner | QFile::WriteOwner);
        m_db.setDatabaseName(database);
        QVERIFY(m_db.open());
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        leggiCfg(m_db, iniFile());
        leggiParCurTag(m_db);
        iniLiv();
        abi_mis_ang = false;
    }

    void startupFramesMatchTheLastLogEntry_data()
    {
        QTest::addColumn<int>("words");
        QTest::addColumn<QString>("built");
        QTest::newRow("cutting curve 1") << 35 << frameCurTag(1).text();
        QTest::newRow("machine configuration") << 6 << frameParCfgMac().text();
        QTest::newRow("machine parameters") << 23 << frameParMac().text();
    }

    void startupFramesMatchTheLastLogEntry()
    {
        QFETCH(int, words);
        QFETCH(QString, built);
        const QStringList frames = loggedFrames(words);
        QVERIFY(!frames.isEmpty());
        if (words == 35) {
            QString level1;
            for (const QString &frame : frames) {
                if (frame.mid(4, 4) == QLatin1String("0002"))
                    level1 = frame;
            }
            QCOMPARE(built, level1);
        } else {
            QCOMPARE(built, frames.last());
        }
    }

    void secondCuttingCurveMatchesTheLog()
    {
        const QStringList frames = loggedFrames(35);
        QString level2;
        for (const QString &frame : frames) {
            if (frame.mid(4, 4) == QLatin1String("0004"))
                level2 = frame;
        }
        QCOMPARE(frameCurTag(2).text(), level2);
    }

    // Executed orders keep their format data, so the frames VB sent can be rebuilt from Ord_mac_ese.dat.
    // Those records are renumbered (old_stag_id is the id sent) and ids come back after a while, so a
    // frame is only compared with the order produced on the date it was sent or the day after.
    void orderFramesMatchTheLog()
    {
        CollezioneOrdini done;
        QCOMPARE(done.Init(true), int(RET_OK));
        done.Leg_dat_ord();
        QHash<int, OrdinePtr> byId;
        for (int i = 1; i <= done.Count(); ++i)
            byId.insert(int(vb::val(done.item(i)->old_stag_id())), done.item(i));

        int compared = 0;
        QStringList different;
        for (const auto &[date, frame] : loggedOrderFrames()) {
            const OrdinePtr order = byId.value(frame.mid(8, 4).toInt(nullptr, 16));
            if (!order)
                continue;
            // dat_ini is MM/dd/yy with the year cut to its first two digits, as VB stored it.
            const QString started = order->text(QStringLiteral("rep_pro.dat_ini"));
            const QDate produced(date.year(), started.left(2).toInt(), started.mid(3, 2).toInt());
            const qint64 days = date.daysTo(produced);
            if (days < 0 || days > 1)
                continue;
            const int command = frame.left(4).toInt(nullptr, 16);
            const auto built = frameOrder((command & 32) ? OrderOperation::Modify : OrderOperation::Send, (command >> 1) & 3, *order, nullptr);
            QVERIFY(built.has_value());
            ++compared;
            QString text = built->text();
            text.replace(8, 4, frame.mid(8, 4)); // stag_id holds the new number
            if (text != frame)
                different << QStringLiteral("%1\nlog   %2\nbuilt %3").arg(date.toString(Qt::ISODate), frame, text);
        }
        qInfo().noquote() << "order frames compared:" << compared << "different:" << different.size();
        for (const QString &text : different.mid(0, 8))
            qInfo().noquote() << text;
        QVERIFY2(compared >= 95, qPrintable(QString::number(compared)));
        QVERIFY2(different.isEmpty(), qPrintable(QString::number(different.size())));
    }

    void currentOrderMatchesItsLastFrame()
    {
        CollezioneOrdini current;
        QCOMPARE(current.Init(false), int(RET_OK));
        current.Leg_dat_ord();
        const auto built = frameOrder(OrderOperation::Send, 2, *current.item(1), nullptr);
        QVERIFY(built.has_value());
        QCOMPARE(built->text(), loggedOrderFrames().last().second);
    }

    void commandWordsFollowTheVbConstants()
    {
        QCOMPARE(frameDeleteOrder(1).text(), QStringLiteral("0013"));
        QCOMPARE(frameDeleteOrder(2).text(), QStringLiteral("0015"));
        QCOMPARE(frameResetCounters().text(), QStringLiteral("0201"));
        QCOMPARE(frameConfirmCutBoards().text(), QStringLiteral("0403"));
        QCOMPARE(frameConfirmCutBoards().logLine(), QStringLiteral("snd_str_PLC: (DM3500-DM3501) = '0403'"));
    }
};

QTEST_GUILESS_MAIN(TestPlcFrames)
#include "tst_plcframes.moc"
