import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { useLocation } from 'react-router-dom';
import { use100vh } from 'react-div-100vh';

import { LogoutIcon } from '@pcf/design-system-icons';
import { TabBar } from 'components/tab-bar';
import { Page } from 'components/page';
import { MenuItemMobile } from 'components/menu-bar';

import { MainRoutes, mainRoutePaths } from './routes';
import { menuItemsMobile, tabBarMenuItems } from './consts/menu-items';
import { NotificacoesMobile } from './features/notificacoes';
import { notificationIconFor } from './features/notificacoes/components/notification-icon';

const mobileRoutesWithTabBar = tabBarMenuItems.map(({ route }) => route);

const MainApp: FC = () => {
  const { pathname } = useLocation();
  const height = use100vh();

  return (
    height && (
      <Flex direction="column" height={`${height}px`}>
        <Page
          menuItems={menuItemsMobile}
          menuMobileFooter={
            <Flex bg="white" h="72px" alignItems="center" px="24px" w="100%">
              <MenuItemMobile
                label="Sair da minha conta"
                icon={LogoutIcon}
                iconActive={LogoutIcon}
                route={mainRoutePaths.LOGOUT}
              />
            </Flex>
          }
        >
          <MainRoutes />
        </Page>

        {mobileRoutesWithTabBar.includes(pathname) && height && (
          <TabBar>
            {tabBarMenuItems.map((item) => (
              <TabBar.Item key={item.label} {...item} />
            ))}

            <NotificacoesMobile
              buttonRender={({ onClick, notifications }) => (
                <TabBar.Item
                  label="Notificações"
                  onClick={onClick as any}
                  icon={notificationIconFor(notifications)}
                />
              )}
            />
          </TabBar>
        )}
      </Flex>
    )
  );
};

export default MainApp;
