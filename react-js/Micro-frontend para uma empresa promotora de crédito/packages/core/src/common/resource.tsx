import { ReactElement, useEffect } from 'react';

import {
  UseMutationResult,
  QueryKey,
  useMutation,
  useQuery,
} from 'react-query';

import { getQueryFor, postQueryFor } from './client';

interface ResourceProps<TOutput, TInput> {
  render({
    data,
  }: {
    data: TOutput;
    postMutation: UseMutationResult<TOutput, unknown, TInput, unknown>;
    refetch: () => void;
  }): ReactElement;
  path: QueryKey | string | (string | undefined)[];
  noDataComponent?: ReactElement;
  loaderComponent?: ReactElement;
  loadCallback?: () => void;
}

function Resource<TOutput, TInput = never>({
  path,
  render,
  noDataComponent,
  loadCallback,
  loaderComponent,
}: ResourceProps<TOutput, TInput>): ReactElement {
  const queryKey = path as string | string[];
  const [clearPath = '', qs] = Array.isArray(queryKey) ? queryKey : [queryKey];

  const { data, isLoading, refetch, isFetching } = useQuery<TOutput>(
    queryKey,
    !qs
      ? getQueryFor<TOutput>(clearPath)
      : getQueryFor<TOutput, any>(clearPath, qs),
  );
  const postMutation = useMutation(
    postQueryFor<TInput, TOutput>(clearPath || ''),
  );

  useEffect(() => {
    if (loadCallback && !isLoading && !!data) {
      loadCallback();
    }
  }, [isLoading]);

  if (isLoading || isFetching) {
    return loaderComponent || <div />;
  }

  if (
    !isLoading &&
    !isFetching &&
    (!data || (Array.isArray(data) && !data.length)) &&
    noDataComponent
  ) {
    return noDataComponent;
  }

  return render({ data: data as TOutput, postMutation, refetch });
}

export { Resource };
