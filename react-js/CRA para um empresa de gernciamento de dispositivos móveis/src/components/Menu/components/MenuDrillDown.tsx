import { Box } from '@chakra-ui/react';
import { useEffect, useState, useContext } from 'react';
import { useHistory } from 'react-router-dom';

import { MenuContext } from '../index';
import { handleItemsToRender, renderDrilldownMenu } from '../utils';

//
// context

function MenuDrillDown() {
  const [itemsToRender, setItemsToRender] = useState();
  const {
    drillDownMenuOpened,
    menuItems,
    selectedMenu,
    setSelectedMenu,
    setDrillDownMenuOpened,
  } = useContext(MenuContext);
  const history = useHistory();

  useEffect(() => {
    handleItemsToRender({ menuItems, setItemsToRender, selectedMenu });
  }, [selectedMenu]);

  return (
    <>
      <Box
        zIndex={2}
        top="0"
        left="0"
        width="100%"
        height="100%"
        position="absolute"
        backgroundColor="rgba(0,0,0,0.5)"
        display={drillDownMenuOpened ? 'block' : 'none'}
      >
        <Box
          p="0 32px 0 82px"
          display="flex"
          backgroundColor="white"
          width="fit-content"
          backdropFilter="blur(30px)"
          boxShadow="0 3px 10px 0 rgba(0, 0, 0, 0.4)"
          borderRadius="0 20px 20px 0"
          flexDirection="column"
          // minHeight="100%"
          justifyContent="space-around"
          h="100vh"
          w="380px"
          position="fixed"
        >
          {selectedMenu === 'root' && (
            <Box
              fontSize="16px"
              lineHeight="1.25"
              letterSpacing="0.64px"
              color="#d7d7dc"
              // margin="30px 0 30px 0"
              h="65px"
              d="flex"
              alignItems="center"
            >
              Fechar Menu
            </Box>
          )}

          {itemsToRender &&
            renderDrilldownMenu({
              itemsToRender,
              setSelectedMenu,
              setDrillDownMenuOpened,
              history,
            })}
        </Box>
      </Box>
    </>
  );
}
export default MenuDrillDown;
