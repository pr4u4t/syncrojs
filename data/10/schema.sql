--
-- PostgreSQL database dump
--

-- Dumped from database version 10.23
-- Dumped by pg_dump version 10.23

-- Started on 2025-02-14 10:47:14

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3105 (class 0 OID 0)
-- Dependencies: 3104
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- TOC entry 6 (class 2615 OID 16393)
-- Name: syncro_db_3892; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA syncro_db_3892;


ALTER SCHEMA syncro_db_3892 OWNER TO postgres;

--
-- TOC entry 3107 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA syncro_db_3892; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA syncro_db_3892 IS 'Pacific Standard Time;-480;(UTC-08:00) Pacific Time (US & Canada);Pacific Standard Time;Pacific Daylight Time;[01:01:0001;12:31:2006;60;[0;02:00:00;4;1;0;];[0;02:00:00;10;5;0;];][01:01:2007;12:31:9999;60;[0;02:00:00;3;2;0;];[0;02:00:00;11;1;0;];];';


--
-- TOC entry 2 (class 3079 OID 12924)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 3108 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- TOC entry 1 (class 3079 OID 16384)
-- Name: adminpack; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;


--
-- TOC entry 3109 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION adminpack; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 198 (class 1259 OID 16394)
-- Name: cuttopattern; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.cuttopattern (
    patternid character varying(20) DEFAULT ' '::character varying NOT NULL,
    patterntext character varying(120) DEFAULT ' '::character varying NOT NULL,
    markdistance_uddmm1 integer NOT NULL,
    markdistance_uddmm2 integer NOT NULL,
    markdistance_uddmm3 integer NOT NULL,
    markdistance_uddmm4 integer NOT NULL,
    markdistance_uddmm5 integer NOT NULL,
    markdistance_uddmm6 integer NOT NULL,
    markdistance_uddmm7 integer NOT NULL,
    markdistance_uddmm8 integer NOT NULL,
    markdistance_uddmm9 integer NOT NULL,
    markdistance_uddmm10 integer NOT NULL,
    eyecontrast integer NOT NULL,
    firstmarkid integer NOT NULL,
    cutoffset_uddmm integer NOT NULL,
    windowbeforepattern_uddmm integer NOT NULL,
    windowafterpattern_uddmm integer NOT NULL,
    addedbydownload boolean NOT NULL,
    lateralpositionpattern_uddmm integer NOT NULL,
    lastused timestamp without time zone NOT NULL
);


ALTER TABLE syncro_db_3892.cuttopattern OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 16399)
-- Name: downtimes; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.downtimes (
    starttime timestamp without time zone NOT NULL,
    finishtime timestamp without time zone NOT NULL,
    reasonkey character varying(20) DEFAULT ' '::character varying NOT NULL,
    reasondescription character varying(200) DEFAULT ' '::character varying NOT NULL,
    zonekey character varying(20) DEFAULT ' '::character varying NOT NULL,
    zonedescription character varying(200) DEFAULT ' '::character varying NOT NULL,
    text character varying(200) DEFAULT ' '::character varying NOT NULL,
    setupid integer NOT NULL,
    setupmodid integer NOT NULL,
    shiftstarttime timestamp without time zone NOT NULL,
    shiftid character varying(1) DEFAULT ' '::character varying NOT NULL,
    job character varying(10) DEFAULT ' '::character varying NOT NULL,
    comm character varying(10) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.downtimes OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16413)
-- Name: flute; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.flute (
    flute character varying(30) DEFAULT ' '::character varying NOT NULL,
    sfsposition character varying(3) DEFAULT ' '::character varying NOT NULL,
    flutetext character varying(100) DEFAULT ' '::character varying NOT NULL,
    scoregap_uddmm integer NOT NULL,
    grindingparameter_udm integer NOT NULL,
    lubricationparameter_udm integer NOT NULL,
    trimgrindingsmultiplier integer NOT NULL,
    grindingdistance_udm integer NOT NULL,
    lubricationtime_utds integer NOT NULL,
    productionspeed_uvm_m integer NOT NULL,
    aocspeed_uvm_m integer NOT NULL,
    cruisingspeed_uvm_m integer NOT NULL,
    thickness_uddmm integer NOT NULL,
    terminalflutecode integer NOT NULL,
    twin400internalflapdisabled integer NOT NULL
);


