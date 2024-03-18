import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put } from 'redux-saga/effects';

import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { GeolocationType, LocationType } from '../../types/locations';
import {
  locationError,
  locationListSuccess,
  locationPagination,
  locationGeolocationListSuccess,
  Types,
} from '../location';
import { api, safe } from './util';

function* handleListLast({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(locationPagination(queryParameters));
    const { items, ...pagination }: ListPayload<LocationType> = yield call(
      api,
      'listLocationLast',
      { ...queryParameters, ...filters }
    );
    yield put(locationListSuccess(items));
    yield put(locationPagination(pagination));
  } catch (e) {
    yield put(locationError(e.body));
  }
}

function* handleListHistoric({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(locationPagination(queryParameters));
    const { items, ...pagination }: ListPayload<LocationType> = yield call(
      api,
      'listLocationHistoric',
      { ...queryParameters, ...filters }
    );
    yield put(locationListSuccess(items));
    yield put(locationPagination(pagination));
  } catch (e) {
    yield put(locationError(e.body));
  }
}

function* handleListGeolocation({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const list: GeolocationType[] = yield call(
      api,
      'listLocationGeolocation',
      { ...queryParameters, ...filters }
    );
    yield put(locationGeolocationListSuccess(list));
  } catch (e) {
    yield put(locationError(e.body));
  }
}

export default function* locationSaga() {
  yield takeLatest(Types.LIST_LAST, safe(handleListLast));
  yield takeLatest(Types.LIST_HISTORIC, safe(handleListHistoric));
  yield takeLatest(Types.LIST_GEOLOCALIZATION, safe(handleListGeolocation));
}

