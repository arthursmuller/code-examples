import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  useRadioGroup,
  Input,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import InputMask from 'react-input-mask';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';

import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import Text from '../../../components/Text';
import { updateGroup } from '../../../store/generalConfig';
import { history } from '../../../store/history';

const GeneralConfigGroupEdit = () => {
  const intl = useIntl();
  const dispatch = useDispatch();
  const [form, setForm] = useState({
    group: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: '',
    track_gps: '',
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: '',
    block_sites: '',
    get_usage_time: '',
    block_url: '',
    total_block_apps: '',
    hotspot: '',
    warning_email: '',
    allow_safe_start: '',
    allow_add_user: '',
    allow_sd_card: '',
  });

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const changeRadio = (name) => (value) => {
    setForm({ ...form, [name]: value });
  };

  const submit = () => {
    dispatch(updateGroup(form));
    history.push('/general-config');
  };

  const options = [
    {
      id: 'existing',
      label: intl.formatMessage({
        id: 'consumption_profile.radio_button_defined',
      }),
    },
    { id: 'yes', label: intl.formatMessage({ id: 'global.radiogroup_yes' }) },
    { id: 'no', label: intl.formatMessage({ id: 'global.radiogroup_no' }) },
  ];

  const { getRadioProps } = useRadioGroup({
    defaultValue: 'existing',
    name: 'lock_outside_work_hours',
    onChange: changeRadio('lock_outside_work_hours'),
  });
  const { getRadioProps: getRadioProps1 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'track_gps',
    onChange: changeRadio('track_gps'),
  });
  const { getRadioProps: getRadioProps2 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'block_apps',
    onChange: changeRadio('block_apps'),
  });
  const { getRadioProps: getRadioProps3 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'block_sites',
    onChange: changeRadio('block_sites'),
  });
  const { getRadioProps: getRadioProps4 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'get_usage_time',
    onChange: changeRadio('get_usage_time'),
  });
  const { getRadioProps: getRadioProps5 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'block_url',
    onChange: changeRadio('block_url'),
  });
  const { getRadioProps: getRadioProps6 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'total_block_apps',
    onChange: changeRadio('total_block_apps'),
  });
  const { getRadioProps: getRadioProps7 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'hotspot',
    onChange: changeRadio('hotspot'),
  });
  const { getRadioProps: getRadioProps8 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'warning_email',
    onChange: changeRadio('warning_email'),
  });
  const { getRadioProps: getRadioProps9 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'allow_safe_start',
    onChange: changeRadio('allow_safe_start'),
  });
  const { getRadioProps: getRadioProps10 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'allow_add_user',
    onChange: changeRadio('allow_add_user'),
  });
  const { getRadioProps: getRadioProps11 } = useRadioGroup({
    defaultValue: 'existing',
    name: 'allow_sd_card',
    onChange: changeRadio('allow_sd_card'),
  });
  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="general_config.edit.group.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="general_config.edit.description" />
        </Text>
      </Box>
      <Card mt="2%">
        <Box d="flex" flexDirection="column">
          <Box d="flex" flexDirection="row">
            <FormControl w="376px">
              <FormLabel color="gray.500" fontSize="sm">
                <FormattedMessage id="global.group" />
              </FormLabel>
              <Input
                backgroundColor="white"
                disabled={true}
                placeholder="VOSTOK ONE"
                _disabled={{ bg: 'gray.400' }}
                _placeholder={{ color: 'gray.500' }}
                name="group"
                value={form.group}
              />
            </FormControl>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.work_config" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="row">
            <FormControl w="100%" mr="24px">
              <FormLabel
                color="gray.500"
                d="flex"
                flexDirection="row"
                fontSize="sm"
              >
                <FormattedMessage id="general_config.form.week_days" />
                <Text m="0px 0px 0px 10px">
                  <FormattedMessage id="general_config.form.optional" />
                </Text>
              </FormLabel>
              <Box d="flex" flexDirection="row">
                <Select
                  name="week_start"
                  value={form.week_start}
                  onChange={handleInputChange}
                />
                <Text m="10px 20px 0px 20px">
                  <FormattedMessage id="general_config.form.until" />
                </Text>
                <Select
                  name="week_end"
                  value={form.week_end}
                  onChange={handleInputChange}
                />
              </Box>
            </FormControl>
            <FormControl w="100%" mr="24px">
              <FormLabel
                color="gray.500"
                d="flex"
                flexDirection="row"
                fontSize="sm"
              >
                <FormattedMessage id="general_config.form.work_hour" />
                <Text m="0px 0px 0px 10px">
                  <FormattedMessage id="general_config.form.optional" />
                </Text>
              </FormLabel>
              <Box d="flex" flexDirection="row">
                <Input
                  as={InputMask}
                  borderColor="gray.600"
                  mask="99:99"
                  maskChar={null}
                  name="work_hour_start"
                  value={form.work_hour_start}
                  onChange={handleInputChange}
                />
                <Text m="10px 20px 0px 20px">
                  <FormattedMessage id="general_config.form.until" />
                </Text>
                <Input
                  as={InputMask}
                  borderColor="gray.600"
                  mask="99:99"
                  maskChar={null}
                  name="work_hour_end"
                  value={form.work_hour_end}
                  onChange={handleInputChange}
                />
              </Box>
            </FormControl>
            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 1% 0">
                <FormattedMessage id="general_config.form.lock_outside_work_hours" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
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
            </Box>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.gps_config" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="column">
            <Box mb="1%">
              <Box w="100%" d="flex" flexDirection="column" alignItems="top">
                <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                  <FormattedMessage id="general_config.edit.group.track_gps" />
                </Text>

                <Box d="flex" flexDirection="row" w="100%">
                  {options.map((option) => {
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
              </Box>
            </Box>
            <Box d="flex" flexDirection="row">
              <FormControl w="100%" mr="24px">
                <FormLabel
                  color="gray.500"
                  d="flex"
                  flexDirection="row"
                  fontSize="sm"
                >
                  <FormattedMessage id="general_config.form.gps_hour" />
                  <Text m="0px 0px 0px 10px">
                    <FormattedMessage id="general_config.form.optional" />
                  </Text>
                </FormLabel>
                <Box d="flex" flexDirection="row">
                  <Input
                    as={InputMask}
                    borderColor="gray.600"
                    mask="99:99"
                    maskChar={null}
                    name="gps_hour_start"
                    value={form.gps_hour_start}
                    onChange={handleInputChange}
                  />
                  <Text m="10px 20px 0px 20px">
                    <FormattedMessage id="general_config.form.until" />
                  </Text>
                  <Input
                    as={InputMask}
                    borderColor="gray.600"
                    mask="99:99"
                    maskChar={null}
                    name="gps_hour_end"
                    value={form.gps_hour_end}
                    onChange={handleInputChange}
                  />
                </Box>
              </FormControl>
              <FormControl w="100%" mr="24px">
                <FormLabel
                  color="gray.500"
                  d="flex"
                  flexDirection="row"
                  fontSize="sm"
                >
                  <FormattedMessage id="general_config.form.gps_precision" />
                </FormLabel>
                <Select
                  name="gps_precision"
                  value={form.gps_precision}
                  onChange={handleInputChange}
                />
              </FormControl>
              <FormControl w="100%" mr="24px">
                <FormLabel
                  color="gray.500"
                  d="flex"
                  flexDirection="row"
                  fontSize="sm"
                >
                  <FormattedMessage id="general_config.form.locate_every" />
                </FormLabel>
                <Select
                  name="locate_every"
                  value={form.locate_every}
                  onChange={handleInputChange}
                />
              </FormControl>
            </Box>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.wifi_config" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="row">
            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.block_apps" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
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
            </Box>

            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.block_sites" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
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
            </Box>

            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.get_usage_time" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
                  const radio = getRadioProps4({
                    value: option.id,
                  });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
              </Box>
            </Box>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.distinct_configs" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="column">
            <Box d="flex" flexDirection="row">
              <Box w="100%" d="flex" flexDirection="column" alignItems="top">
                <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                  <FormattedMessage id="general_config.form.block_url" />
                </Text>

                <Box d="flex" flexDirection="row" w="100%">
                  {options.map((option) => {
                    const radio = getRadioProps5({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
              </Box>

              <Box w="100%" d="flex" flexDirection="column" alignItems="top">
                <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                  <FormattedMessage id="general_config.form.total_block_apps" />
                </Text>

                <Box d="flex" flexDirection="row" w="100%">
                  {options.map((option) => {
                    const radio = getRadioProps6({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
              </Box>

              <Box w="100%" d="flex" flexDirection="column" alignItems="top">
                <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                  <FormattedMessage id="general_config.form.hotspot" />
                </Text>

                <Box d="flex" flexDirection="row" w="100%">
                  {options.map((option) => {
                    const radio = getRadioProps7({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
              </Box>
            </Box>
            <Box mt="15px" d="flex" flexDirection="row">
              <Box w="100%" d="flex" flexDirection="column" alignItems="top">
                <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                  <FormattedMessage id="general_config.form.warning_email" />
                </Text>

                <Box d="flex" flexDirection="row" w="100%">
                  {options.map((option) => {
                    const radio = getRadioProps8({
                      value: option.id,
                    });
                    return (
                      <RadioButton key={option.id} {...radio}>
                        {option.label}
                      </RadioButton>
                    );
                  })}
                </Box>
              </Box>
            </Box>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.device_owner_allowed" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1%" />
          <Text
            m="0"
            mb="1%"
            as="i"
            color="gray.300"
            fontSize="xs"
            fontWeight="400"
          >
            <FormattedMessage id="general_config.form.device_owner_allowed_description" />
          </Text>
          <Box d="flex" flexDirection="row">
            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.allow_safe_start" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
                  const radio = getRadioProps9({
                    value: option.id,
                  });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
              </Box>
            </Box>

            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.allow_add_user" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
                  const radio = getRadioProps10({
                    value: option.id,
                  });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
              </Box>
            </Box>

            <Box w="100%" d="flex" flexDirection="column" alignItems="top">
              <Text color="gray.500" fontSize="sm" m="0 0 10px 0">
                <FormattedMessage id="general_config.form.sd_card_install" />
              </Text>

              <Box d="flex" flexDirection="row" w="100%">
                {options.map((option) => {
                  const radio = getRadioProps11({
                    value: option.id,
                  });
                  return (
                    <RadioButton key={option.id} {...radio}>
                      {option.label}
                    </RadioButton>
                  );
                })}
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
          <Link to="/general-config">
            <Button variant="ghost" color="blue.500">
              <FormattedMessage id="global.cancel" />
            </Button>
          </Link>
        </Box>
      </Card>
    </>
  );
};

export default GeneralConfigGroupEdit;
