import { createQueryFor } from 'utils';

import { MunicipioModel } from '../localizacao/municipios-query';

interface TipoLogradouroModel {
  id: number;
  descricao?: string;
  codigo?: string;
}

export interface EnderecoClienteExibicaoModel {
  id: number;
  titulo?: string;
  municipio?: MunicipioModel;
  bairro?: string;
  tipoLogradouro?: TipoLogradouroModel;
  logradouro?: string;
  numero?: number;
  complemento?: string;
  cep?: string;
  principal: boolean;
}

export const {
  getQueryConfig: getEnderecosQueryConfig,
  useQueryOf: useEnderecos,
} = createQueryFor<EnderecoClienteExibicaoModel[]>(
  'clientes/autenticado/enderecos',
);
