import { Box } from '@chakra-ui/react';
import React, { useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import TimePicker from '../../../components/pages/LocationHistory/TimePicker';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import Table from '../../../components/Table/Table';
import { newMetadata } from '../../../helper/metadata';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listlocationHistoric } from '../../../store/location';

const LocationHistory = () => {
  const { locations, metadata } = useAppSelector((state) => state.location);
  const intl = useIntl();
  const dispatch = useAppDispatch();
  const allFilters = {};
  const filterClean = useMemo(() => {
    return {
      allFilters: [],
    };
  }, [allFilters]);
  function teste() {
    return;
  }
  useEffect(() => {
    dispatch(listlocationHistoric(metadata, allFilters));
  }, []);

  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());

  const handleMetadata = (value) => {
    dispatch(listlocationHistoric({ ...metadata, ...value }, allFilters));
  };
  const handleFilter = () => {
    dispatch(listlocationHistoric(metadata, filterClean));
  };
  const data = locations.map((location) => ({
    cells: [
      {
        field: 'name',
        value: location.device.deviceUser.name,
      },
      {
        field: 'phoneNumber',
        value: location.device.phoneNumber,
      },
      {
        field: 'lat_long',
        value: `${location.latitude} , ${location.longitude}`,
      },
      {
        field: 'address',
        value: location.address,
      },
      {
        field: 'precision',
        value: location.precision,
      },
      {
        field: 'createdAt',
        value: `${intl.formatDate(location.createdAt)} ${intl.formatTime(
          location.createdAt
        )}`,
      },
    ],
  }));
  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'location_history.column.user',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'location_history.column.phone_number',
        }),
        accessor: 'phoneNumber',
        canSort: true,
      },

      {
        header: intl.formatMessage({
          id: 'location_history.column.lat_long',
        }),
        accessor: 'lat_long',
        canSort: true,
      },

      {
        header: intl.formatMessage({
          id: 'location_history.column.address',
        }),
        accessor: 'address',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'location_history.column.precision',
        }),
        accessor: 'accuracy',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'location_history.column.date',
        }),
        accessor: 'createdAt',
        canSort: true,
      },
    ],
    newMetadata()
  );
  return (
    <>
      <PageTitle
        title={<FormattedMessage id="location_history.title" />}
        description={<FormattedMessage id="location_history.subtitle" />}
      />
      <FormContainer
        labelFilter={<FormattedMessage id="global.search" />}
        handleFilter={handleFilter}
      >
        <PageFilter>
          <FormControl
            textLabel={<FormattedMessage id="location_history.month" />}
          >
            <SelectAutocomplete
              options={[]}
              value={{}}
              onChange={teste}
              onInputChange={teste}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="location_history.day" />}
          >
            <SelectAutocomplete
              options={[]}
              value={{}}
              onChange={teste}
              onInputChange={teste}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="location_history.initial_time" />}
          >
            <TimePicker
              textLabel={<FormattedMessage id="location_history.select" />}
              options={startDate}
              onChange={setStartDate}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="location_history.final_time" />}
          >
            <TimePicker
              textLabel={<FormattedMessage id="location_history.select" />}
              options={endDate}
              onChange={setEndDate}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="location_history.precision" />}
          >
            <SelectAutocomplete
              options={[]}
              value={{}}
              onChange={teste}
              onInputChange={teste}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="location_history.user" />}
          >
            <SelectAutocomplete
              options={[]}
              value={{}}
              onChange={teste}
              onInputChange={teste}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      <PageActions initialSearch={''} onSearch={() => teste()} />
      <Box w="90%">
        <Table
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination pagination={metadata} onPageChange={handleMetadata} />
    </>
  );
};

export default LocationHistory;
