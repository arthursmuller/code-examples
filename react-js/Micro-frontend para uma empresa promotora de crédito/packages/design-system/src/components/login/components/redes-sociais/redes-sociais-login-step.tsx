import { FC } from 'react';

import { Button, Box } from '@chakra-ui/react';

import {
  SocialMediaButtonProps,
  SocialMediaButtons,
} from './social-media-buttons';

import { LoginDivider } from '../../login-popover';
import { StepCard } from '../../../steps-container';

interface RedesSociasProps extends SocialMediaButtonProps {
  onBack(): void;
}

export const RedesSociaisStep: FC<RedesSociasProps> = ({
  onBack,
  onGoogleLoginSuccess,
  onFacebookLoginSuccess,
  onAppleLoginSuccess,
}: RedesSociasProps) => {
  return (
    <StepCard
      title="Você está quase lá!"
      information="Escolha uma das opções para acessar sua conta:"
    >
      <Box mt="25px">
        <SocialMediaButtons
          onFacebookLoginSuccess={onFacebookLoginSuccess}
          onGoogleLoginSuccess={onGoogleLoginSuccess}
          onAppleLoginSuccess={onAppleLoginSuccess}
        />
      </Box>

      <LoginDivider />

      <Button onClick={onBack} variant="link" mt="23px" color="primary.regular">
        Entrar com e-mail e senha
      </Button>
    </StepCard>
  );
};
