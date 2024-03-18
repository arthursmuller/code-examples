import { FC } from 'react';

import { Flex, Text, Button } from '@chakra-ui/react';

import { ClockIcon } from '@pcf/design-system-icons';

import { ProposalsDrawer } from './components/proposals-drawer';

import { DashboardCard } from '../components';

export const ProposalsCard: FC = () => (
  <DashboardCard
    title="Minhas Propostas"
    icon={ClockIcon}
    body={
      <Flex direction="column">
        <Text as="p" textStyle="bold12" mb="12px">
          Acesse para ver a listagem das suas intenções de operação e seus
          respectivos detalhes.
        </Text>

        <ProposalsDrawer
          buttonRender={(props) => <Button {...props}>Acessar</Button>}
        />
      </Flex>
    }
  />
);
