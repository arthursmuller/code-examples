import { createQueryFor } from 'utils';

export interface ContratoModel {
  matricula: string;
  contrato: string;
  prestacao: number;
  qtdParcelas: number;
  qtdParcelasPagas: number;
  taxa: number;
  saldoTotal: number;
}

export const CONTRATOS_QUERY_ENDPOINT = 'produtos/consignado/contratos';

export const {
  getQueryConfig: getContratosQueryConfig,
  useQueryOf: useContratosQuery,
} = createQueryFor<ContratoModel[], { matricula?: string }>(
  CONTRATOS_QUERY_ENDPOINT,
);
