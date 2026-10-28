#include "collezioneordini.h"
#include "inistore.h"
#include "parmac.h"
#include "recordfile.h"

#include <QFile>
#include <QSignalSpy>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestOrdini : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;

    void copy(const QString &name)
    {
        const QString target = m_dir.filePath(name);
        QFile::remove(target);
        QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + name, target);
        QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
    }

private slots:
    void init()
    {
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"})
            copy(QString::fromLatin1(name));
        setDataDirectory(m_dir.path());
        pro_set.Nam_fil_uni = QStringLiteral("Ordini");
        pro_set.Nam_fil_dat_ord = QStringLiteral("Ord_mac.dat");
        pro_set.Nam_fil_dat_ord_ese = QStringLiteral("Ord_mac_ese.dat");
        par_abi.abi_com_PU = true;
    }

    void loadsReferenceFiles()
    {
        CollezioneOrdini ordMac;
        QCOMPARE(ordMac.Init(false), int(RET_OK));
        QCOMPARE(ordMac.Init(false), int(RET_BAD));
        ordMac.Leg_dat_ord();
        QVERIFY(ordMac.Count() > 0);
        QCOMPARE(ordMac.item(1)->stag_id(), QStringLiteral("9815"));
        QCOMPARE(ordMac.item(1)->cod_ord_abb(), QStringLiteral("5291.2     "));
        QCOMPARE(ordMac.item(1)->ord(1, QStringLiteral("com.cod_com_ord")).trimmed(), QStringLiteral("R1212026101"));
        QCOMPARE(ordMac.item(ordMac.Count() + 1)->stag_id(), QString(4, QChar(0)));
        QCOMPARE(ordMac.leg_pun_ord(), 1);

        CollezioneOrdini ordMacEse;
        ordMacEse.Init(true);
        ordMacEse.Leg_dat_ord();
        QVERIFY(ordMacEse.Count() > 0);
    }

    void saveIsByteIdentical()
    {
        const QByteArray before = [&] { QFile f(m_dir.filePath(QStringLiteral("Ord_mac.dat"))); f.open(QIODevice::ReadOnly); return f.readAll(); }();
        CollezioneOrdini ordMac;
        ordMac.Init(false);
        ordMac.Leg_dat_ord();
        QSignalSpy refresh(&ordMac, &CollezioneOrdini::Refresh);
        ordMac.Salva(false);
        QCOMPARE(refresh.size(), 1);
        QFile f(m_dir.filePath(QStringLiteral("Ord_mac.dat")));
        QVERIFY(f.open(QIODevice::ReadOnly));
        const QByteArray after = f.readAll();
        // Leg_dat_ord drops unused records in the middle, Salva compacts them; with no gaps the file is unchanged.
        QCOMPARE(after.size(), before.size());
        QCOMPARE(after.left(ordMac.Count() * kOrderRecordSize), before.left(ordMac.Count() * kOrderRecordSize));
    }

    void addMoveDelete()
    {
        CollezioneOrdini ordMac;
        ordMac.Init(false);
        ordMac.Leg_dat_ord();
        const int count = ordMac.Count();
        const QString first = ordMac.item(1)->stag_id();

        const QString codUni = getFileINIString(QStringLiteral("ORD_SET"), QStringLiteral("Cod_uni"), QStringLiteral("9000"), QStringLiteral("Ordini"));
        QCOMPARE(ordMac.add(ordMac.item(1)), int(RET_OK));
        QCOMPARE(ordMac.Count(), count + 1);
        QCOMPARE(ordMac.item(count + 1)->stag_id(), vb::fixedLength(QString::number(int(vb::val(codUni))), 4));
        QCOMPARE(ordMac.item(count + 1)->mod_id(), QStringLiteral("00"));
        QCOMPARE(ordMac.leg_pun_ord(), count + 1);

        // Without sta_tag the machine counts as automatic with SPDE ok: moving is refused, like the VB code.
        QCOMPARE(ordMac.Move_blk(count + 1, count + 1, 1), int(RET_BAD));
        QCOMPARE(ordMac.Del_blk(count + 1, count + 1, false, true), int(RET_OK));
        QCOMPARE(ordMac.Count(), count);
        QCOMPARE(ordMac.item(1)->stag_id(), first);

        QCOMPARE(ordMac.Tro_id_lis(first.trimmed()), int(RET_OK));
        QCOMPARE(ordMac.leg_pun_ord(), 1);
        QCOMPARE(ordMac.Tro_lis(QStringLiteral(" 5291.2 ")), int(RET_OK));

        CollezioneOrdini reread;
        reread.Init(false);
        reread.Leg_dat_ord();
        QCOMPARE(reread.Count(), count);
    }
};

QTEST_GUILESS_MAIN(TestOrdini)
#include "tst_ordini.moc"