ALTER TABLE syncro_db_3892.flute OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 16419)
-- Name: papers; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.papers (
    paperid character varying(15) DEFAULT ' '::character varying NOT NULL,
    addedbydownload boolean NOT NULL,
    maxsplicedbspeed_uvm_m integer NOT NULL,
    maxsplicesf1linerspeed_uvm_m integer NOT NULL,
    maxsplicesf1flutingspeed_uvm_m integer NOT NULL,
    maxsplicesf2linerspeed_uvm_m integer NOT NULL,
    maxsplicesf2flutingspeed_uvm_m integer NOT NULL,
    maxsplicesf3linerspeed_uvm_m integer NOT NULL,
    maxsplicesf3flutingspeed_uvm_m integer NOT NULL,
    corediameter_uddmm integer NOT NULL,
    weightfactor_uwg_m integer NOT NULL,
    defaultthickness_uddmmm integer NOT NULL,
    text character varying(30) DEFAULT ' '::character varying NOT NULL,
    lastused timestamp without time zone NOT NULL,
    paperclassification character varying(36) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.papers OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16425)
-- Name: pastorders; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.pastorders (
    begintime timestamp without time zone NOT NULL,
    finishtime timestamp without time zone NOT NULL,
    runtime_uts integer NOT NULL,
    numofstop integer NOT NULL,
    metersproduceddb_udm integer NOT NULL,
    metersproducedss_udm integer NOT NULL,
    stoptime integer NOT NULL,
    averagespeed integer NOT NULL,
    scraprsh_udm integer NOT NULL,
    cutsproduced0 integer NOT NULL,
    producedbad0 integer NOT NULL,
    produceddefault0 integer NOT NULL,
    cutlenghtdefault0_udmm integer NOT NULL,
    cutsproduced1 integer NOT NULL,
    producedbad1 integer NOT NULL,
    produceddefault1 integer NOT NULL,
    cutlenghtdefault1_udmm integer NOT NULL,
    cutsproduced2 integer NOT NULL,
    producedbad2 integer NOT NULL,
    produceddefault2 integer NOT NULL,
    cutlenghtdefault2_udmm integer NOT NULL,
    typeofrecord integer NOT NULL,
    shiftstarttime timestamp without time zone NOT NULL,
    shiftid character varying(1) DEFAULT ' '::character varying NOT NULL,
    job character varying(10) DEFAULT ' '::character varying NOT NULL,
    comm character varying(10) DEFAULT ' '::character varying NOT NULL,
    scrapmadedischargegroup1_udmm integer NOT NULL,
    scrapmadedischargegroup2_udmm integer NOT NULL,
    scrapmadedischargegroup3_udmm integer NOT NULL,
    setupid integer NOT NULL,
    modid integer NOT NULL,
    officeid integer NOT NULL,
    setupcode character varying(20) DEFAULT ' '::character varying NOT NULL,
    setuplength_uddmm integer NOT NULL,
    webwidth integer NOT NULL,
    "trim" integer NOT NULL,
    trimtype character varying(20) DEFAULT ' '::character varying NOT NULL,
    valid boolean NOT NULL,
    scoregap integer NOT NULL,
    slitstandem integer NOT NULL,
    scorestandem integer NOT NULL,
    shaftmask integer NOT NULL,
    constantsystem boolean NOT NULL,
    tandem boolean NOT NULL,
    knifeonly boolean NOT NULL,
    nochange_stk_0 boolean NOT NULL,
    nochange_stk_1 boolean NOT NULL,
    nochange_stk_2 boolean NOT NULL,
    ordernumber0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    delivery0 timestamp without time zone,
    customername0 character varying(30) DEFAULT ' '::character varying NOT NULL,
    customerorderid0 character varying(30) DEFAULT ' '::character varying NOT NULL,
    custaddress0 character varying(30) DEFAULT ' '::character varying NOT NULL,
    knifelevel0 integer NOT NULL,
    segmentsheetcount0 integer NOT NULL,
    sheetlength0 integer NOT NULL,
    sheetwidth0 integer NOT NULL,
    outs0 integer NOT NULL,
    scoremeasures0 character varying(650) DEFAULT ' '::character varying NOT NULL,
    scoretype0 character varying(2) DEFAULT ' '::character varying NOT NULL,
    posittype0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    teletwinoffset0 integer NOT NULL,
    scoregap0 integer NOT NULL,
    levelname0 character varying(10) DEFAULT ' '::character varying NOT NULL,
    sheetsperstack0 integer NOT NULL,
    stacksperpallet0 integer NOT NULL,
    sending0 character varying(10) DEFAULT ' '::character varying NOT NULL,
    cuttomark0 boolean NOT NULL,
    cuttopattern0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    datatoprint0_1 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint0_2 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint0_3 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint0_4 character varying(60) DEFAULT ' '::character varying NOT NULL,
    stackercontstack0 boolean NOT NULL,
    stackerpallet0 boolean NOT NULL,
    stackernote0 character varying(60) DEFAULT ' '::character varying NOT NULL,
    mthcode0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    mthnextmachine0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    numberofpalletlayout0 integer NOT NULL,
    amountofpalletcopy0 integer NOT NULL,
    numberofpastorderlayout0 integer NOT NULL,
    amountofpastordercopy0 integer NOT NULL,
    preprint0 boolean NOT NULL,
    dischargeside0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    balance0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    ordernumber1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    delivery1 timestamp without time zone,
    customername1 character varying(30) DEFAULT ' '::character varying NOT NULL,
    customerorderid1 character varying(30) DEFAULT ' '::character varying NOT NULL,
    custaddress1 character varying(30) DEFAULT ' '::character varying NOT NULL,
    knifelevel1 integer NOT NULL,
    segmentsheetcount1 integer NOT NULL,
    sheetlength1 integer NOT NULL,
    sheetwidth1 integer NOT NULL,
    outs1 integer NOT NULL,
    scoremeasures1 character varying(650) DEFAULT ' '::character varying NOT NULL,
    scoretype1 character varying(2) DEFAULT ' '::character varying NOT NULL,
    posittype1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    teletwinoffset1 integer NOT NULL,
    scoregap1 integer NOT NULL,
    levelname1 character varying(10) DEFAULT ' '::character varying NOT NULL,
    sheetsperstack1 integer NOT NULL,
    stacksperpallet1 integer NOT NULL,
    sending1 character varying(10) DEFAULT ' '::character varying NOT NULL,
    cuttomark1 boolean NOT NULL,
    cuttopattern1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    datatoprint1_1 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint1_2 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint1_3 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint1_4 character varying(60) DEFAULT ' '::character varying NOT NULL,
    stackercontstack1 boolean NOT NULL,
    stackerpallet1 boolean NOT NULL,
    stackernote1 character varying(60) DEFAULT ' '::character varying NOT NULL,
    mthcode1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    mthnextmachine1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    numberofpalletlayout1 integer NOT NULL,
    amountofpalletcopy1 integer NOT NULL,
    numberofpastorderlayout1 integer NOT NULL,
    amountofpastordercopy1 integer NOT NULL,
    preprint1 boolean NOT NULL,
    dischargeside1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    balance1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    ordernumber2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    delivery2 timestamp without time zone,
    customername2 character varying(30) DEFAULT ' '::character varying NOT NULL,
    customerorderid2 character varying(30) DEFAULT ' '::character varying NOT NULL,
    custaddress2 character varying(30) DEFAULT ' '::character varying NOT NULL,
    knifelevel2 integer NOT NULL,
    segmentsheetcount2 integer NOT NULL,
    sheetlength2 integer NOT NULL,
    sheetwidth2 integer NOT NULL,
    outs2 integer NOT NULL,
    scoremeasures2 character varying(650) DEFAULT ' '::character varying NOT NULL,
    scoretype2 character varying(2) DEFAULT ' '::character varying NOT NULL,
    posittype2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    teletwinoffset2 integer NOT NULL,
    scoregap2 integer NOT NULL,
    levelname2 character varying(10) DEFAULT ' '::character varying NOT NULL,
    sheetsperstack2 integer NOT NULL,
    stacksperpallet2 integer NOT NULL,
    sending2 character varying(10) DEFAULT ' '::character varying NOT NULL,
    cuttomark2 boolean NOT NULL,
    cuttopattern2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    datatoprint2_1 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint2_2 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint2_3 character varying(60) DEFAULT ' '::character varying NOT NULL,
    datatoprint2_4 character varying(60) DEFAULT ' '::character varying NOT NULL,
    stackercontstack2 boolean NOT NULL,
    stackerpallet2 boolean NOT NULL,
    stackernote2 character varying(60) DEFAULT ' '::character varying NOT NULL,
    mthcode2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    mthnextmachine2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    numberofpalletlayout2 integer NOT NULL,
    amountofpalletcopy2 integer NOT NULL,
    numberofpastorderlayout2 integer NOT NULL,
    amountofpastordercopy2 integer NOT NULL,
    preprint2 boolean NOT NULL,
    dischargeside2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    balance2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    qualityid character varying(36) DEFAULT ' '::character varying NOT NULL,
    qualitytext character varying(100) DEFAULT ' '::character varying NOT NULL,
    bundlelayersperstack integer NOT NULL,
    weightfactoruwg_m integer NOT NULL,
    bundlereversemode boolean NOT NULL,
    addedbydownload boolean NOT NULL,
    preprint boolean NOT NULL,
    inclinatelastbelt boolean NOT NULL,
    lastused timestamp without time zone NOT NULL,
    paper1id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper1weightfactor_uwg_m integer NOT NULL,
    paper2id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper2weightfactor_uwg_m integer NOT NULL,
    paper3id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper3weightfactor_uwg_m integer NOT NULL,
    paper4id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper4weightfactor_uwg_m integer NOT NULL,
    paper5id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper5weightfactor_uwg_m integer NOT NULL,
    paper6id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper6weightfactor_uwg_m integer NOT NULL,
    paper7id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper7weightfactor_uwg_m integer NOT NULL,
    flute character varying(30) DEFAULT ' '::character varying NOT NULL,
    sfsposition character varying(3) DEFAULT ' '::character varying NOT NULL,
    flutetext character varying(100) DEFAULT ' '::character varying NOT NULL,
    scoregap_uddmm integer NOT NULL,
    grindingparameter_udm integer NOT NULL,
    lubricationparameter_udm integer NOT NULL,
    trimgrindingsmultiplier integer NOT NULL,
    grindingdistance_udm integer NOT NULL,
    lubricationtime_utds integer NOT NULL,
    productionspeed_uvm_m integer NOT NULL,
    aocspeed_uvm_m integer NOT NULL,
    cruisingspeed_uvm_m integer NOT NULL,
    thickness_uddmm integer NOT NULL,
    terminalflutecode integer NOT NULL,
    twin400internalflapdisabled integer NOT NULL
);


