import { createQueryFor } from 'utils';

import { ConvenioModel } from './convenio.model';

export const {
  getQueryConfig: getConveniosQueryConfig,
  useQueryOf: useConvenios,
} = createQueryFor<ConvenioModel[]>('convenios');
