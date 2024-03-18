import { forwardRef } from 'react';

import { useUnidadesFederativas } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectUnidadesFederativas = forwardRef<
  FormElRef,
  NonFetchBemSelectProps
>((props, ref) => (
  <CreateAsyncSelect
    useQueryHook={useUnidadesFederativas}
    configSelectOption={{
      valueKey: 'id',
      nameKey: 'nome',
    }}
    {...props}
  />
));
