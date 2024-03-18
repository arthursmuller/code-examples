import { createQueryFor } from 'utils';

export interface GeneroModel {
  id: number;
  sigla: string;
  descricao: string;
}

export const GENDER_QUERY_ENDPOINT = 'clientes/informacoes/generos';

export const { getQueryConfig: getGenerosQueryConfig, useQueryOf: useGeneros } =
  createQueryFor<GeneroModel[]>(GENDER_QUERY_ENDPOINT);
