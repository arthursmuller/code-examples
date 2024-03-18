import { StateType } from '../types/state';

export const routeWithParameters = (route: string, parameters?: Record<string, string | number>) => {
  return route.replace(/:([^\/]*)/g, (match, p1) => `${parameters?.[p1]}`);
};

export enum PrivilegeEnum {
  USER = 1,
  SUBGROUP = 7,
  GROUP = 2,
  COMPANY = 3,
}

export enum StateEnum {
  'PRE-ACTIVATED' = 'PRE-ACTIVATED',
  ACTIVATE = 'ACTIVATE',
  CANCELLED = 'CANCELLED',
  CANCELLING = 'CANCELLING',
  SUSPEND = 'SUSPEND',
}

export const listStates: StateType[] = [
  {
    key: StateEnum['PRE-ACTIVATED'],
    value: 'status.preactive',
  },
  {
    key: StateEnum.ACTIVATE,
    value: 'status.active',
  },
  {
    key: StateEnum.CANCELLED,
    value: 'status.cancelled',
  },
  {
    key: StateEnum.CANCELLING,
    value: 'status.cancelling',
  },
  {
    key: StateEnum.SUSPEND,
    value: 'status.suspend',
  },
];

export const listStatesObject: { [key: string]: StateType } = {
  [StateEnum['PRE-ACTIVATED']]: listStates[0],
  [StateEnum.ACTIVATE]: listStates[1],
  [StateEnum.CANCELLED]: listStates[2],
  [StateEnum.CANCELLING]: listStates[3],
  [StateEnum.SUSPEND]: listStates[4],
};

export const DEBOUNCE_TIME = 500;

export const toggleCheckbox: <T extends string | number>(
  list: T[],
  toggleId: T
) => T[] = (list, toggleId) => {
  const listOld = list || [];

  return list?.includes(toggleId)
    ? listOld.filter((id) => id !== toggleId)
    : [...listOld, toggleId];
};

export const GROUP_PAGE_SIZE_FIXED = 10;
export const SUBGROUP_PAGE_SIZE_FIXED = 10;
