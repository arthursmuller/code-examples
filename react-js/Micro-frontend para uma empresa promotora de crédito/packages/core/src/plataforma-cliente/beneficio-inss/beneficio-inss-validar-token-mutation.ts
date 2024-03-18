import {
  useMutation,
  UseMutationOptions,
  UseMutationResult,
} from 'react-query';

import { postQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

export interface ValidacaoTokenBeneficioInssPayload {
  idConsultaBeneficio: number;
  tokenConsulta: string;
}

export interface ValidacaoTokenBeneficioInssResponse {
  tokenEhValido: boolean;
  chaveAutorizacao: string;
}

export function useBeneficioInssValidarTokenMutation(
  mutationOptions?: UseMutationOptions<
    ValidacaoTokenBeneficioInssResponse,
    BemApiErrorResponse,
    ValidacaoTokenBeneficioInssPayload,
    ValidacaoTokenBeneficioInssPayload
  >,
): UseMutationResult<
  ValidacaoTokenBeneficioInssResponse,
  BemApiErrorResponse,
  ValidacaoTokenBeneficioInssPayload,
  ValidacaoTokenBeneficioInssPayload
> {
  return useMutation(
    postQueryFor<
      ValidacaoTokenBeneficioInssPayload,
      ValidacaoTokenBeneficioInssResponse
    >('/cliente/autenticado/beneficios-inss/validacao-token'),
    mutationOptions,
  );
}
