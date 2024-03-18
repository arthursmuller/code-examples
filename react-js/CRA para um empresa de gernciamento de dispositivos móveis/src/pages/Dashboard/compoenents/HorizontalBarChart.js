import { Box } from '@chakra-ui/react';
import { ResponsiveBar } from '@nivo/bar';
import { orderBy } from 'lodash';
import { any } from 'prop-types';

const propTypes = {
  data: any,
};

function HorizontalBarChart({ data }) {
  const handleUsedPercentage = (used) => {
    return Math.round((used * 100) / data.total);
  };

  const handleData = () => {
    const formattedData = data.data.map((item) => {
      return {
        ...item,
        usedPercentage: handleUsedPercentage(item.used),
      };
    });

    const orderedData = orderBy(formattedData, ['usedPercentage'], ['asc']);

    return orderedData;
  };

  if (data) {
    return (
      <>
        <Box w="100%" h="320px" position="relative" minWidth="0">
          <ResponsiveBar
            data={handleData()}
            theme={{
              axis: {
                ticks: {
                  text: {
                    fontSize: 12,
                    fill: '#6e6e78',
                  },
                },
              },
            }}
            keys={['usedPercentage']}
            indexBy="label"
            margin={{ top: 50, right: 100, bottom: 20, left: 145 }}
            padding={0.4}
            maxValue={100}
            layout="horizontal"
            valueScale={{ type: 'linear' }}
            indexScale={{ type: 'band', round: true }}
            colors={{ datum: 'data.color' }}
            gridXValues={[0, 25, 50, 75, 100]}
            axisTop={{
              tickSize: 0,
              tickPadding: 30,
              tickRotation: 0,
              legend: '',
              legendOffset: 36,
              format: (v) => `${v}%`,
              tickValues: [0, 25, 50, 75, 100],
              tickCount: 2,
            }}
            axisRight={{
              tickSize: 0,
              tickPadding: 20,
              tickRotation: 0,
              legend: '',
              legendPosition: 'middle',
              // eslint-disable-next-line react/display-name
              format: (v) => {
                const dataItem = data.data.find((item) => item.label === v);
                return (
                  <>
                    <tspan x="0" dy="-0.7em" font="normal 10px" fill="#31394d">
                      {dataItem.used} {dataItem.unit}
                    </tspan>
                    <tspan x="0" dy="1.2em" font="normal 10px" fill="#a0a0a5">
                      {handleUsedPercentage(dataItem.used)}%
                    </tspan>
                  </>
                );
              },
            }}
            axisBottom={null}
            axisLeft={{
              tickSize: 0,
              tickPadding: 20,
              tickRotation: 0,
              legend: '',
              legendPosition: 'middle',
            }}
            enableGridX={true}
            enableGridY={false}
            enableLabel={false}
            labelSkipWidth={9}
            labelSkipHeight={12}
            isInteractive={false}
            animate={true}
            motionStiffness={90}
            motionDamping={15}
          />
          <Box
            position="absolute"
            borderTop="1px solid #ebedf4"
            borderBottom="1px solid #ebedf4"
            top="49px"
            w="100%"
            h="251px"
            minWidth="0"
          ></Box>
        </Box>
        <Box
          fontSize="14px"
          lineHeight="1.36"
          letterSpacing="0.56px"
          color="#0190fe"
          textAlign="center"
          w="100%"
        >
          Ver lista completa
        </Box>
      </>
    );
  } else {
    return <>no data</>;
  }
}

HorizontalBarChart.propTypes = propTypes;
export default HorizontalBarChart;
