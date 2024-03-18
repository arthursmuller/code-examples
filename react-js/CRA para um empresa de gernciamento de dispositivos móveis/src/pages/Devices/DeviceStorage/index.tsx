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
import { listDeviceStorage } from '../../../store/deviceInfo';
import { history } from '../../../store/history';
import DeviceStorageChart from './DeviceStorageChart';

const DeviceStorage: React.FC = () => {
  const { storage: storageData } = useAppSelector((state) => state.deviceInfo);
  const [filterStorage, setFilterStorage] = useState<Date>();
  const { id } = useParams<{ id: string }>();
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(listDeviceStorage(parseInt(id), { startAt: filterStorage }));
  }, [filterStorage]);
  const handlePeriodFilterChange = (date: Date) => {
    setFilterStorage(date);
  };
  return (
    <Box>
      <PageTitle
        title={<FormattedMessage id="storage.title" />}
        description={<FormattedMessage id="storage.sub_title" />}
      />
      <FormContainer
        labelSecundary={<FormattedMessage id="global.back" />}
        handleSecundary={() => history.push(routes.device.manage)}
      >
        <PageFilter>
          <FormControl
            w="176px"
            mr="24px"
            textLabel={<FormattedMessage id="storage.label.filter" />}
          >
            <DatePicker
              selected={filterStorage}
              onChange={(e) => {
                handlePeriodFilterChange(e);
              }}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      <Card mt="2%" w="90%">
        <DeviceStorageChart storageData={storageData} />
      </Card>
    </Box>
  );
};

export default DeviceStorage;
