import { ReactElement, FC } from 'react';

import { StepsContainer, StepsContainerStyleProps } from './steps-container';
import {
  useStepsContainerContext,
  StepsContainerProvider,
} from './steps-container.context';

export interface StepContainerWrappedProps extends StepsContainerStyleProps {
  children: Array<ReactElement | false>;
  onClose?: () => void;
}

const StepContainerConnection: FC<StepContainerWrappedProps> = ({
  children,
  ...rest
}) => {
  const { stepNumber, previousStep } = useStepsContainerContext();

  return (
    <StepsContainer stepNumber={stepNumber} onBack={previousStep} {...rest}>
      {children}
    </StepsContainer>
  );
};

export const StepsContainerWrapped: FC<StepContainerWrappedProps> = ({
  onClose,
  ...props
}) => (
  <StepsContainerProvider onCloseCb={onClose}>
    <StepContainerConnection {...props} />
  </StepsContainerProvider>
);
