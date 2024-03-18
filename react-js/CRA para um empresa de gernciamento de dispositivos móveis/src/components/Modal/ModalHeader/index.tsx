import {
  ModalHeader as ChakraModalHeader,
} from '@chakra-ui/react';
import React from 'react';

const ModalHeader = ({children}) => {
  return (
    <ChakraModalHeader d="flex" flexDirection="column" alignItems="center">
      {children}
    </ChakraModalHeader>
  )
}

export default ModalHeader;