import { getQueryFor } from 'common/client';
import { createQueryFor } from 'utils';

import { SimulacaoNovoRetornoModel } from './simular-consignado.model';

export interface PortabilidadeRequisicaoModel {
  idRendimentoCliente: number;
  prazoRestante: number;
  prazoTotal: number;
  saldoDevedor: number;
  valorPrestacaoPortabilidade: number;

  valorPrestacaoRefin?: number;
  prazoRefin?: number[];
  prazos: number | number[];
}

export interface PortabilidadeData {
  portavel: boolean;
  mensagem: string;
  parcelaMinima: number;
  tir: number;
  valorRCOBruto: number;
  valorRCOLiquido: number;
  saldoDevedorCorrigido: number;
  primeiroVcto: string;
  emissao: string;
  valorOperacao: number;
  prestacao: number;
  valorIOF: number;
  valorFinanciado: number;
  valorTotal: number;
  taxaMes: number;
  taxaAno: number;
  cetMes: number;
  cetAno: number;
}
export interface PortabilidadeRetornoModel {
  viabilidade: PortabilidadeData;
  simulacoesIntencaoRefinanciamento?: SimulacaoNovoRetornoModel[];
}

const filterUniquePrazoFn = (
  results: SimulacaoNovoRetornoModel[],
  cur: SimulacaoNovoRetornoModel,
): SimulacaoNovoRetornoModel[] =>
  results?.find((r) => r.prazo === parseInt(cur.prazo as string, 10))
    ? results
    : [...results, { ...cur, prazo: parseInt(cur.prazo as string, 10) }];

const queryFn = async (
  queryString?: PortabilidadeRequisicaoModel,
  path?: string,
): Promise<PortabilidadeRetornoModel> => {
  const getSimulation = getQueryFor<
    PortabilidadeRetornoModel,
    PortabilidadeRequisicaoModel
  >(path as string, queryString);

  const result = await getSimulation();

  const nextData = {
    ...result,
    simulacoesIntencaoRefinanciamento: result.simulacoesIntencaoRefinanciamento
      .filter((s) => s.operacaoViavel)
      ?.reduce(filterUniquePrazoFn, [])
      .sort((a, b) => (a.prazo as number) - (b.prazo as number)),
  };

  return nextData;
};

export const {
  getQueryConfig: getPortabilidadeSimulacaoQueryConfig,
  useQueryOf: usePortabilidadeSimulacaoQuery,
} = createQueryFor<PortabilidadeRetornoModel, PortabilidadeRequisicaoModel>(
  '/produtos/consignado/simulacao-portabilidade',
  queryFn,
);
