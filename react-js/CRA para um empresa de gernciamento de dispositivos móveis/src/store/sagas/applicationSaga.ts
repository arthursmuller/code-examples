import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import {
  ApplicationConsumptionHistoryType,
  ApplicationDeviceUserType,
  ApplicationType,
} from '../../types/application';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import {
  Types,
  createApplicationSuccess,
  sendApplicationSuccess,
  destroyApplicationSuccess,
  applicationPagination,
  applicationListSuccess,
  applicationError,
  applicationDeviceUsersMetadata,
  applicationDeviceUsersListSuccess,
  applicationConsumptionHistoryListSuccess,
} from '../application';
import { history } from '../history';
import { api, safe } from './util';

function* handleCreation(action) {
  const data = {
    _id: '2133',
    url: action.payload.url,
    package: action.payload.package,
  };
  yield put(createApplicationSuccess(data));
  history.push('/android/manage-application');
}

function* send(action) {
  yield put(sendApplicationSuccess(action.payload));
}

function* destroy(action) {
  yield put(destroyApplicationSuccess(action.payload));
}

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(applicationPagination(queryParameters));
    const { items, ...pagination }: ListPayload<ApplicationType> = yield call(
      api,
      'applicationsList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(applicationListSuccess(items)),
      put(applicationPagination(pagination)),
    ]);
  } catch (e) {
    yield put(applicationError(e.body));
  }
}

function* handleListApplicationDeviceUsers({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(applicationDeviceUsersMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<ApplicationDeviceUserType> =
      yield call(api, 'applicationDeviceUserList', {
        ...queryParameters,
        ...filters,
      });
    yield all([
      put(applicationDeviceUsersListSuccess(items)),
      put(applicationDeviceUsersMetadata(pagination)),
    ]);
  } catch (e) {
    yield put(applicationError(e.body));
  }
}

function* handleListApplicationConsumptionHistory({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const items: ApplicationConsumptionHistoryType[] = yield call(
      api,
      'applicationConsumptionHistoryList',
      {
        ...queryParameters,
        ...filters,
      }
    );
    yield all([put(applicationConsumptionHistoryListSuccess(items))]);
  } catch (e) {
    yield put(applicationError(e.body));
  }
}

export default function* applicationSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.CREATE_APPLICATION, safe(handleCreation));
  yield takeLatest(Types.SEND_APPLICATION, safe(send));
  yield takeLatest(Types.DESTROY_APPLICATION, safe(destroy));
  yield takeLatest(
    Types.LIST_APPLICATION_DEVICE_USERS,
    safe(handleListApplicationDeviceUsers)
  );
  yield takeLatest(
    Types.LIST_APPLICATION_CONSUMPTION_HISTORY,
    safe(handleListApplicationConsumptionHistory)
  );
}
