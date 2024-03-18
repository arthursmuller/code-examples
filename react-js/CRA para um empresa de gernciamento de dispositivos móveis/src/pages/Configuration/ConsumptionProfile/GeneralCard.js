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

import Card from '../../../components/Card';
import Input from '../../../components/Input';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import { closeGeneralToaster } from '../../../store/consumptionProfile';
import { updateGeneral } from '../../../store/consumptionProfile';

const GeneralCard = ({ general }) => {
  const [selectedData, setSelectedData] = useState(null);
  const [selectedSms, setSelectedSms] = useState(null);
  const [selectedDataRoaming, setSelectedDataRoaming] = useState(null);
  const [selectedSmsRoaming, setSelectedSmsRoaming] = useState(null);
  const { showToaster, general: generalObj } = general;
  const dispatch = useDispatch();
  const [form, setForm] = useState(generalObj);

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = () => {
    dispatch(updateGeneral(form));
  };

  const intl = useIntl();

  const dataOptions = [
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

  const changeData = (value) => {
    setSelectedData(value);
  };
  const changeSms = (value) => {
    setSelectedSms(value);
  };
  const changeDataRoaming = (value) => {
    setSelectedDataRoaming(value);
  };
  const changeSmsRoaming = (value) => {
    setSelectedSmsRoaming(value);
  };

  const { getRadioProps } = useRadioGroup({
    name: 'dataOptions',
    onChange: changeData,
  });
  const { getRadioProps: getRadioProps1 } = useRadioGroup({
    name: 'smsOptions',
    onChange: changeSms,
  });
  const { getRadioProps: getRadioProps2 } = useRadioGroup({
    name: 'dataRoamingOptions',
    onChange: changeDataRoaming,
  });
  const { getRadioProps: getRadioProps3 } = useRadioGroup({
    name: 'smsRoamingOptions',
    onChange: changeSmsRoaming,
  });
  return (
    <>
      <Box mt="2%" w="100%">
        <Toaster
          w="100%"
          open={showToaster}
          onClose={() => dispatch(closeGeneralToaster())}
        >
          <FormattedMessage id="consumption_profile.success" />
        </Toaster>
      </Box>
      <Card w="100%" mt="2%">
        <Box d="flex" flexDirection="column">
          <Box d="flex" flexDirection="row">
            <FormControl w="376px">
              <FormLabel fontSize="sm" color="gray.300">
                <FormattedMessage id="global.company" />
              </FormLabel>
              <Input
                inputProps={{
                  backgroundColor: 'white',
                  disabled: true,
                  placeholder: 'VOSTOK ONE',
                  _disabled: { bg: 'gray.400' },
                  _placeholder: { color: 'gray.500' },
                }}
              />
            </FormControl>
          </Box>
          <Text fontSize="md" color="gray.500" as="i">
            <FormattedMessage id="consumption_profile.general.description" />
          </Text>
          <Box d="flex" flexDirection="row">
            {/* PRIMEIRA COLUNA */}
            <Box w="50%" pr="5px">
              <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.data" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column" mt="4%">
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
                  <FormControl
                    w="176px"
                    mr="24px"
                    isDisabled={selectedData !== 'data_consumption'}
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.quantity" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'dataQuantity',
                        value: form.dataQuantity,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>

                  <FormControl
                    w="176px"
                    isDisabled={selectedData !== 'data_consumption'}
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.bytes" />
                    </FormLabel>
                    <Select name="dataBytes" onChange={handleInputChange}>
                      <option value="KB" selected={form.dataBytes === 'KB'}>
                        KB
                      </option>
                      <option value="KB" selected={form.dataBytes === 'MB'}>
                        MB
                      </option>
                      <option value="GB" selected={form.dataBytes === 'GB'}>
                        GB
                      </option>
                    </Select>
                  </FormControl>
                </Box>
              </Box>

              <Text m="5% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.data_roaming" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column" mt="4%">
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
                  <FormControl
                    w="176px"
                    mr="24px"
                    isDisabled={
                      selectedDataRoaming !== 'data_roaming_consumption'
                    }
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.quantity" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'dataRoamingQuantity',
                        value: form.dataRoamingQuantity,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>

                  <FormControl
                    w="176px"
                    isDisabled={
                      selectedDataRoaming !== 'data_roaming_consumption'
                    }
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.bytes" />
                    </FormLabel>
                    <Select
                      name="dataRoamingBytes"
                      value={form.dataRoamingBytes}
                      onChange={handleInputChange}
                    >
                      <option value="KB">KB</option>
                      <option value="MB">MB</option>
                      <option value="GB">GB</option>
                    </Select>
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
              <Box d="flex" flexDirection="column" mt="4%">
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
                  <FormControl
                    w="176px"
                    isDisabled={selectedSms !== 'sms_consumption'}
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.messages" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'smsMessages',
                        value: form.smsMessages,
                        onChange: handleInputChange,
                      }}
                    />
                  </FormControl>
                </Box>
              </Box>

              <Text m="5% 0% 1% 0%" fontSize="md" fontWeight="600">
                <FormattedMessage id="consumption_profile.general.sms_roaming" />
              </Text>
              <Divider
                borderColor="gray.600"
                orientation="horizontal"
                mb="1.5%"
              />
              <Box d="flex" flexDirection="column" mt="4%">
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
                  <FormControl
                    w="176px"
                    isDisabled={
                      selectedSmsRoaming !== 'sms_roaming_consumption'
                    }
                  >
                    <FormLabel
                      color="gray.500"
                      d="flex"
                      flexDirection="row"
                      fontSize="sm"
                    >
                      <FormattedMessage id="consumption_profile.general.messages" />
                    </FormLabel>
                    <Input
                      inputProps={{
                        name: 'smsRoamingMessages',
                        value: form.smsRoamingMessages,
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
            <FormattedMessage id="global.update" />
          </Button>
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
          <Button variant="ghost" color="blue.500">
            <FormattedMessage id="global.close" />
          </Button>
        </Box>
      </Card>
    </>
  );
};

export default GeneralCard;
