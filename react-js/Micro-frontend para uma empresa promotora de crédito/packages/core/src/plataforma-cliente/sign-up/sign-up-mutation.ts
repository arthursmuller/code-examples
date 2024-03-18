import {
  useMutation,
  UseMutationOptions,
  UseMutationResult,
} from 'react-query';

import { postQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

import { UsuarioCriacaoModel, UsuarioModel } from '.';

export function useSignUpMutation(
  mutationOptions?: UseMutationOptions<
    UsuarioModel,
    BemApiErrorResponse,
    UsuarioCriacaoModel,
    UsuarioCriacaoModel
  >,
): UseMutationResult<
  UsuarioModel,
  BemApiErrorResponse,
  UsuarioCriacaoModel,
  UsuarioCriacaoModel
> {
  return useMutation(
    postQueryFor<UsuarioCriacaoModel, UsuarioModel>('usuarios'),
    mutationOptions,
  );
}
