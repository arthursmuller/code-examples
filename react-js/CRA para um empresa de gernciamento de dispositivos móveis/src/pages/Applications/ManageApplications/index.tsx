import { Box } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import MenuList from '../../../components/pages/Applications/MenuList';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import Table from '../../../components/Table/Table';
import { formatBytesTo } from '../../../helper/bytes';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  applicationSetFilter,
  listApplications,
} from '../../../store/application';
import {
  deviceUserSelected,
  listDeviceUserToFilter,
} from '../../../store/deviceUser';
import { listGroupsToFilter } from '../../../store/group';
import { listSubgroupsToFilter } from '../../../store/subgroup';
import { DeviceUserType } from '../../../types/deviceUser';
import { GroupType } from '../../../types/group';
import { SubgroupType } from '../../../types/subgroups';

const ManageApplications = () => {
  const dispatch = useAppDispatch();

  const { applications, filter, metadata } = useAppSelector(
    (state) => state.application
  );

  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroupsToFilter } = useAppSelector((state) => state.subgroup);
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const [filterSearch, setFilterSearch] = useState('');

  const allFilters = {
    groupId: filter?.group?.id,
    subGroupId: filter?.subgroup?.id,
    userId: filter?.deviceUser?.id,
    search: filterSearch,
  };

  useEffect(() => {
    dispatch(listGroupsToFilter());
    dispatch(listSubgroupsToFilter());
    dispatch(listDeviceUserToFilter());
  }, []);

  useEffect(() => {
    dispatch(listApplications(metadata, allFilters));
  }, [filter, filterSearch]);

  const handleFilterGroup = (group: GroupType) => {
    dispatch(applicationSetFilter({ group }));
  };
  const handleFilterGroupChange = (value) => {
    dispatch(listGroupsToFilter({ search: value }));
  };
  const handleFilterSubGroup = (subgroup: SubgroupType) => {
    dispatch(applicationSetFilter({ subgroup }));
  };
  const handleFilterSubGroupChange = (value) => {
    dispatch(listSubgroupsToFilter({ search: value }));
  };
  const handleFilterDeviceUser = (deviceUser: DeviceUserType) => {
    dispatch(applicationSetFilter({ deviceUser }));
  };
  const handleFilterDeviceUserChange = (value) => {
    dispatch(deviceUserSelected({ search: value }));
  };
  const handleMetadata = (value) => {
    dispatch(listApplications({ ...metadata, ...value }, allFilters));
  };

  const intl = useIntl();
  const data = applications.map((application) => ({
    cells: [
      {
        field: 'name',
        value: application.name,
      },
      {
        field: 'quantity',
        value: application.quantity,
      },
      {
        field: 'consumption',
        value: formatBytesTo({ bytes: application.consumption, to: 'MB' }),
      },
      {
        field: 'time',
        value: application.time,
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <MenuList
              navigationProps={{ applicationName: application?.name }}
              showApplicationDeviceUsersItem={true}
              showConsumptionHistory={true}
            />
          );
        },
      },
    ],
  }));

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'application_manage.column.name',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'application_manage.column.quantity',
        }),
        accessor: 'quantity',
        canSort: true,
      },

      {
        header: intl.formatMessage({
          id: 'application_manage.column.consumption',
        }),
        accessor: 'consumption',
        canSort: true,
      },

      {
        header: intl.formatMessage({
          id: 'application_manage.column.time',
        }),
        accessor: 'time',
        canSort: true,
      },
    ],
    metadata
  );

  return (
    <Box>
      <PageTitle
        title={<FormattedMessage id="application_manage.title" />}
        description={<FormattedMessage id="application_manage.sub_title" />}
      />
      <FormContainer>
        <PageFilter>
          <FormControl
            textLabel={<FormattedMessage id="application_manage.group" />}
          >
            <SelectAutocomplete
              options={groupsToFilter}
              value={filter.group}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="application_manage.sub_group" />}
          >
            <SelectAutocomplete
              options={subgroupsToFilter}
              value={filter.subgroup}
              onChange={handleFilterSubGroup}
              onInputChange={handleFilterSubGroupChange}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="application_manage.users" />}
          >
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filter.deviceUser}
              onChange={handleFilterDeviceUser}
              onInputChange={handleFilterDeviceUserChange}
              getOptionLabel={(option) =>
                `${option.name || ''} ${option.phoneNumber || ''}`
              }
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      <PageActions
        initialSearch={filterSearch}
        onSearch={(e) => setFilterSearch(e)}
      />
      <Box w="90%">
        <Table
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination pagination={metadata} onPageChange={handleMetadata} />
    </Box>
  );
};

export default ManageApplications;
