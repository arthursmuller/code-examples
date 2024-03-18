import { FC } from 'react';

import { Box, Button, Icon } from '@chakra-ui/react';

import { ArrowLeftIcon } from '@pcf/design-system-icons';

export interface PageLayoutBackButtonProps {
  onClick?: { (): void };
}

export const PageLayoutBackButton: FC<PageLayoutBackButtonProps> = ({
  onClick,
}) => (
  <Box>
    <Button
      fontWeight="700"
      variant="link"
      size="sm"
      onClick={onClick}
      color="grey.100"
      textDecoration="underline"
      leftIcon={
        <Icon
          as={ArrowLeftIcon}
          mb="3px"
          w={2}
          h={3}
          color="grey.100"
          marginInlineEnd="10px"
        />
      }
    >
      Voltar
    </Button>
  </Box>
);
