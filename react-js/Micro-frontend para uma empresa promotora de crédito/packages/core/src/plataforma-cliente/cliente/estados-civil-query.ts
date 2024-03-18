import { createQueryFor } from 'utils';

export interface EstadoCivilModel {
  id: number;
  sigla: string;
  descricao: string;
}

export const CIVIL_STATE_QUERY_ENDPOINT = 'clientes/informacoes/estados-civil';

export const {
  getQueryConfig: getEstadosCivilQueryConfig,
  useQueryOf: useEstadosCivil,
} = createQueryFor<EstadoCivilModel[]>(CIVIL_STATE_QUERY_ENDPOINT);
