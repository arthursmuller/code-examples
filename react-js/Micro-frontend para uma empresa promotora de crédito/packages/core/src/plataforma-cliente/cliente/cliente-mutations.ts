import { useMutation, UseMutationResult } from 'react-query';

import { simplePutQueryFor } from 'common/client';

import { ClienteModel, ClienteExibicaoModel } from './cliente.model';

export const CLIENT_MUTATION_ENDPOINT = 'clientes/autenticado';

export function useAtualizarCliente(): UseMutationResult<
  ClienteExibicaoModel,
  Error,
  ClienteModel,
  ClienteModel
> {
  return useMutation(
    simplePutQueryFor<ClienteModel, ClienteExibicaoModel>(
      CLIENT_MUTATION_ENDPOINT,
    ),
  );
}
