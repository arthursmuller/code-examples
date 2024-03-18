import { Box, FormControl, FormLabel, useRadioGroup } from '@chakra-ui/react';
import _ from 'lodash';
import React, { useEffect, useState, useMemo, useCallback } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import Input from '../../../components/Input';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import TableComponent from '../../../components/Table/Table';
import Text from '../../../components/Text';
import {
  DEBOUNCE_TIME,
  listStates,
  listStatesObject,
  StateEnum,
} from '../../../helper';
import { sanitizeFilter } from '../../../helper/filter';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listCompanyLicenses } from '../../../store/company';
import { listPlans } from '../../../store/plan';
import { ListMetadata } from '../../../types/generic_list';
import { PlanType } from '../../../types/plan';
import { StateType } from '../../../types/state';

interface filterCompanyLicense {
  phoneNumber: number;
  status: 'all' | 'yes' | 'no';
  state: StateType['key'] | null;
  plan: PlanType['planName'] | null;
}

const CompanyLicense = () => {
  const dispatch = useAppDispatch();
  const { plans } = useAppSelector((state) => state.plan);
  const { licenses, licensesMetadata } = useAppSelector(
    (state) => state.company
  );
  const [filterPhoneUndebouced, setFilterPhoneUndebouced] = useState<string>();
  const [filter, setFilter] = useState<filterCompanyLicense>({
    phoneNumber: null,
    status: 'all',
    state: null,
    plan: null,
  });

  const cleanFilter = useMemo(() => {
    const allFilters = {
      phoneNumber: filter.phoneNumber,
      status:
        filter.status === 'all'
          ? undefined
          : filter.status === 'yes'
          ? '1'
          : '0',
      state: filter.state,
      planName: filter.plan,
    };
    return sanitizeFilter(allFilters);
  }, [filter]);

  useEffect(() => {
    dispatch(listPlans());
  }, []);

  useEffect(() => {
    dispatch(listCompanyLicenses(licensesMetadata, cleanFilter));
  }, [cleanFilter]);

  const renderStatesOptions = () => {
    return listStates.map((option) => {
      return (
        <option value={option.key} key={Math.random()}>
          {intl.formatMessage({ id: option.value })}
        </option>
      );
    });
  };

  const handleFilterPhoneDebounced = useCallback(
    _.debounce(
      (value) => setFilter({ ...filter, phoneNumber: value }),
      DEBOUNCE_TIME
    ),
    []
  );

  const handleStateSelect = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setFilter({ ...filter, state: StateEnum[event.target.value] });
  };

  const handlePlanSelect = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setFilter({ ...filter, plan: event.target.value });
  };

  const handleNewMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(
      listCompanyLicenses({ ...licensesMetadata, ...newMetadata }, cleanFilter)
    );
  };

  const intl = useIntl();

  const enrolledDeviceValues = [
    {
      value: 'all',
      text: intl.formatMessage({ id: 'global.all' }),
    },
    {
      value: 'yes',
      text: intl.formatMessage({ id: 'global.yes' }),
    },
    {
      value: 'no',
      text: intl.formatMessage({ id: 'global.no' }),
    },
  ];

  const { getRootProps, getRadioProps } = useRadioGroup({
    name: 'framework',
    defaultValue: 'all',
    onChange: (nextValue: filterCompanyLicense['status']) =>
      setFilter({ ...filter, status: nextValue }),
  });
  const groupRadio = getRootProps();

  const columnsTable = useSorting(
    [
      {
        header: intl.formatMessage({ id: 'global.phone' }),
        accessor: 'phoneNumber',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'global.plan' }),
        accessor: 'planName',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'global.last_update' }),
        accessor: 'updatedAt',
        canSort: false,
      },
      {
        header: intl.formatMessage({ id: 'global.Enrolled_device' }),
        accessor: 'status',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'global.state' }),
        accessor: 'state',
        canSort: true,
      },
    ],
    licensesMetadata
  );

  const dataTable = licenses.map((license) => ({
    cells: [
      {
        field: 'phoneNumber',
        value: license.phoneNumber,
      },
      {
        field: 'planName',
        value: license.planName,
      },
      {
        field: 'updatedAt',
        value: license.updatedAt,
      },
      {
        field: 'status',
        value: license.status
          ? intl.formatMessage({ id: 'global.yes' })
          : intl.formatMessage({ id: 'global.no' }),
      },
      {
        field: 'state',
        value: '',
        transform: () => {
          return (
            <Text m="0" fontWeight="400" color="green.500">
              {intl.formatMessage({
                id: listStatesObject[license.state].value,
              })}
            </Text>
          );
        },
      },
    ],
  }));

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="company_license.title" />}
        description={<FormattedMessage id="company_license.description" />}
      />
      <FormContainer>
        <PageFilter>
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="global.phone" />
            </FormLabel>
            <Input
              inputProps={{
                placeholder: intl.formatMessage({ id: 'global.phone' }),
                type: 'text',
                onChange: (e: React.ChangeEvent<HTMLInputElement>) => {
                  setFilterPhoneUndebouced(e.target.value);
                  handleFilterPhoneDebounced(e.currentTarget.value);
                },
                value: filterPhoneUndebouced || '',
                name: 'phone',
              }}
            />
          </FormControl>
          <FormControl
            d="flex"
            flexDirection="column"
            w="210px"
            mr="24px"
            alignItems="center"
          >
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="global.Enrolled_device" />
            </FormLabel>
            <Box d="flex" flexDirection="row" {...groupRadio}>
              {enrolledDeviceValues.map((device) => {
                const radio = getRadioProps({ value: device.value });
                return (
                  <RadioButton key={device.value} {...radio}>
                    {device.text}
                  </RadioButton>
                );
              })}
            </Box>
          </FormControl>

          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="global.state" />
            </FormLabel>
            <Select
              name="state"
              onChange={handleStateSelect}
              value={filter.state}
            >
              {renderStatesOptions()}
            </Select>
          </FormControl>

          <FormControl w="100%">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="global.plan" />
            </FormLabel>
            <Select
              backgroundColor="white"
              onChange={handlePlanSelect}
              value={filter.plan}
            >
              {plans.map((plan) => (
                <option key={plan.planName} value={plan.planName}>
                  {plan.planName}
                </option>
              ))}
            </Select>
          </FormControl>
        </PageFilter>
      </FormContainer>
      <PageActions />
      <Box w="90%">
        <TableComponent
          headerColumns={columnsTable}
          rows={dataTable}
          handleSort={handleNewMetadata}
        />
        <PagePagination
          pagination={licensesMetadata}
          onPageChange={handleNewMetadata}
        />
      </Box>
    </>
  );
};

export default CompanyLicense;
