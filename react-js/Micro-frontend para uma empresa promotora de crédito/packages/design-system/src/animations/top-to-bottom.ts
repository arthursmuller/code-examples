import { keyframes } from '@chakra-ui/react';
import { Keyframes } from '@emotion/react/node_modules/@emotion/serialize';

export const topToBottom: Keyframes = keyframes`
 0% {
    opacity: 0;
    transform:translateY(-10px);
  }
 100% {
    opacity: 1;
    transform:translateY(0);
 }
`;
