import { MessageStatusEnum } from '.././helper/messageStatus';
import { DeviceUserType } from './deviceUser';

export interface MessageType {
  id?: number;
  createdAt?: Date;
  content?: string;
  detail?: string;
  subGroupIds?: ID[];
  groupIds?: ID[];
  userIds?: ID[];
  companyId?: ID;
}

export interface MessageTypeFilter {
  startAt?: Date;
  endAt?: Date;
  search?: string;
}

export interface MessageDetailsType {
  sentAt?: Date;
  status?: MessageStatusEnum;
  deviceUser?: {
    name?: string;
    device?: {
      phoneNumber?: number;
    };
  };
}
