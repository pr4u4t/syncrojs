# Planner wide CSV – one row per setup

Specification and validator for the CSV layout proposed by the planner: **one row = one setup** with up to three order blocks `z1_`, `z2_`, `z3_`. This directory is self-contained:

| File | Purpose |
|---|---|
| `README.md` | this specification |
| [validate_wide_csv.py](validate_wide_csv.py) | validator, Python 3.8+, standard library only |
| [example_as_received.csv](example_as_received.csv) | the sample exactly as received from the planner (fails validation, see below) |
| [example_valid.csv](example_valid.csv) | the same setup corrected (passes) |

> The Syncro application imports this layout automatically with the `extended_orders` parser (Current Orders > Import file), whatever parser is selected in Settings; it is recognised by the `program_number_drcorr` header. The Ozdan export `OrdList.csv` is the reference file ([tests/models/ozdan_data](../../tests/models/ozdan_data/OrdList.csv)); the same orders also arrive as a classic 587-character `Ord_mac.dat`, and both produce the same Syncro setup.

---

## File

- Text file, **semicolon** (`;`) separated, first line is the header. UTF-8 (BOM allowed); Windows-1250 is accepted with a warning.
- Header names are case-insensitive; column order is free. Every row has the same number of columns as the header; one extra empty column at the end of a data row (a trailing `;`, as in the Ozdan export) is ignored.
- All numbers are **whole numbers**. Widths, lengths and creases are in **tenths of a millimetre** (`24500` = 2450.0 mm). No decimal separators.
- One row per setup. An unused order block (`z2_`, `z3_`) has an empty order number; its other fields may be empty or `0`.

## Setup columns

| Column | Required | Format | Meaning |
|---|---|---|---|
| `program_number_drcorr` | yes | text | Program number (informational). |
| `paper_1` … `paper_5` | yes (may be empty) | text, e.g. `f80`, `t110` | Paper recipe, filled from `paper_1` without gaps. Single flute (A, B, C, E) uses 3 papers, double flute (AB, BC…) 5. |
| `FOSBEROID` | yes | 1–12 digits | Setup ID; becomes the Syncro setup code `<id>.2`. Unique in the file and in the Syncro queue. |
| `Quality` | yes | text | Must exist in the Syncro quality table. |
| `flute` | yes | text | Flute type (`A`, `B`, `AB`…). Must exist in the Syncro flute table. |
| `web_width` | yes | 0.1 mm | Web width. |
| `order_length_m` | yes | metres | Linear metres of the setup (informational; the validator warns when it differs from the orders by more than 2 %). |

## Order columns (`N` = 1, 2, 3)

| Column | Required | Format | Meaning |
|---|---|---|---|
| `zN_customer_name` | yes | up to 15 characters | Customer name. |
| `zN_customer_number_drcorr` | no | text | Customer code (informational). |
| `zN_order_number_optima` | yes | up to 30 characters | Order number. **Empty = no order in this block.** |
| `zN_width` | yes | 0.1 mm | Sheet width. |
| `zN_score_1` … `zN_score_17` | yes (may be `0`) | 0.1 mm | Crease panels, see below. |
| `zN_length` | yes | 0.1 mm | Sheet (cut) length. |
| `zN_pieces` | yes | 1–999999 | Quantity of sheets. `0` → the whole setup is skipped. |
| `zN_out_box` | yes | positive integer | Outs (number of sheets across the web); all orders of the setup together at most 14. |
| `zN_level` | no | `1`, `2`, `3`, or `20` / `10` | Knife level, see below. |
| `zN_sheets_per_stack` | yes | 1–1000 | Sheets per stack, sent to Syncro unchanged (same as for `Ord_mac.dat`). The stacker takes about 1400 sheets of a low flute and 1000 of a high one, so the limit is 1000. |

## Creases

`zN_score_1 … zN_score_17` are the **widths of consecutive panels between crease lines**, edge to edge, in **0.1 mm** – the same unit as `zN_width`. They add up to the sheet width.

- Fill from `zN_score_1` without gaps; unused fields are `0` or empty.
- No creases: all fields `0` – or a single panel equal to the sheet width.
- Example, sheet `10000` (1000.0 mm) with four panels: `2000;3000;3000;2000;0;0;…`.

