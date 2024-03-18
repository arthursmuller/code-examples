import { Icon } from '@chakra-ui/react';
import React from 'react';

const DeviceIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Grupo_1825" transform="translate(3023 2501)">
          <g id="Layer_2" transform="translate(-3023 -2501)">
            <g id="smartphone">
              <path
                id="Caminho_172"
                d="M17 2H7a3 3 0 0 0-3 3v14a3 3 0 0 0 3 3h10a3 3 0 0 0 3-3V5a3 3 0 0 0-3-3zm1 17a1 1 0 0 1-1 1H7a1 1 0 0 1-1-1V5a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1z"
                fill="currentColor"
              />
              <circle
                id="Elipse_14"
                cx="1.5"
                cy="1.5"
                r="1.5"
                fill="currentColor"
                transform="translate(10.5 15)"
              />
              <path
                id="Caminho_173"
                d="M14.5 6h-5a1 1 0 0 0 0 2h5a1 1 0 0 0 0-2z"
                fill="currentColor"
              />
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default DeviceIcon;
