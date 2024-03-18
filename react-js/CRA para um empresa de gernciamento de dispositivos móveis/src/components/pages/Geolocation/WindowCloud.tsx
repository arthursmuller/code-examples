import { Text } from '@chakra-ui/react';
import { FormattedMessage } from 'react-intl';

import { MarkerMapProps } from '../../Map';

interface WindowCloudProps {
  selected: MarkerMapProps;
}

const WindowCloud = ({ selected }: WindowCloudProps) => {
  return (
    <>
      <Text color="gray.500">{selected.infos.date}</Text>
      <Text color="gray.500">
        <FormattedMessage id="map.state" />: {selected.infos.state}
      </Text>
      <Text color="gray.500">
        <FormattedMessage id="map.precision" />: {selected.infos.precision}
      </Text>
      <Text color="gray.500">
        <FormattedMessage id="map.battery" />: {selected.infos.battery}%
      </Text>
    </>
  );
};

export default WindowCloud;
