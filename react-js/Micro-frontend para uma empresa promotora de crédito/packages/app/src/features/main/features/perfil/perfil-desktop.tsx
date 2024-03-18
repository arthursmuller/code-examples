import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { ListPerfilOptions } from './components/list-perfil-options';
import { PerfilRoutes } from './perfil-routes';

export const PerfilDesktop: FC = () => {
  return (
    <PerfilRoutes>
      <Flex
        flex="1"
        flexDir="column"
        paddingLeft={6}
        paddingRight={12}
        overflowY="auto"
      >
        <ListPerfilOptions />
      </Flex>
    </PerfilRoutes>
  );
};

export { PerfilDesktop as default };
