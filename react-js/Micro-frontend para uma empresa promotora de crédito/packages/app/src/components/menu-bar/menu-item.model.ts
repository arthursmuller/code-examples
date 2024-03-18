import { ComponentType, FC, FunctionComponent } from 'react';

export interface MenuItem {
  label: string;
  icon?: FunctionComponent | ComponentType;
  iconActive?: FunctionComponent | ComponentType;
  route?: string;
  disabled?: boolean;
  isExternal?: boolean;
  items?: MenuItem[];
  menuColor?: string;
  validator?: FC;
}
