import QtQuick
import Stag

// VB.Shape: 0 rectangle, 1 square, 2 oval, 3 circle, 4 rounded rectangle, 5 rounded square.
Item {
    id: control
    property int shape: 0
    property color fillColor: "transparent"
    property color borderColor: "#000000"
    property int borderWidth: 1

    Rectangle {
        readonly property bool equalSides: control.shape === 1 || control.shape === 3 || control.shape === 5
        readonly property real side: Math.min(control.width, control.height)
        anchors.centerIn: parent
        width: equalSides ? side : control.width
        height: equalSides ? side : control.height
        radius: control.shape === 2 || control.shape === 3 ? Math.min(width, height) / 2
                : control.shape >= 4 ? Math.min(width, height) / 6 : 0
        color: Theme.modern && Math.min(control.width, control.height) <= 6 ? Theme.hairline : Theme.back(control.fillColor)
        border.width: control.borderWidth
        border.color: Theme.modern && control.shape < 2 ? Theme.hairline : Theme.back(control.borderColor)
    }
}
