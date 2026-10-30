import QtQuick

Item {
    id: control
    property url picture
    property bool stretch: false
    property string toolTip
    signal vbEvent(string event, var args)

    Image {
        anchors.fill: control.stretch ? parent : undefined
        source: control.picture
    }
    MouseArea {
        anchors.fill: parent
        onClicked: control.vbEvent("Click", {})
    }
}
