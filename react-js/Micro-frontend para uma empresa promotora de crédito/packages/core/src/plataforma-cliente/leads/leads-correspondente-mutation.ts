import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor } from 'common/client';

export interface LeadCorrespondenteCriacaoModel {
  cnpj: string;
  nome: string;
  telefone: string;
  email: string;
  idMunicipio: number;
  atividades: string;
}

export function useGravarLeadCorrespondente(): UseMutationResult<
  number,
  Error,
  LeadCorrespondenteCriacaoModel,
  LeadCorrespondenteCriacaoModel
> {
  return useMutation(
    postQueryFor<LeadCorrespondenteCriacaoModel, number>(
      'leads-correspondente',
    ),
  );
}
