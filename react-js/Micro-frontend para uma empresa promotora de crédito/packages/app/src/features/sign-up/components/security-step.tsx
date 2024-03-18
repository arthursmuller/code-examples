import { FC } from 'react';

import { Box } from '@chakra-ui/react';

import { StepCard } from '@pcf/design-system';

import SecurityForm from './security-form';

import { useSignUpContext } from '../sign-up.context';
import { useSocialMediaLogo } from '../useSocialMediaLogo';

const SecurityStep: FC = () => {
  const { formData, setSubmit, setCanGoForward, socialEmail } =
    useSignUpContext();
  const logo = useSocialMediaLogo();

  return (
    <StepCard
      title={
        logo
          ? 'Insira os dados abaixo para finalizar o acesso'
          : 'Crie sua conta'
      }
      information="Guarde bem estas informações."
      customTop={logo || null}
    >
      <Box mt="25px">
        <SecurityForm
          socialEmail={socialEmail}
          formData={formData}
          triggerWhenValidateChanges={setCanGoForward}
          setSubmit={setSubmit}
        />
      </Box>
    </StepCard>
  );
};

export default SecurityStep;
