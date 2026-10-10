const { machine_alerts } = require('../../utils/operator-alerts');

test('returns active machine alerts in severity order, including unconfigured machines', () => {
    expect(machine_alerts([
        { name: 'Knife', configured: true, warned: true },
        { name: 'Stacker1', configured: false, alarmed: true, warned: true },
        { name: 'Shear', configured: true, inEmergency: true, alarmed: true },
        { name: 'DoubleBacker', configured: true, readyToRun: false }
    ])).toEqual([
        { machine: 'Shear', severity: 'Emergency' },
        { machine: 'Stacker1', severity: 'Alarm' },
        { machine: 'Knife', severity: 'Warning' }
    ]);
});

test('returns an empty list when there are no active alerts or no machine list', () => {
    expect(machine_alerts([{ name: 'Knife', alarmed: false, warned: false, inEmergency: false }])).toEqual([]);
    expect(machine_alerts(null)).toEqual([]);
});
