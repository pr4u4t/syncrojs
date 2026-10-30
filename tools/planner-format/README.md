# Syncro order file format – planner specification

This directory contains everything the production planner needs to generate order files for the Syncro application:

- this format description,
- [validate_orders.py](validate_orders.py) – a Python validator (3.8+, standard library only) that checks a file with the same rules as the server,
- [../example_extended_orders.csv](../example_extended_orders.csv) – a valid CSV example.

The planner can deliver orders in two ways. Pick whichever is more convenient – both carry the same data:

| | **CSV** (recommended) | **DAT 740** (fixed width) |
|---|---|---|
| Creases per order | up to 15 | up to 15 |
| Creases per setup | up to 22 | up to 22 |
| Outs per order | setup total ≤ 14 | setup total ≤ 14 |
| **Outs per setup (sum)** | **up to 14** | **up to 14** |
| Knife level | explicit `level` column | derived by the server from sheet lengths |
| Units | millimetres (`245.5`) | tenths of a millimetre (`2455`) |
| Sheets per stack | actual value, 1–1000 | actual value, 0–1000 (0 = machine default) |

In Syncro Settings the **`extended_orders`** parser must be selected – it reads CSV, DAT 740 and the legacy DAT 589.

---

## Terms

- **Setup** – one machine set-up: shared web width, quality and flute, 1–3 orders cut side by side.
- **Order** – one item in a setup: sheet width and length, number of outs, quantity, creases.
- **Creases (score measures)** – **the widths of the consecutive panels of the sheet between crease lines**, edge to edge, in the same unit as the sheet width. **The values add up to the sheet width.** Example: an 800 mm sheet → `100;150;300;150;100` (5 panels). A sheet without creases → empty (in legacy DAT files: a single field equal to the sheet width).

---

## 1. CSV

### File

- UTF-8 (BOM allowed), comma separated, the first line is the header.
- LF or CRLF line endings.
- Values containing a comma are quoted: `"Box, Inc."`; a quote inside is doubled: `""`.
- Column order is free, header names are case-insensitive. Unknown columns are ignored.
- **One row = one order.** Rows with the same `setup_code` form one setup (max. 3), in file order: order 1, 2, 3.

### Columns

| Column | Req. | Format | Notes |
|---|---|---|---|
| `setup_code` | yes | 1–27 characters `A-Z a-z 0-9 _ -`, optional `.NN` | Without a suffix the server appends `.2`. Must be unique in the Syncro queue. |
| `web_width_mm` | yes | mm, max. one decimal place | Identical on every row of the setup. |
| `quality_id` | yes | text | Must exist in the quality table. Identical within the setup. |
| `flute` | yes | text | Must exist in the flute table. Identical within the setup. |
| `order_number` | yes | up to 30 characters | |
| `customer_name` | yes | up to 15 characters | |
| `sheet_width_mm` | yes | mm, max. one decimal place | |
| `outs` | yes | positive integer | Sum within the setup ≤ 14. |
| `sheet_length_mm` | yes | mm, max. one decimal place | |
| `quantity_sheets` | yes | integer 1–999999 | `0` → the whole setup is skipped. |
| `sheets_per_stack` | yes | integer 1–1000 | Actual value (stacker limit). |
| `score_measures_mm` | yes (may be empty) | panel widths in mm separated by `;` | Sum = `sheet_width_mm`. See “Creases”. |
| `level` | no | `1`, `2` or `3` | Knife level. Default: order 1 → `3`, later orders → `1`. |

The decimal separator is a **dot**.

### Example

```csv
setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,sheet_length_mm,quantity_sheets,sheets_per_stack,level,score_measures_mm
SETUP-THREE,2500,Q1,A,ORDER-1,Klient Alfa,800,1,1000,10000,100,3,100;150;300;150;100
SETUP-THREE,2500,Q1,A,ORDER-2,Klient Beta,700,1,950,12000,100,1,45;45;45;45;45;45;45;45;45;45;45;45;45;45;70
SETUP-THREE,2500,Q1,A,ORDER-3,Klient Gamma,800,1,950,8000,80,1,400;400
```

---

## 2. DAT 740 (fixed width)

This is the existing MAC DAT record with **15 crease fields per order instead of 5**.

### General rules

- **One record = one setup, exactly 740 characters.** Records are concatenated without a separator (as in current DAT files) or separated by LF/CRLF.
- A record consisting only of spaces is skipped; trailing spaces at the end of the file are ignored.
- UTF-8 without BOM. Lengths are counted in **characters**, not bytes – the safest choice is plain ASCII (no accented letters in customer names).
- Text is left-aligned, numbers right-aligned, padded with spaces.
- Dimensions are in **tenths of a millimetre**: `25000` = 2500.0 mm.
- An empty order 2 or 3 is a block of spaces. An order exists when it has an order number.
- An empty crease field is spaces or `0`. **One value per field.**
- No creases: all fields empty (or, as in legacy DAT files, only the first field equal to the sheet width).

### Record layout

Positions are 0-based.

**Header (0–141)**

