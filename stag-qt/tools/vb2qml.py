#!/usr/bin/env python3
"""Convert the VB6 forms of ../stag into QML views for stag-qt.

Layout only: geometry, captions, colours, fonts, pictures, tab order and control arrays come
from the .frm/.frx files. The VB event code is not translated; every handler is listed in the
report so the C++ port can be tracked. Output is deterministic, re-run it instead of editing it:

    tools/vb2qml.py            write forms, assets, forms.cmake and the report
    tools/vb2qml.py --check    fail if the committed output differs
"""
import argparse
import hashlib
import json
import re
import sys
from collections import Counter
from dataclasses import dataclass, field
from pathlib import Path

TWIPS_PER_PIXEL = 15.0
DIALOG_MAX_WIDTH = 700
ROOT = Path(__file__).resolve().parent.parent
DEFAULT_SOURCE = ROOT.parent / "stag"
QML_DIR = ROOT / "src" / "app" / "qml"

SYSTEM_COLORS = {
    0x00: "#c0c0c0", 0x01: "#000000", 0x02: "#000080", 0x03: "#808080", 0x04: "#c0c0c0",
    0x05: "#ffffff", 0x06: "#000000", 0x07: "#000000", 0x08: "#000000", 0x09: "#ffffff",
    0x0A: "#c0c0c0", 0x0B: "#c0c0c0", 0x0C: "#808080", 0x0D: "#000080", 0x0E: "#ffffff",
    0x0F: "#c0c0c0", 0x10: "#808080", 0x11: "#808080", 0x12: "#000000", 0x13: "#c0c0c0",
    0x14: "#ffffff", 0x15: "#404040", 0x16: "#c0c0c0", 0x17: "#000000", 0x18: "#ffffe1",
}
PICTURE_FORMATS = {b"BM": "bmp", b"GI": "gif", b"\xff\xd8": "jpg", b"\x00\x00": "ico"}
# Windowed VB controls always paint above the lightweight ones of the same container.
TITLE_STRIP_WIDTH = 798
TITLE_STRIP_HEIGHT = 20
TITLE_STRIP_LOGOS_AT = 559
WINDOWED = {"TextBox", "CommandButton", "Frame", "PictureBox", "ComboBox", "CheckBox", "OptionButton",
            "ListBox", "MSFlexGrid", "DBGrid", "DBCombo", "ProgressBar", "SSPanel", "SSFrame", "SSCommand"}
FOCUSABLE = {"TextBox", "CommandButton", "ComboBox", "DBCombo", "CheckBox", "OptionButton", "ListBox", "MSFlexGrid", "DBGrid"}
NON_VISUAL = {"Timer", "Data", "MSComm", "Winsock", "CommonDialog"}
COMPONENTS = {
    "Label": "VbLabel", "TextBox": "VbTextBox", "CommandButton": "VbButton", "SSCommand": "VbButton",
    "Frame": "VbFrame", "SSFrame": "VbFrame", "SSPanel": "VbFrame", "PictureBox": "VbPicture", "Image": "VbImage",
    "Shape": "VbShape", "Line": "VbLine", "ComboBox": "VbCombo", "ListBox": "VbGrid",
    "CheckBox": "VbCheck", "OptionButton": "VbCheck", "MSFlexGrid": "VbGrid", "DBGrid": "VbGrid",
    "DBCombo": "VbCombo", "ProgressBar": "VbProgress",
}


@dataclass
class Control:
    kind: str
    name: str
    props: dict = field(default_factory=dict)
    font: dict = field(default_factory=dict)
    children: list = field(default_factory=list)
    duplicate: str = ""

    @property
    def index(self):
        return int(self.props["Index"]) if "Index" in self.props else None

    @property
    def key(self):
        return self.name if self.index is None else f"{self.name}({self.index})"

    @property
    def qml_id(self):
        suffix = "" if self.index is None else f"_{self.index}"
        return "c_" + re.sub(r"\W", "_", self.name) + suffix + self.duplicate


@dataclass
class Handler:
    control: str
    event: str
    lines: int


@dataclass
class Form:
    root: Control
    source: Path
    handlers: list
    code: list = field(default_factory=list)
    warnings: list = field(default_factory=list)

    @property
    def type_name(self):
        return self.root.name[0].upper() + self.root.name[1:]


