import { ExternalLinkIcon } from '@chakra-ui/icons';
import {
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  Button,
  Box,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { Link } from 'react-router-dom';

import PersonIcon from '../Icons/Person';

interface SettingsProps {
  onCloseMenu?: () => void;
  labelLogout?: string;
  logoutClick?: () => void;
}

const Settings: React.FC<SettingsProps> = ({
  onCloseMenu,
  labelLogout,
  logoutClick,
}: SettingsProps) => {
  return (
    <Menu placement="bottom" onClose={onCloseMenu}>
      <MenuButton
        as={Button}
        borderRadius="100%"
        w="48px"
        h="48px"
        background="#f2f4f8"
        m="0px 10px 0px 14px"
      >
        <Box m="0px 0px 0px -5px">
          <PersonIcon boxSize={7} />
        </Box>
      </MenuButton>
      <MenuList>
        <Link to="#" onClick={logoutClick}>
          <MenuItem color="blue.500" fontSize="sm">
            <ExternalLinkIcon boxSize={5} mr="5px" color="blue.500" />
            <FormattedMessage id={labelLogout} />
          </MenuItem>
        </Link>
      </MenuList>
    </Menu>
  );
};

export default Settings;
