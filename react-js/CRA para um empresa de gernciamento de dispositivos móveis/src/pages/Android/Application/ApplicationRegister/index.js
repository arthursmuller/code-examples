import { Box, Button, Divider, FormControl, FormLabel } from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Input from '../../../../components/Input';
import Text from '../../../../components/Text';
import { createApplication } from '../../../../store/application';

const ApplicationRegister = () => {
  const dispatch = useDispatch();
  const { applications } = useSelector((state) => state.application);
  const [form, setForm] = useState({
    url: '',
    package: '',
  });

  const handleFormChange = ({ target }) => {
    setForm({ ...form, [target.name]: target.value });
  };

  const handleSubmit = () => {
    dispatch(createApplication(form));
  };

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="application_register.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="application_register.title_text" />
        </Text>
      </Box>
      <Card>
        <Box d="flex" flexDirection="column">
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="application_register.url_label" />
            </FormLabel>
            <Input
              inputProps={{
                value: form.url,
                onChange: handleFormChange,
                name: 'url',
                placeholder: 'http://127.0.0.1/my_app.apk',
              }}
            />
          </FormControl>
          <FormControl w="100%" mr="24px" mt="30px" mb="20px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="application_register.package_label" />
            </FormLabel>
            <Input
              inputProps={{
                value: form.package,
                onChange: handleFormChange,
                name: 'package',
                placeholder: 'com.mycompany.app',
              }}
            />
          </FormControl>
        </Box>
        <Box mt="2%">
          <Divider orientation="horizontal" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Button
            bg="blue.500"
            color="white"
            h="45px"
            w="176px"
            onClick={handleSubmit}
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

export default ApplicationRegister;
