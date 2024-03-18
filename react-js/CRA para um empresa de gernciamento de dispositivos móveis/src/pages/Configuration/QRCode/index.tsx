import { Box, Checkbox } from '@chakra-ui/react';
import React, { useEffect, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useHistory } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Input from '../../../components/Input';
import PageTitle from '../../../components/PageTitle';
import Select from '../../../components/Select';
import { idioms } from '../../../data/idioms';
import { timestampData } from '../../../data/timestamp';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  genereateQrCode,
  getFormQrCode,
  toggleCheckbox,
} from '../../../store/qrcode';

const QRCode = () => {
  const intl = useIntl();
  const history = useHistory();

  const dispatch = useAppDispatch();
  const { user } = useAppSelector((state) => state.auth);
  const { form } = useAppSelector((state) => state.qrcode);

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(getFormQrCode({ [e.target.name]: e.target.value }));
  };

  const handleToggleCheckbox = () => {
    dispatch(toggleCheckbox());
  };

  const handleInputBlur = (e: React.ChangeEvent<HTMLInputElement>) => {
    simpleValidator.current.showMessageFor(e.target.name);
  };

  const handlePrimary = () => {
    dispatch(genereateQrCode(form));
    history.push(routes.qrcode.show);
  };

  useEffect(() => {
    if (!form.language) {
      dispatch(getFormQrCode({ language: user.language }));
    }
    if (!form.timezone) {
      dispatch(getFormQrCode({ timezone: user.gmt }));
    }
  }, []);
  return (
    <>
      <PageTitle
        title={<FormattedMessage id="qrcode.title" />}
        description={<FormattedMessage id="qrcode.description" />}
      />
      <FormContainer
        labelPrimary={<FormattedMessage id="qrcode.button" />}
        handlePrimary={handlePrimary}
        disabledPrimary={!simpleValidator.current.allValid()}
      >
        <Box d="flex" flexDirection="row">
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.url" />}
          >
            <Input
              inputProps={{
                name: 'url',
                value: form?.url || '',
                onChange: handleInputChange,
                onBlur: handleInputBlur,
              }}
            />
            {simpleValidator.current.message('url', form.url, 'required')}
          </FormControl>
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.package_name" />}
          >
            <Input
              inputProps={{
                name: 'packageName',
                value: form?.packageName || '',
                onChange: handleInputChange,
                onBlur: handleInputBlur,
              }}
            />
            {simpleValidator.current.message(
              'packageName',
              form.packageName,
              'required'
            )}
          </FormControl>
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.language" />}
          >
            <Select
              name="language"
              value={form?.language || ''}
              onChange={handleInputChange}
              onBlur={handleInputBlur}
            >
              {idioms.map((idiom) => (
                <option key={idiom.code} value={idiom.code}>
                  {idiom.text}
                </option>
              ))}
            </Select>
            {simpleValidator.current.message(
              'language',
              form.language,
              'required'
            )}
          </FormControl>
        </Box>
        <Box d="flex" flexDirection="row" mt="1.5%">
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.timezone" />}
          >
            <Select
              name="timezone"
              value={form?.timezone || ''}
              onChange={handleInputChange}
              onBlur={handleInputBlur}
            >
              {timestampData.map((timestamp) => (
                <option key={timestamp.code} value={timestamp.code}>
                  {timestamp.label}
                </option>
              ))}
            </Select>
            {simpleValidator.current.message(
              'timezone',
              form.timezone,
              'required'
            )}
          </FormControl>
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.wifi_network_name" />}
          >
            <Input
              inputProps={{
                name: 'ssid',
                onChange: handleInputChange,
                placeholder: intl.formatMessage({
                  id: 'qrcode.wifi_network_name_label',
                }),
              }}
            />
          </FormControl>
          <FormControl
            mr="24px"
            textLabel={<FormattedMessage id="qrcode.wifi_network_password" />}
          >
            <Input
              inputProps={{
                name: 'ssidPassword',
                onChange: handleInputChange,
                placeholder: intl.formatMessage({
                  id: 'qrcode.wifi_network_name_label',
                }),
              }}
            />
          </FormControl>
        </Box>
        <Box mt="2%">
          <Checkbox
            defaultChecked
            onChange={handleToggleCheckbox}
            color="gray.500"
            colorScheme="green"
          >
            <FormattedMessage id="qrcode.enable_system_apps" />
          </Checkbox>
        </Box>
      </FormContainer>
    </>
  );
};

export default QRCode;
