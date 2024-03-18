import { forwardRef } from 'react';

import { useGeneros } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectGenero = forwardRef<FormElRef, NonFetchBemSelectProps>(
  (selectProps, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useGeneros}
      configSelectOption={{
        valueKey: 'id',
        nameKey: 'descricao',
      }}
      inputRef={ref}
    />
  ),
);
