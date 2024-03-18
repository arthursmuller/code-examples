import {
  extendTheme,
  withDefaultProps,
  withDefaultSize,
} from '@chakra-ui/react';

import styles from './styles';
import components from './components';
import foundations from './foundations';
import textStyles from './foundations/typography';

const bemTheme = extendTheme(
  {
    styles,
    textStyles,
    ...foundations,

    components,

    sizes: {
      menu: {
        height: '80px',
      },
      nav: {
        height: '72px',
      },
    },

    shadows: {
      soft: '0px 2.32703px 4.5px rgba(105, 112, 117, 0.2)',
      medium: '0px 9.30811px 18.5px rgba(18, 27, 33, 0.1)',
      strong: '0px 18.6162px 116px rgba(18, 27, 33, 0.25)',
      card: '0px 4px 24px rgba(0, 0, 0, 0.15)',
    },
  },
  withDefaultProps({
    defaultProps: {
      colorScheme: 'primary',
      variant: 'solid',
      size: 'lg',
    },
  }),
  withDefaultSize({
    size: 'md',
    components: ['RadioGroup', 'Radio'],
  }),
);

export type Theme = typeof bemTheme;

export default bemTheme;
