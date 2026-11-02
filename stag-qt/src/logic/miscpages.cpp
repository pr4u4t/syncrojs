#include "miscpages.h"
#include "sqlcheck.h"

#include "classestato.h"
#include "finsclient.h"
#include "fixedrecord.h"
#include "inistore.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "staglog.h"
#include "stagrecords.h"
#include "uihooks.h"
#include "unimisang.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QDateTime>
#include <QSqlQuery>
#include <QSqlRecord>

#ifdef Q_OS_WIN
#include <windows.h>
#else
#include <ctime>
#endif

using namespace stag;

namespace {

#include "debugdes.inc"

constexpr quint32 ULTRA_DARK_GREY = 0x101010;
constexpr quint32 COL_GRAY_ORD = 0xDFE0E0;
constexpr quint32 COL_GREEN = 0x008000;

QString at(const char *name, int index)
{
    return QStringLiteral("%1(%2)").arg(QLatin1String(name)).arg(index);
}

QString color(quint32 bgr)
{
    return StaBarModel::vbColor(bgr);
}

QString forDat(const QString &s)
{
    return vb::trim(s).isEmpty() ? QString() : s.mid(6, 2) + u'.' + s.mid(4, 2) + u'.' + s.mid(0, 4);
}

QString forOra(const QString &s)
{
    return vb::trim(s).isEmpty() ? QString() : s.mid(0, 2) + u'.' + s.mid(2, 2) + u'.' + s.mid(4, 2);
}

QString forVal(const QString &s)
{
    return vb::trim(s).isEmpty() ? QStringLiteral("0") : vb::trim(s);
}

QString forMet(const QString &s)
{
    return abi_mis_ang ? con_met_to_fet(s) : forVal(s);
}

QString forMetQua(const QString &s)
{
    return abi_mis_ang ? con_met_qua_to_squ_fet(s) : forVal(s);
}

QString secOre(const QString &text)
{
    const qint64 v = qint64(vb::val(text));
    const qint64 s = v % 60;
    const qint64 m = ((v - s) / 60) % 60;
    const qint64 o = (v - s - m * 60) / 3600;
    return ft(o, 2) + u'.' + ft(m, 2) + u'.' + ft(s, 2);
}

} // namespace

// ---- adj_pat_con

AdjPatConModel::AdjPatConModel(QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_db(db)
    , m_link(link)
    , m_state(state)
{
}

void AdjPatConModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        m_link->boards().setFtcRun(true);
        if (!text.isEmpty())
            messaggio(text, false);
        m_command->resetta();
        showAcquisition(true);
        commit();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        m_link->boards().setFtcRun(false);
        if (!error.isEmpty())
            messaggio(error, true);
        m_command->resetta();
        set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("enabled"), true);
        showAcquisition(false);
        commit();
    });
}

void AdjPatConModel::showAcquisition(bool on)
{
    set(QStringLiteral("lab_proc_acq"), QStringLiteral("visible"), on);
    set(QStringLiteral("sha_proc_acq"), QStringLiteral("visible"), on);
}

// fill_cmb_pat_con. The full list carries "01".."04" and the free one value - 1, so a free "1" asks for
// contrast 0, which snd_sch_tag refuses: both as in VB.
void AdjPatConModel::fillCombo(bool free)
{
    QStringList items;
    if (!free) {
        m_itemData.clear();
        for (int value = 1; value <= 4; ++value) {
            items << QString::number(value);
            m_itemData << value;
        }
    } else {
        QSqlQuery q(m_db);
        if (!q.exec(QStringLiteral("SELECT DISTINCT CONTRASTO FROM PATTERN ORDER BY CONTRASTO")) || !q.next()) {
            messaggio(msg(QStringLiteral("err_no_pat_con_lib")), true);
            return;
        }
        QList<int> left{1, 2, 3, 4};
        do {
            left.removeOne(int(vb::val(q.value(0).toString())));
        } while (q.next());
        m_itemData.clear();
        for (const int value : std::as_const(left)) {
            items << QString::number(value);
            m_itemData << value - 1;
        }
        m_index = items.isEmpty() ? -1 : 0;
    }
    set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("items"), items);
    set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("listIndex"), m_index);
}

