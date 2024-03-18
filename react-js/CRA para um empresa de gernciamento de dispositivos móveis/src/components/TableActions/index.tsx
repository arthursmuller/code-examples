import { Menu, MenuButton, MenuList } from '@chakra-ui/react';
import React, { useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useHistory } from 'react-router-dom';

import { AlertDelete } from '../Alert';
import Delete from '../Icons/Delete';
import Edit from '../Icons/Edit';
import Eye from '../Icons/Eye';
import ThreeDotsIcon from '../Icons/ThreeDots';
import MenuItem from './MenuItem';

interface TableActionsProps {
  entityIntlLabel?: string | React.ReactNode;
  onOpenMenu?: () => void;
  onCloseMenu?: () => void;
  linkEdit?: string;
  linkView?: string;
  openDestroy?: () => void;
  moreItems?: JSX.Element;
}

const TableActions: React.FC<TableActionsProps> = ({
  onOpenMenu,
  onCloseMenu,
  linkEdit,
  linkView,
  openDestroy,
  entityIntlLabel,
  moreItems,
}: TableActionsProps) => {
  const iconRef = useRef();
  const history = useHistory();

  const editItem = {
    icon: <Edit boxSize={6} mr="5px" />,
    text: (
      <FormattedMessage
        id="global.action.edit"
        values={{ entity: entityIntlLabel }}
      />
    ),
    onClick: () => history.push(linkEdit),
  };
  const deleteItem = {
    icon: <Delete boxSize={6} mr="5px" />,
    text: (
      <FormattedMessage
        id="global.action.remove"
        values={{ entity: entityIntlLabel }}
      />
    ),
    onClick: () => AlertDelete({ onConfirm: openDestroy }),
    color: 'red.500',
  };
  const viewItem = {
    icon: <Eye boxSize={6} mr="5px" />,
    text: (
      <FormattedMessage
        id="global.action.view"
        values={{ entity: entityIntlLabel }}
      />
    ),
    onClick: () => history.push(linkView),
  };

  return (
    <Menu placement="top" onClose={onCloseMenu}>
      <MenuButton
        as={ThreeDotsIcon}
        boxSize={6}
        ref={iconRef}
        cursor="pointer"
        onClick={onOpenMenu}
      >
        Actions
      </MenuButton>
      <MenuList>
        {linkEdit && <MenuItem {...editItem} />}
        {linkView && <MenuItem {...viewItem} />}
        {openDestroy && <MenuItem {...deleteItem} />}
        {moreItems}
      </MenuList>
    </Menu>
  );
};

export default TableActions;
