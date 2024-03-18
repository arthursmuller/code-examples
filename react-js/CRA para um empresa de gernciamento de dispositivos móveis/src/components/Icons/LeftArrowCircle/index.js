import { Icon } from '@chakra-ui/react';
import React from 'react';

const LeftArrowCircleIcon = (props) => {
  return (
    <Icon viewBox="0 0 32 32" {...props}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="32"
        height="32"
        viewBox="0 0 32 32"
      >
        <g>
          <g
            fill="#fff"
            stroke="#0190fe"
            strokeWidth="2px"
            transform="translate(-263 -102) translate(263 102)"
          >
            <circle cx="16" cy="16" r="16" stroke="none" />
            <circle cx="16" cy="16" r="15" fill="none" />
          </g>
          <g>
            <g>
              <g fill="#0190fe">
                <path
                  d="M.152 5.089a.635.635 0 0 0 .448.274.6.6 0 0 0 .481-.174l2.866-2.783V11.5a.719.719 0 0 0 .658.767.719.719 0 0 0 .658-.767V2.406l2.866 2.783a.6.6 0 0 0 .927-.1.858.858 0 0 0-.086-1.081L5.025.174l-.1-.069-.084-.054a.571.571 0 0 0-.473 0l-.086.054-.1.069L.238 4.008a.812.812 0 0 0-.238.52.855.855 0 0 0 .15.561z"
                  transform="translate(-263 -102) rotate(180 144.513 64.014) rotate(90 9.873 9.873) translate(5.124 4.482)"
                />
              </g>
            </g>
          </g>
        </g>
      </svg>
    </Icon>
  );
};

export default LeftArrowCircleIcon;
