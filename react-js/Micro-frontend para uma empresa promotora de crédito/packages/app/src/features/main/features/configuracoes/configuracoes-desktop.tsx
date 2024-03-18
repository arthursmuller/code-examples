import { FC } from 'react';

import { Box } from '@chakra-ui/react';
import { MemoryRouter } from 'react-router-dom';

import { Drawer, rightToLeft } from '@pcf/design-system';
import {
  ConfiguracoesFillIcon,
  StatusCloseErrorIcon,
} from '@pcf/design-system-icons';
import { NavBarButton } from 'features/main/components';

import { ConfiguracoesRoutesList } from './configuracoes-routes-list';
import { ConfigRoutes, ConfigRoutesPaths } from './configuracoes-routes';

export const ConfiguracoesDesktop: FC = () => (
  <Drawer
    buttonRender={(props) => (
      <NavBarButton icon={ConfiguracoesFillIcon} {...props} />
    )}
    content={({ onClose }) => (
      <MemoryRouter initialEntries={[ConfigRoutesPaths.configs]}>
        <ConfigRoutes>
          <Box animation={`250ms ${rightToLeft} ease-in-out`}>
            <Drawer.Title
              onClick={onClose}
              icon={StatusCloseErrorIcon}
              title="Configurações"
              color="secondary.mid-dark"
            />

            <Drawer.Body>
              <ConfiguracoesRoutesList />
            </Drawer.Body>
          </Box>
        </ConfigRoutes>
      </MemoryRouter>
    )}
  />
);
