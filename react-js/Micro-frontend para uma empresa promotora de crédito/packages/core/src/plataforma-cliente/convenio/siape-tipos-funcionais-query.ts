import { createQueryFor } from 'utils';

export interface SiapeTipoFuncionalModel {
  id: number;
  descricao: string;
  codigo: string;
}
export const {
  getQueryConfig: getSiapeTiposFuncionaisQueryConfig,
  useQueryOf: useSiapeTiposFuncionais,
} = createQueryFor<SiapeTipoFuncionalModel[]>(
  'convenios/siape/tipos-funcionais',
);
