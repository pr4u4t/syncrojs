#include "stabarmodel.h"

#include "classestato.h"
#include "messages.h"
#include "parmac.h"
#include "vbcompat.h"

#include <QVector>

QString StaBarModel::vbColor(quint32 bgr)
{
    const quint32 r = bgr & 0xFF, g = (bgr >> 8) & 0xFF, b = (bgr >> 16) & 0xFF;
    return QStringLiteral("#%1%2%3").arg(r, 2, 16, QLatin1Char('0')).arg(g, 2, 16, QLatin1Char('0')).arg(b, 2, 16, QLatin1Char('0'));
}

StaBarModel::StaBarModel(QObject *parent)
    : QObject(parent)
{
    const QString green = vbColor(COL_DARK_Green);
    m_txtStaRunFore = m_txtStaResFore = m_txtModFore = vbColor(vbBlack);
    m_txtStaRunBack = m_labRunBack = vbColor(vbWhite);
    m_frmStaRunBack = m_frmStaResBack = m_frmModBack = m_frmVelBack = m_frmComBack = green;
    m_shpAllMal = m_shpAllHar = m_shpAllQe = m_shpAllBm = m_shpAllCon = vbColor(LIGHT_Green);
    m_linNoComSpdeColor = vbColor(vbBlack);
    m_ledPlcRx = m_ledPlcTx = vbColor(vbRed);
}

void StaBarModel::eveTagRun(int run)
{
    switch (run) {
    case ClasseStato::co_in_cor:
    case ClasseStato::co_com:
        m_txtStaRun = msg(run == ClasseStato::co_in_cor ? QStringLiteral("msg_co_in_cor") : QStringLiteral("msg_co_com"));
        m_txtStaRunFore = vbColor(DARK_BLUE);
        m_txtStaRunBack = vbColor(vbWhite);
        m_frmStaRunBack = vbColor(DARK_BLUE);
        m_labRunBack = vbColor(vbWhite);
        m_lampRun = false;
        break;
    case ClasseStato::pre:
        m_txtStaRun = msg(QStringLiteral("msg_pre"));
        m_txtStaRunFore = vbColor(vbWhite);
        m_txtStaRunBack = vbColor(COL_DARK_Green);
        m_labRunBack = vbColor(COL_DARK_Green);
        m_frmStaRunBack = vbColor(COL_DARK_Green);
        m_lampRun = true;
        break;
    case ClasseStato::pro_cam:
    case ClasseStato::in_mar:
    case ClasseStato::pro_mar:
        m_txtStaRun = msg(run == ClasseStato::pro_cam ? QStringLiteral("msg_pro_cam")
                          : run == ClasseStato::in_mar ? QStringLiteral("msg_in_mar") : QStringLiteral("msg_pro_mar"));
        m_txtStaRunFore = vbColor(vbBlack);
        m_txtStaRunBack = vbColor(vbWhite);
        m_frmStaRunBack = vbColor(COL_DARK_Green);
        m_labRunBack = vbColor(vbWhite);
        m_lampRun = false;
        break;
    case ClasseStato::fer:
        m_txtStaRun = msg(QStringLiteral("msg_fer"));
        m_txtStaRunFore = vbColor(COL_ALL_Red);
        m_txtStaRunBack = vbColor(vbWhite);
        m_frmStaRunBack = vbColor(COL_ALL_Red);
        m_labRunBack = vbColor(vbWhite);
        m_lampRun = false;
        break;
    default:
        return;
    }
    emit changed();
}

void StaBarModel::eveTagReset(int staRes)
{
    switch (staRes) {
    case ClasseStato::e_stop:
        m_txtStaRes = QStringLiteral("Emergency Stop");
        m_txtStaResFore = vbColor(COL_ALL_Red);
        m_frmStaResBack = vbColor(COL_ALL_Red);
        break;
    case ClasseStato::f_stop:
        // The VB code sets frm_sta_res.ForeColor here, so the frame keeps its colour.
        m_txtStaRes = QStringLiteral("Fast Stop");
        m_txtStaResFore = vbColor(COL_ALL_Red);
        break;
    case ClasseStato::n_stop:
        m_txtStaRes = QStringLiteral("Normal Stop");
        m_txtStaResFore = vbColor(vbBlack);
        m_frmStaResBack = vbColor(COL_DARK_Green);
        break;
    case ClasseStato::rip:
        m_txtStaRes = msg(QStringLiteral("msg_rip"));
        m_txtStaResFore = vbColor(vbBlack);
        m_frmStaResBack = vbColor(COL_DARK_Green);
        break;
    default:
        m_txtStaRes.clear();
        m_txtStaResFore = vbColor(vbBlack);
        m_frmStaResBack = vbColor(COL_DARK_Green);
        break;
    }
    emit changed();
}

