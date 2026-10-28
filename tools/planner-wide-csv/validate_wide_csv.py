#!/usr/bin/env python3
"""Validate the planner's wide CSV for the Syncro order import.

One row = one setup with up to three order blocks (z1_, z2_, z3_), semicolon separated.
The machine rules mirror models/order-validation.js of the Syncro web application.
Only the Python 3.8+ standard library is used.

Exit status: 0 valid (warnings allowed), 1 validation errors, 2 unreadable input.
"""

import argparse
import csv
import io
import json
import math
import sys
from typing import Dict, List, Optional

# Lengths in tenths of a millimetre unless the name says otherwise.
DEFAULT_LIMITS = {
    "max_creases_per_order": 15,
    "max_creases_per_setup": 22,
    "max_outs_per_order": 5,
    "max_total_outs": 14,
    "knife_level_count": 3,
    "min_cut_length_tenths_mm": 5000,
    "max_cut_length_tenths_mm": [49000, 35000, 51500],
    "min_setup_length_mm": 29380,
    "max_web_width_tenths_mm": 25430,
    "min_setup_width_tenths_mm": 10000,
    "min_trim_tenths_mm": 160,
    "max_trim_tenths_mm": 2300,
    "allow_narrow_trim": False,
    "max_quantity": 999999,
    "max_sheets_per_stack": 9999,
    "order_length_tolerance_percent": 2,
}

SCORE_COLUMNS = 17
PAPER_COLUMNS = 5
SETUP_COLUMNS = (["program_number_drcorr"] + ["paper_%d" % i for i in range(1, PAPER_COLUMNS + 1)]
                 + ["fosberoid", "quality", "flute", "web_width", "order_length_m"])
ORDER_COLUMNS = (["customer_name", "customer_number_drcorr", "order_number_optima", "width"]
                 + ["score_%d" % i for i in range(1, SCORE_COLUMNS + 1)]
                 + ["length", "pieces", "out_box", "level"])
OPTIONAL_ORDER_COLUMNS = ["sheets_per_stack"]
REQUIRED_COLUMNS = SETUP_COLUMNS + ["z%d_%s" % (n, c) for n in (1, 2, 3) for c in ORDER_COLUMNS]


class Report:
    def __init__(self) -> None:
        self.issues: List[dict] = []

    def add(self, severity: str, location: str, message: str) -> None:
        self.issues.append({"severity": severity, "location": location, "message": message})

    def count(self, severity: str) -> int:
        return sum(1 for issue in self.issues if issue["severity"] == severity)


def mm(tenths: int) -> str:
    return ("%.1f" % (tenths / 10)).rstrip("0").rstrip(".") + " mm"


def to_int(text: str) -> Optional[int]:
    """Empty -> None; anything that is not a whole number raises ValueError."""
    text = (text or "").strip()
    if not text:
        return None
    if not text.lstrip("-").isdigit():
        raise ValueError("'%s' is not a whole number" % text)
    return int(text)


def physical_level(level: int, level_count: int) -> Optional[int]:
    if level < 1 or level > 3:
        return None
    if level_count == 1:
        return 1
    if level_count == 2:
        return None if level == 2 else (1 if level == 1 else 2)
    return level


def level_limit(limits: dict, name: str, level: int) -> int:
    value = limits[name]
    return int(value[level - 1]) if isinstance(value, list) else int(value)


def expected_paper_count(flute: str) -> Optional[int]:
    letters = [c for c in flute.upper() if c.isalpha()]
    return 2 * len(letters) + 1 if 1 <= len(letters) <= 3 else None


def read_rows(raw: bytes, report: Report) -> List[Dict[str, str]]:
    try:
        text = raw.decode("utf-8-sig")
    except UnicodeDecodeError:
        text = raw.decode("cp1250")
        report.add("WARNING", "file", "File is not UTF-8; read as Windows-1250. Please export UTF-8.")

    rows = [row for row in csv.reader(io.StringIO(text), delimiter=";") if any(cell.strip() for cell in row)]
    if len(rows) < 2:
        report.add("ERROR", "file", "The file has no data rows.")
        return []

    headers = [header.strip().lower() for header in rows[0]]
    duplicates = sorted({h for h in headers if headers.count(h) > 1})
    missing = [name for name in REQUIRED_COLUMNS if name not in headers]
    if duplicates:
        report.add("ERROR", "header", "Duplicate columns: %s" % ", ".join(duplicates))
    if missing:
        report.add("ERROR", "header", "Missing columns: %s" % ", ".join(missing))
    if duplicates or missing:
        return []
    known = set(REQUIRED_COLUMNS) | {"z%d_%s" % (n, c) for n in (1, 2, 3) for c in OPTIONAL_ORDER_COLUMNS}
    unknown = [h for h in headers if h not in known]
    if unknown:
        report.add("WARNING", "header", "Unknown columns are ignored: %s" % ", ".join(unknown))
    if not any(h.endswith("_sheets_per_stack") for h in headers):
        report.add("WARNING", "header", "No zN_sheets_per_stack columns: Syncro needs sheets per stack, the machine default will be used.")

    result = []
    for line, values in enumerate(rows[1:], start=2):
        if len(values) != len(headers):
            report.add("ERROR", "row %d" % line, "Row has %d columns, header has %d." % (len(values), len(headers)))
            continue
        row = {header: value.strip() for header, value in zip(headers, values)}
        row["__line__"] = str(line)
        result.append(row)
    return result


