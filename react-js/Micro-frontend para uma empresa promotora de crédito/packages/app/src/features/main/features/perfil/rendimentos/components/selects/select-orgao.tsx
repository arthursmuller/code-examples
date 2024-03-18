import { forwardRef } from 'react';

import { useOrgaos } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

interface SelectOrgaoProps extends NonFetchBemSelectProps {
  uf?: string | undefined;
}

export const SelectOrgao = forwardRef<FormElRef, SelectOrgaoProps>(
  ({ uf, ...selectProps }, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useOrgaos}
      queryString={{ termo: uf }}
      configSelectOption={{
        valueKey: 'id',
        nameKey: ['codigo', 'nome'],
      }}
      inputRef={ref}
    />
  ),
);
