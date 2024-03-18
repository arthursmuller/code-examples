import { FC } from 'react';

import { Redirect, useHistory, useParams } from 'react-router-dom';
import { useQueryClient } from 'react-query';

import {
  extractReadableErrorMessage,
  getEnderecosQueryConfig,
  useEnderecos,
  useUpdateEndereco,
} from '@pcf/core';
import {
  Loader,
  useModal,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';

import {
  transformFormDataEnderecoToModel,
  transformModelEnderecoToFormData,
} from './utils/addresses.utils';
import { AddressFormData, AddressForm } from './components/address-form';

export const EditAddress: FC = () => {
  const { data: enderecos, isLoading: isLoadingEnderecos } = useEnderecos();
  const { enderecoId: enderecoIdParam } = useParams<{ enderecoId: string }>();
  const { mutate, isLoading } = useUpdateEndereco(enderecoIdParam);
  const queryCache = useQueryClient();
  const history = useHistory();
  const { showModal } = useModal();

  if (isLoadingEnderecos) {
    return <Loader />;
  }

  const initialEndereco = enderecos?.find(
    ({ id }) => `${id}` === enderecoIdParam,
  );

  if (!initialEndereco) {
    return <Redirect to="/perfil/enderecos" />;
  }

  const initialData: AddressFormData =
    transformModelEnderecoToFormData(initialEndereco);

  function handleEditEndereco(data: AddressFormData): void {
    mutate(transformFormDataEnderecoToModel(data), {
      onSuccess() {
        queryCache.invalidateQueries(getEnderecosQueryConfig().queryKey);

        showModal({
          title: 'Endereço editado!',
          closeOnClickOverlay: true,
          closeText: 'Ok',
          onClose: () => {
            history.push('/perfil/enderecos');
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
      onSuccess={handleEditEndereco}
      isSubmiting={isLoading}
      initialData={initialData}
      hasToBePrincipal={initialEndereco.principal}
    />
  );
};
