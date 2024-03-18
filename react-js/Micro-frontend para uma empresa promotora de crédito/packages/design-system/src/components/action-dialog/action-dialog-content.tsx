import { FC } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';

export const ActionDialogContent: FC = ({ children }) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  return (
    <Flex
      backgroundColor="grey.100"
      borderBottomRadius="12px"
      padding="24px"
      overflowX="hidden"
      overflowY="auto"
      borderTop={isDesktop ? '1px solid' : ''}
      borderTopColor={isDesktop ? 'grey.300' : ''}
    >
      <Flex
        direction="column"
        minHeight="fit-content"
        height="fit-content"
        width="100%"
      >
        {children}
      </Flex>
    </Flex>
  );
};