ALTER TABLE syncro_db_3892.pastorders OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16505)
-- Name: pastrolls; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.pastrolls (
    timeremoved timestamp without time zone NOT NULL,
    timeentered timestamp without time zone NOT NULL,
    syncrosplice boolean NOT NULL,
    remainingdiameter_uddmm integer NOT NULL,
    remaining_udmm integer NOT NULL,
    thickness_uddmmm integer NOT NULL,
    splicernumber integer NOT NULL,
    papertype character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor_uwg_m integer NOT NULL,
    rollid character varying(30) DEFAULT ' '::character varying NOT NULL,
    width_uddmm integer NOT NULL,
    description character varying(50) DEFAULT ' '::character varying NOT NULL,
    datatoprint character varying(100) DEFAULT ' '::character varying NOT NULL,
    corediameter_uddmm integer NOT NULL,
    splicespeed_udmm_min integer NOT NULL,
    splicedone boolean NOT NULL,
    shiftstarttime timestamp without time zone NOT NULL,
    shiftshiftid character varying(1) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.pastrolls OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16513)
-- Name: quality; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.quality (
    qualityid character varying(36) DEFAULT ' '::character varying NOT NULL,
    qualitytext character varying(100) DEFAULT ' '::character varying NOT NULL,
    bundlelayersperstack integer NOT NULL,
    weightfactoruwg_m integer NOT NULL,
    bundlereversemode boolean NOT NULL,
    addedbydownload boolean NOT NULL,
    preprint boolean NOT NULL,
    inclinatelastbelt boolean NOT NULL,
    lastused timestamp without time zone NOT NULL,
    paper1id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper1weightfactor_uwg_m integer NOT NULL,
    paper2id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper2weightfactor_uwg_m integer NOT NULL,
    paper3id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper3weightfactor_uwg_m integer NOT NULL,
    paper4id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper4weightfactor_uwg_m integer NOT NULL,
    paper5id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper5weightfactor_uwg_m integer NOT NULL,
    paper6id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper6weightfactor_uwg_m integer NOT NULL,
    paper7id character varying(12) DEFAULT ' '::character varying NOT NULL,
    paper7weightfactor_uwg_m integer NOT NULL,
    flute character varying(30) DEFAULT ' '::character varying NOT NULL,
    sfsposition character varying(3) DEFAULT ' '::character varying NOT NULL,
    flutetext character varying(100) DEFAULT ' '::character varying NOT NULL,
    scoregap_uddmm integer NOT NULL,
    grindingparameter_udm integer NOT NULL,
    lubricationparameter_udm integer NOT NULL,
    trimgrindingsmultiplier integer NOT NULL,
    grindingdistance_udm integer NOT NULL,
    lubricationtime_utds integer NOT NULL,
    productionspeed_uvm_m integer NOT NULL,
    aocspeed_uvm_m integer NOT NULL,
    cruisingspeed_uvm_m integer NOT NULL,
    thickness_uddmm integer NOT NULL,
    terminalflutecode integer NOT NULL,
    twin400internalflapdisabled integer NOT NULL
);


