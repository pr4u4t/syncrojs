add_library(stagnet STATIC
    fins.cpp fins.h
    finsclient.cpp finsclient.h
    finssimulator.cpp finssimulator.h
)
target_include_directories(stagnet PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
target_link_libraries(stagnet PUBLIC Qt6::Core Qt6::Network)
target_compile_options(stagnet PRIVATE -Wall -Wextra -Wpedantic)
