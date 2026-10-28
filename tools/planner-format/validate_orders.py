#!/usr/bin/env python3
"""Validate planner order files for the Syncro order import.

Supported inputs:
  csv     extended orders CSV (one row per order, grouped by setup_code)
  dat587  classic fixed-width DAT (5 crease fields per order, 1-char outs)
  dat589  extended-outs DAT (5 crease fields, 2-char outs on orders 1-2)
  dat740  extended DAT proposal (15 crease fields per order, 2-char outs)

The rules mirror models/order-validation.js and models/order.js in the
Syncro web application. Only the Python 3.8+ standard library is used.

Exit status: 0 valid (warnings allowed), 1 validation errors, 2 unreadable input.
"""

import argparse
import csv
import io
import json
import math
import re
import sys
from dataclasses import dataclass, field
from decimal import Decimal, InvalidOperation
from typing import Dict, List, Optional, Tuple

# All lengths in tenths of a millimetre unless the name says otherwise.
DEFAULT_LIMITS = {
    "max_creases_per_order": 15,
    "max_creases_per_setup": 22,
    "max_outs_per_order": 5,
    "max_total_outs": 14,
    "max_orders_per_setup": 3,
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
}

CSV_REQUIRED = [
    "setup_code", "web_width_mm", "quality_id", "flute", "order_number", "customer_name",
    "sheet_width_mm", "outs", "sheet_length_mm", "quantity_sheets", "sheets_per_stack",
    "score_measures_mm",
]
CSV_OPTIONAL = ["level"]
SETUP_CODE_RE = re.compile(r"^[A-Za-z0-9_-]{1,27}(?:\.\d{1,2})?$")

HEADER_LAYOUT = [
    ("program_number", 11), ("scheduled_papers", 44), ("fosber_id", 12), ("quality_id", 36),
    ("flute_id", 23), ("web_width", 5), ("linear_meters", 10), ("g", 1),
]
TAIL_LAYOUT = [("reserved_1", 8), ("reserved_2", 8), ("padding", 90)]


def order_layout(score_fields: int, outs_width: int) -> List[Tuple[str, int]]:
    return (
        [("customer_name", 15), ("customer_code", 12), ("order_number", 30), ("sheet_width", 5)]
        + [("score_%d" % (index + 1), 5) for index in range(score_fields)]
        + [("sheets_per_stack", 4), ("sheet_length", 5), ("quantity", 6), ("outs", outs_width),
           ("linear_meters", 8), ("fixed", 2)]
    )


DAT_FORMATS = {
    "dat587": {"orders": [order_layout(5, 1)] * 3, "length": 587},
    "dat589": {"orders": [order_layout(5, 2), order_layout(5, 2), order_layout(5, 1)], "length": 589},
    "dat740": {"orders": [order_layout(15, 2)] * 3, "length": 740},
}
for _spec in DAT_FORMATS.values():
    _total = sum(w for _, w in HEADER_LAYOUT) + sum(w for o in _spec["orders"] for _, w in o) + sum(w for _, w in TAIL_LAYOUT)
    assert _total == _spec["length"], _total


@dataclass
class Issue:
    severity: str  # ERROR or WARNING
    location: str
    message: str


@dataclass
class Order:
    number: str = ""
    customer: str = ""
    sheet_width: Optional[int] = None
    outs: Optional[int] = None
    sheet_length: Optional[int] = None
    quantity: Optional[int] = None
    sheets_per_stack: Optional[float] = None
    level: Optional[int] = None
    scores: List[int] = field(default_factory=list)


@dataclass
class Setup:
    code: str
    location: str
    web_width: Optional[int] = None
    quality: str = ""
    flute: str = ""
    orders: List[Order] = field(default_factory=list)
    source: str = "csv"


class Report:
    def __init__(self) -> None:
        self.issues: List[Issue] = []

    def error(self, location: str, message: str) -> None:
        self.issues.append(Issue("ERROR", location, message))

    def warning(self, location: str, message: str) -> None:
        self.issues.append(Issue("WARNING", location, message))

    @property
    def errors(self) -> int:
        return sum(1 for issue in self.issues if issue.severity == "ERROR")

    @property
    def warnings(self) -> int:
        return sum(1 for issue in self.issues if issue.severity == "WARNING")


def mm(tenths: int) -> str:
    return ("%.1f" % (tenths / 10)).rstrip("0").rstrip(".") + " mm"


