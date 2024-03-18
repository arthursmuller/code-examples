import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { RootState } from '..';

import { UserAdminType } from '../../types/userAdmin';

// Action Types

export const Types = {
  LOGIN: 'auth/LOGIN',
  CLEAR: 'auth/CLEAR',
  LOGOUT: 'auth/LOGOUT',
  RECOVER_PASSWORD: 'auth/RECOVER_PASSWORD',
  RECOVER_PASSWORD_SUCCESS: 'auth/RECOVER_PASSWORD_SUCCESS',
  RECOVER_PASSWORD_ERROR: 'auth/RECOVER_PASSWORD_ERROR',
};

// Reducer

export interface AuthState {
  isLogged: boolean;
  error: boolean;
  errors: Error;
  accessToken: string;
  user: UserAdminType;
  recovery: Record<string, unknown>;
}

const initialState: AuthState = {
  isLogged: false,
  error: false,
  errors: undefined,
  accessToken: null,
  user: undefined,
  recovery: {},
};

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    authLoginSuccess: (state, action: PayloadAction<Partial<AuthState>>) => {
      state.isLogged = true;
      state.accessToken = action.payload.accessToken;
      state.user = action.payload.user;
      state.error = false;
      state.errors = initialState.errors;
    },
    authLoginError: (state, action: PayloadAction<Error>) => {
      state.error = true;
      state.errors = action.payload;
      state.isLogged = false;
    },
    authLogout: (state) => {
      state.isLogged = initialState.isLogged;
      state.error = initialState.error;
      state.errors = initialState.errors;
      state.accessToken = initialState.accessToken;
      state.user = initialState.user;
    },
    authLogoutError: (state, action: PayloadAction<Error>) => {
      state.error = true;
      state.errors = action.payload;
      state.isLogged = false;
    },
    authClear: (state) => {
      state.isLogged = initialState.isLogged;
      state.error = initialState.error;
      state.errors = initialState.errors;
      state.accessToken = initialState.accessToken;
      state.user = initialState.user;
      state.recovery = initialState.recovery;
    },
    authRecoverySuccess: (state, action: PayloadAction<Partial<string>>) => {
      state.recovery = {
        email: action.payload,
      };
    },
    authRecoveryError: (state, action: PayloadAction<Error>) => {
      state.recovery = {
        error: action.payload,
      };
    },
  },
});

export default authSlice.reducer;

// Action Creators

export const {
  authLoginSuccess,
  authLoginError,
  authLogout,
  authLogoutError,
  authClear,
  authRecoverySuccess,
  authRecoveryError,
} = authSlice.actions;

export const getToken = (state: RootState) => state.auth.accessToken;

export function login(data) {
  return {
    type: Types.LOGIN,
    payload: data,
  };
}

export function clearLogin() {
  return {
    type: Types.CLEAR,
  };
}

export function logout() {
  return {
    type: Types.LOGOUT,
  };
}

export function recover(data) {
  return {
    type: Types.RECOVER_PASSWORD,
    payload: data,
  };
}

export function recoverSuccess(data) {
  return {
    type: Types.RECOVER_PASSWORD_SUCCESS,
    payload: data,
  };
}

export function recoverError(errors) {
  return {
    type: Types.RECOVER_PASSWORD_ERROR,
    payload: errors,
  };
}
