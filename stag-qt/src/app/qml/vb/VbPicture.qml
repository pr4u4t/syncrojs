import QtQuick

Rectangle {
    id: control
    property color backColor: "#c0c0c0"
    property color foreColor: "#000000"
    property url picture
    property int borderStyle: 1
    property font font
    property string toolTip
    signal vbEvent(string event, var args)

    color: backColor
    border.width: borderStyle === 1 ? 1 : 0
    border.color: "#404040"
    clip: true

    Image {
        source: control.picture
        visible: source.toString() !== ""
        z: -1
    }
    MouseArea {
        anchors.fill: parent
        z: -2
        onClicked: control.vbEvent("Click", {})
    }
}
