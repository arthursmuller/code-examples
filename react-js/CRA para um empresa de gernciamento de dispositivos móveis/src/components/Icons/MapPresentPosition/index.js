import { Icon } from '@chakra-ui/react';
import React from 'react';

const MapPresentPositionIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g>
          <g fill="#00d2b5">
            <path
              d="M20 20a.94.94 0 0 1-.55-.17l-6.9-4.56a1 1 0 0 0-1.1 0l-6.9 4.56a1 1 0 0 1-1.44-1.28l8-16a1 1 0 0 1 1.78 0l8 16A1 1 0 0 1 20 20z"
              transform="rotate(180 12 12)"
            />
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default MapPresentPositionIcon;
