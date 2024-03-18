import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import {
  DeviceType,
  InventoryItemType,
  ManufacturerType,
  PasswordsHistoricType,
  ModelType,
} from '../../types/device';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import {
  Types,
  deviceMetadata,
  deviceCreateSuccess,
  deviceListSuccess,
  deviceEditSuccess,
  deviceGetSuccess,
  deviceManufacturersListSuccess,
  deviceError,
  deviceInventoryListSuccess,
  deviceModelsListSuccess,
  deviceActionRemoveSuccess,
  deviceActionBlockSuccess,
  deviceActionUnblockSuccess,
  deviceActionNewPasswordSuccess,
  deviceActionListPasswordsSuccess,
} from '../device';
import { api, safe } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(deviceMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<DeviceType> = yield call(
      api,
      'deviceList',
      { ...queryParameters, ...filters }
    );
    yield all([put(deviceListSuccess(items)), put(deviceMetadata(pagination))]);
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleListManufacturer({
  payload: { filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const manufacturers: ManufacturerType[] = yield call(
      api,
      'deviceListManufacturer',
      {
        ...filters,
      }
    );
    yield put(deviceManufacturersListSuccess(manufacturers));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleListInventory({ payload: {} }) {
  try {
    const inventory: InventoryItemType[] = yield call(
      api,
      'deviceInventoryList'
    );
    yield put(deviceInventoryListSuccess(inventory));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleListModel({
  payload: { filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const models: ModelType[] = yield call(api, 'deviceListModel', {
      ...filters,
    });
    yield put(deviceModelsListSuccess(models.filter(model => !!model.model)));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleGet({ payload }: PayloadAction<DeviceType['id']>) {
  try {
    const data: DeviceType = yield call(api, 'deviceGet', payload);
    yield put(deviceGetSuccess(data));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleCreation({ payload }: PayloadAction<DeviceType>) {
  try {
    const data: DeviceType = yield call(api, 'deviceCreation', payload);
    yield put(deviceCreateSuccess(data));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleEdit({ payload }: PayloadAction<DeviceType>) {
  try {
    const data: DeviceType = yield call(api, 'deviceEdit', payload.id, payload);
    yield put(deviceEditSuccess(data));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleRemove({ payload }: PayloadAction<DeviceType>) {
  try {
    yield call(api, 'deviceActionRemove', payload.id, payload);
    yield put(deviceActionRemoveSuccess());
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleBlock({ payload }: PayloadAction<DeviceType>) {
  try {
    yield call(api, 'deviceActionBlock', payload.id, payload);
    yield put(deviceActionBlockSuccess());
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleUnblock({ payload }: PayloadAction<DeviceType>) {
  try {
    yield call(api, 'deviceActionUnblock', payload.id, payload);
    yield put(deviceActionUnblockSuccess());
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleNewPassword({ payload }: PayloadAction<DeviceType>) {
  try {
    yield call(api, 'deviceActionNewPassword', payload.id, payload);
    yield put(deviceActionNewPasswordSuccess());
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

function* handleListPasswords({ payload }: PayloadAction<DeviceType>) {
  try {
    const historic: PasswordsHistoricType[] = yield call(
      api,
      'deviceActionListPasswords',
      payload.id,
      payload
    );
    yield put(deviceActionListPasswordsSuccess(historic));
  } catch (e) {
    yield put(deviceError(e.body));
  }
}

export default function* deviceSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.LIST_MANUFACTURER, safe(handleListManufacturer));
  yield takeLatest(Types.LIST_INVENTORY, safe(handleListInventory));
  yield takeLatest(Types.LIST_MODEL, safe(handleListModel));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.CREATE, safe(handleCreation));
  yield takeLatest(Types.EDIT, safe(handleEdit));
  yield takeLatest(Types.ACTION_REMOVE, safe(handleRemove));
  yield takeLatest(Types.ACTION_BLOCK, safe(handleBlock));
  yield takeLatest(Types.ACTION_UNBLOCK, safe(handleUnblock));
  yield takeLatest(Types.ACTION_NEWPASSWORD, safe(handleNewPassword));
  yield takeLatest(Types.ACTION_LISTPASSWORDS, safe(handleListPasswords));
}
