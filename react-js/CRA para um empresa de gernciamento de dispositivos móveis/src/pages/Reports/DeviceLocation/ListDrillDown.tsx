import { Flex, Box } from '@chakra-ui/react';

import { LocationType } from '../../../types/locations';

interface ListDrillDownProps {
  location: LocationType;
}

function ListDrillDown({ location }: ListDrillDownProps) {
  const itemList = (label, value, color?) => {
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
  return (
    <Flex p="0 0 30px 70px">
      <Flex flexDirection="column">
        {itemList('Dirección', location?.address)}
      </Flex>
    </Flex>
  );
}

export default ListDrillDown;
