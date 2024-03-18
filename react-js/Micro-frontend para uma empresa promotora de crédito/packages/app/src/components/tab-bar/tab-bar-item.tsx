import { ComponentType, FC, FunctionComponent } from 'react';

import { Flex, Icon, Text } from '@chakra-ui/react';
import { NavLink, useLocation } from 'react-router-dom';

export interface MenuItem {
  label: string;
  icon?: FunctionComponent | ComponentType;
  iconActive?: FunctionComponent | ComponentType;
  route?: string;
  disabled?: boolean;
  isExternal?: boolean;
  items?: MenuItem[];
  menuColor?: string;
  validator?: FC;
}

export interface TabBarItemProps extends MenuItem {
  onClick?: () => void;
}

export const TabBarItem: FC<TabBarItemProps> = ({
  route = '',
  icon,
  iconActive,
  label,
  disabled,
  onClick,
}) => {
  const { pathname } = useLocation();

  const isActive = pathname === route;
  const color = isActive ? 'primary.regular' : 'grey.600';

  const content = (
    <Flex
      px="5px"
      flexDir="column"
      alignItems="center"
      h="48px"
      color={color}
      opacity={disabled ? '0.6' : 'inherit'}
      onClick={() => !disabled && onClick && onClick()}
    >
      <Icon boxSize="18px" as={isActive ? iconActive : icon} mt="10px" />

      <Text color={color} textStyle="regular12" mt="5px">
        {label}
      </Text>
    </Flex>
  );

  return !disabled && route ? (
    <NavLink to={route} isActive={(_, location) => location.pathname === route}>
      {content}
    </NavLink>
  ) : (
    <>{content}</>
  );
};
