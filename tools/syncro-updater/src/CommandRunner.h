#pragma once

#include <QObject>
#include <QPointer>
#include <QProcess>
#include <QString>
#include <QStringList>
#include <QVector>

#include <functional>

struct StepResult
{
    int exitCode = 0;
    QString standardOutput;
    QString standardError;
};

// One unit of work: either an external program or an in-process check.
struct Step
{
    QString title;
    QString program;
    std::function<QStringList()> arguments;
    // Returns an error message to abort the sequence, or an empty string to continue.
    std::function<QString(const StepResult &)> onFinished;
    std::function<QString()> action;
    bool cancellable = true;
    bool echoOutput = true;
};

class CommandRunner : public QObject
{
    Q_OBJECT

public:
    explicit CommandRunner(QObject *parent = nullptr);

    void setWorkingDirectory(const QString &directory);
    bool isRunning() const;
    bool canCancel() const;

    void run(const QVector<Step> &steps);
    void cancel();

signals:
    void stepStarted(int index, int count, const QString &title);
    void outputLine(const QString &line, bool isError);
    void cancellableChanged(bool cancellable);
    void finished(bool success, const QString &message);

private:
    void startNext();
    void finishStep(const StepResult &result);
    void finishSequence(bool success, const QString &message);
    void drain(QProcess::ProcessChannel channel, bool flush);

    QString m_workingDirectory;
    QVector<Step> m_steps;
    int m_index = -1;
    bool m_running = false;
    bool m_cancelRequested = false;
    QPointer<QProcess> m_process;
    QString m_stdout;
    QString m_stderr;
    QString m_pendingStdout;
    QString m_pendingStderr;
};
