import { CloseIcon } from '@chakra-ui/icons';
import { Box, Divider, FormControl, FormLabel, Input } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import InputMask from 'react-input-mask';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Button from '../../../components/Button';
import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Checkmark from '../../../components/Icons/Checkmark';
import Select from '../../../components/Select';
import Text from '../../../components/Text';
import { showToaster } from '../../../store/company';
// import { listStates } from '../../../store/state/index.ts old';

const CompanyInfo = () => {
  const dispatch = useDispatch();
  const company = useSelector((state) => state.company);
  const states = useSelector((state) => state.states);

  const { showToaster: toaster, errors } = company;
  const [form, setForm] = useState({
    name: '',
    identification: '',
    email: '',
    contact: '',
    phone: '',
    zip_code: '',
    address_line: '',
    city: '',
    state: '',
  });

  useEffect(() => {
    // dispatch(listStates());
  }, []);

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = () => {
    // dispatch(createCompany(form));
  };

  const renderStatesOptions = () => {
    return states.states.map((state) => (
      <option value={state.label} key={state.id}>
        {state.label}
      </option>
    ));
  };

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="company_info.title" />
        </Heading>
        <Text w="90%">
          <FormattedMessage id="company_info.description" />
        </Text>
      </Box>
      <Box
        d={toaster ? 'flex' : 'none'}
        border="solid 2px #00c3af"
        borderRadius="40px"
        h="70px"
        w="90%"
        justifyContent="space-between"
        alignItems="center"
        mb="2.5%"
      >
        <Text color="#00c3af" m="0px 0px 0px 2%">
          <Checkmark boxSize={8} color="#00c3af" mr="10px" />
          <FormattedMessage id="company_info.toaster.edit_success" />
        </Text>
        <CloseIcon
          boxSize={4}
          onClick={() => dispatch(showToaster(false))}
          color="#00c3af"
          m="0px 2% 0px 0px"
          cursor="pointer"
        />
      </Box>
      <Card>
        <Box d={{ lg: 'block', xl: 'flex' }} justifyContent="space-between">
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyName">
              <FormLabel fontSize="sm" color="#a0a0a5">
                <FormattedMessage id="global.company_name" />
              </FormLabel>
              <Input
                isReadOnly={true}
                bg="gray.400"
                opacity="100%"
                name="name"
                value={form.name}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyID">
              <FormLabel fontSize="sm" color="#a0a0a5">
                <FormattedMessage id="global.identification" />
              </FormLabel>
              <Input
                isReadOnly={true}
                bg="gray.400"
                opacity="100%"
                name="identification"
                value={form.identification}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyEmail">
              <FormLabel
                fontSize="sm"
                color={errors.email ? 'red.500' : 'gray.500'}
              >
                <FormattedMessage id="global.email" />
              </FormLabel>
              <Input
                type="email"
                name="email"
                value={form.email}
                onChange={handleInputChange}
              />
              {errors.email && (
                <Text fontSize="sm" color="red.500">
                  {errors.email.message}
                </Text>
              )}
            </FormControl>
          </Box>
        </Box>
        <Box d={{ lg: 'block', xl: 'flex' }} justifyContent="space-between">
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyContact">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.contact" />
              </FormLabel>
              <Input
                name="contact"
                value={form.contact}
                onChange={handleInputChange}
                required={true}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyTelephone">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.phone" />
              </FormLabel>
              <Input
                as={InputMask}
                borderColor="gray.600"
                mask="(99) 99999-9999"
                maskChar={null}
                name="phone"
                value={form.phone}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyCEP">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.zip_code" />
              </FormLabel>
              <Input
                name="zip_code"
                value={form.zip_code}
                onChange={handleInputChange}
                borderColor="gray.600"
                as={InputMask}
                mask="99999-999"
                maskChar={null}
              />
            </FormControl>
          </Box>
        </Box>
        <Box d={{ lg: 'block', xl: 'flex' }} justifyContent="space-between">
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyAdress">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.address_line" />
              </FormLabel>
              <Input
                name="address_line"
                value={form.address_line}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyCity">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.city" />
              </FormLabel>
              <Input
                name="city"
                value={form.city}
                onChange={handleInputChange}
              />
            </FormControl>
          </Box>
          <Box w={{ lg: '100%', xl: '32%' }} p="10px">
            <FormControl id="companyState">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.state" />
              </FormLabel>
              <Select onChange={handleInputChange} name="state">
                {renderStatesOptions()}
              </Select>
            </FormControl>
          </Box>
        </Box>
        <Box d="flex" flexDirection="column" mt="3.5%">
          <Divider orientation="horizontal" mb="30px" />
          <Box>
            <Button
              onClick={submit}
              w="176px"
              h="45px"
              bg="blue.500"
              color="white"
            >
              <FormattedMessage id="global.update" />
            </Button>
          </Box>
        </Box>
      </Card>
    </>
  );
};

export default CompanyInfo;
