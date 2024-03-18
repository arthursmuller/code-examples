import { Box, FormLabel } from '@chakra-ui/react';
import { useState, useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import PageToaster from '../../../components/PageToaster';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import TableComponent from '../../../components/Table/Table';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import { listReportGps } from '../../../store/reportGps';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';

interface ReportGpsTypeFilter {
  startAt?: Date;
  endAt?: Date;
}

const ReportGps = () => {
  const dispatch = useAppDispatch();
  const { reportGps, errors, metadata } = useAppSelector(
    (state) => state.reportGps
  );
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const [filterReportGps, setFilterReportGps] = useState<ReportGpsTypeFilter>({});
  const [filterPhone, setFilterPhone] = useState<DeviceUserType>();

  const [showToaster, setShowToaster] = useState(false);

  const allFilters = {
    userId: filterPhone?.id,
    startAt: filterReportGps?.startAt,
    endAt: filterReportGps?.endAt,
  };

  useEffect(() => {
    dispatch(listReportGps(metadata, allFilters));
  }, [filterReportGps, filterPhone]);

  const handleFilterPhone = (newFilter: DeviceUserType) => {
    setFilterPhone(newFilter);
    dispatch(listDeviceUserToFilter());
  };
  const handleFilterPhoneChange = (value) => {
    dispatch(listDeviceUserToFilter({ search: value }));
  };

  const handlePagination = (newPagination: Partial<ListMetadata>) => {
    dispatch(listReportGps({ ...metadata, ...newPagination }, allFilters));
  };

  const intl = useIntl();

  const handlePeriodFilterChange = (date: Date, field: string) => {
    setFilterReportGps({ ...filterReportGps, [field]: date });
  };

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: 'global.user',
          defaultMessage: 'Usuário',
        }),
        accessor: 'device',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'global.phone',
          defaultMessage: 'Teléfono',
        }),
        accessor: 'phone',
        canSort: false,
      },
      {
        header: intl.formatMessage({
          id: 'global.gps',
          defaultMessage: 'GPS',
        }),
        accessor: 'gps',
        canSort: false,
      },
      {
        header: intl.formatMessage({
          id: 'global.date',
          defaultMessage: 'Fecha',
        }),
        accessor: 'createdAt',
        canSort: true,
      },
    ],
    metadata
  );

  const data = reportGps.map((gps) => ({
    cells: [
      {
        field: 'device',
        value: gps.device.deviceUser.name,
      },
      {
        field: 'phone',
        value: gps.device.phoneNumber,
      },
      {
        field: 'gps',
        value: '',
        transform: () => {
          return gps.gps ? (
            <FormattedMessage id="global.conected" />
          ) : (
            <FormattedMessage id="global.disconnected" />
          );
        },
      },
      {
        field: 'createdAt',
        value: '',
        transform: () => {
          return gps.createdAt.toString();
        },
      },
    ],
  }));

  return (
    <>
      <Box>
        <PageTitle
          title={<FormattedMessage id="reportsGps.title" />}
          description={<FormattedMessage id="reportsGps.description" />}
        />

        <FormContainer>
          <PageFilter>
            <FormControl w="176px" mr="24px">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="message.start_date" />
              </FormLabel>
              <DatePicker
                selected={filterReportGps.startAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'startAt');
                }}
              />
            </FormControl>
            <FormControl w="176px">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="message.end_date" />
              </FormLabel>
              <DatePicker
                selected={filterReportGps.endAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'endAt');
                }}
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
          </PageFilter>
        </FormContainer>
        <PageToaster
          message={
            errors?.message || <FormattedMessage id="message.toaster_success" />
          }
          onClose={() => setShowToaster(false)}
          showToaster={showToaster}
          type={errors ? 'error' : 'success'}
        />
      </Box>
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

export default ReportGps;
