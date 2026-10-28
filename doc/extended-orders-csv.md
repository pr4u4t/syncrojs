# Extended orders CSV

Select the `extended_orders` parser in Settings, then upload a CSV from Current Orders > Import file. The legacy parser name `extended_outs` remains accepted as a configuration alias.

The format exists because the fixed-width DAT layout carries only five crease (score) fields per order. The CSV carries up to **15 crease measurements per order** while keeping the Syncro limit of **22 crease measurements per setup** (all orders in the set combined).

## File structure

- UTF-8 text, comma separated, first line is the header. A UTF-8 BOM is ignored.
- One row per order. Rows with the same `setup_code` are grouped into one setup; a setup contains one to three orders, in file order (order 1, 2, 3).
- `web_width_mm`, `quality_id`, and `flute` must be identical on every row of a setup.
- Values containing commas must be quoted (`"Box, Inc."`); a quote inside a quoted value is written as `""`.
- Column order does not matter; header names are case-insensitive. Every row must have the same number of columns as the header.

## Columns

| Column | Required | Format | Notes |
|---|---|---|---|
| `setup_code` | yes | 1–27 letters, digits, `_` or `-`, optional `.NN` suffix | Suffix defaults to `.2` (`4971` → `4971.2`). Must not already exist in the Syncro queue. |
| `web_width_mm` | yes | mm, max one decimal | Shared by the setup. |
| `quality_id` | yes | text | Must exist in the quality table. |
| `flute` | yes | text | Must exist in the flute table. |
| `order_number` | yes | max 30 characters | |
| `customer_name` | yes | max 15 characters | |
| `sheet_width_mm` | yes | mm, max one decimal | |
| `outs` | yes | integer 1–5 | Total outs across the setup are limited to 14. |
| `sheet_length_mm` | yes | mm, max one decimal | Orders on the same knife level must share the length. |
| `quantity_sheets` | yes | positive integer | |
| `sheets_per_stack` | yes | positive integer | |
| `score_measures_mm` | yes (may be empty) | mm values separated by `;` | See crease limits below. |
| `level` | no | `1`, `2` or `3` | Knife level. When omitted: order 1 → `3`, later orders → `1`. |

## Crease measurements

`score_measures_mm` holds the **widths of the consecutive panels between crease lines**, edge to edge, in millimeters, separated by semicolons, with a decimal point and at most one decimal place. The values add up to the sheet width: an 800 mm sheet split into five panels is `100;150;300;150;100`.

- Leave the cell empty when the order has no creases.
- Values must be positive and may not exceed the sheet width of that order. Their sum should equal the sheet width (not enforced by the server; the planner validator warns).
- Repeated values are kept as entered.
- **At most 15 measurements per order.**
- **At most 22 measurements per setup**, counting all its orders together.

| Order 1 | Order 2 | Order 3 | Total | Result |
|---|---|---|---|---|
| 15 | 7 | 0 | 22 | accepted |
| 5 | 15 | 2 | 22 | accepted |
| 15 | 15 | 0 | 30 | rejected: setup above 22 |
| 16 | 0 | 0 | 16 | rejected: order above 15 |

A violation marks the setup as invalid (red) in the import preview with the reason. The import cannot be accepted until the file is corrected or the setup is removed. Manual setup entry applies the same limits.

## Example

[example_extended_orders.csv](../tools/example_extended_orders.csv) contains one three-order setup with 5 + 15 + 2 = 22 measurements:

```csv
setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,sheet_length_mm,quantity_sheets,sheets_per_stack,level,score_measures_mm
SETUP-THREE,2500,Q1,A,ORDER-1,Klient Alfa,800,1,1000,10000,100,3,100;150;300;150;100
SETUP-THREE,2500,Q1,A,ORDER-2,Klient Beta,700,1,950,12000,100,1,45;45;45;45;45;45;45;45;45;45;45;45;45;45;70
SETUP-THREE,2500,Q1,A,ORDER-3,Klient Gamma,800,1,950,8000,80,1,400;400
```

## Import behaviour

