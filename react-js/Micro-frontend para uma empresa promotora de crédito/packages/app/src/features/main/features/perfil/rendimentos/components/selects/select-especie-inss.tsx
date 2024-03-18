import { forwardRef } from 'react';

import { useInssEspecies } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectEspecieInss = forwardRef<FormElRef, NonFetchBemSelectProps>(
  (selectProps, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useInssEspecies}
      configSelectOption={{
        valueKey: 'id',
        nameKey: ['codigo', 'descricao'],
      }}
    />
  ),
);
