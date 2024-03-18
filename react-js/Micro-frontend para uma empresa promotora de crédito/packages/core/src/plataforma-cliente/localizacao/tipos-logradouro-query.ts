import { createQueryFor } from 'utils';

export interface TipoLogradouroModel {
  id: number;
  descricao?: string;
  codigo?: string;
}

export const {
  getQueryConfig: getTiposLogradouroQueryConfig,
  useQueryOf: useTiposLogradouro,
} = createQueryFor<TipoLogradouroModel[]>('localizacoes/tipos-logradouro');
