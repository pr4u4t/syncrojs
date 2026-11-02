import QtQuick
import Stag

Rectangle {
    property real x1
    property real y1
    property real x2
    property real y2
    property color borderColor: "#000000"
    property int borderWidth: 1

    x: x1
    y: y1
    width: Math.max(1, Math.hypot(x2 - x1, y2 - y1))
    height: borderWidth
    color: Theme.modern ? Theme.hairline : borderColor
    transformOrigin: Item.TopLeft
    rotation: Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI
}
