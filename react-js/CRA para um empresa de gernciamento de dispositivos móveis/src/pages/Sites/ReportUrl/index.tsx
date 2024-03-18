import { Box } from '@chakra-ui/react';
import _ from 'lodash';
import React, { useCallback, useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Input from '../../../components/Input';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import TableComponent from '../../../components/Table/Table';
import { DEBOUNCE_TIME } from '../../../helper';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import { listSites } from '../../../store/reports';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';

const ReportUrl = () => {
  const dispatch = useAppDispatch();
  const intl = useIntl();

  const { sitesUrl, metadataUrl } = useAppSelector((state) => state.reports);
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);

  const [filterDeviceUser, setFilterDeviceUser] = useState<DeviceUserType>();
  const [filterKeywordUndebounced, setFilterKeywordUndebounced] =
    useState<string>();
  const [filterKeywordDebounced, setFilterKeywordDebounced] =
    useState<string>();
  const [deviceUsersInput, setDeviceUsersInputUndebounced] = useState<string>();

  const setFilterKeyword = useCallback(
    _.debounce((value) => setFilterKeywordDebounced(value), DEBOUNCE_TIME),
    []
  );

  const setDeviceUsersInput = useCallback(
    _.debounce((value) => setDeviceUsersInputUndebounced(value), DEBOUNCE_TIME),
    []
  );

  const allfilters = {
    userId: filterDeviceUser?.id,
    keyword: filterKeywordDebounced,
  };

  useEffect(() => {
    dispatch(listSites(metadataUrl, allfilters));
  }, [filterDeviceUser, filterKeywordDebounced]);

  useEffect(() => {
    dispatch(listDeviceUserToFilter({ search: deviceUsersInput }));
  }, [deviceUsersInput]);

  const handleSearchChange = (value) => {
    setFilterKeyword(value);
    setFilterKeywordUndebounced(value);
  };

  const handleDeviceUsers = (value) => {
    setFilterDeviceUser(value);
  };

  const handleDeviceUsersInput = (value) => {
    setDeviceUsersInput(value);
  };

  const handlePagination = (newMetadata: ListMetadata) => {
    dispatch(listSites({ ...metadataUrl, ...newMetadata }, allfilters));
  };

  const data = sitesUrl.map((siteUrl) => ({
    cells: [
      {
        field: 'domain',
        value: siteUrl.domain,
      },
      {
        field: 'user',
        value: siteUrl.device?.deviceUser?.name,
      },
      {
        field: 'phoneNumber',
        value: siteUrl.device?.phoneNumber,
      },
      {
        field: 'accessedAt',
        value: `${intl.formatDate(siteUrl.accessedAt)} ${intl.formatTime(
          siteUrl.accessedAt
        )}`,
      },
    ],
  }));

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({ id: 'sites_url.column.url' }),
        accessor: 'domain',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'sites_url.column.user' }),
        accessor: 'user',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'sites_url.column.phone_number' }),
        accessor: 'phoneNumber',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'sites_url.column.date' }),
        accessor: 'accessedAt',
        canSort: true,
      },
    ],
    metadataUrl
  );

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="sites_url.title" />}
        description={<FormattedMessage id="sites_url.description" />}
      />
      <FormContainer>
        <PageFilter>
          <FormControl
            textLabel={<FormattedMessage id="sites_url.field_search" />}
          >
            <Input
              inputProps={{
                placeholder: intl.formatMessage({
                  id: 'sites_url.placeholder_search',
                }),
                name: 'search',
                value: filterKeywordUndebounced || '',
                onChange: (e) => handleSearchChange(e.target.value),
              }}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="sites_url.field_user" />}
          >
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filterDeviceUser}
              onChange={handleDeviceUsers}
              onInputChange={handleDeviceUsersInput}
              getOptionLabel={(option) =>
                `${option.name || ''} ${option.phoneNumber}`
              }
              placeholder={<FormattedMessage id="sites_url.field_user" />}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>

      <PageActions />

      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handlePagination}
        />
      </Box>

      <PagePagination
        onPageChange={handlePagination}
        pagination={metadataUrl}
      />
    </>
  );
};

export default ReportUrl;
