import { FC } from 'react';

import {
  Flex,
  Button,
  Popover,
  PopoverTrigger,
  Center,
  Icon,
  PopoverContent,
  PopoverArrow,
  PopoverBody,
} from '@chakra-ui/react';

import { fadeIn, Loader, useDelayedLoading } from '@pcf/design-system';
import { ArrowOutlineRightIcon } from '@pcf/design-system-icons';

import { useAutoFillPersonalDataContext } from '../auto-fill-personal-data.context';

interface BaseLayoutStepFooterProps {
  hasErrors?: boolean;
  errorMessage?: string;
}

export const BaseLayoutStepFooter: FC<BaseLayoutStepFooterProps> = ({
  hasErrors = false,
  errorMessage,
}) => {
  const { onNext, onPrevious } = useAutoFillPersonalDataContext();

  return (
    <Flex justifyContent="space-between" my={6}>
      <Button onClick={onPrevious} mr={4} colorScheme="grey">
        Voltar
      </Button>

      <Popover isOpen={hasErrors}>
        <PopoverTrigger>
          <Button
            onClick={onNext}
            colorScheme="secondary"
            disabled={hasErrors}
            rightIcon={
              <Center ml={2} borderRadius="full" w={6} h={6} bg="white">
                <Icon
                  as={ArrowOutlineRightIcon}
                  color="black"
                  w="6.75px"
                  h="11.61px"
                />
              </Center>
            }
          >
            Continuar
          </Button>
        </PopoverTrigger>
        <PopoverContent bg="secondary.light" color="white">
          <PopoverArrow bg="secondary.light" />
          <PopoverBody>{errorMessage}</PopoverBody>
        </PopoverContent>
      </Popover>
    </Flex>
  );
};

interface BaseLayoutStepComposition {
  Footer: FC<BaseLayoutStepFooterProps>;
}

interface BaseLayoutStepProps {
  isLoading: boolean;
}

const BaseLayoutStep: FC<BaseLayoutStepProps> & BaseLayoutStepComposition = ({
  isLoading,
  children,
}) => {
  const delayedLoading = useDelayedLoading(isLoading, 200);

  return (
    <Flex flexGrow={1} direction="column" mt={6}>
      {delayedLoading ? (
        <Loader />
      ) : (
        <Flex
          w="100%"
          animation={`${fadeIn} 1s forwards`}
          flexDir="column"
          flexGrow={1}
          justifyContent="space-around"
        >
          {children}
        </Flex>
      )}
    </Flex>
  );
};

BaseLayoutStep.Footer = BaseLayoutStepFooter;

export { BaseLayoutStep };
