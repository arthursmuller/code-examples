import { ArrowForwardIcon } from '@chakra-ui/icons';
import { Link as ChakraLink } from '@chakra-ui/react';
import {
  Grid,
  GridItem,
  Text,
  Box,
  Image,
  Checkbox,
} from '@chakra-ui/react';
import React, { useState, useEffect, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { Link } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import login_background from '../../assets/Images/login_background.jpg';
import logo from '../../assets/Images/Logo horizontal@3x.png';
import Button from '../../components/Button';
import Eye from '../../components/Icons/Eye';
import EyeDisabled from '../../components/Icons/EyeDisabled';
import Input from '../../components/Input';
import { validatorDefaultMessages } from '../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../hooks/useRedux';
import { login, clearLogin } from '../../store/auth';
import ErrorModal from './errorModal';
import '../../assets/css/form-validation.css';

const Login = () => {
  const intl = useIntl();

  const simpleValidator = useRef(new SimpleReactValidator({
    messages: {
      ...validatorDefaultMessages(intl),
    }
  }));

  const dispatch = useAppDispatch();
  const { error } = useAppSelector((state) => state.auth);
  const errors = undefined;
  const [form, setForm] = useState({
    email: '',
    password: '',
    keep_logged: false,
  });
  const [open, setOpen] = useState(false);
  const [passwordShown, setPasswordShown] = useState(false);

  const updateForm = (e) => {
    setForm({
      ...form,
      [e.target.name]:
        e.target.type === 'checkbox' ? e.target.checked : e.target.value,
    });
    simpleValidator.current.showMessages();
  };

  const submit = () => {
    dispatch(login(form));
  };

  const togglePasswordVisiblity = () => {
    setPasswordShown(passwordShown ? false : true);
  };

  const clearForm = () => {
    setForm({ email: '', password: '', keep_logged: false });
    dispatch(clearLogin());
  };

  useEffect(() => {
    setOpen(error);
  }, [error]);

  return (
    <Grid h="100vh" templateColumns="repeat(auto-fit, minmax(100px, 1fr))">
      <GridItem rowStart={1} rowEnd={7} colSpan={4} bg="white">
        <Box
          d="flex"
          flexDir="column"
          justifyContent="space-between"
          h="100%"
          pl="20%"
          pr="10%"
        >
          <Box pt="20%">
            <Image
              src={logo}
              htmlWidth="246px"
              htmlHeight="26px"
              alt="Segun Adebayo"
            />
          </Box>

          <Box>
            <Box>
              <Text fontSize="4xl" fontWeight="300">
                <FormattedMessage id="login.welcome_1" />
              </Text>
              <Text fontSize="4xl" as="b">
                <FormattedMessage id="login.welcome_2" />
              </Text>
            </Box>

            <Box minW="xs" pt="40px">
              <Text fontSize="sm" textAlign="left" color="gray.500" mb="12px">
                <FormattedMessage id="global.email" />
              </Text>
              <Input
                inputProps={{
                  placeholder: intl.formatMessage({
                    id: 'login.email_placeholder',
                  }),
                  type: 'email',
                  onChange: updateForm,
                  value: form.email,
                  name: 'email',
                }}
              />
              {simpleValidator.current.message('email', form.email, 'required|email')}
            </Box>
            <Box minW="xs">
              <Text
                fontSize="sm"
                textAlign="left"
                color="gray.500"
                m="30px 0px 12px 0px"
              >
                <FormattedMessage id="global.password" />
              </Text>
              <Input
                inputProps={{
                  type: passwordShown ? 'text' : 'password',
                  autoComplete: 'off',
                  placeholder: intl.formatMessage({
                    id: 'login.password_placeholder',
                  }),
                  onChange: updateForm,
                  value: form.password,
                  name: 'password',
                }}
                rightElement={
                  <i onClick={togglePasswordVisiblity}>
                    {passwordShown ? (
                      <EyeDisabled boxSize={6} cursor="pointer" />
                    ) : (
                      <Eye boxSize={6} cursor="pointer" />
                    )}
                  </i>
                }
              />
              {simpleValidator.current.message('password', form.password, 'required')}
              <Text
                fontSize="sm"
                color="blue.500"
                m="20px 0px 40px 0px"
                cursor="pointer"
              >
                <Link to="/password-recovery">
                  <FormattedMessage id="login.forgot_password" />
                  <ArrowForwardIcon ml="3px" />
                </Link>
              </Text>
            </Box>
            <Box>
              <Button
                color="white"
                bg="blue.500"
                w="170px"
                h="45px"
                borderRadius="5px"
                _disabled={{ bg: '#f0f0f0', color: 'gray.600' }}
                disabled={!simpleValidator.current.allValid()}
                onClick={submit}
              >
                <FormattedMessage id="login.btn_submit" />
              </Button>
            </Box>
            <Box d="flex" mt="20px">
              <Checkbox
                colorScheme="green"
                name="keep_logged"
                isChecked={form.keep_logged}
                onChange={updateForm}
              >
                <Text fontSize="sm" color="gray.500">
                  <FormattedMessage id="login.keep_connected" />
                </Text>
              </Checkbox>
            </Box>
          </Box>
          <Box pb="25px">
            <Text
              fontSize="xs"
              lineHeight="40px"
              textAlign="left"
              color="blue.500"
              cursor="pointer"
            >
              <ChakraLink
                href="https://controlmovil.telcel.com/assets/docs/eula.pdf"
                isExternal
              >
                <FormattedMessage id="login.terms_and_conditions" />
              </ChakraLink>
            </Text>
            <Text
              fontSize="xs"
              lineHeight="40px"
              textAlign="left"
              color="blue.500"
              cursor="pointer"
            >
              <ChakraLink
                href="https://www.telcel.com/aviso-de-privacidad"
                isExternal
              >
                <FormattedMessage id="login.privacy_warning" />
              </ChakraLink>
            </Text>
          </Box>
        </Box>
        <ErrorModal
          open={open}
          setOpen={setOpen}
          clearForm={clearForm}
          errors={errors}
        />
      </GridItem>
      <GridItem rowStart={1} rowEnd={7} colSpan={8} bg="papayawhip">
        <Box
          w="100%"
          h="100%"
          backgroundImage={`url('${login_background}')`}
          backgroundRepeat="no-repeat"
          backgroundSize="cover"
          backgroundPosition="50% 50%"
        />
      </GridItem>
    </Grid>
  );
};

export default Login;