ALTER TABLE syncro_db_3892.quality OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 16528)
-- Name: shiftflutelist; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.shiftflutelist (
    idshift character varying(1) DEFAULT ' '::character varying NOT NULL,
    starttime timestamp without time zone NOT NULL,
    flute character varying(30) DEFAULT ' '::character varying NOT NULL,
    dryend integer NOT NULL,
    shearscrap integer NOT NULL,
    dryendtotal integer NOT NULL,
    dryendm2 integer NOT NULL,
    scrapm2 integer NOT NULL,
    dryendtotalm2 integer NOT NULL,
    job character varying(10) DEFAULT ' '::character varying NOT NULL,
    comm character varying(10) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.shiftflutelist OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16535)
-- Name: shiftlist; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.shiftlist (
    id character varying(1) DEFAULT ' '::character varying NOT NULL,
    starttime timestamp without time zone NOT NULL,
    finishtime timestamp without time zone NOT NULL,
    runtime integer NOT NULL,
    stoptime integer NOT NULL,
    numberofstop integer NOT NULL,
    setupchanges integer NOT NULL,
    qualitychanges integer NOT NULL,
    shiftmanager character varying(25) DEFAULT ' '::character varying NOT NULL,
    people integer NOT NULL,
    avgspeed integer NOT NULL,
    job character varying(10) DEFAULT ' '::character varying NOT NULL,
    comm character varying(10) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.shiftlist OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16542)
