import { FC } from 'react';

import { useRouter } from 'next/router';
import {
  Menu,
  MenuButton,
  Button,
  MenuList,
  MenuItem,
  Icon,
  useTheme,
} from '@chakra-ui/react';

import { ArrowUpIcon } from '@pcf/design-system-icons';

import { MenuItem as MenuItemType } from './menu-item.model';
import { LinkOrExternal, MenuLink } from './menu-link';

interface MenuItemsDesktopProps {
  items: MenuItemType[];
}

export const MenuItemsDesktop: FC<MenuItemsDesktopProps> = ({ items }) => {
  const { asPath: pathname } = useRouter();
  const { layerStyles } = useTheme();

  return (
    <>
      {items.map(({ label, route, isExternal, items: subItems }) => {
        const isActive =
          (route && pathname === route) ||
          !!subItems?.find((i) => i.route && pathname?.includes(i.route));

        return subItems ? (
          <Menu key={label}>
            {({ isOpen }) => (
              <>
                <MenuButton
                  as={Button}
                  variant="link"
                  _active={{
                    color: 'white',
                  }}
                  sx={{
                    '> span': {
                      whiteSpace: 'nowrap',
                      fontSize: ['1vw', '1vw', '1vw', '1vw', '1vw', 'unset'],
                    },
                    ...(isActive ? layerStyles.navActive : layerStyles.nav),
                  }}
                >
                  {label}
                  <Icon
                    as={ArrowUpIcon}
                    width={3}
                    height={3}
                    marginLeft={2}
                    transform={isOpen ? 'rotate(0deg)' : 'rotate(180deg)'}
                    transition="transform .25s"
                  />
                </MenuButton>
                <MenuList>
                  {subItems.map((item: MenuItemType, index) => {
                    const isSubActive =
                      item.route && pathname?.includes(item.route);

                    return (
                      <MenuItem
                        as="div"
                        key={item.label}
                        borderTop={index !== 0 ? '1px solid' : ''}
                        borderColor="grey.300"
                        layerStyle={isSubActive ? 'navActive' : 'nav'}
                        color={isSubActive ? 'primary.regular' : 'grey.800'}
                        fontWeight={isSubActive ? 900 : 500}
                      >
                        <LinkOrExternal
                          isExternal={item.isExternal}
                          label={item.label}
                          route={item.route}
                        />
                      </MenuItem>
                    );
                  })}
                </MenuList>
              </>
            )}
          </Menu>
        ) : (
          <MenuLink
            key={label}
            route={route}
            isExternal={isExternal}
            label={label}
            isActive={isActive}
          />
        );
      })}
    </>
  );
};
