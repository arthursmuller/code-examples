import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { ListMetadata, ListQueryParameters } from '../../types/generic_list';
import { ReportGpsType } from '../../types/reportGps';


// Action Types
export const Types = {
  LIST: 'gps/LIST'
};

// Reducer
interface RepostGpsState {
  reportGps: ReportGpsType[];
  metadata: ListMetadata;
  errors: Error;
  toaster: boolean;
}

const initialState: RepostGpsState = {
  reportGps: [],
  metadata: { sortingProperty: 'id' },
  errors: null,
  toaster: false,
};

export const reportGpsSlice = createSlice({
  name: 'reportGps',
  initialState,
  reducers: {
    reportGpsError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    reportGpsToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    reportGpsMetadata: (state, action: PayloadAction<ListMetadata>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    repostGpsListSuccess: (state, action: PayloadAction<ReportGpsType[]>) => {
      state.reportGps = action.payload;
      state.errors = initialState.errors;
    },
  }
});

export default reportGpsSlice.reducer;

// Action Creators
export const {
  reportGpsError,
  reportGpsToaster,
  reportGpsMetadata,
  repostGpsListSuccess,
} = reportGpsSlice.actions;

export function listReportGps(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}
