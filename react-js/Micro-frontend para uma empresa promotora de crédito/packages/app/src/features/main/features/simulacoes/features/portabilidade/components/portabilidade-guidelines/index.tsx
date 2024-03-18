import { FC, lazy, Suspense } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import { Loader } from '@pcf/design-system';

import { PortabilidadeGuidelinesProps } from './portabilidade-guideline-props';

const StepMobile = lazy(() => import('./portabilidade-guidelines-mobile'));
const StepDesktop = lazy(() => import('./portabilidade-guidelines-desktop'));

export const PortabilidadeGuidelines: FC<PortabilidadeGuidelinesProps> = (
  props,
) => {
  const isMobile = useBreakpointValue({
    base: true,
    sm: true,
    md: true,
    lg: true,
    xl: false,
  });

  return isMobile !== undefined ? (
    <Suspense fallback={<Loader fullHeight />}>
      {isMobile && <StepMobile {...props} />}
      {!isMobile && <StepDesktop {...props} />}
    </Suspense>
  ) : (
    <Loader fullHeight />
  );
};
