import { FC, useState } from 'react';

import { Flex, Text, Box, Button } from '@chakra-ui/react';
import { useParams } from 'react-router-dom';

import { StepCard, CustomHeading } from '@pcf/design-system';
import { useRecuperarSenhaMutation, AutenticacaoModel } from '@pcf/core';
import SecurityForm from 'features/sign-up/components/security-form';
import { UserSecurityInfo } from 'features/sign-up/models/user-info.model';

interface NewPasswordStepProps {
  onNextStep(autenticaoModel: AutenticacaoModel): void;
}

export const NewPasswordStep: FC<NewPasswordStepProps> = ({ onNextStep }) => {
  const [enableNextButton, setEnableNextButton] = useState(false);
  const [submit, setSubmit] = useState<() => void>(() => null);
  const { token } = useParams<{ token: string }>();

  const { mutate, isLoading } = useRecuperarSenhaMutation(token);

  function handleSubmit({ password }: UserSecurityInfo): void {
    mutate(
      { novaSenha: password, senha: password },
      {
        onSuccess(data) {
          onNextStep(data as AutenticacaoModel);
        },
      },
    );
  }

  function setOnForward(innerSubmitFn): void {
    setSubmit(() => innerSubmitFn && innerSubmitFn(handleSubmit));
  }

  return (
    <Flex flexDir="column" as="form" onSubmit={submit} noValidate>
      <StepCard>
        <CustomHeading
          as="h1"
          textStyle="bold32"
          color="secondary.mid-dark"
          textAlign="center"
          mb="24px"
        >
          Insira uma nova senha
        </CustomHeading>

        <Text as="p" textStyle="regular16" mt="2px" textAlign="center">
          Guarde bem estas informações.
        </Text>

        <SecurityForm
          isNestedForm
          setSubmit={setOnForward}
          triggerWhenValidateChanges={setEnableNextButton}
          showEmailField={false}
        />
      </StepCard>

      <Box px="6">
        <Button
          type="submit"
          isFullWidth
          isLoading={isLoading}
          mt={['24px', '24px', '32px', '32px']}
          isDisabled={!enableNextButton}
        >
          Redefinir senha
        </Button>
      </Box>
    </Flex>
  );
};
