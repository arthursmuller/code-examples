import { ArrowForwardIcon, ArrowBackIcon } from '@chakra-ui/icons';
import { Link as ChakraLink } from '@chakra-ui/react';
import {
  Grid,
  GridItem,
  Text,
  Box,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Divider,
} from '@chakra-ui/react';
import React, { useState, useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

import login_background from '../../../assets/Images/login_background.jpg';
import Button from '../../../components/Button';
import Checkmark from '../../../components/Icons/Checkmark';
import Input from '../../../components/Input';
import { recover, clearLogin } from '../../../store/auth';

const Unblock = () => {
  const dispatch = useDispatch();
  const { recovery } = useSelector((state) => state.auth);
  const [form, setForm] = useState({
    email: '',
  });
  const [open, setOpen] = useState(false);

  const updateForm = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const submit = () => {
    dispatch(recover(form));
  };

  const clearForm = () => {
    setForm({ email: '' });
    dispatch(clearLogin());
    setOpen(false);
  };

  useEffect(() => {
    setOpen(!!recovery.email);
  }, [recovery.email]);

  const intl = useIntl();

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
            <Button
              colorScheme="blue"
              variant="link"
              leftIcon={<ArrowBackIcon />}
              as={Link}
              to="/login"
            >
              Voltar
            </Button>
          </Box>

          <Box>
            <Box>
              <Text fontSize="4xl" fontWeight="300">
                <FormattedMessage id="unblock.welcome_1" />
              </Text>
              <Text fontSize="4xl" as="b">
                <FormattedMessage id="unblock.welcome_2" />
              </Text>
            </Box>

            <Box minW="xs" pt="40px" m="20px 0px 40px 0px">
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
            </Box>
            <Box>
              <Button
                color="white"
                bg="blue.500"
                w="170px"
                h="45px"
                borderRadius="5px"
                _disabled={{ bg: '#f0f0f0', color: 'gray.600' }}
                disabled={!form.email}
                onClick={submit}
              >
                <FormattedMessage id="password_recovery.continue" />
              </Button>
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
        <Modal
          isOpen={open}
          onClose={() => setOpen(false)}
          isCentered
          closeOnOverlayClick={false}
        >
          <ModalOverlay />
          <ModalContent w="424px" h="458px">
            <ModalHeader d="flex" flexDirection="column" alignItems="center">
              <Checkmark boxSize={24} mt="20px" color="green.500" />
            </ModalHeader>
            <ModalBody d="flex" flexDirection="column" alignItems="center">
              <Text fontWeight="bold" fontSize="16px">
                <FormattedMessage id="password_recovery.modal.check_email" />
              </Text>
              <Text fontWeight="normal" fontSize="14px" mt="20px">
                <FormattedMessage id="password_recovery.modal.email_sent" />
              </Text>
              <Text fontWeight="bold" fontSize="14px">
                {recovery.email}
              </Text>
              <Text
                fontWeight="normal"
                fontSize="14px"
                mt="20px"
                color="gray.300"
                textAlign="center"
              >
                <FormattedMessage id="password_recovery.modal.support_text" />
              </Text>
              <Text
                fontWeight="normal"
                fontSize="14px"
                mt="30px"
                color="blue.500"
                cursor="pointer"
              >
                <FormattedMessage id="password_recovery.modal.support_link" />{' '}
                <ArrowForwardIcon ml="3px" />
              </Text>
            </ModalBody>

            <ModalFooter d="flex" flexDirection="column" alignSelf="center">
              <Box mb="19px" w="424px">
                <Divider borderColor="gray.600" orientation="horizontal" />
              </Box>
              <Box d="flex" flexDirection="row">
                <Box mr="14px">
                  <Button
                    w="180px"
                    h="45px"
                    fontWeight="normal"
                    colorScheme="blue"
                    onClick={clearForm}
                  >
                    <FormattedMessage id="global.close" />
                  </Button>
                </Box>
              </Box>
            </ModalFooter>
          </ModalContent>
        </Modal>
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

export default Unblock;
