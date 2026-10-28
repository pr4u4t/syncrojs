#pragma once

#include "parmac.h"
#include "stagrecords.h"

#include <QList>
#include <QObject>
#include <QString>

#include <memory>

// ClasseOrdine.cls: one Tip_Ordine; the properties are reached through the record field names.
class ClasseOrdine
{
public:
    ClasseOrdine() = default;

    // Property ordine: the raw record (Ord_str / str_ord).
    QByteArray ordine() const { return m_order.raw(); }
    void setOrdine(const QByteArray &raw) { m_order.setRaw(raw); }

    QString text(const QString &field) const { return m_order.text(field); }
    void setText(const QString &field, const QString &value) { m_order.setText(field, value); }
    double number(const QString &field) const { return m_order.number(field); }

    QString stag_id() const { return text(QStringLiteral("stag_id")); }
    void set_stag_id(const QString &v) { setText(QStringLiteral("stag_id"), v); }
    QString mod_id() const { return text(QStringLiteral("mod_id")); }
    void set_mod_id(const QString &v) { setText(QStringLiteral("mod_id"), v); }
    QString old_stag_id() const { return text(QStringLiteral("old_stag_id")); }
    QString cod_ord_abb() const { return text(QStringLiteral("cod_ord_abb")); }
    QString ord(int i, const QString &field) const { return text(QStringLiteral("ord(%1).%2").arg(i).arg(field)); }

    int Ord_liv(int i) const;
    int ind_liv(int indFor) const;

private:
    stag::OrderRecord m_order;
};

using OrdinePtr = std::shared_ptr<ClasseOrdine>;

// CollezioneOrdini.cls: ord_mac (to do) and ord_mac_ese (done), saved to Ord_mac.dat / Ord_mac_ese.dat.
class CollezioneOrdini : public QObject
{
    Q_OBJECT

public:
    explicit CollezioneOrdini(QObject *parent = nullptr);

    int Init(bool eseg);
    int leg_pun_ord() const { return m_punOrd; }
    int Move_blk(int ini, int fin, int dst);
    int Cpy_blk(int ini, int fin, int dst);
    void Fine();
    int Modifica(const OrdinePtr &ord, int pos, bool rcvOrdSpde = false);
    int add(const OrdinePtr &ord, int pos = stag::MAXINT, bool rcvOrdSpde = false);
    int Count() const { return int(m_ordini.size()); }
    int Del_blk(int ini, int fin, bool shift, bool cmdSpde, bool aggRepProOldOrd = false);
    void Leg_dat_ord();
    OrdinePtr item(int indice) const;
    void pun_ord_mov_next();
    void pun_ord_mov_prev();
    void Salva(bool shift);
    void scr_pun_ord(int nuoPunOrd) { m_punOrd = nuoPunOrd; }
    int Tro_id_lis(QString id);
    int Tro_lis(QString cod);

signals:
    void Refresh(bool shift);

private:
    QString fileName() const;
    void Crea_fil_ord();
    int Cod_uni();
    bool manualeOrSpdeKo() const;

    QList<OrdinePtr> m_ordini;
    bool m_ese = false;
    bool m_iniz = false;
    int m_punOrd = 0;
};
