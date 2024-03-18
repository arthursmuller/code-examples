import { ComposedPath, createQueryFor } from 'utils';

import { IntencaoOperacaoModel } from './intencao-operacao.model';

export const INTENCAO_OPERACAO_ENDPOINT = 'clientes/autenticado/intencoes-operacao';

export const {
  getQueryConfig: getIntencoesOperacaoQueryConfig,
  useQueryOf: useIntencoesOperacao,
} = createQueryFor<IntencaoOperacaoModel[]>(INTENCAO_OPERACAO_ENDPOINT);

export const intencaoOperacaoByIdEndpointGen: ComposedPath<{ id: string }> = (
  qs,
) => [`${INTENCAO_OPERACAO_ENDPOINT}${qs?.id ? `/${qs?.id}` : ''}`, qs];

export const {
  getQueryConfig: getIntencaoOperacaoQueryConfig,
  useQueryOf: useIntencaoOperacao,
} = createQueryFor<IntencaoOperacaoModel, { id: string }>(
  intencaoOperacaoByIdEndpointGen,
  undefined,
);
