import { takeLatest, call, put, select, apply } from 'redux-saga/effects';

import {
  Types,
  groupCreationSuccess,
  groupCreationError,
  generalUpdateSuccess,
  generalUpdateError,
  subgroupCreationSuccess,
  subgroupCreationError,
  userCreationSuccess,
  userCreationError,
} from '../consumptionProfile';
import { history } from '../history';

function* handleCreation(action) {
  try {
    const data = {
      id: '12312313123',
      group: action.payload.group,
      data_qtd: action.payload.data_qtd,
      roaming_data_qtd: action.payload.roaming_data_qtd,
      sms_qtd: action.payload.sms_qtd,
      roaming_sms_qtd: action.payload.roaming_sms_qtd,
    };
    yield put(groupCreationSuccess(data));
    history.push('/consumption-profile');
  } catch (e) {
    yield put(groupCreationError(e.body));
  }
}

function* handleGeneralUpdate(action) {
  try {
    yield put(generalUpdateSuccess({ ...action.payload, id: 1 }));
    history.push('/consumption-profile');
  } catch (e) {
    yield put(generalUpdateError(e.body));
  }
}

function* handleSubgroupCreation(action) {
  try {
    const data = {
      id: '12312313123',
      group: action.payload.group,
      data_qtd: action.payload.data_qtd,
      roaming_data_qtd: action.payload.roaming_data_qtd,
      sms_qtd: action.payload.sms_qtd,
      roaming_sms_qtd: action.payload.roaming_sms_qtd,
    };
    yield put(subgroupCreationSuccess(data));
    history.push('/consumption-profile');
  } catch (e) {
    yield put(subgroupCreationError(e.body));
  }
}

function* handleUserCreation(action) {
  try {
    const data = {
      id: '12312313123',
      user: action.payload.group,
      data_qtd: action.payload.data_qtd,
      roaming_data_qtd: action.payload.roaming_data_qtd,
      sms_qtd: action.payload.sms_qtd,
      roaming_sms_qtd: action.payload.roaming_sms_qtd,
    };
    yield put(userCreationSuccess(data));
    history.push('/consumption-profile');
  } catch (e) {
    yield put(userCreationError(e.body));
  }
}

const safe = (saga, ...args) =>
  function* safeLogin(action) {
    try {
      yield call(saga, ...args, action);
    } catch (err) {
      throw new Error(err);
    }
  };

export default function* consumptionProfileSaga() {
  yield takeLatest(Types.CREATE_GROUP, safe(handleCreation));
  yield takeLatest(Types.UPDATE_GENERAL, safe(handleGeneralUpdate));
  yield takeLatest(Types.CREATE_SUBGROUP, safe(handleSubgroupCreation));
  yield takeLatest(Types.CREATE_USER, safe(handleUserCreation));
}
