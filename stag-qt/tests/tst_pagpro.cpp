#include "classestato.h"
#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "pagpromodel.h"
#include "parmac.h"
#include "stabarmodel.h"

#include <QFile>
#include <QRegularExpression>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

// Lines of a machine log as text; the plant's Windows wrote them in cp1252, only ASCII is read here.
static QStringList logLines(const char *name)
{
    QFile file(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name));
    if (!file.open(QIODevice::ReadOnly))
        return {};
    return QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts);
}

class TestPagPro : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    CollezioneOrdini *m_orders = nullptr;

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
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        iniLiv();
        abi_mis_ang = false;
        m_orders = new CollezioneOrdini(this);
        QCOMPARE(m_orders->Init(false), int(RET_OK));
        m_orders->Leg_dat_ord();
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    // Last "Corr.dis. Plc=9815, Stag=9815, lev=2: Plc=0, Stag=1194" written by the VB program. lev is
    // the format inside the order (stato.cls logs its loop index), not the knife level; Plc is what
    // the PLC held for that format's level, and the "Plc_cur: id*lev1*lev2*lev3" line just before says the same.
    void lastOrderLogEntryMatchesTheOrderFile()
    {
        const QStringList lines = logLines("Ord.log");
        static const QRegularExpression entry(QStringLiteral(R"(Corr\.dis\. Plc=(\d+)\s*, Stag=(\d+), lev=(\d): Plc=(\d+), Stag=(\d+))"));
        static const QRegularExpression current(QStringLiteral(R"(Plc_cur: (\d+)\*(\d+)\*(\d+)\*(\d+))"));
        int at = lines.size() - 1;
        while (at >= 0 && !entry.match(lines[at]).hasMatch())
            --at;
        QVERIFY(at > 0);
        const QRegularExpressionMatch match = entry.match(lines[at]);
        const QRegularExpressionMatch plc = current.match(lines[at - 1]);
        QVERIFY(plc.hasMatch());

        const OrdinePtr order = m_orders->item(1);
        QCOMPARE(order->stag_id(), match.captured(2));
        const int format = match.captured(3).toInt();
        QCOMPARE(QString::number(vb::intDiv(vb::val(order->ord(format, QStringLiteral("sca_lun"))), TODECMM)), match.captured(5));
        QCOMPARE(plc.captured(1 + order->Ord_liv(format)), match.captured(4));

        PagProModel model(m_orders);
        model.refresh();
        QCOMPARE(model.value(QStringLiteral("cor_lun_sca(%1)").arg(format - 1)).toString(), match.captured(5));
    }

    void showsCurrentAndNextOrderFields()
    {
        PagProModel model(m_orders);
        model.refresh();
        const OrdinePtr current = m_orders->item(1);
        QCOMPARE(model.value(QStringLiteral("cor_cod_ord")).toString(), current->cod_ord_abb());
        // The reference file holds a single order, so the next-order block stays empty.
        QCOMPARE(m_orders->Count(), 1);
        QCOMPARE(model.value(QStringLiteral("suc_cod_ord")).toString(), QString());
        QCOMPARE(model.value(QStringLiteral("cor_tip_ond")).toString(), current->text(QStringLiteral("tip_car.Cod_ond")).trimmed());
        QCOMPARE(model.value(QStringLiteral("cor_com(0)")).toString(), current->ord(1, QStringLiteral("com.cod_com_ord")).trimmed());
    }

    void levelTextsComeFromTheMessageTable()
    {
        PagProModel model(m_orders);
        model.refresh();
        const QStringList known{msg(QStringLiteral("liv_bas")), msg(QStringLiteral("liv_med")), msg(QStringLiteral("liv_alt"))};
        QVERIFY(!known.first().isEmpty());
        QVERIFY(known.contains(model.value(QStringLiteral("cor_liv(0)")).toString()));
    }

    void missingOrdersBlankTheirBlock()
    {
        CollezioneOrdini empty;
        PagProModel model(&empty);
        model.refresh();
        QCOMPARE(model.value(QStringLiteral("cor_cod_ord")).toString(), QString());
        QCOMPARE(model.value(QStringLiteral("suc_met(2)")).toString(), QString());
    }

    void inchesFollowUniMisAng()
    {
        abi_mis_ang = true;
        PagProModel model(m_orders);
        model.refresh();
        abi_mis_ang = false;
        const OrdinePtr current = m_orders->item(1);
        QCOMPARE(model.value(QStringLiteral("cor_lun_sca(0)")).toString(),
                 QString::number(vb::val(current->ord(1, QStringLiteral("sca_lun"))) / 254.0, 'f', 4));
    }

    void orderStatusColoursFollowSetCol()
    {
        PagProModel model(m_orders);
        model.orderStatus(ClasseStato::ord_ok, ClasseStato::CORR);
        QCOMPARE(model.value(QStringLiteral("lab_ord(1)")).toString(), msg(QStringLiteral("msg_ord_in_lav")));
        QCOMPARE(model.value(QStringLiteral("frm_sta(1)"), QStringLiteral("backColor")).toString(), QStringLiteral("#0088ff"));
        QCOMPARE(model.value(QStringLiteral("lab_ord(1)"), QStringLiteral("foreColor")).toString(), QStringLiteral("#ffffff"));
        model.orderStatus(ClasseStato::ord_nok, ClasseStato::SUCC);
        QCOMPARE(model.value(QStringLiteral("lab_ord(2)"), QStringLiteral("backColor")).toString(), QStringLiteral("#c00000"));
    }

    // The last "Stato lista" block is the list the file holds.
    void listLogAgreesWithOrderFile()
    {
        const QStringList lines = logLines("Lis.log");
        int start = -1;
        for (int i = lines.size() - 1; i >= 0 && start < 0; --i) {
            if (lines[i].contains(QStringLiteral("Stato lista:")))
                start = i;
        }
        QVERIFY(start >= 0);
        static const QRegularExpression row(QStringLiteral(R"(\|\s+(\d+)\s*\|([^|]*)\|([^|]*)\|$)"));
        QStringList logged;
        for (int i = start + 1; i < lines.size(); ++i) {
            const QRegularExpressionMatch match = row.match(lines[i]);
            if (!match.hasMatch())
                break;
            logged << match.captured(3).trimmed();
        }
        QStringList file;
        for (int i = 1; i <= m_orders->Count(); ++i)
            file << m_orders->item(i)->stag_id().trimmed();
        QVERIFY(!logged.isEmpty());
        QCOMPARE(logged, file);
    }
};

QTEST_GUILESS_MAIN(TestPagPro)
#include "tst_pagpro.moc"
