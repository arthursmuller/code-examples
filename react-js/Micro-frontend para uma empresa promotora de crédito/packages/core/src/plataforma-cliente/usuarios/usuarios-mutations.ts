import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor, simplePutQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

import {
  UsuarioRecuperacaoSenhaRequisicao,
  UsuarioTrocaSenhaRequisicao,
} from './usuarios.models';

import { AutenticacaoModel } from '../login';

export function useRecuperarSenhaMutation(
  token = '',
): UseMutationResult<
  boolean | AutenticacaoModel,
  BemApiErrorResponse,
  UsuarioRecuperacaoSenhaRequisicao,
  UsuarioRecuperacaoSenhaRequisicao
> {
  return useMutation(
    postQueryFor<
      UsuarioRecuperacaoSenhaRequisicao,
      boolean | AutenticacaoModel
    >(`usuarios/recuperacao-senha/${token}`),
    { useErrorBoundary: false },
  );
}

export const trocarSenhaRoute = 'usuarios/autenticado/senha';

export function useAtualizarSenhaMutation(): UseMutationResult<
  boolean,
  BemApiErrorResponse,
  UsuarioTrocaSenhaRequisicao,
  UsuarioTrocaSenhaRequisicao
> {
  return useMutation(
    simplePutQueryFor<UsuarioTrocaSenhaRequisicao, boolean>(trocarSenhaRoute),
  );
}
