import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { rightToLeft } from '../../animations';
import { CustomHeading } from '../custom-heading';

export interface FullLayoutCardProps {
  title?: string;
  paddingX?: string | number;
}

export const FullLayoutCard: FC<FullLayoutCardProps> = ({
  title,
  paddingX,
  children,
}) => {
  return (
    <Flex
      id="full-layout-card"
      flexDir="column"
      layerStyle="card-underneath-bottom"
      flex={1}
      p="0"
      overflow="hidden"
      bg="grey.100"
      sx={{
        animation: `250ms ${rightToLeft} ease-in-out`,
      }}
    >
      <Flex minH="24px" w="100%" flexShrink={0}>
        {title && (
          <CustomHeading
            pt="24px"
            px={paddingX || ['24px', '24px', '24px', '108px']}
            pb="8px"
            as="h2"
            textStyle="bold24_32"
            color="secondary.regular"
          >
            {title}
          </CustomHeading>
        )}
      </Flex>

      <Flex
        flexGrow={1}
        direction="column"
        overflowY={['hidden', 'hidden', 'auto']}
        px={paddingX || ['24px', '24px', '24px', '108px']}
        pt="0"
        position="relative"
      >
        {children}
      </Flex>
    </Flex>
  );
};
