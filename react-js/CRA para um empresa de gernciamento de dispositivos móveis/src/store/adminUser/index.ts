import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import { ListMetadata, ListQueryParameters } from '../../types/generic_list';
import { UserAdminType } from '../../types/userAdmin';

// Action Types

export const Types = {
  LIST: 'adminUser/LIST',
  GET: 'adminUser/GET',
  CREATE: 'adminUser/CREATE',
  EDIT: 'adminUser/EDIT',
  TOASTER: 'adminUser/TOASTER',
  TOASTER_ADMIN: 'adminUser/TOASTER_ADMIN',
  LIST_PERMISSIONS: ' adminUser/PERMISSIONS',
};

// Reducer

interface UserAdminState {
  adminUsers: UserAdminType[];
  adminUser: UserAdminType;
  metadata: ListMetadata;
  errors: Error;
  showToaster: boolean;
}

const initialState: UserAdminState = {
  adminUsers: [],
  adminUser: {},
  metadata: newMetadata(),
  errors: null,
  showToaster: false,
};

export const adminUserSlice = createSlice({
  name: 'adminUsers',
  initialState,
  reducers: {
    adminUserError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.showToaster = true;
    },
    adminUserToaster: (state, action: PayloadAction<boolean>) => {
      state.showToaster = action.payload;
    },
    adminUserMetadata: (state, action: PayloadAction<ListMetadata>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    adminUserSelected: (
      state,
      action: PayloadAction<Partial<UserAdminType>>
    ) => {
      state.adminUser = { ...state.adminUser, ...action.payload };
    },
    adminUserSelectedClear: (state) => {
      state.adminUser = initialState.adminUser;
    },
    adminUserListSuccess: (state, action: PayloadAction<UserAdminType[]>) => {
      state.adminUsers = action.payload;
    },
    adminUserGetSuccess: (state, action: PayloadAction<UserAdminType>) => {
      state.adminUser = action.payload;
      state.errors = initialState.errors;
    },
    adminUserCreateSuccess: (state, action: PayloadAction<UserAdminType>) => {
      state.adminUser = action.payload;
      state.errors = initialState.errors;
      state.showToaster = true;
    },
    adminUserEditSuccess: (state, action: PayloadAction<UserAdminType>) => {
      state.adminUser = action.payload;
      state.errors = initialState.errors;
      state.showToaster = true;
    },
  },
});

export default adminUserSlice.reducer;

// Action Creators

export const {
  adminUserError,
  adminUserToaster,
  adminUserMetadata,
  adminUserSelected,
  adminUserSelectedClear,
  adminUserListSuccess,
  adminUserGetSuccess,
  adminUserCreateSuccess,
  adminUserEditSuccess,
} = adminUserSlice.actions;

export function showToaster(data) {
  adminUserToaster(data);
}

export function listAdminUsers(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function getAdminUsers(id: UserAdminState['adminUser']['id']) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createAdminUser(data) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function editAdminUser(data) {
  return {
    type: Types.EDIT,
    payload: data,
  };
}

export function closeAdminUserToaster() {
  return adminUserToaster(false);
}
