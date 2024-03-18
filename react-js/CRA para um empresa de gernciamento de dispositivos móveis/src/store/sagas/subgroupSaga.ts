import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, select, all } from 'redux-saga/effects';

import { DeviceUserType } from '../../types/deviceUser';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { SubgroupType } from '../../types/subgroups';
import {
  Types,
  subgroupCreateSuccess,
  subgroupErrors,
  subgroupListSuccess,
  subgroupGetSuccess,
  subgroupEditSuccess,
  subgroupPagination,
  listSubgroups,
  subgroupDevicesListSuccess,
  subgroupDevicesPagination,
  subgroupListFilterSuccess,
} from '../subgroup';
import { api, safe } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(subgroupPagination(queryParameters));
    const { items, ...pagination }: ListPayload<SubgroupType> = yield call(
      api,
      'subgroupList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(subgroupListSuccess(items)),
      put(subgroupPagination(pagination)),
    ]);
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* handleListFilter({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const subgroups: SubgroupType[] = yield call(
      api,
      'subgroupListFilter',
      {
        ...queryParameters,
        ...filters,
      }
    );
    yield put(subgroupListFilterSuccess(subgroups));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* handleDelete({ payload }: PayloadAction<SubgroupType['id']>) {
  try {
    yield call(api, 'subgroupDelete', payload);

    const metadata = yield select((state) => state.subgroup.metadata);
    yield put(listSubgroups(metadata));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* handleGet({ payload }: PayloadAction<SubgroupType['id']>) {
  try {
    const data: SubgroupType = yield call(api, 'subgroupGet', payload);
    yield put(subgroupGetSuccess(data));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* handleCreation({ payload }: PayloadAction<SubgroupType>) {
  try {
    const data: SubgroupType = yield call(api, 'subgroupCreation', payload);
    yield put(subgroupCreateSuccess(data));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* handleEdit({ payload }: PayloadAction<SubgroupType>) {
  try {
    const data: SubgroupType = yield call(
      api,
      'subgroupUpdate',
      payload.id,
      payload
    );
    yield put(subgroupEditSuccess(data));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

function* listLinkedUser({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    interface DeviceUserApiType extends DeviceUserType {
      device: { phoneNumber: string };
    }

    const { items, ...pagination }: ListPayload<DeviceUserApiType> = yield call(
      api,
      'subgroupLinkedDeviceList',
      { ...queryParameters, ...filters }
    );
    yield put(
      subgroupDevicesListSuccess(
        items.map((item) => ({
          ...item,
          phoneNumber: item.device?.phoneNumber,
        }))
      )
    );
    yield put(subgroupDevicesPagination(pagination));
  } catch (e) {
    yield put(subgroupErrors(e.body));
  }
}

export default function* subgroupSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.DELETE, safe(handleDelete));
  yield takeLatest(Types.LIST_FILTER, safe(handleListFilter));
  yield takeLatest(Types.CREATE, safe(handleCreation));
  yield takeLatest(Types.EDIT, safe(handleEdit));
  yield takeLatest(Types.LIST_LINKED_USER, safe(listLinkedUser));
}
