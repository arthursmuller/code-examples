import { Box, Flex } from '@chakra-ui/react';
import { ResponsivePie } from '@nivo/pie';
import { object } from 'prop-types';

const propTypes = {
  data: object,
};

function PieChart({ data }) {
  const handleUsedPercentage = () => {
    return (data.used * 100) / data.total;
  };

  const handleData = () => {
    let formattedData = [];

    const remianingAmount = 100 - handleUsedPercentage();

    //
    // adding the remaining percentage
    formattedData.push({
      id: 'remaining',
      label: 'Remining',
      value: remianingAmount,
      color: data.colorTotal,
    });

    //
    // adding the used percentage
    formattedData.push({
      id: 'used',
      label: 'Used',
      value: handleUsedPercentage(),
      color: data.colorUsed,
    });

    return formattedData;
  };

  if (data) {
    return (
      <>
        <Flex justifyContent="center" w="100%" flexGrow="1">
          <Box position="relative" w="180px" h="180px" alignItems="center">
            <ResponsivePie
              data={handleData()}
              colors={{ datum: 'color' }}
              innerRadius={0.85}
              enableRadialLabels={false}
              enableSlicesLabels={false}
              enableSliceLabels={false}
            />
            <Flex
              position="absolute"
              alignItems="center"
              justifyContent="center"
              color="#282832"
              background="transparent"
              w="100%"
              top="0"
              bottom="0"
              // this is important to preserve the chart interactivity
              pointerEvents="none"
            >
              <Box as="span" fontSize="34px" fontWeight="300" color="#282832">
                {Math.round(handleUsedPercentage())}%
              </Box>
            </Flex>
          </Box>
        </Flex>
        <Flex>
          <Flex alignItems="center" mr="20px" fontSize="14px">
            <Box
              width="10px"
              height="10px"
              backgroundColor={data.colorTotal}
              borderRadius="5px"
              mr="10px"
            />
            <Box>
              {data.total} {data.unit} (total)
            </Box>
          </Flex>
          <Flex alignItems="center" fontSize="14px">
            <Box
              width="10px"
              height="10px"
              backgroundColor={data.colorUsed}
              borderRadius="5px"
              mr="10px"
            />
            <Box>
              {data.used} {data.unit} (used)
            </Box>
          </Flex>
        </Flex>
      </>
    );
  } else {
    return 'no data';
  }
}

PieChart.propTypes = propTypes;
export default PieChart;
