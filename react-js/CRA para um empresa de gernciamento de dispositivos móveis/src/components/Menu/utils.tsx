import { TriangleDownIcon } from '@chakra-ui/icons';
import { Box } from '@chakra-ui/react';

export const handleItemsToRender = ({
  menuItems,
  setItemsToRender,
  selectedMenu,
}) => {
  if (selectedMenu === 'root') {
    const items = menuItems?.map((menuItem) => ({
      id: menuItem.id,
      title: menuItem.title,
      link: menuItem.link,
    }));
    setItemsToRender(items);
  } else {
    const menuItem = menuItems?.filter(
      (menuItem) => menuItem.id === selectedMenu
    );

    setItemsToRender(menuItem?.[0].drilldown);
  }
};

export const renderDrilldownMenu = ({
  itemsToRender,
  setSelectedMenu,
  setDrillDownMenuOpened,
  history,
}) => {
  return itemsToRender.map((menuItem, index) => {
    if (menuItem.drilldown) {
      return (
        <Box
          key={index}
          height={menuItem.drilldown ? '100%' : null}
          _first={{
            height: itemsToRender.length == 1 ? '100%' : 'fit-content',
          }}
        >
          <Box
            as="h3"
            fontSize={menuItem.titleStyle === 'sub' ? '16px' : '24px'}
            letterSpacing="0.96px"
            color={menuItem.titleStyle === 'sub' ? '#d7d7dc' : '#0a3b79'}
            margin="23px 0 21px 0"
          >
            {menuItem.title}
          </Box>
          <Box
            as="ul"
            borderLeft="solid 1px #d7d7dc"
            padding="0 0 0 24.5px"
            margin="0 0 0 10px"
          >
            {menuItem.drilldown &&
              menuItem.drilldown.map((menuItem, index) => {
                return (
                  <Box
                    as="li"
                    key={index}
                    margin="0 51px 24px 0"
                    fontSize="16px"
                    letterSpacing="0.64px"
                    color="#282832"
                    listStyleType="none"
                    cursor="pointer"
                    onClick={() => {
                      setSelectedMenu('root');
                      setDrillDownMenuOpened(false);
                      history.push(menuItem.link);
                    }}
                  >
                    {menuItem.title}
                  </Box>
                );
              })}
          </Box>
        </Box>
      );
    } else {
      return (
        <Box
          // h="52.5px"
          display="flex"
          alignItems="center"
          cursor="pointer"
          justifyContent="space-between"
          key={index}
          onClick={() => {
            if (menuItem.link) {
              history.push(menuItem.link);
              setDrillDownMenuOpened(false);
            } else {
              setSelectedMenu(menuItem.id);
            }
          }}
        >
          <Box>{menuItem.title}</Box>
          {!menuItem.link && (
            <Box>
              <TriangleDownIcon color="#d7d7dc" boxSize="3" />
            </Box>
          )}
        </Box>
      );
    }
  });
};