def parse_value(raw):
    """Value text of a `Name = value` line, without the trailing 'comment."""
    raw = raw.strip()
    if raw.startswith(("\"", "$\"")):
        match = re.match(r'\$?"((?:[^"]|"")*)"(?::(\w+))?', raw)
        if match[2] is not None:
            return ("frx", match[1], int(match[2], 16))
        return match[1].replace('""', '"')
    return raw.split("'")[0].strip()


def parse_form(path):
    text = path.read_bytes().decode("cp1252")
    lines = text.splitlines()
    stack, root, position = [], None, 0
    block_depth = 0
    for position, line in enumerate(lines):
        stripped = line.strip()
        if stripped.startswith("Attribute VB_Name"):
            break
        # VB allows a degree sign in a name (txt_cor_tag_reg_1°_sog_att); it is not a valid QML id.
        begin = re.match(r"Begin\s+([\w.]+)\s+([\w\xb0]+)", stripped)
        if begin:
            control = Control(begin[1].split(".")[-1], begin[2].replace("\xb0", "o"))
            if stack:
                stack[-1].children.append(control)
            else:
                root = control
            stack.append(control)
        elif stripped.startswith("BeginProperty"):
            block_depth += 1
        elif stripped == "EndProperty":
            block_depth -= 1
        elif stripped == "End" and stack:
            stack.pop()
        elif "=" in stripped and stack:
            name, value = stripped.split("=", 1)
            name = name.strip()
            if block_depth:
                # Only the control's own font block matters; grid column blocks are skipped.
                stack[-1].font.setdefault(name, parse_value(value))
            else:
                stack[-1].props[name] = parse_value(value)
    if root is None:
        raise ValueError(f"{path.name}: no form found")
    match = re.search(r'Attribute VB_Name = "(\w+)"', text)
    if match:
        root.name = match[1]
    return Form(root, path, parse_handlers(lines[position:]), lines[position:])


def parse_handlers(code):
    handlers, current, count = [], None, 0
    for line in code:
        stripped = line.strip()
        match = re.match(r"(?:(?:Private|Public)\s+)?Sub\s+(\w+?)_(\w+)\s*\(", stripped, re.I)
        if match and current is None:
            current, count = match, 0
            continue
        if current is not None:
            if re.match(r"End\s+Sub\b", stripped, re.I):
                handlers.append(Handler(current[1], current[2], count))
                current = None
            elif stripped and not stripped.startswith("'"):
                count += 1
    return handlers


class Frx:
    """Reads the binary blobs a form refers to; files are looked up ignoring case."""

    def __init__(self, source):
        self.files = {p.name.lower(): p for p in source.iterdir() if p.suffix.lower() == ".frx"}
        self.cache = {}

    def data(self, name):
        key = name.lower()
        if key not in self.files:
            return None
        if key not in self.cache:
            self.cache[key] = self.files[key].read_bytes()
        return self.cache[key]

    def picture(self, ref):
        data = self.data(ref[1])
        offset = ref[2]
        if data is None or data[offset + 4:offset + 8] != b"lt\0\0":
            return None
        size = int.from_bytes(data[offset + 8:offset + 12], "little")
        return data[offset + 12:offset + 12 + size]

    def caption(self, ref):
        data = self.data(ref[1])
        if data is None:
            return ""
        size = int.from_bytes(data[ref[2]:ref[2] + 4], "little")
        return data[ref[2] + 4:ref[2] + 4 + size].decode("cp1252")

    def text(self, ref):
        data = self.data(ref[1])
        if data is None:
            return ""
        size = data[ref[2]]
        return data[ref[2] + 1:ref[2] + 1 + size].decode("cp1252")

    def items(self, ref):
        """List property: u16 count, then u16 length + text per entry."""
        data = self.data(ref[1])
        if data is None:
            return []
        offset = ref[2]
        count = int.from_bytes(data[offset:offset + 2], "little")
        offset += 2
        items = []
        for _ in range(count):
            size = int.from_bytes(data[offset:offset + 2], "little")
            items.append(data[offset + 2:offset + 2 + size].decode("cp1252"))
            offset += 2 + size
        return items


