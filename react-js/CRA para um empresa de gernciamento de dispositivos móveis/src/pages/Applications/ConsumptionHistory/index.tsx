import { Box } from '@chakra-ui/react';
import { useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useParams } from 'react-router-dom';

import Card from '../../../components/Card';
import ApplicationHeader from '../../../components/pages/Applications/ApplicationHeader';
import PageTitle from '../../../components/PageTitle';
import Table from '../../../components/Table/Table';
import { newMetadata } from '../../../helper/metadata';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  applicationDeviceUserSet,
  applicationSet,
  listApplicationConsumptionHistory,
} from '../../../store/application';
import MonthlyClusterBarChart from './MonthlyClusterChart';

const ConsumptionHistory = () => {
  const dispatch = useAppDispatch();
  const { consumptionHistorys, deviceUser, filter } = useAppSelector(
    (state) => state.application
  );

  const intl = useIntl();
  const { deviceUserId, applicationName } =
    useParams<{ deviceUserId: string; applicationName: string }>();

  const data = deviceUser
    ? [
        {
          cells: [
            {
              field: 'group',
              value: deviceUser.group,
            },
            {
              field: 'subgroup',
              value: deviceUser.subgroup,
            },
            {
              field: 'device_user',
              value: deviceUser.user,
            },
            {
              field: 'phone',
              value: deviceUser.phoneNumber,
            },
            {
              field: 'package_name',
              value: deviceUser.packageName,
            },
            {
              field: 'consumption',
              value: deviceUser.consumption,
            },
          ],
        },
      ]
    : [];

  const columns = useSorting(
    [
      {
        accessor: 'group',
        canSort: true,
        header: intl.formatMessage({ id: 'company_consumption.column.group' }),
      },
      {
        accessor: 'subgroup',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.column.subgroup',
        }),
      },
      {
        accessor: 'device_user',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.column.device_user',
        }),
      },
      {
        accessor: 'phone',
        canSort: true,
        header: intl.formatMessage({ id: 'company_consumption.column.phone' }),
      },
      {
        accessor: 'package_name',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.column.package_name',
        }),
      },
      {
        accessor: 'consumption',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.column.consumption',
        }),
      },
    ],
    newMetadata()
  );

  useEffect(() => {
    if (!deviceUserId) return;

    dispatch(applicationDeviceUserSet(Number(deviceUserId)));
  }, [deviceUserId]);

  useEffect(() => {
    dispatch(applicationSet(applicationName));
  }, [applicationName]);

  useEffect(() => {
    dispatch(
      listApplicationConsumptionHistory(
        {
          applicationName,
          userId: deviceUserId ?? '',
        },
        filter
      )
    );
  }, [applicationName, deviceUserId]);

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="company_consumption.title" />}
        description={<FormattedMessage id="company_consumption.description" />}
      />
      <ApplicationHeader
        intlKey="company_consumption"
        showDescription={!!deviceUserId}
      />
      {!!deviceUserId && (
        <Box mt="2%" w="90%">
          <Table headerColumns={columns} rows={data} />
        </Box>
      )}
      {!!consumptionHistorys && (
        <Card mt="2%" w="90%">
          <MonthlyClusterBarChart data={consumptionHistorys} />
        </Card>
      )}
    </>
  );
};

export default ConsumptionHistory;
