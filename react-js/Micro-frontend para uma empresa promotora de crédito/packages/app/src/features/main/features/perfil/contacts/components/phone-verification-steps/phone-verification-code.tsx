import { FC, useState } from 'react';

import {
  Flex,
  PinInput,
  PinInputField,
  Text,
  Button,
  HStack,
} from '@chakra-ui/react';
import { Controller, useForm } from 'react-hook-form';
import { useTimer } from 'use-timer';
import { useMount } from 'react-use';

import {
  useModal,
  useQuickToast,
  useStepsContainerContext,
} from '@pcf/design-system';
import {
  usePhoneConfirmationValidation,
  usePhoneGetConfirmationTokenResend,
} from '@pcf/core';

import { CommonTitle } from './common-title';
import { TelefoneFormData } from './phone-form';

export interface TokenFormData {
  pin: string;
}

const getTimeMask = (time: number): string => {
  if (time > 9) {
    return `00:${time}`;
  }
  return `00:0${time}`;
};

export const PhoneVerificationCode: FC = () => {
  const { handleSubmit, watch, control } = useForm<TokenFormData>();
  const [isTimeOver, setIsTimeOver] = useState(false);
  const { hideModal } = useModal();
  const toast = useQuickToast();
  const {
    data: { id, tipoSolicitacaoConfirmacao },
    previousStep,
  } = useStepsContainerContext<TelefoneFormData>();

  const {
    mutate: mutatePhoneGetConfirmationTokenResend,
    isLoading: isResending,
  } = usePhoneGetConfirmationTokenResend(id);

  const { mutate: mutateConfirmationToken, isLoading: isLoadingConfirmation } =
    usePhoneConfirmationValidation(id);

  const { time, start } = useTimer({
    initialTime: 59,
    endTime: 0,
    timerType: 'DECREMENTAL',
    onTimeOver: () => {
      setIsTimeOver(true);
    },
  });

  useMount(() => {
    start();
  });

  const pinValue = watch('pin');

  function onSubmit(data: TokenFormData): void {
    mutateConfirmationToken(
      { token: data.pin },
      {
        onSuccess() {
          hideModal();
          toast('Telefone Confirmado!', '', 'success');
        },
      },
    );
  }

  function handleResend(): void {
    mutatePhoneGetConfirmationTokenResend(
      { tipoSolicitacaoConfirmacao },
      {
        onSuccess() {
          setIsTimeOver(false);
          start();
        },
      },
    );
  }

  return (
    <Flex flexDir="column" as="form" onSubmit={handleSubmit(onSubmit)}>
      <CommonTitle />

      <Text mt="34px" textAlign="center">
        Digite seu código de verificação abaixo
      </Text>

      <HStack align="center" justify="center" mt={4}>
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

      <Flex flexDir="column" mt={6}>
        {isTimeOver ? (
          <>
            <Text mt={3} textAlign="center" textStyle="bold14">
              Caso você não tenha recebido o token clique em
            </Text>
            <Button
              my={4}
              variant="link"
              onClick={handleResend}
              isLoading={isResending}
            >
              Reenviar Token
            </Button>
            <Text mt={3} textAlign="center" textStyle="bold14">
              Ou caso preferir, volte uma etapa e selecione outro método de
              confirmação
            </Text>
            <Button my={4} variant="link" onClick={previousStep}>
              Voltar
            </Button>
          </>
        ) : (
          <>
            <Text
              textStyle="bold32"
              textAlign="center"
              color="secondary.mid-light"
            >
              {getTimeMask(time)}
            </Text>

            <Text mt={3} textAlign="center" textStyle="bold14">
              Ao final do tempo, o código deverá ser solicitado novamente
            </Text>
          </>
        )}
      </Flex>

      <Button
        mt={12}
        type="submit"
        isFullWidth
        isLoading={isLoadingConfirmation}
        colorScheme="success"
        disabled={pinValue ? pinValue.length !== 4 : true}
      >
        Validar Token
      </Button>
    </Flex>
  );
};
