export const fonts = {
  body: 'Inter, sans-serif',
  heading: 'Inter, serif',
  mono: 'Menlo, monospace',
};

export enum FontWeights {
  bold = 700,
  medium = 500,
  regular = 400,
  light = 300,
  large = 900,
}

/* eslint-disable */
export const fontPropsFor = (size: number | number[], height: number | number[], weight: number = FontWeights.regular, props = {}) => ({
  color: 'inherit',
  fontSize: Array.isArray(size) ? size.map(s => `${s}px`) : `${size}px`,
  lineHeight: Array.isArray(height) ? height.map(s => `${s}px`) : `${height}px`,
  fontWeight: `${weight}`,
  ...props
})

const textStyles = {
  // BODIES

  regular10:    fontPropsFor(10, 14),
  bold10:       fontPropsFor(10, 14, FontWeights.bold),

  regular12:    fontPropsFor(12, 16),
  bold12:       fontPropsFor(12, 16, FontWeights.bold),

  regular14:    fontPropsFor(14, 20),
  light14:      fontPropsFor(14, 20, FontWeights.light),
  bold14:       fontPropsFor(14, 20, FontWeights.bold),

  regular16:    fontPropsFor(16, 24),
  bold16:       fontPropsFor(16, 24, FontWeights.bold),

  //  HEADINGS

  //    H5
  regular20:    fontPropsFor(20, 24),
  bold20:       fontPropsFor(20, 24, FontWeights.bold),
  //    H4
  regular24:    fontPropsFor(24, 32),
  bold24:       fontPropsFor(24, 32, FontWeights.bold),
  //    H3
  regular32:    fontPropsFor(32, 40),
  bold32:       fontPropsFor(32, 40, FontWeights.bold),
  //    H2
  regular40:    fontPropsFor(40, 48),
  bold40:       fontPropsFor(40, 48, FontWeights.bold),
  //    H1
  regular48:    fontPropsFor(48, 56),
  bold48:       fontPropsFor(48, 56, FontWeights.bold),

  //    H2 -> H1
  bold40_48:    fontPropsFor([40, 40, 48], [48, 48, 56], FontWeights.bold),

  //    H3 -> H2
  bold32_40:    fontPropsFor([32, 32, 40], [40, 40, 48], FontWeights.bold),

  //    H4 -> H3
  bold24_32:    fontPropsFor([24, 24, 32], [32, 32, 40], FontWeights.bold),

  //    H4 -> H2
  bold24_40:    fontPropsFor([24, 24, 40], [32, 32, 40], FontWeights.bold),

  //    H5 -> H4
  regular20_24: fontPropsFor([20, 20, 24], [24, 24, 32]),
  bold20_24:    fontPropsFor([20, 20, 24], [24, 24, 32], FontWeights.bold),

  regular24_32: fontPropsFor([24, 24, 32], [24, 24, 32]),

  //    P -> H5
  regular16_20: fontPropsFor([16, 16, 20], 24),
  bold16_20:    fontPropsFor([16, 16, 20], 24, FontWeights.bold),
  bold16_24:    fontPropsFor([16, 16, 24], 24, FontWeights.bold),

  //    Page title
  headline:     fontPropsFor([36, 36, 56], [38, 64], FontWeights.large, { textTransform: 'uppercase', }),

  headline2:    fontPropsFor([32, 32, 48], [32, 48], FontWeights.bold, { textTransform: 'uppercase', }),
};
/* eslint-enable */

export default textStyles;