void AdjPatConModel::activate(const QString &)
{
    m_index = -1;
    fillCombo(true);
    Livello = 1;
    setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
    showAcquisition(false);
    commit();
}

void AdjPatConModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (control == QLatin1String("cmb_pat_con_sel") && event == QLatin1String("Click"))
        m_index = args.value(QStringLiteral("index")).toInt();
}

bool AdjPatConModel::functionKey(const QString &key, int shift)
{
    SchTagLink &boards = m_link->boards();
    if (key == QLatin1String("F2")) {
        if (boards.ftcRun()) {
            messaggio(msg(QStringLiteral("err_pro_acq_cor")), true);
        } else if (par_mac.car_tec.num_liv_tag.val() > 1) {
            Livello = Livello < int(par_mac.car_tec.num_liv_tag.val()) ? Livello + 1 : 1;
            setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
            commit();
        }
        return true;
    }
    if (key == QLatin1String("F6")) {
        if (boards.ftcRun()) {
            boards.setFtcRun(false);
            set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("enabled"), true);
            showAcquisition(false);
            commit();
        } else {
            messaggio(msg(QStringLiteral("err_no_pro_acq_cor")), true);
        }
        return true;
    }
    if (shift != 0)
        return true;
    if (key == QLatin1String("F1")) {
        fillCombo(m_toggle);
        m_toggle = !m_toggle;
        m_index = value(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("items")).toStringList().isEmpty() ? -1 : 0;
        set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("listIndex"), m_index);
        commit();
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (!m_state->bit(ClasseStato::MAC_F_STOP, 1)) {
            messaggio(msg(QStringLiteral("mac_non_fas_sto")), true);
        } else if ((m_index != -1 || !boards.ftcRun()) && m_index >= 0 && m_index < m_itemData.size() && m_command
                   && m_command->esegui(ACQ_CON_SCH_PLC, Livello, 1, m_itemData.at(m_index))) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false);
            set(QStringLiteral("cmb_pat_con_sel"), QStringLiteral("enabled"), false);
            boards.setFtcRun(true);
            commit();
        } else {
            messaggio(msg(QStringLiteral("imp_ese_ope")), true);
        }
        return true;
    }
    return false;
}

// ---- sel_lin

SelLinModel::SelLinModel(QSqlDatabase db, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_db(db)
    , m_state(state)
{
}

namespace {
// The languages offered, under their own names, and their lin_att: the column in MESSAGGI.
const QStringList LANGUAGES{QStringLiteral("Italiano"), QStringLiteral("English"), QStringLiteral("Deutsch"), QStringLiteral("Polski")};
constexpr int LANGUAGE_LIN[] = {0, 1, 2, 8};
} // namespace

void SelLinModel::activate(const QString &)
{
    m_index = 0;
    for (int i = 0; i < int(LANGUAGES.size()); ++i) {
        if (LANGUAGE_LIN[i] == pro_set.lin_att)
            m_index = i;
    }
    set(QStringLiteral("cmb_lin"), QStringLiteral("items"), LANGUAGES);
    set(QStringLiteral("cmb_lin"), QStringLiteral("listIndex"), m_index);
    commit();
}

void SelLinModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (control == QLatin1String("cmb_lin") && event == QLatin1String("Click"))
        m_index = args.value(QStringLiteral("index")).toInt();
}

bool SelLinModel::functionKey(const QString &key, int shift)
{
    if (key != QLatin1String("F5"))
        return false;
    if (shift != 0)
        return true;
    if (!pro_set.ute_wrk) {
        messaggio(msg(QStringLiteral("acc_con_pwd")), false);
        return true;
    }
    messaggio(msg(QStringLiteral("ope_in_cor")), false);
    const int lin = LANGUAGE_LIN[qBound(0, m_index, int(LANGUAGES.size()) - 1)];
    if (pro_set.lin_att != lin) {
        pro_set.lin_att = qint16(lin);
        Messages::instance().load(m_db.databaseName(), lin);
        setFileINIString(QStringLiteral("pro_set"), QStringLiteral("Lin_att"), QString::number(lin));
        m_state->chgLin();
        if (languageChangedHook)
            languageChangedHook();
    }
    messaggio(msg(QStringLiteral("ope_eff")), false);
    return false;
}

// ---- pag_pro_day

