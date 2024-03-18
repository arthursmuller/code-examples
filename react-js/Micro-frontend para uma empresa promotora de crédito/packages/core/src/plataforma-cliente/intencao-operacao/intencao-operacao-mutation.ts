import { useMutation, UseMutationResult } from 'react-query';

import { postQueryFor } from 'common/client';

import {
  IntencaoOperacaoNovoRetornoModel,
  IntencaoOperacaoRequisicaoModel,
} from './intencao-operacao.model';

export const INTENCAO_OPERACAO_POST_ENDPOINT = 'intencoes-operacao';

export function useIntencaoOperacaoMutation(): UseMutationResult<
  IntencaoOperacaoNovoRetornoModel[],
  Error,
  IntencaoOperacaoRequisicaoModel,
  IntencaoOperacaoRequisicaoModel
> {
  return useMutation(
    postQueryFor<
      IntencaoOperacaoRequisicaoModel,
      IntencaoOperacaoNovoRetornoModel[]
    >(INTENCAO_OPERACAO_POST_ENDPOINT),
  );
}
