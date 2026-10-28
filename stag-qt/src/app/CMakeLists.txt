qt_add_executable(stag main.cpp)
qt_add_qml_module(stag
    URI Stag
    VERSION 1.0
    QML_FILES qml/Main.qml qml/FunctionBar.qml qml/StatusBar.qml
)
target_link_libraries(stag PRIVATE staglogic Qt6::Quick)
