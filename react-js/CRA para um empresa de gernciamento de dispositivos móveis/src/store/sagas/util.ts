import { select, apply, call } from 'redux-saga/effects';

import Api, { ApiError } from './api';

export function* api(method: keyof ReturnType<typeof Api>, ...args: unknown[]) {
  try {
    const apiInstance = Api(yield select());
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    return yield apply(apiInstance, method, args as any);
  } catch (err) {
    throw new ApiError(err.response.status, err.response.data);
  }
}

export const safe = (saga, ...args) =>
  function* safeLogin(action) {
    try {
      yield call(saga, ...args, action);
    } catch (err) {
      throw new Error(err);
    }
};
