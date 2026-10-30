import QtQuick

Rectangle {
    id: control
    property string text
    property font font
    property color backColor: "#c0c0c0"
    property color foreColor: "#000000"
    property url picture
    property string toolTip
    signal vbEvent(string event, var args)

    activeFocusOnTab: true
    color: touch.pressed ? Qt.darker(backColor, 1.2) : backColor
    border.width: activeFocus ? 2 : 1
    border.color: "#404040"
    opacity: enabled ? 1 : 0.5

    Image {
        anchors.centerIn: parent
        source: control.picture
        visible: source.toString() !== ""
    }
    Text {
        anchors.fill: parent
        anchors.margins: 2
        text: control.text.replace(/&(?!&)/g, "")
        font: control.font
        color: control.foreColor
        wrapMode: Text.Wrap
        horizontalAlignment: Text.AlignHCenter
        verticalAlignment: Text.AlignVCenter
    }
    MouseArea {
        id: touch
        anchors.fill: parent
        onClicked: {
            control.forceActiveFocus()
            control.vbEvent("Click", {})
        }
    }
    Keys.onPressed: (event) => {
        control.vbEvent("KeyDown", { key: event.key, modifiers: event.modifiers })
        if (event.key === Qt.Key_Return || event.key === Qt.Key_Enter || event.key === Qt.Key_Space) {
            control.vbEvent("Click", {})
            event.accepted = true
        }
    }
}
