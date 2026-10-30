"""Tests for the planner validators in tools/planner-wide-csv and tools/planner-format.

The validators are run as the planner runs them (a subprocess with --json), so the exit code,
the JSON report and the command line are all covered. Run: python3 -m unittest discover -s tests/python
"""
import json
import os
import subprocess
import sys
import tempfile
import unittest

ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__), "..", ".."))
WIDE_CSV = os.path.join(ROOT, "tools", "planner-wide-csv", "validate_wide_csv.py")
ORDERS = os.path.join(ROOT, "tools", "planner-format", "validate_orders.py")
OZDAN_CSV = os.path.join(ROOT, "tests", "models", "ozdan_data", "OrdList.csv")
OZDAN_DAT = os.path.join(ROOT, "tests", "models", "ozdan_data", "Ord_mac.dat")


def run(script, path, *options):
    # -I -B: isolated mode, no __pycache__ written next to the validators.
    completed = subprocess.run([sys.executable, "-I", "-B", script, path, "--json", *options],
                               capture_output=True, text=True, encoding="utf-8")
    report = json.loads(completed.stdout) if completed.stdout.strip() else None
    return completed.returncode, report


def messages(report, severity):
    return [issue["message"] for issue in report["issues"] if issue["severity"] == severity]


class TempFiles(unittest.TestCase):
    def setUp(self):
        self.directory = tempfile.TemporaryDirectory()
        self.addCleanup(self.directory.cleanup)

    def write(self, name, content, encoding="utf-8"):
        path = os.path.join(self.directory.name, name)
        with open(path, "w", encoding=encoding, newline="") as handle:
            handle.write(content)
        return path


class OzdanCsv:
    """The Ozdan OrdList.csv split into rows of cells, to derive variants of the real file."""

    def __init__(self):
        with open(OZDAN_CSV, encoding="utf-8", newline="") as handle:
            lines = handle.read().split("\r\n")
        self.header = lines[0].split(";")
        self.rows = [line.split(";") for line in lines[1:] if line]

    def setup(self, fosberoid):
        return next(row[:] for row in self.rows if row[self.header.index("FOSBEROID")] == fosberoid)

    def set(self, row, column, value):
        row[self.header.index(column)] = value

    def get(self, row, column):
        return row[self.header.index(column)]

    def text(self, rows):
        return "\r\n".join([";".join(self.header)] + [";".join(row) for row in rows]) + "\r\n"


