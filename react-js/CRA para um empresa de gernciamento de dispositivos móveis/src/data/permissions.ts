import { UserAdminRolesType } from "../types/userAdmin";

export const allPermissions: {
  code: keyof UserAdminRolesType;
  textIntlKey: string;
}[] = [
  {
    code: 'viewUsersGps',
    textIntlKey: 'role.viewUsersGps',
  },
  {
    code: 'editUserRole',
    textIntlKey: 'role.editUserRole',
  },
  {
    code: 'editConfig',
    textIntlKey: 'role.editConfig',
  },
  {
    code: 'editLimits',
    textIntlKey: 'role.editLimits',
  },
  {
    code: 'editActions',
    textIntlKey: 'role.editActions',
  },
];
