cmake_minimum_required(VERSION 3.21)
project(stag-qt VERSION 0.1.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 6.5 REQUIRED COMPONENTS Core Sql Network)
find_package(Qt6 6.5 OPTIONAL_COMPONENTS Quick Test)
qt_standard_project_setup(REQUIRES 6.5)

# Directory with the original VB6 data files, used by tests.
set(STAG_REFERENCE_DATA "${CMAKE_CURRENT_SOURCE_DIR}/../stag/dati" CACHE PATH "Original STAG dati directory")

add_subdirectory(src/core)
add_subdirectory(src/net)
add_subdirectory(src/logic)
add_subdirectory(src/tools)
if(TARGET Qt6::Quick)
    add_subdirectory(src/app)
endif()
if(TARGET Qt6::Test)
    enable_testing()
    add_subdirectory(tests)
endif()
