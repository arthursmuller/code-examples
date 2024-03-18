import { useState, FC } from 'react';

import { Box, Text, Button, Checkbox } from '@chakra-ui/react';
import { useMount } from 'react-use';
import { useHistory, useLocation } from 'react-router-dom';

import {
  useModal,
  StepCard,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';
import {
  useSignUpMutation,
  UsuarioCriacaoModel,
  extractReadableErrorMessage,
} from '@pcf/core';

import { TermText } from './terms-or-services';

import { useSocialMediaLogo } from '../useSocialMediaLogo';
import { useSignUpContext } from '../sign-up.context';

const TermsOfServiceForm: FC = () => {
  const { setSubmit, formData, socialEmail } = useSignUpContext();
  const [confirm, setConfirm] = useState<boolean>(false);
  const { mutate, isLoading } = useSignUpMutation({ onError: () => {} });
  const { showModal } = useModal();
  const history = useHistory();
  const logo = useSocialMediaLogo();
  const location = useLocation();
  const urlSearchParams = new URLSearchParams(location.search);

  const socialMedia = urlSearchParams.get('socialMedia');
  const token = urlSearchParams.get('token');

  useMount(() => {
    setSubmit(null);
  });

  const submitUser = (): void => {
    const userForm: UsuarioCriacaoModel = {
      nome: `${formData.name} ${formData.surname}`,
      email: socialEmail || formData.email,
      cpf: formData.cpf,
      senha: formData.password,
      loginSocial: socialMedia
        ? { redeSocial: Number(socialMedia), token }
        : null,
    };

    mutate(userForm, {
      onSuccess() {
        showModal({
          title: 'Pronto! Sua conta foi criada com sucesso!',
          information:
            'Você já pode acessar sua conta e aproveitar todos os serviços que a Bem tem especialmente para você.',
          closeOnClickOverlay: false,
          closeText: 'Acessar minha conta',
          onClose: () => history.push('/'),
        });
      },
      onError(error) {
        showModal(
          getDefaultErrorModalConfig({
            information: extractReadableErrorMessage(error, {
              showFallbackMessage: true,
            }),
          }),
        );
      },
    });
  };

  return (
    <>
      <StepCard title="Termos e condições" customTop={logo || null}>
        <Box
          mt="25px"
          maxHeight="300px"
          overflowY="auto"
          mr="-12px"
          paddingRight={2}
        >
          <Text as="p" textStyle="bold16" mb="24px">
            Garantias, Responsabilidades e Danos
          </Text>

          {TermText.map((line, index) => (
            <Text
              key={`${index}`}
              as="p"
              mb="16px"
              textStyle="regular12"
              whiteSpace="break-spaces"
            >
              {line}
            </Text>
          ))}
        </Box>
      </StepCard>

      <Checkbox
        onChange={(next) => setConfirm(next.target.checked)}
        mt="32px"
        marginX={6}
        colorScheme="success"
      >
        <Text as="p" textStyle="regular12" ml="16px">
          Ao clicar em concordo, você aceita nossa Política de Privacidade e os
          Termos de Uso, finalizando a criação da sua conta.
        </Text>
      </Checkbox>

      <Button
        mt={['24px', '24px', '32px', '32px']}
        onClick={submitUser}
        disabled={!confirm}
        colorScheme="success"
        isLoading={isLoading}
        loadingText="Criando cadastro"
        marginX={6}
      >
        Concordo
      </Button>
    </>
  );
};

export default TermsOfServiceForm;
