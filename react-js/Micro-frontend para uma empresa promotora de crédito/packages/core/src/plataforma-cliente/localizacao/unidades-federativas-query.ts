import { createQueryFor } from 'utils';

export interface UnidadeFederativaModel {
  id: number;
  nome: string;
  sigla: string;
}

export const STATES_QUERY_ENDPOINT = 'localizacoes/unidades-federativas';

export const {
  getQueryConfig: getUnidadesFederativasQueryConfig,
  useQueryOf: useUnidadesFederativas,
} = createQueryFor<UnidadeFederativaModel[]>(STATES_QUERY_ENDPOINT);
