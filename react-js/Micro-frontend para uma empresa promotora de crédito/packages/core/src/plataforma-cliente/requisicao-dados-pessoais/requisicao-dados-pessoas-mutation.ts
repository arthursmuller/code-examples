import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor } from 'common';

export const REQUISICAO_DADOS_PESSOAS_ENDPOINT =
  'dados-pessoais/solicitacao-acesso';

export interface RequisicaoDadosPessoaisModel {
  nome: string;
  sobrenome: string;
  dataNascimento: Date;
  nomeMae: string;
  email: string;
  telefoneCompleto: {
    ddd: string;
    telefone: string;
  };
  motivo: string;
}

export function useRequisicaoDadosPessoasMutation(): UseMutationResult<
  null,
  Error,
  RequisicaoDadosPessoaisModel,
  RequisicaoDadosPessoaisModel
> {
  return useMutation(
    postQueryFor<RequisicaoDadosPessoaisModel, null>(
      REQUISICAO_DADOS_PESSOAS_ENDPOINT,
    ),
  );
}
