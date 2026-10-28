qt_add_executable(stag-migrate-dbf migrate_dbf.cpp)
target_link_libraries(stag-migrate-dbf PRIVATE stagcore)

qt_add_executable(stag-plc-sim plc_sim.cpp)
target_link_libraries(stag-plc-sim PRIVATE stagnet)
