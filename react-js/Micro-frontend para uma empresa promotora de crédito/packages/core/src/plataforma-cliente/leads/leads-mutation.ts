import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor, putQueryFor, PutVariables } from 'common/client';

import { LeadCriacaoModel, LeadNovaModel } from './lead.model';

export function useGravarLead(): UseMutationResult<
  LeadNovaModel,
  Error,
  LeadCriacaoModel,
  LeadCriacaoModel
> {
  return useMutation(postQueryFor<LeadCriacaoModel, LeadNovaModel>('leads'));
}

export function useAtualizarLead(): UseMutationResult<
  LeadNovaModel,
  Error,
  PutVariables<LeadCriacaoModel>,
  LeadCriacaoModel
> {
  return useMutation(putQueryFor<LeadCriacaoModel, LeadNovaModel>('leads'));
}
