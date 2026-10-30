import React, { useEffect, useRef, useState } from 'react';
import { createRoot } from 'react-dom/client';
import { machine_alerts } from '../utils/operator-alerts';
import { classicLevels } from '../utils/classic-levels';
import { MAX_TOTAL_OUTS, totalOuts } from '../utils/order-constraints';

const numberOrDash = (value) => value === null || value === undefined || (typeof value === 'string' && value.trim() === '') ? '—' : value;
const localized = (en, pl) => document.documentElement.lang === 'pl' ? pl : en;
const createManualSetupCode = () => `M${Date.now().toString(36).toUpperCase()}${Math.random().toString(36).slice(2, 10).toUpperCase()}`;
const classicScoreFields = [[12, 13, 14, 15, 16], [27, 28, 29, 30, 31], [42, 43, 44, 45, 46]];
const classicOrderStarts = [8, 23, 38];
const scoreDraftsFromClassicRow = (row) => Object.fromEntries(classicOrderStarts.flatMap((start, orderIndex) => {
  if (!row[start + 2] || (orderIndex === 2 && row.length !== 55)) return [];
  const fields = classicScoreFields[orderIndex];
  const sheetWidth = Number(row[start + 3]);
  const rawScores = fields.map((field) => String(row[field] ?? '').trim());
  const parsedScores = rawScores.flatMap((value) => value ? value.split(/[\s,;]+/).filter(Boolean) : [])
    .filter((value) => Number(value) > 0);
  const noScoreSentinel = parsedScores.length === 1 && Number(parsedScores[0]) === sheetWidth;
  const scoreText = noScoreSentinel ? '' : parsedScores
    .map((value) => Number(value))
    .filter((value) => Number.isFinite(value) && value > 0)
    .map((value) => String(value / 10))
    .join('\n');
  return [[orderIndex, scoreText]];
}));
const parsePositiveTenthsMm = (value) => {
  if (value === null || value === undefined || String(value).trim() === '') return null;
  const millimeters = Number(value);
  if (!Number.isFinite(millimeters) || millimeters <= 0) return NaN;
  const tenths = Math.round(millimeters * 10);
  return Math.abs(millimeters * 10 - tenths) <= 0.000001 ? tenths : NaN;
};
// Tenths of a mm for a crease draft: null with no creases, NaN when a line is not a valid measure.
const scoreDraftSumTenths = (draft) => {
  const lines = String(draft || '').split(/[\s,;]+/).filter(Boolean);
  if (lines.length === 0) return null;
  return lines.reduce((total, line) => total + parsePositiveTenthsMm(line), 0);
};
const parseNonnegativeTenthsMm = (value) => {
  if (value === null || value === undefined || String(value).trim() === '') return null;
  const millimeters = Number(value);
  if (!Number.isFinite(millimeters) || millimeters < 0) return NaN;
  const tenths = Math.round(millimeters * 10);
  return Math.abs(millimeters * 10 - tenths) <= 0.000001 ? tenths : NaN;
};

function DashboardUpdateAge({ receivedAt, delayed }){
  const [now, setNow] = useState(Date.now());
  const [pulse, setPulse] = useState(false);

  useEffect(() => {
    const timer = setInterval(() => setNow(Date.now()), 1000);
    return () => clearInterval(timer);
  }, []);

  useEffect(() => {
    if(!receivedAt) return undefined;
    setPulse(true);
    const timer = setTimeout(() => setPulse(false), 900);
    return () => clearTimeout(timer);
  }, [receivedAt]);

  const timestamp = Number(receivedAt);
  const seconds = Number.isFinite(timestamp) ? Math.max(0, Math.floor((now - timestamp) / 1000)) : null;
  const stale = delayed || seconds === null;

  return (
    <span className={`dashboard-live-indicator ${stale ? 'is-stale' : ''} ${pulse ? 'is-blinking' : ''}`} role="status" aria-live="off">
      <span className="dashboard-live-indicator__dot" aria-hidden="true" />
      {seconds === null ? 'Waiting for update' : <><span>Updated</span><strong>{seconds}</strong><span>s ago</span></>}
    </span>
  );
}

function Home(){
  const [state, setState] = useState({ loading: true, data: null, unavailable: false, updatedAt: null, receivedAt: null });
  const [versions, setVersions] = useState(null);
  const [operatorStatus, setOperatorStatus] = useState(null);
  const [operatorStatusUnavailable, setOperatorStatusUnavailable] = useState(false);

  useEffect(() => {
    const controller = new AbortController();
    const stream = new EventSource('/api/dashboard/events');
    stream.onmessage = (event) => {
      try {
        const snapshot = JSON.parse(event.data);
        setState({ loading: snapshot.loading, data: snapshot.data, unavailable: snapshot.unavailable, updatedAt: snapshot.updatedAt, receivedAt: Date.now() });
        setOperatorStatus(snapshot.operatorStatus);
        setOperatorStatusUnavailable(snapshot.operatorStatusUnavailable);
      } catch {
          setState((current) => ({ ...current, loading: false, unavailable: true }));
      }
    };

    fetch('/api/syncro/versions', { cache: 'no-store', signal: controller.signal })
      .then((response) => response.ok ? response.json() : null)
      .then(setVersions)
      .catch((error) => {
        if (error.name !== 'AbortError') {
          setVersions(null);
        }
      });

    return () => {
      controller.abort();
      stream.close();
    };
  }, []);

  const data = state.data;
  const shift = data && data.Shift ? data.Shift : {};
  const orders = data && data.Orders ? data.Orders : {};
  const activeMachineAlerts = operatorStatus && operatorStatus.activeAlerts && operatorStatus.activeAlerts.length > 0
    ? operatorStatus.activeAlerts
    : machine_alerts(operatorStatus && operatorStatus.machines);
  const machineStatus = (machine) => machine.inEmergency ? 'Emergency'
    : machine.alarmed ? 'Alarm'
      : machine.warned ? 'Warning'
        : machine.readyToRun ? 'Ready'
          : 'Not ready';

  return (
    <>
      <section className="dashboard-live" aria-live="polite" aria-busy={state.loading}>
        {state.loading && (
          <div className="dashboard-loading" role="status">
            <span className="spinner-border" aria-hidden="true" />
            <span>Loading live production data...</span>
          </div>
        )}
        {state.unavailable && (
          <div className="alert alert-warning mb-0" role="alert">
            Syncro TCP is reachable, but dashboard data could not be read.
          </div>
        )}
        {data && (
          <div className="dashboard-overview">
            <section className="dashboard-panel dashboard-panel--shift" aria-labelledby="shift-overview-title">
              <div className="dashboard-panel__heading">
                <div>
                  <span className="dashboard-eyebrow">Live status</span>
                  <h2 id="shift-overview-title">Shift Overview</h2>
                </div>
                <DashboardUpdateAge receivedAt={state.receivedAt} delayed={state.unavailable} />
              </div>
              <dl className="shift-metrics">
                <div><dt>Shift ID</dt><dd>{numberOrDash(shift.ID)}</dd></div>
                <div><dt>Start Time</dt><dd>{numberOrDash(shift.StartTime)}</dd></div>
                <div><dt>Stop Time</dt><dd>{numberOrDash(shift.StopTime || shift.EndTime)}</dd></div>
              </dl>
            </section>

            <section className="dashboard-panel dashboard-panel--production" aria-labelledby="production-title">
              <div className="dashboard-panel__heading">
                <div>
                  <span className="dashboard-eyebrow">Output</span>
                  <h2 id="production-title">Dry End</h2>
                </div>
                <span className="dashboard-panel__note">3 order levels</span>
              </div>
              <div className="production-levels">
                {[0, 1, 2].map((level) => {
                  const order = orders[`Order${level}`] || {};
                  return (
                    <article className="production-level" key={level}>
                      <span className="production-level__index">Level {level}</span>
                      <strong>{numberOrDash(order.Customer)}</strong>
                      <dl>
                        <div><dt>Required</dt><dd>{numberOrDash(order.Sheets)}</dd></div>
                        <div><dt>Good</dt><dd>{numberOrDash(order.GoodSheets)}</dd></div>
                        <div><dt>Bad</dt><dd>{numberOrDash(order.BadSheets)}</dd></div>
                      </dl>
                    </article>
                  );
                })}
              </div>
            </section>
          </div>
        )}
      </section>

      {data && (
        <section className="dashboard-machine-metrics" aria-label="Production metrics">
          <dl>
            <div><dt>Meters produced</dt><dd>{numberOrDash(shift.MetersProduced)}</dd></div>
            <div><dt>Dry meters required</dt><dd>{numberOrDash(shift.DryMetersRequired)}</dd></div>
            <div><dt>Wet-end meters</dt><dd>{numberOrDash(shift.WetEndMetersProduced)}</dd></div>
            <div><dt>Wet-end required</dt><dd>{numberOrDash(shift.WetEndMetersRequired)}</dd></div>
            <div><dt>Max speed</dt><dd>{numberOrDash(shift.MaxSpeed)}</dd></div>
            <div><dt>AOC speed</dt><dd>{numberOrDash(shift.AOCSpeed)}</dd></div>
            <div><dt>Target speed</dt><dd>{numberOrDash(data.TargetSpeed)}</dd></div>
            <div><dt>Dry setup</dt><dd>{numberOrDash(data.DrySetupCode)}</dd></div>
            <div><dt>Dry setup started</dt><dd>{numberOrDash(data.DrySetupStarted)}</dd></div>
            <div><dt>Wet-end started</dt><dd>{numberOrDash(data.WetEndStartTime)}</dd></div>
          </dl>
        </section>
      )}

      {versions && (
        <section className="dashboard-version-strip" aria-label="Installed software versions">
          <h2 className="visually-hidden">Installed software versions</h2>
          <dl>
            <div><dt>Syncro</dt><dd>{numberOrDash(versions.syncro)}</dd></div>
            <div><dt>Data</dt><dd>{numberOrDash(versions.data)}</dd></div>
            <div><dt>ITE</dt><dd>{numberOrDash(versions.ite)}</dd></div>
          </dl>
        </section>
      )}

      {operatorStatus && (
        <section className="dashboard-operator-status" aria-labelledby="operator-status-title" aria-live="polite">
          <div className="dashboard-operator-status__heading">
            <h2 id="operator-status-title">Line status</h2>
            <span className={`dashboard-operator-status__overall ${operatorStatus.readyToRun ? 'is-ready' : 'is-not-ready'}`}>
              {operatorStatus.readyToRun ? 'Ready to run' : 'Not ready to run'}
            </span>
          </div>
          <div className="dashboard-operator-status__metrics">
            <div><dt>Line speed</dt><dd>{numberOrDash(operatorStatus.lineSpeed)} m/min</dd></div>
            <div><dt>Dry meters left</dt><dd>{numberOrDash(operatorStatus.dryMetersLeft)}</dd></div>
            <div><dt>Wet-end meters left</dt><dd>{numberOrDash(operatorStatus.wetEndMetersLeft)}</dd></div>
            <div><dt>Setups in queue</dt><dd>{numberOrDash(operatorStatus.drySetupCount)}</dd></div>
            <div><dt>Dry setup progress</dt><dd>{numberOrDash(operatorStatus.dryProgressPercent)}%</dd></div>
            <div><dt>AOC mode</dt><dd>{numberOrDash(operatorStatus.aocMode)}</dd></div>
            <div><dt>AOC type</dt><dd>{numberOrDash(operatorStatus.aocType)}</dd></div>
            <div><dt>AOC time left</dt><dd>{numberOrDash(operatorStatus.aocTimeLeft)}</dd></div>
            <div><dt>Simulation</dt><dd>{operatorStatus.simulation ? 'Active' : 'Inactive'}</dd></div>
          </div>
          {activeMachineAlerts.length > 0 && (
            <section className="dashboard-alert-list" aria-labelledby="dashboard-alert-title" aria-live="polite">
              <div className="dashboard-alert-list__heading">
                <h3 id="dashboard-alert-title">Active alarms and warnings</h3>
                <span>{activeMachineAlerts.length}</span>
              </div>
              <ul>
                {activeMachineAlerts.map((alert, index) => (
                  <li className={`is-${alert.severity.toLowerCase()}`} key={`${alert.machine}-${alert.severity}-${index}`}>
                    <strong>{alert.severity}</strong>
                    <span className="dashboard-alert-list__machine">{alert.machine || 'Unknown machine'}</span>
                    {alert.zone && <span className="dashboard-alert-list__zone">{alert.zone}</span>}
                    {alert.description && <p className="dashboard-alert-list__description">{alert.description}</p>}
                    {alert.startedAt && <time className="dashboard-alert-list__time" dateTime={alert.startedAt}>{alert.startedAt}</time>}
                  </li>
                ))}
              </ul>
            </section>
          )}
          <ul className="dashboard-machine-status-list" aria-label="Machine readiness">
            {operatorStatus.machines.filter((machine) => machine.configured).map((machine) => (
              <li data-status={machineStatus(machine).toLowerCase().replaceAll(' ', '-')} key={machine.name}>
                <span className="dashboard-machine-status-list__name">{machine.name}</span>
                <span className="dashboard-machine-status-list__state">{machineStatus(machine)}</span>
                <span className="dashboard-machine-status-list__aoc">AOC {machine.readyToAoc ? 'ready' : 'not ready'}</span>
              </li>
            ))}
          </ul>
          {(operatorStatus.firstStopTime || operatorStatus.firstStopMachine || operatorStatus.firstStopReason) && (
            <p className="dashboard-first-stop" role="status">
              First stop: {numberOrDash(operatorStatus.firstStopMachine)} / {numberOrDash(operatorStatus.firstStopReason)} at {numberOrDash(operatorStatus.firstStopTime)}
            </p>
          )}
        </section>
      )}
      {operatorStatusUnavailable && <div className="alert alert-warning mt-3" role="alert">Live operator data could not be read.</div>}

      <section className="dashboard-actions" aria-label="Quick access">
        <a className="dashboard-action" href="/current">
          <img src="/images/orders-current.png" alt="" />
          <span className="dashboard-action__copy">
            <strong>Current Orders</strong>
            <small>Review active production orders</small>
          </span>
          <span className="dashboard-action__arrow" aria-hidden="true">&rarr;</span>
        </a>
        <a className="dashboard-action" href="/past/1">
          <img src="/images/orders-past.png" alt="" />
          <span className="dashboard-action__copy">
            <strong>Past Orders</strong>
            <small>Browse completed production history</small>
          </span>
          <span className="dashboard-action__arrow" aria-hidden="true">&rarr;</span>
        </a>
      </section>
    </>
  );
}

