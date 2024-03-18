import { Box } from '@chakra-ui/react';
import _ from 'lodash';
import { useCallback, useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useParams } from 'react-router-dom';

import PageActions from '../../../components/PageActions';
import PagePagination from '../../../components/PagePagination';
import ApplicationHeader from '../../../components/pages/Applications/ApplicationHeader';
import MenuList from '../../../components/pages/Applications/MenuList';
import PageTitle from '../../../components/PageTitle';
import Table from '../../../components/Table/Table';
import { DEBOUNCE_TIME } from '../../../helper';
import { formatBytesTo } from '../../../helper/bytes';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  applicationSet,
  listApplicationDeviceUsers,
} from '../../../store/application';
import { ListMetadata } from '../../../types/generic_list';

const ApplicationDeviceUsers = () => {
  const dispatch = useAppDispatch();
  const { application, deviceUsers, deviceUsersMetadata, filter } =
    useAppSelector((state) => state.application);

  const intl = useIntl();
  const { applicationName } = useParams<{ applicationName: string }>();

  const [searchFilter, setSearchFilter] = useState('');

  const allFilters = { ...filter, search: searchFilter };
  const setSearchFilterDebounced = useCallback(
    _.debounce((value) => setSearchFilter(value), DEBOUNCE_TIME),
    []
  );

  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(
      listApplicationDeviceUsers(
        {
          ...deviceUsersMetadata,
          ...newMetadata,
          applicationName: application?.name,
        },
        allFilters
      )
    );
  };

  const columns = useSorting(
    [
      {
        accessor: 'group',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.group',
        }),
      },
      {
        accessor: 'subgroup',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.subgroup',
        }),
      },
      {
        accessor: 'device_user',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.device_user',
        }),
      },
      {
        accessor: 'phone',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.phone',
        }),
      },
      {
        accessor: 'package_name',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.package_name',
        }),
      },
      {
        accessor: 'consumption',
        canSort: true,
        header: intl.formatMessage({
          id: 'application_device_users.column.consumption',
        }),
      },
    ],
    deviceUsersMetadata
  );

  const data = deviceUsers.map((deviceUser) => ({
    cells: [
      {
        field: 'group',
        value: deviceUser?.group,
      },
      {
        field: 'subgroup',
        value: deviceUser?.subgroup,
      },
      {
        field: 'device_user',
        value: deviceUser?.user,
      },
      {
        field: 'phone',
        value: deviceUser?.phoneNumber,
      },
      {
        field: 'package_name',
        value: deviceUser?.packageName,
      },
      {
        field: 'consumption',
        value: formatBytesTo({ bytes: deviceUser?.consumption, to: 'MB' }),
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <MenuList
              showConsumptionHistory
              navigationProps={{
                applicationName: application?.name,
                deviceUserId: deviceUser?.userId,
              }}
            />
          );
        },
      },
    ],
  }));

  useEffect(() => {
    dispatch(applicationSet(applicationName));
  }, [applicationName]);

  useEffect(() => {
    if (!application?.name) return;

    dispatch(
      listApplicationDeviceUsers(
        {
          ...deviceUsersMetadata,
          applicationName: application.name,
        },
        allFilters
      )
    );
  }, [searchFilter, application]);

  return (
    <Box>
      <PageTitle
        title={<FormattedMessage id="application_device_users.title" />}
        description={
          <FormattedMessage id="application_device_users.description" />
        }
      />
      <ApplicationHeader intlKey="application_device_users" showDescription />
      <PageActions
        initialSearch={searchFilter}
        onSearch={(value: string) => {
          setSearchFilterDebounced(value);
        }}
      />
      <Box w="90%">
        <Table
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination
        pagination={deviceUsersMetadata}
        onPageChange={handleMetadata}
      />
    </Box>
  );
};

export default ApplicationDeviceUsers;
