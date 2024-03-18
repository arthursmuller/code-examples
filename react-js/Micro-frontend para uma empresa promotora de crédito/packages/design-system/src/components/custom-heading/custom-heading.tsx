import { Box, BoxProps } from '@chakra-ui/react';

import { fonts } from '../../bem-chakra-theme/foundations/typography';

// Foi criado esse componente, pois o componente default Heading do chakra-ui
// parou de aceitar a prop textStyle.
// Portanto será que feito o workaround sugerido aqui até que se tenha uma decisão da equipe chakra.
// https://github.com/chakra-ui/chakra-ui/issues/3501

export const CustomHeading = (props: BoxProps): React.ReactElement => {
  return <Box fontFamily={fonts.heading} {...props} />;
};
