#pragma once

#include "formmodel.h"
#include "parcurtag.h"
#include "parmac.h"
#include "plclink.h"

#include <QSqlDatabase>
#include <QStringList>
#include <QVariantMap>

#include <array>

class ClasseStato;

// sch_tag_bas: what happens once a card answered a CONFIGURA sent by the pages below (ges_fine_trasmissione).
class SchTagConfig : public QObject
{
    Q_OBJECT

public:
    SchTagConfig(QSqlDatabase db, PlcLink *link, CollezioneOrdini *orders, QObject *parent = nullptr);

signals:
    // set_par_sch_tag3.fil_par_sch_tag_liv / fil_par_sch_tag_def_liv
    void configurationChanged(int liv, bool defaults);
    // fil_par_sch_tag_liv of the page shown, pages 1, 2, 4 and 5.
    void valuesChanged(int liv);

private:
    void ended(int liv, int sendCase, bool ack);
    void afterConfiguration(int liv);

    QSqlDatabase m_db;
    PlcLink *m_link;
    CollezioneOrdini *m_orders;
    Rmt *m_macro;
};

// What set_par_sch_tag1..5 share: edit mode, focus by TabIndex, the checks of ver_ese_ope.
class SchTagPageBase : public FormModel
{
    Q_OBJECT

public:
    SchTagPageBase(PlcLink *link, ClasseStato *state, QObject *parent);

    void setCommand(Rmt *command);
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

protected:
    bool verEseOpe(bool tot);
    bool ready(int liv);
    void sendConfigure(int sendCase, int liv, bool all, bool quietWhenBusy);
    void setEditable(const QStringList &controls, bool editable);
    void focus(const QString &control);
    void idle();
    virtual void changed(const QString &control) { Q_UNUSED(control) }
    virtual bool editing() const = 0;

    PlcLink *m_link;
    ClasseStato *m_state;
    Rmt *m_command = nullptr;
    QStringList m_tab;
    int m_focusCount = 0;
};

// set_par_sch_tag1, 2, 4 and 5: the same page over different fields of sch_tag_liv.
class SetParSchTagModel : public SchTagPageBase
{
    Q_OBJECT

public:
    SetParSchTagModel(int page, QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent = nullptr);

    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;
    void filAtt(int liv);
    void filDef(int liv);
    int page() const { return m_page; }
    void setPlcCommand(Rmt *command);

protected:
    void changed(const QString &control) override;
    bool editing() const override { return m_mod || m_modCur; }

private:
    void setLabels();
    void iniForm();
    void filInf(int liv);
    void filCur(int liv);
    void attInDef(bool tot);
    void defInAtt(bool tot);
    void scrAttUte(int liv);
    void scrDefUte(int liv);
    void enableAtt(const QString &first);
    void disableAll();
    bool cutOption() const;
    bool cutPatOption() const;

    int m_page;
    QSqlDatabase m_db;
    bool m_mod = false;
    bool m_modCur = false;
    Rmt *m_plc = nullptr;
    QStringList m_attControls;
    std::array<stag::TipCurTag, 4> m_curUte;
};

// set_par_sch_tag3: the configuration dependent parameters.
class SetParSchTag3Model : public SchTagPageBase
{
    Q_OBJECT

public:
    SetParSchTag3Model(QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent = nullptr);

    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;
    void filAtt(int liv);
    void filDef(int liv);

protected:
    void changed(const QString &control) override;
    bool editing() const override { return m_ope != Nessuna; }

private:
    enum Ope { Nessuna, Inserimento, Modifica };
    void iniForm();
    void scrAttUte(int liv);
    void scrDefUte(int liv);
    bool verEseOpe3();
    void leave();

    QSqlDatabase m_db;
    Ope m_ope = Nessuna;
};

// set_par_mac
class SetParMacModel : public FormModel
{
    Q_OBJECT

public:
    SetParMacModel(QSqlDatabase db, ClasseStato *state, QObject *parent = nullptr);
    void setCommands(Rmt *plc, Rmt *macro);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    void filParMac();
    void scrParMacUte();
    void annullaOpe();
    void lock();
    void unlock();
    void combo(const QString &control, const QStringList &items, int index);
    bool checked(const QString &control) const { return value(control, QStringLiteral("checked")).toBool(); }
    int listIndex(const QString &control) const { return value(control, QStringLiteral("listIndex")).toInt(); }
    QString comboText(const QString &control) const;
    bool verLunTag();
    void save();

    QSqlDatabase m_db;
    ClasseStato *m_state;
    Rmt *m_plc = nullptr;
    Rmt *m_macro = nullptr;
    bool m_mod = false;
    int m_focusCount = 0;
};

// sal_cfg
class SalCfgModel : public FormModel
{
    Q_OBJECT

public:
    SalCfgModel(QSqlDatabase db, PlcLink *link, QObject *parent = nullptr);
    void setCommand(Rmt *command);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

private:
    QSqlDatabase m_db;
    PlcLink *m_link;
    Rmt *m_command = nullptr;
    int m_focusCount = 0;
};

// del_cfg
class DelCfgModel : public FormModel
{
    Q_OBJECT

public:
    explicit DelCfgModel(QSqlDatabase db, QObject *parent = nullptr);
    void activate(const QString &previousForm) override;
    void keyUp(int key) override;
    bool functionKey(const QString &key, int shift) override;

private:
    void aggLab();

    QSqlDatabase m_db;
    QList<QVariantMap> m_records;
    int m_pos = 0;
    bool m_empty = false;
};

// leg_cfg
class LegCfgModel : public FormModel
{
    Q_OBJECT

public:
    LegCfgModel(QSqlDatabase db, PlcLink *link, QObject *parent = nullptr);
    void setCommand(Rmt *command);
    void activate(const QString &previousForm) override;
    void keyUp(int key) override;
    bool functionKey(const QString &key, int shift) override;

private:
    void iniLab();
    void iniDbf();
    void aggLab();
    void scrParUte(int liv);

    QSqlDatabase m_db;
    PlcLink *m_link;
    Rmt *m_command = nullptr;
    QList<QVariantMap> m_records;
    int m_pos = 0;
    bool m_empty = false;
    QString m_forMac;
    QString m_graCar;
    QString m_numMotLiv;
    QString m_numMotAbi;
};
