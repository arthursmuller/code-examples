import { Icon } from '@chakra-ui/react';
import React from 'react';

const GpsIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Grupo_1830" transform="translate(3023 2228)">
          <g id="Layer_2" transform="translate(-3023 -2228)">
            <g id="pin">
              <path
                id="Caminho_179"
                d="M12 2a8 8 0 0 0-8 7.92c0 5.48 7.05 11.58 7.35 11.84a1 1 0 0 0 1.3 0C13 21.5 20 15.4 20 9.92A8 8 0 0 0 12 2zm0 17.65c-1.67-1.59-6-6-6-9.73a6 6 0 0 1 12 0c0 3.7-4.33 8.14-6 9.73z"
                fill="currentColor"
              />
              <path
                id="Caminho_180"
                d="M12 6a3.5 3.5 0 1 0 3.5 3.5A3.5 3.5 0 0 0 12 6zm0 5a1.5 1.5 0 1 1 1.5-1.5A1.5 1.5 0 0 1 12 11z"
                fill="currentColor"
              />
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default GpsIcon;
