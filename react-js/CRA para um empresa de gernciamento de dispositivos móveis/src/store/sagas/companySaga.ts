import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put } from 'redux-saga/effects';

import { CompanyType, ConsumptionDataType, LicenseType } from '../../types/company';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import {
  Types,
  companyError,
  companyLicensesMetadata,
  companyLicensesListSuccess,
  companyGetSuccess,
  companyConsumptionsDataListSuccess,
  companyConsumptionsDataMetadata,
  companyConsumptionsSmsListSuccess,
  companyConsumptionsSmsMetadata,
} from '../company';
import { api, safe } from './util';

function* handleGet({ payload }: PayloadAction<CompanyType['id']>) {
  try {
    const data: CompanyType = yield call(api, 'companyGet', payload);
    yield put(companyGetSuccess({ ...data, name: data?.corporateName }));
  } catch (e) {
    yield put(companyError(e.body));
  }
}

function* handleListConsumptionData({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(companyConsumptionsDataMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<ConsumptionDataType> = yield call(
      api,
      'companyConsumptionData',
      { ...queryParameters, ...filters }
    );
    yield put(companyConsumptionsDataListSuccess(items));
    yield put(companyConsumptionsDataMetadata(pagination));
  } catch (e) {
    yield put(companyError(e.body));
  }
}

function* handleListConsumptionSms({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(companyConsumptionsSmsMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<ConsumptionDataType> = yield call(
      api,
      'companyConsumptionSms',
      { ...queryParameters, ...filters }
    );
    yield put(companyConsumptionsSmsListSuccess(items));
    yield put(companyConsumptionsSmsMetadata(pagination));
  } catch (e) {
    yield put(companyError(e.body));
  }
}

function* handleListLicense({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(companyLicensesMetadata(queryParameters));
    const { items, ...pagination }: ListPayload<LicenseType> = yield call(
      api,
      'companyLicenseList',
      { ...queryParameters, ...filters }
    );
    yield put(companyLicensesListSuccess(items));
    yield put(companyLicensesMetadata(pagination));
  } catch (e) {
    yield put(companyError(e.body));
  }
}

export default function* companySaga() {
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.CONSUMPTION_DATA, safe(handleListConsumptionData));
  yield takeLatest(Types.CONSUMPTION_SMS, safe(handleListConsumptionSms));
  yield takeLatest(Types.LICENSE_LIST, safe(handleListLicense));
}