-- Name: trace_auto_man_status; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_auto_man_status (
    "time" timestamp without time zone NOT NULL,
    dryend integer DEFAULT 0 NOT NULL,
    zone0 integer DEFAULT 0 NOT NULL,
    zone1 integer DEFAULT 0 NOT NULL,
    zone2 integer DEFAULT 0 NOT NULL,
    zone3 integer DEFAULT 0 NOT NULL,
    splicer1 integer DEFAULT 0 NOT NULL,
    splicer2 integer DEFAULT 0 NOT NULL,
    splicer3 integer DEFAULT 0 NOT NULL,
    splicer4 integer DEFAULT 0 NOT NULL,
    splicer5 integer DEFAULT 0 NOT NULL,
    splicer6 integer DEFAULT 0 NOT NULL,
    splicer7 integer DEFAULT 0 NOT NULL
);


ALTER TABLE syncro_db_3892.trace_auto_man_status OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 16557)
-- Name: trace_speed_uvmm_m; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_speed_uvmm_m (
    data timestamp without time zone NOT NULL,
    zone0 integer DEFAULT 0 NOT NULL,
    zone1 integer DEFAULT 0 NOT NULL,
    zone2 integer DEFAULT 0 NOT NULL,
    zone3 integer DEFAULT 0 NOT NULL
);


