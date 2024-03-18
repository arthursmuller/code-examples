import 'regenerator-runtime/runtime';
import axios, { AxiosRequestConfig } from 'axios';

import { ListQueryParameters } from '../../types/generic_list';
import { ID } from '../../types/util';
import { getToken } from '../auth';

export class ApiError extends Error {
  status: number;
  body: unknown;
  constructor(status, body) {
    super(`ApiError: ${status}. ${JSON.stringify(body)}`);
    this.status = status;
    this.body = body;
  }
}

export default function Api(state) {
  return {
    login,
    logout,
    companyCreation,
    companyGet,
    companyConsumptionData,
    companyConsumptionSms,
    companyLicenseList,
    groupList,
    groupGet,
    groupCreation,
    groupUpdate,
    groupDelete,
    groupListFilter,
    groupLinkedDeviceList,
    subgroupList,
    subgroupGet,
    subgroupDelete,
    subgroupCreation,
    subgroupUpdate,
    subgroupListFilter,
    subgroupLinkedDeviceList,
    listLocationLast,
    listLocationHistoric,
    listLocationGeolocation,
    messageList,
    messageListDetails,
    messageCreate,
    messageGet,
    documentList,
    documentListDetails,
    documentCreate,
    documentGet,
    adminUserList,
    adminUserPermissions,
    adminUserGet,
    adminUserCreation,
    adminUserEdit,
    deviceUserList,
    deviceUserGet,
    deviceUserEdit,
    deviceUserSelect,
    deviceList,
    deviceGet,
    deviceCreation,
    deviceEdit,
    reportGpsList,
    deviceListManufacturer,
    deviceInventoryList,
    deviceListModel,
    deviceActionRemove,
    deviceActionBlock,
    deviceActionUnblock,
    deviceActionNewPassword,
    deviceActionListPasswords,
    deviceBatteryList,
    deviceStorageList,
    listStates,
    sitesList,
    sitesDateList,
    planList,
    applicationsList,
    qrcodeGenerate,
    applicationDeviceUserList,
    applicationConsumptionHistoryList,
  };

  function login(data) {
    return request('/auth/login', { data, method: 'POST' });
  }

  function logout() {
    return request('/auth/logout', { method: 'GET' });
  }

  // company
  function companyCreation(data) {
    return request('/companies', { data, method: 'POST' });
  }
  function companyGet(id: string) {
    return request(`/companies/${id}`, { method: 'GET' });
  }

  function companyConsumptionData(data) {
    return request('/companies/consumption/data', {data, method: 'GET' });
  }
  function companyConsumptionSms(data) {
    return request('/companies/consumption/sms', {data, method: 'GET' });
  }

  function companyLicenseList(meta: ListQueryParameters) {
    return request('/licenses', { data: meta, method: 'GET' });
  }

  // group
  function groupList(meta: ListQueryParameters) {
    return request('/groups', { data: meta, method: 'GET' });
  }
  function groupGet(id: string) {
    return request(`/groups/${id}`, { method: 'GET' });
  }
  function groupCreation(data) {
    return request('/groups', { data, method: 'POST' });
  }
  function groupUpdate(id, data) {
    return request(`/groups/${id}`, { data, method: 'PUT' });
  }
  function groupDelete(id: string) {
    return request(`/groups/${id}`, { method: 'DELETE' });
  }
  function groupListFilter(meta: ListQueryParameters) {
    return request('/groups/options_for_select', { data: meta, method: 'GET' });
  }
  function groupLinkedDeviceList(meta: ListQueryParameters) {
    return request(`/groups/users`, {
      data: meta,
      method: 'GET',
    });
  }

  // subgroup
  function subgroupList(meta: ListQueryParameters) {
    return request('/subgroups', { data: meta, method: 'GET' });
  }
  function subgroupDelete(id: string) {
    return request(`/subgroups/${id}`, { method: 'DELETE' });
  }
  function subgroupGet(id: string) {
    return request(`/subgroups/${id}`, { method: 'GET' });
  }
  function subgroupCreation(data) {
    return request('/subgroups', { data, method: 'POST' });
  }
  function subgroupUpdate(id, data) {
    return request(`/subgroups/${id}`, { data, method: 'PUT' });
  }
  function subgroupListFilter(meta: ListQueryParameters) {
    return request('/subgroups/options_for_select', {
      data: meta,
      method: 'GET',
    });
  }
  function subgroupLinkedDeviceList(meta: ListQueryParameters) {
    return request(`/subGroups/users`, { data: meta, method: 'GET' });
  }

  // Device Locations
  function listLocationLast(meta: ListQueryParameters) {
    return request('/devices/last_locations', {
      data: meta,
      method: 'GET',
    });
  }
  function listLocationHistoric(meta: ListQueryParameters) {
    return request('/devices/last_locations', {
      data: meta,
      method: 'GET',
    });
  }
  function listLocationGeolocation(meta: ListQueryParameters) {
    return request('/devices/last_locations', {
      data: meta,
      method: 'GET',
    });
  }

  // Messages
  function messageList(meta: ListQueryParameters) {
    return request('/messages', { data: meta, method: 'GET' });
  }

  function messageListDetails(id, meta: ListQueryParameters) {
    return request(`/messages/${id}/recipients`, { data: meta, method: 'GET' });
  }

  function messageCreate(data) {
    return request('/messages', { data, method: 'POST' });
  }

  function messageGet(id: string) {
    return request(`/messages/${id}`, { method: 'GET' });
  }

  // Documents
  function documentList(meta: ListQueryParameters) {
    return request('/documents', { data: meta, method: 'GET' });
  }

  function documentListDetails(id, meta: ListQueryParameters) {
    return request(`/documents/${id}/recipients`, {
      data: meta,
      method: 'GET',
    });
  }

  function documentCreate(data) {
    return request('/documents', { data, method: 'POST' });
  }

  function documentGet(id: string) {
    return request(`/documents/${id}`, { method: 'GET' });
  }

  // AdminUsers
  function adminUserList(meta: ListQueryParameters) {
    return request(`/admin/users`, { data: meta, method: 'GET' });
  }
  function adminUserPermissions(data) {
    return request('/users', { data, method: 'GET' });
  }
  function adminUserGet(id: number) {
    return request(`/admin/users/${id}`, { method: 'GET' });
  }
  function adminUserCreation(data) {
    return request('/admin/users', { data, method: 'POST' });
  }
  function adminUserEdit(id: number, data) {
    return request(`/admin/users/${id}`, { data, method: 'PUT' });
  }

  // Device Users
  function deviceUserList(meta: ListQueryParameters) {
    return request(`/device/users`, {
      data: meta,
      method: 'GET',
    });
  }
  function deviceUserGet(id: ID) {
    return request(`/device/users/${id}`, { method: 'GET' });
  }
  function deviceUserEdit(id: ID, data) {
    return request(`/device/users/${id}`, { data, method: 'PUT' });
  }
  function deviceUserSelect(meta: ListQueryParameters) {
    return request(`/device/users/phones`, {
      data: meta,
      method: 'GET',
    });
  }

  // Devices
  function deviceList(meta: ListQueryParameters) {
    return request(`/devices`, { data: meta, method: 'GET' });
  }
  function deviceGet(id: number) {
    return request(`/devices/${id}`, { method: 'GET' });
  }
  function deviceCreation(data) {
    return request('/devices', { data, method: 'POST' });
  }
  function deviceEdit(id: number, data) {
    return request(`/devices/${id}`, { data, method: 'PUT' });
  }
  function deviceListManufacturer(meta: ListQueryParameters) {
    return request('/devices/manufacturers', { data: meta, method: 'GET' });
  }
  function deviceInventoryList() {
    return request('/devices/inventory', { method: 'GET' });
  }
  function deviceListModel(meta: ListQueryParameters) {
    return request('/devices/models', { data: meta, method: 'GET' });
  }

  function deviceActionRemove(id: ID) {
    return request(`/devices/${id}`, { method: 'POST' });
  }
  function deviceActionBlock(id: ID) {
    return request(`/devices/${id}`, { method: 'POST' });
  }
  function deviceActionUnblock(id: ID) {
    return request(`/devices/${id}`, { method: 'POST' });
  }
  function deviceActionNewPassword(id: ID) {
    return request(`/devices/${id}`, { method: 'POST' });
  }
  function deviceActionListPasswords(id: ID) {
    return request(`/devices/${id}`, { method: 'POST' });
  }
  // Applications
  function applicationsList(meta: ListQueryParameters) {
    return request('/applications', { data: meta, method: 'GET' });
  }
  // Device Infos
  function deviceBatteryList(id: ID, meta: ListQueryParameters) {
    return request(`/devices/${id}/history/battery`, {
      data: meta,
      method: 'GET',
    });
  }
  function deviceStorageList(id: ID, meta: ListQueryParameters) {
    return request(`/devices/${id}/history/storage`, {
      data: meta,
      method: 'GET',
    });
  }

  // States
  function listStates() {
    return request('/states', { method: 'GET' });
  }

  // Reports Sites
  function sitesList(meta: ListQueryParameters) {
    return request('/sites', { data: meta, method: 'GET' });
  }
  function sitesDateList(meta: ListQueryParameters) {
    return request('/sites/history', { data: meta, method: 'GET' });
  }

  // Plans
  function planList() {
    return request('/licenses/plans', { method: 'GET' });
  }

  // ReportsGps
  function reportGpsList(meta: ListQueryParameters) {
    return request(`/devices/gps`, { data: meta, method: 'GET' });
  }

  // QRCode
  function qrcodeGenerate(data) {
    return request('/devices/provision', { data: data, method: 'POST' });
  }

  // Application
  function applicationDeviceUserList(meta: ListQueryParameters) {
    return request(`/applications/users`, {
      data: meta,
      method: 'GET',
    });
  }
  function applicationConsumptionHistoryList(meta: ListQueryParameters) {
    return request(`/applications/daily_consumptions`, {
      data: meta,
      method: 'GET',
    });
  }

  function request(
    path,
    { method, headers, data, ...config }: AxiosRequestConfig
  ) {
    const BASE_URL =
      process.env.REACT_APP_API_URL || 'http://localhost:3003/api';
    if (method === 'GET' && data) {
      path = [path, toQuery(data)].join('?');
      data = undefined;
    } else {
      data = JSON.stringify(data);
    }
    return axios({
      url: `${BASE_URL}${path}`,
      method,
      data,
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        ContentType: 'application/json',
        Authorization: `Bearer ${getToken(state)}`,
        ...headers,
      },
      ...config,
    })
      .then((response) => {
        return response.data;
      })
      .catch((error) => {
        throw error;
      });
  }

  // TODO: use query-string
  function toQuery(obj) {
    if (!obj) {
      return '';
    }
    return Object.entries(obj)
      .map(([key, val]) => `${key}=${val}`)
      .join('&');
  }
}
