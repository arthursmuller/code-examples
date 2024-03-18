import { createQueryFor } from 'utils';

import { LojaModel } from './loja.model';

const LOJA_ENDPOINT = 'lojas';

export const { getQueryConfig: getLojasQueryConfig, useQueryOf: useLojas } =
  createQueryFor<LojaModel[], { idUf?: number }>(LOJA_ENDPOINT);
