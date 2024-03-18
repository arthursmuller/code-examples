import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { DeviceUserType } from '../../types/deviceUser';
import { ListMetadata, ListQueryParameters } from '../../types/generic_list';

// Action Types

export const Types = {
  LIST: 'deviceUser/LIST',
  SELECT: 'deviceUser/SELECT',
  GET: 'deviceUser/GET',
  EDIT: 'deviceUser/EDIT',
  TOASTER_USER: 'user/TOASTER',
  LIST_FILTER: 'deviceUser/LIST_FILTER',
};

// Reducer

interface DeviceUserState {
  deviceUsers: DeviceUserType[];
  deviceUser: DeviceUserType;
  metadata: ListMetadata;
  errors: Error;
  devicesUsersToFilter: DeviceUserType[];
  toaster: boolean;
}

const initialState: DeviceUserState = {
  deviceUsers: [],
  deviceUser: {},
  metadata: { sortingProperty: 'id' },
  errors: undefined,
  devicesUsersToFilter: [],
  toaster: false,
};

export const deviceUserSlice = createSlice({
  name: 'deviceUsers',
  initialState,
  reducers: {
    deviceUserError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    deviceUserToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    deviceUserMetadata: (state, action: PayloadAction<ListMetadata>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    deviceUserSelected: (
      state,
      action: PayloadAction<Partial<DeviceUserType>>
    ) => {
      state.deviceUser = { ...state.deviceUser, ...action.payload };
    },
    deviceUserSelectedClear: (state) => {
      state.deviceUser = initialState.deviceUser;
    },
    deviceUserListSuccess: (state, action: PayloadAction<DeviceUserType[]>) => {
      state.deviceUsers = action.payload;
    },
    deviceUserFilterListSuccess: (state, action: PayloadAction<DeviceUserType[]>) => {
      state.devicesUsersToFilter = action.payload;
    },
    deviceUserGetSuccess: (state, action: PayloadAction<DeviceUserType>) => {
      state.deviceUser = action.payload;
    },
    deviceUserEditSuccess: (state, action: PayloadAction<DeviceUserType>) => {
      state.deviceUser = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
  },
});

export default deviceUserSlice.reducer;

// Action Creators

export const {
  deviceUserError,
  deviceUserToaster,
  deviceUserMetadata,
  deviceUserSelected,
  deviceUserSelectedClear,
  deviceUserListSuccess,
  deviceUserFilterListSuccess,
  deviceUserGetSuccess,
  deviceUserEditSuccess,
} = deviceUserSlice.actions;

export function listDeviceUsers(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listDeviceUserToFilter(
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_FILTER,
    payload: { filters: sanitizeFilter(filters) },
  };
}

export function getDeviceUser(id: DeviceUserState['deviceUser']['id']) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function editDeviceUser(data) {
  return {
    type: Types.EDIT,
    payload: data,
  };
}
