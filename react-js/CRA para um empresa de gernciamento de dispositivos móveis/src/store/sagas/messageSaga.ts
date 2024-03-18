import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, all } from 'redux-saga/effects';

import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import { MessageDetailsType, MessageType } from '../../types/message';
import { ID } from '../../types/util';
import {
  messageError,
  messageListSuccess,
  messagePagination,
  messagePaginationDetails,
  Types,
  messageCreateSuccess,
  messageDetailListSuccess,
  messageGetSuccess,
} from '../message';
import { api, safe } from './util';

function* handleList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(messagePagination(queryParameters));
    const { items, ...pagination }: ListPayload<MessageType> = yield call(
      api,
      'messageList',
      { ...queryParameters, ...filters }
    );
    yield all([
      put(messageListSuccess(items)),
      put(messagePagination(pagination)),
    ]);
  } catch (e) {
    yield put(messageError(e.body));
  }
}

function* handleListDetails({
  payload: { id, queryParameters, filters },
}: PayloadAction<QuerysWithFilters & { id: ID }>) {
  try {
    yield put(messagePaginationDetails(queryParameters));
    const { items, ...pagination }: ListPayload<MessageDetailsType> =
      yield call(api, 'messageListDetails', id, {
        ...queryParameters,
        ...filters,
      });
    yield all([
      put(messageDetailListSuccess(items)),
      put(messagePaginationDetails(pagination)),
    ]);
  } catch (e) {
    yield put(messageError(e.body));
  }
}

function* handleCreation({ payload }: PayloadAction<MessageType>) {
  try {
    const data: MessageType = yield call(api, 'messageCreate', payload);
    yield put(messageCreateSuccess(data));
  } catch (e) {
    yield put(messageError(e.body));
  }
}

function* handleGet({ payload }: PayloadAction<MessageType['id']>) {
  try {
    const data: MessageType = yield call(api, 'messageGet', payload);
    yield put(messageGetSuccess(data));
  } catch (e) {
    yield put(messageError(e.body));
  }
}

export default function* messageSaga() {
  yield takeLatest(Types.LIST, safe(handleList));
  yield takeLatest(Types.GET, safe(handleGet));
  yield takeLatest(Types.LIST_DETAILS, safe(handleListDetails));
  yield takeLatest(Types.CREATE, safe(handleCreation));
}
