import {
  blockedWebsites,
  lastActivities,
  dataConsumptionPieChart,
  smsConsumptionPieChart,
  applicationConsumption,
  userConsumption,
  applicationConsumptionTime,
  visitedWebsites,
  inventoryPieChart,
  consumption,
  users,
  mapMarkers,
} from './mock';
// Action Types

export const Types = {
  LIST: 'dashboard/LIST',
};

// Reducer

const initialState = {
  blockedWebsites: blockedWebsites(),
  lastActivities: lastActivities(),
  dataConsumptionPieChart: dataConsumptionPieChart(),
  smsConsumptionPieChart: smsConsumptionPieChart(),
  applicationConsumption: applicationConsumption(),
  userConsumption: userConsumption(),
  applicationConsumptionTime: applicationConsumptionTime(),
  visitedWebsites: visitedWebsites(),
  inventoryPieChart: inventoryPieChart(),
  consumption: consumption(),
  users: users(),
  mapMarkers: mapMarkers(),
  users_total_heading: 104,
  uninstalled_applications: 21,
  uninstall_attempts: 12,
  not_activated_licenses: 2,
};

const dashboard = (state = initialState, action) => {
  switch (action.type) {
    default:
      return state;
  }
};

export default dashboard;

// Action Creators

export function listDashboards() {
  return {
    type: Types.LIST,
  };
}
