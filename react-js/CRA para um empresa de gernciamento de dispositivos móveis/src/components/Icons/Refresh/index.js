import { Icon } from '@chakra-ui/react';
import React from 'react';

const RefreshIcon = (props) => {
  return (
    <Icon viewBox="0 0 24 24" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
      >
        <path
          fill="currentColor"
          d="M3.741 13.43a1 1 0 0 1 1.25.65 7.14 7.14 0 0 0 6.87 4.92 7.1 7.1 0 0 0 7.18-7 7.1 7.1 0 0 0-7.18-7 7.26 7.26 0 0 0-4.65 1.67l2.17-.36A1 1 0 1 1 9.7 8.29l-4.24.7h-.17a1 1 0 0 1-.34-.06.33.33 0 0 1-.1-.06.78.78 0 0 1-.2-.11l-.09-.11c0-.05-.09-.09-.13-.15s0-.1-.05-.14a1.34 1.34 0 0 1-.07-.18l-.75-4a1.018 1.018 0 0 1 2-.38l.27 1.45A9.21 9.21 0 0 1 11.861 3a9.1 9.1 0 0 1 9.18 9 9.1 9.1 0 0 1-9.18 9 9.12 9.12 0 0 1-8.82-6.32 1 1 0 0 1 .7-1.25z"
          transform="translate(-.041)"
        />
      </svg>
    </Icon>
  );
};

export default RefreshIcon;
