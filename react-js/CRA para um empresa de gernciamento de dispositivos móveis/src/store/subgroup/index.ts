import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { DeviceUserType } from '../../types/deviceUser';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { SubgroupType } from '../../types/subgroups';
import { ID } from '../../types/util';

// Action Types

export const Types = {
  LIST: 'subgroup/LIST',
  GET: 'subgroup/GET',
  CREATE: 'subgroup/CREATE',
  EDIT: 'subgroup/EDIT',
  TOASTER: 'subgroup/TOASTER',
  DELETE: 'subgroup/DELETE',
  LIST_LINKED_USER: 'subgroup/LIST_LINKED_USER',
  LIST_FILTER: 'subgroup/LIST_FILTER',
};

// Reducer

interface SubgroupsState {
  subgroups: SubgroupType[];
  subgroup: SubgroupType;
  subgroupsToFilter: SubgroupType[];
  metadata: ListMetadata;
  linkedDevices: DeviceUserType[];
  metadataLinkedDevices: ListMetadata;
  errors: Error;
  toaster: boolean;
}

const initialState: SubgroupsState = {
  subgroups: [],
  subgroupsToFilter: [],
  subgroup: {},
  metadata: { sortingProperty: 'id' },
  linkedDevices: [],
  metadataLinkedDevices: { sortingProperty: 'id', pageSize: 10 },
  errors: null,
  toaster: false,
};

export const subgroupSlice = createSlice({
  name: 'subgroups',
  initialState,
  reducers: {
    subgroupErrors: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    subgroupToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    subgroupPagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    subgroupDevicesPagination: (
      state,
      action: PayloadAction<PaginationPayload>
    ) => {
      state.metadataLinkedDevices = { ...state.metadata, ...action.payload };
    },
    subgroupSelected: (state, action: PayloadAction<SubgroupType>) => {
      state.subgroup = { ...state.subgroup, ...action.payload };
    },
    subgroupSelectedClear: (state) => {
      state.subgroup = initialState.subgroup;
    },
    subgroupListSuccess: (state, action: PayloadAction<SubgroupType[]>) => {
      state.subgroups = action.payload;
    },
    subgroupGetSuccess: (state, action: PayloadAction<SubgroupType>) => {
      state.subgroup = action.payload;
    },
    subgroupCreateSuccess: (state, action: PayloadAction<SubgroupType>) => {
      state.subgroup = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    subgroupEditSuccess: (state, action: PayloadAction<SubgroupType>) => {
      state.subgroup = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    subgroupListFilterSuccess: (
      state,
      action: PayloadAction<SubgroupType[]>
    ) => {
      state.subgroupsToFilter = action.payload;
    },
    subgroupFilterClear: (state) => {
      state.subgroupsToFilter = initialState.subgroupsToFilter;
    },
    subgroupDevicesListSuccess: (
      state,
      action: PayloadAction<DeviceUserType[]>
    ) => {
      state.linkedDevices = action.payload;
    },
    subgroupDevicesClean: (
      state
    ) => {
      state.linkedDevices = initialState.linkedDevices;
    },
  },
});

export default subgroupSlice.reducer;

// Action Creators

export const {
  subgroupErrors,
  subgroupToaster,
  subgroupSelected,
  subgroupSelectedClear,
  subgroupListSuccess,
  subgroupGetSuccess,
  subgroupCreateSuccess,
  subgroupEditSuccess,
  subgroupListFilterSuccess,
  subgroupFilterClear,
  subgroupPagination,
  subgroupDevicesPagination,
  subgroupDevicesListSuccess,
  subgroupDevicesClean,
} = subgroupSlice.actions;

export function showToaster(data) {
  return subgroupToaster(data);
}

export function listSubgroups(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listSubgroupsToFilter(
  filters?: Record<string, unknown>,
  groupId?: ID
) {
  return {
    type: Types.LIST_FILTER,
    payload: { filters: sanitizeFilter({ ...filters, groupId }) },
  };
}

export function subgroupDelete(id: SubgroupType['id']) {
  return {
    type: Types.DELETE,
    payload: id,
  };
}

export function getSubgroup(id: SubgroupType['id']) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createSubgroup(data: SubgroupType) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function updateSubgroup(data: SubgroupType) {
  return {
    type: Types.EDIT,
    payload: data,
  };
}

export function listLinkedDevice(
  groupId: ID,
  subGroupId: ID,
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_LINKED_USER,
    payload: {
      queryParameters,
      filters: { ...sanitizeFilter({ ...filters, subGroupId }), groupId },
    },
  };
}
