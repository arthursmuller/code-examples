import {
  ComponentSingleStyleConfig,
  SystemStyleObject,
  ThemeComponentProps,
} from '@chakra-ui/react';

import { Colors, ColorSchemes, ColorVariants } from '../../foundations/colors';
import textStyles from '../../foundations/typography';

const variants = {
  solid: ({
    colorScheme = 'primary',
    theme: { colors },
  }: ThemeComponentProps): SystemStyleObject => {
    let variantBgHover: any | number = ColorVariants.dark;
    let variantBgActive: any | number = ColorVariants.regular;
    let variantBg: any | number = ColorVariants.regular;
    let color: string = Colors.white;
    let boxShadow = 'none';
    let bg;

    if (colorScheme === ColorSchemes.grey) {
      variantBgHover = 200;
      variantBgActive = 100;
      variantBg = 100;
      color = colors[Colors.primary][ColorVariants.regular];
      boxShadow = 'medium';
    }

    bg = colors[colorScheme][variantBg];

    if (
      colorScheme === ColorSchemes.primary ||
      colorScheme === ColorSchemes.secondary
    ) {
      bg = colors[colorScheme][ColorVariants.gradient];
    }

    const disabled = {
      bg: colors[Colors.grey][300],
      color: colors[Colors.grey][500],
      transition: 'background 0s',
    };

    return {
      color,
      bg,
      boxShadow,

      _hover: {
        background: `${colors[colorScheme][variantBgHover]} radial-gradient(circle, transparent 1%, ${colors[colorScheme][variantBgHover]} 1%) center/15000%`,
        boxShadow: 'soft',
        _disabled: disabled,
      },

      _active: {
        backgroundColor: colors[colorScheme][variantBgActive],
        backgroundSize: '100%',
        transition: 'background 0s',
        boxShadow: 'soft',
      },

      _disabled: disabled,
    };
  },
  outline: ({
    colorScheme = 'primary',
    theme: { colors },
  }: ThemeComponentProps): SystemStyleObject => {
    const disabled = {
      color: colors[Colors.grey][500],
      borderColor: colors[Colors.grey][300],
    };

    const color =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 700 : ColorVariants.regular
      ];
    const hoverColor =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 400 : ColorVariants.washed
      ];

    return {
      color,
      border: `1.5px solid`,
      borderColor: color,

      _hover: {
        background: `${hoverColor} radial-gradient(circle, transparent 1%, ${hoverColor} 1%) center/15000%`,
        _disabled: disabled,
      },

      _active: {
        backgroundColor: hoverColor,
        backgroundSize: '100%',
        transition: 'background 0s',
      },

      _disabled: disabled,
    };
  },
  ghost: ({
    colorScheme = 'primary',
    theme: { colors },
  }: ThemeComponentProps): SystemStyleObject => {
    const disabled = {
      color: colors[Colors.grey][500],
    };

    const color =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 700 : ColorVariants.regular
      ];
    const hoverColor =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 200 : ColorVariants.washed
      ];

    return {
      color,

      _hover: {
        background: `${hoverColor} radial-gradient(circle, transparent 1%, ${hoverColor} 1%) center/15000%`,
        _disabled: disabled,
      },

      _active: {
        backgroundColor: hoverColor,
        backgroundSize: '100%',
        transition: 'background 0s',
      },

      _disabled: disabled,
    };
  },
  link: ({
    colorScheme = 'primary',
    theme: { colors },
  }: ThemeComponentProps): SystemStyleObject => {
    const disabled = {
      color: colors[Colors.grey][500],
    };

    const color =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 700 : ColorVariants.regular
      ];
    const hoverColor =
      colors[colorScheme][
        colorScheme === ColorSchemes.grey ? 200 : ColorVariants.washed
      ];

    return {
      color,

      _hover: {
        color: `${hoverColor} radial-gradient(circle, transparent 1%, ${hoverColor} 1%) center/15000%`,
        _disabled: disabled,
      },

      _disabled: disabled,
    };
  },
};

export const Button: ComponentSingleStyleConfig = {
  baseStyle: {
    borderRadius: 'lg',
    backgroundPosition: 'center',
    transition: 'background .8s',
    whiteSpace: 'break-spaces',

    _disabled: {
      opacity: 1,
      cursor: 'not-allowed',
      boxShadow: 'none',
    },
  },
  variants,
  sizes: {
    lg: {
      ...textStyles.bold16,
      fontSize: '1rem',
    },
  },
};
