import QtQuick

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
    property string toolTip
    signal vbEvent(string event, var args)

    color: opaque ? backColor : "transparent"
    border.width: borderStyle === 1 ? 1 : 0
    border.color: "#000000"
    clip: !autoSize

    Text {
        id: label
        anchors.fill: parent
        anchors.margins: control.borderStyle === 1 ? 1 : 0
        color: control.foreColor
        wrapMode: control.autoSize && !control.wordWrap ? Text.NoWrap : Text.Wrap
        horizontalAlignment: [Text.AlignLeft, Text.AlignRight, Text.AlignHCenter][control.alignment]
        onTextChanged: if (control.autoSize && !control.wordWrap) control.width = implicitWidth
    }

    MouseArea {
        anchors.fill: parent
        onClicked: control.vbEvent("Click", {})
        onDoubleClicked: control.vbEvent("DblClick", {})
    }
}
