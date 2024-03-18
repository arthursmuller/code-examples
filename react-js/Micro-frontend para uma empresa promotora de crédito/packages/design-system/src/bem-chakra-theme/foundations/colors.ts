export const enum ColorSchemes {
  primary = 'primary',
  secondary = 'secondary',
  success = 'success',
  error = 'error',
  warning = 'warning',
  grey = 'grey',
}

export const enum Colors {
  primary = 'primary',
  secondary = 'secondary',
  success = 'success',
  error = 'error',
  warning = 'warning',
  grey = 'grey',
  white = 'white',
  black = 'black',
}

export const enum ColorVariants {
  dark = 'dark',
  midDark = 'mid-dark',
  regular = 'regular',
  light = 'light',
  midLight = 'mid-light',
  washed = 'washed',
  gradient = 'gradient',
}

type colorType = keyof typeof ColorSchemes;
type colorVariant = ColorVariants | number;

const colors: {
  [key in colorType]: {
    [key in colorVariant]?: string;
  };
} = {
  [ColorSchemes.primary]: {
    [ColorVariants.washed]: '#FFEADE',
    [ColorVariants.light]: ' #FF9122',
    [ColorVariants.midLight]: '#FF6518',
    [ColorVariants.regular]: '#E15100',
    [ColorVariants.midDark]: '#C3410E',
    [ColorVariants.dark]: '#9A3108',

    [ColorVariants.gradient]: 'linear-gradient(90deg, #FFBE2D 0, #FF6518 100%)',
  },
  [ColorSchemes.secondary]: {
    [ColorVariants.washed]: '#E8F1FF ',
    [ColorVariants.light]: '#5889FA',
    [ColorVariants.midLight]: '#4264CE',
    [ColorVariants.regular]: '#2B3EA1',
    [ColorVariants.midDark]: '#162776 ',
    [ColorVariants.dark]: '#08175B',

    [ColorVariants.gradient]:
      'linear-gradient(90deg, #5889FA 0%, #2B3EA1 100%)',
  },
  [ColorSchemes.success]: {
    [ColorVariants.washed]: '#EAFCE3',
    [ColorVariants.regular]: '#69AF1A',
    [ColorVariants.dark]: '#4A7D0F',
  },
  [ColorSchemes.error]: {
    [ColorVariants.washed]: '#FFE6E6',
    [ColorVariants.regular]: '#FF4C4C',
    [ColorVariants.dark]: '#CE2121',
  },
  [ColorSchemes.warning]: {
    [ColorVariants.washed]: '#FFF7E3',
    [ColorVariants.light]: '#FFBE2D',
    [ColorVariants.regular]: '#F6A405',
    [ColorVariants.dark]: '#D68812',
  },
  [ColorSchemes.grey]: {
    100: '#FAFAFA',
    200: '#F1F1F1',
    300: '#DEE0E1',
    400: '#BDBDBD',
    500: '#9E9E9E',
    600: '#757575',
    700: '#616161',
    800: '#424242',
    900: '#212121',
  },
};

export default colors;
