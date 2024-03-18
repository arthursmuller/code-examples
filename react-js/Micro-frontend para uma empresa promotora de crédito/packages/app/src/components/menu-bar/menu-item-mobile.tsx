import { FC } from 'react';

import {
  Flex,
  Icon,
  Box,
  Accordion,
  AccordionItem,
  AccordionButton,
  AccordionPanel,
  Text,
} from '@chakra-ui/react';
import { useLocation, Link } from 'react-router-dom';

import { Loader } from '@pcf/design-system';
import { ArrowUpIcon } from '@pcf/design-system-icons';

import { MenuItem } from './menu-item.model';
import { useMenuContext } from './menu.context';

const Styles = {
  base: {
    textStyle: 'regular16',
    color: 'grey.800',
    py: '20px',
  },
  active: {
    textStyle: 'bold16',
    color: 'primary.regular',
  },
};

interface MenuItemMobile extends MenuItem {
  isLoading?: boolean;
}

export const MenuItemMobile: FC<MenuItemMobile> = ({
  label,
  route,
  icon,
  iconActive,
  isExternal,
  disabled,
  items: subItems,
  isLoading,
  children,
}) => {
  const { pathname } = useLocation();
  const { onToggle } = useMenuContext();

  const isActive =
    (route && pathname === route) ||
    subItems?.find((i) => i.route && pathname?.includes(i.route));

  const { color } = Styles[isActive ? 'active' : 'base'];

  function handleClick(): void {
    if (isExternal) {
      window.open(route, '_blank');
    }

    onToggle();
  }

  const content = (
    <Flex alignItems="center" color={color}>
      {icon && (
        <Icon as={isActive ? iconActive : icon} boxSize="24px" mr="13px" />
      )}

      <Box marginRight={1}>
        <Text
          {...(isActive ? { ...Styles.base, ...Styles.active } : Styles.base)}
        >
          {label}
        </Text>
      </Box>

      {isLoading && <Loader size="sm" />}

      {children}
    </Flex>
  );

  if (subItems) {
    return (
      <Accordion allowMultiple>
        <AccordionItem background="none">
          {({ isExpanded }) => (
            <>
              <AccordionButton
                variant="link"
                layerStyle={isActive ? 'navActive' : 'nav'}
                fontWeight="inherit"
                padding={0}
                paddingRight={4}
                sx={{
                  background: 'none !important',
                  boxShadow: 'none !important',
                  borderColor: color,
                }}
                _focus={{
                  boxShadow: 'none',
                }}
              >
                <Box flex={1} textAlign="start">
                  <Text
                    {...(isActive
                      ? { ...Styles.base, ...Styles.active }
                      : {
                          ...Styles.base,
                          color: isExpanded ? 'primary.regular' : color,
                        })}
                  >
                    {label}
                  </Text>
                </Box>

                <Icon
                  as={ArrowUpIcon}
                  width="12px"
                  height="12p"
                  transform={isExpanded ? 'rotate(0deg)' : 'rotate(180deg)'}
                  transition="transform .25s"
                  color={color}
                />
              </AccordionButton>

              <AccordionPanel padding="0">
                {subItems.map((item) => (
                  <Flex
                    key={item.label}
                    borderTop="1px solid"
                    borderColor="grey.300"
                    paddingLeft={6}
                  >
                    <MenuItemMobile {...item} />
                  </Flex>
                ))}
              </AccordionPanel>
            </>
          )}
        </AccordionItem>
      </Accordion>
    );
  }

  return !disabled ? (
    <Link to={route || ''} onClick={handleClick}>
      {content}
    </Link>
  ) : (
    <Box opacity="0.5">{content}</Box>
  );
};
