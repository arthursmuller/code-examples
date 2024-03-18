import { lazy, Suspense, FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import { Loader } from '@pcf/design-system';

const DashboardMobile = lazy(() => import('./dashboard-mobile'));
const DashboardDesktop = lazy(() => import('./dashboard-desktop'));

export const Dashboard: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Suspense fallback={<Loader fullHeight fullWidth />}>
      {isMobile && <DashboardMobile />}
      {isMobile === false && <DashboardDesktop />}
    </Suspense>
  );
};
