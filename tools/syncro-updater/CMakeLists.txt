cmake_minimum_required(VERSION 3.19)
project(syncro-updater VERSION 1.0.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 6.2 REQUIRED COMPONENTS Widgets Network)
qt_standard_project_setup()

set(SYNCRO_DEFAULT_REPO "" CACHE PATH "Syncro repository checkout used when --repo is not given")

qt_add_executable(syncro-updater
    src/main.cpp
    src/CommandRunner.cpp
    src/CommandRunner.h
    src/MainWindow.cpp
    src/MainWindow.h
)

configure_file(../syncro-update.svg ${CMAKE_CURRENT_BINARY_DIR}/syncro-update.svg COPYONLY)
qt_add_resources(syncro-updater icons
    PREFIX /
    BASE ${CMAKE_CURRENT_BINARY_DIR}
    FILES ${CMAKE_CURRENT_BINARY_DIR}/syncro-update.svg
)

target_compile_definitions(syncro-updater PRIVATE
    SYNCRO_DEFAULT_REPO="${SYNCRO_DEFAULT_REPO}"
    SYNCRO_UPDATER_VERSION="${PROJECT_VERSION}"
)
target_compile_options(syncro-updater PRIVATE -Wall -Wextra -Wpedantic)
target_link_libraries(syncro-updater PRIVATE Qt6::Widgets Qt6::Network)

install(TARGETS syncro-updater RUNTIME DESTINATION bin)
