import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import { CompanyType, LicenseType, ConsumptionDataType } from '../../types/company';
import {
  ListMetadata,
  ListQueryParameters,
} from '../../types/generic_list';

// Action Types

export const Types = {
  TOASTER: 'company/TOASTER',
  GET: 'company/GET',
  CONSUMPTION_DATA: 'company/CONSUMPTION_DATA',
  CONSUMPTION_SMS: 'company/CONSUMPTION_SMS',
  LICENSE_LIST: 'company/LICENSE_LIST',
};

// Reducer

interface CompanyState {
  companies: CompanyType[];
  company: CompanyType;
  consumptionsData: ConsumptionDataType[];
  consumptionsDataMetadata: ListMetadata;
  consumptionsSms: ConsumptionDataType[];
  consumptionsSmsMetadata: ListMetadata;
  licenses: LicenseType[];
  licensesMetadata: ListMetadata;
  errors: Error;
  showToaster: boolean;
}

const initialState: CompanyState = {
  companies: [],
  company: null,
  consumptionsData: [],
  consumptionsDataMetadata: newMetadata({ sortingProperty: 'dateConsumption', sortingDirection: 'DESC'}),
  consumptionsSms: [],
  consumptionsSmsMetadata: newMetadata({ sortingProperty: 'sendDate', sortingDirection: 'DESC' }),
  licenses: [],
  licensesMetadata: newMetadata(),
  errors: null,
  showToaster: false,
};

const companySlice = createSlice({
  name: 'company',
  initialState,
  reducers: {
    companyError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
    },
    companyToaster: (state, action: PayloadAction<boolean>) => {
      state.showToaster = action.payload;
    },
    companyLicensesMetadata: (
      state,
      action: PayloadAction<ListMetadata>
    ) => {
      state.licensesMetadata = { ...state.licensesMetadata, ...action.payload };
    },
    companyListSuccess: (state, action: PayloadAction<CompanyType[]>) => {
      state.companies = action.payload;
      state.errors = initialState.errors;
    },
    companyGetSuccess: (state, action: PayloadAction<CompanyType>) => {
      state.company = action.payload;
      state.errors = initialState.errors;
    },
    companyLicensesListSuccess: (
      state,
      action: PayloadAction<LicenseType[]>
    ) => {
      state.licenses = action.payload;
      state.errors = initialState.errors;
    },
    companyConsumptionsDataListSuccess: (state, action: PayloadAction<ConsumptionDataType[]>) => {
      state.consumptionsData = action.payload;
      state.errors = initialState.errors;
    },
    companyConsumptionsSmsListSuccess: (state, action: PayloadAction<ConsumptionDataType[]>) => {
      state.consumptionsSms = action.payload;
      state.errors = initialState.errors;
    },
    companyConsumptionsDataMetadata: (
      state,
      action: PayloadAction<ListMetadata>
    ) => {
      state.consumptionsDataMetadata = { ...state.consumptionsDataMetadata, ...action.payload };
    },
    companyConsumptionsSmsMetadata: (
      state,
      action: PayloadAction<ListMetadata>
    ) => {
      state.consumptionsSmsMetadata = { ...state.consumptionsSmsMetadata, ...action.payload };
    },
  },
});

export default companySlice.reducer;

// Action Creators

export const {
  companyError,
  companyToaster,
  companyLicensesMetadata,
  companyListSuccess,
  companyGetSuccess,
  companyLicensesListSuccess,
  companyConsumptionsDataListSuccess,
  companyConsumptionsSmsListSuccess,
  companyConsumptionsDataMetadata,
  companyConsumptionsSmsMetadata,
} = companySlice.actions;

export function showToaster(data) {
  return companyToaster(data);
}

export function getCompany(idCompany: CompanyType['id']) {
  return {
    type: Types.GET,
    payload: idCompany,
  };
}

export function listCompanyLicenses(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LICENSE_LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listCompanyConsumptionData(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.CONSUMPTION_DATA,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listCompanyConsumptionSms(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.CONSUMPTION_SMS,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}