def parse_mm(text: str) -> int:
    """Millimetres with at most one decimal place -> tenths of a millimetre."""
    try:
        value = Decimal(text.strip())
    except InvalidOperation:
        raise ValueError("'%s' nie jest liczbą" % text)
    if value <= 0:
        raise ValueError("'%s' musi być dodatnie" % text)
    tenths = value * 10
    if tenths != tenths.to_integral_value():
        raise ValueError("'%s' ma więcej niż jedno miejsce po przecinku" % text)
    return int(tenths)


def parse_int(text: str) -> int:
    text = text.strip()
    if not re.fullmatch(r"-?\d+", text):
        raise ValueError("'%s' nie jest liczbą całkowitą" % text)
    return int(text)


def classic_levels(lengths: List[Optional[int]], knife_level_count: int) -> List[int]:
    """Port of utils/classic-levels.js: DAT files carry no level, it is derived from lengths."""
    if knife_level_count <= 1:
        return [1, 1, 1]
    levels = [3, 1, 1]
    if len(lengths) != 3 or any(length is None for length in lengths):
        return levels
    groups: Dict[int, List[int]] = {}
    for index, length in enumerate(lengths):
        groups.setdefault(length, []).append(index)
    if len(groups) == 1:
        return [3, 3, 3]
    if len(groups) == 2:
        first = next(iter(groups.values()))
        for indexes in groups.values():
            for index in indexes:
                levels[index] = 3 if indexes is first else 1
    return levels


def physical_knife_level(level: int, level_count: int) -> Optional[int]:
    if level < 1 or level > 3:
        return None
    if level_count == 1:
        return 1
    if level_count == 2:
        return None if level == 2 else (1 if level == 1 else 2)
    return level


def level_limit(limits: dict, name: str, level: int, fallback: int) -> int:
    value = limits.get(name, fallback)
    if isinstance(value, list):
        return int(value[level - 1]) if level - 1 < len(value) else fallback
    return int(value)


# ---------------------------------------------------------------- CSV

def read_csv(text: str, report: Report) -> List[Setup]:
    rows = list(csv.reader(io.StringIO(text)))
    rows = [row for row in rows if any(cell.strip() for cell in row)]
    if len(rows) < 2:
        report.error("plik", "CSV nie zawiera wierszy z zamówieniami.")
        return []

    headers = [header.strip().lower() for header in rows[0]]
    if headers and headers[0].startswith("\ufeff"):
        headers[0] = headers[0][1:]
    duplicates = sorted({header for header in headers if headers.count(header) > 1})
    if duplicates:
        report.error("nagłówek", "Powtórzone kolumny: %s" % ", ".join(duplicates))
        return []
    missing = [name for name in CSV_REQUIRED if name not in headers]
    if missing:
        report.error("nagłówek", "Brak wymaganych kolumn: %s" % ", ".join(missing))
        return []
    unknown = [name for name in headers if name not in CSV_REQUIRED + CSV_OPTIONAL]
    if unknown:
        report.warning("nagłówek", "Nieznane kolumny zostaną zignorowane: %s" % ", ".join(unknown))

    setups: Dict[str, Setup] = {}
    for line_number, values in enumerate(rows[1:], start=2):
        location = "wiersz %d" % line_number
        if len(values) != len(headers):
            report.error(location, "Liczba kolumn (%d) różni się od nagłówka (%d)." % (len(values), len(headers)))
            continue
        row = {header: value.strip() for header, value in zip(headers, values)}
        code = row["setup_code"]
        if not code:
            report.error(location, "Brak setup_code.")
            continue

        setup = setups.get(code)
        web_width = None
        try:
            web_width = parse_mm(row["web_width_mm"])
        except ValueError as error:
            report.error(location, "web_width_mm: %s" % error)
        if setup is None:
            setup = Setup(code=code, location=location, web_width=web_width,
                          quality=row["quality_id"], flute=row["flute"], source="csv")
            setups[code] = setup
        elif (setup.web_width, setup.quality, setup.flute) != (web_width, row["quality_id"], row["flute"]):
            report.error(location, "Setup %s: web_width_mm, quality_id i flute muszą być takie same we wszystkich wierszach." % code)

        if len(setup.orders) >= 3:
            report.error(location, "Setup %s ma więcej niż 3 zamówienia." % code)
            continue

        order = Order(number=row["order_number"], customer=row["customer_name"])
        for name, attribute in (("sheet_width_mm", "sheet_width"), ("sheet_length_mm", "sheet_length")):
            try:
                setattr(order, attribute, parse_mm(row[name]))
            except ValueError as error:
                report.error(location, "%s: %s" % (name, error))
        for name, attribute in (("outs", "outs"), ("quantity_sheets", "quantity"), ("sheets_per_stack", "sheets_per_stack")):
            try:
                setattr(order, attribute, parse_int(row[name]))
            except ValueError as error:
                report.error(location, "%s: %s" % (name, error))
        level_text = row.get("level", "")
        if level_text:
            try:
                order.level = parse_int(level_text)
            except ValueError as error:
                report.error(location, "level: %s" % error)
        else:
            order.level = 3 if not setup.orders else 1
        for index, score in enumerate(filter(None, (part.strip() for part in row["score_measures_mm"].split(";"))), start=1):
            try:
                order.scores.append(parse_mm(score))
            except ValueError as error:
                report.error(location, "score_measures_mm, bigowanie %d: %s" % (index, error))
        setup.orders.append(order)
    return list(setups.values())


