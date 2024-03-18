import { PayloadAction } from '@reduxjs/toolkit';
import {
  takeLatest,
  call,
  put,
  ForkEffect,
  all,
  select,
} from 'redux-saga/effects';

import { DeviceUserType } from '../../types/deviceUser';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { GroupType } from '../../types/group';
import {
  Types,
  listGroups,
  groupError,
  groupPagination,
  groupDevicesPagination,
  groupCreateSuccess,
  groupListSuccess,
  groupGetSuccess,
  groupUpdateSuccess,
  groupDevicesListSuccess,
  groupListFilterSuccess,
} from '../group';
import { api, safe } from './util';

function* handleCreation({ payload }: PayloadAction<GroupType>) {
  try {
    const data: GroupType = yield call(api, 'groupCreation', payload);
    yield put(groupCreateSuccess(data));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(groupPagination(queryParameters));
    const { items, ...pagination }: ListPayload<GroupType> = yield call(
      api,
      'groupList',
      { ...queryParameters, ...filters }
    );
    yield all([put(groupListSuccess(items)), put(groupPagination(pagination))]);
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* handleListFilter({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const groups: GroupType[] = yield call(api, 'groupListFilter', {
      ...queryParameters,
      ...filters,
    });
    yield put(groupListFilterSuccess(groups));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* getGroup({ payload }: PayloadAction<number>) {
  try {
    const data: GroupType = yield call(api, 'groupGet', payload);
    yield put(groupGetSuccess(data));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* updateGroup({ payload }: PayloadAction<GroupType>) {
  try {
    const data: GroupType = yield call(api, 'groupUpdate', payload.id, payload);
    yield put(groupUpdateSuccess(data));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* handleDelete({ payload: id }: PayloadAction<GroupType>) {
  try {
    yield call(api, 'groupDelete', id);

    const metadata = yield select((state) => state.group.metadata);
    yield put(listGroups(metadata));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

function* listLinkedUser({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    interface DeviceUserApiType extends DeviceUserType {
      device: { phoneNumber: string };
    }

    const ret = yield call(api, 'groupLinkedDeviceList', {
      ...queryParameters,
      ...filters,
    });
    const { items, ...pagination }: ListPayload<DeviceUserApiType> = ret;
    yield put(
      groupDevicesListSuccess(
        items.map((item) => ({ ...item, phoneNumber: item.device.phoneNumber }))
      )
    );
    yield put(groupDevicesPagination(pagination));
  } catch (e) {
    yield put(groupError(e.body));
  }
}

export default function* groupSaga(): Generator<
  ForkEffect<never>,
  void,
  unknown
> {
  yield takeLatest(Types.CREATE, safe(handleCreation));
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.LIST_FILTER, safe(handleListFilter));
  yield takeLatest(Types.GET, safe(getGroup));
  yield takeLatest(Types.UPDATE, safe(updateGroup));
  yield takeLatest(Types.DELETE, safe(handleDelete));
  yield takeLatest(Types.LIST_LINKED_USER, safe(listLinkedUser));
}