def qml_color(value):
    text = str(value).strip()
    match = re.match(r"&H([0-9A-Fa-f]+)&?", text)
    number = int(match[1], 16) if match else int(text)
    if number & 0x80000000:
        return SYSTEM_COLORS.get(number & 0xFF, "#c0c0c0")
    return "#%02x%02x%02x" % (number & 0xFF, (number >> 8) & 0xFF, (number >> 16) & 0xFF)


def is_true(value):
    return str(value).split()[0] in ("-1", "True", "1")


def is_white_filler(control):
    """Narrow white labels without text sit at the end of the cells of many forms and cut the lines and
    headers that run under them. Not as in the .frm: hidden, so lines and cells stay whole."""
    props = control.props
    if control.kind != "Label" or props.get("Caption") not in (None, ""):
        return False
    if str(props.get("BackStyle", "1")).split()[0] == "0":
        return False
    if str(props.get("BackColor", "")).strip().upper() not in ("&H00FFFFFF&", "&H80000005&"):
        return False
    return float(props.get("Width", 9999)) <= 120 and float(props.get("Height", 0)) >= 2 * float(props.get("Width", 1))


def without_logos(blob):
    """The title strip of the pages has the DryEvolution and Fosber logos painted into its right end: each row
    continues in the colour it has before them."""
    if blob[:2] != b"BM" or len(blob) < 54:
        return blob
    offset = int.from_bytes(blob[10:14], "little")
    width = int.from_bytes(blob[18:22], "little")
    height = int.from_bytes(blob[22:26], "little")
    depth = int.from_bytes(blob[28:30], "little")
    if (width, height, depth) != (TITLE_STRIP_WIDTH, TITLE_STRIP_HEIGHT, 24):
        return blob
    pixels = bytearray(blob)
    stride = (width * 3 + 3) // 4 * 4
    for row in range(height):
        start = offset + row * stride
        colour = pixels[start + (TITLE_STRIP_LOGOS_AT - 1) * 3:start + TITLE_STRIP_LOGOS_AT * 3]
        pixels[start + TITLE_STRIP_LOGOS_AT * 3:start + width * 3] = bytes(colour) * (width - TITLE_STRIP_LOGOS_AT)
    return bytes(pixels)


def px(twips, scale=1.0):
    return round(float(twips) * scale / TWIPS_PER_PIXEL)


def js(value):
    return json.dumps(value, ensure_ascii=False)


