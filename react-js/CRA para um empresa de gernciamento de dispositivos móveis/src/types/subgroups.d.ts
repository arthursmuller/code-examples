import { GroupType } from './group';

export interface SubgroupType {
  id?: number;
  name?: string;
  userCount?: number;
  group?: GroupType;
  company_name?: string;
  subgroup_users_qtd?: string;
  addDeviceUserIds?: DeviceUserType['id'][];
  deleteDeviceUserIds?: DeviceUserType['id'][];
}
