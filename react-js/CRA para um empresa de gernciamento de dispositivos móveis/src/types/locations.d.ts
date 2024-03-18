import { ID } from './util';

export interface LocationType {
  id: ID;
  latitude: string;
  longitude: string;
  address: string;
  createdAt: string;
  precision: number;
  device: {
    phoneNumber: string;
    deviceUser: {
      id: ID;
      name: string;
    };
  };
}

export interface GeolocationType {
  id: ID;
  latitude: string;
  longitude: string;
  precision: number;
  device: {
    phoneNumber: string;
    deviceUser: {
      id: ID;
      name: string;
    };
  };
}
