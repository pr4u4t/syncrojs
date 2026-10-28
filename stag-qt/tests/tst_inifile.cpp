#include "inifile.h"

#include <QDir>
#include <QFile>
#include <QTemporaryDir>
#include <QTest>

class TestIniFile : public QObject
{
    Q_OBJECT

private:
    static QString writeFile(const QTemporaryDir &directory, const QByteArray &content)
    {
        const QString path = directory.filePath(QStringLiteral("test.ini"));
        QFile file(path);
        file.open(QIODevice::WriteOnly);
        file.write(content);
        return path;
    }

    static QByteArray readFile(const QString &path)
    {
        QFile file(path);
        file.open(QIODevice::ReadOnly);
        return file.readAll();
    }

private slots:
    void readsLikeGetPrivateProfileString()
    {
        QTemporaryDir directory;
        IniFile ini(writeFile(directory, "[pro_set]\r\nJob=3892\t\r\n;Lin_att=0\r\nLin_att=1\r\nName = \"quoted\" \r\nLin_att=2\r\n[Other]\r\nx=1\r\n"));
        QVERIFY(ini.load());
        QCOMPARE(ini.value("PRO_SET", "job"), QStringLiteral("3892"));
        QCOMPARE(ini.value("pro_set", "Lin_att"), QStringLiteral("1"));
        QCOMPARE(ini.value("pro_set", "Name"), QStringLiteral("quoted"));
        bool found = true;
        ini.value("pro_set", "x", &found);
        QVERIFY(!found);
        QCOMPARE(ini.value("other", "X"), QStringLiteral("1"));
    }

    void writesInPlaceAndKeepsOtherLines()
    {
        QTemporaryDir directory;
        const QString path = writeFile(directory, "[A]\r\n;comment\r\nkey=old\r\n\r\n[B]\r\ny=2\r\n");
        IniFile ini(path);
        QVERIFY(ini.load());
        ini.setValue("a", "KEY", "new");
        ini.setValue("A", "added", "1");
        ini.setValue("C", "z", "3");
        QVERIFY(ini.save());
        QCOMPARE(readFile(path), QByteArray("[A]\r\n;comment\r\nkey=new\r\nadded=1\r\n\r\n[B]\r\ny=2\r\n[C]\r\nz=3\r\n"));
    }

    void getFileIniStringWritesMissingDefault()
    {
        QTemporaryDir directory;
        const QString path = writeFile(directory, "[ORD_SET]\r\nCod_primo=9815\r\n");
        IniFile ini(path);
        QVERIFY(ini.load());
        QCOMPARE(ini.getFileINIString("ORD_SET", "Cod_primo", "9000"), QStringLiteral("9815"));
        QCOMPARE(ini.getFileINIString("ORD_SET", "Cod_primo_ese", "9000"), QStringLiteral("9000"));
        QCOMPARE(readFile(path), QByteArray("[ORD_SET]\r\nCod_primo=9815\r\nCod_primo_ese=9000\r\n"));
    }

    void referenceIniReadsAndRoundTrips()
    {
        const QString source = QDir(QStringLiteral(STAG_REFERENCE_DATA)).filePath(QStringLiteral("stag.ini"));
        if (!QFile::exists(source))
            QSKIP("reference data not available");
        QTemporaryDir directory;
        const QString copy = directory.filePath(QStringLiteral("stag.ini"));
        QVERIFY(QFile::copy(source, copy));
        IniFile ini(copy);
        QVERIFY(ini.load());
        QCOMPARE(ini.value("pro_set", "Nam_fil_dat_ord"), QStringLiteral("Ord_mac.dat"));
        QCOMPARE(ini.value("pro_set", "Job"), QStringLiteral("3892"));
        QVERIFY(ini.save());
        QCOMPARE(readFile(copy), readFile(source));
    }
};

QTEST_APPLESS_MAIN(TestIniFile)
#include "tst_inifile.moc"
