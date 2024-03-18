import { FC } from 'react';

import {
  Button,
  Center,
  CenterProps,
  Text,
  Flex,
  Icon,
} from '@chakra-ui/react';
import { FallbackProps } from 'react-error-boundary';

import { AttentionIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

import { loadFailMessages, alertMessages } from './default-messages';
import { BemErrorFallbackMessageModel } from './bem-error-message.model';

import { CustomHeading } from '../../custom-heading';

type SchemeColor = 'white' | 'error';

export interface BemErrorFallbackProps
  extends FallbackProps,
    BemErrorFallbackMessageModel {
  schemeColor?: SchemeColor;
  chakraProps?: CenterProps;
  isAlert?: boolean;
}

export const getColorAndTextColor = (
  schemeColor: SchemeColor,
  isAlert: boolean,
): { color: string; textColor: string } => {
  if (isAlert) {
    return {
      color: 'warning.regular',
      textColor: 'warning.ligth',
    };
  }
  return {
    color: schemeColor === 'error' ? 'error.dark' : 'white',
    textColor: schemeColor === 'error' ? 'grey.800' : 'white',
  };
};

export const BemErrorFallback: FC<BemErrorFallbackProps> = ({
  isAlert = false,
  title = isAlert ? alertMessages.title : loadFailMessages.title,
  description = isAlert
    ? alertMessages.description
    : loadFailMessages.description,
  buttonLabel = loadFailMessages.buttonLabel,
  error,
  schemeColor = 'error',
  resetErrorBoundary,
  chakraProps = {},
}) => {
  const { color, textColor } = getColorAndTextColor(schemeColor, isAlert);

  return (
    <Center
      flexDir="column"
      h="100%"
      padding={6}
      borderRadius={6}
      {...chakraProps}
    >
      <Flex wrap="wrap" align="center" justify="center" marginBottom={2}>
        <Flex
          align="center"
          justify="center"
          border="5px solid"
          borderColor={color}
          width="72px"
          height="72px"
          marginBottom={2}
          borderRadius="100px"
        >
          <Icon
            as={isAlert ? AttentionIcon : StatusCloseErrorIcon}
            fill={color}
            width="32px"
            height="32px"
          />
        </Flex>

        <CustomHeading
          as="h2"
          textStyle="bold24"
          color={color}
          textAlign="center"
          mx={6}
          marginBottom={2}
        >
          {title}
        </CustomHeading>
      </Flex>

      <Text
        as="p"
        mx={4}
        textStyle="regular16"
        color={textColor}
        textAlign="center"
      >
        {description || error.message}
      </Text>

      {!isAlert && (
        <Button
          onClick={resetErrorBoundary}
          colorScheme={schemeColor === 'error' ? 'error' : 'grey'}
          {...(color === 'white' ? { color: 'error.regular' } : {})}
          mt={5}
        >
          {buttonLabel}
        </Button>
      )}
    </Center>
  );
};
