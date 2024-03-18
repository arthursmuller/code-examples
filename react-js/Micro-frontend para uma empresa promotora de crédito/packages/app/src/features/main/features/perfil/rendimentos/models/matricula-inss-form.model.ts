import { ConvenioModel } from '@pcf/core';

export interface MatriculaInssFormModel {
  tipoConvenio: ConvenioModel;
  matricula: string;
  dataInscricaoBeneficio: Date;
  uf: number;
  inssEspecieBeneficio: number;
  valorRendimento: number;

  tipoConta: number;
  banco: number;
  agencia: string;
  conta: string;
  idContaCliente: number;
  idFormaRecebimento?: number;
}

type KeysEnum<T> = { [P in keyof Partial<T>]: P };
export const MatriculaInssFormModelKeys: KeysEnum<MatriculaInssFormModel> = {
  tipoConvenio: 'tipoConvenio',
  matricula: 'matricula',
  dataInscricaoBeneficio: 'dataInscricaoBeneficio',
  uf: 'uf',
  tipoConta: 'tipoConta',
  banco: 'banco',
  agencia: 'agencia',
  conta: 'conta',
  inssEspecieBeneficio: 'inssEspecieBeneficio',
  valorRendimento: 'valorRendimento',
};
