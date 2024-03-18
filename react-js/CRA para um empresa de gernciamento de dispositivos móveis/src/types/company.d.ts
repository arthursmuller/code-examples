import { PlanType } from './plan';
import { StateType } from './state';
import { Bytes, ID } from './util';

export interface CompanyType {
  id: ID;
  corporateName?: string;
  name: string;
}

export interface LicenseType {
  id?: ID;
  name?: string;
  status: true;
  state?: StateType['key'];
  phoneNumber?: string;
  plan?: PlanType;
  planName?: string;
  cancelledAt?: never;
  createdAt?: Date;
  updatedAt?: Date;
  license?: LicenseEntityType;
}

export interface LicenseEntityType {
  id?: ID;
  companyId?: unknown;
  bought?: unknown;
  free?: unknown;
  poc?: unknown;
  pocStarted?: unknown;
  pocEnded?: unknown;
  createdAt?: unknown;
  updatedAt?: unknown;
}
  
export interface ConsumptionType {
  id?: ID;
  consumptionDate?: Date;
  user: string;
  phoneNumber: string;
  group: string;
  subGroup: string;
  roaming: boolean;
  carrierNetwork: string;
}

export interface DataFilter {
  user?: string;
  phoneNumber?: string;
  group?: string;
  subgroup?: string;
};
  
export interface ConsumptionDataType extends ConsumptionType {
  consumption: Bytes;
}
  
export interface ConsumptionSmsType extends ConsumptionType  {
  destinationPhoneNumber: string;
}