class Converter:
    def __init__(self, source):
        self.source = source
        self.frx = Frx(source)
        self.assets = {}
        self.unmapped = Counter()

    def save_picture(self, ref, form):
        blob = self.frx.picture(ref)
        if blob:
            blob = without_logos(blob)
        if not blob:
            form.warnings.append(f"picture {ref[1]}:{ref[2]:04X} not readable")
            return None
        extension = PICTURE_FORMATS.get(blob[:2])
        if extension is None:
            form.warnings.append(f"picture {ref[1]}:{ref[2]:04X} has an unknown format")
            return None
        name = hashlib.sha1(blob).hexdigest()[:16] + "." + extension
        self.assets[name] = blob
        return name

    def resolve(self, control, name, form):
        value = control.props.get(name)
        if isinstance(value, tuple):
            if name == "Caption":
                return self.frx.caption(value)
            if name == "Text":
                return self.frx.text(value)
            form.warnings.append(f"{control.key}.{name}: unexpected binary property")
            return ""
        return value

    def common(self, control, form, scale, tab_links):
        props, out = control.props, []
        if control.kind != "Line":
            if "Left" in props:
                out.append(f"x: {px(props['Left'], scale)}")
            if "Top" in props:
                out.append(f"y: {px(props['Top'], scale)}")
            if "Width" in props:
                out.append(f"width: {px(props['Width'], scale)}")
            if "Height" in props:
                out.append(f"height: {px(props['Height'], scale)}")
        if "Visible" in props and not is_true(props["Visible"]):
            out.append("visible: false")
        if "Enabled" in props and not is_true(props["Enabled"]):
            out.append("enabled: false")
        if control.kind in WINDOWED:
            out.append("z: 1")
        if "BackColor" in props and control.kind not in ("Shape", "Line"):
            out.append(f'backColor: "{qml_color(props["BackColor"])}"')
        if "ForeColor" in props:
            out.append(f'foreColor: "{qml_color(props["ForeColor"])}"')
        if control.kind in ("Label", "TextBox", "Frame", "PictureBox") and "BorderStyle" in props:
            out.append(f"borderStyle: {props['BorderStyle']}")
        if control.font:
            if "Name" in control.font:
                out.append(f"font.family: {js(control.font['Name'])}")
            if "Size" in control.font:
                out.append(f"font.pixelSize: {max(1, round(float(control.font['Size']) * 96 / 72))}")
            if "Weight" in control.font:
                out.append(f"font.bold: {'true' if int(control.font['Weight']) >= 600 else 'false'}")
            if "Italic" in control.font and is_true(control.font["Italic"]):
                out.append("font.italic: true")
            if "Underline" in control.font and is_true(control.font["Underline"]):
                out.append("font.underline: true")
        if "ToolTipText" in props:
            out.append(f"toolTip: {js(props['ToolTipText'])}")
        link = tab_links.get(control.qml_id)
        if link:
            out.append(f"KeyNavigation.tab: {link[0]}")
            out.append(f"KeyNavigation.backtab: {link[1]}")
        out.extend(self.specific(control, form))
        return out

    def specific(self, control, form):
        props, kind, out = control.props, control.kind, []
        caption = self.resolve(control, "Caption", form)
        if kind == "Label":
            if caption:
                out.append(f"text: {js(caption)}")
            if "Alignment" in props:
                out.append(f"alignment: {props['Alignment']}")
            if "BackStyle" in props:
                out.append(f"opaque: {'false' if props['BackStyle'] == '0' else 'true'}")
            if "AutoSize" in props and is_true(props["AutoSize"]):
                out.append("autoSize: true")
            if "WordWrap" in props and is_true(props["WordWrap"]):
                out.append("wordWrap: true")
        elif kind == "TextBox":
            text = self.resolve(control, "Text", form)
            if text:
                out.append(f"text: {js(text)}")
            if "Alignment" in props:
                out.append(f"alignment: {props['Alignment']}")
            if "MaxLength" in props and props["MaxLength"] != "0":
                out.append(f"maxLength: {props['MaxLength']}")
            if "PasswordChar" in props and props["PasswordChar"]:
                out.append("password: true")
            if "Locked" in props and is_true(props["Locked"]):
                out.append("readOnly: true")
            if "MultiLine" in props and is_true(props["MultiLine"]):
                out.append("multiLine: true")
            if "DataField" in props:
                out.append(f"dataField: {js(props['DataField'])}")
            if "DataSource" in props:
                out.append(f"dataSource: {js(props['DataSource'])}")
        elif kind in ("CommandButton", "SSCommand", "Frame", "SSFrame", "SSPanel", "CheckBox", "OptionButton"):
            if caption:
                out.append(f"text: {js(caption)}")
            if kind in ("CheckBox", "OptionButton"):
                out.append(f"exclusive: {'true' if kind == 'OptionButton' else 'false'}")
                if "Value" in props and props["Value"] not in ("0", "False"):
                    out.append("checked: true")
                if props.get("Style", "0").split()[0] == "1":
                    out.append("graphical: true")
        elif kind == "Image":
            if "Stretch" in props and is_true(props["Stretch"]):
                out.append("stretch: true")
        elif kind == "Shape":
            out.append(f"shape: {props.get('Shape', '0').split()[0]}")
            if props.get("FillStyle", "1").split()[0] == "0":
                out.append(f'fillColor: "{qml_color(props.get("FillColor", 0))}"')
            elif props.get("BackStyle", "0").split()[0] == "1":
                out.append(f'fillColor: "{qml_color(props.get("BackColor", "&H80000005&"))}"')
            if "BorderColor" in props:
                out.append(f'borderColor: "{qml_color(props["BorderColor"])}"')
            if "BorderWidth" in props:
                out.append(f"borderWidth: {props['BorderWidth']}")
            if props.get("BorderStyle", "1").split()[0] == "0":
                out.append("borderWidth: 0")
        elif kind == "Line":
            pass
        elif kind in ("MSFlexGrid", "DBGrid", "ListBox"):
            for source, target in (("Rows", "rows"), ("Cols", "cols"), ("FixedRows", "fixedRows"), ("FixedCols", "fixedCols")):
                if source in props:
                    out.append(f"{target}: {props[source]}")
            if kind == "DBGrid":
                out.append("rows: 1")
            if "DataSource" in props:
                out.append(f"dataSource: {js(props['DataSource'])}")
        if isinstance(props.get("List"), tuple):
            out.append(f"items: {js(self.frx.items(props['List']))}")
        if isinstance(props.get("Picture"), tuple):
            asset = self.save_picture(props["Picture"], form)
            if asset:
                out.append(f'picture: Qt.resolvedUrl("../assets/{asset}")')
        return out

    def non_visual(self, control):
        props = control.props
        if control.kind == "Timer":
            return ["VbTimer {", f"    id: {control.qml_id}",
                    f"    interval: {props.get('Interval', '0')}",
                    f"    running: {'false' if 'Enabled' in props and not is_true(props['Enabled']) else 'true'}", "}"]
        if control.kind == "Data":
            return ["VbData {", f"    id: {control.qml_id}",
                    f"    recordSource: {js(str(props.get('RecordSource', '')))}", "}"]
        return ["VbInert {", f"    id: {control.qml_id}", f"    kind: {js(control.kind)}", "}"]

    def emit(self, control, form, depth, scale, tab_links):
        indent = "    " * depth
        if control.kind in NON_VISUAL:
            return [indent + line for line in self.non_visual(control)]
        component = COMPONENTS.get(control.kind)
        if component is None:
            self.unmapped[control.kind] += 1
            form.warnings.append(f"{control.key}: no QML component for {control.kind}")
            component = "VbFrame"
        lines = [f"{indent}{component} {{", f"{indent}    id: {control.qml_id}"]
        if control.kind == "Line":
            props = control.props
            lines += [f"{indent}    x1: {px(props.get('X1', 0), scale)}", f"{indent}    y1: {px(props.get('Y1', 0), scale)}",
                      f"{indent}    x2: {px(props.get('X2', 0), scale)}", f"{indent}    y2: {px(props.get('Y2', 0), scale)}"]
            if "BorderColor" in props:
                lines.append(f'{indent}    borderColor: "{qml_color(props["BorderColor"])}"')
            if "BorderWidth" in props:
                lines.append(f"{indent}    borderWidth: {props['BorderWidth']}")
            if "Visible" in props and not is_true(props["Visible"]):
                lines.append(f"{indent}    visible: false")
        else:
            lines += [f"{indent}    {line}" for line in self.common(control, form, scale, tab_links)]
            if is_white_filler(control):
                lines.append(f"{indent}    visible: false")
        child_scale = container_scale(control)
        # A .frm lists the topmost control first; in QML the last sibling is on top.
        for child in reversed(control.children):
            lines += self.emit(child, form, depth + 1, child_scale, tab_links)
        lines.append(f"{indent}}}")
        return lines

    def form_qml(self, form):
        root = form.root
        props = root.props
        flat = list(walk(root))[1:]
        registered = {}
        for control in flat:
            key = control.key.lower()
            if key in registered:
                # Some .frm files repeat a name and index; the first control keeps the VB name.
                registered[key] += 1
                control.duplicate = f"_dup{registered[key]}"
                form.warnings.append(f"{control.key}: repeated in the form, later copy is {control.qml_id}")
            else:
                registered[key] = 1
        tab_links = build_tab_links(flat)
        width, height = px(props.get("ClientWidth", 0)), px(props.get("ClientHeight", 0))
        body = ["// Generated by tools/vb2qml.py from " + form.source.name + ". Do not edit: re-run the script.",
                "import QtQuick", "import Stag", "", "VbForm {", "    id: form",
                f"    formName: {js(root.name)}", f"    width: {width}", f"    height: {height}"]
        if "BackColor" in props:
            body.append(f'    backColor: "{qml_color(props["BackColor"])}"')
        body.append(f"    dialog: {'true' if is_dialog(form) else 'false'}")
        caption = self.resolve(root, "Caption", form)
        if caption:
            body.append(f"    caption: {js(caption)}")
        if isinstance(props.get("Picture"), tuple):
            asset = self.save_picture(props["Picture"], form)
            if asset:
                body.append(f'    picture: Qt.resolvedUrl("../assets/{asset}")')
        registry = ", ".join(f"{js(c.key.lower())}: {c.qml_id}" for c in flat if not c.duplicate)
        body.append(f"    controls: ({{ {registry} }})")
        scale = container_scale(root)
        # A .frm lists the topmost control first; in QML the last sibling is on top.
        for child in reversed(root.children):
            body += self.emit(child, form, 1, scale, tab_links)
        body.append("}")
        return "\n".join(body) + "\n"


