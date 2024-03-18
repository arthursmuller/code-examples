import { CompanyType } from './company';
import { GroupType } from './group';
import { PrivilegeType } from './privilege';
import { SubgroupType } from './subgroups';
import { ID } from './util';

export interface UserAdminType {
  id?: ID;
  name?: string;
  company?: CompanyType;
  email?: string;
  privilegeId?: PrivilegeType['id'];
  federalCode?: string;
  gmt?: string;
  language?: string;
  adminID?: string;
  language?: string;
  kioskMode?: boolean;
  password?: string;
  role?: UserAdminRolesType;
  groupIds?: GroupType['id'][];
  subGroupIds?: SubgroupType['id'][];
}

export interface UserAdminRolesType {
  viewUsersGps: boolean;
  editUserRole: boolean;
  editConfig: boolean;
  editLimits: boolean;
  editActions: boolean;
}