function Settings(){
  const [settings, setSettings] = useState(null);
  const [activeGroup, setActiveGroup] = useState(0);
  const [unavailable, setUnavailable] = useState(false);
  const [selectedParser, setSelectedParser] = useState('');

  useEffect(() => {
    const controller = new AbortController();
    const pageQuery = new URLSearchParams(window.location.search);
    const dataQuery = new URLSearchParams({
      saved: pageQuery.get('saved') === '1' ? '1' : '0',
      cache: pageQuery.get('cache') === 'cleared' ? 'cleared' : ''
    });

    fetch(`/settings/data?${dataQuery}`, { cache: 'no-store', signal: controller.signal })
      .then((response) => {
        if (!response.ok) {
          throw new Error('Settings unavailable');
        }
        return response.json();
      })
      .then((data) => {
        setSettings(data);
        const parserField = data.groups.flatMap((group) => group.fields).find((field) => field.path === 'orders.parser.type');
        setSelectedParser(parserField ? String(parserField.conf) : '');
        setUnavailable(false);
      })
      .catch((error) => {
        if (error.name !== 'AbortError') {
          setUnavailable(true);
        }
      });

    return () => controller.abort();
  }, []);

  const selectTab = (index) => setActiveGroup(index);
  const handleTabKeyDown = (event, index, groupCount) => {
    let nextIndex;
    if (event.key === 'ArrowRight') {
      nextIndex = (index + 1) % groupCount;
    } else if (event.key === 'ArrowLeft') {
      nextIndex = (index - 1 + groupCount) % groupCount;
    } else if (event.key === 'Home') {
      nextIndex = 0;
    } else if (event.key === 'End') {
      nextIndex = groupCount - 1;
    } else {
      return;
    }

    event.preventDefault();
    selectTab(nextIndex);
    document.getElementById(`settings-tab-${nextIndex}`)?.focus();
  };

  if (!settings && !unavailable) {
    return <div className="settings-content" role="status" aria-busy="true"><span className="spinner-border me-2" aria-hidden="true" />Loading settings...</div>;
  }

  if (unavailable || !settings) {
    return <div className="settings-content alert alert-warning" role="alert">Settings could not be loaded. Refresh the page to try again.</div>;
  }

  return (
    <div id="settings" className="settings-content">
      <div className="alert alert-warning" role="alert">
        Warning: some changes may require a manual service restart.
      </div>
      {settings.cacheCleared && <div className="alert alert-success" role="status">Cached query results cleared. Database records were not changed.</div>}
      {settings.settingsSaved && <div className="alert alert-success" role="status">Settings saved in the database. Some changes require a service restart.</div>}

      <form id="settings-save-form" action="/settings/save" method="post" hidden />
      <div className="settings-tablist" role="tablist" aria-label="Settings sections">
        {settings.groups.map((group, index) => (
          <button
            className={`settings-tab ${index === activeGroup ? 'active' : ''}`}
            id={`settings-tab-${index}`}
            type="button"
            role="tab"
            aria-selected={index === activeGroup}
            aria-controls={group.id}
            tabIndex={index === activeGroup ? 0 : -1}
            key={group.id}
            onClick={() => selectTab(index)}
            onKeyDown={(event) => handleTabKeyDown(event, index, settings.groups.length)}
          >
            {group.title}
          </button>
        ))}
      </div>

      {settings.groups.map((group, index) => (
        <section className="settings-tab-panel" id={group.id} role="tabpanel" aria-labelledby={`settings-tab-${index}`} hidden={index !== activeGroup} key={group.id} tabIndex="0">
          <div className="settings-fields">
            {group.fields.map((field) => (
              <div className="settings-field" key={field.path}>
                <label htmlFor={field.path} className="form-label">{field.path === 'orders.parser.type' ? 'Order file parser' : field.path}</label>
                {Array.isArray(field.options) ? (
                  <>
                    <select
                      className="form-select"
                      id={field.path}
                      name={field.path}
                      form="settings-save-form"
                      value={field.path === 'orders.parser.type' ? selectedParser || String(field.conf) : String(field.conf)}
                      onChange={field.path === 'orders.parser.type' ? (event) => setSelectedParser(event.target.value) : undefined}
                    >
                      {field.options.map((option) => (
                        <option value={option} key={option}>
                          {field.path === 'orders.parser.type' && option === 'extended_orders' ? 'Extended orders (CSV, up to 3 orders)' : option === 'classic' && field.path === 'orders.parser.type' ? 'Classic DAT / CSV optimizer' : option}
                        </option>
                      ))}
                    </select>
                    {field.path === 'orders.parser.type' && (
                      <div className="form-text settings-field__help">
                        {selectedParser === 'extended_orders'
                          ? 'CSV: one row per order; rows sharing setup_code are grouped into one setup. Supports up to 3 orders, 15 crease measurements per order and 22 in total. Put measurements in score_measures_mm, separated by semicolons. Existing fixed-width DAT files remain supported, with 5 crease fields per order.'
                          : 'Classic DAT parser. CSV files are optimized into the classic fixed-width DAT layout before preview and transfer.'}
                      </div>
                    )}
                  </>
                ) : field.type === 'boolean' ? (
                  <select className="form-select" id={field.path} name={field.path} form="settings-save-form" defaultValue={String(field.conf)}>
                    <option value="true">true</option>
                    <option value="false">false</option>
                  </select>
                ) : (
                  <input className="form-control" type={field.type === 'number' ? 'number' : 'text'} id={field.path} name={field.path} form="settings-save-form" defaultValue={field.conf} />
                )}
              </div>
            ))}
          </div>
          {group.id === 'settings-cache' && (
            <section className="mt-3 pt-4 border-top" aria-labelledby="cache-settings-title">
              <h2 id="cache-settings-title" className="h4">Cache maintenance</h2>
              <p>Clear cached query results. This does not delete records from the database.</p>
              <form action="/settings/flush-cache" method="post">
                <button type="submit" className="btn btn-outline-danger">Clear cache</button>
              </form>
            </section>
          )}
        </section>
      ))}

      <button type="submit" form="settings-save-form" className="btn btn-primary mt-3">Save</button>
    </div>
  );
}

function useRemoteData(url, { refreshInterval = 0, keepPreviousData = false } = {}){
  const [data, setData] = useState(null);
  const [error, setError] = useState(false);
  const [refreshIndex, setRefreshIndex] = useState(0);

  useEffect(() => {
    const controller = new AbortController();
    if (!keepPreviousData) setData(null);
    setError(false);
    fetch(url, { cache: 'no-store', signal: controller.signal })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error('Request failed');
        }
        return response.json();
      })
      .then(setData)
      .catch((requestError) => {
        if (requestError.name !== 'AbortError') {
          setError(true);
        }
      });
    const interval = refreshInterval > 0
      ? setInterval(() => setRefreshIndex((value) => value + 1), refreshInterval)
      : null;
    return () => {
      controller.abort();
      if (interval) clearInterval(interval);
    };
  }, [url, refreshIndex, refreshInterval, keepPreviousData]);

  return { data, error, reload: () => setRefreshIndex((value) => value + 1) };
}

// Messages for the rules the server enforces in place of database foreign keys; null for anything else.
const materialRuleMessage = (result) => {
  if (result.error === 'material_in_use') {
    const { type, ids } = result.usedBy;
    const shown = `${ids.slice(0, 5).join(', ')}${ids.length > 5 ? ', ...' : ''}`;
    return type === 'queue'
      ? `Material is used by setups in the Syncro queue (${ids.length}): ${shown}`
      : `Material is used by qualities (${ids.length}): ${shown}`;
  }
  if (result.error === 'material_usage_unknown') return 'Could not check the Syncro queue, so this material was not deleted. Try again when Syncro is connected.';
  if (result.error === 'material_reference_missing') return `Not found in the database: ${result.missing.join(', ')}`;
  return null;
};

