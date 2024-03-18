import {
  FunctionComponent,
  ReactElement,
  FC,
  useEffect,
  Fragment,
} from 'react';

import { Flex, ListItem, List, Icon, Box } from '@chakra-ui/react';
import { useRouter } from 'next/router';
import Link from 'next/link';

import { zIndexes, BurgerButton, fadeIn } from '@pcf/design-system';
import { LogoBemHorizontalWhiteIcon } from '@pcf/design-system-icons';

import { useMenuContext } from './menu.context';
import { MenuItem } from './menu-item.model';
import { MenuItemMobile } from './menu-item-mobile';
import { MenuMobile } from './menu-mobile';
import { MenuItemsDesktop } from './menu-items-desktop';

export interface MenuContentProps {
  inlineActions?: FunctionComponent | ReactElement | null;
  menuMobileHeader?: FunctionComponent | ReactElement | null;
  menuMobileFooter?: FunctionComponent | ReactElement | null;
  menuMobileAlternativeContent?: FunctionComponent | ReactElement | null;
  menuBarAlternativeContent?: FunctionComponent | ReactElement | null;
}

export interface MenuBarProps extends MenuContentProps {
  items?: MenuItem[];
  background?: string | null;
  fixed?: boolean;
  mt?: number;
}

export const MenuBar: FC<MenuBarProps> = ({
  items = [],
  background,
  fixed = false,
  mt = 0,
  inlineActions,
  menuMobileHeader,
  menuMobileFooter,
  menuMobileAlternativeContent,
  menuBarAlternativeContent,
}) => {
  const { isOpen, onToggle, showMobileExtraContent } = useMenuContext();

  const { pathname } = useRouter();

  useEffect(() => {
    if (isOpen) onToggle();
  }, [pathname]);

  return (
    <>
      {fixed && <Flex minHeight="menu.height" />}
      <Flex
        id="nav"
        as="nav"
        minHeight="menu.height"
        width="100%"
        paddingLeft={['25px', '25px', '71px']}
        paddingRight={['4px', '4px', '71px']}
        mt={mt ? `${mt}px` : ''}
        mb="-1px"
        background={background || 'transparent'}
        position={fixed ? 'fixed' : 'initial'}
        zIndex={fixed ? zIndexes.menu : 1}
        animation={fixed ? `.25s ${fadeIn} ease-in` : ''}
      >
        <Flex
          justifyContent="space-between"
          alignItems="center"
          paddingRight={fixed ? '7px' : 0}
          flexGrow={1}
        >
          <Flex justifyContent="space-between" alignItems="center" flexGrow={1}>
            {!menuBarAlternativeContent ? (
              <>
                <Link href="/">
                  <Box as="span" cursor="pointer">
                    <Icon
                      as={LogoBemHorizontalWhiteIcon}
                      marginRight="16px"
                      width="auto"
                      height="30px"
                    />
                  </Box>
                </Link>

                <Flex 
                  display={['none', 'none', 'none', 'none', 'inherit']}
                  alignItems="center"
                >
                  <MenuItemsDesktop items={items} />

                  {inlineActions}
                </Flex>
              </>
            ) : (
              menuBarAlternativeContent
            )}
          </Flex>

          {items.length && (
            <BurgerButton
              expanded={isOpen}
              onClick={onToggle}
              display={['inherit', 'inherit', 'inherit', 'inherit', 'none']}
            />
          )}
        </Flex>
      </Flex>

      {isOpen && (
        <MenuMobile mt={mt}>
          {showMobileExtraContent ? (
            menuMobileAlternativeContent
          ) : (
            <>
              {menuMobileHeader}

              <List px="24px" width="100%" flexGrow={1}>
                {items.map(({ validator: Validator = Fragment, ...item }) => (
                  <ListItem
                    borderTop="1px solid"
                    borderColor="grey.300"
                    key={item.label}
                  >
                    <Validator>
                      <MenuItemMobile {...item} />
                    </Validator>
                  </ListItem>
                ))}
              </List>

              {menuMobileFooter}
            </>
          )}
        </MenuMobile>
      )}
    </>
  );
};
