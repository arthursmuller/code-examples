import { keyframes } from '@chakra-ui/react';
import { Keyframes } from '@emotion/react/node_modules/@emotion/serialize';

export const infiniteSpinning: Keyframes = keyframes`
  to {
    transform:rotate(360deg);
  }
`;
