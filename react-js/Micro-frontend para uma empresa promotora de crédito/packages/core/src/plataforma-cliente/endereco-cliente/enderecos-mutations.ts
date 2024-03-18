import { useMutation, UseMutationResult } from 'react-query';

import { simplePutQueryFor, postQueryFor } from 'common/client';

import { EnderecoClienteExibicaoModel } from './enderecos-query';

export interface EnderecoClienteModel {
  titulo?: string;
  idMunicipio: number;
  bairro?: string;
  idTipoLogradouro: number;
  logradouro: string;
  numero?: number;
  complemento?: string;
  cep?: string;
  principal: boolean;
}

export function useCreateEndereco(): UseMutationResult<
  EnderecoClienteExibicaoModel,
  Error,
  EnderecoClienteModel,
  EnderecoClienteModel
> {
  return useMutation(
    postQueryFor<EnderecoClienteModel, EnderecoClienteExibicaoModel>(
      'clientes/autenticado/enderecos',
    ),
  );
}

export function useUpdateEndereco(
  id: string,
): UseMutationResult<
  EnderecoClienteExibicaoModel,
  Error,
  EnderecoClienteModel,
  EnderecoClienteModel
> {
  return useMutation(
    simplePutQueryFor<EnderecoClienteModel, EnderecoClienteExibicaoModel>(
      `clientes/autenticado/enderecos/${id}`,
    ),
  );
}
