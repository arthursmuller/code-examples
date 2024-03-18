import { Box, FormControl, FormLabel, useRadioGroup } from '@chakra-ui/react';
import _ from 'lodash';
import React, { useState, useEffect, useMemo, useCallback } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import PageToaster from '../../../components/PageToaster';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import TableComponent from '../../../components/Table/Table';
import TableActions from '../../../components/TableActions';
import Text from '../../../components/Text';
import {
  DEBOUNCE_TIME,
  PrivilegeEnum,
  routeWithParameters,
} from '../../../helper';
import { useSorting } from '../../../helper/sort';
import { usePrivilege } from '../../../hooks/usePrivilege';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  listAdminUsers,
  closeAdminUserToaster,
} from '../../../store/adminUser';
import { listGroupsToFilter } from '../../../store/group';
import { listSubgroupsToFilter } from '../../../store/subgroup';
import { ListMetadata } from '../../../types/generic_list';

type radioTabs = 'company' | 'group_manager' | 'subgroup_manager';

const ManageAdminUsers = () => {
  const { accessFilterCompany, accessFilterGroup, accessFilterSubgroup } =
    usePrivilege();

  const dispatch = useAppDispatch();
  const { adminUsers, showToaster, metadata, errors } = useAppSelector(
    (state) => state.adminUser
  );
  
  const { user: { company } } = useAppSelector((state) => state.auth);
  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroupsToFilter } = useAppSelector((state) => state.subgroup);

  const [selectedTab, setSelectedTab] = useState<radioTabs>('company');
  const [selectFilter, setSelectFilter] = useState<number>(company?.id);
  const [searchFilter, setSearchFilter] = useState('');
  const [toasterState, setToasterState] = useState(false);

  const setSearchFilterDebounced = useCallback(
    _.debounce((value) => setSearchFilter(value), DEBOUNCE_TIME),
    []
  );

  const privilegeIdByTab = (selectedTab: radioTabs) =>
    selectedTab === 'company'
      ? PrivilegeEnum.COMPANY
      : selectedTab === 'group_manager'
      ? PrivilegeEnum.GROUP
      : PrivilegeEnum.SUBGROUP;

  const cleanFilter = {
    search: searchFilter,
    privilegeId: privilegeIdByTab(selectedTab),
    groupId: selectedTab === 'group_manager' ? selectFilter : undefined,
    subGroupId: selectedTab === 'subgroup_manager' ? selectFilter : undefined,
  };

  const intl = useIntl();

  const radioDefaultValue = useMemo(() => {
    return accessFilterCompany
      ? 'company'
      : accessFilterGroup
      ? 'group_manager'
      : 'subgroup_manager';
  }, [accessFilterCompany, accessFilterGroup]);

  useEffect(() => {
    setSelectedTab(radioDefaultValue);
  }, [radioDefaultValue]);

  useEffect(() => {
    dispatch(listAdminUsers(metadata, cleanFilter));
  }, [selectedTab, selectFilter, searchFilter]);

  useEffect(() => {
    if (selectedTab === 'company' && accessFilterCompany) {
      setSelectFilter(company?.id);
    }
    if (selectedTab === 'group_manager' && accessFilterGroup) {
      dispatch(listGroupsToFilter());
    }
    if (selectedTab === 'subgroup_manager' && accessFilterSubgroup) {
      dispatch(listSubgroupsToFilter());
    }
  }, [selectedTab]);

  useEffect(() => {
    if (showToaster) {
      setToasterState(true);
      dispatch(closeAdminUserToaster());
    }
  }, [showToaster]);

  const handlePagination = (newPagination: Partial<ListMetadata>) => {
    dispatch(listAdminUsers({ ...metadata, ...newPagination }, cleanFilter));
  };

  const ruleOptions: Array<{ id: radioTabs; label: string }> = [];
  if (accessFilterCompany) {
    ruleOptions.push({
      id: 'company',
      label: intl.formatMessage({
        id: 'manage_admin.filter.company',
        defaultMessage: 'Responsable Empresa',
      }),
    });
  }
  if (accessFilterGroup) {
    ruleOptions.push({
      id: 'group_manager',
      label: intl.formatMessage({
        id: 'manage_admin.filter.group_manager',
        defaultMessage: 'Responsable Grupo',
      }),
    });
  }
  if (accessFilterSubgroup) {
    ruleOptions.push({
      id: 'subgroup_manager',
      label: intl.formatMessage({
        id: 'manage_admin.filter.subgroup_manager',
        defaultMessage: 'Responsable Subgrupo',
      }),
    });
  }

  const changeRadio = (value: radioTabs) => {
    setSelectedTab(value);
    setSelectFilter(value === 'company' ? company?.id : null);
  };

  const { getRadioProps } = useRadioGroup({
    name: 'ruleOptions',
    defaultValue: radioDefaultValue,
    onChange: changeRadio,
  });

  const renderSelectFilter = () => {
    let selectData: {
      title: string;
      selectList: { id?: number; name?: string }[];
      disabled?: boolean;
    } = { title: '', selectList: [] };

    if (selectedTab === 'group_manager') {
      selectData = {
        title: intl.formatMessage({
          id: 'manage_admin.filter.title_group_manager',
        }),
        selectList: groupsToFilter,
      };
    } else if (selectedTab === 'subgroup_manager') {
      selectData = {
        title: intl.formatMessage({
          id: 'manage_admin.filter.title_subgroup_manager',
        }),
        selectList: subgroupsToFilter,
      };
    } else {
      selectData = {
        title: intl.formatMessage({
          id: 'manage_admin.filter.title_company',
        }),
        selectList: [company],
        disabled: true,
      };
    }

    return (
      <>
        <FormLabel fontSize="sm" color="gray.500">
          {selectData.title}
        </FormLabel>
        <Select
          backgroundColor="white"
          value={selectFilter || ''}
          onChange={(e: React.ChangeEvent<HTMLSelectElement>) =>
            setSelectFilter(parseInt(e.target.value))
          }
          disabled={selectData.disabled}
        >
          {selectData.selectList.map((option) => (
            <option key={option?.id} value={option?.id}>
              {option?.name}
            </option>
          ))}
        </Select>
      </>
    );
  };

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'manage_admin.column.name',
          defaultMessage: 'Nombre',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_admin.column.adminID',
          defaultMessage: 'Número de Identificacíon',
        }),
        accessor: 'adminID',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_admin.column.email',
          defaultMessage: 'E-mail',
        }),
        accessor: 'email',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'manage_admin.column.gtm',
          defaultMessage: 'GTM',
        }),
        accessor: 'gmt',
      },
      {
        header: intl.formatMessage({
          id: 'manage_admin.column.language',
          defaultMessage: 'Language',
        }),
        accessor: 'language',
      },
      {
        header: '',
        accessor: 'actions',
      },
    ],
    metadata
  );

  const data = adminUsers.map((adminUser) => ({
    cells: [
      {
        field: 'name',
        value: adminUser.name,
      },
      {
        field: 'adminID',
        value: adminUser.adminID,
      },
      {
        field: 'email',
        value: adminUser.email,
      },
      {
        field: 'gmt',
        value: adminUser.gmt,
      },
      {
        field: 'language',
        value: adminUser.language,
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <TableActions
              entityIntlLabel={intl.formatMessage({
                id: 'manage_admin.entity',
                defaultMessage: 'Admistrador',
              })}
              linkEdit={routeWithParameters(routes.adminUsers.edit, {
                id: adminUser.id,
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
        title={intl.formatMessage({
          id: 'manage_admin.title',
          defaultMessage: 'Gerenciar usuarios administradores',
        })}
        description={intl.formatMessage({
          id: 'manage_admin.description',
          defaultMessage: 'Lorem ipsum',
        })}
      />
      <FormContainer>
        <PageFilter>
          <Box w="100%" d="flex" flexDirection="column" alignItems="top">
            <Text
              color="gray.500"
              fontSize="sm"
              fontWeight="400"
              m="0 0 10px 0"
            >
              <FormattedMessage
                id="manage_admin.filter.title"
                defaultMessage="Privilegio"
                description="titulo filtro"
              />
            </Text>
            <Box d="flex" flexDirection="row" w="100%">
              {ruleOptions.map((option) => {
                const radio = getRadioProps({
                  value: option.id,
                });
                return (
                  <RadioButton key={option.id} {...radio}>
                    {option.label}
                  </RadioButton>
                );
              })}
            </Box>
          </Box>

          <Box w="100%">
            <FormControl>{renderSelectFilter()}</FormControl>
          </Box>
        </PageFilter>
      </FormContainer>

      <PageToaster
        showToaster={toasterState}
        onClose={() => setToasterState(false)}
        message={
          !errors
            ? intl.formatMessage({
                id: 'manage_admin.toaster.success',
                defaultMessage: 'Administrador cadastrado com Sucesso',
                description: 'toster de sucesso',
              })
            : errors?.message
        }
        type={!errors ? 'success' : 'error'}
      />
      <PageActions
        initialSearch={searchFilter}
        onSearch={(value: string) => {
          setSearchFilterDebounced(value);
        }}
        linkNew={routes.adminUsers.register}
      />
      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handlePagination}
        />
      </Box>
      <PagePagination pagination={metadata} onPageChange={handlePagination} />
    </>
  );
};

export default ManageAdminUsers;
