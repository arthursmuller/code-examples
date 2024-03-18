import { FC } from 'react';

import { Box, Flex } from '@chakra-ui/react';

import { NavBar, SideMenu } from './components';
import { MainRoutes } from './routes';
import { menuItems } from './consts/menu-items';
import { ConfiguracoesDesktop } from './features/configuracoes';
import { NotificacoesDesktop } from './features/notificacoes';

export const MainApp: FC = () => (
  <Flex
    justifyContent="stretch"
    minHeight="100vh"
    overflow="hidden"
    width="100%"
  >
    <SideMenu items={menuItems} />

    <Flex flex="1" height="100vh" flexDirection="column">
      <Box>
        <NavBar>
          <NotificacoesDesktop />
          <ConfiguracoesDesktop />
        </NavBar>
      </Box>

      <Flex flex="1" overflow="hidden">
        <MainRoutes />
      </Flex>
    </Flex>
  </Flex>
);

export default MainApp;
