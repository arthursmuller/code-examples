import { Box, SimpleGrid, Checkbox } from '@chakra-ui/react';
import React, { useState } from 'react';

import Text from '../Text';

const HourSelectGrid = ({ selectedColor, onChange, hoursSelected }) => {
  const [hours, setHours] = useState(
    hoursSelected ? hoursSelected : Array(168).fill(false)
  );
  const hourClick = (index) => {
    const copy = [...hours];
    copy.splice(index, 1, !hours[index]);
    setHours(copy);
    onChange(copy);
  };

  const selectRow =
    (row) =>
    ({ target }) => {
      const rowStart = row * 24 - 24;
      const copy = [...hours];
      copy.splice(rowStart, 24, Array(24).fill(target.checked));
      setHours(copy.flat());
      onChange(copy);
    };
  return (
    <Box mt="1%" d="flex" flexDirection="row" p="5px">
      <Box display="flex" flexDirection="column" mt="35px">
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(1)}
          >
            Dom
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(2)}
          >
            Seg
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(3)}
          >
            Ter
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(4)}
          >
            Qua
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(5)}
          >
            Qui
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(6)}
          >
            Sex
          </Checkbox>
        </Box>
        <Box height={{ base: '35px', xxl: '40px' }} pr="5px">
          <Checkbox
            size="sm"
            colorScheme={selectedColor}
            onClick={selectRow(7)}
          >
            Sab
          </Checkbox>
        </Box>
      </Box>
      <Box overflowX="auto">
        <SimpleGrid
          columns={24}
          spacingX="1px"
          spacingY="1px"
          width="fit-content"
        >
          {Array(24)
            .fill(null)
            .map((v, index) => (
              <Text
                key={index}
                width={{ base: '35px', xxl: '40px' }}
                align="center"
                color="black"
                fontSize="sm"
                lineHeight="2.5"
              >
                {String(index).padStart(2, '0')}
              </Text>
            ))}
        </SimpleGrid>
        <SimpleGrid
          columns={24}
          spacingX="1px"
          spacingY="1px"
          width="fit-content"
        >
          {hours.map((hour, i) => (
            <Box
              key={i}
              bg={hour ? `${selectedColor}.500` : 'gray.400'}
              height={{ base: '35px', xxl: '40px' }}
              width={{ base: '35px', xxl: '40px' }}
              border="solid 2px #ffffff"
              onClick={() => hourClick(i)}
            ></Box>
          ))}
        </SimpleGrid>
      </Box>
    </Box>
  );
};

export default HourSelectGrid;
