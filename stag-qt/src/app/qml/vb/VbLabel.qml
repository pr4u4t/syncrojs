import QtQuick
import Stag

Rectangle {
    id: control
    property alias text: label.text
    property alias font: label.font
    property color backColor: "#c0c0c0"
    property color foreColor: "#000000"
    property int alignment: 0
    property bool opaque: true
    property bool autoSize: false
    property bool wordWrap: false
    property int borderStyle: 0
    // Set by the new look only: VB text always starts at the top.
    property bool centered: false
    property string toolTip
    // What the text lies on. White on the VB grey would vanish on the light grey of the new look.
    readonly property color groundColor: opaque || !parent || parent.backColor === undefined ? backColor : parent.backColor
    signal vbEvent(string event, var args)

    color: !opaque ? "transparent" : Theme.modern && Math.min(width, height) <= 6 && text === "" ? Theme.hairline : Theme.back(backColor)
    border.width: borderStyle === 1 ? 1 : 0
    border.color: Theme.border(Theme.hairline, "#000000")
    clip: !autoSize

    Text {
        id: label
        anchors.fill: parent
        anchors.margins: control.borderStyle === 1 ? 1 : 0
        color: Theme.modern && Qt.colorEqual(control.groundColor, "#c0c0c0") && Qt.colorEqual(control.foreColor, "#ffffff") ? "#6b7a89" : Theme.fore(control.foreColor)
        wrapMode: control.autoSize && !control.wordWrap ? Text.NoWrap : Text.Wrap
        verticalAlignment: control.centered ? Text.AlignVCenter : Text.AlignTop
        horizontalAlignment: [Text.AlignLeft, Text.AlignRight, Text.AlignHCenter][control.alignment]
        onTextChanged: if (control.autoSize && !control.wordWrap) control.width = implicitWidth
    }

    MouseArea {
        anchors.fill: parent
        onClicked: control.vbEvent("Click", {})
        onDoubleClicked: control.vbEvent("DblClick", {})
    }
}
