import { DeviceUserType } from './deviceUser';
import { GroupType } from './group';
import { SubgroupType } from './subgroups';

export interface DeviceType {
  id?: number;
  name?: string;
  phoneNumber?: string;
  imei?: string;
  iccid?: string;
  manufacturer?: ManufacturerType['manufacturer'];
  model?: ModelType['model'];
  federalCode?: string,
  compliance?: boolean,
  powerStatus?: boolean;
  unlockPassword?: number,
  oSVersion?: string,
  datamobVersion?: string,
  gpsStatus?: boolean,
  battery?: number,
  activatedAt?: Date,
  updatedAt?: Date,
  freeMemory?: number,
  modeOwner?: boolean,
  locked?: boolean,
  group?: GroupType,
  subGroup?: SubgroupType,
  deviceUser?: DeviceUserType;
}


export interface PasswordsHistoricType {
  password?: number;
  date?: string;
}

export interface ManufacturerType {
  manufacturer: string;
}

export interface InventoryItemType {
  manufacturer?: string;
  count?: number;
}

export interface ModelType {
  model: string;
}
