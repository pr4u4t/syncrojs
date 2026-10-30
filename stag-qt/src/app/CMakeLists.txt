include(forms.cmake)

set(STAG_VB_QML
    qml/vb/VbForm.qml qml/vb/VbLabel.qml qml/vb/VbTextBox.qml qml/vb/VbButton.qml qml/vb/VbFrame.qml
    qml/vb/VbPicture.qml qml/vb/VbImage.qml qml/vb/VbShape.qml qml/vb/VbLine.qml qml/vb/VbGrid.qml
    qml/vb/VbCombo.qml qml/vb/VbCheck.qml qml/vb/VbProgress.qml qml/vb/VbTimer.qml qml/vb/VbData.qml
    qml/vb/VbInert.qml
)

qt_add_executable(stag main.cpp formui.h)
qt_add_qml_module(stag
    URI Stag
    VERSION 1.0
    QML_FILES qml/Main.qml qml/StatusBar.qml ${STAG_VB_QML} ${STAG_FORM_QML}
    RESOURCES ${STAG_FORM_ASSETS}
)
target_link_libraries(stag PRIVATE staglogic Qt6::Quick Qt6::Test)
