import { forwardRef } from 'react';

import { useTiposConta } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectTiposConta = forwardRef<FormElRef, NonFetchBemSelectProps>(
  (selectProps, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useTiposConta}
      configSelectOption={{
        valueKey: 'id',
        nameKey: 'nome',
      }}
      inputRef={ref}
    />
  ),
);
