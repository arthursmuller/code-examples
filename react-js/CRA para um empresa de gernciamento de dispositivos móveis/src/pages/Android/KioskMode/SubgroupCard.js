import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  useRadioGroup,
  Checkbox,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../components/Card';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import {
  kioskUpdateSubgroup,
  kioskCloseSubgroupToaster,
} from '../../../store/site';

const SubgroupCard = () => {
  const intl = useIntl();
  const dispatch = useDispatch();
  const { kiosk_mode } = useSelector((state) => state.site);
  const { showToaster: toaster, subgroups } = kiosk_mode;
  const [kioskActions, setKioskActions] = useState(subgroups);

  const handleCheckboxChange = ({ target }) => {
    setKioskActions({ ...kioskActions, [target.name]: target.checked });
  };

  const submit = () => {
    dispatch(kioskUpdateSubgroup(kioskActions));
  };

  const switches1 = React.useMemo(() => [
    'accessibility',
    'actions_moto',
    'software_update',
    'software_update',
    'administrador',
    'schedule',
    'settings',
    'app_update',
    'settings',
    'settings',
    'settings',
  ]);
  const switches2 = React.useMemo(() => [
    'notepad',
    'bubble_bash',
    'calculator',
    'calendar',
    'capture',
    'catalog',
    'update_center',
    'subscription_center',
    'chrome',
    'chrome',
    'chrome',
  ]);

  const Active = () => {
    return (
      <>
        <Text m="2% 0% 0% 0%" fontWeight="600">
          <FormattedMessage id="sites.kiosk.general.permited_actions" />
        </Text>
        <Box mt="1%">
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
        </Box>
        <Box w="376px">
          <Input
            inputProps={{ backgroundColor: 'white' }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        </Box>
        <Box mt="2%" d="flex" flexDirection="row">
          <Box d="flex" flexDirection="column" w="100%">
            {switches1.map(function (item, i) {
              return (
                <Checkbox
                  key={i}
                  isChecked={kioskActions[item]}
                  name={item}
                  onChange={handleCheckboxChange}
                  fontSize="sm"
                  color="gray.500"
                >
                  <FormattedMessage id={`sites.kiosk.general.${item}`} />
                </Checkbox>
              );
            })}
          </Box>
          <Box d="flex" flexDirection="column" w="100%">
            {switches2.map(function (item, i) {
              return (
                <Checkbox
                  key={i}
                  isChecked={kioskActions[item]}
                  name={item}
                  onChange={handleCheckboxChange}
                  fontSize="sm"
                  color="gray.500"
                >
                  <FormattedMessage id={`sites.kiosk.general.${item}`} />
                </Checkbox>
              );
            })}
          </Box>
        </Box>
        <Box mt="3%">
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Button
            bg="blue.500"
            color="white"
            h="45px"
            w="176px"
            onClick={submit}
          >
            <FormattedMessage id="global.update" />
          </Button>
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
          <Button variant="ghost" color="blue.500">
            <FormattedMessage id="global.cancel" />
          </Button>
        </Box>
      </>
    );
  };
  const [selectedTab, setSelectedTab] = useState('defined');

  const changeRadio = (value) => {
    setSelectedTab(value);
  };

  const stateOptions = [
    {
      id: 'defined',
      label: intl.formatMessage({ id: 'sites.block.radio_button.defined' }),
    },
    {
      id: 'disabled',
      label: intl.formatMessage({ id: 'sites.block.radio_button.disabled' }),
    },
    {
      id: 'active',
      label: intl.formatMessage({ id: 'sites.block.radio_button.active' }),
    },
  ];

  const { getRadioProps } = useRadioGroup({
    name: 'stateOptions',
    defaultValue: 'defined',
    onChange: changeRadio,
  });

  const renderTab = () => {
    switch (selectedTab) {
      case 'defined':
        return <></>;
      case 'disabled':
        return <></>;
      case 'active':
        return <Active />;
    }
  };
  return (
    <>
      <Toaster
        w="100%"
        ml="-1%"
        mt="2%"
        open={subgroups.showToaster}
        onClose={() => dispatch(kioskCloseSubgroupToaster())}
      >
        <FormattedMessage id="sites.kiosk.general.success" />
      </Toaster>
      <Card w="100%" ml="-1%" mt="2%">
        <Box d="flex" flexDirection="column">
          <FormControl w="520px">
            <FormLabel fontSize="sm" color="gray.300">
              <FormattedMessage id="sites.block.subgroup.form_label" />
            </FormLabel>
            <Select placeholder="MOTO G play 7 android 10 v142 - 5551983436815" />
          </FormControl>
          <FormControl mt="2%" w="376px">
            <FormLabel fontSize="sm" color="gray.300">
              <FormattedMessage id="sites.kiosk.kiosk_state" />
            </FormLabel>
            <Box d="flex" flexDirection="row">
              {stateOptions.map((option) => {
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
          {renderTab()}
        </Box>
      </Card>
    </>
  );
};

export default SubgroupCard;
