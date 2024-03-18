import { Box, Divider } from '@chakra-ui/react';
import React from 'react';

import Text from '../Text';

interface FormSubtitleProps {
  subtitle: React.ReactNode;
  description?: React.ReactNode;
  children?: React.ReactNode;
}

const FormSubtitle = ({
  subtitle,
  description,
  children,
}: FormSubtitleProps) => {
  return (
    <Box>
      {subtitle && (
        <Box>
          <Text m="0% 0% 1% 0%" fontSize="24px" fontWeight="600">
            {subtitle}
          </Text>
        </Box>
      )}
      {description && (
        <Text as="i" m="0" fontSize="xs" color="gray.300">
          {description}
        </Text>
      )}

      <Box mt="1%">
        <Divider orientation="horizontal" mb="1.5%" borderColor="gray.600" />
      </Box>
      {children}
    </Box>
  );
};

export default FormSubtitle;
