import { FC } from 'react';

import { Icon } from '@chakra-ui/react';

import { StatusCloseErrorIcon, CheckIcon } from '@pcf/design-system-icons';

export const StatusCheckCircleIcon: FC<{ hasError?: boolean; size?: string }> =
  ({ hasError = false, size = '16px' }) => (
    <Icon
      color={!hasError ? 'success.regular' : 'error.regular'}
      aria-label="validação"
      backgroundColor={!hasError ? 'success.washed' : 'error.washed'}
      p="2px"
      borderRadius="100px"
      as={!hasError ? CheckIcon : StatusCloseErrorIcon}
      height={size}
      width={size}
    />
  );
