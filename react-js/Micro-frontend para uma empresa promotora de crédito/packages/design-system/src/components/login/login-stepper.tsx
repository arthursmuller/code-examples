import { FC } from 'react';

import { Box } from '@chakra-ui/react';

import {
  AcesseSuaContaStep,
  AcesseSuaContaStepProps,
} from './components/acesse-sua-conta';
import {
  RedesSociaisStep,
  SocialMediaButtonProps,
} from './components/redes-sociais';
import { EmailLoginFormProps, EmailLoginStep } from './components/email';
import { LoginStepsData } from './models';

import {
  StepsContainer,
  StepsContainerProvider,
  useStepsContainerContext,
} from '../steps-container';

export interface LoginProps
  extends EmailLoginFormProps,
    SocialMediaButtonProps,
    AcesseSuaContaStepProps {
  onPrevious?: { (): void };
}

export const LoginSteps: FC<LoginProps & { hasPrevious?: boolean }> = ({
  hasPrevious,
  onGoogleLoginSuccess,
  onFacebookLoginSuccess,
  onAppleLoginSuccess,
  onCreateAccountClick,
  showCreateAccountButton,
  showSocialMediaLoginButton,
  ...props
}) => {
  const {
    stepNumber,
    previousStep,
    data: { isEmail },
  } = useStepsContainerContext<LoginStepsData>();

  return (
    <Box height="fit-content" width="100%">
      <StepsContainer
        onBack={previousStep}
        stepNumber={stepNumber}
        showBackButton={!(stepNumber === 1 && !hasPrevious)}
      >
        <AcesseSuaContaStep
          onCreateAccountClick={onCreateAccountClick}
          showSocialMediaLoginButton={showSocialMediaLoginButton}
          showCreateAccountButton={showCreateAccountButton}
        />
        {!isEmail ? (
          <RedesSociaisStep
            onFacebookLoginSuccess={onFacebookLoginSuccess}
            onGoogleLoginSuccess={onGoogleLoginSuccess}
            onAppleLoginSuccess={onAppleLoginSuccess}
            onBack={previousStep}
          />
        ) : (
          <EmailLoginStep {...props} />
        )}
      </StepsContainer>
    </Box>
  );
};

export const LoginStepper: FC<LoginProps> = ({ onPrevious, ...rest }) => (
  <StepsContainerProvider onCloseCb={onPrevious}>
    <LoginSteps {...rest} hasPrevious={!!onPrevious} />
  </StepsContainerProvider>
);
