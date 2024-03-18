import { GroupType } from './group';
import { Bytes, ID } from './util';

export interface ApplicationType {
  consumption?: Bytes;
  icon?: string;
  id?: ID;
  name?: string;
  pacakageName?: string;
  quantity?: number;
  time?: string;
  urlIcon?: string;
}

export interface ApplicationDeviceUserType {
  consumption?: number;
  group?: string;
  packageName?: string;
  phoneNumber?: string;
  subgroup?: string;
  user?: string;
  userId?: ID;
}

export interface ApplicationConsumptionHistoryType {
  day?: number;
  consumption?: number;
}

export type ApplicationsFilter = {
  deviceUser?: DeviceUserType;
  group?: GroupType;
  subgroup?: SubgroupType;
};
