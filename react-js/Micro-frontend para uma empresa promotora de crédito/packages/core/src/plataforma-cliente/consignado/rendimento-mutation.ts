import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor, putQueryFor } from 'common/client';

import { RendimentoResponseModel } from './rendimentos-query';

export interface RendimentoPersistModel {
  convenio: number;
  matricula: string;
  idConvenioOrgao: number;
  idUf: number;
  valorRendimento: number;
  idInssEspecieBeneficio: number;
  idSiapeTipoFuncional: number;
  dataInscricaoBeneficio: Date;
  dataAdmissao: Date;
  matriculaInstituidor: string;
  nomeInstituidor: string;
  possuiRepresentacaoPorProcurador: boolean | null;
  contaCliente: ContaClientePersistModel;
  contaClienteRecebimento: ContaClientePersistModel;
}

export interface ContaClientePersistModel {
  idContaCliente?: number;
  idBanco: number;
  idTipoConta: number;
  idFormaRecebimento?: number;
  agencia: string;
  conta: string;
}

export function useRendimentoMutation(
  id?: number,
): UseMutationResult<
  RendimentoResponseModel,
  Error,
  RendimentoPersistModel,
  RendimentoPersistModel
> {
  const query = !id
    ? postQueryFor<RendimentoPersistModel, RendimentoResponseModel>(
        'clientes/autenticado/rendimentos',
      )
    : (data: RendimentoPersistModel) =>
        putQueryFor<RendimentoPersistModel, RendimentoResponseModel>(
          'clientes/autenticado/rendimentos/',
        )({ data, id });

  return useMutation(query);
}