def is_dialog(form):
    """Pages fill the 800 px width whatever their BorderStyle; only smaller forms open on top."""
    return px(form.root.props.get("ClientWidth", 0)) < DIALOG_MAX_WIDTH


def walk(control):
    yield control
    for child in control.children:
        yield from walk(child)


def container_scale(control):
    """A user ScaleMode maps child coordinates through ScaleWidth to the real width. Some forms keep a
    ScaleWidth from the designer while their children are in twips: those already fit the client area."""
    props = control.props
    outer = props.get("ClientWidth") or props.get("Width")
    inner = props.get("ScaleWidth")
    if props.get("ScaleMode") is None or inner is None or outer is None or float(inner) == float(outer):
        return 1.0
    right = 0.0
    for child in control.children:
        if "Left" in child.props and "Width" in child.props:
            right = max(right, float(child.props["Left"]) + float(child.props["Width"]))
    if right <= float(outer) * 1.05:
        return 1.0
    return float(outer) / float(inner)



def build_tab_links(controls):
    """VB's TabIndex order as KeyNavigation, because QML would otherwise follow item order."""
    stops = [c for c in controls if c.kind in FOCUSABLE and "TabIndex" in c.props
             and not ("TabStop" in c.props and not is_true(c.props["TabStop"]))]
    stops.sort(key=lambda c: int(c.props["TabIndex"]))
    links = {}
    if len(stops) > 1:
        for position, control in enumerate(stops):
            links[control.qml_id] = (stops[(position + 1) % len(stops)].qml_id, stops[position - 1].qml_id)
    return links


