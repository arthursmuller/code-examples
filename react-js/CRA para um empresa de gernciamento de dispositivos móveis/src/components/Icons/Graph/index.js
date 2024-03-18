import { Icon } from '@chakra-ui/react';
import React from 'react';

const GraphIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        id="ic_historial"
        width="23.549"
        height="24"
        viewBox="0 0 23.549 24"
      >
        <path
          id="Retângulo_45"
          d="M0 0H23.549V23.541H0z"
          fill="currentColor"
          transform="translate(0 .459)"
        />
        <g id="bar-chart">
          <path
            id="Retângulo_609"
            d="M0 0H23.541V23.549H0z"
            fill="currentColor"
            transform="rotate(90 11.774 11.775)"
          />
          <path
            id="Caminho_207"
            d="M12 4a.987.987 0 0 0-1 .973v14.595a1 1 0 0 0 2 0V4.973A.987.987 0 0 0 12 4z"
            className="cls-2"
            transform="translate(-.225)"
          />
          <path
            id="Caminho_208"
            d="M19 12a1 1 0 0 0-1 1v7a1 1 0 0 0 2 0v-7a1 1 0 0 0-1-1z"
            className="cls-2"
            transform="translate(-.369 -.459)"
          />
          <path
            id="Caminho_209"
            d="M5 8a1 1 0 0 0-1 1v11a1 1 0 0 0 2 0V9a1 1 0 0 0-1-1z"
            className="cls-2"
            transform="translate(-.082 -.459)"
          />
        </g>
      </svg>
    </Icon>
  );
};

export default GraphIcon;
