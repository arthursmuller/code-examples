import { Box } from '@chakra-ui/react';
import React from 'react';

import Heading from '../Heading';
import Text from '../Text';


interface PageTitleProps {
  title?: React.ReactNode;
  description?: React.ReactNode;
}

const PageTitle: React.FC<PageTitleProps> = ({
  title,
  description,
}: PageTitleProps) => {
  return (
    <Box>
      <Heading>{title}</Heading>
      <Text width="90%">{description}</Text>
    </Box>
  );
};

export default PageTitle;
