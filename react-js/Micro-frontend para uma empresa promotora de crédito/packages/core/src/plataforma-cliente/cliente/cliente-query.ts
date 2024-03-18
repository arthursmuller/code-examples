import { createQueryFor } from 'utils';

import { ClienteExibicaoModel } from './cliente.model';

export const CLIENT_QUERY_ENDPOINT = 'clientes/autenticado';

export const {
  getQueryConfig: getClienteLogadoQueryConfig,
  useQueryOf: useClienteLogado,
} = createQueryFor<ClienteExibicaoModel>(CLIENT_QUERY_ENDPOINT);
