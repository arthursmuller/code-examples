import { Box, Tabs, TabList, TabPanels, Tab, TabPanel } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Heading from '../../../components/Heading';
import Text from '../../../components/Text';
import { updateSelectedTab } from '../../../store/generalConfig';
import GeneralCard from './GeneralCard';
import GroupsCard from './GroupsCard';
import SubgroupCard from './SubgroupCard';
import UserCard from './UsersCard';

const GeneralConfig = () => {
  const dispatch = useDispatch();
  const generalConfig = useSelector((state) => state.generalConfig);
  const { selectedTab } = generalConfig;
  const handleTabsChange = (index) => {
    dispatch(updateSelectedTab(index));
  };
  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="general_config.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="general_config.description" />
        </Text>
      </Box>
      <Tabs
        w="90%"
        borderRadius="5px"
        index={selectedTab}
        onChange={handleTabsChange}
      >
        <TabList w="98%" d="flex" borderBottom="4px">
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
            <FormattedMessage id="global.general" />
          </Tab>
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
            <FormattedMessage id="global.groups" />
          </Tab>
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
            <FormattedMessage id="global.subgroup" />
          </Tab>
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
            <UserCard />
          </TabPanel>
        </TabPanels>
      </Tabs>
    </>
  );
};

export default GeneralConfig;
