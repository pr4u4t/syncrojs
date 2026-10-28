import QtQuick

// Counterpart of sta_bar. Bound to StaBarModel (context property "staBar"); the exact sta_bar.frm geometry comes later.
Rectangle {
    id: bar
    height: 116
    color: "#c0c0c0"

    Text {
        anchors { horizontalCenter: parent.horizontalCenter; bottom: parent.bottom; bottomMargin: 2 }
        text: staBar.labMsg
        color: staBar.labMsgFore
        font.pixelSize: 14
        font.bold: true
    }

    component Frame: Rectangle {
        property string title
        property alias content: inner.data
        width: 120; height: 84
        border.color: "#404040"
        Text { id: caption; text: parent.title; color: "white"; font.pixelSize: 11; x: 4; y: 2 }
        Item { id: inner; anchors { left: parent.left; right: parent.right; top: caption.bottom; bottom: parent.bottom; margins: 4 } }
    }
    component Field: Rectangle {
        property alias text: label.text
        property alias textColor: label.color
        width: parent ? parent.width : 100; height: 22
        color: "white"
        Text { id: label; anchors.centerIn: parent; font.pixelSize: 13; elide: Text.ElideRight; width: parent.width - 4; horizontalAlignment: Text.AlignHCenter }
    }
    component Lamp: Column {
        property string label
        property color lampColor
        spacing: 2
        Rectangle { width: 16; height: 16; radius: 8; color: parent.lampColor; border.color: "black"; anchors.horizontalCenter: parent.horizontalCenter }
        Text { text: parent.label; font.pixelSize: 10; color: "white"; anchors.horizontalCenter: parent.horizontalCenter }
    }

    Row {
        anchors { left: parent.left; top: parent.top; margins: 6 }
        spacing: 6

        Frame {
            title: "COM"
            width: 90
            color: staBar.frmComBack
            content: [
                Grid {
                    columns: 3; spacing: 4
                    Text { text: "PLC"; color: "white"; font.pixelSize: 11 }
                    Rectangle { width: 12; height: 12; color: staBar.ledPlcTx }
                    Rectangle { width: 12; height: 12; color: staBar.ledPlcRx }
                    Text { text: "SPDE"; color: "white"; font.pixelSize: 11 }
                    Rectangle { width: 12; height: 12; color: "#808080" }
                    Rectangle { width: 12; height: 12; color: "#808080" }
                },
                Rectangle { visible: staBar.linNoComPlc; width: 80; height: 3; rotation: -20; y: 10; color: "black" },
                Rectangle { visible: staBar.linNoComSpde; width: 80; height: 3; rotation: -20; y: 30; color: staBar.linNoComSpdeColor },
                Text { text: staBar.labStaMac; visible: text !== ""; color: "yellow"; font.bold: true; font.pixelSize: 18; anchors.right: parent.right; anchors.bottom: parent.bottom }
            ]
        }
        Frame {
            title: "RUN"
            width: 170
            color: staBar.frmStaRunBack
            content: Field { y: 6; text: staBar.txtStaRun; textColor: staBar.txtStaRunFore; color: staBar.txtStaRunBack }
        }
        Frame {
            title: "RESET"
            width: 140
            color: staBar.frmStaResBack
            content: Field { y: 6; text: staBar.txtStaRes; textColor: staBar.txtStaResFore }
        }
        Frame {
            title: "MODE"
            width: 80
            color: staBar.frmModBack
            content: Field { y: 6; text: staBar.txtMod; textColor: staBar.txtModFore }
        }
        Frame {
            title: "SPEED / MAX"
            width: 110
            color: staBar.frmVelBack
            content: Column {
                spacing: 4; width: parent.width
                Field { text: staBar.txtVel }
                Field { text: staBar.txtVelMax }
            }
        }
        Frame {
            title: "METERS / CO"
            width: 100
            color: staBar.frmMetManBack || "#008000"
            content: Column {
                spacing: 4; width: parent.width
                Field { text: staBar.txtMetMan; textColor: staBar.txtMetManFore || "black" }
                Field { text: staBar.txtTemCo }
            }
        }
        Frame {
            title: "ALARMS"
            width: 150
            color: "#008000"
            content: Row {
                spacing: 6; y: 8
                Lamp { label: "HW"; lampColor: staBar.shpAllHar }
                Lamp { label: "QE"; lampColor: staBar.shpAllQe }
                Lamp { label: "BM"; lampColor: staBar.shpAllBm }
                Lamp { label: "CON"; lampColor: staBar.shpAllCon }
                Lamp { label: "MAL"; lampColor: staBar.shpAllMal }
            }
        }
    }

    Text {
        id: clock
        anchors { right: parent.right; rightMargin: 8; bottom: parent.bottom; bottomMargin: 6 }
        font.pixelSize: 13
        text: Qt.formatDateTime(new Date(), "dd/MM/yyyy\nhh:mm:ss")
        horizontalAlignment: Text.AlignRight
        Timer { interval: 1000; running: true; repeat: true; onTriggered: clock.text = Qt.formatDateTime(new Date(), "dd/MM/yyyy\nhh:mm:ss") }
    }
}