function DataList({ title, endpoint, page = 1 }){
  const { data, error, reload } = useRemoteData(`${endpoint}${page}?api=1`);
  const [editing, setEditing] = useState(null);
  const [saving, setSaving] = useState(false);
  const [saveError, setSaveError] = useState('');
  const [savedNotice, setSavedNotice] = useState('');
  const [previewing, setPreviewing] = useState(null);
  const [deleteError, setDeleteError] = useState('');
  const addRequest = useRef(new URLSearchParams(window.location.search).get('add'));
  const dialogRef = useRef(null);
  const previewDialogRef = useRef(null);
  const totalPages = data ? Math.max(0, Number(data.total) || 0) : 0;
  const currentPage = data && Number(data.current) ? Math.min(Number(data.current), Math.max(1, totalPages)) : page;
  const editFields = data && data.editFields || [];
  const visiblePages = [...new Set([1, currentPage - 1, currentPage, currentPage + 1, totalPages].filter((number) => number >= 1 && number <= totalPages))].sort((first, second) => first - second);

  useEffect(() => {
    const dialog = dialogRef.current;
    if (!dialog) return;
    if (editing && !dialog.open) dialog.showModal();
    if (!editing && dialog.open) dialog.close();
  }, [editing]);

  useEffect(() => {
    const dialog = previewDialogRef.current;
    if (!dialog) return;
    if (previewing && !dialog.open) dialog.showModal();
    if (!previewing && dialog.open) dialog.close();
  }, [previewing]);

  const rowValues = (row) => Object.fromEntries(editFields.map((field) => {
    const value = row[field.name];
    if (value === null || value === undefined) return [field.name, ''];
    if (field.type === 'boolean') return [field.name, value ? 'true' : 'false'];
    if (field.type === 'datetime-local') {
      const date = new Date(value);
      const localDate = Number.isNaN(date.getTime()) ? String(value).replace(' ', 'T') : new Date(date.getTime() - date.getTimezoneOffset() * 60000).toISOString();
      return [field.name, localDate.slice(0, 19)];
    }
    return [field.name, String(value)];
  }));

  const rowLabel = (row) => (data.keyFields || []).map((field) => row[field]).join(' / ');

  const editRow = (row) => {
    setSaveError('');
    setSavedNotice('');
    setEditing({
      key: Object.fromEntries((data.keyFields || []).map((field) => [field, row[field]])),
      values: rowValues(row)
    });
  };

  const addRecord = (id = '', fromImport = false) => {
    setSaveError('');
    setSavedNotice('');
    setEditing({
      creating: true,
      fromImport,
      values: Object.fromEntries(editFields.map((field) => [field.name, field.primaryKey ? id : field.type === 'boolean' ? 'false' : '']))
    });
  };

  const deleteRow = async (row) => {
    const label = rowLabel(row);
    if (!window.confirm(localized(`Delete ${label} from the database?`, `Usunąć ${label} z bazy danych?`))) return;
    setPreviewing(null);
    setDeleteError('');
    setSavedNotice('');
    try {
      const response = await fetch(`${endpoint}delete`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ key: Object.fromEntries((data.keyFields || []).map((field) => [field, row[field]])) })
      });
      const result = await response.json();
      if (!response.ok) {
        setDeleteError(materialRuleMessage(result) || (result.error === 'material_not_found'
          ? 'This material was changed or removed. Reload the list and try again.'
          : 'Could not delete this material. Check the database connection.'));
        return;
      }
      setSavedNotice('Record deleted.');
      reload();
    } catch {
      setDeleteError('Could not delete this material. Check the database connection.');
    }
  };

  // ?add=<id> comes from the import preview, for a value missing from the database.
  useEffect(() => {
    if (!addRequest.current || !editFields.length) return;
    const id = addRequest.current;
    addRequest.current = null;
    window.history.replaceState(null, '', window.location.pathname);
    addRecord(id, true);
  }, [data]);

  const copyIntoNewRecord = (rowIndex) => {
    const row = data.rows[Number(rowIndex)];
    if (!row) return;
    setEditing((current) => ({
      ...current,
      values: { ...rowValues(row), ...Object.fromEntries(editFields.filter((field) => field.primaryKey).map((field) => [field.name, current.values[field.name]])) }
    }));
  };

  const saveRow = async (event) => {
    event.preventDefault();
    setSaving(true);
    setSaveError('');
    try {
      const response = await fetch(`${endpoint}${editing.creating ? 'create' : 'update'}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(editing)
      });
      const result = await response.json();
      if (!response.ok) {
        setSaveError(materialRuleMessage(result) || (result.error === 'material_not_found'
          ? 'This material was changed or removed. Reload the list and try again.'
          : result.error === 'material_conflict'
            ? 'A record with these key values already exists.'
            : 'Could not save this material. Check the values and database connection.'));
        return;
      }
      setSavedNotice(!editing.creating ? 'Changes saved.' : editing.fromImport ? 'Record added. Go back to the import tab; validation refreshes automatically.' : 'Record added.');
      setEditing(null);
      reload();
    } catch {
      setSaveError('Could not save this material. Check the values and database connection.');
    } finally {
      setSaving(false);
    }
  };

  return (
    <>
      {savedNotice && <div className="alert alert-success" role="status">{savedNotice}</div>}
      {deleteError && <div className="alert alert-warning" role="alert">{deleteError}</div>}
      {data && !data.unavailable && editFields.length > 0 && <div className="d-flex flex-wrap gap-2 mb-3">
        <button type="button" className="btn btn-primary" onClick={() => addRecord()}>Add record</button>
      </div>}
      {error && <div className="alert alert-warning" role="alert">Database connection unavailable. Could not load records.</div>}
      {!data && !error && <div className="async-data-view__loading" role="status"><span className="spinner-border" aria-hidden="true" />Loading records...</div>}
      {data && (data.unavailable ? (
        <div className="alert alert-warning" role="alert">Database connection unavailable. Could not load records.</div>
      ) : data.rows.length === 0 ? (
        <div className="alert alert-light border" role="status">No records found.</div>
      ) : (
        <div className="data-view-results" role="region" aria-label={title}>
          <table className="table table-striped">
            <thead className="table-dark"><tr>{data.fields.map((field) => <th scope="col" key={field.name}>{field.name}</th>)}{editFields.length > 0 && <th scope="col">Actions</th>}</tr></thead>
            <tbody>{data.rows.map((row, rowIndex) => (
              <tr key={rowIndex} {...(editFields.length > 0 ? {
                className: 'import-preview-row',
                tabIndex: 0,
                onClick: () => setPreviewing(row),
                onKeyDown: (event) => {
                  if (event.target === event.currentTarget && (event.key === 'Enter' || event.key === ' ')) {
                    event.preventDefault();
                    setPreviewing(row);
                  }
                }
              } : {})}>{data.fields.map((field, fieldIndex) => {
                const value = row[field.name] ?? Object.values(row)[fieldIndex];
                return <td key={field.name}>{value === null || value === undefined ? '' : String(value)}</td>;
              })}{editFields.length > 0 && <td><div className="d-flex gap-2">
                <button type="button" className="btn btn-outline-primary btn-sm" onClick={(event) => { event.stopPropagation(); editRow(row); }}>Edit</button>
                <button type="button" className="btn btn-outline-danger btn-sm" onClick={(event) => { event.stopPropagation(); deleteRow(row); }}>Delete</button>
              </div></td>}</tr>
            ))}</tbody>
          </table>
        </div>
      ))}
      {previewing && <dialog className="material-editor-dialog order-detail-dialog" ref={previewDialogRef} aria-labelledby="material-preview-title" onClose={() => setPreviewing(null)}>
        <div>
          <header className="material-editor-dialog__header">
            <div>
              <h2 id="material-preview-title">Record details</h2>
              <p>{rowLabel(previewing)}</p>
            </div>
            <button type="button" className="btn btn-outline-secondary btn-sm" aria-label="Close" onClick={() => setPreviewing(null)}>Close</button>
          </header>
          <dl className="material-editor-dialog__fields m-0">
            {editFields.map((field) => <div className="order-detail-dialog__material-field" key={field.name}>
              <dt>{field.name}</dt>
              <dd>{numberOrDash(previewing[field.name] === null || previewing[field.name] === undefined ? '' : String(previewing[field.name]))}</dd>
            </div>)}
          </dl>
          <footer className="material-editor-dialog__actions">
            <button type="button" className="btn btn-outline-danger me-auto" onClick={() => deleteRow(previewing)}>Delete</button>
            <button type="button" className="btn btn-primary" onClick={() => { const row = previewing; setPreviewing(null); editRow(row); }}>Edit</button>
          </footer>
        </div>
      </dialog>}
      {editing && <dialog className="material-editor-dialog" ref={dialogRef} aria-labelledby="material-editor-title" onClose={() => setEditing(null)}>
        <form onSubmit={saveRow}>
          <header className="material-editor-dialog__header">
            <div>
              <h2 id="material-editor-title">{editing.creating ? 'Add material' : 'Edit material'}</h2>
              <p>{title}</p>
            </div>
            <button type="button" className="btn btn-outline-secondary btn-sm" aria-label="Close" onClick={() => setEditing(null)}>Close</button>
          </header>
          <div className="material-editor-dialog__fields">
            {editing.creating && data.rows.length > 0 && <div className="settings-field material-editor-dialog__field--wide">
              <label className="form-label" htmlFor="material-copy-from">Copy values from</label>
              <select className="form-select" id="material-copy-from" defaultValue="" onChange={(event) => copyIntoNewRecord(event.target.value)}>
                <option value="" disabled>Choose a record...</option>
                {data.rows.map((row, rowIndex) => <option value={rowIndex} key={rowIndex}>{rowLabel(row)}</option>)}
              </select>
            </div>}
            {editFields.filter((field) => !(editing.creating && field.readOnly)).map((field) => <div className={`settings-field ${field.type === 'text' && field.maxLength >= 100 ? 'material-editor-dialog__field--wide' : ''}`} key={field.name}>
              <label className="form-label" htmlFor={`material-field-${field.name}`}>{field.name}</label>
              {field.type === 'boolean' ? <select className="form-select" id={`material-field-${field.name}`} value={editing.values[field.name]} onChange={(event) => setEditing((current) => ({ ...current, values: { ...current.values, [field.name]: event.target.value } }))}>
                <option value="true">Yes</option>
                <option value="false">No</option>
              </select> : <input className="form-control" id={`material-field-${field.name}`} type={field.type === 'integer' ? 'number' : field.type} step={field.type === 'integer' ? '1' : field.type === 'datetime-local' ? '1' : undefined} maxLength={field.maxLength} readOnly={!editing.creating && (field.primaryKey || field.readOnly)} value={editing.values[field.name]} onChange={(event) => setEditing((current) => ({ ...current, values: { ...current.values, [field.name]: event.target.value } }))} required={!editing.creating || field.primaryKey || field.type !== 'text'} />}
            </div>)}
          </div>
          {saveError && <div className="alert alert-warning material-editor-dialog__error" role="alert">{saveError}</div>}
          <footer className="material-editor-dialog__actions">
            <button type="button" className="btn btn-outline-secondary" onClick={() => setEditing(null)} disabled={saving}>Cancel</button>
            <button type="submit" className="btn btn-primary" disabled={saving}>{saving ? 'Saving...' : editing.creating ? 'Add record' : 'Save changes'}</button>
          </footer>
        </form>
      </dialog>}
      {data && totalPages > 1 && (
        <nav aria-label="Pagination">
          <ul className="pagination">
            <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
              {currentPage === 1 ? <span className="page-link" aria-disabled="true">Previous</span> : <a className="page-link" href={`${endpoint}${currentPage - 1}`} aria-label="Previous page">Previous</a>}
            </li>
            {visiblePages.map((number, index) => (
              <React.Fragment key={number}>
                {index > 0 && number - visiblePages[index - 1] > 1 && <li className="page-item disabled" aria-hidden="true"><span className="page-link">...</span></li>}
                <li className={`page-item ${number === currentPage ? 'active' : ''}`}>
                  <a className="page-link" href={`${endpoint}${number}`} aria-current={number === currentPage ? 'page' : undefined}>{number}</a>
                </li>
              </React.Fragment>
            ))}
            <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
              {currentPage === totalPages ? <span className="page-link" aria-disabled="true">Next</span> : <a className="page-link" href={`${endpoint}${currentPage + 1}`} aria-label="Next page">Next</a>}
            </li>
          </ul>
        </nav>
      )}
    </>
  );
}

function CurrentOrders(){
  const { data, error, reload } = useRemoteData('/current/orders', { refreshInterval: 15000, keepPreviousData: true });
  const [clearing, setClearing] = useState(false);
  const clearOrders = async () => {
    setClearing(true);
    try {
      await fetch('/current/clear', { method: 'POST', body: new FormData() });
      // Syncro clears the queue asynchronously, so wait until Orders.Get reports it empty.
      for (let attempt = 0; attempt < 20; attempt += 1) {
        const result = await fetch('/current/orders', { cache: 'no-store' }).then((response) => response.json()).catch(() => null);
        if (result && !result.unavailable && Array.isArray(result.rows) && result.rows.length === 0) break;
        await new Promise((resolve) => setTimeout(resolve, 500));
      }
    } finally {
      setClearing(false);
      reload();
    }
  };

  return (
    <>
      <div className="d-flex flex-wrap gap-2 mt-1">
        <button type="button" className="btn btn-outline-secondary" onClick={reload} disabled={clearing}>Refresh orders</button>
        <button type="button" className="btn btn-outline-danger" onClick={clearOrders} disabled={clearing}>{clearing ? 'Clearing...' : 'Clear Orders'}</button>
      </div>
      {error && <div className="alert alert-warning mt-4" role="alert">Syncro connection unavailable. Current orders could not be loaded.</div>}
      {!data && !error && <div className="async-data-view__loading" role="status"><span className="spinner-border" aria-hidden="true" />Loading...</div>}
      {data && data.unavailable && <div className="alert alert-warning mt-4" role="alert">Syncro connection unavailable. Current orders could not be loaded.</div>}
      {data && !data.unavailable && data.rows.length === 0 && <div className="alert alert-light border mt-4" role="status">No current orders found.</div>}
      {data && !data.unavailable && data.rows.map((row, index) => (
        <div className="current-order" key={`${row.Position}-${index}`}>
          <div className="current-order__summary">
            <div><dt>Position</dt><strong>{numberOrDash(row.Position)}</strong></div>
            <div><dt>Setup code</dt><strong>{numberOrDash(row.SetupCode)}</strong></div>
            <div><dt>Flute</dt><strong>{numberOrDash(row.Flute)}</strong></div>
            <div><dt>Quality</dt><strong>{numberOrDash(row.QualityID)}</strong></div>
          </div>
          <dl className="current-order__details">
            <div><dt>Web width</dt><dd>{numberOrDash(row.WebWidth)}</dd></div>
            <div><dt>Setup length</dt><dd>{numberOrDash(row.ScoreGap)}</dd></div>
            <div><dt>Trim type</dt><dd>{numberOrDash(row.TrimType)}</dd></div>
            <div><dt>Trim</dt><dd>{numberOrDash(row.Trim)}</dd></div>
          </dl>
          <div className="current-order__levels">
            {[0, 1, 2].map((level) => (
              <section key={level} aria-label={`Order ${level + 1}`}>
                <h3>Order {level + 1}</h3>
                <dl>
                  <div><dt>Order number</dt><dd>{numberOrDash(row[`OrderNumber${level}`])}</dd></div>
                  <div><dt>Level</dt><dd>{numberOrDash(row[`LevelName${level}`])}</dd></div>
                  <div><dt>Customer</dt><dd>{numberOrDash(row[`CustomerName${level}`])}</dd></div>
                  <div><dt>Position</dt><dd>{numberOrDash(row[`PositType${level}`])}</dd></div>
                  <div><dt>Address</dt><dd>{numberOrDash(row[`CustAddress${level}`])}</dd></div>
                  <div><dt>Score type</dt><dd>{numberOrDash(row[`ScoreType${level}`])}</dd></div>
                </dl>
              </section>
            ))}
          </div>
        </div>
      ))}
    </>
  );
}

const materialLabels = {
  quality: 'Quality',
  flute: 'Flute'
};

function OrderImport(){
  const [preview, setPreview] = useState(null);
  const [selectedSetup, setSelectedSetup] = useState(null);
  const [scoreSaving, setScoreSaving] = useState(false);
  const [scoreSaveMessage, setScoreSaveMessage] = useState('');
  const setupDialogRef = useRef(null);
  const [error, setError] = useState('');
  const { data: manualOptions, error: manualOptionsError } = useRemoteData('/current/manual/options');
  const [transfer, setTransfer] = useState(null);
  const [busy, setBusy] = useState(false);
  const [manualOrderCount, setManualOrderCount] = useState(1);
  const [manualError, setManualError] = useState('');
  const [manualResult, setManualResult] = useState(null);
  const [manualScores, setManualScores] = useState({});
  const [manualTrimInputs, setManualTrimInputs] = useState({});
  const [manualSetupCode, setManualSetupCode] = useState(createManualSetupCode);
  const [activeTab, setActiveTab] = useState('file');
  const recheckMaterials = Boolean(preview && !transfer && Object.keys(preview.missingMaterials || {}).length > 0);
  // The material form opens in another tab, so coming back means the record may exist now.
  useEffect(() => {
    if (!recheckMaterials) return undefined;
    const revalidate = async () => {
      const response = await fetch('/current/import/revalidate', { method: 'POST' });
      if (!response.ok) return;
      const result = await response.json();
      setPreview((current) => (current ? { ...current, ...result } : current));
      setSelectedSetup((current) => (current ? { ...current, validationError: '' } : current));
    };
    const onFocus = () => { revalidate().catch(() => {}); };
    window.addEventListener('focus', onFocus);
    return () => window.removeEventListener('focus', onFocus);
  }, [recheckMaterials]);
  useEffect(() => {
    const dialog = setupDialogRef.current;
    if (!dialog) return;
    if (selectedSetup && !dialog.open) dialog.showModal();
    if (!selectedSetup && dialog.open) dialog.close();
  }, [selectedSetup]);

  const setupDetails = selectedSetup ? (() => {
    const row = selectedSetup.row;
    const invalidFieldIndexes = preview?.invalid?.[selectedSetup.index]?.fieldIndexes || [];
    const validationError = selectedSetup.validationError || preview?.invalid?.[selectedSetup.index]?.error || '';
    const fromTenths = (value) => {
      if (value === null || value === undefined || String(value).trim() === '' || !Number.isFinite(Number(value))) return '—';
      return `${(Number(value) / 10).toFixed(1)} mm`;
    };
    if (!Array.isArray(row) && row?.format === 'extended_orders_csv') {
      const orders = row.orders.map((order, index) => {
        const level = ({ 3: 'Upper', 2: 'Middle', 1: 'Lower' })[Number(order.level)] || '—';
        return {
          title: `Order ${index + 1}`,
          number: order.orderNumber,
          fields: [
            { label: 'Level', value: level },
            { label: 'Order number', value: order.orderNumber },
            { label: 'Customer', value: order.customerName },
            { label: 'Sheet width (mm)', value: order.sheetWidthMm },
            { label: 'Outs', value: order.outs },
            { label: 'Sheet length (mm)', value: order.sheetLengthMm },
            { label: 'Quantity (sheets)', value: order.quantitySheets },
            { label: 'Sheets per stack', value: order.sheetsPerStack },
            { label: 'Creasing lines (mm)', value: order.scores?.join(', ') || '—' }
          ]
        };
      });
      const usedWidth = row.orders.reduce((total, order) => total + Number(order.sheetWidthMm || 0) * Number(order.outs || 0), 0);
      const trim = Number(row.webWidthMm) - usedWidth;
      return {
        code: row.setupCode,
        webWidth: `${row.webWidthMm} mm`,
        quality: row.qualityId,
        flute: row.fluteId,
        trim: Number.isFinite(trim) ? `${trim.toFixed(1)} mm` : '—',
        orders
      };
    }
    if (Array.isArray(row) && row.length >= 42) {
      const orderStarts = classicOrderStarts;
        const levels = classicLevels(
          [row[18], row[33], row[40] ? row[48] : null],
          Number(preview?.knifeLevelCount) || 3
        );
      const orders = orderStarts.map((start, index) => {
        const orderNumber = row[start + 2];
        if (!orderNumber) return null;
        return {
          title: `Order ${index + 1}`,
          sourceOrderIndex: index,
          number: orderNumber,
          sourceStart: start,
          scoreDrafts: selectedSetup.scoreDrafts?.[index] ?? scoreDraftsFromClassicRow(row)[index],
          customer: row[start],
          level: ({ 3: 'Upper', 2: 'Middle', 1: 'Lower' })[Number(levels[index])] || '—',
          fields: [
            { label: 'Level', value: ({ 3: 'Upper', 2: 'Middle', 1: 'Lower' })[Number(levels[index])] || '—' },
            { label: 'Order number', value: orderNumber, sourceIndexes: [start + 2] },
            { label: 'Customer', value: row[start], sourceIndexes: [start] },
            { label: 'Sheet width (mm)', value: fromTenths(row[start + 3]), sourceIndexes: [start + 3] },
            { label: 'Outs', value: row[start + 12], sourceIndexes: [start + 12] },
            { label: 'Sheet length (mm)', value: fromTenths(row[start + 10]), sourceIndexes: [start + 10] },
            { label: 'Quantity (sheets)', value: row[start + 11], sourceIndexes: [start + 11] },
            { label: 'Sheets per stack', value: Number(row[start + 9]) / 2, sourceIndexes: [start + 9] }
          ]
        };
      }).filter(Boolean);
      const webWidthTenths = Number(row[5]);
      const usedWidthTenths = orders.reduce((total, order) => {
        const widthTenths = Number(row[order.sourceStart + 3]);
        const outs = Number(row[order.sourceStart + 12]);
        return total + widthTenths * outs;
      }, 0);
      return {
        code: `${row[2] || ''}.2`,
        webWidth: fromTenths(row[5]),
        quality: row[3],
        flute: row[4],
        trim: Number.isFinite(webWidthTenths) ? fromTenths(webWidthTenths - usedWidthTenths) : '—',
        orders,
        invalidFieldIndexes,
        validationError,
        canEditScores: true
      };
    }
    return null;
  })() : null;

  const manualMeasureCount = Array.from({ length: manualOrderCount }, (_, index) => manualScores[index + 1] || '')
    .reduce((total, value) => total + value.split(/\r\n|\r|\n/).filter((line) => line.trim() && Number(line) > 0).length, 0);
  const manualTotalOuts = totalOuts(Array.from({ length: manualOrderCount }, (_, index) => manualTrimInputs[`order${index + 1}.outs`]));
  const updateManualTrimInputs = (event) => {
    const { name, value } = event.target;
    if (name === 'webWidthMm' || /^order\d+\.(sheetWidthMm|outs)$/.test(name)) {
      setManualTrimInputs((current) => ({ ...current, [name]: value }));
    }
  };
  const manualTrimPreview = (() => {
    const webWidthTenthsMm = parsePositiveTenthsMm(manualTrimInputs.webWidthMm);
    if (webWidthTenthsMm === null) {
      return { state: 'incomplete', trimTenthsMm: null, usedWidthTenthsMm: null, message: 'Enter web width, sheet widths and outs to calculate trim.' };
    }
    if (!Number.isFinite(webWidthTenthsMm)) {
      return { state: 'invalid', trimTenthsMm: null, usedWidthTenthsMm: null, message: 'Use positive widths with at most one decimal place and outs from 1 to 5.' };
    }

    let usedWidthTenthsMm = 0;
    for (let index = 1; index <= manualOrderCount; index += 1) {
      const sheetWidthValue = manualTrimInputs[`order${index}.sheetWidthMm`];
      const outsValue = manualTrimInputs[`order${index}.outs`];
      if (sheetWidthValue === undefined || sheetWidthValue === '' || outsValue === undefined || outsValue === '') {
        return { state: 'incomplete', trimTenthsMm: null, usedWidthTenthsMm: null, message: 'Enter web width, sheet widths and outs to calculate trim.' };
      }
      const sheetWidthTenthsMm = parsePositiveTenthsMm(sheetWidthValue);
      const outs = Number(outsValue);
      if (!Number.isFinite(sheetWidthTenthsMm) || !Number.isSafeInteger(outs) || outs < 1 || outs > 5) {
        return { state: 'invalid', trimTenthsMm: null, usedWidthTenthsMm: null, message: 'Use positive widths with at most one decimal place and outs from 1 to 5.' };
      }
      usedWidthTenthsMm += sheetWidthTenthsMm * outs;
    }

    const trimTenthsMm = webWidthTenthsMm - usedWidthTenthsMm;
    const configuredMaxTrim = Number(manualOptions && manualOptions.maxTrimTenthsMm);
    const maxTrimTenthsMm = Number.isFinite(configuredMaxTrim) ? configuredMaxTrim : 2300;
    if (trimTenthsMm < 0) {
      return { state: 'invalid', trimTenthsMm, usedWidthTenthsMm, message: 'Used width exceeds web width.' };
    }
    if (trimTenthsMm > maxTrimTenthsMm) {
      return { state: 'invalid', trimTenthsMm, usedWidthTenthsMm, message: 'Calculated trim exceeds configured limit.' };
    }
    for (let index = 1; index <= manualOrderCount; index += 1) {
      const creaseLines = (manualScores[index] || '').split(/\r\n|\r|\n/).map((line) => line.trim()).filter(Boolean);
      for (const creaseLine of creaseLines) {
        const creaseTenthsMm = parseNonnegativeTenthsMm(creaseLine);
        if (creaseTenthsMm === null) continue;
        if (!Number.isFinite(creaseTenthsMm)) {
          return { state: 'invalid', trimTenthsMm, usedWidthTenthsMm, message: 'Crease measurements must be non-negative with at most one decimal place.' };
        }
        if (creaseTenthsMm > webWidthTenthsMm) {
          return { state: 'invalid', trimTenthsMm, usedWidthTenthsMm, message: 'Crease measurements cannot exceed the web width.' };
        }
      }
    }
    return { state: 'valid', trimTenthsMm, usedWidthTenthsMm, message: 'Trim is within configured limits.' };
  })();

  const handleImportTabKeyDown = (event, tab) => {
    const tabOrder = ['file', 'manual'];
    const currentIndex = tabOrder.indexOf(tab);
    const nextIndex = event.key === 'ArrowRight' ? (currentIndex + 1) % tabOrder.length
      : event.key === 'ArrowLeft' ? (currentIndex - 1 + tabOrder.length) % tabOrder.length
        : event.key === 'Home' ? 0
          : event.key === 'End' ? tabOrder.length - 1
            : currentIndex;
    if (nextIndex === currentIndex) return;
    event.preventDefault();
    if (preview && tabOrder[nextIndex] === 'manual') return;
    const nextTab = tabOrder[nextIndex];
    setActiveTab(nextTab);
    document.getElementById(`order-import-tab-${nextTab}`)?.focus();
  };

  const uploadFile = async (event) => {
    event.preventDefault();
    setError('');
    setPreview(null);
    const form = new FormData(event.currentTarget);
    setBusy(true);
    try {
      const response = await fetch('/current/import', { method: 'POST', body: form });
      const result = await response.json();
      if (!response.ok) {
        if (result.error === 'order_ack_unknown') {
          setTransfer({
            current: 0,
            steps: 0,
            complete: false,
            error: `Setup ${result.setupCode || ''} still has an unconfirmed result. Check its status before uploading again.`,
            uncertain: true,
            setupCode: result.setupCode
          });
          return;
        }
        const message = result.error === 'order_file_required'
          ? 'Choose an order file first.'
          : result.error === 'classic_generator_unavailable'
            ? 'The CSV optimizer service is unavailable.'
            : 'The order file could not be read.';
        throw new Error(message);
      }
      setPreview(result);
    } catch (uploadError) {
      setError(uploadError.message);
    } finally {
      setBusy(false);
    }
  };

  const cancelImport = async () => {
    const response = await fetch('/current/import/cancel', { method: 'POST' });
    if (!response.ok) {
      const result = await response.json();
      setError(result.error === 'order_ack_unknown'
        ? `Setup ${result.setupCode || ''} has an unconfirmed result. Check its status before cancelling.`
        : 'Could not cancel the import.');
      return;
    }
    setPreview(null);
    setTransfer(null);
  };

  const openSetupDetails = (row, index) => {
    setScoreSaveMessage('');
    setSelectedSetup({
      row,
      index,
      scoreDrafts: Array.isArray(row) ? scoreDraftsFromClassicRow(row) : null
    });
  };

  const updateScoreDraft = (orderIndex, value) => {
    setScoreSaveMessage('');
    setSelectedSetup((current) => {
      if(!current || !current.scoreDrafts) return current;
      return {
        ...current,
        scoreDrafts: { ...current.scoreDrafts, [orderIndex]: value }
      };
    });
  };

  const saveScoreDrafts = async () => {
    if(!selectedSetup?.scoreDrafts) return;
    setScoreSaving(true);
    setScoreSaveMessage('');
    try{
      const response = await fetch('/current/import/update-scores', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: new URLSearchParams({
          index: String(selectedSetup.index),
          scoresByOrder: JSON.stringify(selectedSetup.scoreDrafts)
        })
      });
      const result = await response.json();
      if(!response.ok || !result.saved){
        setScoreSaveMessage(result.error || 'Could not save the creases.');
        return;
      }

      setPreview((current) => {
        if(!current) return current;
        const rows = [...current.rows];
        const changed = JSON.stringify(rows[selectedSetup.index]) !== JSON.stringify(result.row);
        rows[selectedSetup.index] = result.row;
        const invalid = { ...(current.invalid || {}) };
        if(result.invalid) invalid[selectedSetup.index] = result.invalid;
        else delete invalid[selectedSetup.index];
        const edited = changed ? { ...(current.edited || {}), [selectedSetup.index]: true } : current.edited;
        return { ...current, rows, invalid, edited };
      });
      setSelectedSetup((current) => current ? {
        ...current,
        row: result.row,
        scoreDrafts: scoreDraftsFromClassicRow(result.row),
        validationError: result.invalid?.error || ''
      } : current);
      setScoreSaveMessage(result.invalid
        ? `Creases saved, but the setup still fails validation: ${result.invalid.error}`
        : 'Creases saved; the setup passes validation.');
    }catch(saveError){
      setScoreSaveMessage(saveError.message || 'Could not save the creases.');
    }finally{
      setScoreSaving(false);
    }
  };

  const submitManualSetup = async (event) => {
    event.preventDefault();
    setManualError('');
    setManualResult(null);
    setBusy(true);
    const form = event.currentTarget;
    const fields = new FormData(form);
    const setupInput = String(fields.get('setupCode') || '').trim();
    const setupCode = setupInput.includes('.') ? setupInput : `${setupInput}.2`;
    const orders = Array.from({ length: manualOrderCount }, (_, index) => {
      const field = (name) => fields.get(`order${index + 1}.${name}`);
      return {
        orderNumber: field('orderNumber'),
        customerName: field('customerName'),
        sheetWidthMm: field('sheetWidthMm'),
        outs: field('outs'),
        sheetLengthMm: field('sheetLengthMm'),
        quantitySheets: field('quantitySheets'),
        sheetsPerStack: field('sheetsPerStack'),
        level: field('level'),
        scores: String(field('scores') || '').split(/\r\n|\r|\n/).map((score) => score.trim()).filter(Boolean)
      };
    });

    try {
      const response = await fetch('/current/manual', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          setupCode: setupInput,
          webWidthMm: fields.get('webWidthMm'),
          qualityId: fields.get('qualityId'),
          fluteId: fields.get('fluteId'),
          orders
        })
      });
      const result = await response.json();
      if (response.status === 202 || result.pending) {
        setTransfer({
          current: 0,
          steps: 1,
          complete: false,
          error: `No final acknowledgement for setup ${result.setupCode || setupCode}. Check status before retrying.`,
          uncertain: true,
          setupCode: result.setupCode || setupCode,
          manual: true
        });
        return;
      }
      if (!response.ok || !result.accepted) {
        setManualError(result.error === 'setup_code_already_exists'
          ? `Setup code ${result.setupCode || setupCode} already exists in Syncro.`
          : result.error === 'syncro_unavailable'
            ? 'Could not read the current Syncro queue; no setup was sent.'
            : result.error || 'Syncro rejected the manual setup.');
        return;
      }
      setManualResult(result);
      form.reset();
      setManualOrderCount(1);
      setManualScores({});
      setManualTrimInputs({});
      setManualSetupCode(createManualSetupCode());
    } catch (manualSubmitError) {
      setTransfer({
        current: 0,
        steps: 1,
        complete: false,
        error: `Connection lost while sending setup ${setupCode}. Check status before retrying.`,
        uncertain: true,
        setupCode,
        manual: true
      });
      setManualError(manualSubmitError.message);
    } finally {
      setBusy(false);
    }
  };

  const transferRemaining = async (startingAt, steps) => {
    let current = startingAt;
    while (current < steps) {
      let batch;
      const previewRow = preview && preview.rows[current];
      const pendingSetupCode = previewRow ? (Array.isArray(previewRow) ? previewRow[2] : previewRow.setupCode) || '' : '';
      setTransfer({ current, steps, complete: false, error: null, uncertain: false, waitingForAck: true, setupCode: pendingSetupCode });
      try {
        const response = await fetch(`/current/import/batch/${current + 1}`);
        batch = await response.json();
        if (!response.ok || !batch.status) {
          const uncertain = batch.error === 'order_ack_unknown';
          const fallbackSetupId = pendingSetupCode;
          const message = uncertain
            ? `Syncro has not confirmed setup ${batch.setupCode || fallbackSetupId}. Check queue status before retrying.`
            : batch.error === 'order_rejected'
              ? `Syncro rejected setup ${batch.setupCode || fallbackSetupId}, or its setup code already exists.`
              : batch.error === 'syncro_unavailable'
                ? 'Could not verify the current Syncro queue; this order was not sent.'
              : batch.error || 'Order transfer failed.';
          setTransfer({
            current: batch.current ?? current,
            steps,
            complete: false,
            error: message,
            uncertain,
            setupCode: batch.setupCode || fallbackSetupId
          });
          return;
        }
      } catch (requestError) {
        const fallbackSetupId = pendingSetupCode;
        setTransfer({
          current,
          steps,
          complete: false,
          error: `Connection lost while waiting for setup ${fallbackSetupId}. Check queue status before retrying.`,
          uncertain: true,
          setupCode: fallbackSetupId
        });
        return;
      }

      current = batch.current;
      setTransfer({ current, steps, complete: current >= steps, error: null, uncertain: false, waitingForAck: false });
    }
  };

  const checkImportStatus = async () => {
    setBusy(true);
    try {
      const response = await fetch('/current/import/status', { method: 'POST', body: new FormData() });
      const result = await response.json();
      if (!response.ok) {
        throw new Error(result.error === 'syncro_unavailable'
          ? 'Syncro queue is unavailable. The order result is still unknown.'
          : result.error || 'Could not check order status.');
      }
      if (!result.resolved) {
        setTransfer((previous) => ({
          ...previous,
          error: `Setup ${result.setupCode || ''} is not yet visible in the current Syncro queue. Its result is still unknown.`,
          uncertain: true
        }));
        return;
      }

      if (result.complete) {
        setTransfer({ current: result.current, steps: result.steps, complete: true, error: null, uncertain: false });
        return;
      }
      setTransfer({ current: result.current, steps: result.steps, complete: false, error: null, uncertain: false });
      await transferRemaining(result.current, result.steps);
    } catch (statusError) {
      setTransfer((previous) => ({ ...previous, error: statusError.message, uncertain: true }));
    } finally {
      setBusy(false);
    }
  };

  const confirmImport = async (accepted) => {
    if (!accepted) {
      await fetch('/current/import/confirm', { method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, body: new URLSearchParams({ accept: 'false' }) });
      setPreview(null);
      return;
    }

    setBusy(true);
    setError('');
    try {
      const response = await fetch('/current/import/confirm', { method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, body: new URLSearchParams({ accept: 'true' }) });
      const start = await response.json();
      if (!response.ok || !start.accepted) {
        throw new Error(start.error || 'Could not start import.');
      }
      let current = start.current;
      const steps = start.steps;
      setTransfer({ current, steps, complete: current >= steps, error: null });
      await transferRemaining(current, steps);
    } catch (transferError) {
      setTransfer((previous) => ({ ...previous, error: transferError.message }));
    } finally {
      setBusy(false);
    }
  };

  const removePreviewRow = async (rowIndex) => {
    setBusy(true);
    setError('');
    let removed = false;
    try {
      const response = await fetch('/current/import/remove', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: new URLSearchParams({ index: String(rowIndex) })
      });
      const result = await response.json();
      if (!response.ok || !result.removed) {
        throw new Error(result.error || 'Could not remove this setup.');
      }
      setPreview((current) => {
        if (!current) return current;
        const rows = current.rows.filter((_, index) => index !== rowIndex);
        const reindex = (entries) => Object.fromEntries(Object.entries(entries || {}).flatMap(([index, value]) => {
          const oldIndex = Number(index);
          if (oldIndex === rowIndex) return [];
          return [[String(oldIndex > rowIndex ? oldIndex - 1 : oldIndex), value]];
        }));
        return { ...current, rows, invalid: reindex(current.invalid), warnings: reindex(current.warnings), edited: reindex(current.edited), missingMaterials: reindex(current.missingMaterials) };
      });
      removed = true;
    } catch (removeError) {
      setError(removeError.message);
    } finally {
      setBusy(false);
    }
    return removed;
  };

  const removeSelectedSetup = async () => {
    if (!selectedSetup) return;
    const label = setupDetails?.code || localized(`Setup ${selectedSetup.index + 1}`, `Ustawienie ${selectedSetup.index + 1}`);
    if (!window.confirm(localized(`Remove ${label} from the import?`, `Usunąć ${label} z importu?`))) return;
    if (await removePreviewRow(selectedSetup.index)) setSelectedSetup(null);
  };

  const invalid = preview ? Object.keys(preview.invalid) : [];
  const warningCount = preview ? Object.keys(preview.warnings || {}).length : 0;
  const normalizeScores = (value) => String(value || '').split(/[\s,;]+/).filter(Boolean).join(' ');
  const scoresDirty = Boolean(selectedSetup?.scoreDrafts && Array.isArray(selectedSetup.row)) && (() => {
    const saved = scoreDraftsFromClassicRow(selectedSetup.row);
    return Object.keys(selectedSetup.scoreDrafts).some((orderIndex) => normalizeScores(selectedSetup.scoreDrafts[orderIndex]) !== normalizeScores(saved[orderIndex]));
  })();
  const selectedError = selectedSetup ? preview?.invalid?.[selectedSetup.index]?.error || '' : '';
  const selectedMissing = selectedSetup ? preview?.missingMaterials?.[selectedSetup.index] || [] : [];
  const selectedEdited = Boolean(selectedSetup && preview?.edited?.[selectedSetup.index]);
  const confirmDiscardScores = () => !scoresDirty || window.confirm(localized('Discard unsaved crease changes?', 'Porzucić niezapisane zmiany bigów?'));
  const closeSetupDialog = () => {
    if (confirmDiscardScores()) setSelectedSetup(null);
  };
  const orderSheetWidth = (orderIndex) => Number(selectedSetup.row[classicOrderStarts[orderIndex] + 3]);
  // An order left as the file had it may stay off the sheet width; one edited here may not.
  const scoreSumBlocked = Boolean(selectedSetup?.scoreDrafts && Array.isArray(selectedSetup.row)) && (() => {
    const saved = scoreDraftsFromClassicRow(selectedSetup.row);
    return Object.keys(selectedSetup.scoreDrafts).some((orderIndex) => {
      if (normalizeScores(selectedSetup.scoreDrafts[orderIndex]) === normalizeScores(saved[orderIndex])) return false;
      const sum = scoreDraftSumTenths(selectedSetup.scoreDrafts[orderIndex]);
      return sum !== null && sum !== orderSheetWidth(orderIndex);
    });
  })();
  const popupCreaseCount = selectedSetup?.scoreDrafts
    ? Object.values(selectedSetup.scoreDrafts).reduce((total, value) => total + String(value || '').split(/[\s,;]+/).filter(Boolean).length, 0)
    : 0;
  return (
    <section className="settings-content order-import-content">
      {!transfer && (
        <>
          <div className="settings-tablist" role="tablist" aria-label="Order entry methods">
            <button className={`settings-tab ${activeTab === 'file' ? 'active' : ''}`} id="order-import-tab-file" type="button" role="tab" aria-selected={activeTab === 'file'} aria-controls="order-import-panel-file" tabIndex={activeTab === 'file' ? 0 : -1} onClick={() => setActiveTab('file')} onKeyDown={(event) => handleImportTabKeyDown(event, 'file')}>Import file</button>
            <button className={`settings-tab ${activeTab === 'manual' ? 'active' : ''}`} id="order-import-tab-manual" type="button" role="tab" aria-selected={activeTab === 'manual'} aria-controls="order-import-panel-manual" tabIndex={activeTab === 'manual' ? 0 : -1} disabled={Boolean(preview)} onClick={() => setActiveTab('manual')} onKeyDown={(event) => handleImportTabKeyDown(event, 'manual')}>Enter manually</button>
          </div>
          <section className="settings-tab-panel" id="order-import-panel-file" role="tabpanel" aria-labelledby="order-import-tab-file" hidden={activeTab !== 'file'} tabIndex="0">
            <form onSubmit={uploadFile} className="mb-4">
              <label htmlFor="order-file" className="form-label">Order file input</label>
              <div className="d-flex flex-wrap gap-2">
                <input className="form-control" id="order-file" name="orderFile" type="file" accept=".csv,.txt,.dat" required />
                <button className="btn btn-primary" type="submit" disabled={busy}>{busy ? 'Processing...' : 'Submit'}</button>
              </div>
            </form>
          </section>
          <section className="settings-tab-panel" id="order-import-panel-manual" role="tabpanel" aria-labelledby="order-import-tab-manual" hidden={activeTab !== 'manual'} tabIndex="0">
            <section className="manual-setup-entry mb-4" aria-labelledby="manual-setup-title">
          <h2 id="manual-setup-title" className="h4">Manual setup</h2>
          <form onSubmit={submitManualSetup} onChange={updateManualTrimInputs}>
            <div className="settings-fields manual-setup-fields">
              <div className="settings-field">
                <label className="form-label" htmlFor="manual-setup-code">Setup code</label>
                <input className="form-control" id="manual-setup-code" name="setupCode" maxLength="30" value={manualSetupCode} onChange={(event) => setManualSetupCode(event.target.value)} required />
              </div>
              <div className="settings-field">
                <label className="form-label" htmlFor="manual-web-width">Web width (mm)</label>
                <input className="form-control" id="manual-web-width" name="webWidthMm" type="number" min="0.1" step="0.1" required />
              </div>
              <div className="settings-field">
                <label className="form-label" htmlFor="manual-quality-id">Quality ID</label>
                <select className="form-select" id="manual-quality-id" name="qualityId" defaultValue="" required disabled={!manualOptions || manualOptionsError || manualOptions.qualityIds.length === 0}>
                  <option value="" disabled>Select quality</option>
                  {(manualOptions?.qualityIds || []).map((qualityId) => <option value={qualityId} key={qualityId}>{qualityId}</option>)}
                </select>
              </div>
              <div className="settings-field">
                <label className="form-label" htmlFor="manual-flute-id">Flute ID</label>
                <select className="form-select" id="manual-flute-id" name="fluteId" defaultValue="" required disabled={!manualOptions || manualOptionsError || manualOptions.fluteIds.length === 0}>
                  <option value="" disabled>Select flute</option>
                  {(manualOptions?.fluteIds || []).map((fluteId) => <option value={fluteId} key={fluteId}>{fluteId}</option>)}
                </select>
              </div>
              <div className="settings-field">
                <label className="form-label" htmlFor="manual-order-count">Orders in setup</label>
                <select className="form-select" id="manual-order-count" value={manualOrderCount} onChange={(event) => {
                  const nextCount = Number(event.target.value);
                  setManualOrderCount(nextCount);
                  setManualTrimInputs((current) => {
                    const next = {};
                    for (const [name, value] of Object.entries(current)) {
                      const match = name.match(/^order(\d+)\./);
                      if (!match || Number(match[1]) <= nextCount) next[name] = value;
                    }
                    return next;
                  });
                }}>
                  <option value={1}>1</option>
                  <option value={2}>2</option>
                  <option value={3}>3</option>
                </select>
              </div>
            </div>
            <div className="form-text manual-setup-code-note">Unique setup code generated automatically. Edit if needed.</div>
            <section className={`manual-trim-preview is-${manualTrimPreview.state}`} aria-live="polite" role="status">
              <div className="manual-trim-preview__value"><span>Calculated trim</span><strong>{manualTrimPreview.trimTenthsMm === null ? '—' : `${(manualTrimPreview.trimTenthsMm / 10).toFixed(1)} mm`}</strong></div>
              {manualTrimPreview.usedWidthTenthsMm !== null && <span className="manual-trim-preview__usage">Width used: {(manualTrimPreview.usedWidthTenthsMm / 10).toFixed(1)} mm</span>}
              <span className="manual-trim-preview__message" key={manualTrimPreview.message}>{manualTrimPreview.message}</span>
            </section>
            <div className="manual-orders-grid">
              {Array.from({ length: manualOrderCount }, (_, index) => {
                const orderNumber = index + 1;
                return (
                  <fieldset className="manual-order-fields" key={orderNumber}>
                    <legend>{`Order ${orderNumber}`}</legend>
                    <div className="settings-fields">
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-level`}>Level</label><select className="form-select" id={`manual-order-${orderNumber}-level`} name={`order${orderNumber}.level`} defaultValue={orderNumber === 1 ? '3' : '1'}><option value="3">Upper</option><option value="1">Lower</option></select></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-number`}>Order number</label><input className="form-control" id={`manual-order-${orderNumber}-number`} name={`order${orderNumber}.orderNumber`} maxLength="30" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-customer`}>Customer</label><input className="form-control" id={`manual-order-${orderNumber}-customer`} name={`order${orderNumber}.customerName`} maxLength="15" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-width`}>Sheet width (mm)</label><input className="form-control" id={`manual-order-${orderNumber}-width`} name={`order${orderNumber}.sheetWidthMm`} type="number" min="0.1" step="0.1" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-outs`}>Outs</label><input className="form-control" id={`manual-order-${orderNumber}-outs`} name={`order${orderNumber}.outs`} type="number" min="1" max="5" step="1" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-length`}>Sheet length (mm)</label><input className="form-control" id={`manual-order-${orderNumber}-length`} name={`order${orderNumber}.sheetLengthMm`} type="number" min="0.1" step="0.1" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-quantity`}>Quantity (sheets)</label><input className="form-control" id={`manual-order-${orderNumber}-quantity`} name={`order${orderNumber}.quantitySheets`} type="number" min="1" step="1" required /></div>
                      <div className="settings-field"><label className="form-label" htmlFor={`manual-order-${orderNumber}-stack`}>Sheets per stack</label><input className="form-control" id={`manual-order-${orderNumber}-stack`} name={`order${orderNumber}.sheetsPerStack`} type="number" min="1" step="1" required /></div>
                      <div className="settings-field manual-score-measures">
                        <label className="form-label" htmlFor={`manual-order-${orderNumber}-scores`}>Creasing lines (mm)</label>
                        <div className="manual-score-editor">
                          <div className="manual-score-gutter" aria-hidden="true">
                            {(manualScores[orderNumber] || '').split(/\r\n|\r|\n/).map((_, lineIndex) => <span key={lineIndex}>{lineIndex + 1}</span>)}
                          </div>
                          <textarea
                            className="form-control"
                            id={`manual-order-${orderNumber}-scores`}
                            name={`order${orderNumber}.scores`}
                            rows="6"
                            maxLength="255"
                            placeholder="100"
                            value={manualScores[orderNumber] || ''}
                            onChange={(event) => setManualScores((current) => ({ ...current, [orderNumber]: event.target.value }))}
                            onScroll={(event) => {
                              const gutter = event.currentTarget.previousElementSibling;
                              if (gutter) gutter.scrollTop = event.currentTarget.scrollTop;
                            }}
                          />
                        </div>
                        <div className="form-text">One crease measurement per line. Maximum 15 per order and 22 across the setup.</div>
                      </div>
                    </div>
                  </fieldset>
                );
              })}
            </div>
            <div className="manual-measure-count" aria-live="polite">{manualMeasureCount} / 22 <span>crease measures entered</span></div>
            {manualMeasureCount > 22 && <div className="alert alert-warning mt-2" role="alert">A setup may contain no more than 22 crease measures.</div>}
            {manualTotalOuts > MAX_TOTAL_OUTS && <div className="alert alert-warning mt-2" role="alert">{`The setup has ${manualTotalOuts} outs in total; the limit is ${MAX_TOTAL_OUTS}.`}</div>}
            {manualError && <div className="alert alert-warning mt-3" role="alert">{manualError}</div>}
            {manualResult && <div className="alert alert-success" role="status">Setup {manualResult.setupCode} accepted by Syncro. Trim: {numberOrDash(manualResult.trim / 10)} mm.</div>}
            {manualOptionsError && <div className="alert alert-warning mt-3" role="alert">Quality and flute lists could not be loaded from the database.</div>}
            {manualOptions && (!manualOptions.qualityIds.length || !manualOptions.fluteIds.length) && <div className="alert alert-warning mt-3" role="alert">No quality or flute IDs are available in the database.</div>}
            <button className="btn btn-primary mt-3" type="submit" disabled={busy || manualMeasureCount > 22 || manualTotalOuts > MAX_TOTAL_OUTS || manualTrimPreview.state !== 'valid' || !manualOptions || manualOptionsError || !manualOptions.qualityIds.length || !manualOptions.fluteIds.length}>{busy ? 'Sending...' : 'Send setup to Syncro'}</button>
          </form>
            </section>
          </section>
          <div hidden={activeTab !== 'file'}>
            {error && <div className="alert alert-danger" role="alert">{error}</div>}
            {preview && (
              <>
          <div className="data-view-results">
            <table className="table table-striped">
              <thead className="table-dark"><tr><th>#</th>{preview.fields.map((field) => <th key={field}>{field}</th>)}<th>Action</th></tr></thead>
              <tbody>{preview.rows.map((row, index) => {
                const cells = Array.isArray(row) ? row : row.preview;
                const fieldIndexes = preview.invalid[index]?.fieldIndexes || [];
                const rowClass = preview.invalid[index]
                  ? 'import-preview-row table-danger import-row-invalid'
                  : preview.warnings?.[index] ? 'import-preview-row table-warning import-row-warning' : 'import-preview-row';
                const rowIssue = preview.invalid[index]?.error || preview.warnings?.[index]?.message;
                return <tr className={rowClass} title={rowIssue || undefined} key={index} tabIndex="0" onClick={() => openSetupDetails(row, index)} onKeyDown={(event) => {
                  if (event.key === 'Enter' || event.key === ' ') {
                    event.preventDefault();
                    openSetupDetails(row, index);
                  }
                }}>
                  <td>{index + 1}{preview.edited?.[index] && <span className="badge badge-edited ms-2" title="Scores changed manually">Edited</span>}</td>
                  {cells.map((value, column) => <td className={fieldIndexes.includes(column) ? 'table-danger import-invalid-cell' : ''} key={column}>{value}</td>)}
                  <td><button type="button" className="btn btn-sm btn-outline-danger" disabled={busy} onClick={(event) => { event.stopPropagation(); removePreviewRow(index); }}>Remove setup</button></td>
                </tr>;
              })}</tbody>
            </table>
          </div>
          <section className="import-summary mt-4">
            <div>
              <h2 className="h4 mb-2">Summary</h2>
              <dl className="import-summary__counts">
                {[
                  ['Total', preview.rows.length],
                  ['Ready to send', preview.rows.length - invalid.length - warningCount],
                  ['Skipped', warningCount],
                  ['Invalid', invalid.length]
                ].map(([label, value]) => <div key={label}><dt>{label}</dt><dd>{value}</dd></div>)}
              </dl>
            </div>
            <div className="d-flex gap-2">
              <button type="button" className="btn btn-outline-danger" disabled={busy} onClick={() => confirmImport(false)}>Deny</button>
              <button type="button" className="btn btn-primary" disabled={busy || invalid.length > 0 || preview.rows.length === 0} onClick={() => confirmImport(true)}>Accept</button>
            </div>
          </section>
              </>
      )}
          </div>
        </>
      )}
      {transfer && (
        <section className="import-progress" aria-live="polite">
          <h2 className="h3">{transfer.error ? 'Failed' : transfer.complete ? 'Success' : 'Orders are being added'}</h2>
          <p>
            {transfer.error || (transfer.complete
              ? warningCount > 0
                ? `Skipped setups (${warningCount}): zero quantity or already in Syncro. The remaining setups were sent.`
                : 'Orders were transferred successfully.'
              : transfer.waitingForAck
                ? <><span>Waiting for Syncro acknowledgement for setup</span> <strong>{transfer.setupCode || '...'}</strong>.</>
                : 'Processing order list...')}
          </p>
          <div className="progress" role="progressbar" aria-valuenow={transfer.current} aria-valuemin="0" aria-valuemax={transfer.steps}>
            <div className="progress-bar" style={{ width: `${transfer.steps ? Math.floor(transfer.current / transfer.steps * 100) : 100}%` }}>{transfer.steps ? Math.floor(transfer.current / transfer.steps * 100) : 100}%</div>
          </div>
          <div className="d-flex justify-content-end gap-2 mt-3">
            {!transfer.complete && !transfer.error && <button className="btn btn-outline-danger" type="button" onClick={cancelImport}>Cancel</button>}
            {transfer.uncertain && <button className="btn btn-outline-primary" type="button" onClick={checkImportStatus} disabled={busy}>{busy ? 'Checking...' : 'Check order status'}</button>}
            {(transfer.complete || transfer.error) && <a className="btn btn-primary" href="/current">Go to Orders</a>}
            <a className="btn btn-outline-secondary" href="/current/import">Back</a>
          </div>
        </section>
      )}
      {selectedSetup && <dialog className="material-editor-dialog order-detail-dialog" ref={setupDialogRef} aria-labelledby="order-detail-title" onCancel={(event) => { if (!confirmDiscardScores()) event.preventDefault(); }} onClose={() => setSelectedSetup(null)}>
        <div>
          <header className="material-editor-dialog__header">
            <div>
              <h2 id="order-detail-title">Setup details</h2>
              <p>
                {setupDetails?.code || `Setup ${selectedSetup.index + 1}`}
                {selectedEdited && <span className="badge badge-edited ms-2">Edited</span>}
                {scoresDirty && <span className="badge text-bg-warning ms-2">Unsaved changes</span>}
              </p>
            </div>
            <button type="button" className="btn btn-outline-secondary btn-sm" aria-label="Close" onClick={closeSetupDialog}>Close</button>
          </header>
          {(selectedError || selectedMissing.length > 0) && <div className={`alert ${selectedError ? 'alert-danger' : 'alert-warning'} order-detail-dialog__warning`} role="alert">
            {selectedError && <div>{selectedError}</div>}
            {selectedMissing.map(({ type, id }) => <div key={type}>
              {!selectedError && `${materialLabels[type]} ${id} does not exist in the database. The setup will be sent with this ID. `}
              <a className="alert-link" href={`/${type}/?add=${encodeURIComponent(id)}`} target="_blank" rel="noopener">{`Add ${materialLabels[type].toLowerCase()} ${id} to the database`}</a>
            </div>)}
          </div>}
          {setupDetails ? <div className="order-detail-dialog__fields">
            <section className="order-detail-dialog__setup" aria-label="Setup parameters">
              {[
                ['Web width (mm)', setupDetails.webWidth],
                ['Quality ID', setupDetails.quality],
                ['Flute ID', setupDetails.flute],
                ['Trim (mm)', setupDetails.trim]
              ].map(([label, value]) => <div className="settings-field" key={label}><span className="form-label">{label}</span><strong>{numberOrDash(value)}</strong></div>)}
            </section>
            <section className="order-detail-dialog__orders" aria-label="Orders">
              {setupDetails.orders.map((order) => <fieldset className="manual-order-fields" key={order.number}>
                <legend>{order.title}</legend>
                <dl className="order-detail-dialog__order-fields">
                  {order.fields.map((field) => {
                    const invalid = field.sourceIndexes?.some((sourceIndex) => setupDetails.invalidFieldIndexes?.includes(sourceIndex));
                    return <div className={`order-detail-dialog__order-field ${invalid ? 'is-invalid' : ''}`} title={invalid ? setupDetails.validationError : undefined} key={field.label}><dt>{field.label}</dt><dd>{numberOrDash(field.value)}</dd></div>;
                  })}
                </dl>
                {order.scoreDrafts !== undefined && <div className="order-detail-dialog__score-editor">
                  <label className="form-label" htmlFor={`setup-score-editor-${selectedSetup.index}-${order.sourceOrderIndex}`}>Creasing lines (mm), one per line</label>
                  <div className="manual-score-editor order-detail-dialog__score-lines">
                    <div className="manual-score-gutter" aria-hidden="true">
                      {String(order.scoreDrafts || '').split(/\r\n|\r|\n/).map((_, lineIndex) => <span key={lineIndex}>{lineIndex + 1}</span>)}
                    </div>
                    <textarea
                      className={`form-control ${classicScoreFields[order.sourceOrderIndex].some((fieldIndex) => setupDetails.invalidFieldIndexes.includes(fieldIndex)) ? 'is-invalid' : ''}`}
                      id={`setup-score-editor-${selectedSetup.index}-${order.sourceOrderIndex}`}
                      rows="5"
                      maxLength="255"
                      placeholder="No creases"
                      value={order.scoreDrafts}
                      title={classicScoreFields[order.sourceOrderIndex].some((fieldIndex) => setupDetails.invalidFieldIndexes.includes(fieldIndex)) ? setupDetails.validationError : undefined}
                      onChange={(event) => updateScoreDraft(order.sourceOrderIndex, event.target.value)}
                      onScroll={(event) => {
                        const gutter = event.currentTarget.previousElementSibling;
                        if (gutter) gutter.scrollTop = event.currentTarget.scrollTop;
                      }}
                    />
                  </div>
                  {scoreDraftSumTenths(order.scoreDrafts) !== null && (() => {
                    const sum = scoreDraftSumTenths(order.scoreDrafts);
                    const width = orderSheetWidth(order.sourceOrderIndex);
                    return <div className={`form-text ${sum === width ? '' : 'text-danger'}`} role="status">{`Sum of creases: ${Number.isNaN(sum) ? '—' : sum / 10} / ${width / 10} mm`}</div>;
                  })()}
                </div>}
              </fieldset>)}
            </section>
          </div> : <div className="order-detail-dialog__fallback">Detailed order fields are not available for this row.</div>}
          {setupDetails?.canEditScores && <div className={`order-detail-dialog__score-feedback ${popupCreaseCount > 22 ? 'text-danger' : ''}`} role="status">Creases in setup: {popupCreaseCount} / 22.</div>}
          {preview?.warnings?.[selectedSetup.index] && <div className="alert alert-warning order-detail-dialog__warning" role="status">{preview.warnings[selectedSetup.index].message}</div>}
          {scoreSaveMessage && <div className="order-detail-dialog__score-feedback" role="status">{scoreSaveMessage}</div>}
          <footer className="material-editor-dialog__actions">
            <button type="button" className="btn btn-outline-danger me-auto" onClick={removeSelectedSetup} disabled={busy || scoreSaving}>Remove setup</button>
            {setupDetails?.canEditScores && <button type="button" className="btn btn-primary" onClick={saveScoreDrafts} disabled={!scoresDirty || scoreSumBlocked || scoreSaving || busy || popupCreaseCount > 22}>{scoreSaving ? 'Saving...' : 'Save creases'}</button>}
            <button type="button" className="btn btn-outline-secondary" onClick={closeSetupDialog}>Close</button>
          </footer>
        </div>
      </dialog>}
    </section>
  );
}

