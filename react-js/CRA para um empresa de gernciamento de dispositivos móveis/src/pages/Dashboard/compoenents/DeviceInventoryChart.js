import { ResponsivePie } from '@nivo/pie';
import { any } from 'prop-types';

const propTypes = {
  data: any,
};

function DeviceInventoryChart({ data }) {
  const handlePercentage = (qty) => {
    return Math.round((qty * 100) / data.total);
  };

  const handleData = () => {
    const formattedData = data.data.map((item) => {
      return {
        ...item,
        value: handlePercentage(item.qty),
      };
    });

    return formattedData;
  };

  return (
    <ResponsivePie
      data={handleData()}
      colors={{ datum: 'color' }}
      innerRadius={0.7}
      margin={{ top: 0, right: 80, bottom: 80, left: 80 }}
      enableRadialLabels={false}
      enableSlicesLabels={true}
      enableSliceLabels={true}
      isInteractive={false}
      sliceLabel={(item) => {
        return `${item.value}%`;
      }}
      legends={[
        {
          anchor: 'bottom',
          direction: 'row',
          justify: false,
          translateX: 0,
          translateY: 56,
          itemsSpacing: 0,
          itemWidth: 100,
          itemHeight: 18,
          itemTextColor: '#999',
          itemDirection: 'left-to-right',
          itemOpacity: 1,
          symbolSize: 10,
          symbolShape: 'circle',
        },
      ]}
    />
  );
}

DeviceInventoryChart.propTypes = propTypes;
export default DeviceInventoryChart;