PagProDayModel::PagProDayModel(ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_state(state)
{
}

void PagProDayModel::aggiorna()
{
    const FixedRecord production(shiftProductionLayout(), m_state->get_pro_tg());
    const auto row = [&](const QString &title, const QString &prefix) {
        const auto field = [&](const char *name) { return production.text(prefix + QLatin1String(name)); };
        return QStringList{title, forDat(field("dat_ini")), forOra(field("ora_ini")), forDat(field("dat_fin")), forOra(field("ora_fin")), forMet(field("met_lin")),
                           forMetQua(field("met_qua")), forVal(field("vel_med")), secOre(field("tem_fer")), secOre(field("tem_mar")), forVal(field("num_fer")), forVal(field("num_cam"))};
    };
    QVariantList cells;
    QVariantList back;
    QVariantList fore;
    QStringList header;
    for (const char *name : {"ptg_tur", "ptg_dat_ini", "ptg_ora_ini", "ptg_dat_fin", "ptg_ora_fin", "ptg_met_pro", "ptg_met_qua", "ptg_vel_med", "ptg_tem_fer", "ptg_tem_mar", "ptg_num_fer", "ptg_num_cam"})
        header << msg(QLatin1String(name));
    cells << QVariant(header);
    back << color(ULTRA_DARK_GREY);
    fore << color(0xFFFFFF);
    for (int j = 1; j <= 4; ++j) {
        cells << QVariant(j <= 3 ? row(msg(QStringLiteral("ptg_tur")) + u' ' + QString::number(j), QStringLiteral("tur(%1).").arg(j)) : row(msg(QStringLiteral("ptg_gio")) + u' ', QStringLiteral("gio.")));
        const bool current = j == pro_set.tur;
        back << color(current ? StaBarModel::COL_DARK_Green : (j % 2 ? COL_GRAY_ORD : 0xFFFFFF));
        fore << color(current ? 0xFFFFFF : 0x000000);
    }
    set(QStringLiteral("lis_pro"), QStringLiteral("rows"), cells.size());
    set(QStringLiteral("lis_pro"), QStringLiteral("cols"), 12);
    set(QStringLiteral("lis_pro"), QStringLiteral("cells"), cells);
    set(QStringLiteral("lis_pro"), QStringLiteral("rowBack"), back);
    set(QStringLiteral("lis_pro"), QStringLiteral("rowFore"), fore);
    setText(QStringLiteral("txt_tur"), QString::number(pro_set.tur));
}

void PagProDayModel::activate(const QString &)
{
    aggiorna();
    commit();
}

void PagProDayModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &)
{
    if (control == QLatin1String("tim_ref") && event == QLatin1String("Timer")) {
        aggiorna();
        commit();
    }
}

// ---- cam_tur, con_azz

ShiftDialogModel::ShiftDialogModel(Kind kind, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_kind(kind)
    , m_state(state)
{
}

void ShiftDialogModel::activate(const QString &)
{
    if (m_kind == CamTur)
        setText(QStringLiteral("txt_tur"), QString());
    set(QStringLiteral("txt_tur"), QStringLiteral("focusCount"), ++m_focusCount);
    commit();
}

void ShiftDialogModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (control == QLatin1String("txt_tur") && event == QLatin1String("Change"))
        setText(control, args.value(QStringLiteral("text")).toString());
}

bool ShiftDialogModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key != QLatin1String("F11"))
        return false;
    const int v = int(vb::val(value(QStringLiteral("txt_tur")).toString()));
    if (v < 1 || v > 3) {
        messaggio(msg(QStringLiteral("err_val")), true, false);
        set(QStringLiteral("txt_tur"), QStringLiteral("focusCount"), ++m_focusCount);
        commit();
        return true;
    }
    if (m_kind == CamTur) {
        m_state->chg_tur(v);
    } else {
        pro_set.tur = qint16(v);
        setFileINIString(QStringLiteral("pro_set"), QStringLiteral("tur"), QString::number(v));
        m_state->tempi(true, true, true);
    }
    return false;
}

// ---- dia_sch_tag

namespace {
// How many lab_par_<n>(1..) labels each alarm line of dia_sch_tag has.
constexpr int PAR_LABELS[19] = {0, 10, 9, 9, 8, 8, 6, 8, 5, 3, 7, 4, 2, 3, 1, 0, 5, 4, 0};
} // namespace

