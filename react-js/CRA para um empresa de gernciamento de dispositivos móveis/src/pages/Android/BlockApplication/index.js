import { Box, Tabs, TabList, TabPanels, Tab, TabPanel } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Heading from '../../../components/Heading';
import Text from '../../../components/Text';
import { updateSelectedTab } from '../../../store/application';
import GeneralCard from './GeneralCard/GeneralCard';
import GroupCard from './GroupCard/GroupCard';
import SubgroupCard from './SubgroupCard/SubgroupCard';
import UserCard from './UserCard/UserCard';

const BlockApplication = () => {
  const dispatch = useDispatch();
  const application = useSelector((state) => state.application);
  const { general, selectedTab } = application;
  const handleTabsChange = (index) => {
    dispatch(updateSelectedTab(index));
  };
  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="block_application.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="block_application.title_text" />
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
            <FormattedMessage id="block_application.general" />
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
            <FormattedMessage id="block_application.groups" />
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
            <FormattedMessage id="block_application.subgroups" />
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
            <FormattedMessage id="block_application.users" />
          </Tab>
        </TabList>
        <TabPanels>
          <TabPanel>
            <GeneralCard general={general} />
          </TabPanel>
          <TabPanel>
            <GroupCard general={general} />
          </TabPanel>
          <TabPanel>
            <SubgroupCard general={general} />
          </TabPanel>
          <TabPanel>
            <UserCard general={general} />
          </TabPanel>
        </TabPanels>
      </Tabs>
    </>
  );
};

export default BlockApplication;
