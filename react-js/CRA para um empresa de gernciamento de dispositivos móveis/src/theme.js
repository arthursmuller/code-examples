import { extendTheme } from '@chakra-ui/react';
const themeOption = {
  colors: {
    transparent: 'transparent',
    white: '#fff',
    gray: {
      300: '#a0a0a5',
      400: '#f2f4f8',
      500: '#6e6e78',
      600: '#d7d7dc',
    },
    black: {
      500: '#282832',
    },
    red: {
      500: '#ff0000',
    },
    blue: {
      500: '#0190fe',
      600: '#0a3b79',
    },
    green: {
      500: '#00c3af',
    },
  },
  fonts: {
    body: '"Open Sans", no-serif',
    heading: 'Open Sans',
    mono: 'Open Sans',
  },
  fontSizes: {
    xs: '12px',
    sm: '14px',
    md: '16px',
    lg: '18px',
    xl: '20px',
    '2xl': '24px',
    '3xl': '28px',
    '4xl': '32px',
    '5xl': '40px',
    '6xl': '64px',
  },
  sizes: {
    full: '100%',
    '3xs': '14rem',
    '2xs': '16rem',
    xs: '20rem',
    sm: '24rem',
    md: '28rem',
    lg: '32rem',
    xl: '36rem',
    '2xl': '42rem',
    '3xl': '48rem',
    '4xl': '56rem',
    '5xl': '64rem',
    '6xl': '72rem',
  },
  breakpoints: {
    sm: '320px',
    md: '768px',
    lg: '960px',
    xl: '1200px',
    xxl: '1336px',
  },
};

const theme = extendTheme(themeOption);
export default theme;
