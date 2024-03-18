import { FC, ReactElement } from 'react';

import { Avatar, Flex, Center } from '@chakra-ui/react';

import { PageLayout } from './page-layout';

import { CustomHeading } from '../custom-heading';

export default {
  title: 'PageLayout',
  component: PageLayout,
  decorators: [
    (Story: FC): ReactElement => (
      <Flex flexDir="column" h="90vh">
        <Story />
      </Flex>
    ),
  ],
};

export const Default: FC = () => (
  <PageLayout>
    <PageLayout.Header>
      <PageLayout.BackButton onClick={() => {}} />
      <PageLayout.Title>Header title</PageLayout.Title>
    </PageLayout.Header>

    <PageLayout.Content title="Title Content">
      <CustomHeading>Content goes here...</CustomHeading>
    </PageLayout.Content>
  </PageLayout>
);

export const LongContent: FC = () => (
  <PageLayout>
    <PageLayout.Header>
      <PageLayout.BackButton onClick={() => {}} />
      <PageLayout.Title>Header title</PageLayout.Title>
    </PageLayout.Header>

    <PageLayout.Content title="Title Content">
      <CustomHeading>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ex ipsum
        necessitatibus error id assumenda, animi rerum placeat mollitia sint
        nesciunt odit repudiandae incidunt modi autem, ratione similique neque
        fuga fugit? Lorem ipsum dolor sit amet consectetur adipisicing elit. Ex
        ipsum necessitatibus error id assumenda, animi rerum placeat mollitia
        sint nesciunt odit repudiandae incidunt modi autem, ratione similique
        neque fuga fugit? ipsum necessitatibus error id assumenda, animi rerum
        placeat mollitia sint nesciunt odit repudiandae incidunt modi autem,
        ratione similique neque fuga fugit?
      </CustomHeading>
    </PageLayout.Content>
  </PageLayout>
);

export const CustomHeader: FC = () => (
  <PageLayout bg="primary.regular">
    <PageLayout.Header>
      <PageLayout.BackButton onClick={() => {}} />
      <Center h="75px" w="100%">
        <Avatar />
      </Center>
    </PageLayout.Header>

    <PageLayout.Content>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
      <CustomHeading>Content goes here...</CustomHeading>
    </PageLayout.Content>
  </PageLayout>
);

export const CustomContent: FC = () => (
  <PageLayout>
    <PageLayout.Header>
      <PageLayout.BackButton onClick={() => {}} />
      <PageLayout.Title>Header title</PageLayout.Title>
    </PageLayout.Header>

    <Flex bg="success.washed" h="250px" w="450px" m="auto" />
  </PageLayout>
);
