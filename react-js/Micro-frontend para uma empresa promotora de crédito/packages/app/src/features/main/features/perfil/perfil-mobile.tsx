import { FC } from 'react';

import { useHistory } from 'react-router-dom';

import {
  PageLayout,
  BemErrorBoundary,
  BemErrorFallback,
} from '@pcf/design-system';
import { mainRoutePaths } from 'features/main/routes';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';

import { ListPerfilOptions } from './components/list-perfil-options';
import { QuickyProfile } from './components/quicky-profile';
import { PerfilRoutes } from './perfil-routes';
import { perfilOptions } from './perfil.consts';

export const PerfilMobile: FC = () => {
  const history = useHistory();

  useSubRouteMenu('Perfil', perfilOptions);

  return (
    <PerfilRoutes>
      <PageLayout>
        <PageLayout.Header>
          <PageLayout.BackButton
            onClick={() => {
              history.push(mainRoutePaths.INICIO);
            }}
          />
          <BemErrorBoundary
            fallbackRender={(props) => (
              <BemErrorFallback {...props} schemeColor="white" />
            )}
          >
            <QuickyProfile px={0} asDialog />
          </BemErrorBoundary>
        </PageLayout.Header>

        <PageLayout.Content>
          <ListPerfilOptions />
        </PageLayout.Content>
      </PageLayout>
    </PerfilRoutes>
  );
};

export { PerfilMobile as default };
