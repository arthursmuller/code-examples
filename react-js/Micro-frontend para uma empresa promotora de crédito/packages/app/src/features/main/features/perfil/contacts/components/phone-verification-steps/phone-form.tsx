import React, { FC } from 'react';

import { Flex, Button } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  PhoneInput,
  TelefoneClienteExibicaoModel,
  useStepsContainerContext,
} from '@pcf/design-system';
import { TipoSolicitacaoConfirmacao, useCreatePhones } from '@pcf/core';

import { CommonTitle } from './common-title';

import { transformFormDataToModel } from '../../utils/contacts.utils';

export interface TelefoneFormData {
  phone: string;
  id?: number;
  tipoSolicitacaoConfirmacao?: TipoSolicitacaoConfirmacao;
}

export interface FormModalTelefoneProps {
  phone?: TelefoneClienteExibicaoModel;
}

export const PhoneForm: FC<FormModalTelefoneProps> = ({ phone }) => {
  const { control, formState, handleSubmit } = useForm<TelefoneFormData>({
    mode: 'onChange',
  });
  const { nextStep } = useStepsContainerContext<TelefoneFormData>();
  const { mutate, isLoading } = useCreatePhones();

  const { errors } = formState;

  function onSubmit(data: TelefoneFormData): void {
    const dddAndPhone = transformFormDataToModel(data.phone);

    mutate(
      { ...phone, ...dddAndPhone },
      {
        onSuccess() {
          nextStep({ phone: data.phone });
        },
      },
    );
  }

  return (
    <Flex as="form" onSubmit={handleSubmit(onSubmit)} direction="column">
      <Flex mb={4} flexDirection="column">
        <CommonTitle
          phone={phone}
          subTitle="Insira seu telefone para contato"
        />
      </Flex>

      <PhoneInput
        defaultValue={phone?.fone ? `${phone.ddd}${phone.fone}` : ''}
        background="white"
        label="Telefone"
        name="phone"
        errorMessage={errors?.phone?.message}
        control={control}
      />

      <Button
        mt={14}
        isFullWidth
        isLoading={isLoading}
        colorScheme="secondary"
        type="submit"
      >
        Continuar
      </Button>
    </Flex>
  );
};