ALTER TABLE syncro_db_3892.trace_speed_uvmm_m OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16564)
-- Name: trace_syc_gluegap_1; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_gluegap_1 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_gluegap_1 OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 16568)
-- Name: trace_syc_gluegap_11; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_gluegap_11 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_gluegap_11 OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16572)
-- Name: trace_syc_gluegap_2; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_gluegap_2 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_gluegap_2 OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 16576)
-- Name: trace_syc_gluegap_21; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_gluegap_21 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_gluegap_21 OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16580)
-- Name: trace_syc_rolls; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_rolls (
    "time" timestamp without time zone NOT NULL,
    type character varying(50) DEFAULT ' '::character varying NOT NULL,
    setupid integer NOT NULL,
    setupmodid integer NOT NULL,
    setupcode character varying(20) DEFAULT ' '::character varying NOT NULL,
    ordernumber0 character varying(20) DEFAULT ' '::character varying NOT NULL,
    ordernumber1 character varying(20) DEFAULT ' '::character varying NOT NULL,
    ordernumber2 character varying(20) DEFAULT ' '::character varying NOT NULL,
    metersproduceddb_udm integer NOT NULL,
    metersproducedss_udm integer NOT NULL,
    papertype1 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor1_uwg_m integer NOT NULL,
    rollid1 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype2 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor2_uwg_m integer NOT NULL,
    rollid2 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype3 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor3_uwg_m integer NOT NULL,
    rollid3 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype4 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor4_uwg_m integer NOT NULL,
    rollid4 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype5 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor5_uwg_m integer NOT NULL,
    rollid5 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype6 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor6_uwg_m integer NOT NULL,
    rollid6 character varying(30) DEFAULT ' '::character varying NOT NULL,
    papertype7 character varying(12) DEFAULT ' '::character varying NOT NULL,
    weightfactor7_uwg_m integer NOT NULL,
    rollid7 character varying(30) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_rolls OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16602)
