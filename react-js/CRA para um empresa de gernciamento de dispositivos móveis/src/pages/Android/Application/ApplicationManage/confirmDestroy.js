import {
  Box,
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Divider,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';

import AlertModal from '../../../../components/Icons/AlertModal';
import Text from '../../../../components/Text';

const confirmDestroy = ({ open, onClose, onDestroy }) => {
  return (
    <Modal isOpen={open} onClose={onClose} isCentered>
      <ModalOverlay />
      <ModalContent w="424px" h="363px">
        <ModalHeader d="flex" flexDirection="column" alignItems="center">
          <AlertModal boxSize={24} mt="20px" />
        </ModalHeader>
        <ModalBody d="flex" flexDirection="column" alignItems="center">
          <Text
            fontWeight="bold"
            fontSize="md"
            color="black.500"
            textAlign="center"
          >
            <FormattedMessage id="application_manage.modal.destroy.title" />
          </Text>
          <Text fontWeight="normal" fontSize="sm" mt="10px" color="black.500">
            <FormattedMessage id="application_manage.modal.destroy.text" />
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
                onClick={onDestroy}
              >
                <FormattedMessage id="global.remove" />
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
                onClick={onClose}
              >
                <FormattedMessage id="global.cancel" />
              </Button>
            </Box>
          </Box>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default confirmDestroy;
