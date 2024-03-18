import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { ReportSiteDateType, ReportSiteUrlType } from '../../types/reports';

// Action Types

export const Types = {
  SITES_DATE_FIRST: 'reports/SitesListFirst',
  SITES_DATE: 'reports/SitesDate',
  SITES_LIST: 'reports/SitesList',
};

interface SitesDateState {
  sitesDate: ReportSiteDateType[];
  sitesUrl: ReportSiteUrlType[];
  metadataDate: ListMetadata;
  metadataUrl: ListMetadata;
  errors: Error;
}

const initialState: SitesDateState = {
  sitesDate: [],
  sitesUrl: [],
  metadataDate: newMetadata({page: 1}),
  metadataUrl: newMetadata({sortingProperty: 'accessedAt', sortingDirection: 'DESC'}),
  errors: null,
};

export const reportsSlice = createSlice({
  name: 'reports',
  initialState,
  reducers: {
    reportsError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
    },
    reportsSitesDatePagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadataDate = { ...state.metadataDate, ...action.payload };
    },
    reportsSitesDateListSuccess: (state, action: PayloadAction<ReportSiteDateType[]>) => {
      state.sitesDate = action.payload;
      state.errors = initialState.errors;
    },
    reportsSitesDateListClean: (state) => {
      state.sitesDate = initialState.sitesDate;
      state.metadataDate = initialState.metadataDate;
    },
    reportsSitesUrlPagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadataUrl = { ...state.metadataUrl, ...action.payload };
    },
    reportsSitesUrlListSuccess: (state, action: PayloadAction<ReportSiteUrlType[]>) => {
      state.sitesUrl = action.payload;
      state.errors = initialState.errors;
    },
  },
});

export default reportsSlice.reducer;

// Action Creators

export const {
  reportsError,
  reportsSitesDatePagination,
  reportsSitesDateListSuccess,
  reportsSitesDateListClean,
  reportsSitesUrlPagination,
  reportsSitesUrlListSuccess,
} = reportsSlice.actions;

export function listDataFirst(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.SITES_DATE_FIRST,
    payload: {
      queryParameters: {...queryParameters, page: 1},
      filters: sanitizeFilter(filters)
    },
  };
}

export function listDataMore(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.SITES_DATE,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listSites(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.SITES_LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}
