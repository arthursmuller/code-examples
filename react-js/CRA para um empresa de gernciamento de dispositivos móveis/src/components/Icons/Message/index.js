import { Icon } from '@chakra-ui/react';
import React from 'react';

const MessageIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <g id="Grupo_1828" transform="translate(3023 2337)">
          <g id="Layer_2" transform="translate(-3023 -2337)">
            <g id="message-circle">
              <circle
                id="Elipse_15"
                cx="1"
                cy="1"
                r="1"
                fill="currentColor"
                transform="translate(11 11)"
              />
              <circle
                id="Elipse_16"
                cx="1"
                cy="1"
                r="1"
                fill="currentColor"
                transform="translate(15 11)"
              />
              <circle
                id="Elipse_17"
                cx="1"
                cy="1"
                r="1"
                fill="currentColor"
                transform="translate(7 11)"
              />
              <path
                id="Caminho_178"
                d="M19.07 4.93a10 10 0 0 0-16.28 11 1.06 1.06 0 0 1 .09.64L2 20.8A.994.994 0 0 0 3 22h.2l4.28-.86a1.26 1.26 0 0 1 .64.09 10 10 0 0 0 11-16.28zm.83 8.36a8 8 0 0 1-11 6.08 3.26 3.26 0 0 0-1.25-.26 3.43 3.43 0 0 0-.56.05l-2.82.57.57-2.82a3.09 3.09 0 0 0-.21-1.81 8 8 0 1 1 15.27-1.81z"
                fill="currentColor"
              />
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default MessageIcon;
