import QtQuick

Rectangle {
    property real value: 0
    property real maximum: 100
    property color backColor: "#ffffff"
    property color foreColor: "#000080"

    color: backColor
    border.color: "#404040"

    Rectangle {
        x: 1
        y: 1
        height: parent.height - 2
        width: (parent.width - 2) * Math.min(1, parent.value / Math.max(1, parent.maximum))
        color: parent.foreColor
    }
}
