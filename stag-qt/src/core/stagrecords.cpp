#include "stagrecords.h"

#include <stdexcept>

namespace stag {
namespace {

RecordLayout ondaLayout()
{
    RecordLayout layout; // Tip_ONDA
    layout.add("cod_com_car", 36);
    layout.add("Cod_ond", 3);
    layout.add("Alt_ond", 4);
    layout.add("Vel_aoc", 4);
    layout.add("vel_pro", 4);
    layout.add("Pes_mt2", 4);
    layout.add("Cod_cod_ond", 4);
    return layout;
}

RecordLayout formatoLayout()
{
    RecordLayout commessa; // Tip_Commessa
    commessa.add("cod_com_ord", 15);
    commessa.add("cod_cli", 12);

    RecordLayout layout; // Tip_Formato
    layout.addGroup("com", commessa);
    layout.add("nom_cli", 30);
    layout.add("sca_lar", 5);
    layout.add("sca_lun", 5);
    layout.add("sca_tot_num", 6);
    layout.add("Sca_per_tag", 1);
    layout.add("met", 8);
    layout.add("Ord_liv", 1);
    layout.add("cut_to_mark", 1);
    layout.add("pat_cod", 16);
    layout.add("pat_con", 1);
    layout.add("fst_FTC", 1);
    layout.add("Ofs_Cut", 4);
    for (int index = 1; index <= 10; ++index)
        layout.add(QStringLiteral("L%1").arg(index), 3);
    layout.add("fin_pri", 4);
    layout.add("fin_dop", 4);
    layout.add("pos_pattern", 5);
    return layout;
}

RecordLayout reportLayout()
{
    RecordLayout level; // tip_rep_pro_liv
    level.add("fog_buo", 7);
    level.add("fog_err", 7);
    level.add("fog_sca", 7);
    level.add("mil_sca", 7);

    RecordLayout layout; // tip_rep_pro
    layout.add("ora_ini", 8);
    layout.add("dat_ini", 8);
    layout.add("ora_fin", 8);
    layout.add("dat_fin", 8);
    layout.add("met_tot", 8);
    layout.add("tem_fer", 8);
    layout.add("tem_mar", 8);
    layout.add("num_fer", 8);
    layout.add("vel_med", 3);
    layout.addGroup("liv", level, 3);
    return layout;
}

RecordLayout productionPeriodLayout()
{
    RecordLayout layout; // tip_pro_fosber21_FOS
    layout.add("dat_ini", 8);
    layout.add("ora_ini", 6);
    layout.add("dat_fin", 8);
    layout.add("ora_fin", 6);
    for (const char *name : {"tem_mar", "tem_fer", "vel_med", "num_fer", "met_lin", "met_qua", "num_cam"})
        layout.add(name, 9);
    return layout;
}

std::shared_ptr<const RecordLayout> checked(RecordLayout layout, int expectedSize)
{
    if (layout.size() != expectedSize)
        throw std::logic_error("record layout size mismatch");
    return std::make_shared<const RecordLayout>(std::move(layout));
}

} // namespace

std::shared_ptr<const RecordLayout> orderLayout()
{
    static const auto layout = [] {
        RecordLayout order; // Tip_Ordine
        order.add("cod_ord_abb", 11);
        order.add("spde_id", 4);
        order.add("spde_ModID", 2);
        order.add("stag_id", 4);
        order.add("mod_id", 2);
        order.add("old_stag_id", 4);
        order.add("old_mod_id", 2);
        order.addGroup("tip_car", ondaLayout());
        order.add("alt_bob", 5);
        order.add("met_tot_ord", 8);
        order.add("pre_all", 3);
        order.addGroup("ord", formatoLayout(), 3);
        order.addGroup("rep_pro", reportLayout());
        return checked(std::move(order), kOrderRecordSize);
    }();
    return layout;
}

std::shared_ptr<const RecordLayout> shiftProductionLayout()
{
    static const auto layout = [] {
        RecordLayout production; // tip_pro_tur_gio_fosber21_FOS
        production.addGroup("tur", productionPeriodLayout(), 3);
        production.addGroup("gio", productionPeriodLayout());
        return checked(std::move(production), kShiftProductionSize);
    }();
    return layout;
}

std::shared_ptr<const RecordLayout> orderProductionLayout()
{
    static const auto layout = [] {
        RecordLayout production; // tot_pro_ese
        production.add("tem_fer", 9);
        production.add("num_fer", 9);
        return checked(std::move(production), kOrderProductionSize);
    }();
    return layout;
}

OrderRecord::OrderRecord()
    : FixedRecord(orderLayout())
{
}

OrderRecord::OrderRecord(const QByteArray &raw)
    : FixedRecord(orderLayout(), raw)
{
}

bool OrderRecord::isUsed() const
{
    return number("stag_id") != 0;
}

} // namespace stag
