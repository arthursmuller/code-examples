import { Icon } from '@chakra-ui/react';
import React from 'react';

const AlertIcon = (props) => {
  return (
    <Icon viewBox="0 0 32 32" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="32"
        height="32"
        viewBox="0 0 32 32"
      >
        <g fill="currentColor">
          <path
            d="M0 14a14 14 0 1 1 14 14A14 14 0 0 1 0 14zm2.8 0A11.2 11.2 0 1 0 14 2.8 11.2 11.2 0 0 0 2.8 14zm9.8 5.6A1.4 1.4 0 1 1 14 21a1.4 1.4 0 0 1-1.4-1.4zm0-4.2v-7a1.4 1.4 0 0 1 2.8 0v7a1.4 1.4 0 0 1-2.8 0z"
            transform="translate(2 2)"
          />
        </g>
      </svg>
    </Icon>
  );
};

export default AlertIcon;
