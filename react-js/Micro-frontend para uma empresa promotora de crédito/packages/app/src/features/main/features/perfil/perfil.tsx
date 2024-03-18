import { lazy, Suspense, FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import { Loader } from '@pcf/design-system';

const PerfilMobile = lazy(() => import('./perfil-mobile'));
const PerfilDesktop = lazy(() => import('./perfil-desktop'));

export const Perfil: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Suspense fallback={<Loader fullHeight fullWidth />}>
      {isMobile && <PerfilMobile />}
      {isMobile === false && <PerfilDesktop />}
    </Suspense>
  );
};