function PastExport(){
  const [date, setDate] = useState(new Date().toISOString().slice(0, 10));
  const [submittedDate, setSubmittedDate] = useState(date);
  const { data, error } = useRemoteData(`/past/export?api=1&date=${encodeURIComponent(submittedDate)}`);
  const download = async (format) => {
    const body = new FormData();
    body.set('date', date);
    const response = await fetch(`/past/export.${format}`, { method: 'POST', body });
    if (!response.ok) {
      return;
    }
    const blob = await response.blob();
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = `past-orders-${date}.${format}`;
    link.click();
    URL.revokeObjectURL(link.href);
  };

  return (
    <>
      <form className="d-flex flex-wrap align-items-end gap-2 mb-4" onSubmit={(event) => { event.preventDefault(); setSubmittedDate(date); }}>
        <div><label className="form-label" htmlFor="export-date">Go to date</label><input className="form-control" id="export-date" type="date" value={date} onChange={(event) => setDate(event.target.value)} /></div>
        <button className="btn btn-primary" type="submit">Submit</button>
        <button className="btn btn-outline-primary" type="button" onClick={() => window.print()}>Print / Save as PDF</button>
        <button className="btn btn-outline-secondary" type="button" onClick={() => download('csv')}>Export CSV</button>
        <button className="btn btn-outline-secondary" type="button" onClick={() => download('xlsx')}>Export XLSX</button>
      </form>
      {error && <div className="alert alert-warning" role="alert">Database connection unavailable. Export rows could not be loaded.</div>}
      {!data && !error && <div className="async-data-view__loading" role="status"><span className="spinner-border" aria-hidden="true" />Loading records...</div>}
      {data && (data.unavailable ? <div className="alert alert-warning" role="alert">Database connection unavailable. Export rows could not be loaded.</div> : data.rows.length === 0 ? <div className="alert alert-light border" role="status">No records found.</div> : (
        <div className="data-view-results" id="past_export"><table className="table table-striped"><thead className="table-dark"><tr>{data.fields.map((field) => <th key={field.name}>{field.name}</th>)}</tr></thead><tbody>{data.rows.map((row, index) => <tr key={index}>{data.fields.map((field, fieldIndex) => <td key={field.name}>{String(row[field.name] ?? Object.values(row)[fieldIndex] ?? '')}</td>)}</tr>)}</tbody></table></div>
      ))}
    </>
  );
}

