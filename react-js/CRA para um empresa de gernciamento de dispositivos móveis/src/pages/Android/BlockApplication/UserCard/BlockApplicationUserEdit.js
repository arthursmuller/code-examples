import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  useRadioGroup,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Input from '../../../../components/Input';
import RadioButton from '../../../../components/RadioButton';
import Select from '../../../../components/Select';
import Text from '../../../../components/Text';
// import { createGroup } from '../../../../store/application';
import GeofenceTab from './GeofenceTab';
import ScheduleTab from './ScheduleTab';

const EditUserRule = () => {
  const dispatch = useDispatch();
  const { general } = useSelector((state) => state.application);
  const [form, setForm] = useState({
    group: '',
    data_qtd: '',
    roaming_data_qtd: '',
    sms_qtd: '',
    roaming_sms_qtd: '',
  });

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = () => {
    // dispatch(createGroup(form));
  };

  const AppRadio = () => {
    return (
      <>
        <Select
          ml="24px"
          w="376px"
          placeholder="Selecione o aplicativo"
          color="gray.300"
        />
      </>
    );
  };

  const PackageNameRadio = () => {
    return (
      <>
        <Input
          inputProps={{
            ml: '24px',
            w: '376px',
            placeholder: 'Ex.: com.whatsapp',
            _placeholder: { color: 'gray.300' },
          }}
        />
      </>
    );
  };

  const [selectedTab, setSelectedTab] = useState('default');
  const [action, setAction] = useState('block');
  const [selectedApply, setselectedApply] = useState('app');

  const ruleOptions = [
    { id: 'default', label: 'Padrão' },
    { id: 'geofence', label: 'Geofence' },
    { id: 'schedule', label: 'Horário' },
  ];
  const actionOptions = [
    { id: 'block', label: 'Bloquear' },
    { id: 'allow', label: 'Liberar' },
  ];
  const applyOptions = [
    { id: 'app', label: 'Aplicativo' },
    { id: 'package_name', label: 'Nombre del paquete' },
  ];

  const changeRadio = (name) => (value) => {
    if (name == 'rule') {
      setSelectedTab(value);
    } else if (name == 'apply') {
      setselectedApply(value);
    } else {
      setAction(value);
    }
  };

  const { getRadioProps } = useRadioGroup({
    name: 'ruleOptions',
    defaultValue: 'default',
    onChange: changeRadio('rule'),
  });
  const { getRadioProps: getRadioProps1 } = useRadioGroup({
    name: 'actionOptions',
    defaultValue: 'block',
    onChange: changeRadio('action'),
  });
  const { getRadioProps: getRadioProps2 } = useRadioGroup({
    name: 'applyOptions',
    defaultValue: 'app',
    onChange: changeRadio('apply'),
  });

  const renderTab = () => {
    switch (selectedTab) {
      case 'geofence':
        return <GeofenceTab action={action} />;
      case 'schedule':
        return <ScheduleTab action={action} />;
    }
  };

  const renderApplyTo = () => {
    switch (selectedApply) {
      case 'app':
        return <AppRadio />;
      case 'package_name':
        return <PackageNameRadio />;
    }
  };

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="block_application.general.edit.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="block_application.general.register.title_text" />
        </Text>
      </Box>
      <Card>
        <Box d="flex" flexDirection="column">
          <FormControl w="376px">
            <FormLabel fontSize="sm" color="gray.500">
              Regra aplicada ao usuário
            </FormLabel>
            <Select placeholder="Selecione o usuário" color="gray.300" />
          </FormControl>
          <FormControl mt="1%" w="100%">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="block_application.apply_to" />
            </FormLabel>
            <Box d="flex" flexDirection="row">
              <Box d="flex" flexDirection="row">
                {applyOptions.map((option) => {
                  const radio = getRadioProps2({
                    value: option.id,
                  });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
              </Box>
              <Box>{renderApplyTo()}</Box>
            </Box>
          </FormControl>
        </Box>
        <Box mt="3%">
          <Text m="0% 0% 1% 0%" fontWeight="600">
            Configurar regra
          </Text>
          <Divider orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="row">
            <FormControl w="100%">
              <FormLabel fontSize="sm" color="gray.500">
                Tipo de regra
              </FormLabel>
              <Box d="flex" flexDirection="row">
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
            </FormControl>
            <FormControl w="100%" ml="25px">
              <FormLabel fontSize="sm" color="gray.500">
                Selecione a ação
              </FormLabel>
              <Box d="flex" flexDirection="row">
                {actionOptions.map((option) => {
                  const radio = getRadioProps1({ value: option.id });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
              </Box>
            </FormControl>
          </Box>
        </Box>
        <Box>{renderTab()}</Box>
        <Box mt="3%">
          <Divider orientation="horizontal" borderColor="gray.600" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Button bg="blue.500" color="white" h="45px" w="176px">
            Atualizar
          </Button>
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
          <Button variant="ghost" color="blue.500">
            Cancelar
          </Button>
        </Box>
      </Card>
    </>
  );
};

export default EditUserRule;
