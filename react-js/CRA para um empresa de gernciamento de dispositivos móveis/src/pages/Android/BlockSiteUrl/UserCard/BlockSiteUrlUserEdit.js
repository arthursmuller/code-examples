import {
  Box,
  FormControl,
  FormLabel,
  Divider,
  Button,
  useRadioGroup,
} from '@chakra-ui/react';
import React, { useState } from 'react';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Input from '../../../../components/Input';
import RadioButton from '../../../../components/RadioButton';
import Text from '../../../../components/Text';
import GeofenceTab from './GeofenceTab';
import ScheduleTab from './ScheduleTab';

const BlockSiteUrlUserEdit = () => {
  const [selectedTab, setSelectedTab] = useState('default');
  const [action, setAction] = useState('block');

  const ruleOptions = [
    { id: 'default', label: 'Padrão' },
    { id: 'geofence', label: 'Geofence' },
    { id: 'schedule', label: 'Horário' },
  ];
  const actionOptions = [
    { id: 'block', label: 'Bloquear' },
    { id: 'allow', label: 'Liberar' },
  ];
  const changeRadio = (name) => (value) => {
    if (name == 'rule') {
      setSelectedTab(value);
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

  const renderTab = () => {
    switch (selectedTab) {
      case 'geofence':
        return <GeofenceTab action={action} />;
      case 'schedule':
        return <ScheduleTab action={action} />;
    }
  };

  return (
    <>
      <Box>
        <Heading>Editar regra de sitio</Heading>
        <Text width="90%">
          Configure bloqueios e desbloqueios das aplicações, em Device Owner
          consectetur adipiscing elit, sed do eiusmod tempor incididunt ut
          labore et creates disputes but now you can run test cases in the
          sandbox that create disputes.
        </Text>
      </Box>
      <Card>
        <Box d="flex" flexDirection="column">
          <FormControl w="376px">
            <FormLabel fontSize="sm" color="gray.500">
              Regra aplicada ao usuário
            </FormLabel>
            <Input
              inputProps={{
                placeholder: 'VOSTOK ONE',
                disabled: true,
                _disabled: { bg: 'gray.400', color: '#6e6e78' },
              }}
            />
          </FormControl>
          <FormControl mt="1%" w="376px">
            <FormLabel fontSize="sm" color="gray.500">
              URL/Palabra
            </FormLabel>
            <Input
              inputProps={{
                placeholder: 'Palabra-clave o dominio www.nombredelsitio.com',
                _placeholder: { color: 'gray.300' },
              }}
            />
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
            Cadastrar
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

export default BlockSiteUrlUserEdit;
