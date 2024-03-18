import { forwardRef } from 'react';

import { useBancos } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

export const SelectBancos = forwardRef<FormElRef, NonFetchBemSelectProps>(
  (selectProps, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useBancos}
      configSelectOption={{
        valueKey: 'id',
        nameKey: ['codigo', 'nome'],
      }}
      inputRef={ref}
    />
  ),
);
