import { Tabs, TabList, TabPanels, TabPanel } from '@chakra-ui/react';
import React, { useRef, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import SimpleReactValidator from 'simple-react-validator';

import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PageTitle from '../../../components/PageTitle';
import Tab from '../../../components/Tab';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  listCompanyConsumptionData,
  listCompanyConsumptionSms,
} from '../../../store/company';
import DataTable from './dataTable';
import SmsTable from './smsTable';

interface LocationTypeFilter {
  startAt?: Date;
  endAt?: Date;
  search?: string;
}

enum TabEnum {
  DATA,
  SMS,
}

const CompanyConsumption = () => {
  const intl = useIntl();
  const dispatch = useAppDispatch();

  const {
    consumptionsData,
    consumptionsDataMetadata,
    consumptionsSms,
    consumptionsSmsMetadata,
  } = useAppSelector((state) => state.company);

  const [dateInput, setDateInput] = useState<LocationTypeFilter>({});
  const [tabSelected, setTabSelected] = useState<TabEnum>(TabEnum.DATA);

  const allFilters = {
    startDate: dateInput?.startAt,
    endDate: dateInput?.endAt,
    search: dateInput?.search,
  };

  const handleTabsChange = (index) => {
    setTabSelected(index);
  };
  const handlePeriodFilterChange = (date: Date, field: string) => {
    setDateInput({ ...dateInput, [field]: date });
  };
  const handleFilter = () => {
    if (tabSelected === TabEnum.DATA) {
      dispatch(
        listCompanyConsumptionData(consumptionsDataMetadata, allFilters)
      );
    }
    if (tabSelected === TabEnum.SMS) {
      dispatch(listCompanyConsumptionSms(consumptionsSmsMetadata, allFilters));
    }
  };

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="company_consumption.title" />}
        description={<FormattedMessage id="company_consumption.description" />}
      />
      <Tabs borderRadius="5px" onChange={handleTabsChange}>
        <TabList d="flex" flexDirection="row" w="90%" borderBottom="4px">
          <Tab>
            <FormattedMessage id="company_consumption.tabs_data" />
          </Tab>
          <Tab>
            <FormattedMessage id="company_consumption.tabs_sms" />
          </Tab>
        </TabList>
        <FormContainer
          labelFilter={<FormattedMessage id="global.search" />}
          handleFilter={handleFilter}
          disabledFilter={!simpleValidator.current.allValid()}
        >
          <PageFilter>
            <FormControl
              w="176px"
              mr="24px"
              textLabel={
                <FormattedMessage id="company_consumption.start_date" />
              }
            >
              <DatePicker
                name="startAt"
                selected={dateInput.startAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'startAt');
                }}
                selectedsStart
                maxDate={dateInput.endAt}
              />
              {simpleValidator.current.message(
                'startAt',
                dateInput.startAt,
                `required`
              )}
            </FormControl>
            <FormControl
              w="176px"
              textLabel={<FormattedMessage id="company_consumption.end_date" />}
            >
              <DatePicker
                name="endAt"
                selected={dateInput.endAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'endAt');
                }}
                minDate={dateInput.startAt}
              />
              {simpleValidator.current.message(
                'endAt',
                dateInput.endAt,
                `required`
              )}
            </FormControl>
          </PageFilter>
        </FormContainer>
        <PageActions
          onSearch={(search) => setDateInput({ ...dateInput, search })}
        />
        <TabPanels>
          <TabPanel>
            <DataTable data={consumptionsData} allFilters={allFilters} />
          </TabPanel>
          <TabPanel>
            <SmsTable data={consumptionsSms} allFilters={allFilters} />
          </TabPanel>
        </TabPanels>
      </Tabs>
    </>
  );
};

export default CompanyConsumption;
