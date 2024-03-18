import { FC } from 'react';

import { ButtonProps, Icon, IconButton } from '@chakra-ui/react';

import { ArrowRightIcon, ArrowLeftIcon } from '@pcf/design-system-icons';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

export interface NavigationActionProps extends ButtonProps {
  schemeColor?: ColorSchemes | keyof typeof ColorSchemes;
  direction: 'left' | 'right';
}

export const NavigationAction: FC<NavigationActionProps> = ({
  schemeColor = ColorSchemes.grey,
  direction,
  ...props
}) => {
  return (
    <IconButton
      {...props}
      size="sm"
      isRound
      height="24px"
      minWidth="24px"
      colorScheme={schemeColor}
      aria-label={direction}
      icon={
        direction === 'left' ? (
          <Icon as={ArrowLeftIcon} mr="2px" width="8px" height="12px" />
        ) : (
          <Icon as={ArrowRightIcon} ml="1px" width="8px" height="12px" />
        )
      }
    >
      {direction === 'left' ? '<' : '>'}
    </IconButton>
  );
};
