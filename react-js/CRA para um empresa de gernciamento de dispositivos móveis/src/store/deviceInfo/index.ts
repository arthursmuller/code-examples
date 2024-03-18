import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { DeviceType } from '../../types/device';
import {
  DeviceInfoBatteryType,
  DeviceInfoStorageType,
} from '../../types/deviceInfo';

// Action Types

export const Types = {
  LIST_BATTERY: 'deviceInfo/LIST_BATTERY',
  LIST_STORAGE: 'deviceInfo/LIST_STORAGE',
};

// Reducer

interface DeviceInfoState {
  battery: DeviceInfoBatteryType[];
  storage: DeviceInfoStorageType[];
  errors: Error;
}

const initialState: DeviceInfoState = {
  battery: [],
  storage: [],
  errors: null,
};

export const deviceInfoSlice = createSlice({
  name: 'deviceInfos',
  initialState,
  reducers: {
    deviceInfoError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
    },
    deviceInfoBatteryListSuccess: (
      state,
      action: PayloadAction<DeviceInfoBatteryType[]>
    ) => {
      state.battery = action.payload;
    },
    deviceInfoStorageListSuccess: (
      state,
      action: PayloadAction<DeviceInfoBatteryType[]>
    ) => {
      state.storage = action.payload;
    },
  },
});

export default deviceInfoSlice.reducer;

// Action Creators

export const {
  deviceInfoError,
  deviceInfoBatteryListSuccess,
  deviceInfoStorageListSuccess,
} = deviceInfoSlice.actions;

export function listDeviceBattery(
  idDeviceInfo: DeviceType['id'],
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_BATTERY,
    payload: { idDeviceInfo, filters: sanitizeFilter(filters) },
  };
}

export function listDeviceStorage(
  idDeviceInfo: DeviceType['id'],
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_STORAGE,
    payload: { idDeviceInfo, filters: sanitizeFilter(filters) },
  };
}
