import { FC } from 'react';

import { Icon, Text, Wrap, WrapItem } from '@chakra-ui/react';

import * as AllIcons from '.';

export default {
  title: 'Icons',
};

export const Default: FC = () => (
  <Wrap spacing="15px">
    {Object.keys(AllIcons).map((key) => (
      <WrapItem
        bg="grey.400"
        w="200px"
        h="100px"
        borderRadius="mds"
        justifyContent="center"
        alignItems="center"
        flexDir="column"
      >
        <Icon as={AllIcons[key]} boxSize={6} />
        <Text textStyle="regular12" mt={2}>
          {key}
        </Text>
      </WrapItem>
    ))}
  </Wrap>
);
