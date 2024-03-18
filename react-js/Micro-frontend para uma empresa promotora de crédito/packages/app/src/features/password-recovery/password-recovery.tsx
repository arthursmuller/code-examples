import { useHistory } from 'react-router-dom';

import { StepsContainer, useStepState } from '@pcf/design-system';

import { EmailStep } from './components/email-step';
import { SuccessStep } from './components/success-step';

export const PasswordRecovery: React.FC = () => {
  const { stepNumber, nextStep } = useStepState();
  const history = useHistory();

  return (
    <StepsContainer
      onBack={() => history.goBack()}
      showBackButton={stepNumber === 1}
      stepNumber={stepNumber}
    >
      <EmailStep onNextStep={nextStep} />
      <SuccessStep />
    </StepsContainer>
  );
};