-- Name: trace_syc_steamshower_1; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_steamshower_1 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_steamshower_1 OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16606)
-- Name: trace_syc_steamshower_2; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_steamshower_2 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_steamshower_2 OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16610)
-- Name: trace_syc_steamshower_3; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_steamshower_3 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_steamshower_3 OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16614)
-- Name: trace_syc_steamshower_4; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_steamshower_4 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_steamshower_4 OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16618)
-- Name: trace_syc_wraparm_1; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_1 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_1 OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16622)
-- Name: trace_syc_wraparm_11; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_11 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_11 OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16626)
-- Name: trace_syc_wraparm_13; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_13 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_13 OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16630)
-- Name: trace_syc_wraparm_15; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_15 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_15 OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16634)
-- Name: trace_syc_wraparm_2; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_2 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_2 OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16638)
-- Name: trace_syc_wraparm_22; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_22 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_22 OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 16642)
-- Name: trace_syc_wraparm_23; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_23 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_23 OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16646)
-- Name: trace_syc_wraparm_24; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_24 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_24 OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 16650)
-- Name: trace_syc_wraparm_25; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_25 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_25 OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 16654)
-- Name: trace_syc_wraparm_31; Type: TABLE; Schema: syncro_db_3892; Owner: postgres
--

CREATE TABLE syncro_db_3892.trace_syc_wraparm_31 (
    data timestamp without time zone NOT NULL,
    percentage integer DEFAULT 0 NOT NULL,
    auto boolean NOT NULL
);


ALTER TABLE syncro_db_3892.trace_syc_wraparm_31 OWNER TO postgres;

--
-- TOC entry 2957 (class 2606 OID 16664)
-- Name: cuttopattern cuttopattern_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.cuttopattern
    ADD CONSTRAINT cuttopattern_pkey PRIMARY KEY (patternid);


--
-- TOC entry 2959 (class 2606 OID 16666)
-- Name: downtimes downtimes_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.downtimes
    ADD CONSTRAINT downtimes_pkey PRIMARY KEY (shiftid, starttime);


--
-- TOC entry 2961 (class 2606 OID 16668)
-- Name: flute flute_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.flute
    ADD CONSTRAINT flute_pkey PRIMARY KEY (flute);


--
-- TOC entry 2963 (class 2606 OID 16670)
-- Name: papers papers_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.papers
    ADD CONSTRAINT papers_pkey PRIMARY KEY (paperid, weightfactor_uwg_m);


--
-- TOC entry 2965 (class 2606 OID 16672)
-- Name: pastorders pastorders_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.pastorders
    ADD CONSTRAINT pastorders_pkey PRIMARY KEY (setupid, begintime);


--
-- TOC entry 2967 (class 2606 OID 16674)
-- Name: pastrolls pastrolls_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.pastrolls
    ADD CONSTRAINT pastrolls_pkey PRIMARY KEY (splicernumber, timeremoved);


--
-- TOC entry 2969 (class 2606 OID 16676)
-- Name: quality quality_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.quality
    ADD CONSTRAINT quality_pkey PRIMARY KEY (qualityid);


--
-- TOC entry 2971 (class 2606 OID 16678)
-- Name: shiftflutelist shiftflutelist_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.shiftflutelist
    ADD CONSTRAINT shiftflutelist_pkey PRIMARY KEY (idshift, starttime, flute);


--
-- TOC entry 2973 (class 2606 OID 16680)
-- Name: shiftlist shiftlist_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.shiftlist
    ADD CONSTRAINT shiftlist_pkey PRIMARY KEY (id, starttime);


--
-- TOC entry 2975 (class 2606 OID 16682)
-- Name: trace_auto_man_status trace_auto_man_status_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.trace_auto_man_status
    ADD CONSTRAINT trace_auto_man_status_pkey PRIMARY KEY ("time");


--
-- TOC entry 2977 (class 2606 OID 16684)
-- Name: trace_syc_rolls trace_syc_rolls_pkey; Type: CONSTRAINT; Schema: syncro_db_3892; Owner: postgres
--

ALTER TABLE ONLY syncro_db_3892.trace_syc_rolls
    ADD CONSTRAINT trace_syc_rolls_pkey PRIMARY KEY (setupid, type, "time");


-- Completed on 2025-02-14 10:47:14

--
-- PostgreSQL database dump complete
--

