import { ConvenioModel } from '@pcf/core';

export interface MatriculaSiapeFormModel {
  tipoConvenio: ConvenioModel;
  matricula: string;
  dataAdmissao: Date;
  uf: number;
  orgao: number;
  siapeTipoFuncional: number;
  possuiRepresentacaoPorProcurador: string;
  matriculaInstituidor?: string;
  nomeInstituidor?: string;
  valorRendimento: number;

  tipoConta: number;
  banco: number;
  agencia: string;
  conta: string;
  idContaCliente: number;
  idFormaRecebimento?: number;
}

type KeysEnum<T> = { [P in keyof Partial<T>]: P };
export const MatriculaSiapeFormModelKeys: KeysEnum<MatriculaSiapeFormModel> = {
  tipoConvenio: 'tipoConvenio',
  matricula: 'matricula',
  dataAdmissao: 'dataAdmissao',
  uf: 'uf',
  orgao: 'orgao',
  tipoConta: 'tipoConta',
  banco: 'banco',
  agencia: 'agencia',
  conta: 'conta',
  siapeTipoFuncional: 'siapeTipoFuncional',
  matriculaInstituidor: 'matriculaInstituidor',
  nomeInstituidor: 'nomeInstituidor',
  possuiRepresentacaoPorProcurador: 'possuiRepresentacaoPorProcurador',
  valorRendimento: 'valorRendimento',
};
