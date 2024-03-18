import {
  Modal as ChakraModal,
  ModalContent,
  ModalOverlay,
} from '@chakra-ui/react';
import React, { ReactNode } from 'react';

import ModalBody from './ModalBody';
import ModalFooter from './ModalFooter';
import ModalHeader from './ModalHeader';

interface ModalActionProps {
  children: ReactNode;
  isOpen: boolean;
  onClose: () => void;
}

const Modal: React.FC<ModalActionProps> = ({
  children,
  isOpen,
  onClose,
}: ModalActionProps) => {
  return (
    <ChakraModal isOpen={isOpen} onClose={onClose} isCentered>
      <ModalOverlay />
      <ModalContent w="724px" h="600px" maxW={'unset'}>
        {children}
      </ModalContent>
    </ChakraModal>
  );
};

export default Modal;

export { ModalHeader, ModalBody, ModalFooter };
