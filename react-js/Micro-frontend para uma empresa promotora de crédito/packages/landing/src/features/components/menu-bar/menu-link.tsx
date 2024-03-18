import { ReactElement, FC } from 'react';

import Link from 'next/link';
import { Link as ChakraLink } from '@chakra-ui/react';

interface LinkOrExternalProps {
  isExternal?: boolean;
  route?: string;
  label: ReactElement | string;
}

export const LinkOrExternal: FC<LinkOrExternalProps> = ({
  isExternal,
  label,
  route,
}) =>
  !isExternal ? (
    <Link href={route}>{label}</Link>
  ) : (
    <a href={route} target="_blank" rel="noreferrer">
      {label}
    </a>
  );

interface MenuLinkProps extends LinkOrExternalProps {
  onClick?: () => void;

  isActive?: boolean;
}

export const MenuLink: FC<MenuLinkProps> = ({
  onClick,
  isActive,
  isExternal,
  label,
  route,
}) => {
  return isExternal ? (
    <ChakraLink
      color="white"
      layerStyle={isActive ? 'navActive' : 'nav'}
      onClick={onClick}
      href={route}
      target="_blank"
      rel="noreferrer"
      maxW="fit-content"
      whiteSpace="nowrap"
      fontSize={['1vw', '1vw', '1vw', '1vw', '1vw', 'unset']}
    >
      {label}
    </ChakraLink>
  ) : (
    <ChakraLink
      as="div"
      color="white"
      layerStyle={isActive ? 'navActive' : 'nav'}
      onClick={onClick}
      maxW="fit-content"
      whiteSpace="nowrap"
      fontSize={['1vw', '1vw', '1vw', '1vw', '1vw', 'unset']}
    >
      <Link href={route}>{label}</Link>
    </ChakraLink>
  );
};
