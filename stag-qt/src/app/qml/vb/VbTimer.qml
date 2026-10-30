import QtQuick

Timer {
    id: control
    signal vbEvent(string event, var args)

    repeat: true
    onTriggered: control.vbEvent("Timer", {})
}
