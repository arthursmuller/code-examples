import { FC } from 'react';

import { Box, Button, Text } from '@chakra-ui/react';

import { useStepsContainerContext, StepCard } from '../../../steps-container';
import { LoginStepsData } from '../../models';

export interface AcesseSuaContaStepProps {
  onCreateAccountClick(): void;
  showCreateAccountButton: boolean;
  showSocialMediaLoginButton: boolean;
}

export const AcesseSuaContaStep: FC<AcesseSuaContaStepProps> = ({
  onCreateAccountClick,
  showCreateAccountButton,
  showSocialMediaLoginButton,
}) => {
  const { nextStep } = useStepsContainerContext<LoginStepsData>();

  function handleLoginWithSocialMedia(): void {
    nextStep({ isEmail: false });
  }

  return (
    <StepCard
      title="Acesse sua conta"
      subTitle="Que bom te ver novamente!"
      information="Escolha uma das opções para acessar sua conta:"
    >
      <Box mt="25px">
        <Button isFullWidth onClick={() => nextStep({ isEmail: true })}>
          Entrar com e-mail e senha
        </Button>
      </Box>

      {(showSocialMediaLoginButton || showCreateAccountButton) && (
        <Text as="p" textStyle="regular14" mt="19px" textAlign="center">
          ou, se preferir
        </Text>
      )}

      {showSocialMediaLoginButton && (
        <Button
          onClick={handleLoginWithSocialMedia}
          mt="21px"
          colorScheme="grey"
        >
          Entrar com redes sociais
        </Button>
      )}

      {showCreateAccountButton && (
        <Button onClick={onCreateAccountClick} mt="21px" colorScheme="grey">
          Criar Conta
        </Button>
      )}
    </StepCard>
  );
};
