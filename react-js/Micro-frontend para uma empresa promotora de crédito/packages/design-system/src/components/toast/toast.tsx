import { FC } from 'react';

import {
  Alert,
  AlertDescription,
  Icon,
  AlertTitle,
  CloseButton,
  Box,
  Center,
} from '@chakra-ui/react';

export interface ToastProps {
  title?: string;
  description?: string;
  status: 'error' | 'success';
  onClose?: { (): void };
  icon: FC;
  iconProps?: {
    height?: string | number;
    width?: string | number;
  };
}

const COLORS = {
  success: {
    borderColor: 'success.dark',
    bg: 'success.regular',
  },
  error: {
    borderColor: 'error.dark',
    bg: 'error.regular',
  },
};

export const Toast: FC<ToastProps> = ({
  title,
  description,
  status,
  onClose,
  icon,
  iconProps,
}) => {
  const { bg, borderColor } = COLORS[status];

  return (
    <Alert
      variant="left-accent"
      status={status}
      bg={bg}
      borderColor={borderColor}
      alignItems="flex-start"
      borderLeftRadius="md"
      py="6px"
      minH="64px"
      maxW="360px"
      mb="80px"
    >
      <Center borderRadius="full" mt="4px" mr={2} boxSize={6} bg={borderColor}>
        <Icon as={icon} color="white" {...iconProps} />
      </Center>

      <Box flex="1">
        {title && (
          <AlertTitle textStyle="bold16" color="white">
            {title}
          </AlertTitle>
        )}
        {description && (
          <AlertDescription
            lineHeight="normal"
            display="block"
            textStyle="regular12"
            color="white"
            mr="36px"
          >
            {description}
          </AlertDescription>
        )}
      </Box>

      <CloseButton size="sm" onClick={onClose} color="white" />
    </Alert>
  );
};
