import { ExternalLinkIcon } from '@chakra-ui/icons';
import { Link, Tooltip } from '@chakra-ui/react';
import React from 'react';

const TimelineSite = ({ label }) => (
  <Tooltip label={label}>
    <Link
      href={label}
      isExternal
      _hover={{
        filter: 'invert(1)',
      }}
      role="group"
      whiteSpace="nowrap"
      width="400px"
      d="block"
      overflow="hidden"
      textOverflow="ellipsis"
    >
      {label}
      <ExternalLinkIcon
        display="none"
        _groupHover={{ display: 'inline' }}
        mx="2px"
      />
    </Link>
  </Tooltip>
);

export default TimelineSite;
