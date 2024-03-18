import { FC, Children } from 'react';

import { Flex } from '@chakra-ui/react';

export interface DefaultModalContainerProps {
  maxWidth?: number | string;
  maxHeight?: number | string;
}

export const DefaultModalContainer: FC<DefaultModalContainerProps> = ({
  maxWidth = '600px',
  maxHeight = '480px',
  children,
}) => {
  const [header, content] = Children.toArray(children) || [];

  return (
    <Flex
      direction="column"
      align="center"
      backgroundColor="grey.100"
      borderRadius="10px"
      maxWidth={maxWidth}
      maxHeight={maxHeight}
      width="100%"
      padding="0"
      margin="32px 32px"
      onClick={(e) => e.stopPropagation()}
    >
      {header}

      <Flex
        paddingX="24px"
        paddingBottom="24px"
        direction="column"
        width="100%"
        overflow="auto"
      >
        {content}
      </Flex>
    </Flex>
  );
};
