export interface SimulacaoNovoRequisicaoModel {
  idConvenio: number;
  valorOperacao?: number;
  valorPrestacao?: number;
  retornarSomenteOperacoesViaveis: boolean;
}

export interface SimulacaoNovoRetornoModel {
  prazo: string | number;
  plano: string;
  prestacao: number;
  valorFinanciado: number;
  valorTotal: number;
  valorAF: number;
  taxaMes: number;
  taxaAno: number;
  primeiroVcto: string;

  valorOperacao: number;
  valorIOF: number;
  cetAno: number;
  cetMes: number;

  operacaoViavel?: boolean;
}