def parse_navigation(form, names):
    """Page changes of the eve_key_<key> handlers: {key: {shift: [{to, conditional, keep}]}}."""
    keys, current, shift, ifs, selects = {}, None, 0, 0, 0
    for line in form.code:
        stripped = line.strip()
        if stripped.startswith("'"):
            continue
        start = re.match(r"(?:Private\s+|Public\s+)?Sub\s+eve_key_(F\d+|esc)\s*\(", stripped, re.I)
        if start:
            current, shift, ifs, selects = start[1].upper() if start[1][0] in "fF" else "ESC", 0, 0, 0
            keys.setdefault(current, {})
            continue
        if current is None:
            continue
        code = stripped.split(" '")[0].strip()
        lower = code.lower()
        if re.match(r"end\s+sub\b", lower):
            current = None
        elif re.match(r"select\s+case\b", lower):
            selects += 1
        elif re.match(r"end\s+select\b", lower):
            selects -= 1
        elif selects == 1 and re.match(r"case\s+\d+", lower):
            shift = int(lower.split()[1])
        elif selects == 1 and re.match(r"case\s+else", lower):
            shift = -1
        elif re.match(r"if\b.*\bthen$", lower):
            ifs += 1
        elif re.match(r"end\s+if\b", lower):
            ifs -= 1
        target = re.search(r"eve_key\.chg\s+(\w+)(\s*,.*)?", code, re.I)
        if target and shift >= 0:
            name = target[1].lower()
            if name == "pag_pre":
                to = "@back"
            elif name in names:
                to = names[name]
            else:
                form.warnings.append(f"eve_key_{current}: unknown target {target[1]}")
                continue
            single_line_if = re.match(r"if\b.*\bthen\b.+", lower) is not None
            keys[current].setdefault(str(shift), []).append(
                {"to": to, "conditional": ifs > 0 or single_line_if, "keep": bool(target[2])})
    return keys


def navigation_js(forms):
    names = {f.root.name.lower(): f.type_name for f in forms}
    table = {}
    for form in sorted(forms, key=lambda f: f.type_name):
        props = form.root.props
        table[form.type_name] = {
            "width": px(props.get("ClientWidth", 0)), "height": px(props.get("ClientHeight", 0)),
            "dialog": is_dialog(form),
            "keys": parse_navigation(form, names),
        }
    return ("// Generated by tools/vb2qml.py from the eve_key_* handlers. Do not edit: re-run the script.\n"
            ".pragma library\n\nvar forms = " + json.dumps(table, indent=1, sort_keys=True) + ";\n")


