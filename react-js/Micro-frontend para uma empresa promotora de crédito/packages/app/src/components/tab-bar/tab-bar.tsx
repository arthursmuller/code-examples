import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { TabBarItem } from './tab-bar-item';

export interface TabBarCompound {
  Item: typeof TabBarItem;
}

const TabBar: FC & TabBarCompound = ({ children }) => {
  return (
    <Flex
      h="56px"
      bg="grey.100"
      boxShadow="card"
      alignItems="center"
      justifyContent="space-around"
    >
      {children}
    </Flex>
  );
};

TabBar.Item = TabBarItem;

export { TabBar };
