import { createQueryFor } from 'utils';

export interface TelefoneClienteExibicaoModel {
  id: number;
  idCliente: number;
  ddd?: string;
  fone?: string;
  principal: boolean;
  confirmado: boolean;

  telefone?: string;
}

export const TELEFONES_QUERY_ENDPOINT = 'clientes/autenticado/telefones';

export interface TelefoneClienteModel {
  id?: number | string | null;
  ddd?: string;
  fone?: string;
  principal?: boolean;
}

export const {
  getQueryConfig: getTelefonesQueryConfig,
  useQueryOf: useTelefones,
} = createQueryFor<TelefoneClienteExibicaoModel[]>(TELEFONES_QUERY_ENDPOINT);
