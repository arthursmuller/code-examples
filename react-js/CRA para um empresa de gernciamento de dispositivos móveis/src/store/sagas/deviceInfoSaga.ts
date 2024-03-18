import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put } from 'redux-saga/effects';

import {
  DeviceInfoBatteryType,
  DeviceInfoStorageType,
} from '../../types/deviceInfo';
import { QuerysWithFilters } from '../../types/generic_list';
import {
  Types,
  deviceInfoError,
  deviceInfoBatteryListSuccess,
  deviceInfoStorageListSuccess,
} from '../deviceInfo';
import { api, safe } from './util';

function* handleListBattery({
  payload: { idDeviceInfo, filters },
}: PayloadAction<QuerysWithFilters & { idDeviceInfo: number }>) {
  try {
    const infos: DeviceInfoBatteryType[] = yield call(
      api,
      'deviceBatteryList',
      idDeviceInfo,
      { ...filters }
    );
    yield put(deviceInfoBatteryListSuccess(infos));
  } catch (e) {
    yield put(deviceInfoError(e.body));
  }
}

function* handleListStorage({
  payload: { idDeviceInfo, filters },
}: PayloadAction<QuerysWithFilters & { idDeviceInfo: number }>) {
  try {
    const infos: DeviceInfoStorageType[] = yield call(
      api,
      'deviceStorageList',
      idDeviceInfo,
      { ...filters }
    );
    yield put(deviceInfoStorageListSuccess(infos));
  } catch (e) {
    yield put(deviceInfoError(e.body));
  }
}

export default function* locationSaga() {
  yield takeLatest(Types.LIST_BATTERY, safe(handleListBattery));
  yield takeLatest(Types.LIST_STORAGE, safe(handleListStorage));
}
