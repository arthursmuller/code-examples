import { getQueryFor } from 'common/client';
import { createQueryFor } from 'utils';

import {
  SimulacaoNovoRequisicaoModel,
  SimulacaoNovoRetornoModel,
} from './simular-consignado.model';

export const filterUniquePrazoFn = (
  results: SimulacaoNovoRetornoModel[],
  cur: SimulacaoNovoRetornoModel,
): SimulacaoNovoRetornoModel[] =>
  results?.find((r) => r.prazo === parseInt(cur.prazo as string, 10))
    ? results
    : [...results, { ...cur, prazo: parseInt(cur.prazo as string, 10) }];

const queryFn = async (
  queryString?: SimulacaoNovoRequisicaoModel,
  path?: string,
): Promise<SimulacaoNovoRetornoModel[]> => {
  const getSimulation = getQueryFor<
    SimulacaoNovoRetornoModel[],
    SimulacaoNovoRequisicaoModel
  >(path as string, queryString);

  const result = await getSimulation();

  const nextData = result
    .reduce(filterUniquePrazoFn, [])
    .sort((a, b) => (a.prazo as number) - (b.prazo as number));

  return nextData;
};

export const {
  getQueryConfig: getSimularConsignadoQueryConfig,
  useQueryOf: useSimularConsignadoQuery,
} = createQueryFor<SimulacaoNovoRetornoModel[], SimulacaoNovoRequisicaoModel>(
  'produtos/consignado/simulacao-novo',
  queryFn,
);
