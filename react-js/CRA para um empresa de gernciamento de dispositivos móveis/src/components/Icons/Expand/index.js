import { Icon } from '@chakra-ui/react';
import React from 'react';

const Expand = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        id="Layer_2"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="arrowhead-down">
          <path
            id="Caminho_40"
            d="M17.37 12.39L12 16.71l-5.36-4.48a1 1 0 1 0-1.28 1.54l6 5a1 1 0 0 0 1.27 0l6-4.83a1 1 0 1 0-1.26-1.55z"
            fill="currentColor"
          />
          <path
            id="Caminho_41"
            d="M11.36 11.77a1 1 0 0 0 1.27 0l6-4.83a1 1 0 1 0-1.26-1.56L12 9.71 6.64 5.23a1 1 0 1 0-1.28 1.54z"
            fill="currentColor"
          />
        </g>
      </svg>
    </Icon>
  );
};

export default Expand;
