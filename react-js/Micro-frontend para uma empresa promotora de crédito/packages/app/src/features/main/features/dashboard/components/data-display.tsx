import { FC } from 'react';

import { Box, Text } from '@chakra-ui/react';

interface DataDisplay {
  label: string;
  value?: number | string;
  component?: React.ReactElement;
}

export const DataDisplay: FC<DataDisplay> = ({ label, value, component }) => {
  return (
    <Box>
      <Text textStyle="bold14" color="secondary.regular">
        {label}
      </Text>
      {!!value && (
        <Text textStyle="regular16" color="grey.700">
          {value}
        </Text>
      )}
      {component && component}
    </Box>
  );
};
