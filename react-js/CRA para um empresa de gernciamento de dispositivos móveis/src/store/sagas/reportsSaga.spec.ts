import { ReportSiteDateType, SiteType } from '../../types/reports';
import { convertSitesToReportSiteUrl } from './reportsSaga';

const anyItem = (date: string): SiteType => ({
  domain: 'url',
  accessedAt: new Date(date).toISOString(),
});

const reportsEmpty: ReportSiteDateType[] = [];

const reportsOneDateOneItemsSite: ReportSiteDateType[] = [
  {
    date: '2020-01-01',
    items: [anyItem('2020-01-01')],
  },
];

const reportsManyDatesManyItemsSites: ReportSiteDateType[] = [
  {
    date: '2020-01-01',
    items: [anyItem('2020-01-01'), anyItem('2020-01-01')],
  },
  {
    date: '2020-01-02',
    items: [
      anyItem('2020-01-02'),
      anyItem('2020-01-02'),
      anyItem('2020-01-02'),
    ],
  },
  {
    date: '2020-01-03',
    items: [
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
      anyItem('2020-01-03'),
    ],
  },
];

describe('function convertSitesToReportSiteUrl', () => {
  test('Should not modified array when item is null', () => {
    expect(convertSitesToReportSiteUrl(reportsEmpty, null)).toEqual([]);

    expect(
      convertSitesToReportSiteUrl(
        reportsManyDatesManyItemsSites,
        null
      )
    ).toEqual(reportsManyDatesManyItemsSites);
  });

  test('Should include item in final of array when date (yy/mm/dd) not exists in array', () => {
    expect(
      convertSitesToReportSiteUrl(reportsEmpty, anyItem('2020-01-02'))
    ).toEqual([
      {
        date: '2020-01-02',
        items: [anyItem('2020-01-02')],
      },
    ]);

    expect(
      convertSitesToReportSiteUrl(
        reportsOneDateOneItemsSite,
        anyItem('2020-01-02')
      )
    ).toEqual([
      ...reportsOneDateOneItemsSite,
      {
        date: '2020-01-02',
        items: [anyItem('2020-01-02')],
      },
    ]);

    expect(
      convertSitesToReportSiteUrl(
        reportsManyDatesManyItemsSites,
        anyItem('2020-01-04')
      )
    ).toEqual([
      ...reportsManyDatesManyItemsSites,
      {
        date: '2020-01-04',
        items: [anyItem('2020-01-04')],
      },
    ]);
  });

  test('Should include item in index of array when date (yy/mm/dd) exists in array', () => {
    expect(
      convertSitesToReportSiteUrl(reportsOneDateOneItemsSite, anyItem('2020-01-01'))
    ).toEqual([
      {
        date: '2020-01-01',
        items: [anyItem('2020-01-01'), anyItem('2020-01-01')],
      }
    ]);

    const reportReceived = convertSitesToReportSiteUrl(reportsManyDatesManyItemsSites, anyItem('2020-01-03'));
    const reportExpect = [...reportsManyDatesManyItemsSites];
    reportExpect[2].items.push(anyItem('2020-01-03'));
    expect(reportReceived).toEqual([...reportsManyDatesManyItemsSites]);
   });
});
