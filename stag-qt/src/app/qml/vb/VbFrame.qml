import QtQuick

// VB.Frame: children use the frame's top-left as origin, the caption sits on the top border.
Rectangle {
    id: control
    property string text
    property font font
    property color backColor: "#c0c0c0"
    property color foreColor: "#000000"
    property int borderStyle: 1
    property string toolTip
    signal vbEvent(string event, var args)

    color: backColor

    Rectangle {
        anchors { fill: parent; topMargin: caption.visible ? caption.height / 2 : 0 }
        z: -1
        color: "transparent"
        border.width: control.borderStyle === 0 ? 0 : 1
        border.color: "#808080"
    }
    Text {
        id: caption
        x: 8
        z: -1
        visible: control.text !== ""
        text: control.text.replace(/&(?!&)/g, "")
        font: control.font
        color: control.foreColor
        Rectangle { anchors.fill: parent; z: -1; color: control.backColor }
    }
    MouseArea {
        anchors.fill: parent
        z: -2
        onClicked: control.vbEvent("Click", {})
    }
}
