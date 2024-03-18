import { forwardRef } from 'react';

import { useSiapeTiposFuncionais } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectSiapeTipoFuncional = forwardRef<
  FormElRef,
  NonFetchBemSelectProps
>((selectProps, ref) => (
  <CreateAsyncSelect
    {...selectProps}
    useQueryHook={useSiapeTiposFuncionais}
    configSelectOption={{
      valueKey: 'id',
      nameKey: 'descricao',
    }}
    inputRef={ref}
  />
));
