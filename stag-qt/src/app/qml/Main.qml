import QtQuick
import QtQuick.Window
import Stag
import "nav.js" as Nav

// Shell of the original layout: Tasti on top, the page below, status bar at the bottom. Designed at
// 800x600 like the VB forms and scaled to the panel. Pages change like key.cls: eve_key.chg in nav.js.
Window {
    id: root
    width: 800
    height: 600
    visible: true
    title: "STAG"
    color: Theme.modern ? Theme.ink : "#c0c0c0"

    // The release of a modifier goes to whatever has the focus then, as with Cmd+Shift+4 on macOS.
    onActiveChanged: {
        if (!active && shiftMask !== 0) {
            shiftMask = 0
            updateLabels()
        }
    }
    minimumWidth: 640
    minimumHeight: 480
    property string currentForm: "Pag_pro"
    property string previousForm: "Pag_pro"
    property string overlayForm: ""
    property int shiftMask: 0

    function formUrl(name) {
        return name === "" ? "" : Qt.resolvedUrl("forms/" + name + ".qml")
    }
    function activeForm() {
        return overlayForm !== "" ? overlayForm : currentForm
    }
    function showForm(name) {
        // The form being replaced may hold the focus; without it the function keys would stop working.
        shell.forceActiveFocus()
        const closedOverlay = overlayForm
        overlayForm = ""
        if (name === currentForm && closedOverlay !== "") {
            // Coming back from a dialog is a Form_Activate of the page underneath.
            stagUi.setPreviousForm(closedOverlay)
            if (page.item)
                page.item.activate()
        }
        if (name !== currentForm) {
            previousForm = currentForm
            stagUi.setPreviousForm(currentForm)
            currentForm = name
        }
        updateLabels()
    }
    function updateLabels() {
        const labels = stagUi.functionLabels(activeForm(), shiftMask)
        const marks = stagUi.functionMarks(activeForm())
        const active = overlayForm !== "" ? overlay.item : page.item
        const hidden = active && active.model ? active.model.hiddenFunctionKeys() : []
        for (let key = 1; key <= 10; ++key) {
            const label = tasti.ctl("des_fun", key)
            if (label) {
                label.text = labels[key - 1]
                label.visible = !hidden.includes("F" + key)
            }
            const mark = tasti.ctl("txt_shift", key)
            if (mark)
                mark.text = marks[key - 1]
        }
    }
    function press(key, shift) {
        const form = activeForm()
        const active = overlayForm !== "" ? overlay.item : page.item
        if (active && active.model && stagUi.functionKey(active.model, key, shift))
            return
        const entry = Nav.forms[form] ? Nav.forms[form].keys[key] : undefined
        const targets = entry ? entry[String(shift)] : undefined
        // A key VB gives no action does nothing.
        if (!targets || targets.length === 0)
            return
        const chosen = active && active.model ? active.model.navigationTarget(key, shift) : ""
        const target = chosen !== "" ? { to: chosen, keep: false } : targets[0]
        if (target.to === "@back") {
            if (overlayForm !== "") {
                shell.forceActiveFocus()
                stagUi.setPreviousForm(overlayForm)
                overlayForm = ""
                if (page.item)
                    page.item.activate()
            } else {
                showForm(previousForm)
            }
            updateLabels()
        } else if (target.keep || (Nav.forms[target.to] && Nav.forms[target.to].dialog)) {
            overlayForm = target.to
            updateLabels()
        } else {
            showForm(target.to)
        }
    }

    Item {
        id: shell
        width: 800
        height: 600
        // Width and height apart, like PercX and PercY of VB: the layout fills the window at any proportion.
        transform: Scale { xScale: root.width / 800; yScale: root.height / 600 }
        focus: true

        Keys.onPressed: (event) => {
            const shift = (event.modifiers & Qt.ShiftModifier ? 1 : 0)
                        | (event.modifiers & Qt.ControlModifier ? 2 : 0)
                        | (event.modifiers & Qt.AltModifier ? 4 : 0)
            if (shift !== root.shiftMask) {
                root.shiftMask = shift
                root.updateLabels()
            }
            if (event.key >= Qt.Key_F1 && event.key <= Qt.Key_F12) {
                root.press("F" + (event.key - Qt.Key_F1 + 1), shift)
                event.accepted = true
            } else if (event.key === Qt.Key_Escape) {
                root.press("ESC", shift)
                event.accepted = true
            }
        }
        Keys.onReleased: (event) => {
            const active = root.overlayForm !== "" ? overlay.item : page.item
            if (active && active.model)
                stagUi.keyUp(active.model, event.key)
            const shift = (event.modifiers & Qt.ShiftModifier ? 1 : 0)
                        | (event.modifiers & Qt.ControlModifier ? 2 : 0)
                        | (event.modifiers & Qt.AltModifier ? 4 : 0)
            if (shift !== root.shiftMask) {
                root.shiftMask = shift
                root.updateLabels()
            }
        }

        Tasti {
            id: tasti
            x: 0
            y: 0
            width: 800
            onVbEvent: (control, index, event) => {
                if ((control === "des_fun" || (Theme.modern && control === "tas_fun")) && event === "Click")
                    root.press("F" + index, root.shiftMask)
            }
            Component.onCompleted: {
                // Not as in the .frm: key and text centered in the whole cell, and the purple
                // separators whole, without the white pieces the form puts over their top.
                for (let key = 1; key <= 10; ++key) {
                    const name = ctl("tas_fun", key)
                    const text = ctl("des_fun", key)
                    name.x = text.x
                    name.width = text.width
                    name.alignment = 2
                    // Below the purple bar, which ends at 12.
                    name.y = 13
                    name.font.pixelSize = 17
                    text.y = 33
                    text.height = 28
                    text.alignment = 2
                    // The number is centred now, so the S/C/A mark goes to the corner of the cell.
                    const mark = ctl("txt_shift", key)
                    if (mark) {
                        mark.x = text.x + text.width - mark.width - 2
                        mark.y = 13
                        mark.alignment = 1
                    }
                    const white = ctl("shp_bianca", key - 1)
                    if (white)
                        white.visible = false
                }
                // The new look: cells on a plain ground, without the purple bar and separators.
                if (Theme.modern) {
                    for (const key of Object.keys(controls)) {
                        if (key.startsWith("shp_"))
                            controls[key].visible = false
                    }
                    for (let key = 1; key <= 10; ++key) {
                        const name = ctl("tas_fun", key)
                        name.y = 6
                        name.font.pixelSize = 15
                        name.foreColor = Theme.accent
                        ctl("des_fun", key).y = 28
                    }
                }
                Qt.callLater(root.updateLabels)
            }
        }

        Loader {
            id: page
            x: 0
            y: tasti.height
            width: 800
            height: 600 - tasti.height
            clip: true
            source: root.formUrl(root.currentForm)
            onLoaded: Qt.callLater(root.updateLabels)
        }

        Rectangle {
            anchors.fill: parent
            visible: overlay.status === Loader.Ready
            color: "#60000000"
            z: 2
            MouseArea { anchors.fill: parent }
        }
        Loader {
            id: overlay
            anchors.centerIn: parent
            z: 3
            source: root.formUrl(root.overlayForm)
        }

        // The on-screen F11 of the dialogs.
        Connections {
            target: overlay.item
            function onVbEvent(control, index, event) {
                if (control === "tas_f1" && event === "Click")
                    root.press("F11", 0)
            }
            function onCloseRequested() { root.press("ESC", 0) }
        }

        Sta_bar {
            id: statusBar
            anchors { left: parent.left; bottom: parent.bottom }
            z: 1
            // txt_idle, which only holds the focus in VB, lies far above the form.
            clip: true
        }

        // messaggio with vis_for opens pag_msg over the page, and a new language renews the captions.
        Connections {
            target: stagUi
            function onOverlayRequested(form) {
                root.overlayForm = form
                root.updateLabels()
            }
            function onLanguageChanged() { root.updateLabels() }
        }
    }
}
