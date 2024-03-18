import { FC, lazy, Suspense } from 'react';

import { BrowserRouter as Router } from 'react-router-dom';

import { Loader, BemErrorBoundary } from '@pcf/design-system';
import { useUnloadPromptDialog } from 'components';
import { useAuthContext } from 'app/auth/auth.context';

const PrivateRoutes = lazy(() => import('./private'));
const PublicRoutes = lazy(() => import('./public'));

export const AppRoutes: FC = () => {
  const { isAuthenticated } = useAuthContext();

  const confirmNavigate = useUnloadPromptDialog();

  return (
    <Router getUserConfirmation={confirmNavigate}>
      <BemErrorBoundary>
        <Suspense fallback={<Loader fullHeight />}>
          {isAuthenticated ? <PrivateRoutes /> : <PublicRoutes />}
        </Suspense>
      </BemErrorBoundary>
    </Router>
  );
};
