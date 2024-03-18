import { takeLatest, call, put, ForkEffect } from 'redux-saga/effects';

import { PlanType } from '../../types/plan';
import { planListSuccess, planError, Types } from '../plan';
import { api, safe } from './util';

function* listPlans() {
  try {
    const data: PlanType[] = yield call(api, 'planList');
    yield put(planListSuccess(data));
  } catch (e) {
    yield put(planError(e.body));
  }
}

export default function* groupSaga(): Generator<ForkEffect<never>, void, unknown> {
  yield takeLatest(Types.LIST, safe(listPlans));
}

