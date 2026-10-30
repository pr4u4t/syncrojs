#pragma once

#include "formmodel.h"

// Pwd_frm.frm: the two passwords and changing them. VB's F2-F7 start Windows programs from the service
// panel (Explorer, cmd, Task Manager, pcAnywhere, VB5, SysWin); here they only report that they are not available.
class PwdModel : public FormModel
{
    Q_OBJECT

public:
    using FormModel::FormModel;

    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

    // code and decode: each character shifted by its position.
    static QString code(const QString &text);
    static QString decodeShift(const QString &text);

private:
    enum ChangeState { InOldPwd, InNewPwd, ConfNewPwd, ConfNewPwdLiv1, InOldPwdLiv1, InNewPwdLiv1 };

    QString decode(const QString &stored, bool mainPassword);
    void enterPassword();
    void enterChange();
    void showPanels(bool settings, bool password, bool change);
    void startChange(ChangeState state);
    void askChange(const QString &message, ChangeState next);
    void setMessage(const QString &text, quint32 color);

    QString m_pwd;
    QString m_chgPwd;
    QString m_newPwd;
    QString m_newPwdLiv1;
    ChangeState m_state = InOldPwd;
    int m_focusCount = 0;
};
