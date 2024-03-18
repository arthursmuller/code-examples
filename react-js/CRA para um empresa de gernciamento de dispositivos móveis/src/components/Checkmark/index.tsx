import { Icon } from '@chakra-ui/react';
import React from 'react';

const Checkmark = (props) => {
  return (
    <Icon viewBox="0 0 32 32" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        id="Layer_2"
        width="32"
        height="32"
        viewBox="0 0 32 32"
      >
        <g id="checkmark-circle">
          <path
            id="Caminho_207"
            d="M10.284 13.05a1.34 1.34 0 0 0-1.9 1.9l4 4a1.3 1.3 0 0 0 1.909-.067L23.645 8.2a1.335 1.335 0 0 0-2-1.762l-8.3 9.61z"
            fill="currentColor"
            transform="translate(2.677 2.02)"
          />
          <path
            id="Caminho_208"
            d="M27.343 14.013a1.335 1.335 0 0 0-1.335 1.335A10.675 10.675 0 1 1 15.33 4.67a11.733 11.733 0 0 1 2.536.294 1.335 1.335 0 1 0 .627-2.589A14.069 14.069 0 0 0 15.33 2a13.348 13.348 0 1 0 13.348 13.348 1.335 1.335 0 0 0-1.335-1.335z"
            fill="currentColor"
            transform="translate(.687 .67)"
          />
        </g>
      </svg>
    </Icon>
  );
};

export default Checkmark;
