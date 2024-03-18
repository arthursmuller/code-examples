import { FC } from 'react';

import { Link, useLocation } from 'react-router-dom';
import {
  Menu,
  MenuButton,
  Button,
  MenuList,
  MenuItem,
  Icon,
  useTheme,
  Link as ChakraLink,
} from '@chakra-ui/react';

import { ArrowUpIcon } from '@pcf/design-system-icons';

import { MenuItem as MenuItemType } from './menu-item.model';

const linkProps = (isExternal = false, route = '/'): any =>
  !isExternal
    ? {
        as: Link,
        to: route,
      }
    : {
        target: '_blank',
        href: route,
      };

interface MenuItemsDesktopProps {
  items: MenuItemType[];
}

export const MenuItemsDesktop: FC<MenuItemsDesktopProps> = ({ items }) => {
  const { pathname } = useLocation();
  const { layerStyles } = useTheme();

  return (
    <>
      {items.map(({ label, route, isExternal, items: subItems }) => {
        const isActive =
          (route && pathname === route) ||
          subItems?.find((i) => i.route && pathname?.includes(i.route));

        return subItems ? (
          <Menu key={label}>
            {({ isOpen }) => (
              <>
                <MenuButton
                  as={Button}
                  variant="link"
                  sx={isActive ? layerStyles.navActive : layerStyles.nav}
                  _active={{
                    color: 'white',
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
                        key={item.label}
                        {...linkProps(item.isExternal, item.route)}
                        borderTop={index !== 0 ? '1px solid' : ''}
                        borderColor="grey.300"
                        layerStyle={isSubActive ? 'navActive' : 'nav'}
                        color={isSubActive ? 'primary.regular' : 'grey.800'}
                        fontWeight={isSubActive ? 900 : 500}
                      >
                        {item.label}
                      </MenuItem>
                    );
                  })}
                </MenuList>
              </>
            )}
          </Menu>
        ) : (
          <ChakraLink
            key={label}
            color="white"
            layerStyle={isActive ? 'navActive' : 'nav'}
            {...linkProps(isExternal, route)}
          >
            {label}
          </ChakraLink>
        );
      })}
    </>
  );
};
