add_library(staglogic STATIC
    classestato.cpp classestato.h
    stabarmodel.cpp stabarmodel.h
    stagapp.cpp stagapp.h
    collezioneordini.cpp collezioneordini.h
    gesord.cpp gesord.h
    stagglobals.h
)
target_include_directories(staglogic PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
target_link_libraries(staglogic PUBLIC stagcore stagnet)
target_compile_options(staglogic PRIVATE -Wall -Wextra -Wpedantic)
