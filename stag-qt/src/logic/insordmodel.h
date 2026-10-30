#pragma once

#include "collezioneordini.h"
#include "formmodel.h"

#include <QHash>
#include <QSqlDatabase>
#include <QStringList>
#include <QVector>

class ClasseStato;

// Insord.frm with ins_ord_bas.bas: new, changed and null orders. It only changes ord_mac and its file;
// telling the PLC about the list is not part of this form.
class InsOrdModel : public FormModel
{
    Q_OBJECT

public:
    enum TipOp { OLI_NUL, OLI_NEW, OLI_MOD, OLI_NEW_NULL };

    InsOrdModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent = nullptr);

    // Form_Load
    void load();
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

    // eve_sta_modo, with ClasseStato::StaMod
    void machineMode(int mode);

    TipOp operation() const { return m_tipOp; }
    QString text(const QString &control) const { return m_text.value(control); }
    int listIndex(const QString &control) const { return m_listIndex.value(control, -1); }
    const ClasseOrdine &orderBeingEdited() const { return m_rec; }
    // get_new_dat, public for the round trip test.
    void getNewDat();

private:
    bool machineFree() const;
    void iniForm();
    void riempiCelle(int order);
    void clearCell();
    void enableFrames(bool enabled);
    void applyCutPatternStops();
    void setCutStops(int format, bool enabled);
    void setText(const QString &control, const QString &value);
    void setListIndex(const QString &control, int index);
    void setDbCombo(const QString &control, const QStringList &items, const QString &text);
    void reloadFlutes();
    void reloadPapers(const QString &flute);
    void reloadPatterns();
    void comboChanged(const QString &key, const QString &value);
    void focus(const QString &control);
    void tab(const QString &from, int step);
    bool valInsOrd(const QString &control, int index, int passedIndex);
    bool validateLast();
    void gotFocus(const QString &key, const QString &name, int index);
    void cancel();
    void save();
    void newOrder();
    void nullOrder();
    void refresh(bool shift);

    CollezioneOrdini *m_orders;
    QSqlDatabase m_db;
    TipOp m_tipOp = OLI_NUL;
    ClasseOrdine m_rec;
    QHash<QString, QString> m_text;
    QHash<QString, int> m_listIndex;
    QVector<int> m_levelData;
    QVector<int> m_cutData;
    QStringList m_controls;
    QHash<int, bool> m_cutStops;
    QString m_ultCon;
    int m_ultConIndex = -1;
    int m_ultInd = -1;
    bool m_flgRev = false;
    int m_focusCount = 0;
    QHash<int, QString> m_oldPattern;
};
