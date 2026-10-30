import QtQuick
import QtQuick.Controls.Basic

// VB.ComboBox and DBCombo in dropdown style. A model selects by listIndex or by text.
ComboBox {
    id: control
    property var items: []
    property string text
    property int listIndex: -1
    property color backColor: "#ffffff"
    property color foreColor: "#000000"
    property string toolTip
    property bool modelFocus: false
    property int focusCount: 0
    signal vbEvent(string event, var args)

    function selectText() {
        const wanted = text.trim()
        currentIndex = model.findIndex ? model.findIndex((item) => String(item).trim() === wanted) : -1
    }

    model: items
    leftPadding: 2
    rightPadding: 14
    topPadding: 0
    bottomPadding: 0
    implicitHeight: 21
    background: Rectangle {
        color: control.backColor
        border.color: "#404040"
        border.width: control.activeFocus ? 2 : 1
    }
    indicator: Text {
        x: control.width - width - 2
        anchors.verticalCenter: parent.verticalCenter
        text: "\u25BC"
        font.pixelSize: 8
    }
    contentItem: Text {
        text: control.displayText
        font: control.font
        color: control.foreColor
        verticalAlignment: Text.AlignVCenter
        elide: Text.ElideRight
    }
    onItemsChanged: {
        if (text !== "")
            selectText()
        else
            currentIndex = listIndex
    }
    onTextChanged: selectText()
    onListIndexChanged: currentIndex = listIndex
    onFocusCountChanged: if (focusCount > 0) control.forceActiveFocus()
    onActiveFocusChanged: control.vbEvent(activeFocus ? "GotFocus" : "LostFocus", {})
    onActivated: (index) => {
        control.text = control.textAt(index)
        control.vbEvent("Change", { text: control.textAt(index) })
        control.vbEvent("Click", { index: index })
    }
    Keys.onPressed: (event) => {
        control.vbEvent("KeyDown", { key: event.key, modifiers: event.modifiers })
        // ges_vbkeydbCombo: the arrows left and right go through the list.
        if (control.modelFocus && (event.key === Qt.Key_Left || event.key === Qt.Key_Right)) {
            const next = currentIndex + (event.key === Qt.Key_Left ? -1 : 1)
            if (next >= 0 && next < count) {
                currentIndex = next
                control.activated(next)
            }
            event.accepted = true
            return
        }
        if (control.modelFocus && [Qt.Key_Return, Qt.Key_Enter, Qt.Key_Tab, Qt.Key_Backtab, Qt.Key_Up, Qt.Key_Down].includes(event.key))
            event.accepted = true
    }
}
