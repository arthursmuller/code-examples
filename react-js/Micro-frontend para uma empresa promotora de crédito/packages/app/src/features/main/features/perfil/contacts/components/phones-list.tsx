import { FC } from 'react';

import { Wrap, Icon, Text, Button, Tooltip, useToast } from '@chakra-ui/react';

import {
  useModal,
  ColorSchemes,
  AddButton,
  DataDisplayCard,
  getFormattedPhone,
  useQuickToast,
} from '@pcf/design-system';
import {
  extractReadableErrorMessage,
  TelefoneClienteExibicaoModel,
  useDeletePhone,
} from '@pcf/core';
import { ActionEditIcon, ActionTrashIcon } from '@pcf/design-system-icons';
import { useFeatureFlags } from 'app';

import { PhoneVerificationBagde } from './phone-verification-bagde';

import { useCreateOrUpdateTelefoneDialog } from '../useCreateOrUpdateTelefoneDialog';
import { useCreateOrUpdateTelefoneDialogWithVerification } from '../useCreateOrUpdateTelefoneDialogWithVerification';

interface PhonesFormProps {
  phones?: TelefoneClienteExibicaoModel[];
}

export const PhonesList: FC<PhonesFormProps> = ({ phones = [] }) => {
  const { showModal } = useModal();
  const { open } = useCreateOrUpdateTelefoneDialog();
  const { open: openWithVerification } =
    useCreateOrUpdateTelefoneDialogWithVerification();
  const { mutateAsync } = useDeletePhone();
  const toast = useQuickToast();
  const { closeAll } = useToast();
  const { flags } = useFeatureFlags();

  const mainPhone = phones.find((phone) => phone.principal);
  const secondaryPhone = phones.find((phone) => !phone.principal);

  function handleRemovePhone(phone: TelefoneClienteExibicaoModel): void {
    showModal({
      title: 'Excluir informação?',
      information: `Deseja excluir o telefone ${getFormattedPhone(phone)}?`,
      type: ColorSchemes.error,
      closeOnClickOverlay: false,
      onClose: () => {},
      confirmText: 'Excluir',
      closeText: 'Cancelar',
      onConfirm: async () => {
        await mutateAsync(phone.id, {
          onSuccess() {
            closeAll();
            toast('Telefone foi excluído com sucesso!', '', 'success');
          },
          onError(error) {
            toast('Ocorreu um erro!', extractReadableErrorMessage(error));
          },
        });
      },
    });
  }

  return (
    <Wrap spacing={8} mt={6} w="100%" align="center">
      {mainPhone?.fone ? (
        <DataDisplayCard w="100%">
          <DataDisplayCard.Header
            justifyContent="space-between"
            alignItems="center"
          >
            <Text textStyle="bold16" color="white">
              Principal
            </Text>

            {flags?.TELEFONE_CRIACAO_VALIDACAO && (
              <PhoneVerificationBagde
                onVerify={() => openWithVerification(mainPhone)}
                phone={mainPhone}
              />
            )}
          </DataDisplayCard.Header>
          <DataDisplayCard.Content>
            <Text>{getFormattedPhone(mainPhone)}</Text>
          </DataDisplayCard.Content>
          <DataDisplayCard.Footer>
            <Button
              variant="link"
              size="sm"
              mr={6}
              colorScheme="secondary"
              onClick={() =>
                flags?.TELEFONE_CRIACAO_VALIDACAO
                  ? openWithVerification(mainPhone)
                  : open(mainPhone)
              }
              rightIcon={
                <Icon
                  as={ActionEditIcon}
                  color="grey.900"
                  w="12.47px"
                  h="12.47px"
                />
              }
            >
              Editar
            </Button>

            <Tooltip
              textAlign="center"
              label="Você não pode excluir o telefone principal. Para deletá-lo, selecione outro endereço como principal."
              placement="bottom"
              hasArrow
              shouldWrapChildren
            >
              <Button
                variant="link"
                size="sm"
                colorScheme="error"
                disabled
                onClick={() => {}}
                rightIcon={
                  <Icon
                    as={ActionTrashIcon}
                    w="12.09px"
                    h="13.33px"
                    color="grey.900"
                  />
                }
              >
                Excluir
              </Button>
            </Tooltip>
          </DataDisplayCard.Footer>
        </DataDisplayCard>
      ) : (
        <AddButton
          onClick={() =>
            flags?.TELEFONE_CRIACAO_VALIDACAO ? openWithVerification() : open()
          }
          text="Cadastre o seu telefone principal"
        />
      )}

      {!!mainPhone?.fone && (
        <>
          {secondaryPhone?.fone ? (
            <DataDisplayCard w="100%">
              <DataDisplayCard.Header
                bg="secondary.mid-light"
                justifyContent="space-between"
                alignItems="center"
              >
                <Text textStyle="bold16" color="white">
                  Alternativo
                </Text>

                {flags?.TELEFONE_CRIACAO_VALIDACAO && (
                  <PhoneVerificationBagde
                    onVerify={() => openWithVerification(secondaryPhone)}
                    phone={secondaryPhone}
                  />
                )}
              </DataDisplayCard.Header>
              <DataDisplayCard.Content>
                <Text>{getFormattedPhone(secondaryPhone)}</Text>
              </DataDisplayCard.Content>
              <DataDisplayCard.Footer>
                <Button
                  variant="link"
                  size="sm"
                  mr={6}
                  colorScheme="secondary"
                  onClick={() =>
                    flags?.TELEFONE_CRIACAO_VALIDACAO
                      ? openWithVerification(secondaryPhone)
                      : open(secondaryPhone)
                  }
                  rightIcon={
                    <Icon
                      as={ActionEditIcon}
                      color="grey.900"
                      w="12.47px"
                      h="12.47px"
                    />
                  }
                >
                  Editar
                </Button>
                <Button
                  variant="link"
                  size="sm"
                  colorScheme="error"
                  onClick={() => handleRemovePhone(secondaryPhone)}
                  rightIcon={
                    <Icon
                      as={ActionTrashIcon}
                      w="12.09px"
                      h="13.33px"
                      color="grey.900"
                    />
                  }
                >
                  Excluir
                </Button>
              </DataDisplayCard.Footer>
            </DataDisplayCard>
          ) : (
            <AddButton
              onClick={() =>
                flags?.TELEFONE_CRIACAO_VALIDACAO
                  ? openWithVerification()
                  : open()
              }
              text="Cadastre o seu telefone alternativo"
            />
          )}
        </>
      )}
    </Wrap>
  );
};
