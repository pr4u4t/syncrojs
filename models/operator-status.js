'use strict';

const { XMLParser } = require('fast-xml-parser');
const logger = require('../utils/logger');
const { connect: syncroConnect } = require('../utils/syncro');

const parser = new XMLParser();
const asBoolean = (value) => String(value).toLowerCase() === 'true';

const operator_status = async function(){
    let socket;
    try{
        socket = await syncroConnect();
        const command = 'Operator.Status';
        const written = await socket.write(command);
        if(written !== Buffer.byteLength(command)){
            return null;
        }

        const response = await socket.readAll();
        const xml = response ? response.toString('utf8').trim() : '';
        if(!xml || xml === 'error'){
            return null;
        }

        const parsed = parser.parse(xml);
        const snapshot = parsed && parsed.OperatorStatus;
        if(!snapshot){
            return null;
        }

        const sourceMachines = snapshot.Machines && snapshot.Machines.Machine;
        const machines = sourceMachines ? (Array.isArray(sourceMachines) ? sourceMachines : [sourceMachines]) : [];
        const sourceAlerts = snapshot.ActiveAlerts && snapshot.ActiveAlerts.Alert;
        const activeAlerts = sourceAlerts ? (Array.isArray(sourceAlerts) ? sourceAlerts : [sourceAlerts]) : [];
        return {
            lineSpeed: snapshot.LineSpeed,
            dryMetersLeft: snapshot.DryMetersLeft,
            wetEndMetersLeft: snapshot.WetEndMetersLeft,
            drySetupCount: snapshot.DrySetupCount,
            dryProgressPercent: Number(snapshot.DryProgressPercent) || 0,
            aocTimeLeft: snapshot.AocTimeLeft,
            aocMode: snapshot.AocMode,
            aocType: snapshot.AocType,
            simulation: asBoolean(snapshot.Simulation),
            firstStopTime: snapshot.FirstStopTime,
            firstStopMachine: snapshot.FirstStopMachine,
            firstStopReason: snapshot.FirstStopReason,
            readyToRun: asBoolean(snapshot.ReadyToRun),
            readyToAoc: asBoolean(snapshot.ReadyToAOC),
            activeAlerts: activeAlerts.map((alert) => ({
                machine: alert.Machine,
                code: alert.Code,
                severity: alert.Severity,
                zone: alert.Zone,
                description: alert.Description,
                startedAt: alert.StartedAt
            })),
            machines: machines.map((machine) => ({
                name: machine.Name,
                configured: asBoolean(machine.Configured),
                readyToRun: asBoolean(machine.ReadyToRun),
                readyToAoc: asBoolean(machine.ReadyToAOC),
                alarmed: asBoolean(machine.Alarmed),
                warned: asBoolean(machine.Warned),
                inEmergency: asBoolean(machine.InEmergency)
            }))
        };
    }catch(error){
        logger.warn({error}, 'Failed to fetch Syncro operator status');
        return null;
    }finally{
        if(socket){
            socket.destroy();
        }
    }
};

module.exports = {
    operator_status
};
