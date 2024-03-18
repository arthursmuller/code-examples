import { createQueryFor } from 'utils';

import { BancoModel, TipoContaModel } from '../bancario';
import {
  ConvenioModel,
  InssEspecieModel,
  SiapeTipoFuncionalModel,
} from '../convenio';
import { OrgaoModel } from '../convenio/orgaos-query';
import { UnidadeFederativaModel } from '../localizacao/unidades-federativas-query';

export interface ContaCliente {
  idContaCliente?: number;
  agencia: string;
  banco: BancoModel;
  conta: string;
  tipoConta: TipoContaModel;
}
export interface RendimentoResponseModel {
  id: number;
  convenio: ConvenioModel;
  convenioOrgao: OrgaoModel | undefined;
  dataAdmissao: Date | undefined;
  dataInscricaoBeneficio: Date | undefined;
  siapeTipoFuncional: SiapeTipoFuncionalModel | undefined;
  inssEspecieBeneficio: InssEspecieModel | undefined;
  matricula: string;
  matriculaInstituidor: string;
  possuiRepresentacaoPorProcurador: boolean;
  uf: UnidadeFederativaModel;
  valorRendimento: number;
  nomeInstituidor: string;
  contaCliente: ContaCliente;
  contaClienteRecebimento: ContaCliente;
}

export const RENDIMENTOS_QUERY_ENDPOINT = 'clientes/autenticado/rendimentos';

export const {
  getQueryConfig: getRendimentosQueryConfig,
  useQueryOf: useRendimentosQuery,
} = createQueryFor<RendimentoResponseModel[]>(RENDIMENTOS_QUERY_ENDPOINT);
