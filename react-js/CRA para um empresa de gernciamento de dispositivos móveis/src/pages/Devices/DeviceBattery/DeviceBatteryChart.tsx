import { useIntl } from 'react-intl';

import ChartLine from '../../../components/ChartLine';
import { DeviceInfoBatteryType } from '../../../types/deviceInfo';

interface DeviceBatteryChartProps {
  batteryData?: DeviceInfoBatteryType[];
}

function DeviceBatteryChart({ batteryData }: DeviceBatteryChartProps) {
  const intl = useIntl();

  const chartData = [
    {
      id: 'battery',
      data: batteryData.map((item) => ({
        ...item,
        x: intl.formatTime(item.date),
        y: item.percent,
      })),
    },
  ];

  return (
    <ChartLine
      chartData={chartData}
      yScale={{ type: 'linear', stacked: false, min: 0, max: 100 }}
      yFormat={(value) => `${value}%`}
      // xFormat={(value) => `${intl.formatTime(value)}%`}
    />
  );
}

export default DeviceBatteryChart;
