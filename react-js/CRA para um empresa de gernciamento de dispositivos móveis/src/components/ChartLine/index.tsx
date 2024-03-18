import { Box } from '@chakra-ui/react';
import { linearGradientDef } from '@nivo/core';
import { LineSvgProps, ResponsiveLine, Serie } from '@nivo/line';

interface ChartLineProps extends Omit<LineSvgProps, 'data'> {
  chartData: Serie[];
}

const ChartLine = ({ chartData, ...rest }: ChartLineProps) => {
  const baseProperties: Partial<LineSvgProps> = {
    yScale: {
      type: 'linear',
      stacked: false,
    },
    margin: { top: 20, right: 20, bottom: 60, left: 80 },
  };

  const styleProperties: Partial<LineSvgProps> = {
    curve: 'monotoneX',
    colors: { scheme: 'dark2' },
    lineWidth: 2,
    enableArea: true,
    areaOpacity: 0.5,
    defs: [
      linearGradientDef(
        'red_green',
        [
          { offset: 0, color: '#900', opacity: 1 },
          { offset: 20, color: '#060', opacity: 1 },
          { offset: 100, color: '#6F6', opacity: 1 },
        ],
        {
          gradientTransform: 'rotate(180 0.5 0.5)',
        }
      ),
    ],
    fill: [{ match: '*', id: 'red_green' }],
  };

  const pointProperties: Partial<LineSvgProps> = {
    pointSize: 10,
    pointBorderWidth: 2,
    enablePointLabel: true,
    pointLabel: 'yFormatted',
  };
  return (
    <Box h="400px">
      <ResponsiveLine
        {...baseProperties}
        {...styleProperties}
        {...pointProperties}
        enableGridX={false}
        isInteractive
        useMesh
        animate
        {...rest}
        data={chartData}
      />
    </Box>
  );
};

export default ChartLine;
