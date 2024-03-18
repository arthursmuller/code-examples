import {
  ThemeComponentProps,
  ComponentMultiStyleConfig,
  SystemStyleObjectRecord,
} from '@chakra-ui/react';

import { Colors, ColorSchemes, ColorVariants } from '../../foundations/colors';
import textStyles from '../../foundations/typography';

export const Accordion: ComponentMultiStyleConfig = {
  parts: ['container', 'button', 'panel', 'icon'],
  baseStyle: ({
    colorScheme = 'primary',
    theme: { colors },
  }: ThemeComponentProps): SystemStyleObjectRecord => {
    const isSecondary = colorScheme === ColorSchemes.secondary;

    const bgHover = colors[colorScheme][ColorVariants.regular];

    const bgExpanded = isSecondary ? bgHover : colors[Colors.white];
    const colorExpanded = isSecondary
      ? colors[Colors.white]
      : colors[Colors.primary][ColorVariants.regular];

    const boxShadow = isSecondary ? '' : 'medium';

    return {
      container: {
        bg: 'white',
        borderColor: 'transparent',
      },
      button: {
        ...textStyles.bold20,
        bg: 'white',
        borderRadius: '8px',
        color: 'primary.regular',
        p: '22px 28px',

        boxShadow,

        _hover: {
          bg: bgHover,
          color: 'white',
          boxShadow,
        },

        _expanded: {
          borderRadius: '8px 8px 0px 0px',
          bg: bgExpanded,
          color: colorExpanded,

          _hover: {
            bg: bgHover,
            color: 'white',
            boxShadow,
          },

          boxShadow,
        },
      },
      panel: {},
      icon: {},
    };
  },
};
