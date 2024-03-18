// Action Types

import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import {
  ApplicationConsumptionHistoryType,
  ApplicationDeviceUserType,
  ApplicationsFilter,
  ApplicationType,
} from '../../types/application';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';

export const Types = {
  LIST: 'application/LIST',
  CHECK_INSTALL_APPLICATION: 'application/android/CHECK_INSTALL_APPLICATION',
  CHECK_UNINSTALL_APPLICATION:
    'application/android/CHECK_UNINSTALL_APPLICATION',
  CREATE_APPLICATION_SUCCESS: 'application/android/CREATE_APPLICATION_SUCCESS',
  CREATE_APPLICATION: 'application/android/CREATE_APPLICATION',
  DESTROY_APPLICATION_SUCCESS:
    'application/android/DESTROY_APPLICATION_SUCCESS',
  DESTROY_APPLICATION: 'application/android/DESTROY_APPLICATION',
  LIST_APPLICATION_DEVICE_USERS: 'application/deviceusers/LIST',
  LIST_APPLICATION_CONSUMPTION_HISTORY:
    'application/LIST_APPLICATION_CONSUMPTION_HISTORY',
  SEND_APPLICATION_SUCCESS: 'application/android/SEND_APPLICATION_SUCCESS',
  SEND_APPLICATION: 'application/android/SEND_APPLICATION',
  TOASTER_APPLICATION: 'application/android/TOASTER',
  TOASTER_SEND_APPLICATION: 'application/android/TOASTER_SEND_APPLICATION',
  UPDATE_APPLICATION_SEARCH: 'application/android/UPDATE_APPLICATION_SEARCH',
  UPDATE_TAB: 'application/UPDATE_TAB',
};

// Reducer
interface ApplicationsState {
  application: ApplicationType;
  applications: ApplicationType[];
  consumptionHistorys: ApplicationConsumptionHistoryType[];
  deviceUser: ApplicationDeviceUserType;
  deviceUsers: ApplicationDeviceUserType[];
  deviceUsersMetadata: ListMetadata;
  errors: Error;
  filter: ApplicationsFilter;
  metadata: ListMetadata;
  toaster: boolean;
}

const initialState: ApplicationsState = {
  application: {},
  applications: [],
  consumptionHistorys: [],
  deviceUser: {},
  deviceUsers: [],
  errors: null,
  filter: {},
  metadata: {},
  deviceUsersMetadata: {},
  toaster: false,
};

export const applicationSlice = createSlice({
  name: 'applications',
  initialState,
  reducers: {
    applicationConsumptionHistoryListSuccess: (
      state,
      action: PayloadAction<ApplicationConsumptionHistoryType[]>
    ) => {
      state.consumptionHistorys = action.payload;
      state.errors = initialState.errors;
    },
    applicationDeviceUsersListSuccess: (
      state,
      action: PayloadAction<ApplicationDeviceUserType[]>
    ) => {
      state.deviceUsers = action.payload;
      state.errors = initialState.errors;
    },
    applicationDeviceUsersMetadata: (
      state,
      action: PayloadAction<ListMetadata>
    ) => {
      state.deviceUsersMetadata = {
        ...state.deviceUsersMetadata,
        ...action.payload,
      };
    },
    applicationDeviceUserSet: (
      state,
      action: PayloadAction<ApplicationDeviceUserType['userId']>
    ) => {
      const deviceUserId = action.payload;
      state.deviceUser = state.deviceUsers.find(
        (deviceUser) => deviceUser.userId === deviceUserId
      );
    },
    applicationError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    applicationListSuccess: (
      state,
      action: PayloadAction<ApplicationType[]>
    ) => {
      state.applications = action.payload;
    },
    applicationPagination: (
      state,
      action: PayloadAction<PaginationPayload>
    ) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    applicationSelected: (
      state,
      action: PayloadAction<Partial<ApplicationType>>
    ) => {
      state.application = { ...state.application, ...action.payload };
    },
    applicationSelectedClear: (state) => {
      state.application = initialState.application;
    },
    applicationSet: (state, action: PayloadAction<ApplicationType['name']>) => {
      const applicationName = action.payload;
      state.application = state.applications.find(
        (application) => application.name === applicationName
      );
    },
    applicationSetFilter: (
      state,
      action: PayloadAction<Partial<ApplicationsFilter>>
    ) => {
      state.filter = { ...state.filter, ...action.payload };
    },
    applicationToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
  },
});

export default applicationSlice.reducer;

// Action Creators

export const {
  applicationConsumptionHistoryListSuccess,
  applicationDeviceUserSet,
  applicationDeviceUsersListSuccess,
  applicationDeviceUsersMetadata,
  applicationError,
  applicationListSuccess,
  applicationPagination,
  applicationSet,
  applicationSetFilter,
} = applicationSlice.actions;

export function checkInstallApplication(data) {
  return {
    type: Types.CHECK_INSTALL_APPLICATION,
    payload: data,
  };
}

export function checkUninstallApplication(data) {
  return {
    type: Types.CHECK_UNINSTALL_APPLICATION,
    payload: data,
  };
}

export function closeApplicationToaster() {
  return {
    type: Types.TOASTER_APPLICATION,
    payload: false,
  };
}

export function closeSentToaster() {
  return {
    type: Types.TOASTER_SEND_APPLICATION,
    payload: false,
  };
}

export function createApplication(data) {
  return {
    type: Types.CREATE_APPLICATION,
    payload: data,
  };
}

export function createApplicationSuccess(data) {
  return {
    type: Types.CREATE_APPLICATION_SUCCESS,
    payload: data,
  };
}

export function destroyApplication(id) {
  return {
    type: Types.DESTROY_APPLICATION,
    payload: id,
  };
}

export function destroyApplicationSuccess(id) {
  return {
    type: Types.DESTROY_APPLICATION_SUCCESS,
    payload: id,
  };
}

export function listApplications(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listApplicationConsumptionHistory(
  queryParameters: ListQueryParameters & {
    applicationName: string;
    userId?: string | number;
  },
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_APPLICATION_CONSUMPTION_HISTORY,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listApplicationDeviceUsers(
  queryParameters: ListQueryParameters & { applicationName: string },
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_APPLICATION_DEVICE_USERS,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function searchApplication(search) {
  return {
    type: Types.UPDATE_APPLICATION_SEARCH,
    payload: search,
  };
}

export function sendApplication(data) {
  return {
    type: Types.SEND_APPLICATION,
    payload: data,
  };
}

export function sendApplicationSuccess(data) {
  return {
    type: Types.SEND_APPLICATION_SUCCESS,
    payload: data,
  };
}

export function updateSelectedTab(index) {
  return {
    type: Types.UPDATE_TAB,
    payload: index,
  };
}
