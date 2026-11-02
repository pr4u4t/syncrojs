include(forms.cmake)

set(STAG_VB_QML
    qml/vb/VbForm.qml qml/vb/VbLabel.qml qml/vb/VbTextBox.qml qml/vb/VbButton.qml qml/vb/VbFrame.qml
    qml/vb/VbPicture.qml qml/vb/VbImage.qml qml/vb/VbShape.qml qml/vb/VbLine.qml qml/vb/VbGrid.qml
    qml/vb/VbCombo.qml qml/vb/VbCheck.qml qml/vb/VbProgress.qml qml/vb/VbTimer.qml qml/vb/VbData.qml
    qml/vb/VbInert.qml qml/vb/Theme.qml
)
set_source_files_properties(qml/vb/Theme.qml PROPERTIES QT_QML_SINGLETON_TYPE TRUE)

qt_add_executable(stag main.cpp formui.h)
qt_add_qml_module(stag
    URI Stag
    VERSION 1.0
    QML_FILES qml/Main.qml ${STAG_VB_QML} ${STAG_FORM_QML}
    RESOURCES ${STAG_FORM_ASSETS}
)
target_link_libraries(stag PRIVATE staglogic Qt6::Quick Qt6::Test)
# The QML module builds a "Stag" directory next to the target; on the case-insensitive disks of macOS and
# Windows an executable called "stag" in the same place collides with it.
set_target_properties(stag PROPERTIES RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin")

# bin/ is ready to run: stag starts there with no arguments.
add_custom_command(TARGET stag POST_BUILD
    COMMAND ${CMAKE_COMMAND}
        -DSOURCE_DB=${CMAKE_SOURCE_DIR}/stag.sqlite
        -DSOURCE_DATA=${STAG_REFERENCE_DATA}
        -DTARGET_DIR=$<TARGET_FILE_DIR:stag>
        -P ${CMAKE_SOURCE_DIR}/cmake/CopyRuntimeData.cmake
    VERBATIM
)
