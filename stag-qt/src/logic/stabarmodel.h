#pragma once

#include <QObject>
#include <QString>

// sta_bar.frm event handlers (eve_tag_*) turned into bindable properties for StatusBar.qml.
class StaBarModel : public QObject
{
    Q_OBJECT
    Q_PROPERTY(QString txtStaRun MEMBER m_txtStaRun NOTIFY changed)
    Q_PROPERTY(QString txtStaRunFore MEMBER m_txtStaRunFore NOTIFY changed)
    Q_PROPERTY(QString txtStaRunBack MEMBER m_txtStaRunBack NOTIFY changed)
    Q_PROPERTY(QString frmStaRunBack MEMBER m_frmStaRunBack NOTIFY changed)
    Q_PROPERTY(QString labRunBack MEMBER m_labRunBack NOTIFY changed)
    Q_PROPERTY(bool lampRun MEMBER m_lampRun NOTIFY changed)
    Q_PROPERTY(QString txtStaRes MEMBER m_txtStaRes NOTIFY changed)
    Q_PROPERTY(QString txtStaResFore MEMBER m_txtStaResFore NOTIFY changed)
    Q_PROPERTY(QString frmStaResBack MEMBER m_frmStaResBack NOTIFY changed)
    Q_PROPERTY(QString labStaMac MEMBER m_labStaMac NOTIFY changed)
    Q_PROPERTY(QString txtMod MEMBER m_txtMod NOTIFY changed)
    Q_PROPERTY(QString txtModFore MEMBER m_txtModFore NOTIFY changed)
    Q_PROPERTY(QString frmModBack MEMBER m_frmModBack NOTIFY changed)
    Q_PROPERTY(QString txtVel MEMBER m_txtVel NOTIFY changed)
    Q_PROPERTY(QString txtVelMax MEMBER m_txtVelMax NOTIFY changed)
    Q_PROPERTY(QString frmVelBack MEMBER m_frmVelBack NOTIFY changed)
    Q_PROPERTY(QString shpAllMal MEMBER m_shpAllMal NOTIFY changed)
    Q_PROPERTY(QString shpAllHar MEMBER m_shpAllHar NOTIFY changed)
    Q_PROPERTY(QString shpAllQe MEMBER m_shpAllQe NOTIFY changed)
    Q_PROPERTY(QString shpAllBm MEMBER m_shpAllBm NOTIFY changed)
    Q_PROPERTY(QString shpAllCon MEMBER m_shpAllCon NOTIFY changed)
    Q_PROPERTY(bool linNoComPlc MEMBER m_linNoComPlc NOTIFY changed)
    Q_PROPERTY(bool linNoComSpde MEMBER m_linNoComSpde NOTIFY changed)
    Q_PROPERTY(QString linNoComSpdeColor MEMBER m_linNoComSpdeColor NOTIFY changed)
    Q_PROPERTY(QString frmComBack MEMBER m_frmComBack NOTIFY changed)
    Q_PROPERTY(QString ledPlcRx MEMBER m_ledPlcRx NOTIFY changed)
    Q_PROPERTY(QString ledPlcTx MEMBER m_ledPlcTx NOTIFY changed)
    Q_PROPERTY(QString labLiv1 MEMBER m_labLiv1 NOTIFY changed)
    Q_PROPERTY(QString labLiv2 MEMBER m_labLiv2 NOTIFY changed)
    Q_PROPERTY(QString labLiv3 MEMBER m_labLiv3 NOTIFY changed)
    Q_PROPERTY(QString labMsg MEMBER m_labMsg NOTIFY changed)
    Q_PROPERTY(QString labMsgFore MEMBER m_labMsgFore NOTIFY changed)
    Q_PROPERTY(QString txtTemCo MEMBER m_txtTemCo NOTIFY changed)
    Q_PROPERTY(QString txtMetMan MEMBER m_txtMetMan NOTIFY changed)
    Q_PROPERTY(QString txtMetManFore MEMBER m_txtMetManFore NOTIFY changed)
    Q_PROPERTY(QString frmMetManBack MEMBER m_frmMetManBack NOTIFY changed)

public:
    // Var_stag.bas colours converted from VB &HBBGGRR.
    static QString vbColor(quint32 bgr);
    static const quint32 vbBlack = 0x000000, vbWhite = 0xFFFFFF, vbRed = 0x0000FF, vbGreen = 0x00FF00;
    static const quint32 COL_MODO = 0x000080, RED_GRU_ALL = 0x0000FF, LIGHT_Green = 0x00FF00, DARK_BLUE = 0xC00000;
    static const quint32 COL_ALL_Red = 0x0000C0, COL_DARK_Green = 0x008000;

    explicit StaBarModel(QObject *parent = nullptr);

    // Values sta_bar reads back from sta_tag / par_mac.
    struct Context {
        bool spdeOK = true;
        bool macInManuale = false;
    };
    Context &context() { return m_ctx; }

    void eveTagRun(int run);
    void eveTagReset(int staRes);
    void eveTagMac(int mac);
    void eveTagModo(int staMod);
    void eveTagSpde(bool ok);
    void eveTagVel(qint32 v);
    void eveTagVelmax(int v);
    // Returns gru_all_att(1..7) for sta_tag.
    QVector<bool> eveTagGruall(const QString &v);
    void eveTagLiv();

    void setLinNoComPlc(bool visible);
    void setFrmComBack(quint32 color);
    void setLedPlc(quint32 rx, quint32 tx);
    QString ledPlcRx() const { return m_ledPlcRx; }
    // uti_stag.messaggio (sta_bar.lab_msg); stay is kept in lab_msg.Tag.
    void messaggio(const QString &text, bool isAll, bool stay);
    bool labMsgStay() const { return m_labMsgStay; }
    QString txtVel() const { return m_txtVel; }
    void setMetMan(const QString &temCo, const QString &metMan, bool negative);

signals:
    void changed();

private:
    Context m_ctx;
    QString m_txtStaRun, m_txtStaRunFore, m_txtStaRunBack, m_frmStaRunBack, m_labRunBack;
    bool m_lampRun = false;
    QString m_txtStaRes, m_txtStaResFore, m_frmStaResBack;
    QString m_labStaMac;
    QString m_txtMod, m_txtModFore, m_frmModBack;
    QString m_txtVel, m_txtVelMax, m_frmVelBack;
    QString m_shpAllMal, m_shpAllHar, m_shpAllQe, m_shpAllBm, m_shpAllCon;
    bool m_linNoComPlc = true;
    bool m_linNoComSpde = false;
    QString m_linNoComSpdeColor;
    QString m_frmComBack;
    QString m_ledPlcRx, m_ledPlcTx;
    QString m_labLiv1, m_labLiv2, m_labLiv3;
    QString m_labMsg, m_labMsgFore;
    bool m_labMsgStay = false;
    QString m_txtTemCo, m_txtMetMan, m_txtMetManFore, m_frmMetManBack;
};
