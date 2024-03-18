import { createQueryFor } from 'utils';

export interface TipoContaModel {
  id: number;
  nome?: string;
  sigla?: string;
}

export const {
  getQueryConfig: getTiposContaQueryConfig,
  useQueryOf: useTiposConta,
} = createQueryFor<TipoContaModel[]>('bancarios/tipos-conta');
