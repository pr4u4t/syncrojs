#pragma once

#include "fixedrecord.h"

#include <QElapsedTimer>
#include <QObject>
#include <QString>
#include <QVector>

#include <array>

class ClasseOrdine;
class FinsClient;

// Port of stato.cls (ClasseStato): machine state read from DM3600-3699, events raised to the forms,
// production totals of order, shift and day (tempi).
class ClasseStato : public QObject
{
    Q_OBJECT

public:
    enum StaReset { rip, e_stop, f_stop, n_stop, ness_reset };
    enum StaMac { mac_in_man, mac_in_sim, mac_in_lap, ness_mac };
    enum StaMod { manuale, automatico };
    enum StaOrd { ord_nok = 1, ord_ok, ord_ass };
    enum TipOrd { CORR = 1, SUCC };
    enum StaRun { co_in_cor = 1, co_com, pre, pro_cam, in_mar, pro_mar, fer };

    // Tip_sta_PLC: one '0'/'1' character per bit, DM3600 bit 0 first.
    enum StaPlcBit {
        MAC_RIPRISTINATA, MAC_E_STOP, MAC_F_STOP, MAC_N_STOP, MAC_PRONTA_MARCIA, MAC_PRONTA_AL_CAMBIO, MAC_IN_MARCIA,
        MAC_IN_PREALLARME, MAC_CAM_ORD_IN_CORSO, LIV1_IN_LAV, LIV2_IN_LAV, LIV3_IN_LAV, MAC_PRE_CAR_LIV1, MAC_PRE_CAR_LIV2,
        MAC_PRE_CAR_LIV3, MAC_CAM_ORD_COMPLETATO,
        MAC_IN_MANUALE, MAC_IN_MANUTENZIONE, MAC_IN_SIMULAZIONE, MAC_IN_LAPPATURA, MAC_JOG_NASTRI_IN_COR,
        SCH_TAG_LIV_1_PRONTA, SCH_TAG_LIV_2_PRONTA, SCH_TAG_LIV_3_PRONTA,
    };

    static constexpr int POS_GRU_HAR = 1;
    static constexpr int POS_GRU_QE_MST = 2;
    static constexpr int POS_GRU_QE_SLV = 3;
    static constexpr int POS_GRU_QE_EST = 4;
    static constexpr int POS_GRU_BM = 5;
    static constexpr int POS_GRU_CON = 6;
    static constexpr int POS_GRU_MAL = 7;

    // Globals of the VB program not ported yet (sta_tag_ute).
    struct Context {
        bool macInManualeUte = false;
    };

    struct TipOrdPlc {
        qint32 lunTag = 0;
        double numFogBuo = 0;
        qint32 numFogErr = 0;
        qint32 errTag = 0;
    };

    struct TipOrdPro {
        qint32 numFogBuo = 0;
        qint32 numFogErr = 0;
        qint32 numFogSca = 0;
        qint32 numMilSca = 0;
    };

    // Tip_sta_mac
    struct Stato {
        QString staPlc = QString(32, QChar(0));
        QString codOrdCor = QString(4, QChar(0));
        std::array<TipOrdPlc, 4> ordPlc{};
        QString codOrdSuc = QString(4, QChar(0));
        std::array<qint32, 4> lunTagSuc{};
        std::array<qint32, 4> nextCutLenFromExternal{};
        QString codOrdPro = QString(4, QChar(0));
        std::array<TipOrdPro, 4> ordPro{};
        std::array<qint32, 4> velLin{};
        qint32 velAnaSanIng = 0;
        qint32 velSanIng = 0;
        std::array<qint32, 4> velSanUsc{};
        QString gruAll = QString(16, QChar(0));
        QString allAtt = QString(336, QChar(0));
        std::array<bool, 8> gruAllAtt{};
        QString ris3500 = QString(16, QChar(0));
        QString ris3501 = QString(16, QChar(0));
        bool shiLis = false;
        bool comOK = false;
        bool spdeOK = true;
        int staRun = 0;
    };

    explicit ClasseStato(QObject *parent = nullptr);

