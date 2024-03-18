import React from 'react';
import CSSReset from '@chakra-ui/css-reset';
import { ChakraProvider, ThemeProvider } from '@chakra-ui/react';
import { addDecorator } from '@storybook/react';
import theme from '../src/theme';

export const Chakra = ({ children }) => (
  <ThemeProvider theme={theme}>
    <ChakraProvider theme={theme}>
      <CSSReset />
      {children}
    </ChakraProvider>
  </ThemeProvider>
);

addDecorator((StoryFn) => (
  <Chakra>
    <StoryFn />
  </Chakra>
));
