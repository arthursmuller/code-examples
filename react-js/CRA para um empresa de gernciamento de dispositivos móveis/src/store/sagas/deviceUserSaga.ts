import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import { DeviceUserType } from '../../types/deviceUser';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import {
  Types,
  deviceUserMetadata,
  deviceUserError,
  deviceUserListSuccess,
  deviceUserGetSuccess,
  deviceUserEditSuccess,
  deviceUserFilterListSuccess,
} from '../deviceUser';
import { safe, api } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(deviceUserMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<DeviceUserType> = yield call(
      api,
      'deviceUserList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(deviceUserListSuccess(items)),
      put(deviceUserMetadata(pagination)),
    ]);
  } catch (e) {
    yield put(deviceUserError(e.body));
  }
}

function* handleGet({ payload }: PayloadAction<DeviceUserType['id']>) {
  try {
    const data: DeviceUserType = yield call(api, 'deviceUserGet', payload);
    yield put(deviceUserGetSuccess(data));
  } catch (e) {
    yield put(deviceUserError(e.body));
  }
}

function* handleEdit({ payload }: PayloadAction<DeviceUserType>) {
  try {
    const data: DeviceUserType = yield call(
      api,
      'deviceUserEdit',
      payload.id,
      payload
    );
    yield put(deviceUserEditSuccess(data));
  } catch (e) {
    yield put(deviceUserError(e.body));
  }
}

function* handleListFilter({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  type DeviceUserSelectType = {
    id?: number;
    name?: string;
    device: {
      phoneNumber?: string;
    }
  }

  try {
    const items: DeviceUserSelectType[] = yield call(
      api,
      'deviceUserSelect',
      { ...queryParameters, ...filters }
    );
    yield put(deviceUserFilterListSuccess(items.map((item) => ({ ...item, phoneNumber: item.device.phoneNumber }))),);
  } catch (e) {
    yield put(deviceUserError(e.body));
  }
}

export default function* userSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.EDIT, safe(handleEdit));
  yield takeLatest(Types.LIST_FILTER, safe(handleListFilter));
}
