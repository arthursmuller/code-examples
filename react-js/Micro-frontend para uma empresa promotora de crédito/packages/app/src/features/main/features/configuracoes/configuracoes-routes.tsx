import { FC, lazy, Suspense } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';
import { Route, Switch } from 'react-router-dom';

import { Loader } from '@pcf/design-system';

const AtualizarSenha = lazy(() => import('./features/atualizar-senha'));
const LayoutMobile = lazy(() => import('./components/config-layout-mobile'));
const LayoutDesktop = lazy(() => import('./components/config-layout-desktop'));

export enum ConfigRoutesPaths {
  configs = '/configuracoes',
  atualizarSenha = '/configuracoes/atualizar-senha',
}

export const ConfigRoutes: FC = ({ children }) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  const PageLayout = isDesktop ? LayoutDesktop : LayoutMobile;

  return (
    <Suspense fallback={<Loader fullHeight />}>
      <Switch>
        <Route exact path={ConfigRoutesPaths.configs}>
          {children}
        </Route>
        <Route exact path={ConfigRoutesPaths.atualizarSenha}>
          <PageLayout title="Redefinição de senha">
            <AtualizarSenha />
          </PageLayout>
        </Route>
      </Switch>
    </Suspense>
  );
};