void StaBarModel::eveTagMac(int mac)
{
    switch (mac) {
    case ClasseStato::mac_in_lap: m_labStaMac = QStringLiteral("L"); break;
    case ClasseStato::mac_in_man: m_labStaMac = QStringLiteral("M"); break;
    case ClasseStato::mac_in_sim: m_labStaMac = QStringLiteral("S"); break;
    default: m_labStaMac.clear(); break;
    }
    emit changed();
}

void StaBarModel::eveTagModo(int staMod)
{
    if (staMod == ClasseStato::automatico) {
        m_txtMod = msg(QStringLiteral("msg_mac_in_aut"));
        m_txtModFore = vbColor(DARK_BLUE);
        m_frmModBack = vbColor(COL_DARK_Green);
        if (m_ctx.spdeOK)
            m_linNoComSpde = false;
    } else {
        m_txtMod = msg(QStringLiteral("msg_mac_in_man"));
        m_txtModFore = vbColor(COL_MODO);
        m_frmModBack = vbColor(COL_MODO);
        if (m_ctx.spdeOK) {
            m_linNoComSpde = true;
            m_linNoComSpdeColor = vbColor(RED_GRU_ALL);
        }
    }
    emit changed();
}

void StaBarModel::eveTagSpde(bool ok)
{
    m_linNoComSpde = !ok || m_ctx.macInManuale;
    m_linNoComSpdeColor = vbColor(m_ctx.macInManuale ? RED_GRU_ALL : vbBlack);
    m_frmComBack = vbColor(m_linNoComSpde && m_linNoComPlc ? COL_ALL_Red : COL_DARK_Green);
    emit changed();
}

void StaBarModel::eveTagVel(qint32 v)
{
    // abi_mis_ang (feet) conversion is ported together with con_met_to_fet.
    m_txtVel = QString::number(v);
    m_frmVelBack = vbColor(v > vb::val(m_txtVelMax) ? COL_ALL_Red : COL_DARK_Green);
    emit changed();
}

void StaBarModel::eveTagVelmax(int v)
{
    m_txtVelMax = QString::number(v);
    m_frmVelBack = vbColor(vb::val(m_txtVel) > v ? COL_ALL_Red : COL_DARK_Green);
    emit changed();
}

QVector<bool> StaBarModel::eveTagGruall(const QString &v)
{
    qint64 bits = 0;
    for (const QChar c : v)
        bits = qint64(vb::val(QString(c))) + 2 * bits;
    const auto on = [bits](int pos) { return (bits & (qint64(1) << pos)) != 0; };

    QVector<bool> att(8, false);
    for (int pos = ClasseStato::POS_GRU_HAR; pos <= ClasseStato::POS_GRU_MAL; ++pos)
        att[pos] = on(pos);
    const auto lamp = [](bool alarm) { return vbColor(alarm ? RED_GRU_ALL : LIGHT_Green); };
    m_shpAllMal = lamp(att[ClasseStato::POS_GRU_MAL]);
    m_shpAllHar = lamp(att[ClasseStato::POS_GRU_HAR]);
    m_shpAllQe = lamp(att[ClasseStato::POS_GRU_QE_MST] || att[ClasseStato::POS_GRU_QE_SLV] || att[ClasseStato::POS_GRU_QE_EST]);
    m_shpAllBm = lamp(att[ClasseStato::POS_GRU_BM]);
    m_shpAllCon = lamp(att[ClasseStato::POS_GRU_CON]);
    emit changed();
    return att;
}

void StaBarModel::eveTagLiv()
{
    const int numLivTag = int(stag::par_mac.car_tec.num_liv_tag.val());
    m_labLiv3 = msg(QStringLiteral("liv_alt"));
    m_labLiv2 = numLivTag == 3 ? msg(QStringLiteral("liv_med")) : QString();
    m_labLiv1 = numLivTag >= 2 ? msg(QStringLiteral("liv_bas")) : QString();
    emit changed();
}

void StaBarModel::messaggio(const QString &text, bool isAll, bool stay)
{
    if (text == m_labMsg)
        return;
    m_labMsgFore = vbColor(isAll ? COL_ALL_Red : COL_DARK_Green);
    m_labMsg = text;
    m_labMsgStay = stay;
    emit changed();
}

void StaBarModel::setMetMan(const QString &temCo, const QString &metMan, bool negative)
{
    m_txtTemCo = temCo;
    m_txtMetMan = metMan;
    m_txtMetManFore = vbColor(negative ? COL_ALL_Red : vbBlack);
    m_frmMetManBack = vbColor(negative ? COL_ALL_Red : COL_DARK_Green);
    emit changed();
}

void StaBarModel::setLinNoComPlc(bool visible)
{
    m_linNoComPlc = visible;
    emit changed();
}

void StaBarModel::setFrmComBack(quint32 color)
{
    m_frmComBack = vbColor(color);
    emit changed();
}

void StaBarModel::setLedPlc(quint32 rx, quint32 tx)
{
    m_ledPlcRx = vbColor(rx);
    m_ledPlcTx = vbColor(tx);
    emit changed();
}
