export enum MessageStatusEnum {
  ERROR_SENDING_MESSAGE = -1,
  IN_THE_SEND_QUEUE = 0,
  SENT = 1,
  RECEIVED = 2,
  READ = 3,
  INSTALLED = 4,
  NOT_INSTALLED = 5,
  EXECUTED = 6,
  NOT_EXECUTED = 7,
  UNINSTALLED = 8,
  NOT_UNINSTALLED = 9,
}

export const MessageStatusToIntl = {
  [MessageStatusEnum.ERROR_SENDING_MESSAGE]:
    'message_detail.status.error_sending_message',
  [MessageStatusEnum.IN_THE_SEND_QUEUE]:
    'message_detail.status.in_the_send_queue',
  [MessageStatusEnum.SENT]: 'message_detail.status.sent',
  [MessageStatusEnum.RECEIVED]: 'message_detail.status.received',
  [MessageStatusEnum.READ]: 'message_detail.status.read',
  [MessageStatusEnum.INSTALLED]: 'message_detail.status.installed',
  [MessageStatusEnum.NOT_INSTALLED]: 'message_detail.status.not_installed',
  [MessageStatusEnum.EXECUTED]: 'message_detail.status.executed',
  [MessageStatusEnum.NOT_EXECUTED]: 'message_detail.status.not_executed',
  [MessageStatusEnum.UNINSTALLED]: 'message_detail.status.uninstalled',
  [MessageStatusEnum.NOT_UNINSTALLED]: 'message_detail.status.not_uninstalled',
};
