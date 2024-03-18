import { createQueryFor } from 'utils';

export interface InssEspecieModel {
  id: number;
  descricao: string;
  codigo: string;
}

export const {
  getQueryConfig: getInssEspeciesQueryConfig,
  useQueryOf: useInssEspecies,
} = createQueryFor<InssEspecieModel[]>('convenios/inss/especies-beneficios');