- The preview shows one row per setup with its order numbers and total crease count.
- A setup containing any order with quantity `0` is skipped (yellow).
- A setup already present in the Syncro queue with the same orders, or repeated in the file, is skipped (yellow). The same setup code with different orders is an error (red).
- Existing fixed-width 589-character DAT files remain supported, but their layout only carries five score fields per order.

## Extended fixed-width DAT (740 characters)

> Read by the `extended_orders` parser next to CSV and the 589-character DAT. The planner-facing specification and a Python validator are in [tools/planner-format](../tools/planner-format/README.md). It is the existing DAT record with 15 score fields per order instead of 5; the 15 values are packed into the first score field of the internal 55-field row, so preview, score editing and validation work unchanged.

### Conventions

- One setup per record; records follow each other **without line breaks**, exactly like the current DAT. Fully blank records are ignored.
- Record length: **740 characters** (650 data + 90 blank padding). Trailing blanks after the last record are ignored. With the `extended_orders` parser selected, the file is read as 740 when its records have `G` at position 141 and do not fit the 589 layout; for a single record that fits both, the fixed field `20` of order 1 (position 304) decides, so always write it.
- Text fields are left-aligned and padded with spaces. Numeric fields are right-aligned and padded with spaces.
- Widths, lengths and crease panel widths are in **tenths of a millimetre** (`22000` = 2200.0 mm), as in the current DAT.
- An unused order block is entirely blank. An unused score field is blank or `0`. One value per score field — no packing of several values into one field.

### Record layout

Offsets are 0-based character positions.

**Header (0–141, 142 characters)**

| Offset | Length | Field | Example |
|---|---|---|---|
| 0 | 11 | Program number | `824805` |
| 11 | 44 | Scheduled papers | `f80f80f80` |
| 55 | 12 | Fosber ID → setup code (`4971` → `4971.2`) | `4971` |
| 67 | 36 | Quality ID | `FALA A` |
| 103 | 23 | Flute ID | `A` |
| 126 | 5 | Web width (0.1 mm) | `22000` |
| 131 | 10 | Linear meters | `0` |
| 141 | 1 | Constant `G` | `G` |

**Order block (164 characters, repeated three times)**

| Relative offset | Length | Field |
|---|---|---|
| 0 | 15 | Customer name |
| 15 | 12 | Customer code |
| 27 | 30 | Order number (blank = no order) |
| 57 | 5 | Sheet width (0.1 mm) |
| 62 | 75 | Scores 1–15, 15 × 5 characters (0.1 mm); score *n* at `62 + 5·(n−1)` |
| 137 | 4 | Sheets per stack (same meaning as current DAT) |
| 141 | 5 | Sheet length (0.1 mm) |
| 146 | 6 | Quantity of sheets |
| 152 | 2 | Outs (1–5) |
| 154 | 8 | Linear meters |
| 162 | 2 | Fixed field (`20` for order 1, `10` for orders 2–3, as in current DAT) |

| Block | Offsets |
|---|---|
| Order 1 | 142–305 |
| Order 2 | 306–469 |
| Order 3 | 470–633 |

**Tail (634–739)**

| Offset | Length | Field |
|---|---|---|
| 634 | 8 | Reserved (kept unchanged) |
| 642 | 8 | Reserved (kept unchanged) |
| 650 | 90 | Blank padding |

### Differences from the current DAT

| | Classic DAT | Extended orders DAT (589) | Extended DAT (740) |
|---|---|---|---|
| Score fields per order | 5 | 5 | **15** |
| Outs field | 1 char | 2 chars (orders 1–2) | 2 chars (all orders) |
| Order block | 113 | 114 / 114 / 113 | 164 / 164 / 164 |
| Record length | 587 | 589 | **740** |

### Validation

Identical to the CSV: at most **15 creases per order**, at most **22 per setup**, each crease positive and not wider than the order's sheet width; total outs ≤ 14; orders on the same knife level share a sheet length. Records breaking a rule are shown red in the preview with the reason.

The setup from the CSV example above would occupy one 740-character record: order 1 uses score fields 1–5 (`1000`, `1500`, `3000`, `1500`, `1000`), order 2 all 15 fields (14 × `450` and `700`), order 3 fields 1–2 (`4000`, `4000`), all other score fields blank.
