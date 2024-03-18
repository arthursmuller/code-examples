import { RendimentoResponseModel } from '@pcf/core';

import { MatriculaInssFormModel } from '../models/matricula-inss-form.model';
import { MatriculaSiapeFormModel } from '../models/matricula-siape-form.model';

export const toSiapeFormModel = (
  entry: RendimentoResponseModel,
): MatriculaSiapeFormModel => {
  return {
    tipoConvenio: entry.convenio,
    matricula: entry.matricula,
    dataAdmissao: new Date(entry.dataAdmissao as Date),
    uf: entry.uf.id,
    orgao: entry.convenioOrgao?.id as number,

    tipoConta: entry.contaCliente.tipoConta.id,
    banco: entry.contaCliente.banco.id,
    agencia: entry.contaCliente.agencia,
    conta: entry.contaCliente.conta,
    idContaCliente: entry.contaCliente.idContaCliente,
    idFormaRecebimento: entry.contaCliente.idFormaRecebimento,

    siapeTipoFuncional: entry.siapeTipoFuncional?.id as number,
    possuiRepresentacaoPorProcurador: entry.possuiRepresentacaoPorProcurador
      ? '1'
      : '0',
    matriculaInstituidor: entry.matriculaInstituidor,
    nomeInstituidor: entry.nomeInstituidor,
    valorRendimento: entry.valorRendimento,
  };
};

export const toInssFormModal = (
  entry: RendimentoResponseModel,
): MatriculaInssFormModel => {
  return {
    tipoConvenio: entry.convenio,
    matricula: entry.matricula,
    dataInscricaoBeneficio: new Date(entry.dataInscricaoBeneficio as Date),
    uf: entry.uf.id,

    tipoConta: entry.contaCliente.tipoConta.id,
    banco: entry.contaCliente.banco.id,
    agencia: entry.contaCliente.agencia,
    conta: entry.contaCliente.conta,
    idContaCliente: entry.contaCliente.idContaCliente,
    idFormaRecebimento: entry.contaCliente.idFormaRecebimento,

    inssEspecieBeneficio: entry.inssEspecieBeneficio?.id as number,
    valorRendimento: entry.valorRendimento,
  };
};
