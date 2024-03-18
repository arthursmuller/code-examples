import { ErrorMessageTypes } from "common/bem-api-error.model";

export interface MensagemBase {
  codigo: number;
  mensagem: string;
  tipo: ErrorMessageTypes;
}

export interface BemApiRetorno<T> {
  retorno: T;
  mensagem: string;
  alertas: MensagemBase[];
}