class WideCsvValidatorTest(TempFiles):
    def setUp(self):
        super().setUp()
        self.ozdan = OzdanCsv()

    def test_accepts_the_ozdan_export_as_received(self):
        code, report = run(WIDE_CSV, OZDAN_CSV)

        self.assertEqual(code, 0)
        self.assertEqual(report["setups"], 6)
        self.assertEqual(report["errors"], 0)
        self.assertEqual(messages(report, "WARNING"), ["Orders R952026103 have 0 pieces; the application skips the whole setup."])

    def test_examples_shipped_with_the_specification(self):
        valid_code, valid = run(WIDE_CSV, os.path.join(ROOT, "tools", "planner-wide-csv", "example_valid.csv"))
        received_code, received = run(WIDE_CSV, os.path.join(ROOT, "tools", "planner-wide-csv", "example_as_received.csv"))

        self.assertEqual((valid_code, valid["errors"], valid["warnings"]), (0, 0, 0))
        self.assertEqual((received_code, received["errors"]), (1, 3))

    def test_ignores_the_trailing_semicolon_but_rejects_a_real_extra_column(self):
        row = self.ozdan.setup("5148")
        trailing_code, trailing = run(WIDE_CSV, self.write("trailing.csv", self.ozdan.text([row])))
        extra_code, extra = run(WIDE_CSV, self.write("extra.csv", self.ozdan.text([row[:-1] + ["x", ""]])))

        self.assertEqual((trailing_code, trailing["errors"]), (0, 0))
        self.assertEqual(extra_code, 2)
        self.assertEqual(messages(extra, "ERROR"), ["Row has 91 columns, header has 89."])

    def test_accepts_more_than_five_outs_on_one_order(self):
        # Setup 5122 has 6 outs on its only order; only the zero quantity is reported.
        code, report = run(WIDE_CSV, OZDAN_CSV)

        self.assertEqual(code, 0)
        self.assertFalse([message for message in messages(report, "ERROR") if "out_box" in message])

    def test_rejects_outs_above_fourteen_across_the_setup(self):
        row = self.ozdan.setup("5151")
        for block, outs in (("z1", "6"), ("z2", "6"), ("z3", "3")):
            self.ozdan.set(row, block + "_out_box", outs)
        code, report = run(WIDE_CSV, self.write("outs.csv", self.ozdan.text([row])))

        self.assertEqual(code, 1)
        self.assertIn("Total out_box is 15; the limit is 14.", messages(report, "ERROR"))

    def test_treats_dat_fixed_levels_as_empty_and_keeps_real_levels(self):
        fixed = self.ozdan.setup("5151")
        self.assertEqual([self.ozdan.get(fixed, block + "_level") for block in ("z1", "z2", "z3")], ["20", "10", "10"])
        explicit = self.ozdan.setup("5151")
        for block, level in (("z1", "3"), ("z2", "3"), ("z3", "1")):
            self.ozdan.set(explicit, block + "_level", level)
        wrong = self.ozdan.setup("5151")
        self.ozdan.set(wrong, "z3_level", "7")

        self.assertEqual(run(WIDE_CSV, self.write("fixed.csv", self.ozdan.text([fixed])))[0], 0)
        self.assertEqual(run(WIDE_CSV, self.write("explicit.csv", self.ozdan.text([explicit])))[0], 0)
        code, report = run(WIDE_CSV, self.write("wrong.csv", self.ozdan.text([wrong])))
        self.assertEqual(code, 1)
        self.assertIn("Order 3: knife level 7 is not available on this machine.", messages(report, "ERROR"))

    def test_rejects_different_lengths_on_one_knife_level(self):
        row = self.ozdan.setup("5151")
        for block in ("z1", "z2", "z3"):
            self.ozdan.set(row, block + "_level", "1")
        code, report = run(WIDE_CSV, self.write("levels.csv", self.ozdan.text([row])))

        self.assertEqual(code, 1)
        self.assertIn("Orders on knife level 1 must have the same length.", messages(report, "ERROR"))

    def test_creases_limits(self):
        per_order = self.ozdan.setup("5148")
        for index in range(1, 17):
            self.ozdan.set(per_order, "z1_score_%d" % index, "500")
        self.ozdan.set(per_order, "z1_score_17", "170")
        per_setup = self.ozdan.setup("5151")
        for block, count in (("z1", 15), ("z2", 8)):
            for index in range(1, count + 1):
                self.ozdan.set(per_setup, "%s_score_%d" % (block, index), "10")
        order_code, order_report = run(WIDE_CSV, self.write("creases.csv", self.ozdan.text([per_order])))
        setup_code, setup_report = run(WIDE_CSV, self.write("setup.csv", self.ozdan.text([per_setup])))

        self.assertEqual(order_code, 1)
        self.assertIn("Order 1 has 17 creases; the limit is 15 per order.", messages(order_report, "ERROR"))
        self.assertEqual(setup_code, 1)
        self.assertTrue(any("23" in message and "22" in message for message in messages(setup_report, "ERROR")), setup_report["issues"])

    def test_accepts_windows_1250_with_a_warning(self):
        row = self.ozdan.setup("5148")
        self.ozdan.set(row, "z1_customer_name", "ŁÓDŹ-KARTON")
        code, report = run(WIDE_CSV, self.write("cp1250.csv", self.ozdan.text([row]), encoding="cp1250"))

        self.assertEqual((code, report["errors"]), (0, 0))
        self.assertTrue(any("1250" in message for message in messages(report, "WARNING")), report["issues"])

    def test_limits_sheets_per_stack_to_1000(self):
        row = self.ozdan.setup("5148")
        self.ozdan.set(row, "z1_sheets_per_stack", "1000")
        valid_code, _ = run(WIDE_CSV, self.write("stack1000.csv", self.ozdan.text([row])))
        self.ozdan.set(row, "z1_sheets_per_stack", "1001")
        invalid_code, invalid = run(WIDE_CSV, self.write("stack1001.csv", self.ozdan.text([row])))

        self.assertEqual(valid_code, 0)
        self.assertEqual(invalid_code, 1)
        self.assertIn("Order 1: sheets_per_stack must be 1-1000.", messages(invalid, "ERROR"))

    def test_repeated_fosberoid(self):
        row = self.ozdan.setup("5148")
        other = self.ozdan.setup("5148")
        self.ozdan.set(other, "z1_order_number_optima", "R999")
        same_code, same = run(WIDE_CSV, self.write("same.csv", self.ozdan.text([row, row])))
        different_code, different = run(WIDE_CSV, self.write("different.csv", self.ozdan.text([row, other])))

        self.assertEqual(same_code, 0)
        self.assertTrue(any("skipped by the application" in message for message in messages(same, "WARNING")))
        self.assertEqual(different_code, 1)
        self.assertTrue(any("with different orders" in message for message in messages(different, "ERROR")))


