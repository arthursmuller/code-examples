import { createQueryFor } from 'utils';

export interface RendimentosMargemSiapeItem {
  pendenteInformacoesBanco: boolean;
  orgao: string;
  matricula: string;
  instituidor: string;
  valorMaximoParcela: number;
  emprestimoAutorizado: boolean;
  portabilidadeAutorizada: boolean;
}

export interface RendimentosMargemSiape {
  pendenteInformacoesBanco: boolean;
  items: RendimentosMargemSiapeItem[];
}

interface MargemSiapeQS {
  idRendimento: number;
}

export const {
  getQueryConfig: getRendimentosMargemSiapeQueryConfig,
  useQueryOf: useRendimentosMargemSiapeQuery,
} = createQueryFor<RendimentosMargemSiape, MargemSiapeQS>(
  'clientes/autenticado/rendimentos-siape/margens',
);
