import { Box, Divider } from '@chakra-ui/react';
import React from 'react';

import HourSelectGrid from '../../../../components/HourSelectGrid';
import Text from '../../../../components/Text';

const ScheduleTab = ({ action }) => {
  const color = action === 'block' ? 'red' : 'green';

  const print = (value) => {
    // console.log(value);
  };

  return (
    <>
      <Box>
        <Text m="2% 0% 0% 0%" fontWeight="600">
          Horários
        </Text>
        <Divider
          orientation="horizontal"
          borderColor="gray.600"
          m="0.5% 0% 1.5% 0%"
        />
      </Box>
      <Box>
        <Text as="i" color="gray.300" fontSize="xs" m="0">
          Selecione os horários que deseja efetuar a ação.
        </Text>
      </Box>
      <Box>
        <HourSelectGrid selectedColor={color} onChange={print} />
      </Box>
    </>
  );
};

export default ScheduleTab;