def report(forms, unmapped, assets):
    total_lines = sum(h.lines for f in forms for h in f.handlers)
    out = ["# VB6 form conversion report", "",
           "Generated by `tools/vb2qml.py`. A form's layout is converted; its VB handlers listed here hold the",
           "behaviour that has to be ported to C++ by hand (they drive the PLC and the order files).", "",
           f"- Forms: {len(forms)}", f"- Controls: {sum(len(list(walk(f.root))) - 1 for f in forms)}",
           f"- Pictures: {len(assets)} unique", f"- Event handlers: {sum(len(f.handlers) for f in forms)} ({total_lines} code lines)",
           f"- Unmapped control kinds: {dict(sorted(unmapped.items())) or 'none'}", "",
           "| Form | Source | Size | Controls | Handlers | Code lines | Warnings |", "|---|---|---|---:|---:|---:|---:|"]
    for form in sorted(forms, key=lambda f: f.root.name.lower()):
        props = form.root.props
        out.append(f"| {form.type_name} | {form.source.name} | {px(props.get('ClientWidth', 0))}x{px(props.get('ClientHeight', 0))} | "
                   f"{len(list(walk(form.root))) - 1} | {len(form.handlers)} | {sum(h.lines for h in form.handlers)} | {len(form.warnings)} |")
    warnings = [(f.type_name, w) for f in forms for w in f.warnings]
    if warnings:
        out += ["", "## Warnings", ""] + [f"- {name}: {text}" for name, text in warnings]
    out += ["", "## Handlers per form", ""]
    for form in sorted(forms, key=lambda f: f.root.name.lower()):
        if form.handlers:
            items = ", ".join(f"`{h.control}_{h.event}` ({h.lines})" for h in form.handlers)
            out += [f"**{form.type_name}**: {items}", ""]
    return "\n".join(out).rstrip() + "\n"


def generate(source):
    """Returns {relative path: bytes} for everything the script owns."""
    converter = Converter(source)
    paths = sorted(p for p in source.iterdir() if p.suffix.lower() == ".frm")
    forms = [parse_form(path) for path in paths]
    lower_names = Counter(f.type_name.lower() for f in forms)
    clashes = [name for name, count in lower_names.items() if count > 1]
    if clashes:
        raise ValueError(f"form names clash ignoring case: {clashes}")
    output = {}
    for form in forms:
        output[f"src/app/qml/forms/{form.type_name}.qml"] = converter.form_qml(form).encode()
    for name, blob in converter.assets.items():
        output[f"src/app/qml/assets/{name}"] = blob
    output["src/app/qml/nav.js"] = navigation_js(forms).encode()
    files = sorted(f"qml/forms/{f.type_name}.qml" for f in forms) + ["qml/nav.js"]
    assets = sorted(f"qml/assets/{name}" for name in converter.assets)
    cmake = ["# Generated by tools/vb2qml.py.", "set(STAG_FORM_QML", *[f"    {f}" for f in files], ")",
             "set(STAG_FORM_ASSETS", *[f"    {a}" for a in assets], ")", ""]
    output["src/app/forms.cmake"] = "\n".join(cmake).encode()
    output["doc/vb-forms.md"] = report(forms, converter.unmapped, converter.assets).encode()
    return output


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--source", type=Path, default=DEFAULT_SOURCE, help="directory with the .frm/.frx files")
    parser.add_argument("--check", action="store_true", help="only compare with the files on disk")
    args = parser.parse_args()
    output = generate(args.source)
    owned = {str(p.relative_to(ROOT)) for folder in ("forms", "assets") for p in (QML_DIR / folder).glob("*")}
    stale = sorted(owned - set(output))
    changed = [name for name, data in output.items() if not (ROOT / name).exists() or (ROOT / name).read_bytes() != data]
    if args.check:
        for name in changed + stale:
            print(f"out of date: {name}", file=sys.stderr)
        return 1 if changed or stale else 0
    for name in stale:
        (ROOT / name).unlink()
    for name in changed:
        path = ROOT / name
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_bytes(output[name])
    print(f"{len(output)} files, {len(changed)} written, {len(stale)} removed")
    return 0


if __name__ == "__main__":
    sys.exit(main())
