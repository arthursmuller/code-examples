import { Box, Tabs, TabList, TabPanels, Tab, TabPanel } from '@chakra-ui/react';
import React from 'react';

import Heading from '../../../components/Heading';
import Text from '../../../components/Text';
import GeneralCard from './GeneralCard/GeneralCard';
import GroupCard from './GroupCard/GroupCard';
import SubgroupCard from './SubgroupCard/SubgroupCard';
import UserCard from './UserCard/UserCard';

const BlockSiteUrl = () => {
  return (
    <>
      <Box>
        <Heading>Bloquear / Desbloquear sitios web por URL</Heading>
        <Text width="90%">
          Consulte Regras já criadas para bloquear ou liberar aplicativos, Las
          acciones generadas en este formulário se refieren al acceso a
          internet. Las aplicaciones podrán ser abiertos normalmente. La
          excepción ocurre en el modo device owner donde los iconos de las
          aplicaciones quedarán ocultas.
        </Text>
      </Box>
      <Box>
        <Tabs w="90%" borderRadius="5px">
          <TabList w="98%" d="flex" flexDirection="row" borderBottom="4px">
            <Tab
              fontSize="xl"
              fontWeight="bold"
              color="gray.300"
              ml="10px"
              _selected={{
                color: 'blue.600',
                borderBottom: '5px solid #0a3b79',
                marginBottom: '-4px',
              }}
            >
              General
            </Tab>
            <Tab
              fontSize="xl"
              fontWeight="bold"
              color="gray.300"
              _selected={{
                color: 'blue.600',
                borderBottom: '5px solid #0a3b79',
                marginBottom: '-4px',
              }}
            >
              Grupos
            </Tab>
            <Tab
              fontSize="xl"
              fontWeight="bold"
              color="gray.300"
              _selected={{
                color: 'blue.600',
                borderBottom: '5px solid #0a3b79',
                marginBottom: '-4px',
              }}
            >
              Subgrupos
            </Tab>
            <Tab
              fontSize="xl"
              fontWeight="bold"
              color="gray.300"
              _selected={{
                color: 'blue.600',
                borderBottom: '5px solid #0a3b79',
                marginBottom: '-4px',
              }}
            >
              Usuarios
            </Tab>
          </TabList>
          <TabPanels>
            <TabPanel>
              <GeneralCard />
            </TabPanel>
            <TabPanel>
              <GroupCard />
            </TabPanel>
            <TabPanel>
              <SubgroupCard />
            </TabPanel>
            <TabPanel>
              <UserCard />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Box>
    </>
  );
};

export default BlockSiteUrl;
