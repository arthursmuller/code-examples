import React, { useState, createContext } from 'react';

import MenuDrillDown from './components/MenuDrillDown';
import MenuSideIcons from './components/MenuSideIcons';
import getMenuItems, { MenuItem } from './menuItems';

interface MenuContext {
  selectedMenu: string;
  setSelectedMenu: (selectedMenu: string) => void;
  drillDownMenuOpened: boolean;
  setDrillDownMenuOpened: (selectedMenu: boolean) => void;
  menuItems: MenuItem[];
}

//
// Menu context creation
export const MenuContext = createContext({} as MenuContext);

function Menu() {
  const [drillDownMenuOpened, setDrillDownMenuOpened] = useState(false);
  const [selectedMenu, setSelectedMenu] = useState('root');

  const menuItems = getMenuItems();

  return (
    <MenuContext.Provider
      value={{
        selectedMenu,
        setSelectedMenu,
        drillDownMenuOpened,
        setDrillDownMenuOpened,
        menuItems,
      }}
    >
      <MenuSideIcons />
      <MenuDrillDown />
    </MenuContext.Provider>
  );
}

export default Menu;
