import { FC } from 'react';

import {
  Box,
  Button,
  ButtonProps,
  Icon,
  IconProps,
  Text,
} from '@chakra-ui/react';

import { ArrowOutlineRightIcon } from '@pcf/design-system-icons';

import {
  ColorSchemes,
  ColorVariants,
} from '../../bem-chakra-theme/foundations/colors';
import { CustomHeading } from '../custom-heading';

export interface ButtonCardProps extends ButtonProps {
  title: string;
  description?: string;
  icon?: FC;
  iconProps?: IconProps;
  colorScheme?:
    | ColorSchemes.primary
    | ColorSchemes.secondary
    | `${ColorSchemes.primary}`
    | `${ColorSchemes.secondary}`;
}

export const ButtonCard: FC<ButtonCardProps> = ({
  title,
  description,
  icon,
  iconProps = {},
  colorScheme = ColorSchemes.secondary,
  ...buttonProps
}: ButtonCardProps) => {
  const color = buttonProps.disabled
    ? 'grey.600'
    : `${colorScheme}.${ColorVariants.regular}`;

  const colorIcon = buttonProps.disabled
    ? 'grey.500'
    : iconProps?.color || 'grey.700';

  const extraStyles = buttonProps.disabled
    ? {}
    : {
        background: 'white',
      };

  return (
    <Button
      justifyContent="left"
      display="flex"
      h="auto"
      colorScheme="grey"
      paddingRight={[6, 6, 8]}
      paddingLeft={!icon ? [6, 6, 8] : [4, 4, 6]}
      paddingY={[6, 6, 4]}
      boxShadow="soft"
      {...extraStyles}
      {...buttonProps}
    >
      {icon && (
        <Icon
          as={icon}
          boxSize={6}
          marginRight={4}
          {...iconProps}
          color={colorIcon}
        />
      )}

      <Box flex={1}>
        <CustomHeading
          as="h3"
          textStyle="bold20"
          textAlign="start"
          color={color}
        >
          {title}
        </CustomHeading>

        {description && (
          <Text
            as="p"
            textStyle="regular16"
            whiteSpace="normal"
            textAlign="start"
            fontWeight="400"
            color={color}
          >
            {description}
          </Text>
        )}
      </Box>

      <Icon
        ml={3}
        as={ArrowOutlineRightIcon}
        boxSize={3}
        color={iconProps?.color || color}
      />
    </Button>
  );
};
