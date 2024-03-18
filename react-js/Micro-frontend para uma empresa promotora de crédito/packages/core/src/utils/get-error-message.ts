import {
  BemApiErrorResponse,
  ErrorMessageTypes,
} from 'common/bem-api-error.model';

export const isUserFriendlyType = (type?: ErrorMessageTypes): boolean =>
  !!type &&
  (type === ErrorMessageTypes.business ||
    type === ErrorMessageTypes.form ||
    type === ErrorMessageTypes.db);

interface extractReadableErrorMessageConfigs {
  showFallbackMessage?: boolean;
  fallbackMessage?: string;
}

export const extractReadableErrorMessage = (
  error: BemApiErrorResponse | Error | null,
  config?: extractReadableErrorMessageConfigs,
): string | undefined => {
  const errorData = (error as BemApiErrorResponse)?.response?.data;

  if (errorData) {
    const { tipo, mensagem } =
      (errorData?.alertas?.length && errorData?.alertas[0]) ||
      (errorData?.erros?.length && errorData?.erros[0]) ||
      {};

    if (isUserFriendlyType(tipo)) {
      return mensagem;
    }
  }

  if (config?.showFallbackMessage) {
    return config?.fallbackMessage || 'Houve um erro desconhecido';
  }

  return undefined;
};