def parse_setup(row: Dict[str, str], limits: dict, report: Report) -> Optional[dict]:
    where = "row %s, setup %s" % (row["__line__"], row["fosberoid"] or "?")
    error = lambda message: report.add("ERROR", where, message)
    warning = lambda message: report.add("WARNING", where, message)

    def number(column: str, label: str) -> Optional[int]:
        try:
            return to_int(row[column])
        except ValueError as problem:
            error("%s: %s" % (label, problem))
            return None

    setup = {"where": where, "code": row["fosberoid"], "quality": row["quality"], "flute": row["flute"], "orders": []}
    if not setup["code"].isdigit() or len(setup["code"]) > 12:
        error("FOSBEROID must be 1-12 digits (it becomes the Syncro setup code %s.2)." % (setup["code"] or "?"))
    if not setup["quality"] or not setup["flute"]:
        error("Quality and flute are required.")

    papers = [row["paper_%d" % i] for i in range(1, PAPER_COLUMNS + 1)]
    used_papers = [paper for paper in papers if paper]
    if any(not paper for paper in papers[:len(used_papers)]):
        warning("Paper columns have gaps; fill paper_1, paper_2, ... without skipping.")
    expected = expected_paper_count(setup["flute"])
    if expected and len(used_papers) != expected:
        warning("Flute %s normally uses %d papers, the row has %d." % (setup["flute"], expected, len(used_papers)))

    setup["web_width"] = number("web_width", "web_width")
    setup["order_length_m"] = number("order_length_m", "order_length_m")

    for n in (1, 2, 3):
        prefix = "z%d_" % n
        label = "Order %d" % n
        block = {c: row.get(prefix + c, "") for c in ORDER_COLUMNS + OPTIONAL_ORDER_COLUMNS}
        if not block["order_number_optima"]:
            if any(value not in ("", "0") for value in block.values()):
                warning("%s has no order number but other values; it is ignored." % label)
            continue

        order = {"n": n, "number": block["order_number_optima"], "customer": block["customer_name"]}
        if n == 1 and order["customer"][:1] == "G" and order["customer"][1:2].isupper():
            warning("%s customer '%s' starts with 'G' - looks like the DAT constant 'G' leaked into the name (field shifted by one)." % (label, order["customer"]))
        if n > 1 and order["customer"][:1] == "0":
            warning("%s customer '%s' starts with '0' - looks like the previous field leaked into the name (field shifted by one)." % (label, order["customer"]))
        if not order["customer"]:
            error("%s: customer_name is empty." % label)
        elif len(order["customer"]) > 15:
            error("%s: customer_name is longer than 15 characters." % label)
        if len(order["number"]) > 30:
            error("%s: order_number_optima is longer than 30 characters." % label)

        for key, column in (("width", "width"), ("length", "length"), ("pieces", "pieces"),
                            ("outs", "out_box"), ("level", "level"), ("stack", "sheets_per_stack")):
            try:
                order[key] = to_int(block[column])
            except ValueError as problem:
                error("%s %s: %s" % (label, prefix + column, problem))
                order[key] = None

        scores, seen_gap = [], False
        for index in range(1, SCORE_COLUMNS + 1):
            try:
                value = to_int(block["score_%d" % index])
            except ValueError as problem:
                error("%s score_%d: %s" % (label, index, problem))
                continue
            if not value:
                seen_gap = True
                continue
            if seen_gap:
                warning("%s: score_%d follows an empty score field; fill scores from score_1 without gaps." % (label, index))
                seen_gap = False
            scores.append(value)
        width = order["width"]
        if width and scores and sum(scores) == width * 10:
            error("%s: creases add up to %d = 10 x width %d - they look like 0.01 mm; write them in 0.1 mm like the width." % (label, sum(scores), width))
            scores = []
        elif width and len(scores) == 1 and scores[0] == width:
            scores = []  # a single panel equal to the sheet width means "no creases"
        order["scores"] = scores
        setup["orders"].append(order)
    return setup