function ShiftOverview(){
  const { data, error, reload } = useRemoteData('/shift/info');
  const stopShift = async () => {
    await fetch('/shift/stop?api=1');
    reload();
  };
  const columns = ['ID', 'StartTime', 'FinishTime', 'RunTime', 'StopTime', 'NumberOfStop', 'SetupChanges', 'QualityChanges', 'ShiftManager', 'People', 'AvgSpeed', 'Job', 'Comm'];
  return (
    <>
      {error && <div className="alert alert-warning" role="alert">Syncro is unavailable. Shift data could not be loaded.</div>}
      {data && data.unavailable && <div className="alert alert-warning" role="alert">Syncro is unavailable. Shift data could not be loaded.</div>}
      {data && !data.unavailable && <>
        <div className={`alert ${data.state === 'idle' ? 'alert-danger' : data.state === 'working' ? 'alert-success' : 'alert-warning'}`} role="status">Shift: {data.state}</div>
        {data.state === 'working' && <button className="btn btn-outline-danger mb-3" type="button" onClick={stopShift}>Stop shift</button>}
        <div className="data-view-results"><table className="table table-striped"><thead className="table-dark"><tr>{columns.map((column) => <th key={column}>{column}</th>)}</tr></thead><tbody>{(data.rows || []).map((row, index) => <tr key={index}>{columns.map((column) => <td key={column}>{numberOrDash(row[column])}</td>)}</tr>)}</tbody></table></div>
      </>}
    </>
  );
}

