import { Flex, Box } from '@chakra-ui/react';
import React from 'react'

import { COLOR } from '../../../../types/util';

interface ItemListDrillProps {
  label: React.ReactNode;
  value: React.ReactNode;
  color?: COLOR;
}

const ItemListDrill = ({ label, value, color}: ItemListDrillProps) => {
  return (
    <Flex>
      <Box
        fontSize="14px"
        fontWeight="bold"
        lineHeight="2.86"
        letterSpacing="0.56px"
        textAlign="left"
        color="#6e6e78"
        mr="5px"
      >
        {label}:
      </Box>
      <Box
        fontSize="14px"
        lineHeight="2.86"
        letterSpacing="0.56px"
        textAlign="left"
        color={color ? color : '#6e6e78'}
      >
        {value}
      </Box>
    </Flex>
  );
};

export default ItemListDrill;