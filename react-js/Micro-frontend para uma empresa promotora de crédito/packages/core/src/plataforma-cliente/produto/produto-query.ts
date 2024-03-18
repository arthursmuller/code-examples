import { createQueryFor } from 'utils';

import { ProdutoModel } from './produto.model';

export const {
  getQueryConfig: getProdutosQueryConfig,
  useQueryOf: useProdutos,
} = createQueryFor<ProdutoModel[]>('produtos');
