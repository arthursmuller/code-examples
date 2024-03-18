import { FC } from 'react';

import { Flex, Text, Button } from '@chakra-ui/react';

import { TabPropostasInativaIcon } from '@pcf/design-system-icons';

import { ContractsDrawer } from './components/contracts-drawer';

import { DashboardCard } from '../components';

export const ContractsCard: FC = () => (
  <DashboardCard
    title="Meus Contratos"
    icon={TabPropostasInativaIcon}
    body={
      <Flex direction="column" flexGrow={1}>
        <Text as="p" textStyle="bold12" mb="12px" flexGrow={1}>
          Acesse para ver a listagem dos seus contratos e seus respectivos
          detalhes.
        </Text>

        <ContractsDrawer
          buttonRender={(props) => <Button {...props}>Acessar</Button>}
        />
      </Flex>
    }
  />
);
