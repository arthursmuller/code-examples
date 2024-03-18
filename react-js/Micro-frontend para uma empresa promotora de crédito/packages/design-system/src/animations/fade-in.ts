import { keyframes } from '@chakra-ui/react';
import { Keyframes } from '@emotion/react/node_modules/@emotion/serialize';

export const fadeIn: Keyframes = keyframes`
0% {
   opacity: 0;
 }
100% {
   opacity: 1;
}
`;
