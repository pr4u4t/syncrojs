#include "vbcompat.h"

#include <QTest>

class TestVbCompat : public QObject
{
    Q_OBJECT

private slots:
    void val_data()
    {
        QTest::addColumn<QString>("text");
        QTest::addColumn<double>("expected");
        QTest::newRow("plain") << QStringLiteral("123") << 123.0;
        QTest::newRow("padding") << QStringLiteral("  0028000") << 28000.0;
        QTest::newRow("inner spaces") << QStringLiteral(" 1 2 3") << 123.0;
        QTest::newRow("trailing text") << QStringLiteral("12abc") << 12.0;
        QTest::newRow("decimal") << QStringLiteral("-1.5") << -1.5;
        QTest::newRow("exponent D") << QStringLiteral("1D3") << 1000.0;
        QTest::newRow("hex integer") << QStringLiteral("&HFFFF") << -1.0;
        QTest::newRow("hex long") << QStringLiteral("&H10000") << 65536.0;
        QTest::newRow("octal") << QStringLiteral("&O17") << 15.0;
        QTest::newRow("empty") << QString() << 0.0;
        QTest::newRow("nul record") << QString(4, QChar(0)) << 0.0;
        QTest::newRow("text") << QStringLiteral("abc") << 0.0;
    }

    void val()
    {
        QFETCH(QString, text);
        QFETCH(double, expected);
        QCOMPARE(vb::val(text), expected);
    }

    void cLngRoundsHalfToEven()
    {
        QCOMPARE(vb::cLng(2.5), 2);
        QCOMPARE(vb::cLng(3.5), 4);
        QCOMPARE(vb::cLng(-2.5), -2);
        QCOMPARE(vb::cLng(2.51), 3);
        bool ok = true;
        vb::cLng(3e9, &ok);
        QVERIFY(!ok);
    }

    void integerDivisionRoundsOperandsFirst()
    {
        QCOMPARE(vb::intDiv(7, 2), 3);
        QCOMPARE(vb::intDiv(7.5, 2), 4);   // 7.5 -> 8
        QCOMPARE(vb::intDiv(-7, 2), -3);
        QCOMPARE(vb::intDiv(5, 2.5), 2);   // 2.5 -> 2
        bool ok = true;
        vb::intDiv(1, 0, &ok);
        QVERIFY(!ok);
    }

    void fixedLengthPadsAndTruncates()
    {
        QCOMPARE(vb::fixedLength("ab", 4), QStringLiteral("ab  "));
        QCOMPARE(vb::fixedLength("abcdef", 4), QStringLiteral("abcd"));
    }

    void ansiRoundTrip()
    {
        QByteArray all;
        for (int code = 0; code < 256; ++code)
            all.append(char(code));
        QCOMPARE(vb::toAnsi(vb::fromAnsi(all)), all);
        QCOMPARE(vb::fromAnsi(QByteArray("\x80")), QStringLiteral("\u20AC"));
        QCOMPARE(vb::toAnsi(QStringLiteral("\u0141")), QByteArray("?")); // not in Windows-1252
    }

    void trimOnlySpaces()
    {
        QCOMPARE(vb::trim(QStringLiteral("  a\t ")), QStringLiteral("a\t"));
    }
};

QTEST_APPLESS_MAIN(TestVbCompat)
#include "tst_vbcompat.moc"
