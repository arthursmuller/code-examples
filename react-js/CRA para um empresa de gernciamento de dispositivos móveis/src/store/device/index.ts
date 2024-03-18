import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import {
  DeviceType,
  InventoryItemType,
  ManufacturerType,
  PasswordsHistoricType,
  ModelType,
} from '../../types/device';
import { ListMetadata, ListQueryParameters } from '../../types/generic_list';
import { ID } from '../../types/util';

// Action Types

export const Types = {
  LIST: 'device/LIST',
  GET: 'device/GET',
  CREATE: 'device/CREATE',
  EDIT: 'device/EDIT',
  LIST_MANUFACTURER: 'device/LIST_MANUFACTURER',
  LIST_INVENTORY: 'device/LIST_INVENTORY',
  LIST_MODEL: 'device/LIST_MODEL',
  ACTION_REMOVE: 'device/ACTION_REMOVE',
  ACTION_BLOCK: 'device/ACTION_BLOCK',
  ACTION_UNBLOCK: 'device/ACTION_UNBLOCK',
  ACTION_NEWPASSWORD: 'device/ACTION_NEWPASSWORD',
  ACTION_LISTPASSWORDS: 'device/ACTION_LISTPASSWORDS',
};

// Reducer

interface DeviceState {
  devices: DeviceType[];
  device: DeviceType;
  manufacturers: ManufacturerType[];
  models: ModelType[];
  metadata: ListMetadata;
  errors: Error;
  showToaster: boolean;
  inventory: InventoryItemType[];
  passwordsHistoric: PasswordsHistoricType[];
}

const initialState: DeviceState = {
  devices: [],
  device: {},
  metadata: { sortingProperty: 'id' },
  errors: null,
  showToaster: false,
  passwordsHistoric: [],
  manufacturers: [],
  inventory: [],
  models: [],
};

export const deviceSlice = createSlice({
  name: 'devices',
  initialState,
  reducers: {
    deviceError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
    },
    deviceToaster: (state, action: PayloadAction<boolean>) => {
      state.showToaster = action.payload;
    },
    deviceMetadata: (state, action: PayloadAction<ListMetadata>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    deviceCreateSuccess: (state, action: PayloadAction<DeviceType>) => {
      state.device = action.payload;
      state.errors = initialState.errors;
      state.showToaster = true;
    },
    deviceListSuccess: (state, action: PayloadAction<DeviceType[]>) => {
      state.devices = action.payload;
      state.errors = initialState.errors;
    },
    deviceManufacturersListSuccess: (
      state,
      action: PayloadAction<ManufacturerType[]>
    ) => {
      state.manufacturers = action.payload;
    },
    deviceInventoryListSuccess: (
      state,
      action: PayloadAction<InventoryItemType[]>
    ) => {
      state.inventory = action.payload;
    },

    deviceModelsListSuccess: (state, action: PayloadAction<ModelType[]>) => {
      state.models = action.payload;
    },
    deviceGetSuccess: (state, action: PayloadAction<DeviceType>) => {
      state.device = action.payload;
    },
    deviceEditSuccess: (state, action: PayloadAction<DeviceType>) => {
      state.device = action.payload;
      state.errors = initialState.errors;
      state.showToaster = true;
    },
    deviceActionRemoveSuccess: (state) => {
      state.errors = initialState.errors;
    },
    deviceActionBlockSuccess: (state) => {
      state.errors = initialState.errors;
    },
    deviceActionUnblockSuccess: (state) => {
      state.errors = initialState.errors;
    },
    deviceActionNewPasswordSuccess: (state) => {
      state.errors = initialState.errors;
    },
    deviceActionListPasswordsSuccess: (
      state,
      action: PayloadAction<PasswordsHistoricType[]>
    ) => {
      state.passwordsHistoric = action.payload;
    },
  },
});

export default deviceSlice.reducer;

// Action Creators

export const {
  deviceError,
  deviceToaster,
  deviceMetadata,
  deviceCreateSuccess,
  deviceListSuccess,
  deviceGetSuccess,
  deviceEditSuccess,
  deviceManufacturersListSuccess,
  deviceInventoryListSuccess,
  deviceModelsListSuccess,
  deviceActionRemoveSuccess,
  deviceActionBlockSuccess,
  deviceActionUnblockSuccess,
  deviceActionNewPasswordSuccess,
  deviceActionListPasswordsSuccess,
} = deviceSlice.actions;

export function listDevices(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function getDevices(id: ID) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createDevice(data) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function editDevice(data) {
  return {
    type: Types.EDIT,
    payload: data,
  };
}

export function listDeviceManufacturer(filters?: Record<string, unknown>) {
  return {
    type: Types.LIST_MANUFACTURER,
    payload: { filters: sanitizeFilter(filters) },
  };
}
export function listDeviceInventory() {
  return {
    type: Types.LIST_INVENTORY,
    payload: {},
  };
}
export function listDeviceModel(filters?: Record<string, unknown>) {
  return {
    type: Types.LIST_MODEL,
    payload: { filters: sanitizeFilter(filters) },
  };
}
export function removeDevice(idDevice: ID) {
  return {
    type: Types.ACTION_REMOVE,
    payload: idDevice,
  };
}

export function blockDevice(idDevice: ID) {
  return {
    type: Types.ACTION_BLOCK,
    payload: idDevice,
  };
}

export function unblockDevice(idDevice: ID) {
  return {
    type: Types.ACTION_UNBLOCK,
    payload: idDevice,
  };
}

export function newPasswordDevice(idDevice: ID) {
  return {
    type: Types.ACTION_NEWPASSWORD,
    payload: idDevice,
  };
}

export function listPasswordHistoric(idDevice: ID) {
  return {
    type: Types.ACTION_LISTPASSWORDS,
    payload: idDevice,
  };
}
