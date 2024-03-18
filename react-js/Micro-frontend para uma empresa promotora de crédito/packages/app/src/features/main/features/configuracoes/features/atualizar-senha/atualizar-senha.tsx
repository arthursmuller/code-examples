import { FC, useState } from 'react';

import { Button, Flex, Grid, Box, useBreakpointValue } from '@chakra-ui/react';

import { useModal } from '@pcf/design-system';
import { useAtualizarSenhaMutation } from '@pcf/core';
import { useNavigatePathUp } from 'hooks';
import SecurityForm from 'features/sign-up/components/security-form';
import { UserSecurityInfo } from 'features/sign-up/models/user-info.model';

export const AtualizarSenha: FC = () => {
  const { showModal } = useModal();
  const navigateBack = useNavigatePathUp();
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const { mutate, isLoading } = useAtualizarSenhaMutation();

  const [enableNextButton, setEnableNextButton] = useState(false);
  const [submit, setSubmit] = useState<() => void>(() => null);

  function handleSubmit({ password, currentPassword }: UserSecurityInfo): void {
    mutate(
      { novaSenha: password, senhaAtual: currentPassword as string },
      {
        onSuccess() {
          showModal({
            title: 'Sua senha foi redefinida com sucesso!',
            information: 'Guarde bem essa informação',
            closeOnClickOverlay: true,
            closeText: 'Fechar',
            onClose: navigateBack,
          });
        },
      },
    );
  }

  function setOnForward(innerSubmitFn): void {
    setSubmit(() => innerSubmitFn && innerSubmitFn(handleSubmit));
  }

  return (
    <Flex flexDir="column" as="form" onSubmit={submit} noValidate height="100%">
      <Box flex={1} marginTop={[0, 0, 10]}>
        <SecurityForm
          isNestedForm
          requirePassword
          setSubmit={setOnForward}
          triggerWhenValidateChanges={setEnableNextButton}
          showEmailField={false}
        />
      </Box>

      <Grid
        my={8}
        gridRowGap={[2, 2, 4]}
        gridColumnGap={6}
        alignItems="center"
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
      >
        <Button
          type="submit"
          isFullWidth
          isLoading={isLoading}
          isDisabled={!enableNextButton}
          mt={['0', '0', '32px', '32px']}
        >
          {isMobile ? 'Salvar nova senha' : 'Salvar'}
        </Button>

        {!isMobile && (
          <Button
            type="submit"
            colorScheme="grey"
            variant="outline"
            isFullWidth
            mt={['24px', '24px', '32px', '32px']}
            onClick={navigateBack}
          >
            Cancelar
          </Button>
        )}
      </Grid>
    </Flex>
  );
};
