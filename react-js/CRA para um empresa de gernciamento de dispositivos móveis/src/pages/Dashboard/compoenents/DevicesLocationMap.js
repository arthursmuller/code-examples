import { Box } from '@chakra-ui/react';
import { GoogleMap, useJsApiLoader, Marker } from '@react-google-maps/api';
import { any } from 'prop-types';
import React from 'react';

const containerStyle = {
  width: '100%',
  height: '485px',
};

const center = { lat: 39.5, lng: -98.35 };

const propTypes = {
  data: any,
};
function DevicesLocationMap({ data }) {
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: 'AIzaSyBTkTC5K1wAHF6ZXzN_sKweYskVLQItMCw',
  });

  return (
    <Box m="40px 0" w="100%">
      <Box as="h2" fontSize="24px" lineHeight="1.38" color="#282832" mb="20px">
        Ubicación de dispositivos
      </Box>
      <Box>
        {isLoaded && (
          <GoogleMap
            mapContainerStyle={containerStyle}
            center={center}
            zoom={3}
          >
            {data &&
              data.length > 0 &&
              data.map((position, index) => {
                return <Marker key={index} position={position} />;
              })}
          </GoogleMap>
        )}
      </Box>
    </Box>
  );
}

DevicesLocationMap.propTypes = propTypes;
export default DevicesLocationMap;
