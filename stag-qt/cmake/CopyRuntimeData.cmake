# Puts next to the program what it needs to start: the database and a copy of the machine's data.
# Only what is missing: the program writes to these files, and a rebuild must not undo that.
# Arguments: SOURCE_DB, SOURCE_DATA, TARGET_DIR.
# An empty file counts as missing: SQLite leaves one behind when a program opens a database that is not there.
set(database_size 0)
if(EXISTS "${TARGET_DIR}/stag.sqlite")
    file(SIZE "${TARGET_DIR}/stag.sqlite" database_size)
endif()
if(database_size EQUAL 0 AND EXISTS "${SOURCE_DB}")
    file(COPY "${SOURCE_DB}" DESTINATION "${TARGET_DIR}")
endif()

if(EXISTS "${SOURCE_DATA}")
    file(GLOB data_files "${SOURCE_DATA}/*.ini" "${SOURCE_DATA}/*.dat" "${SOURCE_DATA}/*.txt")
    file(MAKE_DIRECTORY "${TARGET_DIR}/dati")
    foreach(data_file IN LISTS data_files)
        get_filename_component(name "${data_file}" NAME)
        if(NOT EXISTS "${TARGET_DIR}/dati/${name}")
            file(COPY "${data_file}" DESTINATION "${TARGET_DIR}/dati")
        endif()
    endforeach()
else()
    message(WARNING "No machine data in ${SOURCE_DATA}: start stag with --data")
endif()
