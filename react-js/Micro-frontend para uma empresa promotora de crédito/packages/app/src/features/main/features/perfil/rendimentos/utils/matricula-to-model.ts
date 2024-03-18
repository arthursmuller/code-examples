import { RendimentoPersistModel } from '@pcf/core';

import { MatriculaInssFormModel } from '../models/matricula-inss-form.model';
import { MatriculaSiapeFormModel } from '../models/matricula-siape-form.model';

export const toRequestModel = (
  entry: MatriculaInssFormModel | MatriculaSiapeFormModel,
): RendimentoPersistModel => {
  let request: Partial<RendimentoPersistModel> = {
    convenio: +entry.tipoConvenio.id,
    matricula: entry.matricula,
    idUf: +entry.uf,

    contaCliente: {
      idContaCliente: entry.idContaCliente,
      idFormaRecebimento: entry.idFormaRecebimento,
      idBanco: +entry.banco,
      idTipoConta: +entry.tipoConta,
      agencia: entry.agencia,
      conta: entry.conta,
    }
  };

  const inssEntry = entry as MatriculaInssFormModel;

  request = {
    ...request,
    idInssEspecieBeneficio: +inssEntry.inssEspecieBeneficio || undefined,
    valorRendimento: inssEntry.valorRendimento,
    dataInscricaoBeneficio: inssEntry.dataInscricaoBeneficio,
  };

  const siapeEntry = entry as MatriculaSiapeFormModel;

  request = {
    ...request,
    idConvenioOrgao: +siapeEntry.orgao || undefined,
    idSiapeTipoFuncional: +siapeEntry.siapeTipoFuncional || undefined,
    dataAdmissao: siapeEntry.dataAdmissao,
    matriculaInstituidor: siapeEntry.matriculaInstituidor,
    nomeInstituidor: siapeEntry.nomeInstituidor,
    possuiRepresentacaoPorProcurador:
      +siapeEntry.possuiRepresentacaoPorProcurador === 1,
  };

  return request as RendimentoPersistModel;
};
