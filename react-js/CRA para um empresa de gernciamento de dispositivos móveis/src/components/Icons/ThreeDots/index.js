import { createIcon } from '@chakra-ui/icon';
import React from 'react';

export const ThreeDotsIcon = createIcon({
  displayName: 'ViewIcon',
  path: (
    <g id="more-horizotnal">
      <circle
        id="Elipse_2"
        cx="2"
        cy="2"
        r="2"
        fill="currentColor"
        transform="translate(10 10)"
      />
      <circle
        id="Elipse_3"
        cx="2"
        cy="2"
        r="2"
        fill="currentColor"
        transform="translate(17 10)"
      />
      <circle
        id="Elipse_4"
        cx="2"
        cy="2"
        r="2"
        fill="currentColor"
        transform="translate(3 10)"
      />
    </g>
  ),
});

export default ThreeDotsIcon;
