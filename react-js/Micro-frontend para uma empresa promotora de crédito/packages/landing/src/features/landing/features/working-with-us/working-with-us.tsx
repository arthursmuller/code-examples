import React, { FC } from 'react';

import {
  Tabs,
  TabList,
  Tab,
  TabPanels,
  TabPanel,
  Flex,
} from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header';

import WorkingWithUsBanner from './assets/trabalhe-conosco2x.jpg';
import { About, Culture, Programs } from './components';

const tabStyle = {
  fontWeight: 'bold',
  borderColor: 'primary.regular',
  color: 'primary.regular',
};

export const WorkingWithUs: FC = () => {
  return (
    <>
      <SubPageHeader
        backgroundImage={WorkingWithUsBanner}
        backgroundImageAlt="Uma pessoa em pé escrevendo em um caderno"
        position={['86%', '86%', 'right top']}
      >
        <SubPageHeader.Title
          title="TRABALHE CONOSCO"
          width={['270px', '270px', '570px']}
        />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle="É Seguro! É Tranquilo!  É Bem Promotora!"
          width={['220px', '220px', '500px']}
        />
      </SubPageHeader>

      <Flex p={['20px 16px', '32px 16px', '47px 133px']}>
        <Tabs variant="line" defaultIndex={0} maxW="100%" isLazy>
          <TabList>
            <Tab fontSize={['14px', '14px', '16px']} _selected={tabStyle}>
              Sobre a Bem
            </Tab>
            <Tab fontSize={['14px', '14px', '16px']} _selected={tabStyle}>
              Cultura
            </Tab>
            <Tab fontSize={['14px', '14px', '16px']} _selected={tabStyle}>
              Programas
            </Tab>
          </TabList>

          <TabPanels maxW="100%">
            <TabPanel maxW="100%">
              <About />
            </TabPanel>
            <TabPanel p={0}>
              <Culture />
            </TabPanel>
            <TabPanel p={0}>
              <Programs />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Flex>
    </>
  );
};
