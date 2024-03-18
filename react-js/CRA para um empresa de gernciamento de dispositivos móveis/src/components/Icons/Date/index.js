import { Icon } from '@chakra-ui/react';
import React from 'react';

const Date = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Layer_2" transform="rotate(180 14 12)">
          <g id="calendar" transform="rotate(180 14 12)">
            <path
              id="Caminho_42"
              d="M18 4h-1V3a1 1 0 0 0-2 0v1H9V3a1 1 0 0 0-2 0v1H6a3 3 0 0 0-3 3v12a3 3 0 0 0 3 3h12a3 3 0 0 0 3-3V7a3 3 0 0 0-3-3zM6 6h1v1a1 1 0 0 0 2 0V6h6v1a1 1 0 0 0 2 0V6h1a1 1 0 0 1 1 1v4H5V7a1 1 0 0 1 1-1zm12 14H6a1 1 0 0 1-1-1v-6h14v6a1 1 0 0 1-1 1z"
              fill="currentColor"
            />
            <circle
              id="Elipse_5"
              cx="1"
              cy="1"
              r="1"
              fill="currentColor"
              transform="translate(7 15)"
            />
            <path
              id="Caminho_43"
              d="M16 15h-4a1 1 0 0 0 0 2h4a1 1 0 0 0 0-2z"
              fill="currentColor"
            />
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default Date;
