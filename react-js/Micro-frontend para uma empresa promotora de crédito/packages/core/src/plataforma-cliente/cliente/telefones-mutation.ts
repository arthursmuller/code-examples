import { useMutation, UseMutationResult, useQueryClient } from 'react-query';

import { deleteQueryFor, patchQueryFor, postQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

import {
  TelefoneClienteModel,
  TELEFONES_QUERY_ENDPOINT,
} from './telefones-query';

interface TelefoneClienteSolicitacaoConfirmacaoModel {
  id: number;
  solicitacaoEnviada: boolean;
}
interface TelefoneClienteConfirmacaoToken {
  token: string;
}

export enum TipoSolicitacaoConfirmacao {
  Sms = 1,
  WhatsApp = 2,
  Telefonema = 3,
}

interface TelefoneClienteSolicitacaoConfirmacaoEnvioModel {
  tipoSolicitacaoConfirmacao: TipoSolicitacaoConfirmacao;
}

export const useCreatePhones = (): UseMutationResult<
  boolean,
  BemApiErrorResponse,
  TelefoneClienteModel,
  TelefoneClienteModel
> => {
  const queryClient = useQueryClient();

  return useMutation(
    postQueryFor<TelefoneClienteModel, boolean>(TELEFONES_QUERY_ENDPOINT),
    {
      onSuccess() {
        queryClient.invalidateQueries(TELEFONES_QUERY_ENDPOINT);
      },
    },
  );
};

export const usePhoneGetConfirmationToken = (
  idPhone: number,
): UseMutationResult<
  TelefoneClienteSolicitacaoConfirmacaoModel,
  BemApiErrorResponse,
  TelefoneClienteSolicitacaoConfirmacaoEnvioModel,
  TelefoneClienteSolicitacaoConfirmacaoEnvioModel
> => {
  return useMutation(
    postQueryFor<
      TelefoneClienteSolicitacaoConfirmacaoEnvioModel,
      TelefoneClienteSolicitacaoConfirmacaoModel
    >(`/telefones/${idPhone}/solicitacoes-confirmacao`),
  );
};

export const usePhoneGetConfirmationTokenResend = (
  idPhone: number,
): UseMutationResult<
  TelefoneClienteSolicitacaoConfirmacaoModel,
  BemApiErrorResponse,
  TelefoneClienteSolicitacaoConfirmacaoEnvioModel,
  TelefoneClienteSolicitacaoConfirmacaoEnvioModel
> => {
  return useMutation(
    patchQueryFor<
      TelefoneClienteSolicitacaoConfirmacaoEnvioModel,
      TelefoneClienteSolicitacaoConfirmacaoModel
    >(`/telefones/${idPhone}/solicitacoes-confirmacao/reenvio`),
  );
};

export const usePhoneConfirmationValidation = (
  idPhone: number,
): UseMutationResult<
  boolean,
  BemApiErrorResponse,
  TelefoneClienteConfirmacaoToken,
  TelefoneClienteConfirmacaoToken
> => {
  const queryClient = useQueryClient();

  return useMutation(
    patchQueryFor<TelefoneClienteConfirmacaoToken, boolean>(
      `/telefones/${idPhone}/confirmacoes`,
    ),
    {
      onSuccess() {
        queryClient.invalidateQueries(TELEFONES_QUERY_ENDPOINT);
      },
    },
  );
};

export const useDeletePhone = (): UseMutationResult<
  boolean,
  Error,
  number,
  number
> => {
  const queryClient = useQueryClient();

  return useMutation(
    async (id: number) => {
      return deleteQueryFor<boolean>(`${TELEFONES_QUERY_ENDPOINT}/${id}`)();
    },
    {
      onSuccess() {
        queryClient.invalidateQueries(TELEFONES_QUERY_ENDPOINT);
      },
    },
  );
};
