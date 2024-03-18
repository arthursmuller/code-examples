import { Box, Divider, Text } from '@chakra-ui/react';
import { FormattedMessage } from 'react-intl';

import currentPositionMap from '../../../assets/Images/current-position-map.svg';
import initialPositionMap from '../../../assets/Images/initial-position-map.svg';
import lastPositionMap from '../../../assets/Images/last-position-map.svg';
import MapInitialPositionIcon from '../../Icons/MapInitialPosition';
import MapLastPositionIcon from '../../Icons/MapLastPosition';
import MapPresentPositionIcon from '../../Icons/MapPresentPosition';
import Map from '../../Map';
import WindowCloud from './WindowCloud';

export const GeolocationMap = (data) => {
  const mapIcons = {
    currentPosition: currentPositionMap,
    initialPosition: initialPositionMap,
    lastPosition: lastPositionMap,
  };

  const markers = data.data.map((dataInfo) => {
    return {
      name: dataInfo.name,
      lat: dataInfo.lat,
      lng: dataInfo.lng,
      icon: mapIcons[dataInfo.name || 'default'],
      infos: {
        date: dataInfo.infos.date,
        state: dataInfo.infos.state,
        precision: dataInfo.infos.precision,
        battery: dataInfo.infos.battery,
      },
    };
  });

  return (
    <>
      <Text m="2% 0% 1% 0%" fontSize="md" fontWeight="600">
        <FormattedMessage id="geolocation.label" />
      </Text>
      <Box>
        <Divider borderColor="gray.600" orientation="horizontal" />
      </Box>
      <Box
        m="1% 0%"
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
      >
        <Text m="0" fontSize="sm" d="flex">
          <MapInitialPositionIcon boxSize={6} mr="5px" />
          <FormattedMessage id="geolocation.initial_position" />
        </Text>
        <Text m="0" fontSize="sm" d="flex">
          <MapPresentPositionIcon boxSize={6} mr="5px" />
          <FormattedMessage id="geolocation.current_position" />
        </Text>
        <Text m="0" fontSize="sm" d="flex">
          <MapLastPositionIcon boxSize={6} mr="5px" />
          <FormattedMessage id="geolocation.position_history" />
        </Text>
      </Box>
      <Box mt="1%">
        <Map markers={markers} WindowCloud={WindowCloud} />
      </Box>
    </>
  );
};
export default GeolocationMap;
