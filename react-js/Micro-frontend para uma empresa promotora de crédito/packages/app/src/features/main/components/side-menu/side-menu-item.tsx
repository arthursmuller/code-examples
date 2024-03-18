import { ReactElement, FC } from 'react';

import { Flex, Icon, Text, useTheme } from '@chakra-ui/react';
import { useHistory, useLocation } from 'react-router-dom';

import { Loader } from '@pcf/design-system';
import { MenuItem as MenuItemDefault } from 'components/menu-bar/menu-item.model';

const active = { borderLeft: '4px solid white', bg: 'primary.light' };

export interface SideMenuItemProps extends MenuItemDefault {
  content?: ReactElement;
  isLoading?: boolean;
  flagKey?: string;
}

export const SideMenuItem: FC<SideMenuItemProps> = ({
  icon,
  iconActive,
  label,
  route = '',
  disabled,
  isLoading,
}) => {
  const { pathname } = useLocation();
  const history = useHistory();
  const theme = useTheme();

  const isActive = pathname.includes(route);

  function handleClick(): void {
    history.push(route);
  }

  const color = disabled ? 'primary.dark' : 'white';

  return (
    <Flex
      as="button"
      width="100%"
      _hover={{ bg: 'primary.light' }}
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
      cursor={disabled ? 'not-allowed' : 'pointer'}
      disabled={disabled}
      height="80px"
      transition="all .2s"
      onClick={handleClick}
      outline="none"
      _focus={{
        boxShadow: theme.shadows.outline,
      }}
      sx={isActive ? active : {}}
    >
      {isLoading ? (
        <Loader size="sm" theme="white" />
      ) : (
        <>
          {icon && <Icon as={isActive ? iconActive : icon} color={color} />}
          <Text mx="8px" mt="8px" textStyle="bold14" color={color}>
            {label}
          </Text>
        </>
      )}
    </Flex>
  );
};
