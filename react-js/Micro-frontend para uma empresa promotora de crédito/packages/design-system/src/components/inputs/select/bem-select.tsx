import { useBreakpointValue } from '@chakra-ui/react';
import { forwardRef } from 'react';

import { FormElRef } from '../form-item';
import { Select } from './components/select';
import { SelectMobile } from './components/select-mobile';
import { BemSelectProps } from './select.model';

export const BemSelect = forwardRef<FormElRef, BemSelectProps>((props, ref) => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return isMobile ? (
    <SelectMobile {...props} />
  ) : (
    <Select {...props} ref={ref} />
  );
});
