import { Box } from '@chakra-ui/react';
import _ from 'lodash';
import { useState, useEffect, useCallback } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import Card from '../../../components/Card';
import FormControl from '../../../components/FormControl';
import Heading from '../../../components/Heading';
import PageActions from '../../../components/PageActions';
import PagePagination from '../../../components/PagePagination';
import PageToaster from '../../../components/PageToaster';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import TableComponent from '../../../components/Table/Table';
import TableActions from '../../../components/TableActions';
import Text from '../../../components/Text';
import { DEBOUNCE_TIME, routeWithParameters } from '../../../helper';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  listDeviceUserToFilter,
  listDeviceUsers,
  deviceUserToaster,
} from '../../../store/deviceUser';
import { listGroupsToFilter } from '../../../store/group';
import { listSubgroupsToFilter } from '../../../store/subgroup';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';
import { GroupType } from '../../../types/group';
import { SubgroupType } from '../../../types/subgroups';

const ManageUsers = () => {
  const dispatch = useAppDispatch();

  const {
    deviceUsers,
    devicesUsersToFilter,
    metadata: deviceMetadata,
    errors,
    toaster,
  } = useAppSelector((state) => state.deviceUser);
  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroupsToFilter } = useAppSelector((state) => state.subgroup);

  const [filterGroup, setFilterGroup] = useState<GroupType>();
  const [filterSubgroup, setFilterSubgroup] = useState<SubgroupType>();
  const [filterPhone, setFilterPhone] = useState<DeviceUserType>();
  const [searchFilter, setSearchFilter] = useState('');

  const [showToaster, setShowToaster] = useState(false);
  const setSearchFilterDebounced = useCallback(
    _.debounce((value) => setSearchFilter(value), DEBOUNCE_TIME),
    []
  );

  const intl = useIntl();

  useEffect(() => {
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listGroupsToFilter());
    dispatch(listSubgroupsToFilter());
  }, []);

  useEffect(() => {
    if (toaster) {
      setShowToaster(true);
      dispatch(deviceUserToaster(false));
    }
  }, [toaster]);

  const handleFilterGroup = (newFilter: GroupType) => {
    setFilterGroup(newFilter);
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listGroupsToFilter());
  };
  const handleFilterGroupChange = (value) => {
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listGroupsToFilter({ search: value }));
  };

  const handleFilterSubGroup = (newFilter: SubgroupType) => {
    setFilterSubgroup(newFilter);
    dispatch(listSubgroupsToFilter());
    dispatch(listDeviceUsers(deviceMetadata));
  };
  const handleFilterSubGroupChange = (value) => {
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listSubgroupsToFilter({ search: value }));
  };

  const handleFilterPhone = (newFilter: DeviceUserType) => {
    setFilterPhone(newFilter);
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listDeviceUserToFilter());
  };
  const handleFilterPhoneChange = (value) => {
    dispatch(listDeviceUsers(deviceMetadata));
    dispatch(listDeviceUserToFilter({ search: value }));
  };

  const handlePagination = (newPagination: Partial<ListMetadata>) => {
    dispatch(listDeviceUsers({ ...deviceMetadata, ...newPagination }));
  };

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'global.name',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.phone',
        }),
        accessor: 'phone_number',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_deviceuser.table.id_mumber',
        }),
        accessor: 'id_number',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.company',
        }),
        accessor: 'company_name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.group',
        }),
        accessor: 'group',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.subgroup',
        }),
        accessor: 'subgroup',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.email',
        }),
        accessor: 'email',
        canSort: true,
      },
    ],
    deviceMetadata
  );

  const data = deviceUsers.map((deviceUser) => ({
    cells: [
      {
        field: 'name',
        value: deviceUser.name || '',
      },
      {
        field: 'phone_number',
        value: deviceUser.device?.phoneNumber || '',
      },
      {
        field: 'id_number',
        value: '',
      },
      {
        field: 'company_name',
        value: deviceUser.company?.corporateName || '',
      },
      {
        field: 'group',
        value: deviceUser.group?.name || '',
      },
      {
        field: 'subgroup',
        value: deviceUser.subGroup?.name || '',
      },
      {
        field: 'email',
        value: deviceUser.email || '',
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <TableActions
              entityIntlLabel={intl.formatMessage({
                id: 'manage_deviceuser.entity',
              })}
              linkEdit={routeWithParameters(routes.deviceUsers.edit, {
                id: deviceUser.id,
              })}
            />
          );
        },
      },
    ],
  }));

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="edit_deviceuser.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="edit_deviceuser.description" />
        </Text>
      </Box>
      <Card>
        <Text
          margin="0 0 20px 0"
          color="gray.600"
          fontSize="md"
          fontWeight="bold"
        >
          <FormattedMessage id="global.filter" />
        </Text>
        <Box d="flex" flexDirection="row">
          <FormControl
            textLabel={<FormattedMessage id="global.group" />}
            w="100%"
            mr="24px"
          >
            <SelectAutocomplete
              options={groupsToFilter}
              value={filterGroup}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
              placeholder={<FormattedMessage id="global.group" />}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="global.subgroup" />}
            w="100%"
            mr="24px"
          >
            <SelectAutocomplete
              options={subgroupsToFilter}
              value={filterSubgroup}
              onChange={handleFilterSubGroup}
              onInputChange={handleFilterSubGroupChange}
              placeholder={<FormattedMessage id="global.subgroup" />}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="global.phone" />}
            w="100%"
            mr="24px"
          >
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filterPhone}
              onChange={handleFilterPhone}
              onInputChange={handleFilterPhoneChange}
              placeholder={<FormattedMessage id="global.phone" />}
            />
          </FormControl>
        </Box>
      </Card>
      <PageToaster
        showToaster={showToaster}
        type={errors ? 'error' : 'success'}
        message={
          errors ? (
            errors.message
          ) : (
            <FormattedMessage id="edit_deviceuser.success" />
          )
        }
        onClose={() => setShowToaster(false)}
      />
      <PageActions
        initialSearch={searchFilter}
        onSearch={(value: string) => {
          setSearchFilterDebounced(value);
        }}
      />
      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handlePagination}
        />
      </Box>
      <PagePagination
        pagination={deviceMetadata}
        onPageChange={handlePagination}
      />
    </>
  );
};

export default ManageUsers;
