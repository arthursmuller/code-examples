import { FC } from 'react';

import { Box, Text } from '@chakra-ui/react';

import { StatusCheckCircleIcon } from './check-icon';

export interface ValidationTextProps {
  hasError?: boolean;
}

export const ValidationText: FC<ValidationTextProps> = ({
  hasError = false,
  children,
}) => (
  <Box display="flex" alignItems="center">
    <StatusCheckCircleIcon hasError={hasError} />
    <Text
      as="p"
      textStyle="regular12"
      color={`${!hasError ? 'success' : 'error'}.regular`}
      ml="8px"
    >
      {children}
    </Text>
  </Box>
);
