import { ModalBody as ModalBodyChakra } from '@chakra-ui/react';
import React from 'react';

interface ModalBodyProps {
  children: React.ReactNode;
}

const ModalBody = ({ children }: ModalBodyProps) => {
  return (
    <ModalBodyChakra d="flex" flexDirection="column" alignItems="center">
      {children}
    </ModalBodyChakra>
  );
};

export default ModalBody;
