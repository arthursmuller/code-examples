import {
  GoogleMap,
  InfoWindow,
  Marker,
  MarkerClusterer,
  useJsApiLoader,
} from '@react-google-maps/api';
import { useState } from 'react';
export interface MarkerMapProps {
  name: string;
  lat: number;
  lng: number;
  icon: string;
  infos: {
    date: string;
    state: string;
    precision: number;
    battery: number;
  };
}

interface MapProps {
  markers: MarkerMapProps[];
  WindowCloud?: (props: { selected: MarkerMapProps }) => JSX.Element;
}

export const Map = ({ markers, WindowCloud }: MapProps) => {
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: process.env.GOOGLE_MAPS_API_KEY,
  });

  const highValueLat = markers.reduce(function (prev, current) {
    return prev.lat < current.lat ? prev : current;
  });
  const lowValueLat = markers.reduce(function (prev, current) {
    return prev.lat > current.lat ? prev : current;
  });
  const highValueLng = markers.reduce(function (prev, current) {
    return prev.lng < current.lng ? prev : current;
  });
  const lowValueLng = markers.reduce(function (prev, current) {
    return prev.lng > current.lng ? prev : current;
  });

  const [selected, setSelected] = useState<MarkerMapProps>(null);
  const [center, setCenter] = useState({
    lat: (lowValueLat.lat + highValueLat.lat) / 2,
    lng: (lowValueLng.lng + highValueLng.lng) / 2,
  });
  const [zoom, setZoom] = useState(5);
  const [mapref, setMapref] = useState(null);

  const handleOnLoad = (map) => {
    setMapref(map);
  };

  const handleCenterChanged = () => {
    if (mapref) {
      const newCenter = mapref.getCenter();
      setCenter({ lat: newCenter.lat, lng: newCenter.lng });
    }
  };

  const handleZoomChanged = () => {
    if (mapref) {
      const newZoom = mapref.getZoom();
      setZoom(newZoom);
    }
  };

  return (
    <>
      {isLoaded && (
        <GoogleMap
          mapContainerStyle={{
            width: '100%',
            height: '485px',
          }}
          center={{
            lat: center.lat,
            lng: center.lng,
          }}
          onLoad={handleOnLoad}
          onCenterChanged={handleCenterChanged}
          onZoomChanged={handleZoomChanged}
          zoom={zoom}
        >
          <MarkerClusterer averageCenter={true}>
            {(clusterer) =>
              markers.map((m) => (
                <Marker
                  key={m.name}
                  position={{ lat: m.lat, lng: m.lng }}
                  icon={{
                    url: m.icon,
                  }}
                  onClick={() => {
                    setSelected(m);
                  }}
                  animation={4}
                  clusterer={clusterer}
                />
              ))
            }
          </MarkerClusterer>
          {selected ? (
            <InfoWindow
              position={{ lat: selected.lat, lng: selected.lng }}
              onCloseClick={() => {
                setSelected(null);
              }}
            >
              <WindowCloud selected={selected} />
            </InfoWindow>
          ) : null}
        </GoogleMap>
      )}
    </>
  );
};
export default Map;
