# Classic DAT generator

This standalone C++17 tool reads one CSV row per order, chooses compatible pairs, optimizes `outs`, and writes a 100-slot classic `.dat` file that the JavaScript classic parser can read.

## Build

The development container installs `g++` and Boost headers through its Dev Container compose override. Rebuild the Dev Container after pulling the configuration change, then run:

```sh
g++ -std=c++17 -O2 tools/classic_generator.cpp -o classic-generator
./classic-generator tools/orders.csv uploads/generated.dat
./classic-generator --self-test
```

Boost Graph is used for exact maximum-weight matching when pairing orders. The program also accepts an optional maximum trim in millimeters; the default is 230 mm, matching `orders.max_trim`.

## Input CSV

The header must contain these columns:

```csv
web_width_mm,quality_id,flute,order_number,customer_name,customer_code,sheet_width_mm,sheet_length_mm,quantity_sheets,sheets_per_stack,score1,score2,score3,score4,score5
```

Widths, lengths, and score positions are in millimeters with at most one decimal place. Blank score columns are treated as zero. `quality_id` must exist in the application's quality table before importing the generated file.

Orders can share a setup only when web width, quality ID, and flute match. The optimizer enumerates `outs` from 1 through 5, rejects layouts exceeding the web width or trim limit, minimizes total shared run length, then prefers the layout using more of the web. It globally pairs orders to minimize total paper consumed; an order remains single when pairing it would increase paper consumption.

When called through the Node Unix-socket client, the current `orders.max_trim` setting (in tenths of a millimeter) is sent with the CSV request. The standalone CLI uses the 230 mm default unless an explicit limit is supplied.

The DAT contains 100 records of 587 characters each. Four undocumented trailing fields are retained as blank/zero placeholders observed in the fixtures; the final 90 characters of each record are blank padding.
Each generated setup receives a random positive 32-bit `Fosber ID`, unique within that DAT file, because SyncroS parses this field as an integer key.

## Node Unix socket service

Compose runs the same binary as the `classic-generator` sidecar and shares `/run/syncrojs/classic-generator.sock` with the Node server. Uploading a `.csv` on the Current Orders import page sends it to this local socket; the returned classic DAT then enters the existing preview and confirmation flow. The socket is not exposed as a TCP port.

Each request is `<max-trim-tenths-mm>\n` followed by the UTF-8 CSV bytes; the client half-closes the write side to mark the request end. The response is one status byte, a four-byte big-endian payload length, then either the DAT bytes (status 0) or an UTF-8 error message (nonzero status).
