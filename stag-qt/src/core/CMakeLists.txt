add_library(stagcore STATIC
    vbcompat.cpp vbcompat.h
    fixedrecord.cpp fixedrecord.h
    stagrecords.cpp stagrecords.h
    recordfile.cpp recordfile.h
    inifile.cpp inifile.h
    dbf.cpp dbf.h
    sqlitemigration.cpp sqlitemigration.h
    staglog.cpp staglog.h
    messages.cpp messages.h
    vbtypes.cpp vbtypes.h
    parmac.h
    inibas.cpp inibas.h
    parcfgschtag.cpp parcfgschtag.h
    inistore.cpp inistore.h
    parcurtag.cpp parcurtag.h
    uihooks.h
)
target_include_directories(stagcore PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
target_link_libraries(stagcore PUBLIC Qt6::Core Qt6::Sql)
target_compile_options(stagcore PRIVATE -Wall -Wextra -Wpedantic)
