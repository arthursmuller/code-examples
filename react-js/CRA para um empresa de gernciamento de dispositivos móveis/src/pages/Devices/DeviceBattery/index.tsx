import { Box } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { useParams } from 'react-router-dom';

import Card from '../../../components/Card';
import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageFilter from '../../../components/PageFilter';
import PageTitle from '../../../components/PageTitle';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { listDeviceBattery } from '../../../store/deviceInfo';
import { history } from '../../../store/history';
import DeviceBatteryChart from './DeviceBatteryChart';

const DeviceBattery: React.FC = () => {
  const { battery: batteryData } = useAppSelector((state) => state.deviceInfo);
  const [filterBattery, setFilterBattery] = useState<Date>();
  const { id } = useParams<{ id: string }>();
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(listDeviceBattery(parseInt(id), { startAt: filterBattery }));
  }, [filterBattery]);
  const handlePeriodFilterChange = (date: Date) => {
    setFilterBattery(date);
  };

  return (
    <Box>
      <PageTitle
        title={<FormattedMessage id="battery.title" />}
        description={<FormattedMessage id="battery.sub_title" />}
      />
      <FormContainer
        labelSecundary={<FormattedMessage id="global.back" />}
        handleSecundary={() => history.push(routes.device.manage)}
      >
        <PageFilter>
          <FormControl
            w="176px"
            mr="24px"
            textLabel={<FormattedMessage id="battery.label.filter" />}
          >
            <DatePicker
              selected={filterBattery}
              onChange={(e) => {
                handlePeriodFilterChange(e);
              }}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      <Card mt="2%" w="90%">
        <DeviceBatteryChart batteryData={batteryData} />
      </Card>
    </Box>
  );
};

export default DeviceBattery;
