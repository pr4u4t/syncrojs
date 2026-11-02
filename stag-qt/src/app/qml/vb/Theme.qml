pragma Singleton
import QtQuick

// The look of the controls, chosen with [Gui] Style in stag.ini. "legacy" gives back every colour as the
// .frm has it. "normal" swaps each colour for one of the palette below; distinct colours stay distinct,
// because on these pages a colour can be a state (a grey order, a red lamp).
QtObject {
    readonly property bool modern: typeof stagUi !== "undefined" && stagUi !== null && stagUi.modernGui

    readonly property color ink: "#1c2733"
    readonly property color accent: "#2563b0"
    readonly property color hairline: "#d3dae2"
    readonly property color edge: "#b4bfca"
    readonly property color canvas: "#eef1f5"
    readonly property color header: "#2f3e4e"

    readonly property var backs: ({
        "#c0c0c0": "#eef1f5", "#000000": "#2f3e4e", "#0000ff": "#2563b0", "#000080": "#1e3a5f",
        "#008000": "#2f3e4e", "#7b71b5": "#2f3e4e", "#7b71b6": "#2f3e4e", "#7b70b5": "#2f3e4e", "#00ff00": "#22b35e",
        "#ff0000": "#dc3545", "#ffffc0": "#fff7d1", "#fefec0": "#fff7d1", "#ffff80": "#ffeb99",
        "#7a857c": "#5b6b7b", "#6d8d83": "#5b6b7b", "#748b7c": "#5b6b7b", "#808080": "#8a97a5",
        "#008080": "#14808a", "#ffc0c0": "#fbd5d8", "#ffe0c0": "#fde6cf", "#c0ffff": "#d3f2f5"
    })
    readonly property var fores: ({
        "#000000": "#1c2733", "#808080": "#6b7a89", "#0000c0": "#2563b0", "#0000ff": "#2563b0",
        "#000080": "#1e3a5f", "#ff0000": "#c62828", "#c00000": "#c62828", "#800000": "#8e1f1f",
        "#008000": "#1b8a4b", "#7b71b6": "#2563b0", "#804040": "#7a4a3f", "#400000": "#4a2a22",
        "#ff8000": "#c9650c", "#008080": "#14808a"
    })

    function pick(table, value) {
        if (!modern)
            return value
        const mapped = table[String(value).toLowerCase()]
        return mapped !== undefined ? mapped : value
    }
    function back(value) { return pick(backs, value) }
    function fore(value) { return pick(fores, value) }
    function border(value, legacy) { return modern ? value : legacy }
    function radius(value) { return modern ? value : 0 }
}
