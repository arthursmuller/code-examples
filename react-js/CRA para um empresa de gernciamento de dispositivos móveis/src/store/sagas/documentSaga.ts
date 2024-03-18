import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import { DocumentDetailsType, DocumentType } from '../../types/document';
import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { ID } from '../../types/util';
import {
  documentError,
  documentListSuccess,
  documentPagination,
  documentPaginationDetails,
  Types,
  documentCreateSuccess,
  documentDetailListSuccess,
  documentGetSuccess,
} from '../document';
import { api, safe } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(documentPagination(queryParameters));
    const { items, ...pagination }: ListPayload<DocumentType> = yield call(
      api,
      'documentList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(documentListSuccess(items)),
      put(documentPagination(pagination)),
    ]);
  } catch (e) {
    yield put(documentError(e.body));
  }
}

function* handleListDetails({
  payload: { id, queryParameters, filters },
}: PayloadAction<QuerysWithFilters & { id: ID }>) {
  try {
    yield put(documentPaginationDetails(queryParameters));
    const { items, ...pagination }: ListPayload<DocumentDetailsType> =
      yield call(api, 'documentListDetails', id, {
        ...queryParameters,
        ...filters,
      });
    yield all([
      put(documentDetailListSuccess(items)),
      put(documentPaginationDetails(pagination)),
    ]);
  } catch (e) {
    yield put(documentError(e.body));
  }
}

function* handleCreation({ payload }: PayloadAction<DocumentType>) {
  try {
    const data: DocumentType = yield call(api, 'documentCreate', payload);
    yield put(documentCreateSuccess(data));
  } catch (e) {
    yield put(documentError(e.body));
  }
}

function* handleGet({ payload }: PayloadAction<DocumentType['id']>) {
  try {
    const data: DocumentType = yield call(api, 'documentGet', payload);
    yield put(documentGetSuccess(data));
  } catch (e) {
    yield put(documentError(e.body));
  }
}

export default function* documentSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.LIST_DETAILS, safe(handleListDetails));
  yield takeLatest(Types.CREATE, safe(handleCreation));
}
