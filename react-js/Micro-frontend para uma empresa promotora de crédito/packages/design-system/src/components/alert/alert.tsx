import { FC, ReactElement } from 'react';

import {
  Alert,
  AlertDescription,
  Icon,
  AlertProps,
  CloseButton,
  Flex,
  AlertTitle,
  Box,
  Center,
} from '@chakra-ui/react';

import {
  StatusCloseErrorIcon,
  InfoIcon,
  StatusCheckCircleIcon,
} from '@pcf/design-system-icons';

const STATUSES = {
  success: {
    icon: {
      component: StatusCheckCircleIcon,
      size: {
        w: '13.31px',
        h: '9.07px',
      },
      bg: 'success.dark',
    },
    boxShadow: '0px 4px 24px rgba(0, 161, 84, 0.3)',
    solid: {
      bg: 'success.regular',
      color: 'white',
    },
    subtle: {
      bg: 'success.washed',
      color: 'success.dark',
    },
    'left-accent': { bg: 'success.washed', color: 'success.dark' },
  },
  error: {
    icon: {
      component: StatusCloseErrorIcon,
      size: {
        w: '10.49px',
        h: '10.49px',
      },
      bg: 'error.dark',
    },
    boxShadow: '0px 4px 24px rgba(235, 87, 87, 0.3)',
    solid: { bg: 'error.regular', color: 'white' },
    subtle: { bg: 'error.washed', color: 'error.regular' },
    'left-accent': { bg: 'error.washed', color: 'error.regular' },
  },
  warning: {
    icon: {
      component: InfoIcon,
      size: {
        w: '6px',
        h: '14px',
      },
      bg: 'warning.dark',
    },
    boxShadow: '0px 4px 24px rgba(255, 214, 0, 0.3)',
    solid: { bg: 'warning.regular', color: 'white' },
    subtle: { bg: 'warning.washed', color: 'warning.dark' },
    'left-accent': { bg: 'warning.washed', color: 'warning.dark' },
  },
  info: {
    icon: {
      component: InfoIcon,
      size: {
        w: '6px',
        h: '14px',
      },
      bg: 'secondary.dark',
    },
    boxShadow: '',
    solid: { bg: 'secondary.regular', color: 'white' },
    subtle: { bg: 'secondary.washed', color: 'secondary.dark' },
    'left-accent': { bg: 'secondary.washed', color: 'secondary.dark' },
  },
};

export interface BemAlertProps extends AlertProps {
  description?: string | ReactElement;
  title?: string;
  onClose?: { (): void };
}

export const BemAlert: FC<BemAlertProps> = ({
  status = 'info',
  variant = 'subtle',
  description = '',
  title = '',
  onClose = () => {},
  ...rest
}) => {
  const { icon, boxShadow, ...bgAndColor } = STATUSES[status];
  const { color, bg } = bgAndColor[variant];

  return (
    <Alert
      borderRadius="md"
      boxShadow={boxShadow}
      justifyContent="space-between"
      bg={bg}
      status={status}
      variant={variant}
      alignItems={title ? 'flex-start' : 'center'}
      {...rest}
    >
      <Flex alignItems={title ? 'flex-start' : 'center'}>
        <Center
          boxSize={6}
          borderRadius="full"
          bg={icon.bg}
          mr={2}
          mt={title ? '4px' : ''}
        >
          <Icon as={icon.component} {...icon.size} width="24px" color="white" />
        </Center>

        <Box>
          {title && (
            <AlertTitle color={color} textStyle="bold16">
              {title}
            </AlertTitle>
          )}
          <AlertDescription
            display="block"
            color={color}
            textStyle={title ? 'regular12' : 'bold14'}
          >
            {description}
          </AlertDescription>
        </Box>
      </Flex>

      <CloseButton size="md" color={color} onClick={onClose} />
    </Alert>
  );
};
