import { FC, useMemo, useEffect } from 'react';

import { QueryClientProvider, QueryClient } from 'react-query';

import { extractReadableErrorMessage } from 'utils/get-error-message';
import { BemApiErrorResponse } from 'common/bem-api-error.model';

import { setApiBaseUrl } from './client';

const createQueryCacheConfig = (): QueryClient =>
  new QueryClient({
    defaultOptions: {
      queries: {
        refetchOnWindowFocus: false,
        cacheTime: 5 * 60 * 1000,
        staleTime: 5 * 60 * 1000,
        useErrorBoundary: true,
      },
    },
  });

export interface CoreProviderProps {
  onError?: (msg: string, error?: BemApiErrorResponse) => void;
  baseUrl?: string;
}
export const queryCacheConfig = createQueryCacheConfig();

export const CoreProvider: FC<CoreProviderProps> = ({
  children,
  onError,
  baseUrl,
}) => {
  useMemo(() => {
    if (baseUrl) setApiBaseUrl(baseUrl);
  }, [baseUrl]);

  useEffect(() => {
    const options = queryCacheConfig.getDefaultOptions();

    const handleOnError = (error): void => {
      const mensagem = extractReadableErrorMessage(
        error as BemApiErrorResponse,
        {
          showFallbackMessage: true,
          fallbackMessage: 'Houve um erro de comunicação.',
        },
      );

      if (mensagem) {
        onError && onError(mensagem);
      }
    };

    queryCacheConfig.setDefaultOptions({
      ...options,
      queries: {
        ...options.queries,
        retry: (failureCount, error: BemApiErrorResponse | string) => {
          if (process.env.NODE_ENV === 'test') {
            return false;
          }

          if (typeof error === 'string') return false;

          if (failureCount > 2) {
            handleOnError(error);
            return false;
          }
          return true;
        },
      },
      mutations: {
        onError: handleOnError,
      },
    });
    // Somente onMount mesmo, pois essa prop onError é passado nos Providers e não na hieraquia de aplicação
    // ou seja não vai mudar mesmo.
    // eslint-disable-next-line
  }, []);

  return (
    <QueryClientProvider client={queryCacheConfig}>
      {children}
    </QueryClientProvider>
  );
};
