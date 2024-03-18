import { useToast } from '@chakra-ui/react';

import {
  StatusCheckCircleIcon,
  StatusCloseErrorIcon,
} from '@pcf/design-system-icons';

import { Toast } from './toast';

export const useQuickToast: () => (
  title?: string | undefined,
  description?: string | undefined,
  status?: 'error' | 'success',
) => string | number | undefined = () => {
  const toast = useToast();

  return (
    title?: string,
    description?: string,
    status: 'error' | 'success' = 'error',
  ): string | number | undefined => {
    const isError = status === 'error';

    return toast({
      position: 'bottom-right',
      render: ({ onClose }) => {
        return (
          <Toast
            icon={isError ? StatusCloseErrorIcon : StatusCheckCircleIcon}
            iconProps={isError ? { height: '11px', width: '11px' } : undefined}
            title={title}
            description={description}
            status={status}
            onClose={onClose}
          />
        );
      },
    });
  };
};