DiaSchTagModel::DiaSchTagModel(PlcLink *link, QObject *parent)
    : FormModel(parent)
{
    connect(&link->boards(), &SchTagLink::diagnosticsRead, this, [this](int, const QString &first, const QString &second) {
        colLabMsgAll(vb::hexBin(first, false), vb::hexBin(second, false));
    });
}

void DiaSchTagModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        if (!text.isEmpty())
            messaggio(text, false);
        m_command->resetta();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        if (!error.isEmpty())
            messaggio(error, true);
        m_command->resetta();
    });
}

// Only a red line paints its lab_par labels; going back to black happens in ini_form, as in VB.
void DiaSchTagModel::paint(int line, bool red)
{
    const QString shown = red ? StaBarModel::vbColor(StaBarModel::vbRed) : QStringLiteral("#000000");
    set(at("lab_msg_all", line), QStringLiteral("foreColor"), shown);
    set(at("lab_num", line), QStringLiteral("foreColor"), shown);
    if (red && line <= 17) {
        for (int k = 1; k <= PAR_LABELS[line]; ++k)
            set(QStringLiteral("lab_par_%1(%2)").arg(line).arg(k), QStringLiteral("foreColor"), shown);
    }
}

void DiaSchTagModel::colLabMsgAll(const QString &first, const QString &second)
{
    const bool ok = (first + second) == QString(first.size() + second.size(), u'0');
    setText(QStringLiteral("lab_ris"), ok ? QStringLiteral("OK") : QStringLiteral("ERROR"));
    set(QStringLiteral("lab_ris"), QStringLiteral("foreColor"), color(ok ? StaBarModel::COL_DARK_Green : StaBarModel::COL_ALL_Red));
    QString reversed = first;
    std::reverse(reversed.begin(), reversed.end());
    int i = 1;
    for (; i <= reversed.size(); ++i)
        paint(i, reversed.at(i - 1) == u'1');
    paint(i, second.right(1) == QLatin1String("1"));
    ++i;
    set(at("lab_msg_all", i), QStringLiteral("foreColor"), second.mid(second.size() - 2, 1) == QLatin1String("1") ? StaBarModel::vbColor(StaBarModel::vbRed) : QStringLiteral("#000000"));
    set(at("lab_num", i), QStringLiteral("foreColor"), second.mid(second.size() - 2, 1) == QLatin1String("1") ? StaBarModel::vbColor(StaBarModel::vbRed) : QStringLiteral("#000000"));
    commit();
}

void DiaSchTagModel::activate(const QString &previousForm)
{
    for (int line = 1; line <= 18; ++line) {
        paint(line, false);
        for (int k = 1; k <= PAR_LABELS[line]; ++k)
            set(QStringLiteral("lab_par_%1(%2)").arg(line).arg(k), QStringLiteral("foreColor"), QStringLiteral("#000000"));
    }
    setText(QStringLiteral("lab_ris"), QString());
    const QString previous = previousForm.toLower();
    if (previous == QLatin1String("all_qe1"))
        setText(QStringLiteral("lab_liv"), QStringLiteral("1"));
    else if (previous == QLatin1String("all_qe2"))
        setText(QStringLiteral("lab_liv"), QStringLiteral("2"));
    else if (previous == QLatin1String("all_qe3"))
        setText(QStringLiteral("lab_liv"), QStringLiteral("3"));
    commit();
}

bool DiaSchTagModel::functionKey(const QString &key, int shift)
{
    if (key != QLatin1String("F3"))
        return false;
    if (shift != 0)
        return true;
    const int level = int(vb::val(value(QStringLiteral("lab_liv")).toString()));
    if (level <= int(par_mac.car_tec.num_liv_tag.val())) {
        num_com_sch_tag = 1;
        if (m_command && m_command->esegui(LETTURA_STATO, level, 0))
            messaggio(msg(QStringLiteral("inv_par_sch_tag")), false);
        else
            messaggio(msg(QStringLiteral("imp_ese_ope")), true);
    } else {
        messaggio(msg(QStringLiteral("sch_tag_non_pre")), true, false);
    }
    return true;
}

// ---- frm_debug, frm_debug_des

