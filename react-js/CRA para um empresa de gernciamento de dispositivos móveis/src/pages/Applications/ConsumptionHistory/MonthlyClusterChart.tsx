import { linearGradientDef } from '@nivo/core';

import ChartLine from '../../../components/ChartLine';
import { formatBytesTo } from '../../../helper/bytes';
import { ApplicationConsumptionHistoryType } from '../../../types/application';

interface MonthlyClusterChart {
  data: ApplicationConsumptionHistoryType[];
}

const MonthlyClusterChart = ({ data }: MonthlyClusterChart) => {
  const chartData = [
    {
      id: 'consumption',
      data: data.map(({ day, consumption }) => ({
        x: day,
        y: consumption,
      })),
    },
  ];

  return (
    <ChartLine
      chartData={chartData}
      yScale={{ type: 'linear', stacked: false }}
      axisLeft={{
        tickSize: 0,
        tickPadding: 15,
        tickRotation: 0,
        legendPosition: 'middle',
        format: (v: number) => formatBytesTo({ bytes: v, to: 'MB' }),
      }}
      axisBottom={{
        tickSize: 0,
        tickPadding: 15,
        tickRotation: 0,
        legendPosition: 'middle',
        legendOffset: 0,
      }}
      colors={'#4a83e4'}
      defs={[
        linearGradientDef(
          'white_blue',
          [
            { offset: 0, color: '#fff', opacity: 1 },
            { offset: 30, color: '#4a83e4', opacity: 1 },
            { offset: 100, color: '#005cfa', opacity: 1 },
          ],
          {
            gradientTransform: 'rotate(180 0.5 0.5)',
          }
        ),
      ]}
      yFormat={(v: number) => formatBytesTo({ bytes: v, to: 'MB' })}
    />
  );
};

export default MonthlyClusterChart;
