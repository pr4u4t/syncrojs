add_library(staglogic STATIC
    classestato.cpp classestato.h
    stabarmodel.cpp stabarmodel.h
    stagapi.cpp stagapi.h
    stabarform.cpp stabarform.h
    stagapp.cpp stagapp.h
    collezioneordini.cpp collezioneordini.h
    gesord.cpp gesord.h
    formmodel.h
    plcframes.cpp plcframes.h
    plclink.cpp plclink.h
    schtagframes.cpp schtagframes.h
    schtaglink.cpp schtaglink.h
    schtagpages.cpp schtagpages.h schtagtables.inc
    setparmodels.cpp setparmodels.h
    miscpages.cpp miscpages.h debugdes.inc
    spdelink.cpp spdelink.h
    pagpromodel.cpp pagpromodel.h
    orderlistmodel.cpp orderlistmodel.h
    paglisordmodel.cpp paglisordmodel.h
    paglisesemodel.cpp paglisesemodel.h
    alarmstore.cpp alarmstore.h
    recordeditmodel.cpp recordeditmodel.h
    ondemodel.cpp ondemodel.h
    patternmodel.cpp patternmodel.h
    orderdialogmodel.cpp orderdialogmodel.h
    validazioneordini.cpp validazioneordini.h
    insordmodel.cpp insordmodel.h
    pwdmodel.cpp pwdmodel.h
    abilitazionimodel.cpp abilitazionimodel.h
    alarmgridmodel.cpp alarmgridmodel.h
    stagglobals.h
)
target_include_directories(staglogic PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
target_link_libraries(staglogic PUBLIC stagcore stagnet)
target_compile_options(staglogic PRIVATE -Wall -Wextra -Wpedantic)