FrmDebugModel::FrmDebugModel(bool described, FinsClient *fins, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_described(described)
    , m_fins(fins)
    , m_state(state)
{
}

void FrmDebugModel::setRunning(bool on)
{
    set(QStringLiteral("tim_debug"), QStringLiteral("running"), on);
}

void FrmDebugModel::fillPage()
{
    if (!m_described) {
        for (int i = 0; i < 100; ++i)
            setText(at("lab_dm", i), QString::number(m_dm + i));
        return;
    }
    setText(QStringLiteral("lab_int_dm"), QStringLiteral("%1 - %2").arg(m_dm).arg(m_dm + 49));
    for (int i = 0; i < 50; ++i) {
        QString text;
        for (const auto &row : DEBUG_DES) {
            if (row.dm == m_dm + i)
                text = QString::fromUtf8(row.text);
        }
        setText(at("lab_dm", i), text);
    }
    for (const auto &row : DEBUG_COLORS) {
        if (row.block == m_dm)
            set(at("lab_dm", row.index), QStringLiteral("backColor"), color(row.color));
    }
}

void FrmDebugModel::activate(const QString &)
{
    const QStringList NAMES{msg(QStringLiteral("vis_dec")), msg(QStringLiteral("vis_esa")), msg(QStringLiteral("vis_bin"))};
    setText(QStringLiteral("lab_vis"), NAMES.at(m_vis));
    setRunning(m_state->comOK());
    if (!m_state->comOK()) {
        for (int i = 0; i < words(); ++i)
            setText(at("txt_dm", i), QString());
    }
    fillPage();
    commit();
}

void FrmDebugModel::read()
{
    if (m_fins->readArea(quint16(m_dm), 100) < 0)
        return;
    const QVector<qint32> data = m_fins->retData();
    for (int i = 0; i < words() && i < data.size(); ++i) {
        const qint32 word = data.at(i);
        setText(at("txt_dm", i), m_vis == 0 ? QString::number(word) : m_vis == 1 ? vb::intEsa(word) : vb::hexBin(vb::intEsa(word), false));
    }
    commit();
}

void FrmDebugModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &)
{
    if (control == QLatin1String("tim_debug") && event == QLatin1String("Timer"))
        read();
}

bool FrmDebugModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key == QLatin1String("F1")) {
        if (m_described)
            m_dm = m_dm == 3650 ? 3500 : m_dm + 50;
        else
            m_dm = m_dm == 3500 ? 3600 : 3500;
        fillPage();
    } else if (key == QLatin1String("F2")) {
        const QStringList NAMES{msg(QStringLiteral("vis_dec")), msg(QStringLiteral("vis_esa")), msg(QStringLiteral("vis_bin"))};
        m_vis = (m_vis + 1) % 3;
        setText(QStringLiteral("lab_vis"), NAMES.at(m_vis));
    } else if (key == QLatin1String("F3")) {
        setRunning(false);
    } else if (key == QLatin1String("F4")) {
        if (m_state->comOK())
            setRunning(true);
    } else {
        setRunning(false);
        commit();
        return false;
    }
    commit();
    return true;
}

// ---- pag_msg

void PagMsgModel::show(const QString &text, bool isAll, int liv)
{
    if (liv <= 0 || liv > int(par_mac.car_tec.num_liv_tag.val()))
        return;
    if (!m_open) {
        m_open = true;
        for (int i = 0; i < 3; ++i)
            setText(at("lab_msg", i), QString());
        emit opened();
    }
    setText(at("lab_msg", liv - 1), text);
    set(at("lab_msg", liv - 1), QStringLiteral("foreColor"), color(isAll ? StaBarModel::COL_ALL_Red : StaBarModel::COL_DARK_Green));
    commit();
}

bool PagMsgModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("ESC") && shift == 0)
        m_open = false;
    return false;
}

// ---- frm_data

namespace {
// Date = and Time = of VB.
bool setSystemClock(const QDateTime &when)
{
#ifdef Q_OS_WIN
    const QDate d = when.date();
    const QTime t = when.time();
    SYSTEMTIME st{WORD(d.year()), WORD(d.month()), WORD(d.dayOfWeek() % 7), WORD(d.day()), WORD(t.hour()), WORD(t.minute()), WORD(t.second()), 0};
    return SetLocalTime(&st);
#else
    timespec ts{};
    ts.tv_sec = when.toSecsSinceEpoch();
    return clock_settime(CLOCK_REALTIME, &ts) == 0;
#endif
}
} // namespace

