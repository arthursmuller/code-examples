import { Flex, Box } from '@chakra-ui/react';
import { ResponsiveBar } from '@nivo/bar';
import { BoxLegendSvg } from '@nivo/legends';
import { any } from 'prop-types';
import React from 'react';

const propTypes = {
  data: any,
};

function MonthlyClusterBarChart({ data }) {
  // eslint-disable-next-line react/prop-types
  const BarLegend = ({ height, legends, width }) => {
    return (
      <React.Fragment>
        {legends.map((legend) => (
          <BoxLegendSvg
            key={JSON.stringify(legend.data.map(({ id }) => id))}
            {...legend}
            containerHeight={height}
            containerWidth={width}
            width="10"
            height="10"
            anchor="bottom-left"
            translateX="-50"
            // eslint-disable-next-line react/jsx-no-duplicate-props
            symbolShape={({ x, y, size, fill, borderWidth, borderColor }) => (
              <circle
                r="5"
                cx="10"
                cy="12.5"
                fill={fill}
                strokeWidth="0"
                stroke="transparent"
                style={{ pointerEvents: 'none' }}
                width={10}
                height={10}
              />
            )}
          />
        ))}
      </React.Fragment>
    );
  };

  if (data) {
    return (
      <Box h="240px" w="100%">
        <ResponsiveBar
          data={data.data}
          theme={{
            axis: {
              ticks: {
                text: {
                  fontSize: 12,
                  fill: '#43425d',
                },
              },
            },
          }}
          keys={data.keys}
          indexBy="period"
          margin={{ top: 10, right: 0, bottom: 60, left: 45 }}
          padding={0.4}
          groupMode="grouped"
          valueScale={{ type: 'linear' }}
          indexScale={{ type: 'band', round: true }}
          colors={({ id, data }) => {
            return data[`${id}Color`];
          }}
          isInteractive={true}
          tooltip={({ id, data }) => {
            const tooltipLayout = (
              <>
                <Box>{data.period}</Box>
                <Flex fontSize="12px" color="#a0a0a5" alignItems="center">
                  <Flex alignItems="center">
                    <Box
                      w="10px"
                      h="10px"
                      borderRadius={5}
                      backgroundColor={data[`${id}Color`]}
                    />
                    <Box ml="5px">{data[`${id}Label`]}</Box>
                  </Flex>
                  <Box ml="10px" color="#282832" fontSize="14px">
                    {data[id]} {data[`${id}Unit`]}
                  </Box>
                </Flex>
              </>
            );
            return tooltipLayout;
          }}
          gridYValues={[0, 25, 50, 75, 100]}
          enableLabel={false}
          layers={['grid', 'axes', 'bars', 'markers', BarLegend]}
          maxValue={100}
          borderColor={{ from: 'color', modifiers: [['darker', 1.6]] }}
          axisLeft={{
            tickSize: 0,
            tickPadding: 15,
            tickRotation: 0,
            legendPosition: 'middle',
            format: (v) => `${v}%`,
            tickValues: [0, 25, 50, 75, 100],
          }}
          axisBottom={{
            tickSize: 0,
            tickPadding: 15,
            tickRotation: 0,
            legendPosition: 'middle',
            legendOffset: 0,
          }}
          innerPadding={5}
          legends={[
            {
              dataFrom: 'keys',
              data: data.keys.map((id, index) => ({
                color: data.legendColors[index],
                id,
                label: id === 'smsUsed' ? 'SMS' : 'Dados',
              })),
              anchor: 'bottom-left',
              direction: 'row',
              justify: false,
              translateY: 60,
              itemsSpacing: 70,
              itemWidth: 30,
              itemHeight: 25,
              itemDirection: 'left-to-right',
              color: '#a0a0a5',
            },
          ]}
        />
      </Box>
    );
  } else {
    return <>no data</>;
  }
}

MonthlyClusterBarChart.propTypes = propTypes;
export default MonthlyClusterBarChart;
