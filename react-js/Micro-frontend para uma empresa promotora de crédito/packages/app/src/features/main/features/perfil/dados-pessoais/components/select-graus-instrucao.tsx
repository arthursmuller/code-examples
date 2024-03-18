import { forwardRef } from 'react';

import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';
import { useGrausInstrucao } from '@pcf/core';

export const SelectGrausInstrucao = forwardRef<
  FormElRef,
  NonFetchBemSelectProps
>((selectProps, ref) => (
  <CreateAsyncSelect
    {...selectProps}
    useQueryHook={useGrausInstrucao}
    configSelectOption={{
      valueKey: 'id',
      nameKey: 'descricao',
    }}
    inputRef={ref}
  />
));
