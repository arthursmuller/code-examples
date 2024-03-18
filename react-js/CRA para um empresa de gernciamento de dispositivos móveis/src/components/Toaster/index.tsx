import { CloseIcon } from '@chakra-ui/icons';
import { Box } from '@chakra-ui/react';
import React, { useEffect } from 'react';

import AlertIcon from '../Icons/Alert';
import Checkmark from '../Icons/Checkmark';
import Text from '../Text';

interface ToasterProps {
  open: boolean;
  onClose: () => void;
  children: React.ReactNode;
  type?: 'success' | 'error';
}

const Toaster = ({ open, onClose, children, type, ...rest }: ToasterProps) => {
  const color = type !== 'success' ? '#ff5f5f' : '#00c3af';
  const Icon = type !== 'success' ? AlertIcon : Checkmark;
  useEffect(() => {
    if (open) {
      const timer = setTimeout(() => {
        onClose();
      }, 5000);
      window.scrollTo(0, 0);
      return () => clearTimeout(timer);
    }
  }, [open]);
  return (
    <Box
      d={open ? 'flex' : 'none'}
      border={`solid 2px ${color}`}
      borderRadius="40px"
      h="70px"
      w="90%"
      justifyContent="space-between"
      alignItems="center"
      mb="2.5%"
      {...rest}
    >
      <Text color={color} m="0px 0px 0px 2%">
        <Icon boxSize={8} color={color} mr="10px" />
        {children}
      </Text>
      <CloseIcon
        boxSize={4}
        onClick={onClose}
        color={color}
        m="0px 2% 0px 0px"
        cursor="pointer"
      />
    </Box>
  );
};

export default Toaster;
