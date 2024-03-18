import { FC, ReactElement } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';
import { usePrevious } from 'react-use';

import { ActionDialogContainer } from './action-dialog-container';
import { ActionDialogContent } from './action-dialog-content';
import { ActionDialogCloseButton } from './action-dialog-header';

import { rightToLeft, leftToRight } from '../../animations';
import { useModal } from '../modal';
import { StepsContainer } from '../steps-container';
import {
  StepsContainerProvider,
  useStepsContainerContext,
} from '../steps-container/steps-container.context';

export interface StepsActionDialogProps {
  children: Array<ReactElement | false>;
}

export const StepsActionDialogComp: FC<StepsActionDialogProps> = ({
  children,
}) => {
  const { stepNumber, previousStep } = useStepsContainerContext();
  const lastStepNumber = usePrevious(stepNumber) || 0;
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');
  const { hideModal } = useModal();

  const childrenElements = children?.filter((child) => !!child) || [];

  return (
    <ActionDialogContainer>
      <StepsContainer
        stepNumber={stepNumber}
        colorScheme={isDesktop ? 'secondary' : 'white'}
        size="md"
        containerHeight="full"
        onBack={previousStep}
        backText={isDesktop ? undefined : ''}
        customAction={<ActionDialogCloseButton onClose={hideModal} />}
      >
        {childrenElements.map((child, index) => (
          <ActionDialogContent key={`step-${index}`}>
            <Flex
              direction="column"
              animation={`250ms ${
                lastStepNumber <= stepNumber ? rightToLeft : leftToRight
              } ease-in-out`}
            >
              {child}
            </Flex>
          </ActionDialogContent>
        ))}
      </StepsContainer>
    </ActionDialogContainer>
  );
};

export const StepsActionDialog: FC<StepsActionDialogProps> = ({ children }) => {
  const { hideModal } = useModal();

  return (
    <StepsContainerProvider onCloseCb={hideModal}>
      <StepsActionDialogComp>{children}</StepsActionDialogComp>
    </StepsContainerProvider>
  );
};
