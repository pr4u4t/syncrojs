import QtQuick

// Root of a generated form. Handlers attach to vbEvent, the way VB wires Sub <control>_<event>.
Rectangle {
    id: form
    property string formName
    property string caption
    property bool dialog: false
    property color backColor: "#c0c0c0"
    property url picture
    property var controls: ({})
    property QtObject model: typeof stagUi !== "undefined" ? stagUi.formModel(formName) : null
    property bool applying: false
    property bool pendingState: false
    signal vbEvent(string control, int index, string event, var args)
    signal closeRequested()

    // Control by VB name, case-insensitive like VB; index for control arrays.
    function ctl(name, index) {
        const key = String(name).toLowerCase()
        return controls[index === undefined ? key : key + "(" + index + ")"]
    }

    function activate() {
        if (model)
            model.activate(typeof stagUi !== "undefined" ? stagUi.previousForm() : "")
    }

    // Copies what the C++ model assigned onto the controls.
    function applyState() {
        // A property set here can raise an event that changes the state again (disabling a focused
        // field raises LostFocus); that newer state is applied after this pass, not overwritten by it.
        if (applying) {
            pendingState = true
            return
        }
        applying = true
        do {
            pendingState = false
            const state = model.state
            for (const key of Object.keys(state)) {
                const control = controls[key]
                if (!control)
                    continue
                const properties = state[key]
                for (const name of Object.keys(properties)) {
                    if (control[name] !== properties[name])
                        control[name] = properties[name]
                }
            }
        } while (pendingState)
        applying = false
    }

    color: backColor

    Connections {
        target: form.model
        function onStateChanged() { form.applyState() }
        function onCloseRequested() { form.closeRequested() }
    }

    Image {
        source: form.picture
        visible: source.toString() !== ""
        z: -1
    }

    Component.onCompleted: {
        for (const key of Object.keys(controls)) {
            const control = controls[key]
            if (control.vbEvent === undefined)
                continue
            const match = key.match(/^(.*)\((\d+)\)$/)
            const name = match ? match[1] : key
            const index = match ? Number(match[2]) : -1
            if (typeof stagUi !== "undefined" && control.text !== undefined) {
                const translated = stagUi.controlText(form.formName, name, Math.max(index, 0))
                if (translated !== "")
                    control.text = translated
            }
            control.vbEvent.connect((event, args) => {
                form.vbEvent(name, index, event, args)
                if (form.model)
                    form.model.controlEvent(name, index, event, args)
            })
        }
        if (model) {
            applyState()
            activate()
        }
    }
}
