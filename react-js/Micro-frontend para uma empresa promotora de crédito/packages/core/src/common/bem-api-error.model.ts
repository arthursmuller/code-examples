export enum ErrorMessageTypes {
  business = 1,
  form = 2,
  db = 3,
  exception = 4,
  communication = 5,
  domain = 6,
}

export interface BemApiError {
  codigo: number;
  tipo: ErrorMessageTypes;
  mensagem: string;
}

export interface BemApiErrorResponse {
  response: {
    data: {
      alertas: BemApiError[];
      erros: BemApiError[];
    };
  };
}
