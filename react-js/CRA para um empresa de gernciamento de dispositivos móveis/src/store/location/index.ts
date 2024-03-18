// Action Types

import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { LocationType, GeolocationType } from '../../types/locations';

export const Types = {
  LIST_LAST: 'location/LIST_LAST',
  LIST_HISTORIC: 'location/LIST_HISTORIC',
  LIST_GEOLOCALIZATION: 'geolocation/LIST_GEOLOCALIZATION',
};

// Reducer

interface LocationState {
  locations: LocationType[];
  metadata: ListMetadata;
  errors: Error;
  toaster: boolean;
  geolocations: GeolocationType[];
}

const initialState: LocationState = {
  locations: [],
  metadata: newMetadata({
    sortingProperty: 'createdAt',
    sortingDirection: 'ASC',
  }),
  errors: null,
  toaster: false,
  geolocations: [],
};

export const locationsSlice = createSlice({
  name: 'locations',
  initialState,
  reducers: {
    locationError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    locationToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    locationPagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    locationListSuccess: (state, action: PayloadAction<LocationType[]>) => {
      state.locations = action.payload;
      state.errors = initialState.errors;
    },
    locationGeolocationListSuccess: (state, action: PayloadAction<GeolocationType[]>) => {
      state.geolocations = action.payload;
      state.errors = initialState.errors;
    },
  },
});

export default locationsSlice.reducer;

// Action Creators

export const {
  locationError,
  locationToaster,
  locationPagination,
  locationListSuccess,
  locationGeolocationListSuccess,
} = locationsSlice.actions;

export function listLastLocation(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_LAST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listlocationHistoric(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_HISTORIC,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function listGeolocation(
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_GEOLOCALIZATION,
    payload: { filters: sanitizeFilter(filters) },
  };
}
