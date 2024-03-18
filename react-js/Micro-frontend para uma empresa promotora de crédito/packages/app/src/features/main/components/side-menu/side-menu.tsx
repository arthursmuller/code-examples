import { useMemo, FC, Fragment } from 'react';

import { Flex } from '@chakra-ui/react';
import { useLocation } from 'react-router-dom';

import { fadeIn } from '@pcf/design-system';

import { SideMenuItem, SideMenuItemProps } from './side-menu-item';

interface SideMenuProps {
  items?: SideMenuItemProps[];
}

export const SideMenu: FC<SideMenuProps> = ({ items = [] }) => {
  const { pathname } = useLocation();

  const activeItem: SideMenuItemProps | undefined = useMemo(() => {
    return items.find((item) => pathname === item.route);
  }, [items, pathname]);

  return (
    <Flex maxHeight="100vh">
      <Flex
        flexShrink={0}
        bg="primary.regular"
        width="71px"
        flexDirection="column"
        justifyContent="space-around"
        py="8px"
        transition="width .2s ease-in-out"
        sx={{
          p: {
            transition: 'all .2s ease-in-out',
            fontSize: 0,
            height: 0,
          },
          'button svg': {
            transition: 'all .3s',
            width: '28px',
            height: '28px',
          },
          _hover: {
            width: '112px',
            p: {
              fontSize: '14px',
              height: 'auto',
            },
            'button svg': {
              width: '24px',
              height: '24px',
            },
          },
        }}
      >
        {items.map(({ validator: Validator = Fragment, ...item }) => (
          <Validator key={item.label}>
            <SideMenuItem {...item} />
          </Validator>
        ))}
      </Flex>

      <Flex
        flexDirection="column"
        flexShrink={0}
        animation={activeItem?.content ? `${fadeIn} 2s forwards` : ''}
        bg="primary.gradient"
        transition="width .4s"
        width={activeItem?.content ? '288px' : '0px'}
        boxShadow="medium"
      >
        {activeItem?.content}
      </Flex>
    </Flex>
  );
};
