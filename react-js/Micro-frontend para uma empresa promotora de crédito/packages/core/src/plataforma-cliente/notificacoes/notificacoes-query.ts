import { createQueryFor } from 'utils';

import { Notificacao } from '.';

export const NOTIFICACOES_QUERY_ENDPOINT = 'clientes/autenticado/notificacoes';

export const {
  getQueryConfig: getNotificacoesQueryConfig,
  useQueryOf: useNotificacoes,
} = createQueryFor<Notificacao[]>(NOTIFICACOES_QUERY_ENDPOINT);
