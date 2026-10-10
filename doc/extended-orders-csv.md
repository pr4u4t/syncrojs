# Extended orders CSV

Select the `extended_orders` parser in Settings, then upload a CSV from Current Orders > Import file. The legacy parser name `extended_outs` remains accepted as a configuration alias.

The file contains one row per order. Rows with the same `setup_code` are grouped into one setup; a setup can contain one to three orders. `web_width_mm`, `quality_id`, and `flute` must be identical on every row in that setup.

Required columns:

- `setup_code`
- `web_width_mm`
- `quality_id`
- `flute`
- `order_number`
- `customer_name`
- `sheet_width_mm`
- `outs`
- `sheet_length_mm`
- `quantity_sheets`
- `sheets_per_stack`
- `score_measures_mm`

`score_measures_mm` contains millimeter measurements separated by semicolons. Leave it empty when an order has no measures. Up to 22 measures are accepted across the setup; all 22 may be on one order. Use a decimal point and at most one decimal place. The optional `level` column accepts `1`, `2`, or `3`; when omitted, the first order defaults to level 3 and later orders to level 1.

See [example_extended_orders.csv](../tools/example_extended_orders.csv) for a three-order setup with 22 measures on the second order. Existing fixed-width 589-character DAT files remain supported, but their layout only carries five score fields per order.
