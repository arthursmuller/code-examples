import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { UserAdminType } from '../../types/userAdmin';
import {
  Types,
  adminUserError,
  adminUserMetadata,
  adminUserListSuccess,
  adminUserGetSuccess,
  adminUserCreateSuccess,
  adminUserEditSuccess,
} from '../adminUser';
import { api, safe } from './util';

function* HandleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(adminUserMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<UserAdminType> = yield call(
      api,
      'adminUserList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(adminUserListSuccess(items)),
      put(adminUserMetadata(pagination)),
    ]);
  } catch (e) {
    yield put(adminUserError(e.body));
  }
}


function* handleGet({ payload }: PayloadAction<UserAdminType['id']>) {
  try {
    const data: UserAdminType = yield call(api, 'adminUserGet', payload);
    yield put(adminUserGetSuccess(data));
  } catch (e) {
    yield put(adminUserError(e.body));
  }
}

function* handleCreation({ payload }: PayloadAction<UserAdminType>) {
  try {
    const data: UserAdminType = yield call(api, 'adminUserCreation', payload);
    yield put(adminUserCreateSuccess(data));
  } catch (e) {
    yield put(adminUserError(e.body));
  }
}

function* handleEdit({ payload }: PayloadAction<UserAdminType>) {
  try {
    const data: UserAdminType = yield call(
      api,
      'adminUserEdit',
      payload.id,
      payload
    );
    yield put(adminUserEditSuccess(data));
  } catch (e) {
    yield put(adminUserError(e.body));
  }
}

export default function* adminUserSaga() {
  yield takeLatest(Types.LIST, safe(HandleList));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.CREATE, safe(handleCreation));
  yield takeLatest(Types.EDIT, safe(handleEdit));
}
