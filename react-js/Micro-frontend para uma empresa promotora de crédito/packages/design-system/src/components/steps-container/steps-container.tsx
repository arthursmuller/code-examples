import { ReactElement, FC } from 'react';

import {
  Flex,
  Text,
  Button as ChakraButton,
  Button,
  Box,
  Center,
  Icon,
} from '@chakra-ui/react';

import { ArrowLeftIcon } from '@pcf/design-system-icons';

import { CustomHeading } from '../custom-heading';

export interface StepsContainerControlProps {
  children: Array<ReactElement | false>;
  onBack?: () => void;
  onForward?: () => void;
  stepNumber: number;
  canForward?: boolean;
}

export interface StepsContainerStyleProps {
  title?: string;
  titleTag?;
  backText?: string;
  customAction?: ReactElement;
  showStepsIndicator?: boolean;
  showBackButton?: boolean;
  showHeader?: boolean;

  size?: 'sm' | 'md' | 'lg';
  containerHeight?: 'default' | 'full';
  colorScheme?: 'primary' | 'secondary' | 'white';
}

export interface StepsContainerProps
  extends StepsContainerControlProps,
    StepsContainerStyleProps {}

const sizes = {
  sm: '440px',
  md: '648px',
  lg: '1016px',
  default: '24px',
  full: '0px',
};

const colorSchemes = {
  primary: {
    back: 'primary.regular',
    step: 'secondary.mid-dark',
  },
  secondary: {
    back: 'secondary.mid-dark',
    step: 'secondary.mid-dark',
  },
  white: {
    back: 'grey.100',
    step: 'grey.100',
  },
};

export const StepsContainer: FC<StepsContainerProps> = ({
  onBack,
  onForward,
  canForward,
  stepNumber,

  title,
  titleTag = 'h1',

  customAction,
  children,

  showHeader = true,
  showBackButton = true,
  showStepsIndicator = true,
  backText = 'Voltar',
  size = 'sm',
  colorScheme = 'primary',
  containerHeight = 'default',
}: StepsContainerProps) => {
  const childrenElements = children?.filter((child) => !!child) || [];

  return (
    <Flex
      className="step-container"
      w="100%"
      h="100%"
      justifyContent="center"
      overflowY="auto"
      overflowX="hidden"
    >
      <Flex
        flexDir="column"
        width="100%"
        maxWidth={sizes[size]}
        paddingBottom={sizes[containerHeight]}
        justifyContent="center"
      >
        {showHeader && (
          <Flex
            className="step-container__header"
            justifyContent="space-between"
            direction={title ? 'column' : 'row'}
            alignItems={title ? 'baseline' : 'center'}
            minH={containerHeight === 'full' ? '64px' : '18px'}
            pt="2px"
            mb="16px"
            mt={sizes[containerHeight]}
            px="6"
            position="relative"
          >
            {showBackButton ? (
              <ChakraButton
                color={colorSchemes[colorScheme].back}
                fontWeight="700"
                variant="link"
                size="sm"
                onClick={onBack}
                fill={colorSchemes[colorScheme].back}
                textDecoration="underline"
                leftIcon={<Icon display="flex" as={ArrowLeftIcon} />}
              >
                {backText}
              </ChakraButton>
            ) : (
              <span />
            )}

            {title && (
              <Center>
                <CustomHeading
                  as={titleTag}
                  textStyle="bold32"
                  color={colorSchemes[colorScheme].step}
                  mb="20px"
                  mt="16px"
                >
                  {title}
                </CustomHeading>
              </Center>
            )}

            {showStepsIndicator && (
              <Text
                as="p"
                textStyle="bold14"
                color={colorSchemes[colorScheme].step}
                alignSelf="center"
              >
                Etapa {stepNumber} de {childrenElements.length}
              </Text>
            )}

            {customAction}
          </Flex>
        )}

        {childrenElements[stepNumber - 1]}

        {onForward && (
          <Box px="6">
            <Button
              isFullWidth
              mt={['24px', '24px', '32px', '32px']}
              onClick={onForward}
              disabled={!canForward}
            >
              Continuar
            </Button>
          </Box>
        )}
      </Flex>
    </Flex>
  );
};
