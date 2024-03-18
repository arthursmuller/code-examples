import { ConvenioModel } from '../convenio';

export interface LeadCriacaoModel {
  cpf?: string;
  telefone?: string;
  celular?: string;
  email?: string;
  idConvenio?: number;
  idProduto?: number;
  latitude?: number | null;
  longitude?: number | null;
  origemRequisicaoPalavraChave?: string | null;
  origemRequisicaoMidia?: string | null;
  origemRequisicaoConteudo?: string | null;
  origemRequisicaoTermo?: string | null;
  origemRequisicaoCampanha?: string | null;
  desejaContatoWhatsApp?: boolean;
  quitacao?: boolean;
  nome?: string;
}

export interface LeadModel extends LeadCriacaoModel {
  convenio: ConvenioModel;
}
export interface LeadNovaModel {
  id: number;
  latitude?: number | null;
  longitude?: number | null;
  linkContatoWhatsAppLoja?: string | null;
}