def resolve_levels(setup: dict) -> None:
    """Empty level: copy the level of an order with the same length, otherwise order 1 -> 3, later -> 1."""
    for order in setup["orders"]:
        if order["level"] is not None:
            continue
        twin = next((other for other in setup["orders"] if other is not order and other["level"] is not None
                     and other["length"] == order["length"]), None)
        order["level"] = twin["level"] if twin else (3 if order["n"] == 1 else 1)
        order["level_derived"] = True


def validate_setup(setup: dict, limits: dict, report: Report, qualities: Optional[set], flutes: Optional[set]) -> None:
    where = setup["where"]
    error = lambda message: report.add("ERROR", where, message)
    warning = lambda message: report.add("WARNING", where, message)

    if qualities is not None and setup["quality"] not in qualities:
        error("Quality '%s' does not exist in Syncro." % setup["quality"])
    if flutes is not None and setup["flute"] not in flutes:
        error("Flute '%s' does not exist in Syncro." % setup["flute"])

    orders = setup["orders"]
    numbers = [order["n"] for order in orders]
    if 1 not in numbers:
        error("The setup must contain order 1 (z1_).")
        return
    if 3 in numbers and 2 not in numbers:
        error("Order 3 cannot be used without order 2.")

    zero = [order["number"] for order in orders if order["pieces"] == 0]
    if zero:
        warning("Orders %s have 0 pieces; the application skips the whole setup." % ", ".join(zero))
        return

    web = setup["web_width"]
    if web is None or web <= 0:
        error("web_width must be positive.")
    elif web > limits["max_web_width_tenths_mm"]:
        error("web_width %s exceeds the maximum %s." % (mm(web), mm(limits["max_web_width_tenths_mm"])))

    complete = web is not None and web > 0
    total_creases = 0
    for order in orders:
        label = "Order %d" % order["n"]
        if order["width"] is None or order["width"] <= 0:
            error("%s: width must be positive." % label)
        if order["length"] is None or order["length"] <= 0:
            error("%s: length must be positive." % label)
        if order["pieces"] is None or not 1 <= order["pieces"] <= limits["max_quantity"]:
            error("%s: pieces must be 1-%d." % (label, limits["max_quantity"]))
        if order["outs"] is None or not 1 <= order["outs"] <= limits["max_outs_per_order"]:
            error("%s: out_box must be 1-%d." % (label, limits["max_outs_per_order"]))
        if order["stack"] is not None and not 1 <= order["stack"] <= limits["max_sheets_per_stack"]:
            error("%s: sheets_per_stack must be 1-%d." % (label, limits["max_sheets_per_stack"]))
        if physical_level(order["level"], int(limits["knife_level_count"])) is None:
            error("%s: knife level %s is not available on this machine." % (label, order["level"]))
        if None in (order["width"], order["length"], order["pieces"], order["outs"]):
            complete = False

        if len(order["scores"]) > limits["max_creases_per_order"]:
            error("%s has %d creases; the limit is %d per order." % (label, len(order["scores"]), limits["max_creases_per_order"]))
        for index, score in enumerate(order["scores"], start=1):
            if order["width"] and score > order["width"]:
                error("%s: crease panel %d (%s) is wider than the sheet (%s)." % (label, index, mm(score), mm(order["width"])))
        if order["scores"] and order["width"] and sum(order["scores"]) != order["width"]:
            warning("%s: crease panels add up to %s, the sheet is %s." % (label, mm(sum(order["scores"])), mm(order["width"])))
        total_creases += len(order["scores"])

    if total_creases > limits["max_creases_per_setup"]:
        error("The setup has %d creases; the limit is %d." % (total_creases, limits["max_creases_per_setup"]))
    if not complete:
        return

    outs = sum(order["outs"] for order in orders)
    if outs > limits["max_total_outs"]:
        error("Total out_box is %d; the limit is %d." % (outs, limits["max_total_outs"]))

    lengths_by_level: Dict[int, int] = {}
    for order in orders:
        level = physical_level(order["level"], int(limits["knife_level_count"]))
        if level is None:
            continue
        minimum = level_limit(limits, "min_cut_length_tenths_mm", level)
        maximum = level_limit(limits, "max_cut_length_tenths_mm", level)
        if order["length"] < minimum:
            error("Order %d: length %s is below the minimum %s." % (order["n"], mm(order["length"]), mm(minimum)))
        if order["length"] > maximum:
            error("Order %d: length %s exceeds the maximum %s for knife level %d." % (order["n"], mm(order["length"]), mm(maximum), level))
        if level in lengths_by_level and lengths_by_level[level] != order["length"]:
            error("Orders on knife level %d must have the same length." % level)
        lengths_by_level.setdefault(level, order["length"])

    used = sum(order["width"] * order["outs"] for order in orders)
    if used < limits["min_setup_width_tenths_mm"]:
        error("Used width %s is below the minimum %s." % (mm(used), mm(limits["min_setup_width_tenths_mm"])))
    if used > web:
        error("Used width %s exceeds web_width %s." % (mm(used), mm(web)))
    else:
        trim = web - used
        if trim > limits["max_trim_tenths_mm"]:
            error("Trim %s exceeds the maximum %s." % (mm(trim), mm(limits["max_trim_tenths_mm"])))
        if trim < limits["min_trim_tenths_mm"] and not limits["allow_narrow_trim"]:
            error("Trim %s is below the minimum %s." % (mm(trim), mm(limits["min_trim_tenths_mm"])))

    totals = [(order["length"] // 10) * math.ceil(order["pieces"] / order["outs"]) for order in orders]
    longest = max(totals)
    first_total = totals[0]
    if abs(first_total - longest) >= orders[0]["length"]:
        first_total = (orders[0]["length"] // 10) * (longest * 10 // orders[0]["length"])
    if first_total < limits["min_setup_length_mm"]:
        error("Setup length %d mm is below the minimum %d mm." % (first_total, limits["min_setup_length_mm"]))

    declared = setup["order_length_m"]
    computed = longest / 1000
    if declared and abs(declared - computed) > max(1, computed * limits["order_length_tolerance_percent"] / 100):
        warning("order_length_m is %d m, the orders need about %.0f m." % (declared, computed))


def main(argv: Optional[List[str]] = None) -> int:
    parser = argparse.ArgumentParser(description="Validate the planner wide CSV for Syncro.")
    parser.add_argument("file")
    parser.add_argument("--limits", help="JSON file overriding DEFAULT_LIMITS")
    parser.add_argument("--qualities", help="allowed quality IDs: comma separated or @file")
    parser.add_argument("--flutes", help="allowed flute IDs: comma separated or @file")
    parser.add_argument("--json", action="store_true", help="machine-readable output")
    parser.add_argument("--strict", action="store_true", help="warnings also fail")
    args = parser.parse_args(argv)

    limits = dict(DEFAULT_LIMITS)
    if args.limits:
        with open(args.limits, encoding="utf-8") as handle:
            limits.update(json.load(handle))

    def load_list(value: Optional[str]) -> Optional[set]:
        if not value:
            return None
        if value.startswith("@"):
            with open(value[1:], encoding="utf-8") as handle:
                return {line.strip() for line in handle if line.strip()}
        return {item.strip() for item in value.split(",") if item.strip()}

    try:
        with open(args.file, "rb") as handle:
            raw = handle.read()
    except OSError as problem:
        print("Cannot read file: %s" % problem, file=sys.stderr)
        return 2

    report = Report()
    rows = read_rows(raw, report)
    qualities, flutes = load_list(args.qualities), load_list(args.flutes)
    setups = []
    for row in rows:
        setup = parse_setup(row, limits, report)
        resolve_levels(setup)
        validate_setup(setup, limits, report, qualities, flutes)
        setups.append(setup)

    first_by_code: Dict[str, dict] = {}
    for setup in setups:
        orders = sorted(order["number"] for order in setup["orders"])
        first = first_by_code.setdefault(setup["code"], setup)
        if first is not setup:
            same = sorted(order["number"] for order in first["orders"]) == orders
            report.add("WARNING" if same else "ERROR", setup["where"],
                       "FOSBEROID repeats %s%s." % (first["where"], " (skipped by the application)" if same else " with different orders"))

    if args.json:
        print(json.dumps({"setups": len(setups), "errors": report.count("ERROR"), "warnings": report.count("WARNING"),
                          "issues": report.issues}, ensure_ascii=False, indent=2))
    else:
        for issue in report.issues:
            print("%-7s %s: %s" % (issue["severity"], issue["location"], issue["message"]))
        print("Setups: %d, errors: %d, warnings: %d" % (len(setups), report.count("ERROR"), report.count("WARNING")))

    if not rows and report.count("ERROR"):
        return 2
    if report.count("ERROR") or (args.strict and report.count("WARNING")):
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
