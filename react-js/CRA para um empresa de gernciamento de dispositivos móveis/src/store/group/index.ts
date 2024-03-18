import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import { DeviceUserType } from '../../types/deviceUser';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { GroupType } from '../../types/group';
import { ID } from '../../types/util';

// Action Types

export const Types = {
  CREATE: 'group/CREATE',
  TOASTER: 'group/TOASTER',
  LIST: 'group/LIST',
  LIST_FILTER: 'group/LIST_FILTER',
  GET: 'group/GET',
  UPDATE: 'group/UPDATE',
  LIST_LINKED_USER: 'group/LIST_LINKED_USER',
  DELETE: 'group/DELETE',
};

// Reducer

interface GroupsState {
  groups: GroupType[];
  group: GroupType;
  metadata: ListMetadata;
  errors: Error;
  groupsToFilter: GroupType[];
  toaster: boolean;
  linkedDevices: DeviceUserType[];
  metadataLinkedDevices: ListMetadata;
}

const initialState: GroupsState = {
  groups: [],
  group: {},
  metadata: newMetadata(),
  errors: null,
  groupsToFilter: [],
  toaster: false,
  linkedDevices: [],
  metadataLinkedDevices: newMetadata({pageSize: 10 }),
};

export const groupsSlice = createSlice({
  name: 'groups',
  initialState,
  reducers: {
    groupError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    groupToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    groupPagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    groupDevicesPagination: (
      state,
      action: PayloadAction<PaginationPayload>
    ) => {
      state.metadataLinkedDevices = { ...state.metadata, ...action.payload };
    },
    groupSelected: (state, action: PayloadAction<Partial<GroupType>>) => {
      state.group = { ...state.group, ...action.payload };
    },
    groupSelectedClear: (state) => {
      state.group = initialState.group;
    },
    groupListSuccess: (state, action: PayloadAction<GroupType[]>) => {
      state.groups = action.payload;
      state.errors = initialState.errors;
    },
    groupCreateSuccess: (state, action: PayloadAction<GroupType>) => {
      state.group = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    groupGetSuccess: (state, action: PayloadAction<GroupType>) => {
      state.group = action.payload;
      state.errors = initialState.errors;
    },
    groupUpdateSuccess: (state, action: PayloadAction<GroupType>) => {
      state.group = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    groupListFilterSuccess: (state, action: PayloadAction<GroupType[]>) => {
      state.groupsToFilter = action.payload;
    },
    groupDevicesListSuccess: (
      state,
      action: PayloadAction<DeviceUserType[]>
    ) => {
      state.linkedDevices = action.payload;
    },
    groupDevicesClean: (
      state
    ) => {
      state.linkedDevices = initialState.linkedDevices;
    },
  },
});

export default groupsSlice.reducer;

// Action Creators

export const {
  groupError,
  groupToaster,
  groupPagination,
  groupDevicesPagination,
  groupSelected,
  groupSelectedClear,
  groupListSuccess,
  groupCreateSuccess,
  groupGetSuccess,
  groupUpdateSuccess,
  groupListFilterSuccess,
  groupDevicesListSuccess,
  groupDevicesClean,
} = groupsSlice.actions;

export function listGroups(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listGroupsToFilter(
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_FILTER,
    payload: { filters: sanitizeFilter(filters) },
  };
}

export function getGroup(id: number) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createGroup(data: GroupType) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function updateGroup(data: GroupType) {
  return {
    type: Types.UPDATE,
    payload: data,
  };
}

export function deleteGroup(id: GroupType['id']) {
  return {
    type: Types.DELETE,
    payload: id,
  };
}

export function listLinkedDevice(
  queryParameters: ListQueryParameters,
  groupId?: ID,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_LINKED_USER,
    payload: {
      queryParameters,
      filters: sanitizeFilter({ ...filters, groupId }),
    },
  };
}
