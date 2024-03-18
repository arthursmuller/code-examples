import { FC } from 'react';

import { useHistory } from 'react-router-dom';

import { PageLayout } from '@pcf/design-system';
import { mainRoutePaths } from 'features/main/routes/main-routes-paths';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';

import { ConfigRoutes } from './configuracoes-routes';
import {
  configOptions,
  ConfiguracoesRoutesList,
} from './configuracoes-routes-list';

export const ConfiguracoesMobile: FC = () => {
  const history = useHistory();

  useSubRouteMenu('Configurações', configOptions);

  return (
    <ConfigRoutes>
      <PageLayout>
        <PageLayout.Header>
          <PageLayout.BackButton
            onClick={() => {
              history.push(mainRoutePaths.PERFIL);
            }}
          />
          <PageLayout.Title>Configurações</PageLayout.Title>
        </PageLayout.Header>

        <PageLayout.Content>
          <ConfiguracoesRoutesList />
        </PageLayout.Content>
      </PageLayout>
    </ConfigRoutes>
  );
};

export { ConfiguracoesMobile as default };
