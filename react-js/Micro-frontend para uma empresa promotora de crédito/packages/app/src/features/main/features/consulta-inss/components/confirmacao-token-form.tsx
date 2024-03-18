import { FC } from 'react';

import {
  Flex,
  PinInput,
  PinInputField,
  Text,
  Button,
  HStack,
  Icon,
} from '@chakra-ui/react';
import { Controller, useForm } from 'react-hook-form';
import { useHistory } from 'react-router-dom';

import { useModal, ColorSchemes } from '@pcf/design-system';
import { PhoneIcon } from '@pcf/design-system-icons';
import { useBeneficioInssValidarTokenMutation } from '@pcf/core';

export interface TokenFormData {
  pin: string;
}

interface ConfirmacaoTokenFormProps {
  active?: boolean;
  idConsultaBeneficio?: number;
}

const inactiveStyles = {
  opacity: '0.4',
  pointerEvents: 'none',
};

export const ConfirmacaoTokenForm: FC<ConfirmacaoTokenFormProps> = ({
  active = false,
  idConsultaBeneficio,
}) => {
  const { handleSubmit, watch, control } = useForm<TokenFormData>();
  const { mutate, isLoading } = useBeneficioInssValidarTokenMutation();
  const { showModal } = useModal();
  const history = useHistory();

  const pinValue = watch('pin');

  function onSubmit(data: TokenFormData): void {
    if (idConsultaBeneficio) {
      mutate(
        { idConsultaBeneficio, tokenConsulta: data.pin },
        {
          onSuccess() {
            showModal({
              title: 'Seu Token foi validado com sucesso!',
              confirmText: 'Acessar meus valores',
              onConfirm() {
                history.push('/');
              },
              closeOnClickOverlay: false,
            });
          },
          onError() {
            showModal({
              title: 'Houve um erro com a validação do seu Token',
              type: ColorSchemes.error,
              closeText: 'Tentar Novamente',
              closeOnClickOverlay: false,
            });
          },
        },
      );
    } else {
      throw new Error('Não pode estar ativo sem idConsultaBeneficio');
    }
  }

  return (
    <Flex
      w={['100%', '100%', '251px']}
      minH="304px"
      layerStyle="card"
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      flexDirection="column"
      justifyContent="space-between"
      sx={!active ? inactiveStyles : {}}
    >
      <Flex flexDir="column">
        <Text
          textStyle="bold14"
          color="secondary.mid-dark"
          mb={4}
          textAlign="center"
        >
          Confirmação do Token
        </Text>

        <Flex>
          <Icon as={PhoneIcon} w="18px" h="28px" mr="15px" />
          <Text textStyle="regular12">
            Eviamos para seu número um SMS com o token.{' '}
            <b>Confira seu celular!</b>
          </Text>
        </Flex>
      </Flex>

      <HStack align="center" justify="center">
        <Controller
          name="pin"
          control={control}
          render={({ field: { onChange, value } }) => (
            <PinInput otp onChange={onChange} value={value}>
              <PinInputField
                border="2px solid"
                borderColor="primary.light"
                h="60px"
              />
              <PinInputField
                border="2px solid"
                borderColor="primary.light"
                h="60px"
              />
              <PinInputField
                border="2px solid"
                borderColor="primary.light"
                h="60px"
              />
              <PinInputField
                border="2px solid"
                borderColor="primary.light"
                h="60px"
              />
            </PinInput>
          )}
        />
      </HStack>

      <Button
        type="submit"
        isFullWidth
        size="sm"
        colorScheme="secondary"
        loading={isLoading}
        disabled={pinValue ? pinValue.length !== 4 : true}
      >
        Validar Token
      </Button>
    </Flex>
  );
};
