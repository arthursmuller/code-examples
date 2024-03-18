import { Icon } from '@chakra-ui/react';
import React from 'react';

const UserIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Grupo_1824" transform="translate(3023 2555)">
          <g id="Layer_2" transform="translate(-3023 -2555)">
            <g id="person">
              <path
                id="Caminho_168"
                d="M12 11a4 4 0 1 0-4-4 4 4 0 0 0 4 4zm0-6a2 2 0 1 1-2 2 2 2 0 0 1 2-2z"
                fill="currentColor"
              />
              <path
                id="Caminho_169"
                d="M12 13a7 7 0 0 0-7 7 1 1 0 0 0 2 0 5 5 0 0 1 10 0 1 1 0 0 0 2 0 7 7 0 0 0-7-7z"
                fill="currentColor"
              />
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default UserIcon;