# ---------------------------------------------------------------- DAT

def split_fields(record: str, layout: List[Tuple[str, int]], offset: int) -> Tuple[Dict[str, str], int]:
    values = {}
    for name, width in layout:
        values[name] = record[offset:offset + width]
        offset += width
    return values, offset


def dat_records(text: str, length: int) -> List[Tuple[int, str]]:
    """Records may be concatenated or separated by LF/CRLF, exactly as the server reads them."""
    return [(match.start(), match.group(0)) for match in re.finditer(r"[^\r\n]{%d}" % length, text)]


def detect_dat_format(text: str, report: Report) -> Optional[str]:
    # Lengths alone are ambiguous (real files end with blank padding), so check that
    # every non-blank record has the constant 'G' where the header expects it.
    matching = []
    for name, spec in DAT_FORMATS.items():
        records = [record for _, record in dat_records(text, spec["length"]) if record.strip()]
        if records and all(record[141] == "G" for record in records):
            leftover = re.sub(r"[^\r\n]{%d}" % spec["length"], "", text)
            if not leftover.strip():
                matching.append(name)
    if len(matching) > 1:
        # Same tie-break as the server: order 1's fixed "20" field sits at 254 (587/589 differ by outs width) or 304 (740).
        first = text.lstrip("\r\n")
        markers = {"dat587": 253, "dat589": 254, "dat740": 304}
        marked = [name for name in matching if first[markers[name]:markers[name] + 2] == "20"]
        if len(marked) == 1:
            matching = marked
    if len(matching) == 1:
        return matching[0]
    if not matching:
        report.error("plik", "Plik nie pasuje do żadnego formatu DAT (587, 589, 740 znaków na rekord, 'G' na pozycji 141).")
    else:
        report.error("plik", "Nie da się jednoznacznie rozpoznać formatu (%s). Podaj --format." % ", ".join(matching))
    return None


