import { ArrowForwardIcon } from '@chakra-ui/icons';
import {
  chakra,
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
import React, { useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { Link } from 'react-router-dom';

import Button from '../../components/Button';
import AlertModal from '../../components/Icons/AlertModal';

const ErrorModal = ({ open, setOpen, clearForm, errors }) => {
  const intl = useIntl();
  const errors_config = {
    invalid_user_password: {
      title: intl.formatMessage({
        id: 'login.modal.title.invalid_user_password',
      }),
      text: intl.formatMessage({
        id: 'login.modal.invalid_user_password',
      }),
    },
    user_last_try: {
      title: intl.formatMessage({
        id: 'login.modal.title.invalid_user_password',
      }),
      text: intl.formatMessage({
        id: 'login.modal.invalid_user_password',
      }),
      warning: intl.formatMessage({
        id: 'login.modal.user_last_try',
      }),
    },
    user_blocked: {
      blocked_text: true,
    },
  };
  if (errors === undefined || errors === null) {
    return null;
  }
  return (
    <Modal
      isOpen={open}
      onClose={() => setOpen(false)}
      isCentered
      closeOnOverlayClick={false}
    >
      <ModalOverlay />
      <ModalContent w="424px" h="450px">
        <ModalHeader d="flex" flexDirection="column" alignItems="center">
          <AlertModal boxSize={24} mt="20px" />
        </ModalHeader>
        <ModalBody d="flex" flexDirection="column" alignItems="center">
          {errors_config[errors].title && (
            <Text fontWeight="bold" fontSize="16px">
              {errors_config[errors].title}
            </Text>
          )}
          {errors_config[errors].text && (
            <Text fontWeight="normal" fontSize="14px" mt="10px">
              {errors_config[errors].text}
            </Text>
          )}
          {errors_config[errors].warning && (
            <Text
              fontWeight="bold"
              fontSize="14px"
              mt="30px"
              color="red.500"
              textAlign="center"
            >
              {errors_config[errors].warning}
            </Text>
          )}
          {errors_config[errors].blocked_text && (
            <Text fontWeight="bold" fontSize="16px" textAlign="center">
              <chakra.span color="red.500">Usuário bloqueado </chakra.span>
              para iniciar sesíon, se enviará um correo electrónico para
              desbloquear o esperar 15 minutos.
            </Text>
          )}

          <Text
            fontWeight="normal"
            fontSize="14px"
            mt="30px"
            color="blue.500"
            cursor="pointer"
          >
            {errors_config[errors].blocked_text ? (
              <Link to="/unblock">
                <FormattedMessage id="login.modal.unblock_user" />
              </Link>
            ) : (
              <Link to="/password-recovery">
                <FormattedMessage id="login.forgot_password" />
                <ArrowForwardIcon ml="3px" />
              </Link>
            )}
          </Text>
        </ModalBody>

        <ModalFooter d="flex" flexDirection="column" alignSelf="center">
          <Box mb="19px" w="424px">
            <Divider borderColor="gray.600" orientation="horizontal" />
          </Box>
          <Box d="flex" flexDirection="row">
            {errors_config[errors].blocked_text ? (
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
            ) : (
              <>
                <Box mr="14px">
                  <Button
                    w="180px"
                    h="45px"
                    fontWeight="normal"
                    colorScheme="blue"
                    onClick={clearForm}
                  >
                    <FormattedMessage id="global.try_again" />
                  </Button>
                </Box>
                <Box>
                  <Button
                    w="180px"
                    h="45px"
                    fontWeight="normal"
                    variant="outline"
                    colorScheme="blue"
                    borderColor="#0190fe"
                    onClick={clearForm}
                  >
                    <FormattedMessage id="global.cancel" />
                  </Button>
                </Box>
              </>
            )}
          </Box>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default ErrorModal;
