import { Box } from '@chakra-ui/react';
import { useState, useEffect } from 'react';
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
import { listGroupsToFilter } from '../../../store/group';
import {
  listSubgroups,
  listSubgroupsToFilter,
  subgroupDelete,
  subgroupToaster,
} from '../../../store/subgroup';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';
import { GroupType } from '../../../types/group';
import { SubgroupType } from '../../../types/subgroups';
import { ID } from '../../../types/util';

const ManageSubGroups = () => {
  const dispatch = useAppDispatch();
  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroups, metadata, subgroupsToFilter, errors, toaster } =
    useAppSelector((state) => state.subgroup);
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const [filterGroup, setFilterGroup] = useState<GroupType>();
  const [filterSubGroup, setFilterSubGroup] = useState<SubgroupType>();
  const [filterPhone, setFilterPhone] = useState<DeviceUserType>();
  const [showToaster, setShowToaster] = useState(false);

  const intl = useIntl();

  const allFilters = {
    subGroupId: filterSubGroup?.id,
    groupId: filterGroup?.id,
    userId: filterPhone?.id,
  };

  useEffect(() => {
    dispatch(listGroupsToFilter());
    dispatch(listSubgroupsToFilter());
    dispatch(listDeviceUserToFilter());
  }, []);

  useEffect(() => {
    if (toaster) {
      setShowToaster(true);
      dispatch(subgroupToaster(false));
    }
  }, [toaster]);

  useEffect(() => {
    dispatch(listSubgroups(metadata, allFilters));
  }, [filterSubGroup, filterGroup, filterPhone]);

  const handleDestroy = (id: ID) => {
    dispatch(subgroupDelete(id));
  };

  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(listSubgroups({ ...metadata, ...newMetadata }, allFilters));
  };

  const handleFilterSubGroup = (newFilter) => {
    setFilterSubGroup(newFilter);
  };

  const handleFilterSubGroupChange = (value) => {
    dispatch(listSubgroupsToFilter({ search: value }));
  };

  const handleFilterGroup = (newFilter: GroupType) => {
    setFilterGroup(newFilter);
  };

  const handleFilterPhone = (newFilter) => {
    setFilterPhone(newFilter);
  };

  const handlePagination = (newPagination: Partial<ListMetadata>) => {
    dispatch(listSubgroups({ ...metadata, ...newPagination }));
  };

  const handleFilterPhoneChange = (value) => {
    dispatch(listDeviceUserToFilter({ search: value }));
  };

  const handleFilterGroupChange = (value) => {
    dispatch(listGroupsToFilter({ search: value }));
  };
  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'manage_subgroups.column.name',
          defaultMessage: 'Nome do subgrupo',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_subgroups.column.group',
          defaultMessage: 'Nome do grupo',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_subgroups.column.amount',
          defaultMessage: 'Quantidade de usuários no subgrupo',
        }),
        accessor: 'amount',
        canSort: true,
      },
    ],
    metadata
  );

  const data = subgroups.map((subgroup) => ({
    cells: [
      {
        field: 'name',
        value: subgroup.name,
      },
      {
        field: 'group',
        value: subgroup.group?.name,
      },
      {
        field: 'amount',
        value: subgroup.userCount,
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <TableActions
              entityIntlLabel={
                <FormattedMessage
                  id="manage_subgroups.entity"
                  defaultMessage="Subgrupo"
                />
              }
              openDestroy={() => handleDestroy(subgroup.id)}
              linkEdit={routeWithParameters(routes.subgroups.edit, {
                id: subgroup.id,
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
        title={<FormattedMessage id="manage_subgroups.title" />}
        description={<FormattedMessage id="manage_subgroups.description" />}
      />
      <FormContainer>
        <PageFilter>
          <FormControl textLabel={<FormattedMessage id="global.group" />}>
            <SelectAutocomplete
              options={groupsToFilter}
              value={filterGroup}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
              placeholder={<FormattedMessage id="global.group" />}
            />
          </FormControl>

          <FormControl textLabel={<FormattedMessage id="global.subgroup" />}>
            <SelectAutocomplete
              options={subgroupsToFilter}
              value={filterSubGroup}
              onChange={handleFilterSubGroup}
              onInputChange={handleFilterSubGroupChange}
              placeholder={<FormattedMessage id="global.subgroup" />}
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

      <PageToaster
        message={
          errors?.message || <FormattedMessage id="manage_subgroups.success" />
        }
        onClose={() => setShowToaster(false)}
        showToaster={showToaster}
        type={errors ? 'error' : 'success'}
      />

      <PageActions linkNew={routes.subgroups.register} />

      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>

      <PagePagination pagination={metadata} onPageChange={handlePagination} />
    </>
  );
};

export default ManageSubGroups;
