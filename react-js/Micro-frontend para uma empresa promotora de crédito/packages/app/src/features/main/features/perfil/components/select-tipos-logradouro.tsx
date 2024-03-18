import { forwardRef } from 'react';

import { useTiposLogradouro } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectTiposLogradouro = forwardRef<
  FormElRef,
  NonFetchBemSelectProps
>((selectProps, ref) => (
  <CreateAsyncSelect
    {...selectProps}
    useQueryHook={useTiposLogradouro}
    configSelectOption={{
      valueKey: 'id',
      nameKey: 'descricao',
    }}
    inputRef={ref}
  />
));
