import { createQueryFor } from 'utils';

import { InformacaoProdutoOperacao } from '../produto';

const baseUrl = 'produtos/consignado/informacoes-produto-operacao';

export const TIPO_OPERACAO_QUERY_ENDPOINT_NOVO = `${baseUrl}/novo`;

export const {
  getQueryConfig: getProdutoOperacaoConsignadoQueryConfig,
  useQueryOf: useProdutoOperacaoConsignadoQuery,
} = createQueryFor<InformacaoProdutoOperacao>(
  TIPO_OPERACAO_QUERY_ENDPOINT_NOVO,
);

export const TIPO_OPERACAO_QUERY_ENDPOINT_REFIN = `${baseUrl}/refin`;

export const {
  getQueryConfig: getProdutoOperacaoRefinQueryConfig,
  useQueryOf: useProdutoOperacaoRefinQuery,
} = createQueryFor<InformacaoProdutoOperacao>(
  TIPO_OPERACAO_QUERY_ENDPOINT_REFIN,
);

export const TIPO_OPERACAO_QUERY_ENDPOINT_PORTABILIDADE = `${baseUrl}/portabilidade`;

export const {
  getQueryConfig: getProdutoOperacaoPortabilidadeQueryConfig,
  useQueryOf: useProdutoOperacaoPortabilidadeQuery,
} = createQueryFor<InformacaoProdutoOperacao>(
  TIPO_OPERACAO_QUERY_ENDPOINT_PORTABILIDADE,
);