| Pos. | Len. | Field | Example |
|---|---|---|---|
| 0 | 11 | Program number (informational) | `824805` |
| 11 | 44 | Scheduled papers (informational) | `f80f80f80` |
| 55 | 12 | Fosber ID → setup code (`4971` → `4971.2`). **Digits only.** | `4971` |
| 67 | 36 | Quality ID | `FALA A` |
| 103 | 23 | Flute ID | `A` |
| 126 | 5 | Web width (0.1 mm) | `25000` |
| 131 | 10 | Linear meters (informational) | |
| 141 | 1 | Constant `G` (**mandatory**) | `G` |

**Order block – 164 characters, three times: 142–305, 306–469, 470–633**

| Pos. in block | Len. | Field |
|---|---|---|
| 0 | 15 | Customer name |
| 15 | 12 | Customer code (informational) |
| 27 | 30 | Order number (empty = no order) |
| 57 | 5 | Sheet width (0.1 mm) |
| 62 | 75 | Creases 1–15: panel widths, 15 × 5 characters (0.1 mm, e.g. `1950` = 195.0 mm); field *n* starts at `62 + 5·(n−1)` |
| 137 | 4 | Sheets per stack, actual value (0–1000, 0 = machine default) |
| 141 | 5 | Sheet length (0.1 mm) |
| 146 | 6 | Quantity of sheets |
| 152 | 2 | Outs (setup total ≤ 14) |
| 154 | 8 | Linear meters (informational) |
| 162 | 2 | Fixed field: `20` for order 1, `10` for orders 2 and 3 (**always write `20` in order 1**) |

**End of record (634–739)**

| Pos. | Len. | Field |
|---|---|---|
| 634 | 8 | Reserved (spaces) |
| 642 | 8 | Reserved (spaces) |
| 650 | 90 | Space padding |

### Knife level in DAT

DAT has no level field. The server derives it from sheet lengths:

- 1 or 2 orders: order 1 → level 3, order 2 → level 1,
- 3 orders with the same length → all on level 3,
- 3 orders with two different lengths → the first length group on level 3, the other on level 1,
- 3 orders with three different lengths → **error** (the machine cannot cut three independent lengths).

If the planner needs a different level assignment, use CSV with the `level` column.

### Easiest way to a valid DAT

The validator can convert a valid CSV into DAT 740 – it is also the reference implementation of the writer:

```sh
python3 validate_orders.py orders.csv --write-dat740 orders.dat
```

---

## 3. Validation rules (CSV and DAT)

The server rejects a setup (red in the preview) when:

1. **Creases:** max. **15 per order** and max. **22 per setup**; every panel width positive and not wider than the order's sheet. The panels should add up to the sheet width – the server does not enforce this, the validator reports a warning.
2. **Outs:** the sum of outs of all orders in the setup ≤ **14**, e.g. 6 + 8 or 5 + 5 + 4 is allowed, 5 + 5 + 5 = 15 is not; there is no separate limit per order.
3. **Widths:** web ≤ **2543 mm**; sum of `sheet width × outs` ≥ **1000 mm** and ≤ web width.
4. **Trim** (web − used width): **16–230 mm**.
5. **Cut length** per knife level: min. **500 mm**, max. **4900 / 3500 / 5150 mm** for levels 1 / 2 / 3.
6. Orders on the **same knife level must have the same sheet length**.
7. **Minimum setup length:** `sheet length [mm] × ceil(quantity / outs)` (computed for order 1, aligned to the longest order) ≥ **29380 mm**.
8. Quality and flute must exist in the Syncro database.
9. Setup code already used in the Syncro queue by **different** orders → error. The same setup with the same orders → skipped (yellow).

Warnings (yellow, the setup is skipped, the rest of the import continues):

- an order with quantity `0` – the whole setup is skipped,
- a setup repeated in the file or already present in the Syncro queue.

Limits 3–7 are configurable on the server (`orders.validation` in the application configuration); the values above are the defaults.

---

## 4. Validator

Requires only Python 3.8+.

```sh
python3 validate_orders.py orders.csv
python3 validate_orders.py orders.dat                  # format detected automatically
python3 validate_orders.py orders.dat --format dat740  # or forced: csv, dat587, dat589, dat740
python3 validate_orders.py orders.csv --qualities "FALA A,FALA B" --flutes A,B,C
python3 validate_orders.py orders.csv --qualities @qualities.txt --flutes @flutes.txt
python3 validate_orders.py orders.csv --json           # machine-readable result
python3 validate_orders.py orders.csv --strict         # warnings also fail
python3 validate_orders.py orders.csv --limits limits.json
```

Exit code: `0` – file valid (warnings possible), `1` – validation errors, `2` – file cannot be read.

Sample output (validator messages are in Polish):

```
ERROR   wiersz 2, setup S1: Zamówienie 1 zawiera 16 bigów; limit to 15 na zamówienie.
ERROR   wiersz 2, setup S1: Setup zawiera 24 bigów; limit to 22.
WARNING wiersz 4, setup S2: Zamówienia O3 mają 0 sztuk. Aplikacja pominie cały setup.
Format: csv, setupów: 2, błędów: 2, ostrzeżeń: 1
```

`--limits` takes a JSON file with keys as in `DEFAULT_LIMITS` at the top of the script, e.g. when the machine has a different configuration:

```json
{ "max_trim_tenths_mm": 2500, "knife_level_count": 2 }
```

The validator checks everything that can be checked without access to Syncro. It does not know the order queue on the machine or the quality and flute tables (unless `--qualities` / `--flutes` are given), so a file that passes the validator can still be stopped by the server for those reasons.
