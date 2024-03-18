import { useIntl } from 'react-intl';

import ChartLine from '../../../components/ChartLine';
import { convertBytesTo } from '../../../helper/bytes';
import { DeviceInfoStorageType } from '../../../types/deviceInfo';

interface DeviceStorageChartProps {
  storageData?: DeviceInfoStorageType[];
}

function DeviceStorageChart({ storageData }: DeviceStorageChartProps) {
  const intl = useIntl();

  const chartData = [
    {
      id: 'storage',
      data: storageData.map((item) => ({
        ...item,
        x: intl.formatTime(item.date),
        y: convertBytesTo({ bytes: item.bytes, to: 'GB' }),
      })),
    },
  ];

  return <ChartLine chartData={chartData} />;
}

export default DeviceStorageChart;
