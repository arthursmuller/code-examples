import { Icon } from '@chakra-ui/react';
import React from 'react';

const MapInitialPositionIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        id="pin"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <circle
          id="Elipse_26"
          cx="1.5"
          cy="1.5"
          r="1.5"
          fill="#00d23f"
          transform="translate(10.5 8)"
        />
        <path
          id="Caminho_211"
          d="M12 2a8 8 0 0 0-8 7.92c0 5.48 7.05 11.58 7.35 11.84a1 1 0 0 0 1.3 0C13 21.5 20 15.4 20 9.92A8 8 0 0 0 12 2zm0 11a3.5 3.5 0 1 1 3.5-3.5A3.5 3.5 0 0 1 12 13z"
          fill="#00d23f"
        />
      </svg>
    </Icon>
  );
};

export default MapInitialPositionIcon;
