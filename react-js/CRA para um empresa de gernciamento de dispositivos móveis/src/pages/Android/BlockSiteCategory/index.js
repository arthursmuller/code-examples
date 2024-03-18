import { Box, Tabs, TabList, TabPanels, Tab, TabPanel } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';

import Heading from '../../../components/Heading';
import Text from '../../../components/Text';
import GeneralCard from './GeneralCard';
import GroupsCard from './GroupsCard';
import SubgroupCard from './SubgroupCard';
import UsersCard from './UsersCard';

const BlockSiteCategory = () => {
  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="sites.block.general.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="sites.block.general.text" />
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
              <FormattedMessage id="global.general" />
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
              <FormattedMessage id="global.groups" />
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
              <FormattedMessage id="global.subgroups" />
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
              <FormattedMessage id="global.users" />
            </Tab>
          </TabList>
          <TabPanels>
            <TabPanel>
              <GeneralCard />
            </TabPanel>
            <TabPanel>
              <GroupsCard />
            </TabPanel>
            <TabPanel>
              <SubgroupCard />
            </TabPanel>
            <TabPanel>
              <UsersCard />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Box>
    </>
  );
};

export default BlockSiteCategory;
