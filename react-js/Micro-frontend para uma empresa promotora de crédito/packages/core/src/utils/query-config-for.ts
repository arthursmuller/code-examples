import { UseQueryOptions, UseQueryResult, useQuery } from 'react-query';

import { getQueryFor } from 'common/client';

export interface QueryStringObject {
  [key: string]: string | number;
}

export interface ClientConfigs {
  skipAlerts?: boolean;
}

export type UseQueryWithConfig<TOutput, TQueryString = QueryStringObject> = (
  qs?: TQueryString,
  config?: Partial<UseQueryOptions<TOutput, Error>> & ClientConfigs,
) => UseQueryResult<TOutput, Error>;

export type GetQueryConfig<TOutput, TQueryString = QueryStringObject> = (
  qs?: TQueryString,
  config?: Partial<UseQueryOptions<TOutput, Error>> & ClientConfigs,
) => UseQueryOptions<TOutput, Error>;

export type CustomQuery<TOutput, TQueryString> = (
  queryString?: TQueryString,
  path?: string,
) => Promise<TOutput>;

export type ComposedPath<TQueryString> = (
  qs?: TQueryString,
) => [string, Partial<TQueryString> | undefined | null];

/**
 * Create a Reacts Query's Query Hook and Query Config for the given configuration wrapped with getQueryFor HTTP helper.
 *
 * @param path            Query Key for caching (together with called Query String object), and endpoint path for the GET request.
 * @param customQueryFn   instead of using the default getQueryFor wrapper, React Query will execute the attributed queryFn instead.
 * @param composedPath    for having a computed endpoint based on any Query String object values, that is injected when the query is called. Expects a new path and Query String object as return.
 *                           The caching key still uses the pair [path, query string object].
 */
export const createQueryFor = <TOutput, TQueryString = QueryStringObject>(
  path: string | ComposedPath<TQueryString>,
  customQueryFn?: CustomQuery<TOutput, TQueryString>,
  clientConfig?: ClientConfigs,
): {
  getQueryConfig: GetQueryConfig<TOutput, TQueryString>;
  useQueryOf: UseQueryWithConfig<TOutput, TQueryString>;
} => {
  const getQueryConfig: GetQueryConfig<TOutput, TQueryString> = (
    qs?,
    config?,
  ) => {
    const mergedClientConfigs = {
      ...clientConfig,
      ...config,
    };

    const [nextPath, nextQS] =
      typeof path === 'function'
        ? (path as ComposedPath<TQueryString>)(qs)
        : [path, qs];

    return {
      queryKey: [nextPath, nextQS],
      queryFn: customQueryFn
        ? () => customQueryFn(nextQS as TQueryString, nextPath)
        : getQueryFor<TOutput, TQueryString>(
            nextPath,
            nextQS as TQueryString,
            mergedClientConfigs?.skipAlerts,
          ),
      ...config,
    };
  };

  const useQueryOf: UseQueryWithConfig<TOutput, TQueryString> = (
    qs?,
    config?,
  ) => useQuery(getQueryConfig(qs, config));

  return { getQueryConfig, useQueryOf };
};
