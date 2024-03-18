import { Icon } from '@chakra-ui/react';
import React from 'react';

const AlertModal = (props) => {
  return (
    <Icon viewBox="0 0 96 96" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="96"
        height="95.999"
        viewBox="0 0 96 95.999"
      >
        <g>
          <path
            fill="#d7d7dc"
            d="M0 48A47.874 47.874 0 0 1 29.622 3.644 48.008 48.008 0 1 1 0 48zm9.6 0A38.4 38.4 0 1 0 48 9.6 38.4 38.4 0 0 0 9.6 48zm33.6 19.2A4.8 4.8 0 1 1 48 72a4.8 4.8 0 0 1-4.8-4.8zm0-14.4v-24a4.8 4.8 0 1 1 9.6 0v24a4.8 4.8 0 1 1-9.6 0z"
          />
        </g>
      </svg>
    </Icon>
  );
};

export default AlertModal;
