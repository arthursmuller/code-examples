import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  useRadioGroup,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Input from '../../../../components/Input';
import RadioButton from '../../../../components/RadioButton';
import Select from '../../../../components/Select';
import Text from '../../../../components/Text';
import { createSubgroup } from '../../../../store/consumptionProfile';

const RegisterSubgroupConsumptionRole = () => {
  const dispatch = useDispatch();
  const { groups } = useSelector((state) => state.consumptionProfile);
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
    dispatch(createSubgroup(form));
  };

  const intl = useIntl();

  const renderGroupsOptions = () => {
    return groups.groups.map((group) => (
      <option value={group.group} key={group.id}>
        {group.group}
      </option>
    ));
  };

  const dataOptions = [
    {
      id: 'data_defined_above',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_defined',
      }),
    },
    {
      id: 'data_ilimited',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_ilimited',
      }),
    },
    {
      id: 'data_consumption',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_assigned',
      }),
    },
  ];
  const smsOptions = [
    {
      id: 'sms_defined_above',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_defined',
      }),
    },
    {
      id: 'sms_ilimited',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_ilimited',
      }),
    },
    {
      id: 'sms_consumption',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_assigned',
      }),
    },
  ];
  const dataRoamingOptions = [
    {
      id: 'data_roaming_defined_above',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_defined',
      }),
    },
    {
      id: 'data_roaming_ilimited',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_ilimited',
      }),
    },
    {
      id: 'data_roaming_consumption',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_assigned',
      }),
    },
  ];
  const smsRoamingOptions = [
    {
      id: 'sms_roaming_defined_above',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_defined',
      }),
    },
    {
      id: 'sms_roaming_ilimited',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_ilimited',
      }),
    },
    {
      id: 'sms_roaming_consumption',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_assigned',
      }),
    },
  ];

  const { getRadioProps } = useRadioGroup({
    name: 'dataOptions',
    defaultValue: 'data_defined_above',
  });
  const { getRadioProps: getRadioProps1 } = useRadioGroup({
    name: 'smsOptions',
    defaultValue: 'sms_defined_above',
  });
  const { getRadioProps: getRadioProps2 } = useRadioGroup({
    name: 'dataRoamingOptions',
    defaultValue: 'data_roaming_defined_above',
  });
  const { getRadioProps: getRadioProps3 } = useRadioGroup({
    name: 'smsRoamingOptions',
    defaultValue: 'sms_roaming_defined_above',
  });

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="consumption_profile.group_register.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="consumption_profile.group_register.description" />
        </Text>
      </Box>
      <Card mt="2%">
        <Box d="flex" flexDirection="column">
          <Box d="flex" flexDirection="row">
            <FormControl w="376px">
              <FormLabel fontSize="sm">
                <FormattedMessage id="global.group" />
              </FormLabel>
              <Select
                placeholder={intl.formatMessage({ id: 'global.select_group' })}
                name="group"
                onChange={handleInputChange}
              >
                {renderGroupsOptions()}
              </Select>
            </FormControl>
          </Box>
          <Text color="gray.300" fontSize="md" as="i" m="60px 0px 10px 0px">
            <FormattedMessage id="consumption_profile.group_register.form_description" />
          </Text>
          <Box d="flex" flexDirection="row">
            {/* PRIMEIRA COLUNA */}
            <Box w="50%" pr="5px">
              <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="global.data" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column">
                <Box d="flex" flexDirection="row">
                  {dataOptions.map((option) => {
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
                <Box d="flex" flexDirection="row" mt="30px">
                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.quantity" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'data_qtd',
                        value: form.data_qtd,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>

                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.bytes" />
                    </FormLabel>
                    <Select />
                  </FormControl>
                </Box>
              </Box>

              <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.data_roaming" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column">
                <Box d="flex" flexDirection="row">
                  {dataRoamingOptions.map((option) => {
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
                <Box d="flex" flexDirection="row" mt="30px">
                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.quantity" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'roaming_data_qtd',
                        value: form.roaming_data_qtd,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>

                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.bytes" />
                    </FormLabel>
                    <Select />
                  </FormControl>
                </Box>
              </Box>
            </Box>

            {/* SEGUNDA COLUNA */}
            <Box w="50%" pl="5px">
              <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.sms" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column">
                <Box d="flex" flexDirection="row">
                  {smsOptions.map((option) => {
                    const radio = getRadioProps1({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
                <Box d="flex" flexDirection="row" mt="30px">
                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.messages" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'sms_qtd',
                        value: form.sms_qtd,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>
                </Box>
              </Box>

              <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.sms_roaming" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column">
                <Box d="flex" flexDirection="row">
                  {smsRoamingOptions.map((option) => {
                    const radio = getRadioProps3({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
                <Box d="flex" flexDirection="row" mt="30px">
                  <FormControl w="176px" mr="24px">
                    <FormLabel d="flex" flexDirection="row" fontSize="sm">
                      <FormattedMessage id="consumption_profile.general.messages" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'roaming_sms_qtd',
                        value: form.roaming_sms_qtd,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>
                </Box>
              </Box>
            </Box>
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
            <FormattedMessage id="global.register" />
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
      </Card>
    </>
  );
};

export default RegisterSubgroupConsumptionRole;
