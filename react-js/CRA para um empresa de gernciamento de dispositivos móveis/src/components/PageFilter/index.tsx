import { Box } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';

import Text from '../Text';

interface PageFilterProps {
  children: React.ReactNode;
}

const PageFilter: React.FC<PageFilterProps> = ({ children }: PageFilterProps) => {
  return (
    <Box>
      <Text
        margin="0 0 20px 0"
        color="gray.600"
        fontSize="16px"
        fontWeight="bold"
      >
        <FormattedMessage id="global.filter" />
      </Text>
      <Box d="flex" flexDirection="row">
        {children}
      </Box>
    </Box>
  );
};

export default PageFilter;