EXTENDED_HEADER = ("setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,"
                   "sheet_length_mm,quantity_sheets,sheets_per_stack,level,score_measures_mm")


class OrdersValidatorTest(TempFiles):
    def test_accepts_the_ozdan_classic_dat(self):
        code, report = run(ORDERS, OZDAN_DAT)

        self.assertEqual(code, 0)
        self.assertEqual((report["format"], report["setups"], report["errors"]), ("dat587", 6, 0))
        self.assertEqual(len(messages(report, "WARNING")), 1)

    def test_accepts_the_extended_csv_example(self):
        code, report = run(ORDERS, os.path.join(ROOT, "tools", "example_extended_orders.csv"))

        self.assertEqual((code, report["format"], report["errors"], report["warnings"]), (0, "csv", 0, 0))

    def test_outs_are_limited_only_by_the_setup_total(self):
        # 6 + 8 outs of 170 mm on a 2500 mm web leave a 120 mm trim.
        def setup(outs_2):
            return "\n".join([
                EXTENDED_HEADER,
                "S1,2500,Q1,A,A-1,Customer A,170,6,1200,3000,100,3,",
                "S1,2500,Q1,A,A-2,Customer B,170,%s,1200,3000,100,3," % outs_2,
            ]) + "\n"

        valid_code, valid = run(ORDERS, self.write("outs14.csv", setup("8")))
        invalid_code, invalid = run(ORDERS, self.write("outs15.csv", setup("9")))

        self.assertEqual(valid_code, 0, valid["issues"])
        self.assertFalse([message for message in messages(valid, "ERROR") if "outs" in message])
        self.assertEqual(invalid_code, 1)
        self.assertTrue(any("Łączna liczba użytków wynosi 15; limit to 14." in message for message in messages(invalid, "ERROR")),
                        invalid["issues"])

    def test_dat_sheets_per_stack_is_the_actual_value(self):
        # Ozdan writes 690 into the DAT; it is no longer read as 345 doubled, and odd values are fine.
        # Setup 5148 (second record); order 1 sheets per stack: block start 142 + 15 + 12 + 30 + 5 + 5 x 5.
        with open(OZDAN_DAT, encoding="utf-8") as handle:
            record = handle.read()[587:1174]
        stack = 142 + 87
        self.assertEqual(record[stack:stack + 4], " 690")
        odd = record[:stack] + " 691" + record[stack + 4:]
        too_many = record[:stack] + "1001" + record[stack + 4:]

        odd_code, odd_report = run(ORDERS, self.write("odd.dat", odd))
        many_code, many_report = run(ORDERS, self.write("many.dat", too_many))

        self.assertEqual((odd_code, odd_report["errors"], odd_report["warnings"]), (0, 0, 0), odd_report["issues"])
        self.assertEqual(many_code, 1)
        self.assertTrue(any("arkusze na stos muszą być w zakresie 0-1000" in message for message in messages(many_report, "ERROR")),
                        many_report["issues"])

    def test_dat_accepts_zero_sheets_per_stack_as_the_machine_default(self):
        # Both orders of the second setup in this planner file have 0 sheets per stack.
        code, report = run(ORDERS, os.path.join(ROOT, "tests", "models", "classic_data", "Ord_mac 5 bigow.dat"))

        self.assertNotEqual(code, 2)
        self.assertFalse([message for message in messages(report, "ERROR") if "arkusze na stos" in message], report["issues"])

    def test_write_dat740_keeps_the_actual_sheets_per_stack(self):
        out = os.path.join(self.directory.name, "out.dat")
        code, _ = run(ORDERS, os.path.join(ROOT, "tools", "example_extended_orders.csv"), "--write-dat740", out)
        with open(out, encoding="utf-8") as handle:
            record = handle.read()

        self.assertEqual(code, 0)
        # Order 1 block starts at 142; sheets per stack at relative offset 137 (example: 100 sheets).
        self.assertEqual(record[142 + 137:142 + 141], " 100")

    def test_rejects_an_unknown_file(self):
        code, report = run(ORDERS, self.write("unknown.dat", "not an orders file\n"))

        self.assertEqual(code, 2)
        self.assertIsNone(report["format"])


if __name__ == "__main__":
    unittest.main()
