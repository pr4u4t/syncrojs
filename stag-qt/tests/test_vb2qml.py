import json
import sys
import tempfile
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
sys.path.insert(0, str(ROOT / "tools"))
import vb2qml  # noqa: E402

FORM = '''VERSION 5.00
Begin VB.Form sample
   BackColor       =   &H00FF8000&
   ClientHeight    =   1500
   ClientWidth     =   3000
   Begin VB.TextBox second
      Height          =   300
      Left            =   150
      TabIndex        =   1
      Top             =   300
      Width           =   600
   End
   Begin VB.TextBox first
      Height          =   300
      Left            =   150
      TabIndex        =   0
      Top             =   0
      Width           =   600
   End
   Begin VB.Label cap
      Caption         =   "He said ""hi"""
      Height          =   300
      Index           =   2
      Left            =   15
      Top             =   30
      Width           =   45
   End
   Begin VB.Label cap
      Height          =   300
      Index           =   2
      Left            =   15
      Top             =   30
      Width           =   45
   End
End
Attribute VB_Name = "Sample"
Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg target_page
  Case 2
    If ready Then
      eve_key.chg target_page, keep:=1
    End If
  Case Else
End Select
End Sub
Private Sub eve_key_esc(Shift As Integer)
  eve_key.chg pag_pre
End Sub
Private Sub first_Change()
  ' a comment does not count
  x = 1
End Sub
'''


class Vb2QmlTest(unittest.TestCase):
    def setUp(self):
        self.folder = tempfile.TemporaryDirectory()
        self.source = Path(self.folder.name)
        (self.source / "sample.frm").write_bytes(FORM.encode("cp1252"))
        self.form = vb2qml.parse_form(self.source / "sample.frm")

    def tearDown(self):
        self.folder.cleanup()

    def test_colors_convert_from_bgr_and_system_palette(self):
        self.assertEqual(vb2qml.qml_color("&H00FF8000&"), "#0080ff")
        self.assertEqual(vb2qml.qml_color("&H80000005&"), "#ffffff")
        self.assertEqual(vb2qml.qml_color("16777215"), "#ffffff")

    def test_values_lose_comments_and_unescape_quotes(self):
        self.assertEqual(vb2qml.parse_value("3  'Fixed Dialog"), "3")
        self.assertEqual(vb2qml.parse_value('"a ""b"" c"'), 'a "b" c')
        self.assertEqual(vb2qml.parse_value('$"x.frx":00FF'), ("frx", "x.frx", 255))

    def test_form_name_comes_from_the_attribute_and_handlers_skip_comments(self):
        self.assertEqual(self.form.type_name, "Sample")
        change = [h for h in self.form.handlers if h.event == "Change"][0]
        self.assertEqual((change.control, change.lines), ("first", 1))

    def test_geometry_scales_twips_to_pixels_and_duplicates_get_their_own_id(self):
        qml = vb2qml.Converter(self.source).form_qml(self.form)
        self.assertIn("width: 200\n", qml)
        self.assertIn("id: c_cap_2\n", qml)
        self.assertIn("id: c_cap_2_dup2\n", qml)
        self.assertIn('text: "He said \\"hi\\""', qml)
        self.assertEqual(qml.count('"cap(2)":'), 1)
        self.assertTrue(any("repeated" in warning for warning in self.form.warnings))

    def test_tab_order_follows_tabindex_not_file_order(self):
        qml = vb2qml.Converter(self.source).form_qml(self.form)
        first = qml[qml.index("id: c_first"):]
        self.assertIn("KeyNavigation.tab: c_second", first[:first.index("}")])

    def test_navigation_records_shift_condition_keep_and_back(self):
        keys = vb2qml.parse_navigation(self.form, {"target_page": "Target_page"})
        self.assertEqual(keys["F1"]["0"], [{"to": "Target_page", "conditional": False, "keep": False}])
        self.assertEqual(keys["F1"]["2"], [{"to": "Target_page", "conditional": True, "keep": True}])
        self.assertEqual(keys["ESC"]["0"][0]["to"], "@back")

    def test_frx_picture_blob_is_read_by_its_declared_size(self):
        payload = b"BM" + b"\x01" * 10
        blob = (len(payload) + 8).to_bytes(4, "little") + b"lt\0\0" + len(payload).to_bytes(4, "little") + payload + b"junk"
        (self.source / "p.frx").write_bytes(blob)
        self.assertEqual(vb2qml.Frx(self.source).picture(("frx", "P.FRX", 0)), payload)


class RealFormsTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.output = vb2qml.generate(vb2qml.DEFAULT_SOURCE)

    def test_every_form_is_generated_with_a_unique_name(self):
        forms = [name for name in self.output if name.startswith("src/app/qml/forms/")]
        self.assertEqual(len(forms), 62)
        self.assertEqual(len({name.lower() for name in forms}), 62)

    def test_main_page_navigation_matches_the_vb_handlers(self):
        text = self.output["src/app/qml/nav.js"].decode()
        forms = json.loads(text.split("var forms = ")[1].rstrip().rstrip(";"))
        keys = forms["Pag_pro"]["keys"]
        self.assertEqual(keys["F1"]["0"][0]["to"], "Pag_all")
        self.assertEqual(keys["F9"]["0"][0]["to"], "Pag_lis_ord")
        self.assertEqual([t["to"] for t in keys["F3"]["0"]], ["Pwd_frm", "Set_par_mac"])
        self.assertTrue(forms["Pwd_frm"]["dialog"])
        self.assertFalse(forms["Pag_all"]["dialog"])

    def test_no_control_kind_is_left_without_a_component(self):
        report = self.output["doc/vb-forms.md"].decode()
        self.assertIn("Unmapped control kinds: none", report)


if __name__ == "__main__":
    unittest.main()
