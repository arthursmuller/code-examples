import { ArrowBackIcon } from '@chakra-ui/icons';
import { Box } from '@chakra-ui/react';
import { useContext } from 'react';
import { useHistory } from 'react-router-dom';

import MenuIcon from '../../Icons/Menu';
import { MenuContext } from '../index';

function MenuSideIcons() {
  const {
    drillDownMenuOpened,
    menuItems,
    selectedMenu,
    setSelectedMenu,
    setDrillDownMenuOpened,
  } = useContext(MenuContext);
  const history = useHistory();

  return (
    <Box
      backgroundColor="#0a3b79"
      w="65px"
      position="fixed"
      min-height="100%"
      zIndex={3}
    >
      <Box
        backgroundColor="white"
        d="flex"
        h="80px"
        w="65px"
        alignItems="center"
        justifyContent="center"
        cursor="pointer"
        onClick={() => {
          if (selectedMenu === 'root') {
            setDrillDownMenuOpened(!drillDownMenuOpened);
          } else {
            setSelectedMenu('root');
          }
        }}
      >
        {!drillDownMenuOpened && <MenuIcon boxSize={7} color="#0190fe" />}
        {drillDownMenuOpened && <ArrowBackIcon boxSize={7} color="#0190fe" />}
      </Box>
      <Box
        d="flex"
        flexDirection="column"
        alignItems="center"
        w="65px"
        justifyContent="space-around"
        p="10px 0 0 0"
        h="calc(100vh - 80px)"
      >
        {menuItems.map((menuItem, index) => (
          <Box
            key={`menu-${index}`}
            cursor="pointer"
            // height="52.5px"
            onClick={() => {
              if (menuItem.link) {
                setDrillDownMenuOpened(false);
                setSelectedMenu('root');
                history.push(menuItem.link);
              } else {
                setDrillDownMenuOpened(true);
                setSelectedMenu(menuItem.id);
              }
            }}
          >
            {menuItem.icon}
          </Box>
        ))}
      </Box>
    </Box>
  );
}
export default MenuSideIcons;
