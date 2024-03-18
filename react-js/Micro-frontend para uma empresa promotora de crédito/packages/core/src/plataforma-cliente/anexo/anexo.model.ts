import { DocumentTypeCode } from './document-types.enum';

export interface AnexoCriacaoModel {
  idTipoDocumento: DocumentTypeCode;
  anexoBase64: string;
  extensao: string;
}

export interface Anexo {
  linkAnexo: string;
  id: number;
  tipoDocumento: {
    id: DocumentTypeCode;
    codigo: string;
    nome: string;
  };
  dataCadastro: string;
  extensao: string;
}
