import { Box } from '@chakra-ui/react';
import React, { useState, useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import PageToaster from '../../../components/PageToaster';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import TableComponent from '../../../components/Table/Table';
import TableActions from '../../../components/TableActions';
import { routeWithParameters } from '../../../helper';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import {
  deleteGroup,
  groupToaster,
  listGroups,
  listGroupsToFilter,
} from '../../../store/group';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';
import { GroupType } from '../../../types/group';
import { ID } from '../../../types/util';

const ManageGroups = () => {
  const dispatch = useAppDispatch();

  const { groups, metadata, groupsToFilter, toaster, errors } = useAppSelector(
    (state) => state.group
  );
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const [filterGroup, setFilterGroup] = useState<GroupType>();
  const [filterPhone, setFilterPhone] = useState<DeviceUserType>();

  const [showToaster, setShowToaster] = useState(false);

  const intl = useIntl();

  const allFilters = {
    name: filterGroup?.name,
    userId: filterPhone?.id,
  };

  useEffect(() => {
    dispatch(listDeviceUserToFilter());
    dispatch(listGroupsToFilter());
  }, []);

  useEffect(() => {
    dispatch(listGroups(metadata, allFilters));
  }, [filterGroup, filterPhone]);

  useEffect(() => {
    if (toaster) {
      setShowToaster(true);
      dispatch(groupToaster(false));
    }
  }, [toaster]);

  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(listGroups({ ...metadata, ...newMetadata }, allFilters));
  };

  const handleFilterGroup = (newFilter: GroupType) => {
    setFilterGroup(newFilter);
    dispatch(listGroupsToFilter());
  };

  const handleFilterPhone = (newFilter: DeviceUserType) => {
    setFilterPhone(newFilter);
    dispatch(listDeviceUserToFilter());
  };

  const handleFilterGroupChange = (value) => {
    dispatch(listGroupsToFilter({ search: value }));
  };

  const handleFilterPhoneChange = (value) => {
    dispatch(listDeviceUserToFilter({ search: value }));
  };

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'manage_groups.column.name',
          defaultMessage: 'Nome',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_groups.column.userCount',
          defaultMessage: 'Quantidade de usuários no grupo',
        }),
        accessor: 'userCount',
        canSort: true,
      },
    ],
    metadata
  );

  const handleDestroy = (id: ID) => {
    dispatch(deleteGroup(id));
  };

  const data = groups.map((group) => ({
    cells: [
      {
        field: 'name',
        value: group.name,
      },
      {
        field: 'amount',
        value: group.userCount,
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <TableActions
              entityIntlLabel={
                <FormattedMessage
                  id="manage_groups.entity"
                  defaultMessage="Grupo"
                />
              }
              openDestroy={() => handleDestroy(group.id)}
              linkEdit={routeWithParameters(routes.groups.edit, {
                id: group.id,
              })}
            />
          );
        },
      },
    ],
  }));

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="manage_groups.title" />}
        description={<FormattedMessage id="manage_groups.description" />}
      />
      <FormContainer>
        <PageFilter>
          <FormControl textLabel={<FormattedMessage id="global.group" />}>
            <SelectAutocomplete
              options={groupsToFilter}
              value={filterGroup}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
              placeholder={<FormattedMessage id="global.phone" />}
            />
          </FormControl>

          <FormControl textLabel={<FormattedMessage id="global.phone" />}>
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filterPhone}
              onChange={handleFilterPhone}
              onInputChange={handleFilterPhoneChange}
              getOptionLabel={(option) =>
                `${option.name || ''} ${option.phoneNumber}`
              }
              placeholder={<FormattedMessage id="global.phone" />}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      {errors ? (
        <PageToaster
          message={errors?.message}
          onClose={() => setShowToaster(false)}
          showToaster={showToaster}
          type="error"
        />
      ) : (
        <PageToaster
          message={<FormattedMessage id="manage_groups.success" />}
          onClose={() => setShowToaster(false)}
          showToaster={showToaster}
          type="success"
        />
      )}

      <PageActions linkNew={routes.groups.register} />
      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>

      <PagePagination pagination={metadata} onPageChange={handleMetadata} />
    </>
  );
};

export default ManageGroups;
