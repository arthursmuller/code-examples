import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { ReportGpsType } from '../../types/reportGps';
import {
  Types,
  reportGpsError,
  reportGpsMetadata,
  repostGpsListSuccess,
} from '../reportGps';
import { safe, api } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(reportGpsMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<ReportGpsType> = yield call(
      api,
      'reportGpsList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(repostGpsListSuccess(items)),
      put(reportGpsMetadata(pagination)),
    ]);
  } catch (e) {
    yield put(reportGpsError(e.body));
  }
}

export default function* reportGpsSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
}