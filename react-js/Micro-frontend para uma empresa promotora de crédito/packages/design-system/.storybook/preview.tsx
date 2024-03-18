import { ChakraProvider } from '@chakra-ui/react';

import bemTheme from '../src/bem-chakra-theme';

export const parameters = {
  actions: { argTypesRegex: '^[A-Z].*' },
};

export const decorators = [
  (Story) => (
    <ChakraProvider resetCSS theme={bemTheme}>
      <Story />
    </ChakraProvider>
  ),
];
