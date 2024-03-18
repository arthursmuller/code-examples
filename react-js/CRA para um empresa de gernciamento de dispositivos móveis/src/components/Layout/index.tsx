import { Grid, GridItem, Flex, Box, Image, Text } from '@chakra-ui/react';

import logo from '../../assets/Images/Logo horizontal@3x.png';
import { useAppDispatch, useAppSelector } from '../../hooks/useRedux';
import { logout } from '../../store/auth';
import Menu from '../Menu';
import Settings from '../Settings';

const Layout = ({ children }) => {
  const dispatch = useAppDispatch();

  const { user } = useAppSelector((state) => state.auth);

  const handleLogout = () => {
    dispatch(logout());
  };

  return (
    <Flex color="blue" flexDirection="row" position="relative">
      <Menu />
      <Grid
        w="100%"
        templateAreas="'appBar' 'content'"
        templateColumns="repeat(auto-fit, minmax(80px, 1fr))"
        zIndex={0}
        ml="65px"
      >
        <GridItem
          gridArea="appBar"
          bg="white"
          borderBottom="solid 1px #d7d7dc"
          borderLeft="solid 1px #d7d7dc"
          w="inherit"
          position="fixed"
          zIndex={5}
        >
          <Box
            h="80px"
            d="flex"
            justifyContent="space-between"
            m="0% 8% 0% 2.5%"
            position="sticky"
            alignItems="center"
          >
            <Image
              src={logo}
              width="200px"
              objectFit="contain"
              height="24px"
              layout="fixed"
              alt="logo"
            />
            <Box d="flex" alignItems="center" justifyContent="space-evenly">
              <Settings labelLogout="global.logoff" logoutClick={handleLogout} />
              <Text color="gray.500" fontSize="sm">
                { user?.name || user?.email }
              </Text>
            </Box>
          </Box>
        </GridItem>
        <GridItem gridArea="content" bg="#f2f4f8" mt="80px">
          <Box m="3% 0% 2% 4%" minHeight="85vh">
            {children}
          </Box>
        </GridItem>
      </Grid>
    </Flex>
  );
};

export default Layout;
