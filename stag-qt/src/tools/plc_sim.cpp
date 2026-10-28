#include "finssimulator.h"

#include <QCommandLineParser>
#include <QCoreApplication>
#include <QDateTime>
#include <QFile>
#include <QRegularExpression>
#include <QTextStream>
#include <QTimer>

namespace {

struct ReplayStep {
    QDateTime time;
    int address;
    QVector<quint16> values;
};

// Rebuilds DM3600/3601 and DM3650 from the "Stato Macchina" and "Speed" lines STAG wrote to Plc.log.
QVector<ReplayStep> loadReplay(const QString &path)
{
    QVector<ReplayStep> steps;
    QFile file(path);
    if (!file.open(QIODevice::ReadOnly))
        return steps;
    static const QRegularExpression line(QStringLiteral(R"(^(\d\d/\d\d/\d\d) \| (\d\d\.\d\d\.\d\d) \| (?:Stato Macchina: ([01]{32})|Speed: (-?\d+)))"));
    while (!file.atEnd()) {
        const QString text = QString::fromLatin1(file.readLine()).trimmed();
        const QRegularExpressionMatch m = line.match(text);
        if (!m.hasMatch())
            continue;
        ReplayStep step;
        step.time = QDateTime::fromString(m.captured(1) + u' ' + m.captured(2), QStringLiteral("dd/MM/yy hh.mm.ss")).addYears(100);
        if (!m.captured(3).isEmpty()) {
            const QString bits = m.captured(3);
            quint16 words[2] = {0, 0};
            for (int i = 0; i < 32; ++i) {
                if (bits.at(i) == u'1')
                    words[i / 16] |= quint16(1u << (i % 16));
            }
            step.address = 3600;
            step.values = {words[0], words[1]};
        } else {
            step.address = 3650;
            step.values = {quint16(m.captured(4).toInt())};
        }
        steps.append(step);
    }
    return steps;
}

} // namespace

int main(int argc, char *argv[])
{
    QCoreApplication app(argc, argv);
    QCommandLineParser parser;
    parser.setApplicationDescription(QStringLiteral("Omron FINS/TCP PLC simulator for STAG"));
    parser.addHelpOption();
    QCommandLineOption portOption(QStringLiteral("port"), QStringLiteral("TCP port"), QStringLiteral("port"), QStringLiteral("9600"));
    QCommandLineOption nodeOption(QStringLiteral("node"), QStringLiteral("PLC FINS node"), QStringLiteral("node"), QStringLiteral("30"));
    QCommandLineOption replayOption(QStringLiteral("replay"), QStringLiteral("Replay machine state and speed from a STAG Plc.log"), QStringLiteral("file"));
    QCommandLineOption speedOption(QStringLiteral("speed"), QStringLiteral("Replay speed factor"), QStringLiteral("factor"), QStringLiteral("10"));
    parser.addOptions({portOption, nodeOption, replayOption, speedOption});
    parser.process(app);

    FinsSimulator plc(quint8(parser.value(nodeOption).toUInt()));
    QTextStream out(stdout);
    QObject::connect(&plc, &FinsSimulator::dmWritten, [&out](int address, const QVector<quint16> &values) {
        QString hex;
        for (quint16 v : values)
            hex += QStringLiteral("%1").arg(v, 4, 16, QLatin1Char('0')).toUpper();
        out << "DM" << address << "-DM" << address + values.size() - 1 << " = '" << hex << "'" << Qt::endl;
    });
    if (!plc.listen(QHostAddress::Any, quint16(parser.value(portOption).toUInt()))) {
        QTextStream(stderr) << "Cannot listen on port " << parser.value(portOption) << Qt::endl;
        return 1;
    }
    out << "PLC simulator listening on port " << plc.port() << Qt::endl;

    const QVector<ReplayStep> steps = parser.isSet(replayOption) ? loadReplay(parser.value(replayOption)) : QVector<ReplayStep>();
    const double speed = qMax(0.1, parser.value(speedOption).toDouble());
    int next = 0;
    QTimer replay;
    replay.setSingleShot(true);
    QObject::connect(&replay, &QTimer::timeout, [&] {
        const ReplayStep &step = steps[next];
        plc.setDm(step.address, step.values);
        out << step.time.toString(QStringLiteral("dd/MM/yy hh:mm:ss")) << " DM" << step.address << " <- " << step.values[0]
            << (step.values.size() > 1 ? QStringLiteral(", %1").arg(step.values[1]) : QString()) << Qt::endl;
        const int following = (next + 1) % steps.size();
        const qint64 gap = following == 0 ? 2000 : steps[next].time.msecsTo(steps[following].time);
        next = following;
        replay.start(int(qBound(qint64(50), qint64(gap / speed), qint64(3000))));
    });
    if (!steps.isEmpty()) {
        out << "Replaying " << steps.size() << " steps from " << parser.value(replayOption) << Qt::endl;
        replay.start(0);
    }
    return app.exec();
}
