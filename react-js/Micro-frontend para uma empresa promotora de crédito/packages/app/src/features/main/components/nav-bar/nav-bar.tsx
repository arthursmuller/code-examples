import { Children, FC } from 'react';

import {
  Flex,
  Button,
  Icon,
  Divider,
  ComponentWithAs,
  ButtonProps,
  forwardRef,
} from '@chakra-ui/react';
import { Link as ReactRouterDomLink } from 'react-router-dom';

import {
  LogoBemHorizontalIconIcon,
  TabSairInativaIcon,
} from '@pcf/design-system-icons';
import { mainRoutePaths } from 'features/main/routes';

import { Search } from './search';

export const NavBar: FC = ({ children }) => {
  return (
    <Flex
      flex="1"
      height="nav.height"
      bg="white"
      alignItems="center"
      justifyContent="space-between"
      paddingX={6}
      paddingY={4}
      boxShadow="medium"
    >
      <LogoBemHorizontalIconIcon width="230px" height="30px" />

      <Search />

      {Children.map(children, (child) => (
        <Flex marginLeft={6}>{child}</Flex>
      ))}

      <Divider orientation="vertical" borderColor="grey.300" marginX={6} />

      <Button
        variant="link"
        as={ReactRouterDomLink}
        to={mainRoutePaths.LOGOUT}
        size="sm"
        color="primary.regular"
        rightIcon={<Icon as={TabSairInativaIcon} name="sair" boxSize="25px" />}
      >
        Sair
      </Button>
    </Flex>
  );
};

interface NavBarButtonProps extends ComponentWithAs<'button', ButtonProps> {
  icon: FC;
  label?: string | number;
  active?: boolean;
}

export const NavBarButton = forwardRef<any, NavBarButtonProps>(
  ({ icon, label, active, ...props }, ref) => (
    <Button
      variant="outline"
      colorScheme="primary"
      size="md"
      borderRadius="100px"
      height="32px"
      aria-label="configs"
      ref={ref}
      {...(active
        ? { backgroundColor: 'primary.regular', fill: 'white', color: 'white' }
        : { fill: 'primary.regular' })}
      {...props}
    >
      <Icon as={icon} height="24px" width="24px" fill="inherit" />
      {label ? ` ${label}` : ''}
    </Button>
  ),
);
