import { forwardRef } from 'react';

import { useMunicipios } from '@pcf/core';
import {
  CreateAsyncSelect,
  NonFetchBemSelectProps,
  FormElRef,
} from '@pcf/design-system';

interface SelectMunicipiosProps extends NonFetchBemSelectProps {
  idUF: string | undefined;
  municipioText?: string | undefined;
}

export const SelectMunicipios = forwardRef<FormElRef, SelectMunicipiosProps>(
  ({ idUF, municipioText, ...selectProps }, ref) => (
    <CreateAsyncSelect
      {...selectProps}
      useQueryHook={useMunicipios}
      queryString={{ idUF }}
      queryConfig={{ enabled: !!idUF }}
      configSelectOption={{
        valueKey: 'id',
        nameKey: 'descricao',
      }}
      disabled={!idUF}
      defaultValueText={municipioText}
      inputRef={ref}
    />
  ),
);
