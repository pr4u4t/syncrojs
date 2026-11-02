import QtQuick
import Stag

Item {
    id: control
    property string text
    property font font
    property bool checked: false
    property bool exclusive: false
    property color backColor: "#c0c0c0"
    property color foreColor: "#000000"
    property string toolTip
    // Style = 1: drawn as a button that stays pressed, with no box.
    property bool graphical: false
    property bool modelFocus: false
    property int focusCount: 0
    signal vbEvent(string event, var args)

    function toggle() {
        checked = exclusive ? true : !checked
        vbEvent("Click", { checked: checked })
    }

    onFocusCountChanged: if (focusCount > 0) control.forceActiveFocus()

    activeFocusOnTab: true
    Keys.onPressed: (event) => {
        vbEvent("KeyDown", { key: event.key, modifiers: event.modifiers })
        if (event.key === Qt.Key_Space || (modelFocus && (event.key === Qt.Key_Return || event.key === Qt.Key_Enter))) {
            toggle()
            event.accepted = true
        } else if (modelFocus && [Qt.Key_Left, Qt.Key_Right, Qt.Key_Tab, Qt.Key_Backtab].includes(event.key)) {
            event.accepted = true
        }
    }

    Rectangle {
        anchors.fill: parent
        color: Theme.modern && control.graphical && control.checked ? Theme.accent : Theme.back(control.backColor)
        radius: control.graphical ? Theme.radius(6) : 0
        border.width: control.graphical ? (control.activeFocus ? 2 : 1) : 0
        border.color: Theme.border(Theme.edge, "#404040")
    }
    Rectangle {
        id: box
        visible: !control.graphical
        width: 13
        height: 13
        anchors.verticalCenter: parent.verticalCenter
        radius: control.exclusive ? 7 : Theme.radius(3)
        color: Theme.modern && control.checked ? Theme.accent : "#ffffff"
        border.color: Theme.modern ? (control.activeFocus || control.checked ? Theme.accent : Theme.edge) : control.activeFocus ? "#000080" : "#404040"
        Rectangle {
            anchors.centerIn: parent
            width: 7
            height: 7
            radius: control.exclusive ? 4 : Theme.radius(1)
            color: Theme.modern ? "#ffffff" : "#000000"
            visible: control.checked
        }
    }
    Text {
        anchors { left: control.graphical ? parent.left : box.right; leftMargin: control.graphical ? 0 : 4; right: parent.right; verticalCenter: parent.verticalCenter }
        horizontalAlignment: control.graphical ? Text.AlignHCenter : Text.AlignLeft
        text: control.text.replace(/&(?!&)/g, "")
        font: control.font
        color: Theme.modern && control.graphical && control.checked ? "#ffffff" : Theme.fore(control.foreColor)
        elide: Text.ElideRight
    }
    MouseArea {
        anchors.fill: parent
        onClicked: control.toggle()
    }
}