The layout has 17 columns per order (51 per setup), but **our limits apply**:

| Limit | Value |
|---|---|
| Creases per order | **max. 15** |
| Creases per setup (z1 + z2 + z3) | **max. 22** |

## Knife level

- `zN_level` set → used as given (`1`, `2`, `3`; level 2 only on machines with three knife levels).
- `zN_level` `20` / `10` → the DAT fixed field (order 1 / orders 2–3) copied by the Ozdan export; treated like an empty level.
- `zN_level` empty → the level of another order in the setup with **the same length**; if there is none, order 1 → `3`, orders 2 and 3 → `1`.
- Orders on the same knife level must have the same length.

## Validation rules (our limits)

A setup is rejected when:

1. **Creases:** more than 15 per order or more than 22 per setup; a panel wider than the sheet.
2. **Outs:** `zN_out_box` below 1, or the setup total above **14** (6 + 8 or 5 + 5 + 4 is fine, 5 + 5 + 5 is not).
3. **Widths:** web above **2543 mm**; used width (`width × out_box` summed) below **1000 mm** or above the web.
4. **Trim** (web − used width) outside **16–230 mm**.
5. **Cut length** below **500 mm** or above **4900 / 3500 / 5150 mm** on knife levels 1 / 2 / 3; different lengths on one knife level.
6. **Setup length** (`length × ceil(pieces / out_box)` for order 1, aligned to the longest order) below **29380 mm**.
7. Order 1 missing, or order 3 used without order 2.
8. `FOSBEROID` not 1–12 digits, or repeated in the file with different orders.
9. Creases written in the wrong unit (sum = 10 × sheet width).

Warnings (the file is still valid): an order with `0` pieces (the setup is skipped), crease panels not adding up to the sheet width, gaps in score or paper columns, paper count not matching the flute, `order_length_m` not matching the orders, missing `zN_sheets_per_stack`, suspicious customer names (see below).

Limits 3–6 are server settings (`orders.validation`); the values above are the defaults. Override them in the validator with `--limits limits.json`.

## Problems in the received sample

[example_as_received.csv](example_as_received.csv) fails for reasons the planner export must fix:

| Problem | Example | Fix |
|---|---|---|
| Creases in 0.01 mm, ten times the width | `z1_width` `10000`, `z1_score_1` `100000` | Write creases in 0.1 mm like the width: `10000` (or the real panels). |
| Customer names shifted by one character | `GPOLMLEKART`, `0COMFORTPRINt`, `0HAK-POL` | The export cuts the DAT record one position too early: the `G` constant and the previous field's last digit leak in. Correct names: `POLMLEKART`, `COMFORTPRINT`, `HAK-POL`. |
| No sheets per stack | – | Add `zN_sheets_per_stack`. |
| `z2_level` empty | order 2 has the same length as order 1 (level 2) | Allowed – derived as level 2 – but better to write it explicitly. |

[example_valid.csv](example_valid.csv) is the same setup with these corrections.

## Validator

```sh
python3 validate_wide_csv.py orders.csv
python3 validate_wide_csv.py orders.csv --qualities "FALA A,FALA B" --flutes A,B,AB
python3 validate_wide_csv.py orders.csv --qualities @qualities.txt --flutes @flutes.txt
python3 validate_wide_csv.py orders.csv --json        # machine-readable result
python3 validate_wide_csv.py orders.csv --strict      # warnings also fail
python3 validate_wide_csv.py orders.csv --limits limits.json
```

Exit code: `0` valid (warnings possible), `1` validation errors, `2` file unreadable or header wrong.

Output for the received sample:

```
WARNING header: No zN_sheets_per_stack columns: Syncro needs sheets per stack, the machine default will be used.
WARNING row 2, setup 5094: Order 1 customer 'GPOLMLEKART' starts with 'G' - looks like the DAT constant 'G' leaked into the name (field shifted by one).
ERROR   row 2, setup 5094: Order 1: creases add up to 100000 = 10 x width 10000 - they look like 0.01 mm; write them in 0.1 mm like the width.
...
Setups: 1, errors: 3, warnings: 4
```

The validator cannot see the Syncro order queue or the quality and flute tables (unless `--qualities` / `--flutes` are given), so the server may still stop a file that passes here for those reasons.