void FrmDataModel::updTime()
{
    const QDateTime now = vb::now();
    setText(QStringLiteral("txt_day"), ft(now.date().day(), 2));
    setText(QStringLiteral("txt_month"), ft(now.date().month(), 2));
    setText(QStringLiteral("txt_year"), ft(now.date().year(), 4));
    setText(QStringLiteral("txt_ore"), ft(now.time().hour(), 2));
    setText(QStringLiteral("txt_min"), ft(now.time().minute(), 2));
    setText(QStringLiteral("txt_sec"), ft(now.time().second(), 2));
}

void FrmDataModel::activate(const QString &)
{
    set(QStringLiteral("tim_ora"), QStringLiteral("running"), true);
    updTime();
    set(QStringLiteral("txt_day"), QStringLiteral("focusCount"), ++m_focusCount);
    commit();
}

void FrmDataModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (control == QLatin1String("tim_ora") && event == QLatin1String("Timer")) {
        updTime();
        commit();
    } else if (event == QLatin1String("Change")) {
        setText(control, args.value(QStringLiteral("text")).toString());
        // my_key: typing a digit stops the clock.
        set(QStringLiteral("tim_ora"), QStringLiteral("running"), false);
        commit();
    }
}

bool FrmDataModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key != QLatin1String("F3"))
        return false;
    const auto field = [this](const char *name) { return int(vb::val(value(QLatin1String(name)).toString())); };
    const QDateTime wanted(QDate(field("txt_year"), field("txt_month"), field("txt_day")), QTime(field("txt_ore"), field("txt_min"), field("txt_sec")));
    const ClockSetter setter = m_setter ? m_setter : ClockSetter(setSystemClock);
    if (!wanted.isValid() || !setter(wanted)) {
        log_str(QStringLiteral("on error intervenuta in frm_data eve_key_F3: impostazione orologio fallita"), MED_PRIORITY, CosLog::Gen);
        messaggio(msg(QStringLiteral("imp_ese_ope")), true, false);
    }
    return false;
}

// ---- imp_all

ImpAllModel::ImpAllModel(QSqlDatabase db, QObject *parent)
    : FormModel(parent)
    , m_db(db)
{
    m_addresses << QStringLiteral("Indirizzo");
    for (int i = 0; i < 21; ++i) {
        for (int j = 0; j < 16; ++j)
            m_addresses << QString::number(i) + ft(j, 2);
    }
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val() * par_mac.car_tec.num_mot_liv_tag.val()); ++i)
        m_addresses << u'L' + ft(i, 2);
    QVariantList rows;
    for (const QString &address : std::as_const(m_addresses))
        rows << QVariant(QStringList{address});
    set(QStringLiteral("add"), QStringLiteral("rows"), rows.size());
    set(QStringLiteral("add"), QStringLiteral("cols"), 1);
    set(QStringLiteral("add"), QStringLiteral("cells"), rows);
    set(QStringLiteral("griglia"), QStringLiteral("rows"), 6);
    set(QStringLiteral("griglia"), QStringLiteral("cols"), 6);
    set(QStringLiteral("griglia"), QStringLiteral("fixedRows"), 0);
    set(QStringLiteral("griglia"), QStringLiteral("fixedCols"), 0);
    set(QStringLiteral("griglia"), QStringLiteral("fitCells"), true);
}

// des_pag_all
QString ImpAllModel::page() const
{
    static const QStringList PAGES{QStringLiteral("mac"), QStringLiteral("har"), QStringLiteral("con"), QStringLiteral("qe1"), QStringLiteral("qe2"), QStringLiteral("qe3"), QStringLiteral("war")};
    return PAGES.at(m_page);
}

void ImpAllModel::loadPage(int number)
{
    m_page = number;
    m_cells.clear();
    QSqlQuery q(m_db);
    if (q.exec(QStringLiteral("SELECT DM FROM ALL_%1 ORDER BY rec_no LIMIT 36").arg(page().toUpper()))) {
        while (q.next())
            m_cells << q.value(0).toString();
    }
    while (m_cells.size() < 36)
        m_cells << QString();
}

