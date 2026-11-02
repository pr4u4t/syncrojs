import QtQuick
import Stag

Item {
    id: control
    property url picture
    property bool stretch: false
    property string toolTip
    signal vbEvent(string event, var args)

    // The purple gradient of the page titles, in the new look a plain bar.
    readonly property bool titleBar: Theme.modern && control.picture.toString().endsWith("d99758855f95eeea.bmp")
    // The arrows beside the order captions: gone from the new look, the caption takes their place.
    readonly property bool arrow: Theme.modern && ["b5f08916fee0ceb3.bmp", "6fc976842c7c0131.bmp"].some((name) => control.picture.toString().endsWith(name))
    Component.onCompleted: {
        if (!arrow)
            return
        for (const sibling of parent.children) {
            if (sibling.centered !== undefined) {
                sibling.x = 0
                sibling.y = 0
                sibling.width = parent.width
                sibling.height = parent.height
                sibling.alignment = 2
                sibling.centered = true
            }
        }
    }
    Rectangle {
        anchors.fill: parent
        visible: control.titleBar
        color: Theme.header
    }
    Image {
        anchors.fill: control.stretch ? parent : undefined
        visible: !control.titleBar && !control.arrow
        source: control.picture
    }
    MouseArea {
        anchors.fill: parent
        onClicked: control.vbEvent("Click", {})
    }
}
