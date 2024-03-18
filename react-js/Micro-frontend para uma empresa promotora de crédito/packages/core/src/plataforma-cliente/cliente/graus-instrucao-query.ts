import { createQueryFor } from 'utils';

export interface GrauInstrucaoModel {
  id: number;
  descricao: string;
}

export const GRAU_INSTRUCAO_QUERY_ENDPOINT =
  'clientes/informacoes/graus-instrucao';

export const {
  getQueryConfig: getGrausInstrucao,
  useQueryOf: useGrausInstrucao,
} = createQueryFor<GrauInstrucaoModel[]>(GRAU_INSTRUCAO_QUERY_ENDPOINT);
