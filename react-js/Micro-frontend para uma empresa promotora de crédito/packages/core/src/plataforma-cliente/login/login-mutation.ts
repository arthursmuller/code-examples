import {
  useMutation,
  UseMutationOptions,
  UseMutationResult,
} from 'react-query';

import { postQueryFor } from 'common/client';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

import {
  LoginModel,
  AutenticacaoModel,
  AutenticacaoLoginSocialModel,
  LoginSocialEnvioModel,
} from './login.model';

export function useLogin(
  mutationOptions?: UseMutationOptions<
    AutenticacaoModel,
    BemApiErrorResponse,
    LoginModel,
    LoginModel
  >,
): UseMutationResult<
  AutenticacaoModel,
  BemApiErrorResponse,
  LoginModel,
  LoginModel
> {
  return useMutation(
    postQueryFor<LoginModel, AutenticacaoModel>('login'),
    mutationOptions,
  );
}

export function useLoginSocial(
  socialMediaId: number,
  mutationOptions?: UseMutationOptions<
    AutenticacaoLoginSocialModel,
    BemApiErrorResponse,
    LoginSocialEnvioModel,
    LoginSocialEnvioModel
  >,
): UseMutationResult<
  AutenticacaoLoginSocialModel,
  BemApiErrorResponse,
  LoginSocialEnvioModel,
  LoginSocialEnvioModel
> {
  return useMutation(
    postQueryFor<LoginSocialEnvioModel, AutenticacaoLoginSocialModel>(
      `login-social/${socialMediaId}`,
    ),
    mutationOptions,
  );
}