    Context &context() { return m_ctx; }
    const Stato &stato() const { return m_stato; }

    bool bit(StaPlcBit b, int v) const;   // MAC_xxx(v) properties: Val(bit) = v
    bool comOK() const { return m_stato.comOK; }
    void setComOK(bool v) { m_stato.comOK = v; }
    bool spdeOK() const { return m_stato.spdeOK; }
    void setSpdeOK(bool v) { m_stato.spdeOK = v; }
    int staRun() const { return m_stato.staRun; }
    QString gruAll() const { return m_stato.gruAll; }
    bool gruAllAtt(int posGru) const;
    void setGruAllAtt(int posGru, bool v);
    void chgLin() { m_camLin = true; }

    void aggiorna(FinsClient &fins);
    void chkAllSftw();
    void aggiornaSpde();

    void rea_dat_ord_pro(bool fineProd);
    void ins_dat_pro(ClasseOrdine &tmpOrd);
    void tempi(bool resOrd, bool resTur, bool resGio);
    QByteArray get_pro_ese() const { return m_totEse.raw(); }
    QByteArray get_pro_tg() const { return m_totTg.raw(); }
    void set_pro_ese(const QByteArray &s) { m_totEse.setRaw(s); }
    void set_pro_tg(const QByteArray &s) { m_totTg.setRaw(s); }
    void lunghezza(qint32 met, qint32 larMm);
    void chg_tur(int newTur);
    void chg_ord();

    qint32 num_tag_eff(int i) const { return qint32(ordPlcAt(i).numFogBuo); }
    qint32 lun_tag_cor(int i) const { return ordPlcAt(i).lunTag; }
    qint32 err_tag(int i) const { return ordPlcAt(i).errTag; }
    qint32 vel_lin(int i) const { return m_stato.velLin.at(std::size_t(i)); }
    QString cod_ord_cor() const { return m_stato.codOrdCor; }
    QString cod_ord_suc() const { return m_stato.codOrdSuc; }

    // Exposed for tests: ass_stato on a DM3600-3699 image.
    void assStato(const QVector<qint32> &dm);

signals:
    void run(int run);
    void reset(int staRes);
    void mac(int staMac);
    void modo(int staMod);
    void ord(int eve, int ord);
    void liv();
    void spde(bool ok);
    void vel(qint32 v);
    void gruall(const QString &v);
    void sezall(const QString &allPlc);
    void velmax(int v);
    void err(int liv, const QString &err);
    void num(int liv, const QString &mis);
    void numerr(int liv, const QString &mis);

private:
    static int firstOne(const QString &bits);
    QString runString(const Stato &s) const;
    QString resetString(const Stato &s) const;
    QString macString(const Stato &s) const;
    const TipOrdPlc &ordPlcAt(int i) const;
    static const TipOrdPlc &ordPlcOf(const Stato &s, int i);
    void aggiornaBody(FinsClient &fins);
    qint32 agg_med(const QString &group) const;

    Context m_ctx;
    Stato m_stato;
    Stato m_oldStato;
    bool m_camLin = false;
    FinsClient *m_fins = nullptr;
    FixedRecord m_totEse;
    FixedRecord m_totTg;

    // Static locals of aggiorna / ass_stato / aggiorna_spde / tempi / lunghezza.
    int m_ini = 0;
    int m_oldVelMaxTag = 0;
    int m_oldNumOrd = 0;
    bool m_oldAbiMisAng = false;
    std::array<qint32, 4> m_oldLimInf{};
    std::array<qint32, 4> m_oldLimSup{};
    QString m_oldIdCor;
    bool m_oldDisCur = false;
    QString m_oldPlcLogCur;
    int m_oldVel = 0;
    int m_iniSpde = 0;
    bool m_oldSpde = false;
    QElapsedTimer m_tickCount;
    qint64 m_lasTic = 0;
    qint64 m_lasTicFerOrd = 0;
    qint64 m_lasTicFerGio = 0;
    qint64 m_lasTicFerTur = 0;
    int m_iniLunghezza = 0;
    qint32 m_metCur = 0;
    qint32 m_resq = 0;
};
