import { getQueryFor } from 'common/client';
import { createQueryFor } from 'utils';

import { MunicipioModel } from './municipios-query';
import { TipoLogradouroModel } from './tipos-logradouro-query';
import { UnidadeFederativaModel } from './unidades-federativas-query';

export interface CepModel {
  id: number;
  estado?: UnidadeFederativaModel;
  cidade?: MunicipioModel;
  tipoLogradouro?: TipoLogradouroModel;
  logradouro?: string;
  bairro?: string;
  cep?: string;
  descricaoApoio?: string;
  permiteEditarLogradouro: boolean;
}

export const getEndereco = (codigo: string): Promise<CepModel> =>
  getQueryFor<CepModel>(`/localizacoes/cep/${codigo}`)();

export const FIND_CEP_QUERY_ENDPOINT = 'localizacoes/ceps';

export interface FindCepQuery {
  idUnidadeFederativa?: number;
  idTipoLogradouro?: number;
  idMunicipio?: number;
  logradouro?: string;
  bairro?: string;
}

export const { getQueryConfig: getFindCepQueryConfig, useQueryOf: useFindCep } =
  createQueryFor<CepModel[], FindCepQuery>(FIND_CEP_QUERY_ENDPOINT);
