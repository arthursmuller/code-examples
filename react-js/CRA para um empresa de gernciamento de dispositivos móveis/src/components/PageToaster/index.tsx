import { Box } from '@chakra-ui/react';
import React from 'react';

import Toaster from '../Toaster';

interface PageToasterProps {
  showToaster: boolean;
  onClose: () => void;
  message: string | React.ReactNode;
  type?: 'success' | 'error';
}

const PageToaster: React.FC<PageToasterProps> = ({ showToaster, onClose, message, type }: PageToasterProps) => {
  return (
    <Box mt="3%">
      <Toaster
        open={showToaster}
        onClose={onClose}
        type={type}
      >
        {message}
      </Toaster>
    </Box>
  );
}

export default PageToaster;