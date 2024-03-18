import { FC } from 'react';

import { Flex, FlexProps } from '@chakra-ui/react';

export type PageLayoutHeaderProps = FlexProps;

export const PageLayoutHeader: FC<PageLayoutHeaderProps> = ({
  children,
  ...flexProps
}) => {
  return (
    <Flex
      className="page-layout-header"
      flexDir="column"
      px={[6, 6, 0]}
      my={[0, 0, 4]}
      {...flexProps}
    >
      {children}
    </Flex>
  );
};
