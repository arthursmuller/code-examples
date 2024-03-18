import {
  useMutation,
  UseMutationOptions,
  UseMutationResult,
} from 'react-query';

import { postQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

export interface SolicitacaoAutorizacaoConsultaBeneficioEnvioModel {
  latitude: number | null;
  longitude: number | null;
  idTelefoneEnvioSolicitacao: number;
}

export interface SolicitacaoAutorizacaoConsultaBeneficioModel {
  idConsultaBeneficio: number;
}

export function useBeneficioInssAutorizacaoMutation(
  mutationOptions?: UseMutationOptions<
    SolicitacaoAutorizacaoConsultaBeneficioModel,
    BemApiErrorResponse,
    SolicitacaoAutorizacaoConsultaBeneficioEnvioModel,
    SolicitacaoAutorizacaoConsultaBeneficioEnvioModel
  >,
): UseMutationResult<
  SolicitacaoAutorizacaoConsultaBeneficioModel,
  BemApiErrorResponse,
  SolicitacaoAutorizacaoConsultaBeneficioEnvioModel,
  SolicitacaoAutorizacaoConsultaBeneficioEnvioModel
> {
  return useMutation(
    postQueryFor<
      SolicitacaoAutorizacaoConsultaBeneficioEnvioModel,
      SolicitacaoAutorizacaoConsultaBeneficioModel
    >('/cliente/autenticado/beneficios-inss/autorizacoes'),
    mutationOptions,
  );
}
