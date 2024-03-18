import { Icon } from '@chakra-ui/react';
import React from 'react';

const HelpIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Grupo_1820" transform="translate(3023 2003)">
          <g id="Layer_2" transform="translate(-3023 -2003)">
            <g id="menu-arrow-circle">
              <path
                id="Caminho_186"
                d="M12 2a10 10 0 1 0 10 10A10 10 0 0 0 12 2zm0 18a8 8 0 1 1 8-8 8 8 0 0 1-8 8z"
                fill="currentColor"
              />
              <path
                id="Caminho_187"
                d="M12 6a3.5 3.5 0 0 0-3.5 3.5 1 1 0 1 0 2 0A1.5 1.5 0 1 1 12 11a1 1 0 0 0-1 1v2a1 1 0 0 0 2 0v-1.16A3.49 3.49 0 0 0 12 6z"
                fill="currentColor"
              />
              <circle
                id="Elipse_18"
                cx="1"
                cy="1"
                r="1"
                fill="currentColor"
                transform="translate(11 16)"
              />
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default HelpIcon;
