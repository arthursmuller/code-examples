import { FC } from 'react';

import { Box, Button, ButtonProps, Icon } from '@chakra-ui/react';

interface BoxIconProps extends ButtonProps {
  label: string;
  icon: FC;
}

export const BoxIcon: FC<BoxIconProps> = ({
  label,
  icon,
  children,
  ...rest
}: BoxIconProps) => {
  return (
    <Button
      {...rest}
      colorScheme="grey"
      flexDirection="column"
      textStyle="bold12"
      h={['144px', '144px', '112px']}
      w={['auto', '148px', '112px']}
      color="primary.regular"
      whiteSpace="normal"
      lineHeight="16px"
      p="0 8px"
    >
      <Icon as={icon} boxSize="33px" />
      <Box mt="10px" color="inherit" textStyle="bold12">
        {label}
      </Box>
      {children}
    </Button>
  );
};
