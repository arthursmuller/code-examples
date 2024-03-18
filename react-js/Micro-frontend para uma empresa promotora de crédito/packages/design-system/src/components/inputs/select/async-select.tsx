import { ReactElement, useMemo } from 'react';

import { UseQueryOptions, UseQueryResult } from 'react-query';

import { BemSelect } from './bem-select';
import { BemSelectProps, SelectOption } from './select.model';

import { useQuickToast } from '../../toast';
import { FormElRef } from '../form-item';

export interface QueryStringObject {
  [key: string]: string | number;
}

export type UseQueryWithConfig<TOutput, TQueryString = QueryStringObject> = (
  qs?: TQueryString,
  config?: Partial<UseQueryOptions<TOutput, Error>>,
) => UseQueryResult<TOutput, Error>;

export interface NonFetchBemSelectProps
  extends Omit<
    BemSelectProps,
    | 'options'
    | 'isLoading'
    | 'searchQuery'
    | 'retry'
    | 'hasError'
    | 'defaultValue'
  > {
  defaultValue: string | number | undefined;
}
interface ConfigSelectOption {
  valueKey: string | number;
  nameKey: string | string[];
}

interface CreateAsyncSelectProps<TOutput, TQueryString>
  extends NonFetchBemSelectProps {
  useQueryHook: UseQueryWithConfig<TOutput, TQueryString>;
  configSelectOption?: ConfigSelectOption;
  queryString?: TQueryString;
  queryConfig?: Partial<UseQueryOptions<TOutput, Error>>;
  defaultValueText?: string;
  inputRef?: FormElRef;
}

export function CreateAsyncSelect<TOutput, TQueryString = QueryStringObject>({
  useQueryHook,
  configSelectOption,
  queryString,
  queryConfig,
  defaultValueText = '',
  inputRef,
  ...selectProps
}: CreateAsyncSelectProps<TOutput[], TQueryString>): ReactElement {
  const toast = useQuickToast();

  const {
    data = [],
    isLoading,
    refetch,
    isError,
  } = useQueryHook(queryString, {
    ...queryConfig,
    enabled: queryConfig?.enabled ?? true,
    onError: (err) => {
      if (queryConfig?.onError) queryConfig?.onError(err);
      toast('Ops!', 'Ocorreu um erro ao carregar as opções.');
    },
    useErrorBoundary: false,
  });

  const { valueKey, nameKey } = configSelectOption as ConfigSelectOption;
  const options: SelectOption[] = useMemo(() => {
    if (data.length < 1 && !!defaultValueText)
      return [{ name: defaultValueText, value: `${selectProps.defaultValue}` }];

    return data.map((dataOption) => ({
      name: Array.isArray(nameKey)
        ? nameKey.reduce((a, c) => `${a}${a && ' - '}${dataOption[c]}`, '')
        : dataOption[nameKey],
      value: `${dataOption[valueKey]}`,
    }));
  }, [data, nameKey, valueKey, defaultValueText, selectProps.defaultValue]);

  const { defaultValue, ...rest } = selectProps;
  const nextDefaultValue = defaultValue ? `${defaultValue}` : '';

  return (
    <BemSelect
      {...rest}
      isLoading={isLoading}
      options={options}
      defaultValue={nextDefaultValue}
      searchQuery={undefined}
      retry={refetch}
      hasError={isError}
      ref={inputRef}
    />
  );
}

CreateAsyncSelect.defaultProps = {
  configSelectOption: {
    valueKey: 'value',
    nameKey: 'descricao',
  },
  queryString: null,
  queryConfig: {},
  defaultValueText: '',
  inputRef: null,
};
