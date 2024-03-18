import { DeviceUserType } from "./deviceUser";

export interface GroupType {
  id?: number;
  name?: string;
  userCount?: number;
  addDeviceUserIds?: DeviceUserType['id'][];
  deleteDeviceUserIds?: DeviceUserType['id'][];
}
