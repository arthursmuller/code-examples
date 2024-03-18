import { forwardRef } from 'react';

import { useEstadosCivil } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectEstadosCivil = forwardRef<FormElRef, NonFetchBemSelectProps>(
  (selectProps, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useEstadosCivil}
      configSelectOption={{
        valueKey: 'id',
        nameKey: 'descricao',
      }}
      inputRef={ref}
    />
  ),
);