void ImpAllModel::showGrid()
{
    QVariantList cells;
    QVariantList back;
    QVariantList fore;
    for (int row = 0; row < 6; ++row) {
        QStringList texts;
        QStringList backs;
        QStringList fores;
        for (int col = 0; col < 6; ++col) {
            const bool cursor = row == m_row && col == m_col;
            texts << m_cells.value(row * 6 + col);
            backs << color(cursor ? COL_GREEN : 0xFFFFFF);
            fores << color(cursor ? 0xFFFFFF : 0x000000);
        }
        cells << QVariant(texts);
        back << QVariant(backs);
        fore << QVariant(fores);
    }
    set(QStringLiteral("griglia"), QStringLiteral("cells"), cells);
    set(QStringLiteral("griglia"), QStringLiteral("cellBack"), back);
    set(QStringLiteral("griglia"), QStringLiteral("cellFore"), fore);
    set(QStringLiteral("add"), QStringLiteral("row"), m_addressRow);
    set(QStringLiteral("all"), QStringLiteral("enabled"), m_textsOn);
    set(QStringLiteral("det_all"), QStringLiteral("enabled"), m_textsOn);
    set(QStringLiteral("add"), QStringLiteral("enabled"), m_addOn);
    set(QStringLiteral("txt_pag_allarmi"), QStringLiteral("enabled"), m_pageOn);
}

// griglia_EnterCell
void ImpAllModel::showTexts()
{
    const QString blank = vb::trim(cell()).isEmpty() ? QStringLiteral("   ") : cell();
    setText(QStringLiteral("all"), Messages::instance().find(QStringLiteral("all_%1_").arg(page()) + blank));
    setText(QStringLiteral("det_all"), Messages::instance().find(QStringLiteral("det_all_%1_").arg(page()) + blank));
}

void ImpAllModel::activate(const QString &)
{
    m_opeAbi = true;
    m_gridOn = true;
    m_addOn = m_textsOn = m_pageOn = false;
    m_addressRow = 1;
    QStringList pages;
    for (const char *name : {"AMAC", "AHAR", "ACON", "AQE1", "AQE2", "AQE3", "AWAR"})
        pages << msg(QLatin1String(name));
    set(QStringLiteral("txt_pag_allarmi"), QStringLiteral("items"), pages);
    set(QStringLiteral("txt_pag_allarmi"), QStringLiteral("listIndex"), 0);
    loadPage(0);
    showGrid();
    showTexts();
    commit();
}

void ImpAllModel::keyUp(int key)
{
    if (m_addOn) {
        if (key == Qt::Key_Down && m_addressRow < m_addresses.size() - 1)
            ++m_addressRow;
        else if (key == Qt::Key_Up && m_addressRow > 1)
            --m_addressRow;
        else
            return;
    } else if (m_gridOn) {
        if (key == Qt::Key_Right && m_col < 5)
            ++m_col;
        else if (key == Qt::Key_Left && m_col > 0)
            --m_col;
        else if (key == Qt::Key_Down && m_row < 5)
            ++m_row;
        else if (key == Qt::Key_Up && m_row > 0)
            --m_row;
        else
            return;
        showTexts();
    } else {
        return;
    }
    showGrid();
    commit();
}

void ImpAllModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (event == QLatin1String("Change") && (control == QLatin1String("all") || control == QLatin1String("det_all"))) {
        setText(control, args.value(QStringLiteral("text")).toString());
    } else if (event == QLatin1String("Click") && control == QLatin1String("txt_pag_allarmi")) {
        m_page = args.value(QStringLiteral("index")).toInt();
    } else if (event == QLatin1String("Click") && control == QLatin1String("griglia") && m_gridOn) {
        m_row = qBound(0, args.value(QStringLiteral("row")).toInt(), 5);
        m_col = qBound(0, args.value(QStringLiteral("col")).toInt(), 5);
        showGrid();
        showTexts();
        commit();
    }
}

