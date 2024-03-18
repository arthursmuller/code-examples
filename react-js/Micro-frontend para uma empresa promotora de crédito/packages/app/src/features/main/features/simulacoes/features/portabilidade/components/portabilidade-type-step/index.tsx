import { FC, lazy } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import { FullLayoutCard, LazyByDevice } from '@pcf/design-system';

const StepMobile = lazy(() => import('./portabilidade-type-step-mobile'));
const StepDesktop = lazy(() => import('./portabilidade-type-step-desktop'));

export const SimulationValueStep: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <FullLayoutCard paddingX={isMobile ? '0' : undefined}>
      <LazyByDevice mobile={StepMobile} desktop={StepDesktop} />
    </FullLayoutCard>
  );
};
