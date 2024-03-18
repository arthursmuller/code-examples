import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  Checkbox,
  Input,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import InputMask from 'react-input-mask';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../components/Card';
import Select from '../../../components/Select';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import {
  updateGeneral,
  closeGeneralToaster,
} from '../../../store/generalConfig';

const GeneralCard = () => {
  const generalConfig = useSelector((state) => state.generalConfig);
  const dispatch = useDispatch();
  const [form, setForm] = useState({
    company: '',
    cycle_start: '',
    sync_every: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: false,
    gps_lock_outside_work_hours: false,
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: false,
    block_sites: false,
    get_usage_time: false,
    block_url: false,
    total_block_apps: false,
    hotspot: false,
    warning_email: false,
    allow_safe_start: false,
    allow_add_user: false,
  });
  const { showToasterGeneralEdit } = generalConfig;

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSwitchChange = ({ target }) => {
    setForm({ ...form, [target.name]: target.checked });
  };

  const submit = () => {
    dispatch(updateGeneral(form));
  };

  return (
    <>
      <Toaster
        w="100%"
        mt="3%"
        open={showToasterGeneralEdit}
        onClose={() => dispatch(closeGeneralToaster())}
      >
        <FormattedMessage id="general_config.general.toaster" />
      </Toaster>
      <Card w="100%" ml="-1%" mt="2%">
        <Box d="flex" flexDirection="column">
          <Box d="flex" flexDirection="row">
            <FormControl w="100%" mr="24px">
              <FormLabel fontSize="sm" color="gray.300">
                <FormattedMessage id="global.company" />
              </FormLabel>
              <Input
                backgroundColor="white"
                disabled={true}
                placeholder="VOSTOK ONE"
                _disabled={{ bg: 'gray.400' }}
                _placeholder={{ color: 'gray.500' }}
                name="company"
                value={form.company}
                onChange={handleInputChange}
              />
            </FormControl>
            <FormControl w="100%" mr="24px">
              <FormLabel fontSize="sm" color="gray.300">
                <FormattedMessage id="general_config.form.cycle_start" />
              </FormLabel>
              <Select
                name="cycle_start"
                value={form.cycle_start}
                onChange={handleInputChange}
              />
            </FormControl>
            <FormControl w="100%">
              <FormLabel fontSize="sm" color="gray.300">
                <FormattedMessage id="general_config.form.sync_every" />
              </FormLabel>
              <Select
                name="sync_every"
                value={form.sync_every}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.work_config" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="row">
            <FormControl w="100%" mr="24px">
              <FormLabel d="flex" flexDirection="row" fontSize="sm">
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
              <FormLabel d="flex" flexDirection="row" fontSize="sm">
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
            <Box w="100%">
              <Checkbox
                name="lock_outside_work_hours"
                isChecked={form.lock_outside_work_hours}
                onChange={handleSwitchChange}
                mt="5%"
              >
                <FormattedMessage id="general_config.form.lock_outside_work_hours" />
              </Checkbox>
            </Box>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.gps_config" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="column">
            <Box mb="1%">
              <Checkbox
                name="gps_lock_outside_work_hours"
                isChecked={form.gps_lock_outside_work_hours}
                onChange={handleSwitchChange}
              >
                <FormattedMessage id="general_config.form.gps_lock_outside_work_hours" />
              </Checkbox>
            </Box>
            <Box d="flex" flexDirection="row">
              <FormControl w="100%" mr="24px">
                <FormLabel d="flex" flexDirection="row" fontSize="sm">
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
                <FormLabel d="flex" flexDirection="row" fontSize="sm">
                  <FormattedMessage id="general_config.form.gps_precision" />
                </FormLabel>
                <Select
                  name="gps_precision"
                  value={form.gps_precision}
                  onChange={handleInputChange}
                />
              </FormControl>
              <FormControl w="100%" mr="24px">
                <FormLabel d="flex" flexDirection="row" fontSize="sm">
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
            <Checkbox
              name="block_apps"
              isChecked={form.block_apps}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.block_apps" />
            </Checkbox>
            <Checkbox
              name="block_sites"
              isChecked={form.block_sites}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.block_sites" />
            </Checkbox>
            <Checkbox
              name="get_usage_time"
              isChecked={form.get_usage_time}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.get_usage_time" />
            </Checkbox>
          </Box>
          <Text m="3% 0% 1% 0%" fontSize="md" fontWeight="600">
            <FormattedMessage id="general_config.form.distinct_configs" />
          </Text>
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
          <Box d="flex" flexDirection="column">
            <Box d="flex" flexDirection="row">
              <Checkbox
                name="block_url"
                isChecked={form.block_url}
                onChange={handleSwitchChange}
                w="100%"
              >
                <FormattedMessage id="general_config.form.block_url" />
              </Checkbox>
              <Checkbox
                name="total_block_apps"
                isChecked={form.total_block_apps}
                onChange={handleSwitchChange}
                w="100%"
              >
                <FormattedMessage id="general_config.form.total_block_apps" />
              </Checkbox>
              <Checkbox
                name="hotspot"
                isChecked={form.hotspot}
                onChange={handleSwitchChange}
                w="100%"
              >
                <FormattedMessage id="general_config.form.hotspot" />
              </Checkbox>
            </Box>
            <Box mt="15px" d="flex" flexDirection="row">
              <Checkbox
                name="warning_email"
                isChecked={form.warning_email}
                onChange={handleSwitchChange}
                w="100%"
              >
                <FormattedMessage id="general_config.form.warning_email" />
              </Checkbox>
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
            <Checkbox
              name="allow_safe_start"
              isChecked={form.allow_safe_start}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.allow_safe_start" />
            </Checkbox>
            <Checkbox
              name="allow_add_user"
              isChecked={form.allow_add_user}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.allow_add_user" />
            </Checkbox>
            <Checkbox
              name="allow_add_user"
              isChecked={form.allow_add_user}
              onChange={handleSwitchChange}
              w="100%"
            >
              <FormattedMessage id="general_config.form.allow_add_user" />
            </Checkbox>
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
      </Card>
    </>
  );
};

export default GeneralCard;