// sal_ind: rc.Move to the cell's record.
void ImpAllModel::saveAddress(const QString &address)
{
    QSqlQuery q(m_db);
    sqlPrepare(q, QStringLiteral("UPDATE ALL_%1 SET DM = ? WHERE rec_no = (SELECT rec_no FROM ALL_%1 ORDER BY rec_no LIMIT 1 OFFSET ?)").arg(page().toUpper()));
    q.addBindValue(address);
    q.addBindValue(m_row * 6 + m_col);
    if (!q.exec())
        log_str(QStringLiteral("imp_all: sal_ind fallita"), MED_PRIORITY, CosLog::Gen);
}

// sal_des. The text column is the language's own: lin_att + 3 in VB, + 1 here for rec_no.
void ImpAllModel::saveTexts()
{
    if (cell().isEmpty())
        return;
    QSqlQuery columns(m_db);
    if (!columns.exec(QStringLiteral("SELECT * FROM MESSAGGI LIMIT 0")))
        return;
    const QString column = columns.record().fieldName(4 + pro_set.lin_att);
    for (const auto &[key, control] : {std::pair(QStringLiteral("all_%1_").arg(page()) + cell(), QStringLiteral("all")),
                                       std::pair(QStringLiteral("det_all_%1_").arg(page()) + cell(), QStringLiteral("det_all"))}) {
        QSqlQuery q(m_db);
        sqlPrepare(q, QStringLiteral("UPDATE MESSAGGI SET \"%1\" = ? WHERE CONTROLLO = ?").arg(column));
        q.addBindValue(value(control).toString());
        q.addBindValue(key);
        sqlExec(q);
        Messages::instance().setText(key, value(control).toString());
    }
}

// ESC_imp_all. The mia_ope statics of VB survive it, so they stay set here too.
void ImpAllModel::escape()
{
    if (m_textsOn) {
        m_gridOn = true;
        m_textsOn = false;
        showTexts();
    }
    if (m_addOn) {
        m_gridOn = true;
        m_addressRow = 1;
        m_addOn = false;
    }
    if (m_pageOn) {
        m_gridOn = true;
        m_pageOn = false;
    }
    m_opeAbi = true;
    showGrid();
    commit();
}

bool ImpAllModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("ESC")) {
        if (shift == 0)
            escape();
        return true;
    }
    const bool edit = key == QLatin1String("F1") || key == QLatin1String("F2") || key == QLatin1String("F3") || key == QLatin1String("F4");
    if (edit && !pro_set.ute_wrk) {
        messaggio(msg(QStringLiteral("acc_pwd")), true);
        return true;
    }
    if (shift != 0)
        return true;
    if (key == QLatin1String("F1")) {
        if (m_opeAbi || m_mineAdd) {
            if (m_gridOn) {
                m_gridOn = false;
                m_addOn = true;
                m_mineAdd = true;
                m_opeAbi = false;
            } else {
                const QString address = m_addresses.value(m_addressRow);
                m_cells[m_row * 6 + m_col] = address;
                saveAddress(address);
                showTexts();
                m_gridOn = true;
                m_addOn = false;
                m_mineAdd = false;
                m_opeAbi = true;
            }
        }
    } else if (key == QLatin1String("F2")) {
        if ((m_opeAbi || m_mineDes) && !cell().isEmpty() && !m_addOn) {
            if (m_gridOn) {
                m_gridOn = false;
                m_textsOn = true;
                set(QStringLiteral("all"), QStringLiteral("focusCount"), ++m_focusCount);
                m_mineDes = true;
                m_opeAbi = false;
            } else {
                saveTexts();
                m_gridOn = true;
                m_textsOn = false;
                m_mineDes = false;
                m_opeAbi = true;
            }
        }
    } else if (key == QLatin1String("F3")) {
        if (m_opeAbi) {
            m_cells[m_row * 6 + m_col] = QStringLiteral("   ");
            saveAddress(QStringLiteral("   "));
            setText(QStringLiteral("det_all"), QString());
            setText(QStringLiteral("all"), QString());
        }
    } else if (key == QLatin1String("F4")) {
        if (m_opeAbi || m_minePage) {
            if (!m_pageOn) {
                m_pageOn = true;
                m_gridOn = false;
                m_minePage = true;
                m_opeAbi = false;
            } else {
                m_pageOn = false;
                m_gridOn = true;
                loadPage(m_page);
                showTexts();
                m_minePage = false;
                m_opeAbi = true;
            }
        }
    } else {
        return !m_opeAbi;
    }
    showGrid();
    commit();
    return true;
}
