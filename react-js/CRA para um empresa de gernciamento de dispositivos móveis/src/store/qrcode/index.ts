import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { QrCodeGenerateType } from '../../types/qrCode';

// Action Types

export const Types = {
  GENERATE: 'qrCode/GENERATE',
};

// Reducer

interface QrCodeState {
  form: QrCodeGenerateType;
  textToGenerate: string;
  errors: Error;
}

const initialState: QrCodeState = {
  form: {
    packageName: process.env.REACT_APP_DEVICE_OWNER_PACKAGE_NAME,
    url: process.env.REACT_APP_DEVICE_OWNER_URL,
    enableAllApps: true,
  },
  textToGenerate: '',
  errors: null,
};

export const qrCodeSlice = createSlice({
  name: 'qrCodes',
  initialState,
  reducers: {
    qrCodeError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
    },
    qrCodeGenerateSuccess: (
      state,
      action: PayloadAction<string>
    ) => {
      state.textToGenerate = action.payload;
    },
    getFormQrCode: (state, action: PayloadAction<Partial<QrCodeGenerateType>>) => {
      state.form = {... state.form, ...action.payload}
    },
    toggleCheckbox: (state) => {
      state.form.enableAllApps = !state.form.enableAllApps
    }
  },
});

export default qrCodeSlice.reducer;

// Action Creators

export const {
  qrCodeError,
  qrCodeGenerateSuccess,
  getFormQrCode,
  toggleCheckbox
} = qrCodeSlice.actions;

export function genereateQrCode(
  fields?: QrCodeGenerateType
) {
  return {
    type: Types.GENERATE,
    payload: { fields },
  };
}
