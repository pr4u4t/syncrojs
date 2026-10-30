import QtQuick

FocusScope {
    id: control
    property string text
    property font font
    property color backColor: "#ffffff"
    property color foreColor: "#000000"
    property int alignment: 0
    property int maxLength: 32767
    property bool password: false
    property bool readOnly: false
    property bool multiLine: false
    property int borderStyle: 1
    property string dataField
    property string dataSource
    property string toolTip
    // Set by a model: it decides where Return, Tab and the arrows go, and moves the focus with focusCount.
    property bool modelFocus: false
    property int focusCount: 0
    // The _KeyPress filters: UCase$ and get_num.
    property bool upperCase: false
    property bool digitsOnly: false
    property string extraChars
    signal vbEvent(string event, var args)

    onFocusCountChanged: if (focusCount > 0) control.forceActiveFocus()
    onTextChanged: {
        // VB's MaxLength limits typing only; QML would also cut a longer value set from code.
        input.maximumLength = Math.max(maxLength, text.length)
        if (input.text !== text) {
            input.text = text
            // A value set from code shows from its start, not scrolled to the cursor at the end.
            if (!input.activeFocus)
                input.cursorPosition = 0
        }
        if (edit.text !== text)
            edit.text = text
    }

    function handleKey(event) {
        control.vbEvent("KeyDown", { key: event.key, modifiers: event.modifiers })
        if (control.modelFocus && [Qt.Key_Return, Qt.Key_Enter, Qt.Key_Tab, Qt.Key_Backtab, Qt.Key_Up, Qt.Key_Down].includes(event.key)) {
            event.accepted = true
            return
        }
        if (event.text.length === 1)
            control.vbEvent("KeyPress", { ascii: event.text.charCodeAt(0) })
    }
    function edited(value) {
        let filtered = value
        if (control.upperCase)
            filtered = filtered.toUpperCase()
        if (control.digitsOnly)
            filtered = filtered.split("").filter((c) => (c >= "0" && c <= "9") || extraChars.includes(c)).join("")
        if (input.text !== filtered)
            input.text = filtered
        if (filtered !== control.text) {
            control.text = filtered
            control.vbEvent("Change", { text: filtered })
        }
    }

    onActiveFocusChanged: {
        if (activeFocus)
            input.selectAll()
        control.vbEvent(activeFocus ? "GotFocus" : "LostFocus", {})
    }

    Rectangle {
        anchors.fill: parent
        color: control.backColor
        border.width: control.borderStyle === 1 ? 1 : 0
        border.color: "#404040"
        clip: true

        TextInput {
            id: input
            anchors.fill: parent
            anchors.margins: 2
            visible: !control.multiLine
            focus: !control.multiLine
            font: control.font
            color: control.foreColor
            maximumLength: control.maxLength
            readOnly: control.readOnly
            echoMode: control.password ? TextInput.Password : TextInput.Normal
            horizontalAlignment: [TextInput.AlignLeft, TextInput.AlignRight, TextInput.AlignHCenter][control.alignment]
            verticalAlignment: TextInput.AlignVCenter
            onTextEdited: control.edited(text)
            Keys.onPressed: (event) => control.handleKey(event)
        }
        TextEdit {
            id: edit
            anchors.fill: parent
            anchors.margins: 2
            visible: control.multiLine
            focus: control.multiLine
            text: control.text
            font: control.font
            color: control.foreColor
            readOnly: control.readOnly
            wrapMode: TextEdit.Wrap
            onTextChanged: if (visible) control.edited(text)
            Keys.onPressed: (event) => control.handleKey(event)
        }
    }
}
