const severityPriority = {
    Warning: 1,
    Alarm: 2,
    Emergency: 3
};

const machine_alerts = (machines) => (Array.isArray(machines) ? machines : [])
    .map((machine) => {
        const severity = machine.inEmergency ? 'Emergency'
            : machine.alarmed ? 'Alarm'
                : machine.warned ? 'Warning'
                    : null;
        return severity ? { machine: machine.name || '', severity } : null;
    })
    .filter(Boolean)
    .sort((first, second) => severityPriority[second.severity] - severityPriority[first.severity]);

module.exports = { machine_alerts };