function ShiftStart(){
  const [message, setMessage] = useState('');
  const [busy, setBusy] = useState(false);
  const startShift = async (event) => {
    event.preventDefault();
    setBusy(true);
    setMessage('');
    const values = Object.fromEntries(new FormData(event.currentTarget));
    try {
      const response = await fetch('/shift/start', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(values) });
      if (!response.ok) {
        throw new Error('Could not start shift. Check the entered values and Syncro connection.');
      }
      window.location.href = '/shift';
    } catch (error) {
      setMessage(error.message);
    } finally {
      setBusy(false);
    }
  };
  return (
    <section className="settings-content">
      <h2 className="h4">New Shift</h2>
      {message && <div className="alert alert-danger" role="alert">{message}</div>}
      <form className="settings-fields" onSubmit={startShift}>
        <div className="settings-field"><label className="form-label" htmlFor="shift-id">ID</label><input className="form-control" id="shift-id" name="ID" type="number" min="1" max="9" required /></div>
        <div className="settings-field"><label className="form-label" htmlFor="shift-manager">Manager</label><input className="form-control" id="shift-manager" name="manager" required /></div>
        <div className="settings-field"><label className="form-label" htmlFor="shift-people">People</label><input className="form-control" id="shift-people" name="people" type="number" min="1" required /></div>
        <div className="settings-field shift-start-submit"><button className="btn btn-primary" type="submit" disabled={busy}>{busy ? 'Submitting...' : 'Submit'}</button></div>
      </form>
    </section>
  );
}

