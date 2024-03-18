import { forwardRef } from 'react';

import { BemSelect, BemSelectProps, FormElRef } from '@pcf/design-system';

const data = [
  { value: 'false', name: 'Não' },
  { value: 'true', name: 'Sim' },
];

export const SelectDeficienteVisual = forwardRef<
  FormElRef,
  Omit<BemSelectProps, 'options'>
>((props, ref) => {
  return <BemSelect options={data} {...props} ref={ref} />;
});
