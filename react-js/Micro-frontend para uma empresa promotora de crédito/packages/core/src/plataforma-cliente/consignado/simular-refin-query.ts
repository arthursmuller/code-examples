import { postQueryFor } from 'common/client';
import { createQueryFor } from 'utils';

import { SimulacaoNovoRetornoModel } from './simular-consignado.model';

export interface SimulacaoNovoRefinRequisicaoModel {
  idConvenio: number;
  valorOperacao?: number;
  prestacao?: number;
  plano?: string;
  prazo?: string;
  prazos: number[];
  retornarSomenteOperacoesViaveis: boolean;
  proposta?: number;
  contratosRefinanciamento: { contrato: string }[];
}

const queryFn = async (
  request?: SimulacaoNovoRefinRequisicaoModel,
  path?: string,
): Promise<SimulacaoNovoRetornoModel[]> => {
  const query = postQueryFor<
    SimulacaoNovoRefinRequisicaoModel,
    SimulacaoNovoRetornoModel[]
  >(path as string);

  const result = await query(request as SimulacaoNovoRefinRequisicaoModel);

  const nextData = result
    .map((r) => ({ ...r, prazo: +r.prazo }))
    .sort((a, b) => (a.prazo as number) - (b.prazo as number));

  return nextData;
};

export const REFIN_QUERY_ENDPOINT =
  'produtos/consignado/simulacao-refinanciamento';

export const {
  getQueryConfig: getSimularRefinQueryConfig,
  useQueryOf: useSimularRefinQuery,
} = createQueryFor<
  SimulacaoNovoRetornoModel[],
  SimulacaoNovoRefinRequisicaoModel
>(REFIN_QUERY_ENDPOINT, queryFn);
