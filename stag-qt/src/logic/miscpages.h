#pragma once

#include "collezioneordini.h"
#include "formmodel.h"
#include "plclink.h"

#include <QSqlDatabase>
#include <QDateTime>
#include <QStringList>

#include <functional>

class ClasseStato;
class FinsClient;

// adj_pat_con: the photocell learns a contrast. PLC and cut board are told through the macro queue.
class AdjPatConModel : public FormModel
{
    Q_OBJECT

public:
    AdjPatConModel(QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent = nullptr);
    void setCommand(Rmt *command);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    void fillCombo(bool free);
    void showAcquisition(bool on);

    QSqlDatabase m_db;
    PlcLink *m_link;
    ClasseStato *m_state;
    Rmt *m_command = nullptr;
    QList<int> m_itemData;
    int m_index = -1;
    bool m_toggle = false;
};

// sel_lin
class SelLinModel : public FormModel
{
    Q_OBJECT

public:
    SelLinModel(QSqlDatabase db, ClasseStato *state, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    QSqlDatabase m_db;
    ClasseStato *m_state;
    int m_index = 0;
};

// pag_pro_day
class PagProDayModel : public FormModel
{
    Q_OBJECT

public:
    explicit PagProDayModel(ClasseStato *state, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

private:
    void aggiorna();

    ClasseStato *m_state;
};

// cam_tur (change shift) and con_azz (reset the counters): one field and F11.
class ShiftDialogModel : public FormModel
{
    Q_OBJECT

public:
    enum Kind { CamTur, ConAzz };
    ShiftDialogModel(Kind kind, ClasseStato *state, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    Kind m_kind;
    ClasseStato *m_state;
    int m_focusCount = 0;
};

// dia_sch_tag
class DiaSchTagModel : public FormModel
{
    Q_OBJECT

public:
    explicit DiaSchTagModel(PlcLink *link, QObject *parent = nullptr);
    void setCommand(Rmt *command);
    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;
    // col_lab_msg_all, with the two words already in binary text.
    void colLabMsgAll(const QString &first, const QString &second);

private:
    void paint(int line, bool red);
    Rmt *m_command = nullptr;
};

// frm_debug and frm_debug_des: DM words read from the PLC every 300 ms. Reads only.
class FrmDebugModel : public FormModel
{
    Q_OBJECT

public:
    FrmDebugModel(bool described, FinsClient *fins, ClasseStato *state, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;
    bool running() const { return value(QStringLiteral("tim_debug"), QStringLiteral("running")).toBool(); }

private:
    void fillPage();
    void setRunning(bool on);
    void read();
    int words() const { return m_described ? 50 : 100; }

    bool m_described;
    FinsClient *m_fins;
    ClasseStato *m_state;
    int m_dm = 3500;
    int m_vis = 0;
};

// pag_msg: what the cut boards answered, one line per level, until ESC.
class PagMsgModel : public FormModel
{
    Q_OBJECT

public:
    using FormModel::FormModel;
    void show(const QString &text, bool isAll, int liv);
    bool functionKey(const QString &key, int shift) override;

signals:
    void opened();

private:
    bool m_open = false;
};

// frm_data. Setting the clock of the PC can fail without privileges: that gives a message.
class FrmDataModel : public FormModel
{
    Q_OBJECT

public:
    using FormModel::FormModel;
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;
    using ClockSetter = std::function<bool(const QDateTime &)>;
    void setClockSetter(ClockSetter setter) { m_setter = std::move(setter); }

private:
    void updTime();
    ClockSetter m_setter;
    int m_focusCount = 0;
};

// imp_all: which DM bit each cell of the seven alarm pages shows, and the alarm's texts.
class ImpAllModel : public FormModel
{
    Q_OBJECT

public:
    explicit ImpAllModel(QSqlDatabase db, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void keyUp(int key) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    QString page() const;
    QString cell() const { return m_cells.value(m_row * 6 + m_col); }
    void loadPage(int number);
    void showGrid();
    void showTexts();
    void saveAddress(const QString &address);
    void saveTexts();
    void escape();

    QSqlDatabase m_db;
    QStringList m_cells;
    QStringList m_addresses;
    int m_page = 0;
    int m_row = 0;
    int m_col = 0;
    int m_addressRow = 1;
    bool m_opeAbi = true;
    bool m_gridOn = true;
    bool m_addOn = false;
    bool m_textsOn = false;
    bool m_pageOn = false;
    bool m_mineAdd = false;
    bool m_mineDes = false;
    bool m_minePage = false;
    int m_focusCount = 0;
};
