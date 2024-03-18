import { Box } from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';

import DatePicker from '../../components/Datepicker';
import FormContainer from '../../components/FormContainer';
import FormControl from '../../components/FormControl';
import PageFilter from '../../components/PageFilter';
import GeolocationMap from '../../components/pages/Geolocation/GeolocationMap';
import PageTitle from '../../components/PageTitle';
import Select from '../../components/Select';
import SelectAutocomplete from '../../components/SelectAutocomplete';
import WarningIfExitRoute from '../../components/WarningIfExitRoute';
import { useAppDispatch, useAppSelector } from '../../hooks/useRedux';
import { listDeviceUserToFilter } from '../../store/deviceUser';
import { DeviceUserType } from '../../types/deviceUser';


const Geolocation = () => {
  const dispatch = useAppDispatch();
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [form, setForm] = useState({} as any);

  const handleFilterChange = (field) => (value) => {
    setForm({ ...form, [field]: value });
  };

  const submit = () => {
    // dispatch(filterUpdate(form));
  };

  const handleFilterDeviceUser = (newFilter: DeviceUserType) => {
    setForm({ ...form, deviceUser: newFilter });
    dispatch(listDeviceUserToFilter());
  };
  const handleFilterDeviceUserInput = (search: string) => {
    dispatch(listDeviceUserToFilter({ search }));
  };

  const mock = [
    {
      name: 'currentPosition',
      lat: -32.093154,
      lng: -52.758082,
      infos: {
        date: '2022-02-12 22:47:51',
        state: 'Parado 102min',
        precision: '23',
        battery: '23',
      },
    },
    {
      name: 'initialPosition',
      lat: -29.758897,
      lng: -51.091057,
      infos: {
        date: '2022-02-12 22:47:51',
        state: 'Parado 136min',
        precision: '22',
        battery: '71',
      },
    },
    {
      name: 'lastPosition',
      lat: -31.093154,
      lng: -50.758082,
      infos: {
        date: '2022-02-12 22:47:51',
        state: 'Em movimento',
        precision: '12',
        battery: '54',
      },
    }
  ]

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="geolocation.title" />}
        description={<FormattedMessage id="geolocation.title_text" />}
      />
      <WarningIfExitRoute preventExit={false} message='Está procurando a localização em tempo real. Não saia da tela!' />

      <FormContainer
        labelFilter={<FormattedMessage id="geolocation.search" />}
        handleFilter={submit}
      >
        <PageFilter>
          <FormControl
            flex="1"
            textLabel={<FormattedMessage id="geolocation.user" />}
          >
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={form.deviceUser}
              onChange={handleFilterDeviceUser}
              onInputChange={handleFilterDeviceUserInput}
              getOptionLabel={(option) =>
                `${option.name || ''} ${option.phoneNumber}`
              }
            />
          </FormControl>
          <FormControl
            flex="0"
            minWidth="130px"
            textLabel={<FormattedMessage id="geolocation.findBy" />}
          >
            <Select placeholder="" backgroundColor="white">
              <option value="Por Data">Por Data</option>
              <option value="Agora">Agora</option>
            </Select>
          </FormControl>
          <FormControl
            flex="0"
            minWidth="160px"
            textLabel={<FormattedMessage id="geolocation.date" />}
          >
            <DatePicker
              selected={form.date}
              onChange={handleFilterChange('date')}
            />
          </FormControl>
          <FormControl
            flex="0"
            minWidth="160px"
            textLabel={<FormattedMessage id="geolocation.timezone" />}
          >
            <Select placeholder="" backgroundColor="white">
              <option value="Usuário atual">Usuário atual</option>
              <option value="Dispositivo">Dispositivo</option>
            </Select>
          </FormControl>
          <FormControl
            flex="0"
            minWidth="130px"
            textLabel={<FormattedMessage id="geolocation.precision" />}
          >
            <Select placeholder="">
              <option value="Todas">Todas</option>
              <option value="200">200</option>
              <option value="300">300</option>
              <option value="400">400</option>
              <option value="500">500</option>
              <option value="600">600</option>
              <option value="700">700</option>
              <option value="800">800</option>
              <option value="900">900</option>
              <option value="1000">1000</option>
            </Select>
          </FormControl>
        </PageFilter>
      </FormContainer>

      <Box w="90%" d="flex" flexDirection="column">
        <GeolocationMap data={mock} />
      </Box>
    </>
  );
};

export default Geolocation;
