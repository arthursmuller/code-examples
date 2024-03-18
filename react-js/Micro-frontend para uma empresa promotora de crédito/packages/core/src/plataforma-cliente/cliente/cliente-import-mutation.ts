import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor } from 'common/client';

import { ClienteExibicaoModel } from './cliente.model';

export function useImportClientData(): UseMutationResult<
  ClienteExibicaoModel,
  Error,
  null,
  null
> {
  return useMutation(
    postQueryFor<null, ClienteExibicaoModel>(
      'clientes/autenticado/solicitacao-importacao-dados',
    ),
  );
}