def read_dat(text: str, format_name: str, limits: dict, report: Report) -> List[Setup]:
    spec = DAT_FORMATS[format_name]
    setups: List[Setup] = []
    leftover = re.sub(r"[^\r\n]{%d}" % spec["length"], "", text)
    if leftover.strip():
        report.warning("plik", "Część danych nie tworzy pełnych rekordów %d-znakowych i zostanie pominięta." % spec["length"])

    for record_number, (offset, record) in enumerate(dat_records(text, spec["length"]), start=1):
        if not record.strip():
            continue
        location = "rekord %d (znak %d)" % (record_number, offset)
        header, position = split_fields(record, HEADER_LAYOUT, 0)
        fosber_id = header["fosber_id"].strip()
        setup = Setup(code=fosber_id + ".2" if fosber_id else "", location=location,
                      quality=header["quality_id"].strip(), flute=header["flute_id"].strip(), source=format_name)
        if not fosber_id:
            report.error(location, "Brak Fosber ID (pozycja 55, 12 znaków).")
        elif not fosber_id.isdigit():
            report.warning(location, "Fosber ID '%s' nie jest liczbą; SyncroS traktuje go jako klucz liczbowy." % fosber_id)
        try:
            setup.web_width = parse_int(header["web_width"])
        except ValueError as error:
            report.error(location, "Szerokość wstęgi: %s" % error)
        if header["g"] != "G":
            report.warning(location, "Pole stałe na pozycji 141 powinno zawierać 'G'.")

        raw_orders = []
        for layout in spec["orders"]:
            values, position = split_fields(record, layout, position)
            raw_orders.append(values)

        for index, values in enumerate(raw_orders):
            number = values["order_number"].strip()
            if index > 0 and not number:
                if any(value.strip() not in ("", "0") for key, value in values.items() if key != "fixed"):
                    report.warning(location, "Zamówienie %d nie ma numeru, ale ma inne dane; zostanie pominięte." % (index + 1))
                continue
            order = Order(number=number, customer=values["customer_name"].strip())
            where = "%s, zamówienie %d" % (location, index + 1)
            for key, attribute in (("sheet_width", "sheet_width"), ("sheet_length", "sheet_length"),
                                   ("quantity", "quantity"), ("outs", "outs")):
                try:
                    setattr(order, attribute, parse_int(values[key]))
                except ValueError as error:
                    report.error(where, "%s: %s" % (key, error))
            try:
                doubled = parse_int(values["sheets_per_stack"])
                order.sheets_per_stack = doubled / 2
                if doubled % 2:
                    report.warning(where, "Pole arkuszy na stos (%d) jest nieparzyste; w DAT zapisuje się podwójną wartość." % doubled)
            except ValueError as error:
                report.error(where, "sheets_per_stack: %s" % error)

            score_texts = [values[key].strip() for key in values if key.startswith("score_")]
            measures = []
            for score_index, text_value in enumerate(score_texts, start=1):
                if not text_value or text_value == "0":
                    continue
                for part in text_value.split():
                    try:
                        measures.append(parse_int(part))
                    except ValueError as error:
                        report.error(where, "bigowanie %d: %s" % (score_index, error))
            if len(measures) == 1 and measures[0] == order.sheet_width:
                measures = []  # a single crease equal to the sheet width means "no creases"
            order.scores = measures
            setup.orders.append(order)

        levels = classic_levels([order.sheet_length for order in setup.orders] if len(setup.orders) == 3 else [],
                                int(limits["knife_level_count"]))
        for order, level in zip(setup.orders, levels):
            order.level = level
        setups.append(setup)
    return setups


def write_dat740(setups: List[Setup], report: Report) -> str:
    """Reference writer for the proposed 740-character layout."""
    def text(value: str, width: int) -> str:
        return str(value)[:width].ljust(width)

    def number(value, width: int) -> str:
        if value is None or value == "":
            return " " * width
        rendered = str(int(value))
        if len(rendered) > width:
            raise ValueError("wartość %s nie mieści się w %d znakach" % (rendered, width))
        return rendered.rjust(width)

    records = []
    for setup in setups:
        fosber_id = setup.code.split(".")[0]
        if len(fosber_id) > 12:
            report.error(setup.location, "Kod setupu '%s' nie zmieści się w 12-znakowym polu Fosber ID." % fosber_id)
            continue
        derived = classic_levels([o.sheet_length for o in setup.orders] if len(setup.orders) == 3 else [], 3)
        if any(order.level != level for order, level in zip(setup.orders, derived)):
            report.warning(setup.location, "DAT nie przenosi poziomu noża; serwer wyliczy poziomy %s z długości arkuszy." % derived[:len(setup.orders)])
        try:
            parts = [text("", 11), text("", 44), text(fosber_id, 12), text(setup.quality, 36), text(setup.flute, 23),
                     number(setup.web_width, 5), text("", 10), "G"]
            for index in range(3):
                order = setup.orders[index] if index < len(setup.orders) else None
                if order is None:
                    parts.append(" " * 164)
                    continue
                if len(order.scores) > 15:
                    raise ValueError("zamówienie %d ma więcej niż 15 bigów" % (index + 1))
                scores = [number(score, 5) for score in order.scores] + [" " * 5] * (15 - len(order.scores))
                stack = None if order.sheets_per_stack is None else int(order.sheets_per_stack * 2)
                parts += [text(order.customer, 15), text("", 12), text(order.number, 30), number(order.sheet_width, 5)]
                parts += scores
                parts += [number(stack, 4), number(order.sheet_length, 5), number(order.quantity, 6),
                          number(order.outs, 2), text("", 8), "20" if index == 0 else "10"]
            parts += [" " * 8, " " * 8, " " * 90]
        except ValueError as error:
            report.error(setup.location, "Nie można zapisać DAT 740: %s" % error)
            continue
        record = "".join(parts)
        assert len(record) == 740, len(record)
        records.append(record)
    return "".join(records)


