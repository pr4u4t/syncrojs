import QtQuick
import Stag

// MSFlexGrid, DBGrid and ListBox. Text lives in cells[row][col]; call setText or assign cells.
Rectangle {
    id: control
    property int rows: 2
    property int cols: 2
    property int fixedRows: 1
    property int fixedCols: 1
    property var cells: []
    property var colWidths: []
    property var colAlign: []
    property var colBold: []
    property var colMerge: []
    property var rowBack: []
    property var cellBack: []
    property var cellFore: []
    property var rowFore: []
    property int rowHeight: 18
    property int cellPixelSize: 0
    property bool fitCells: false
    property var items: []
    property int row: -1
    property string dataSource
    property font font
    property color backColor: "#ffffff"
    property color foreColor: "#000000"
    property string toolTip
    signal vbEvent(string event, var args)

    function textAt(rowIndex, colIndex) {
        const cells = control.cells[rowIndex]
        return cells && cells[colIndex] !== undefined ? String(cells[colIndex]) : ""
    }
    function setText(rowIndex, colIndex, value) {
        const next = control.cells.slice()
        next[rowIndex] = (next[rowIndex] || []).slice()
        next[rowIndex][colIndex] = value
        control.cells = next
        control.rows = Math.max(control.rows, rowIndex + 1)
        control.cols = Math.max(control.cols, colIndex + 1)
    }
    function colWidth(colIndex) {
        if (control.fitCells)
            return control.width / Math.max(1, control.cols) * 0.99
        return control.colWidths[colIndex] || Math.max(40, control.width / Math.max(1, control.cols))
    }

    onItemsChanged: if (items.length > 0) {
        control.cols = 1
        control.fixedRows = 0
        control.fixedCols = 0
        control.rows = items.length
        control.cells = items.map((item) => [item])
    }

    activeFocusOnTab: true
    color: Theme.back(backColor)
    border.width: 1
    border.color: Theme.border(Theme.hairline, "#404040")
    clip: true

    onRowChanged: if (row >= 0) rowList.positionViewAtIndex(row, ListView.Contain)

    ListView {
        id: rowList
        anchors.fill: parent
        anchors.margins: 1
        model: control.rows
        boundsBehavior: Flickable.StopAtBounds
        delegate: Row {
            id: rowItem
            required property int index
            height: control.fitCells ? (control.height - 2) / Math.max(1, control.rows) * 0.99 : control.rowHeight
            Repeater {
                model: control.cols
                Rectangle {
                    required property int index
                    readonly property bool fixed: rowItem.index < control.fixedRows || index < control.fixedCols
                    // MergeCol: an equal value below the row above shows as one cell.
                    readonly property bool merged: control.colMerge[index] === true && rowItem.index > 1
                        && control.textAt(rowItem.index, index) === control.textAt(rowItem.index - 1, index)
                    width: control.colWidth(index)
                    height: rowItem.height
                    color: control.cellBack[rowItem.index] !== undefined ? Theme.back(control.cellBack[rowItem.index][index])
                         : control.rowBack[rowItem.index] !== undefined ? Theme.back(control.rowBack[rowItem.index])
                         : rowItem.index === control.row ? Theme.border(Theme.accent, "#000080") : fixed ? Theme.back("#c0c0c0") : Theme.back(control.backColor)
                    border.width: Theme.modern ? 0 : 1
                    border.color: "#808080"
                    // Only the lines between the rows.
                    Rectangle {
                        visible: Theme.modern
                        anchors { left: parent.left; right: parent.right; bottom: parent.bottom }
                        height: 1
                        color: Theme.hairline
                    }
                    Text {
                        anchors.fill: parent
                        anchors.leftMargin: 2
                        verticalAlignment: Text.AlignVCenter
                        anchors.rightMargin: 2
                        text: parent.merged ? "" : control.textAt(rowItem.index, parent.index)
                        font.family: control.font.family
                        font.pixelSize: control.cellPixelSize > 0 ? control.cellPixelSize : control.font.pixelSize
                        font.bold: control.colBold[parent.index] !== undefined ? control.colBold[parent.index] || rowItem.index === 0 : control.font.bold
                        horizontalAlignment: [Text.AlignLeft, Text.AlignHCenter, Text.AlignRight][control.colAlign[parent.index] || 0]
                        color: control.cellFore[rowItem.index] !== undefined ? Theme.fore(control.cellFore[rowItem.index][parent.index])
                             : control.rowFore[rowItem.index] !== undefined ? Theme.fore(control.rowFore[rowItem.index])
                             : rowItem.index === control.row ? "#ffffff" : Theme.fore(control.foreColor)
                        elide: Text.ElideRight
                    }
                    MouseArea {
                        anchors.fill: parent
                        onClicked: {
                            control.row = rowItem.index
                            control.vbEvent("Click", { row: rowItem.index, col: parent.index })
                        }
                        onDoubleClicked: control.vbEvent("DblClick", { row: rowItem.index, col: parent.index })
                    }
                }
            }
        }
    }
}
