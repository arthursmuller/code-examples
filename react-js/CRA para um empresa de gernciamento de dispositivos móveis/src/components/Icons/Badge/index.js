import { Icon } from '@chakra-ui/react';
import React from 'react';

const Badge = (props) => {
  return (
    <Icon viewBox="0" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="48"
        height="48"
        viewBox="0 0 48 48"
      >
        <path
          fill="#d7d7dc"
          d="M38 41.5l-4.62-18A11.88 11.88 0 0036 16a12 12 0 10-24 0 11.88 11.88 0 002.68 7.54L10 41.5a2 2 0 002.96 2.22l10.66-6.26 11.36 6.28A1.82 1.82 0 0036 44a2 2 0 002-2.5zM24 8a8 8 0 11-8 8 8 8 0 018-8zm.62 25.42a2 2 0 00-2 0l-7.5 4.4L18 26.42a11.88 11.88 0 0011.84 0L32.9 38z"
        ></path>
      </svg>
    </Icon>
  );
};

export default Badge;
