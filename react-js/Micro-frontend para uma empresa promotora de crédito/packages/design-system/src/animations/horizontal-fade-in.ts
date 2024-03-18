import { keyframes } from '@chakra-ui/react';
import { Keyframes } from '@emotion/react/node_modules/@emotion/serialize';

export const rightToLeft: Keyframes = keyframes`
  0% {
    opacity: 0;
    transform: translateX(10rem);
    }
  80% {
    transform: translateX(-1rem);
    }
  100% {
    opacity: 1;
    transform: translate(0);
    }
`;

export const leftToRight: Keyframes = keyframes`
  0% {
    opacity: 0;
    transform: translateX(-10rem);
    }
  80% {
    transform: translateX(1rem);
    }
  100% {
    opacity: 1;
    transform: translate(0);
    }
`;
