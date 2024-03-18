import { MenuItem as MenuItemChakra, MenuItemProps as MenuItemPropsChakra } from '@chakra-ui/react';
import React from 'react';

interface MenuItemProps2 extends MenuItemPropsChakra {
  text: React.ReactNode;
  onClick: () => void;
}

const MenuItem = ({text, onClick, ...rest}: MenuItemProps2) => {
  return (
    <MenuItemChakra
      color="black.500"
      fontSize="sm"
      {...rest}
      onClick={onClick}
    >
      {text}
    </MenuItemChakra>
  );
}

export default MenuItem;