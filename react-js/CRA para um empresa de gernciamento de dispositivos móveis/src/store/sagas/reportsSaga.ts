import { PayloadAction } from '@reduxjs/toolkit';
import { takeLatest, call, put, select } from 'redux-saga/effects';

import { RootState } from '..';

import { ListPayload, QuerysWithFilters } from '../../types/generic_list';
import {
  ReportSiteDateType,
  ReportSiteUrlType,
  SiteType,
} from '../../types/reports';
import {
  Types,
  reportsSitesDateListSuccess,
  reportsSitesDatePagination,
  reportsError,
  reportsSitesUrlPagination,
  reportsSitesUrlListSuccess,
  reportsSitesDateListClean,
} from '../reports';
import { api, safe } from './util';

export const convertSitesToReportSiteUrl = (
  reportsAcc: ReportSiteDateType[],
  site: SiteType
) => {
  if (!site) {
    return reportsAcc;
  }
  
  const dateOfSite = new Date(site.accessedAt).toISOString().split('T')[0];
  const index = reportsAcc.findIndex((report) => report.date === dateOfSite);

  if (index === -1) {
    return [...reportsAcc, { date: dateOfSite, items: [site] }];
  } else {
    const newReportsAcc = [...reportsAcc];
    newReportsAcc[index] = {
      date: newReportsAcc[index].date,
      items: [...newReportsAcc[index].items, site]
    };
    return newReportsAcc;
  }
};

function* sitesDateListFirst(action: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(reportsSitesDateListClean());
    yield call(sitesDateList, action);
  } catch (e) {
    yield put(reportsError(e));
  }
}

function* sitesDateList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    const { items, ...pagination }: ListPayload<SiteType> = yield call(
      api,
      'sitesDateList',
      { ...queryParameters, ...filters }
    );

    const sitesDatesList: ReportSiteDateType[] = yield select(
      (state: RootState) => state.reports.sitesDate
    );
    /*
      SiteType[] to ReportSiteDateType[]
    */
    const allReports: ReportSiteDateType[] = items.reduce(
      convertSitesToReportSiteUrl,
      sitesDatesList
    );

    yield put(reportsSitesDateListSuccess(allReports));
    yield put(reportsSitesDatePagination(pagination));
  } catch (e) {
    yield put(reportsError(e));
  }
}

function* sitesList({
  payload: { queryParameters, filters },
}: PayloadAction<QuerysWithFilters>) {
  try {
    yield put(reportsSitesUrlPagination(queryParameters));
    const { items, ...pagination }: ListPayload<ReportSiteUrlType> = yield call(
      api,
      'sitesList',
      { ...queryParameters, ...filters }
    );
    yield put(reportsSitesUrlListSuccess(items));
    yield put(reportsSitesUrlPagination(pagination));
  } catch (e) {
    yield put(reportsError(e.body));
  }
}

export default function* reportsSaga() {
  yield takeLatest(Types.SITES_DATE, safe(sitesDateList));
  yield takeLatest(Types.SITES_DATE_FIRST, safe(sitesDateListFirst));
  yield takeLatest(Types.SITES_LIST, safe(sitesList));
}
