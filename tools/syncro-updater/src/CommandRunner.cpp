#include "CommandRunner.h"

#include <QProcessEnvironment>
#include <QStandardPaths>
#include <QTimer>

CommandRunner::CommandRunner(QObject *parent)
    : QObject(parent)
{
}

void CommandRunner::setWorkingDirectory(const QString &directory)
{
    m_workingDirectory = directory;
}

bool CommandRunner::isRunning() const
{
    return m_running;
}

bool CommandRunner::canCancel() const
{
    return m_running && m_index >= 0 && m_index < m_steps.size() && m_steps.at(m_index).cancellable;
}

void CommandRunner::run(const QVector<Step> &steps)
{
    if (m_running)
        return;

    m_steps = steps;
    m_index = -1;
    m_running = true;
    m_cancelRequested = false;
    QTimer::singleShot(0, this, &CommandRunner::startNext);
}

void CommandRunner::cancel()
{
    if (!canCancel())
        return;

    m_cancelRequested = true;
    if (m_process && m_process->state() != QProcess::NotRunning) {
        m_process->terminate();
        QTimer::singleShot(3000, m_process.data(), [process = m_process]() {
            if (process && process->state() != QProcess::NotRunning)
                process->kill();
        });
    }
}

void CommandRunner::startNext()
{
    if (m_cancelRequested) {
        finishSequence(false, tr("Operacja anulowana."));
        return;
    }

    ++m_index;
    if (m_index >= m_steps.size()) {
        finishSequence(true, QString());
        return;
    }

    const Step &step = m_steps.at(m_index);
    emit stepStarted(m_index, m_steps.size(), step.title);
    emit cancellableChanged(step.cancellable);

    if (step.action) {
        const QString error = step.action();
        if (!error.isEmpty()) {
            finishSequence(false, error);
            return;
        }
        QTimer::singleShot(0, this, &CommandRunner::startNext);
        return;
    }

    const QString executable = QStandardPaths::findExecutable(step.program);
    if (executable.isEmpty()) {
        finishSequence(false, tr("Nie znaleziono programu „%1”. Zainstaluj go i spróbuj ponownie.").arg(step.program));
        return;
    }

    const QStringList arguments = step.arguments ? step.arguments() : QStringList();
    if (step.echoOutput)
        emit outputLine(QStringLiteral("$ %1 %2").arg(step.program, arguments.join(QLatin1Char(' '))), false);

    m_stdout.clear();
    m_stderr.clear();
    m_pendingStdout.clear();
    m_pendingStderr.clear();

    auto *process = new QProcess(this);
    m_process = process;
    process->setWorkingDirectory(m_workingDirectory);

    QProcessEnvironment environment = QProcessEnvironment::systemEnvironment();
    // A hidden credential prompt would hang the GUI forever.
    environment.insert(QStringLiteral("GIT_TERMINAL_PROMPT"), QStringLiteral("0"));
    environment.insert(QStringLiteral("GIT_ASKPASS"), QStringLiteral("/bin/false"));
    environment.insert(QStringLiteral("SSH_ASKPASS"), QStringLiteral("/bin/false"));
    environment.insert(QStringLiteral("LC_MESSAGES"), QStringLiteral("C"));
    process->setProcessEnvironment(environment);

    connect(process, &QProcess::readyReadStandardOutput, this, [this]() { drain(QProcess::StandardOutput, false); });
    connect(process, &QProcess::readyReadStandardError, this, [this]() { drain(QProcess::StandardError, false); });
    connect(process, &QProcess::errorOccurred, this, [this, process](QProcess::ProcessError error) {
        if (error != QProcess::FailedToStart)
            return;
        process->deleteLater();
        finishSequence(false, tr("Nie udało się uruchomić „%1”: %2").arg(m_steps.at(m_index).program, process->errorString()));
    });
    connect(process, &QProcess::finished, this, [this, process](int exitCode, QProcess::ExitStatus exitStatus) {
        drain(QProcess::StandardOutput, true);
        drain(QProcess::StandardError, true);
        StepResult result;
        result.exitCode = exitStatus == QProcess::NormalExit ? exitCode : -1;
        result.standardOutput = m_stdout;
        result.standardError = m_stderr;
        process->deleteLater();
        finishStep(result);
    });

    process->start(executable, arguments);
}

void CommandRunner::drain(QProcess::ProcessChannel channel, bool flush)
{
    if (!m_process)
        return;

    const bool isError = channel == QProcess::StandardError;
    const QByteArray data = isError ? m_process->readAllStandardError() : m_process->readAllStandardOutput();
    const QString text = QString::fromLocal8Bit(data);
    (isError ? m_stderr : m_stdout).append(text);

    QString &pending = isError ? m_pendingStderr : m_pendingStdout;
    pending.append(text);
    // Progress output from git/docker uses carriage returns to redraw a line.
    pending.replace(QStringLiteral("\r\n"), QStringLiteral("\n"));
    pending.replace(QLatin1Char('\r'), QLatin1Char('\n'));

    qsizetype newline;
    while ((newline = pending.indexOf(QLatin1Char('\n'))) >= 0) {
        const QString line = pending.left(newline);
        pending.remove(0, newline + 1);
        if (m_steps.at(m_index).echoOutput && !line.trimmed().isEmpty())
            emit outputLine(line, isError);
    }
    if (flush && !pending.trimmed().isEmpty() && m_steps.at(m_index).echoOutput)
        emit outputLine(pending, isError);
    if (flush)
        pending.clear();
}

void CommandRunner::finishStep(const StepResult &result)
{
    if (m_cancelRequested) {
        finishSequence(false, tr("Operacja anulowana."));
        return;
    }

    const Step &step = m_steps.at(m_index);
    QString error;
    if (step.onFinished) {
        error = step.onFinished(result);
    } else if (result.exitCode != 0) {
        const QString detail = result.standardError.trimmed();
        error = tr("Krok „%1” zakończył się błędem (kod %2).").arg(step.title).arg(result.exitCode);
        if (!detail.isEmpty())
            error += QLatin1Char('\n') + detail.section(QLatin1Char('\n'), -3);
    }

    if (!error.isEmpty()) {
        finishSequence(false, error);
        return;
    }
    QTimer::singleShot(0, this, &CommandRunner::startNext);
}

void CommandRunner::finishSequence(bool success, const QString &message)
{
    if (!m_running)
        return;
    m_running = false;
    m_process = nullptr;
    emit cancellableChanged(false);
    emit finished(success, message);
}
