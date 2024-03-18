import { createQueryFor } from 'utils';

export interface BancoModel {
  id: number;
  nome: string;
  codigo: string;
}

export const { getQueryConfig: getBancosQueryConfig, useQueryOf: useBancos } =
  createQueryFor<BancoModel[]>('bancarios/bancos');
