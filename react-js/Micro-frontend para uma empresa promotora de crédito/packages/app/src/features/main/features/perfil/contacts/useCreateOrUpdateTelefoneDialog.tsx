import React, { FC } from 'react';

import { Flex, Button, Text, useToast } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  ActionDialogContainer,
  ActionDialogContent,
  ActionDialogHeader,
  CustomHeading,
  PhoneInput,
  rightToLeft,
  TelefoneClienteExibicaoModel,
  useModal,
  useQuickToast,
} from '@pcf/design-system';
import { extractReadableErrorMessage, useCreatePhones } from '@pcf/core';

import { transformFormDataToModel } from './utils/contacts.utils';

interface TelefoneFormData {
  phone: string;
}

const FormModalTelefone: FC<{
  hideModal: () => void;
  phone?: TelefoneClienteExibicaoModel;
}> = ({ hideModal, phone }) => {
  const { control, formState, handleSubmit } = useForm<TelefoneFormData>({
    mode: 'onChange',
  });
  const { errors } = formState;

  const { mutate, isLoading } = useCreatePhones();
  const toast = useQuickToast();
  const { closeAll } = useToast();

  function onSubmit(data: TelefoneFormData): void {
    mutate(
      { ...phone, ...transformFormDataToModel(data.phone) },
      {
        onSuccess() {
          closeAll();
          hideModal();
          toast('Alterações salvas!', '', 'success');
        },
        onError(error) {
          closeAll();
          toast('Ocorreu um erro!', extractReadableErrorMessage(error));
        },
      },
    );
  }

  return (
    <ActionDialogContainer>
      <ActionDialogHeader onClose={hideModal} />
      <ActionDialogContent>
        <Flex
          as="form"
          onSubmit={handleSubmit(onSubmit)}
          direction="column"
          animation={`250ms ${rightToLeft} ease-in-out`}
        >
          <CustomHeading
            mb={phone?.fone ? 8 : 2}
            textStyle="bold24"
            color="secondary.regular"
            textAlign="center"
          >
            {phone?.fone ? 'Editar Telefone' : 'Cadastrar Telefone'}
          </CustomHeading>

          {!phone?.fone && (
            <Text color="secondary.regular" mb={4} textAlign="center">
              Insira seu telefone para contato
            </Text>
          )}

          <PhoneInput
            defaultValue={phone?.fone ? `${phone.ddd}${phone.fone}` : ''}
            background="white"
            label="Telefone"
            name="phone"
            errorMessage={errors?.phone?.message}
            control={control}
          />

          <Button
            isLoading={isLoading}
            mt={6}
            isFullWidth
            colorScheme="success"
            type="submit"
          >
            Salvar
          </Button>
        </Flex>
      </ActionDialogContent>
    </ActionDialogContainer>
  );
};

export const useCreateOrUpdateTelefoneDialog = (): {
  open: (phone?: TelefoneClienteExibicaoModel) => void;
} => {
  const { showModal, hideModal } = useModal();

  function open(phone?: TelefoneClienteExibicaoModel): void {
    showModal({
      closeOnClickOverlay: false,
      modal: <FormModalTelefone phone={phone} hideModal={hideModal} />,
    });
  }

  return { open };
};
