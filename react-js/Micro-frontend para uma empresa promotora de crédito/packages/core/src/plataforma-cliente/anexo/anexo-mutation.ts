import { useMutation, UseMutationResult } from 'react-query';

import { deleteQueryFor, postQueryFor } from 'common/client';

import { Anexo, AnexoCriacaoModel } from './anexo.model';

export const ANEXO_ENDPOINT = 'anexos';

const postQuery = postQueryFor<AnexoCriacaoModel, Anexo>(ANEXO_ENDPOINT);

export function useGravarAnexo(): UseMutationResult<
  Anexo,
  Error,
  AnexoCriacaoModel,
  AnexoCriacaoModel
> {
  return useMutation(postQuery);
}

export function useGravarAnexos(): UseMutationResult<
  Anexo[],
  Error,
  AnexoCriacaoModel[],
  AnexoCriacaoModel[]
> {
  const multipleQuery = (requests: AnexoCriacaoModel[]): Promise<Anexo[]> => {
    const queries = requests.map((r) => postQuery(r));
    return Promise.all(queries);
  };

  return useMutation(multipleQuery);
}

export function useExcluirAnexo({
  id,
}: {
  id: string | number;
}): UseMutationResult<boolean, Error, void, void> {
  const request = deleteQueryFor<boolean>(`${ANEXO_ENDPOINT}/${id}`);
  return useMutation(request);
}