function ConnectionStatus(){
  const [status, setStatus] = useState({ application: 'Connecting', database: 'Checking', syncro: 'Checking' });
  useEffect(() => {
    const stream = new EventSource('/health/events');
    stream.onopen = () => setStatus((current) => ({ ...current, application: 'Connected' }));
    stream.onmessage = (event) => {
      const next = JSON.parse(event.data);
      setStatus({ application: 'Connected', database: next.database ? 'Connected' : 'Disconnected', syncro: next.syncro ? 'Connected' : 'Disconnected' });
    };
    stream.onerror = () => setStatus((current) => ({ ...current, application: 'Disconnected' }));
    return () => stream.close();
  }, []);
  return (
    <div className="connection-status connection-status--navbar" aria-label="Connection status">
      {Object.entries(status).map(([service, value]) => <span className="connection-status__item" id={`${service}-status`} data-state={value === 'Connected' ? 'connected' : value === 'Disconnected' ? 'disconnected' : 'checking'} aria-live="polite" key={service}><span className="connection-status__label">{service === 'database' ? 'PostgreSQL' : service === 'syncro' ? 'Syncro TCP' : 'Application'}</span><span className="connection-status__value">{value}</span></span>)}
    </div>
  );
}

function Application(){
  const pathname = window.location.pathname;
  const page = pathname === '/' ? 'home'
    : pathname.startsWith('/settings') ? 'settings'
      : pathname === '/current/import' ? 'import'
        : pathname.startsWith('/current') ? 'current'
          : pathname === '/past/export' ? 'export'
            : pathname.startsWith('/past') ? 'past'
              : pathname.startsWith('/papers') ? 'papers'
                : pathname.startsWith('/quality') ? 'quality'
                  : pathname.startsWith('/flute') ? 'flute'
                    : pathname === '/shift/start' ? 'shift-start'
                      : pathname.startsWith('/shift') ? 'shift'
                        : 'not-found';
  const pageTitles = { home: 'Home dashboard', settings: 'Settings', import: 'Current Order Import', current: 'Current Orders', export: 'Past Order Export', past: 'Past Order List', papers: 'Paper List', quality: 'Quality List', flute: 'Flute List', shift: 'Shift List', 'shift-start': 'Shift Start', 'not-found': 'Page not found' };

  return (
    <>
      <nav className="navbar navbar-expand-xl navbar-dark fixed-top bg-dark">
        <div className="container-fluid navbar-shell">
          <a className="navbar-brand" href="/">Syncro Orders</a>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation"><span className="navbar-toggler-icon" /></button>
          <div className="collapse navbar-collapse" id="navbarCollapse">
            <ul className="navbar-nav navbar-links mb-2 mb-lg-0">
              <li className="nav-item"><a className={`nav-link ${page === 'home' ? 'active' : ''}`} href="/">Home</a></li>
              <li className="nav-item dropdown"><a className={`nav-link dropdown-toggle ${['current', 'import', 'shift', 'shift-start'].includes(page) ? 'active' : ''}`} href="#operations" data-bs-toggle="dropdown" role="button" aria-expanded="false">Operations</a><ul className="dropdown-menu"><li><a className="dropdown-item" href="/current">Current Orders</a></li><li><a className="dropdown-item" href="/current/import">Import Orders</a></li><li><a className="dropdown-item" href="/shift">Shift Overview</a></li><li><a className="dropdown-item" href="/shift/start">Start Shift</a></li></ul></li>
              <li className="nav-item dropdown"><a className={`nav-link dropdown-toggle ${['papers', 'quality', 'flute'].includes(page) ? 'active' : ''}`} href="#materials" data-bs-toggle="dropdown" role="button" aria-expanded="false">Materials</a><ul className="dropdown-menu"><li><a className="dropdown-item" href="/papers">Papers</a></li><li><a className="dropdown-item" href="/quality">Quality</a></li><li><a className="dropdown-item" href="/flute">Flute</a></li></ul></li>
              <li className="nav-item dropdown"><a className={`nav-link dropdown-toggle ${['past', 'export'].includes(page) ? 'active' : ''}`} href="#history" data-bs-toggle="dropdown" role="button" aria-expanded="false">History</a><ul className="dropdown-menu"><li><a className="dropdown-item" href="/past/1">Past Orders</a></li><li><a className="dropdown-item" href="/past/export">Export</a></li></ul></li>
              <li className="nav-item"><a className={`nav-link ${page === 'settings' ? 'active' : ''}`} href="/settings">Settings</a></li>
            </ul>
            <div className="navbar-utility">
              <ConnectionStatus />
              <div id="language-picker" className="language-switch" role="group" aria-label="Language"><button className="language-switch__button" type="button" data-language="en" lang="en" aria-pressed="true">EN</button><button className="language-switch__button" type="button" data-language="pl" lang="pl" aria-pressed="false">PL</button></div>
            </div>
          </div>
        </div>
      </nav>
      <main className="container page-main" id="main">
        <h1 className="pt-2">{pageTitles[page]}</h1>
        <hr />
        {page === 'home' && <Home />}
        {page === 'settings' && <Settings />}
        {page === 'current' && <CurrentOrders />}
        {page === 'import' && <OrderImport />}
        {page === 'past' && <DataList title={pageTitles.past} endpoint="/past/" page={Number(pathname.split('/')[2]) || 1} />}
        {page === 'papers' && <DataList title={pageTitles.papers} endpoint="/papers/" page={Number(pathname.split('/')[2]) || 1} />}
        {page === 'quality' && <DataList title={pageTitles.quality} endpoint="/quality/" page={Number(pathname.split('/')[2]) || 1} />}
        {page === 'flute' && <DataList title={pageTitles.flute} endpoint="/flute/" page={Number(pathname.split('/')[2]) || 1} />}
        {page === 'export' && <PastExport />}
        {page === 'shift' && <ShiftOverview />}
        {page === 'shift-start' && <ShiftStart />}
        {page === 'not-found' && <div className="alert alert-warning" role="alert">Page not found.</div>}
      </main>
      <footer className="app-footer"><a className="app-footer__brand" href="/">Syncro Orders</a><span>Production operations</span></footer>
    </>
  );
}

const root = document.getElementById('react-app-root');
if (root) {
  createRoot(root).render(<Application />);
}