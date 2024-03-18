import { FC } from 'react';

import { Box } from '@chakra-ui/react';

import { EmailLoginForm, EmailLoginFormProps } from './email-login-form';

import { StepCard } from '../../../steps-container';

export const EmailLoginStep: FC<EmailLoginFormProps> = (props) => {
  return (
    <StepCard
      title="Você está quase lá!"
      information="Preencha seu e-mail e senha para acessar sua conta:"
    >
      <Box mt="25px">
        <EmailLoginForm {...props} />
      </Box>
    </StepCard>
  );
};
