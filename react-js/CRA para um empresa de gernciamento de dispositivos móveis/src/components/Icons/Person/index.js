import { Icon } from '@chakra-ui/react';
import React from 'react';

const PersonIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <path
          id="Caminho_8"
          d="M11.788 10.576A3.788 3.788 0 1 0 8 6.788a3.788 3.788 0 0 0 3.788 3.788zm0-5.682a1.894 1.894 0 1 1-1.894 1.894 1.894 1.894 0 0 1 1.894-1.894z"
          fill="#0190fe"
        />
        <path
          id="Caminho_9"
          d="M12.129 13A6.893 6.893 0 0 0 5 19.629a.985.985 0 0 0 1.018.947.985.985 0 0 0 1.018-.947 4.924 4.924 0 0 1 5.092-4.735 4.924 4.924 0 0 1 5.092 4.735 1.021 1.021 0 0 0 2.037 0A6.893 6.893 0 0 0 12.129 13z"
          fill="#0190fe"
        />
      </svg>
    </Icon>
  );
};

export default PersonIcon;
