import { CompanyType } from './company';
import { DeviceType } from './device';
import { GroupType } from './group'
import { SubgroupType } from './subgroup'
import { ID } from "./util";

export interface DeviceUserType {
  id?: ID;
  name?: string;
  federalCode?: string;
  phoneNumber?: string;
  group?: GroupType;
  groupId?: GroupType['id'];
  subGroup?: SubgroupType;
  subGroupId?: SubgroupType['id'];
  company?: CompanyType;
  email?: string;
  device?: DeviceType;
}
