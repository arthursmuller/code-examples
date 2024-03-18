import { FC } from 'react';

import { useQueryClient } from 'react-query';
import { useHistory } from 'react-router-dom';

import {
  extractReadableErrorMessage,
  getEnderecosQueryConfig,
  useCreateEndereco,
  useEnderecos,
} from '@pcf/core';
import { useModal, getDefaultErrorModalConfig } from '@pcf/design-system';

import { transformFormDataEnderecoToModel } from './utils/addresses.utils';
import { AddressFormData, AddressForm } from './components/address-form';

import { PerfilRoutesPaths } from '../perfil.routes.enum';

export const CreateAddress: FC = () => {
  const { mutate, isLoading } = useCreateEndereco();
  const queryCache = useQueryClient();
  const { showModal } = useModal();
  const history = useHistory();
  const { data: enderecos } = useEnderecos();

  const anyPrincipal = !!enderecos?.find((endereco) => endereco.principal);

  function handleCreateEndereco(data: AddressFormData): void {
    mutate(transformFormDataEnderecoToModel(data), {
      onSuccess() {
        queryCache.invalidateQueries(getEnderecosQueryConfig().queryKey);

        showModal({
          title: 'Endereço Cadastrado!',
          closeOnClickOverlay: true,
          closeText: 'Ok',
          onClose: () => {
            history.push(PerfilRoutesPaths.enderecos);
          },
        });
      },
      onError(error) {
        showModal(
          getDefaultErrorModalConfig({
            information: extractReadableErrorMessage(error),
          }),
        );
      },
    });
  }

  return (
    <AddressForm
      onSuccess={handleCreateEndereco}
      isSubmiting={isLoading}
      hasToBePrincipal={!anyPrincipal}
      showNewLink={false}
    />
  );
};
