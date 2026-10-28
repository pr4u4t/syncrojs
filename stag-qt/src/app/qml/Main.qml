import QtQuick
import QtQuick.Window

// Shell of the original layout: F1-F10 bar on top (tasti_stag), page area, status bar (sta_bar).
// Designed at 800x600 like the VB forms and scaled to the panel.
Window {
    id: root
    width: 800
    height: 600
    visible: true
    title: "STAG"
    color: "#c0c0c0"

    readonly property real scaleFactor: Math.min(width / 800, height / 600)

    Item {
        width: 800
        height: 600
        scale: root.scaleFactor
        transformOrigin: Item.TopLeft
        focus: true

        Keys.onPressed: (event) => {
            if (event.key >= Qt.Key_F1 && event.key <= Qt.Key_F10) {
                functionBar.activate(event.key - Qt.Key_F1 + 1)
                event.accepted = true
            }
        }

        FunctionBar {
            id: functionBar
            anchors { left: parent.left; right: parent.right; top: parent.top }
            labels: ["", "", "", "", "", "", "", "", "", ""]
            onTriggered: (number) => pageArea.lastKey = number
        }

        Rectangle {
            id: pageArea
            property int lastKey: 0
            anchors { left: parent.left; right: parent.right; top: functionBar.bottom; bottom: statusBar.top }
            color: "#d4d0c8"
            Text {
                anchors.centerIn: parent
                text: pageArea.lastKey ? "F" + pageArea.lastKey : "STAG"
                font.pixelSize: 28
            }
        }

        StatusBar {
            id: statusBar
            anchors { left: parent.left; right: parent.right; bottom: parent.bottom }
        }
    }
}
