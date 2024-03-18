import { takeLatest, call, put } from 'redux-saga/effects';

import {
  Types,
  authLoginSuccess,
  authLoginError,
  authRecoverySuccess,
  authRecoveryError,
  authLogout,
  authLogoutError,
  AuthState,
} from '../auth';
import { history } from '../history';
import { api, safe } from './util';

function* handleLogin(action) {
  try {
    const data: Partial<AuthState> = yield call(api, 'login', action.payload);

    yield put(
      authLoginSuccess({
        ...data,
        user: {
          ...data.user,
          company: {
            ...data.user.company,
            name: data.user.company.corporateName,
          },
        },
      })
    );
    yield call(forwardTo, '/dashboard');
  } catch (e) {
    yield put(authLoginError(e.body));
  }
}

function* handleLogout() {
  try {
    yield put(authLogout());
    yield call(api, 'logout');
    yield call(forwardTo, '/login');
  } catch (e) {
    yield call(forwardTo, '/login');
    yield put(authLogoutError(e));
  }
}

function* handleRecover(action) {
  try {
    // TODO - implement
    const data = { success: true, email: action.payload.email };
    yield put(authRecoverySuccess(data.email));
  } catch (e) {
    yield put(authRecoveryError(e.body));
  }
}

function forwardTo(location) {
  history.push(location);
}

export default function* sessionSaga() {
  yield takeLatest(Types.LOGIN, safe(handleLogin));
  yield takeLatest(Types.RECOVER_PASSWORD, safe(handleRecover));
  yield takeLatest(Types.LOGOUT, safe(handleLogout));
}
