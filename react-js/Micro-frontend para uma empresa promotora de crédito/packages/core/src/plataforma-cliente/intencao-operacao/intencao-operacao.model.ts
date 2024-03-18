import { RendimentoResponseModel } from '../consignado/rendimentos-query';
import { LeadModel } from '../leads';
import { LojaModel } from '../loja';
import { ProdutoModel } from '../produto';
import { UsuarioModel } from '../sign-up';

export interface IntencaoOperacaoPassoModel {
  id: number;
  titulo?: string;
  descricao?: string;
  completo: boolean;
  excepcional: boolean;
  pendenciaUsuario: boolean;
}

export interface TipoOperacaoModel {
  id: number;
  nome: string;
  sigla: string;
}

export interface IntencaoOperacaoModel {
  id: number;
  prestacao: number;
  valorAuxilioFinanceiro: number;
  taxaMes: number;
  taxaAno: number;
  valorFinanciado: number;
  dataAtualizacao: string;
  dataInclusao: string;
  primeiroVencimento: string;
  prazo: number;

  tipoOperacao: TipoOperacaoModel;
  produto: ProdutoModel;
  lead?: LeadModel;
  loja: LojaModel;
  passosProduto: IntencaoOperacaoPassoModel[];

  rendimento: RendimentoResponseModel;
  usuario: UsuarioModel;
}

export interface IntencaoOperacaoRequisicaoModel {
  idTipoOperacao: number;
  idProduto: number;
  idLoja?: number;
  idLead?: number;
  idUsuario?: number;
  prestacao: number;
  valorAuxilioFinanceiro: number;
  taxaMes: number;
  taxaAno: number;
  valorFinanciado: number;
  prazo: number;
  idSituacao?: number;
  primeiroVencimento: string;
  idRendimentoCliente: number;
  contratos?: { contrato: string }[];

  custoEfetivoTotalMes: number;
  custoEfetivoTotalAno: number;
  impostoOperacaoFinanceira: number;

  portabilidade?: {
    idBancoOriginador: number;
    prazoRestante: number;
    prazoTotal: number;
    saldoDevedor: number;
    planoRefinanciamento: string;
    prazoRefinanciamento: number;
    valorPrestacaoRefinanciamento: number;
  };
}

export interface IntencaoOperacaoNovoRetornoModel {
  int: number;
}
