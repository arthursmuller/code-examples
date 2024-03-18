import { useEffect, useRef, forwardRef, MutableRefObject } from 'react';

import { Box } from '@chakra-ui/react';
import { Loader } from '@googlemaps/js-api-loader';

const DEFAULT_LOCATION = { lat: -30.0299199, lng: -51.2316381 };

const mapInfoContentFor = (
  title?: string,
  subtitle?: string,
  info?: string,
): string => `
  <div>
    ${subtitle ? `<b>${subtitle}</b><br/>` : ''}
    ${title ? `${title}<br/>` : ''}
    ${info ? `${info}` : ''}
  </div>
`;

export interface Point {
  latitude: number;
  longitude: number;

  title?: string;
  subtitle?: string;
  info?: string;
  image?: string;
}

export interface GoogleMapProps {
  points?: Point[];
  clientLatitude: number | null;
  clientLongitude: number | null;
}

export type GoogleMapRef = {
  focusOn: (atitude: number, longitude: number, zoom?: boolean) => void;
};

export const GoogleMap = forwardRef<GoogleMapRef, GoogleMapProps>(
  ({ points, clientLatitude, clientLongitude }, ref) => {
    const map = useRef<google.maps.Map>();
    const markers = useRef<google.maps.Marker[]>([]);
    const mapDiv = useRef<HTMLDivElement>(null);

    const focusOn = (
      latitude: number,
      longitude: number,
      zoom = true,
    ): void => {
      if (zoom) map.current?.setZoom(16);

      map.current?.setCenter({
        lat: latitude,
        lng: longitude,
      });
    };

    useEffect(() => {
      const loader = new Loader({
        apiKey: process.env.NEXT_PUBLIC_GOOGLE_API_KEY ?? '',
        version: 'weekly',
      });

      loader.load().then(() => {
        map.current = new google.maps.Map(mapDiv.current, {
          center: {
            lat: clientLatitude || DEFAULT_LOCATION.lat,
            lng: clientLongitude || DEFAULT_LOCATION.lng,
          },
          zoom: 13,
          mapTypeControl: false,
          fullscreenControl: false,
          streetViewControl: false,
          zoomControlOptions: {
            position: google.maps.ControlPosition.TOP_LEFT,
          },
        });
      });

      if (ref) {
        (ref as MutableRefObject<GoogleMapRef>).current = { focusOn }; // eslint-disable-line
      }
    }, []); // eslint-disable-line

    useEffect(() => {
      if (clientLatitude && clientLongitude) {
        map.current?.setCenter({
          lat: clientLatitude,
          lng: clientLongitude,
        });
      }
    }, [clientLatitude, clientLongitude]);

    useEffect(() => {
      if (map.current) {
        if (markers.current) {
          markers.current.forEach((marker) => marker.setMap(null));
          markers.current = [];
        }

        points?.forEach((point) => {
          const marker = new google.maps.Marker({
            position: { lat: point.latitude, lng: point.longitude },
            map: map.current,
            title: point.title,
            ...(point.image ? { icon: point.image } : {}),
          });

          const infowindow =
            point.subtitle || point.info
              ? new google.maps.InfoWindow({
                  content: mapInfoContentFor(
                    point.title,
                    point.subtitle,
                    point.info,
                  ),
                })
              : null;

          marker.addListener('click', () => {
            map.current?.setZoom(16);

            map.current?.setCenter(marker.getPosition() || DEFAULT_LOCATION);

            infowindow && infowindow.open(map.current, marker);
          });

          markers.current = [...markers.current, marker];
        });

        if (points?.length)
          focusOn(points[0].latitude, points[0].longitude, false);
      }
    }, []); // estava causando um bug, quando saia da aba e voltava ele perdia a localização no mapa
    // com o loading de lojas no componente pai ele vai renderizar novamente, não sendo necessário adicionar os points no array de dependências.

    return <Box ref={mapDiv} borderRadius="8px" width="100%" />;
  },
);
