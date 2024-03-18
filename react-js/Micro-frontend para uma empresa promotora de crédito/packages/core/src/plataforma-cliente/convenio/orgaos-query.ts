import { createQueryFor } from 'utils';

export interface OrgaoModel {
  id: number;
  codigo: string;
  nome: string;
  cnpj: string;
  uf: string;
}

interface OrgaosParams {
  termo: string | undefined;
}

export const { getQueryConfig: getOrgaosQueryConfig, useQueryOf: useOrgaos } =
  createQueryFor<OrgaoModel[], OrgaosParams>('convenios/siape/orgaos');
