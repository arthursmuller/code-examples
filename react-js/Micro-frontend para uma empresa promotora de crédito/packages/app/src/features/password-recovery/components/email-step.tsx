import { Flex, Text, Box, Button } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { useErrorHandler } from 'react-error-boundary';

import { EmailInput, StepCard, CustomHeading } from '@pcf/design-system';
import {
  useRecuperarSenhaMutation,
  BemApiErrorResponse,
  extractReadableErrorMessage,
} from '@pcf/core';

interface EmailStepFormData {
  email?: string;
}

interface EmailStepProps {
  onNextStep(): void;
}

export const EmailStep: React.FC<EmailStepProps> = ({ onNextStep }) => {
  const {
    handleSubmit,
    control,
    formState: { errors, isValid },
    setError,
  } = useForm<EmailStepFormData>({ mode: 'onChange' });
  const { mutate, isLoading } = useRecuperarSenhaMutation();
  const handleError = useErrorHandler<BemApiErrorResponse>();

  function onSubmit(data: EmailStepFormData): void {
    mutate(data, {
      onSuccess() {
        onNextStep();
      },
      onError(error) {
        const readableErrorMessage = extractReadableErrorMessage(error);

        if (!readableErrorMessage) {
          handleError(error);
        } else {
          setError('email', {
            type: 'manual',
            message: readableErrorMessage,
          });
        }
      },
    });
  }

  return (
    <Flex
      flexDir="column"
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      noValidate
    >
      <StepCard>
        <CustomHeading
          as="h1"
          textStyle="bold32"
          color="secondary.mid-dark"
          textAlign="center"
          mb="24px"
        >
          Recuperação de senha
        </CustomHeading>

        <Text
          as="p"
          textStyle="regular16"
          mt="2px"
          textAlign="center"
          mb="20px"
        >
          Insira seu email para procurarmos sua conta
        </Text>

        <Flex>
          <EmailInput
            control={control}
            errorMessage={errors?.email?.message}
            required
          />
        </Flex>
      </StepCard>

      <Box px="6">
        <Button
          type="submit"
          isFullWidth
          isLoading={isLoading}
          mt={['24px', '24px', '32px', '32px']}
          disabled={!isValid}
        >
          Continuar
        </Button>
      </Box>
    </Flex>
  );
};
