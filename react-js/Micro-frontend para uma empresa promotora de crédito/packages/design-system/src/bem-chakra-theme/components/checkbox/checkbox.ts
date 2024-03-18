import { SystemStyleObject, ThemeComponentProps } from '@chakra-ui/react';

import { ColorVariants } from '../../foundations/colors';

export const Checkbox = {
  baseStyle: ({
    colorScheme,
    theme,
  }: ThemeComponentProps): SystemStyleObject => {
    const cs = colorScheme || 'primary';
    const color = theme.colors[cs][ColorVariants.regular];
    const bg = theme.colors[cs][ColorVariants.washed];

    return {
      control: {
        borderColor: theme.colors.grey[600],
        border: '1px solid',
        borderRadius: '4px',
        transition: 'all .2s',

        padding: '11px',

        bg: theme.colors.grey[200],

        _checked: {
          bg,
          color,
          borderColor: color,
        },
      },
    };
  },
};
