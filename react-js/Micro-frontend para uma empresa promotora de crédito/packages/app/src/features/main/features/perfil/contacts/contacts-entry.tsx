import { FC } from 'react';

import { Divider } from '@chakra-ui/react';

import { PageLayout } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import { Phones } from './phones';
import { Emails } from './emails';

export const Contacts: FC = () => {
  const navigateUp = useNavigatePathUp();

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Meus Contatos</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <Phones />

        <Divider borderColor="secondary.washed" borderBottomWidth="2px" />

        <Emails />
      </PageLayout.Content>
    </PageLayout>
  );
};
