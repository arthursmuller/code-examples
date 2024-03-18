import { FC } from 'react';

import { Link, LinkProps } from '@chakra-ui/react';

import {
  ColorSchemes,
  ColorVariants,
} from '../../bem-chakra-theme/foundations/colors';

const [activeBulletTone, inactiveBulletTone] = [
  {
    [ColorSchemes.grey]: 100,
    [ColorSchemes.secondary]: ColorVariants.regular,
  },
  {
    [ColorSchemes.grey]: 500,
    [ColorSchemes.secondary]: ColorVariants.light,
  },
];

export interface NavigationBulletProp extends LinkProps {
  schemeColor?: ColorSchemes.grey | ColorSchemes.secondary;
  active?: boolean;
}

export const NavigationBullet: FC<NavigationBulletProp> = ({
  schemeColor = ColorSchemes.grey,
  active,
  ...props
}) => {
  return (
    <Link
      {...props}
      display="inline-block"
      width={5}
      fontSize="10px"
      color={`${schemeColor}.${
        active ? activeBulletTone[schemeColor] : inactiveBulletTone[schemeColor]
      }`}
      _hover={{ textDecoration: 'none' }}
    >
      &#9679;
    </Link>
  );
};
