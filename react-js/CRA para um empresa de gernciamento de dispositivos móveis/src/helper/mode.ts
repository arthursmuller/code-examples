/*
 * Helpers for CRUD mode
 */

export enum ModeEnum {
  CREATE = 'CREATE',
  UPDATE = 'UPDATE',
  DELETE = 'DELETE',
}

export const ModeObject: { [key: string]: ModeEnum } = {
  [ModeEnum.CREATE]: ModeEnum.CREATE,
  [ModeEnum.UPDATE]: ModeEnum.UPDATE,
  [ModeEnum.DELETE]: ModeEnum.DELETE,
};

export const getMode = (paramsId: string) =>
  !paramsId ? ModeObject.CREATE : ModeObject.UPDATE;
