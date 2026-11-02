import QtQuick
import Stag

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
            stagUi.activate(model, stagUi.previousForm())
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

    color: Theme.back(backColor)

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

    // The caption the .frm gives each control, in Italian: what the translation of a caption starts from.
    property var captions: ({})
    function localize(key, control) {
        if (captions[key] === undefined)
            captions[key] = control.text
        const match = key.match(/^(.*)\((\d+)\)$/)
        const translated = stagUi.controlText(form.formName, match ? match[1] : key, match ? Number(match[2]) : 0)
        // Only a real caption: the blank ones, like the function key texts, belong to whoever fills them.
        const caption = translated !== "" ? translated : stagUi.captionText(captions[key])
        if (caption.trim() !== "")
            control.text = caption
    }
    function translate() {
        if (typeof stagUi === "undefined")
            return
        for (const key of Object.keys(controls)) {
            const control = controls[key]
            if (control.vbEvent !== undefined && control.text !== undefined)
                localize(key, control)
        }
        if (model)
            applyState()
    }
    Connections {
        target: typeof stagUi !== "undefined" ? stagUi : null
        function onLanguageChanged() { form.translate() }
    }

    // The new look of what repeats on the pages: the title inside its bar, and the section captions
    // with the colour of the frame, which is a state, left as a stripe beside them.
    function restyle() {
        const title = ctl("pag_title")
        if (title) {
            title.x = 10
            title.y = 1
            title.height = 20
            title.font.pixelSize = 14
            title.centered = true
        }
        for (const index of [1, 2]) {
            const section = ctl("lab_ord", index)
            if (section) {
                section.x = 6
                section.y = 0
                section.height = 28
                section.centered = true
            }
        }
    }

    Component.onCompleted: {
        if (Theme.modern)
            restyle()
        for (const key of Object.keys(controls)) {
            const control = controls[key]
            if (control.vbEvent === undefined)
                continue
            const match = key.match(/^(.*)\((\d+)\)$/)
            const name = match ? match[1] : key
            const index = match ? Number(match[2]) : -1
            if (typeof stagUi !== "undefined" && control.text !== undefined)
                localize(key, control)
            control.vbEvent.connect((event, args) => {
                form.vbEvent(name, index, event, args)
                if (form.model)
                    stagUi.controlEvent(form.model, name, index, event, args)
            })
        }
        if (model) {
            applyState()
            activate()
        }
    }
}
