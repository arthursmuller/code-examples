import { ChevronDownIcon, ChevronRightIcon } from '@chakra-ui/icons';
import { Box } from '@chakra-ui/react';
import React, { useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import PageToaster from '../../../components/PageToaster';
import TableComponent from '../../../components/Table/Table';
import { Body } from '../../../components/Table/TableInterfaces';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listLastLocation } from '../../../store/location';
import { messageToaster } from '../../../store/message';
import { ListMetadata } from '../../../types/generic_list';
import ListDrillDown from './ListDrillDown';

export interface LocationTypeFilter {
  startAt?: Date;
  endAt?: Date;
  search?: string;
}

const DeviceLocation = () => {
  const dispatch = useAppDispatch();
  const { locations, errors, metadata, toaster } = useAppSelector(
    (state) => state.location
  );

  const [showToaster, setShowToaster] = useState(false);

  const [filterLocation, setFilterLocation] = useState<LocationTypeFilter>({});

  const filterClean = useMemo(() => {
    return {
      startAt: filterLocation?.startAt,
      endAt: filterLocation?.endAt,
      search: filterLocation?.search,
    };
  }, [filterLocation]);

  useEffect(() => {
    if (toaster) {
      setShowToaster(true);
      dispatch(messageToaster(false));
    }
  }, [toaster]);

  const handleFilter = () => {
    dispatch(listLastLocation(metadata, filterClean));
  };

  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(listLastLocation({ ...metadata, ...newMetadata }, filterClean));
  };

  const handlePeriodFilterChange = (date: Date, field: string) => {
    setFilterLocation({ ...filterLocation, [field]: date });
  };

  const intl = useIntl();
  const columns = useSorting(
    [
      {
        header: '',
        accessor: 'expanderHandler',
      },
      {
        header: intl.formatMessage({
          id: 'device_location.column.user',
          defaultMessage: 'Usuário',
        }),
        accessor: 'user',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'device_location.column.phone_number',
          defaultMessage: 'Telefone',
        }),
        accessor: 'phoneNumber',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'device_location.column.lat_lng',
          defaultMessage: 'Lat/Lng',
        }),
        accessor: 'lat_lng',
      },
      {
        header: intl.formatMessage({
          id: 'device_location.column.created_at',
          defaultMessage: 'Data',
        }),
        accessor: 'createdAt',
        canSort: true,
      },
    ],
    metadata
  );
  const data: Body[] = locations.map((location) => ({
    cells: [
      {
        field: 'expanderHandler',
        value: null,
        transform: ({ isExpanded, toggleExpanded }) => (
          <span>
            {isExpanded ? (
              <ChevronDownIcon boxSize="5" color="#d7d7dc" onClick={toggleExpanded} />
            ) : (
              <ChevronRightIcon boxSize="5" color="#d7d7dc" onClick={toggleExpanded} />
            )}
          </span>
        ),
        chackraProps: { width: 0 }
      },
      {
        field: 'user',
        value: location.device.deviceUser?.name,
      },
      {
        field: 'phoneNumber',
        value: location.device.phoneNumber,
      },
      {
        field: 'lat_lng',
        value: `${location.latitude},${location.longitude}`,
      },
      {
        field: 'createdAt',
        value: `${intl.formatDate(location.createdAt)} ${intl.formatTime(
          location.createdAt
        )}`,
      },
      {
        field: 'expander',
        value: null,
        transform: () => (
          <ListDrillDown location={location} />
        ),
      },
    ],
  }));

  return (
    <>
      <Box>
        <PageTitle
          title={<FormattedMessage id="device_location.title" />}
          description={<FormattedMessage id="device_location.sub_title" />}
        />

        <FormContainer
          labelFilter={<FormattedMessage id="global.search" />}
          handleFilter={handleFilter}
        >
          <PageFilter>
            <FormControl
              w="176px"
              mr="24px"
              textLabel={<FormattedMessage id="device_location.start_date" />}
            >
              <DatePicker
                selected={filterLocation.startAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'startAt');
                }}
              />
            </FormControl>
            <FormControl
              w="176px"
              textLabel={<FormattedMessage id="device_location.end_date" />}
            >
              <DatePicker
                selected={filterLocation.endAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'endAt');
                }}
              />
            </FormControl>
          </PageFilter>
        </FormContainer>
        <PageToaster
          message={
            errors?.message || (
              <FormattedMessage id="device_location.toaster_success" />
            )
          }
          onClose={() => setShowToaster(false)}
          showToaster={showToaster}
          type={errors ? 'error' : 'success'}
        />
      </Box>
      <PageActions
        onSearch={(e) => setFilterLocation({ ...filterLocation, search: e })}
      />
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

export default DeviceLocation;
