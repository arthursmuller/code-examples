import { createQueryFor } from 'utils';

import { Anexo } from './anexo.model';

export const ANEXOS_QUERY_ENDPOINT = 'usuarios/autenticado/anexos';

export const {
  getQueryConfig: getAnexosQueryConfig,
  useQueryOf: useAnexosQuery,
} = createQueryFor<Anexo[]>(ANEXOS_QUERY_ENDPOINT, undefined, {
  skipAlerts: true,
});
