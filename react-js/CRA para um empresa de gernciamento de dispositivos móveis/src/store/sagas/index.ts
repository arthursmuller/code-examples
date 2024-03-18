import { all } from 'redux-saga/effects';

import adminUserSaga from './adminUserSaga';
import applicationSaga from './applicationSaga';
import authSaga from './authSaga';
import companySaga from './companySaga';
import consumptionProfileSaga from './consumptionProfileSaga';
import deviceInfoSaga from './deviceInfoSaga';
import deviceSaga from './deviceSaga';
import deviceUserSaga from './deviceUserSaga';
import documentSaga from './documentSaga';
import groupSaga from './groupSaga';
import locationSaga from './locationSaga';
import messageSaga from './messageSaga';
import planSaga from './planSaga';
import qrcodeSaga from './qrcodeSaga';
import reportGpsSaga from './reportGpsSaga';
import reportsSaga from './reportsSaga';
import subgroupSaga from './subgroupSaga';

export default function* rootSaga() {
  yield all([
    adminUserSaga(),
    applicationSaga(),
    authSaga(),
    companySaga(),
    consumptionProfileSaga(),
    deviceInfoSaga(),
    deviceSaga(),
    deviceUserSaga(),
    documentSaga(),
    groupSaga(),
    locationSaga(),
    messageSaga(),
    planSaga(),
    qrcodeSaga(),
    reportGpsSaga(),
    reportsSaga(),
    subgroupSaga(),
  ]);
}
