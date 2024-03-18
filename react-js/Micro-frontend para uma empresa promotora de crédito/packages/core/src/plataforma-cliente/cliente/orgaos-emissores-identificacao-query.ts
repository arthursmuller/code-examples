import { createQueryFor } from 'utils';

export interface OrgaoEmissorIdentificacaoModel {
  id: number;
  codigo: string;
  descricao: string;
}

export const {
  getQueryConfig: getOrgaosEmissoresIdentificacaoQueryConfig,
  useQueryOf: useOrgaosEmissoresIdentificacao,
} = createQueryFor<OrgaoEmissorIdentificacaoModel[]>(
  'clientes/informacoes/orgaos-emissores-identificacao',
);
