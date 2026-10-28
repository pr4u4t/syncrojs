import QtQuick

// Ten function keys usable by touch or keyboard; an empty label disables the key.
Row {
    id: bar
    property var labels: []
    signal triggered(int number)

    function activate(number) {
        if (labels[number - 1])
            triggered(number)
    }

    height: 56
    spacing: 2

    Repeater {
        model: 10
        Rectangle {
            required property int index
            readonly property string label: bar.labels[index] || ""
            width: (bar.width - 9 * bar.spacing) / 10
            height: bar.height
            color: touch.pressed ? "#808080" : label ? "#e0e0e0" : "#b0b0b0"
            border.color: "#404040"

            Text {
                anchors.fill: parent
                anchors.margins: 2
                text: "F" + (index + 1) + (parent.label ? "\n" + parent.label : "")
                horizontalAlignment: Text.AlignHCenter
                verticalAlignment: Text.AlignVCenter
                wrapMode: Text.WordWrap
                font.pixelSize: 12
            }

            MouseArea {
                id: touch
                anchors.fill: parent
                enabled: parent.label !== ""
                onClicked: bar.activate(index + 1)
            }
        }
    }
}