# ---------------------------------------------------------------- rules

def validate_setup(setup: Setup, limits: dict, report: Report, qualities: Optional[set], flutes: Optional[set]) -> None:
    where = "%s, setup %s" % (setup.location, setup.code or "?")
    error = lambda message: report.error(where, message)

    if setup.source == "csv" and not SETUP_CODE_RE.match(setup.code):
        error("setup_code musi mieć 1-27 znaków [A-Za-z0-9_-] i opcjonalny sufiks .NN.")
    if not setup.quality or not setup.flute:
        error("Brak jakości lub fali.")
    if qualities is not None and setup.quality and setup.quality not in qualities:
        error("Jakość '%s' nie istnieje w bazie." % setup.quality)
    if flutes is not None and setup.flute and setup.flute not in flutes:
        error("Fala '%s' nie istnieje w bazie." % setup.flute)

    web = setup.web_width
    if web is not None:
        if web <= 0:
            error("Szerokość wstęgi musi być dodatnia.")
        elif web > limits["max_web_width_tenths_mm"]:
            error("Szerokość wstęgi %s przekracza maksimum %s." % (mm(web), mm(limits["max_web_width_tenths_mm"])))

    orders = setup.orders
    if not orders:
        error("Setup nie zawiera zamówień.")
        return
    if len(orders) > limits["max_orders_per_setup"]:
        error("Setup ma %d zamówień; limit to %d." % (len(orders), limits["max_orders_per_setup"]))

    zero = [order.number for order in orders if order.quantity == 0]
    if zero:
        report.warning(where, "Zamówienia %s mają 0 sztuk. Aplikacja pominie cały setup." % ", ".join(zero))
        return

    is_csv = setup.source == "csv"
    if not is_csv:
        # Like the server, a DAT order with outs 0 is inactive.
        if orders[0].outs == 0:
            error("Setup musi zawierać pierwsze zamówienie (outs > 0).")
            return
        if len(orders) == 3 and orders[2].outs and not orders[1].outs:
            error("Nie można pominąć drugiego zamówienia, jeśli podano trzecie.")
        orders = [order for order in orders if order.outs != 0]
    max_outs = limits["max_outs_per_order"] if is_csv else 9

    complete = True
    total_creases = 0
    for index, order in enumerate(orders, start=1):
        label = "Zamówienie %d" % index
        if not order.number:
            error("%s: brak numeru zamówienia." % label)
        elif len(order.number) > 30:
            error("%s: numer zamówienia dłuższy niż 30 znaków." % label)
        if not order.customer:
            (error if is_csv else lambda message: report.warning(where, message))("%s: brak nazwy klienta." % label)
        elif len(order.customer) > 15:
            error("%s: nazwa klienta dłuższa niż 15 znaków." % label)
        if order.outs is None or not 1 <= order.outs <= max_outs:
            error("%s: outs musi być liczbą całkowitą 1-%d." % (label, max_outs))
        if order.quantity is None or not 1 <= order.quantity <= limits["max_quantity"]:
            error("%s: ilość arkuszy musi być liczbą całkowitą 1-%d." % (label, limits["max_quantity"]))
        if is_csv and (order.sheets_per_stack is None or not 1 <= order.sheets_per_stack <= limits["max_sheets_per_stack"]):
            error("%s: arkusze na stos muszą być w zakresie 1-%d." % (label, limits["max_sheets_per_stack"]))
        if order.sheet_width is None or order.sheet_width <= 0:
            error("%s: szerokość arkusza musi być dodatnia." % label)
        if order.sheet_length is None or order.sheet_length <= 0:
            error("%s: długość arkusza musi być dodatnia." % label)
        if order.level is None or physical_knife_level(order.level, int(limits["knife_level_count"])) is None:
            error("%s: poziom noża %s nie jest dostępny na tej maszynie." % (label, order.level))
        if None in (order.outs, order.quantity, order.sheet_width, order.sheet_length, order.level):
            complete = False

        for position, score in enumerate(order.scores, start=1):
            if score <= 0:
                error("%s: bigowanie %d musi być dodatnie." % (label, position))
            elif order.sheet_width and score > order.sheet_width:
                error("%s: bigowanie %d (%s) przekracza szerokość arkusza %s." % (label, position, mm(score), mm(order.sheet_width)))
        if len(order.scores) > limits["max_creases_per_order"]:
            error("%s zawiera %d bigów; limit to %d na zamówienie." % (label, len(order.scores), limits["max_creases_per_order"]))
        if order.scores and order.sheet_width and sum(order.scores) != order.sheet_width:
            report.warning(where, "%s: suma szerokości pól bigowania (%s) różni się od szerokości arkusza (%s)."
                           % (label, mm(sum(order.scores)), mm(order.sheet_width)))
        total_creases += len(order.scores)

    if total_creases > limits["max_creases_per_setup"]:
        error("Setup zawiera %d bigów; limit to %d." % (total_creases, limits["max_creases_per_setup"]))

    if not complete or web is None:
        return

    outs_sum = sum(order.outs for order in orders)
    if outs_sum > limits["max_total_outs"]:
        error("Łączna liczba użytków wynosi %d; limit to %d." % (outs_sum, limits["max_total_outs"]))

    level_count = int(limits["knife_level_count"])
    length_by_level: Dict[int, int] = {}
    for index, order in enumerate(orders, start=1):
        knife = physical_knife_level(order.level, level_count)
        if knife is None:
            continue
        minimum = level_limit(limits, "min_cut_length_tenths_mm", knife, 5000)
        maximum = level_limit(limits, "max_cut_length_tenths_mm", knife, 50000)
        if order.sheet_length < minimum:
            error("Zamówienie %d: długość cięcia %s jest mniejsza od minimum %s." % (index, mm(order.sheet_length), mm(minimum)))
        if order.sheet_length > maximum:
            error("Zamówienie %d: długość cięcia %s przekracza maksimum %s dla poziomu %d." % (index, mm(order.sheet_length), mm(maximum), knife))
        if knife in length_by_level and length_by_level[knife] != order.sheet_length:
            error("Zamówienia na tym samym poziomie noża (%d) muszą mieć tę samą długość arkusza." % knife)
        length_by_level.setdefault(knife, order.sheet_length)

    if setup.source != "csv" and len(setup.orders) == 3 and len(orders) == 3:
        pairs = [(a, b) for a in range(3) for b in range(a + 1, 3)
                 if orders[a].level == orders[b].level and orders[a].sheet_length == orders[b].sheet_length]
        if not pairs:
            error("Przy trzech zamówieniach co najmniej dwa muszą mieć tę samą długość arkusza (maszyna nie tnie trzech niezależnych długości).")

    used = sum(order.sheet_width * order.outs for order in orders)
    if used < limits["min_setup_width_tenths_mm"]:
        error("Szerokość zestawu %s jest mniejsza od minimum %s." % (mm(used), mm(limits["min_setup_width_tenths_mm"])))
    if used > web:
        error("Suma szerokości zamówień (%s) przekracza szerokość wstęgi (%s)." % (mm(used), mm(web)))
    else:
        trim = web - used
        if trim > limits["max_trim_tenths_mm"]:
            error("Trim %s przekracza maksimum %s." % (mm(trim), mm(limits["max_trim_tenths_mm"])))
        if trim < limits["min_trim_tenths_mm"] and not limits["allow_narrow_trim"]:
            error("Trim %s jest mniejszy od minimum %s." % (mm(trim), mm(limits["min_trim_tenths_mm"])))

    # Port of the Syncro AOC minimum run-length check from order-validation.js.
    totals = [(order.sheet_length // 10) * math.ceil(order.quantity / order.outs) for order in orders]
    longest = max(totals)
    first = orders[0]
    first_total = totals[0]
    if abs(first_total - longest) >= first.sheet_length:
        first_total = (first.sheet_length // 10) * (longest * 10 // first.sheet_length)
    if first_total < limits["min_setup_length_mm"]:
        error("Łączna długość setupu (%d mm) jest mniejsza od wymaganego minimum %d mm." % (first_total, limits["min_setup_length_mm"]))


def check_duplicates(setups: List[Setup], report: Report) -> None:
    seen: Dict[str, Setup] = {}
    for setup in setups:
        key = setup.code.lstrip("0")
        if not key:
            continue
        orders = sorted(order.number.lstrip("0") for order in setup.orders)
        if key in seen:
            first = seen[key]
            if sorted(order.number.lstrip("0") for order in first.orders) == orders:
                report.warning(setup.location, "Setup %s powtarza %s; aplikacja go pominie." % (setup.code, first.location))
            else:
                report.error(setup.location, "Kod setupu %s występuje też w %s z innymi zamówieniami." % (setup.code, first.location))
        else:
            seen[key] = setup


# ---------------------------------------------------------------- CLI

def load_list(value: Optional[str]) -> Optional[set]:
    if not value:
        return None
    if value.startswith("@"):
        with open(value[1:], encoding="utf-8") as handle:
            return {line.strip() for line in handle if line.strip()}
    return {item.strip() for item in value.split(",") if item.strip()}


def main(argv: Optional[List[str]] = None) -> int:
    parser = argparse.ArgumentParser(description="Walidator plików zamówień dla importu Syncro.")
    parser.add_argument("file", help="plik CSV lub DAT")
    parser.add_argument("--format", choices=["auto", "csv", "dat587", "dat589", "dat740"], default="auto")
    parser.add_argument("--limits", help="plik JSON nadpisujący limity (klucze jak w DEFAULT_LIMITS)")
    parser.add_argument("--qualities", help="dozwolone ID jakości: lista po przecinku albo @plik")
    parser.add_argument("--flutes", help="dozwolone ID fal: lista po przecinku albo @plik")
    parser.add_argument("--json", action="store_true", help="wynik w formacie JSON")
    parser.add_argument("--strict", action="store_true", help="ostrzeżenia też kończą się kodem 1")
    parser.add_argument("--write-dat740", metavar="OUT", help="zapisz poprawne setupy w formacie DAT 740")
    args = parser.parse_args(argv)

    limits = dict(DEFAULT_LIMITS)
    if args.limits:
        with open(args.limits, encoding="utf-8") as handle:
            limits.update(json.load(handle))

    try:
        with open(args.file, "rb") as handle:
            raw = handle.read()
    except OSError as error:
        print("Nie można odczytać pliku: %s" % error, file=sys.stderr)
        return 2
    try:
        text = raw.decode("utf-8-sig")
    except UnicodeDecodeError:
        print("Plik nie jest w UTF-8. Zapisz go jako UTF-8 (bez BOM dla DAT).", file=sys.stderr)
        return 2

    report = Report()
    file_format = args.format
    if file_format == "auto":
        first_line = text.split("\n", 1)[0].lower()
        if "setup_code" in [cell.strip() for cell in first_line.split(",")]:
            file_format = "csv"
        else:
            file_format = detect_dat_format(text, report) or ""

    setups: List[Setup] = []
    if file_format == "csv":
        setups = read_csv(text, report)
    elif file_format in DAT_FORMATS:
        if any(ord(character) > 127 for character in text):
            report.warning("plik", "DAT zawiera znaki spoza ASCII (np. polskie litery); każdy liczy się jako 1 znak - sprawdź długości pól.")
        setups = read_dat(text, file_format, limits, report)

    qualities, flutes = load_list(args.qualities), load_list(args.flutes)
    for setup in setups:
        validate_setup(setup, limits, report, qualities, flutes)
    check_duplicates(setups, report)

    if args.write_dat740 and setups:
        invalid_locations = {issue.location.split(", setup")[0] for issue in report.issues if issue.severity == "ERROR"}
        valid = [setup for setup in setups if setup.location not in invalid_locations]
        with open(args.write_dat740, "w", encoding="utf-8", newline="") as handle:
            handle.write(write_dat740(valid, report))

    if args.json:
        print(json.dumps({
            "format": file_format or None,
            "setups": len(setups),
            "errors": report.errors,
            "warnings": report.warnings,
            "issues": [issue.__dict__ for issue in report.issues],
        }, ensure_ascii=False, indent=2))
    else:
        for issue in report.issues:
            print("%-7s %s: %s" % (issue.severity, issue.location, issue.message))
        print("Format: %s, setupów: %d, błędów: %d, ostrzeżeń: %d" % (file_format or "?", len(setups), report.errors, report.warnings))

    if not file_format:
        return 2
    if report.errors or (args.strict and report.warnings):
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
