import { FC } from 'react';

import { Route, Switch, useRouteMatch } from 'react-router-dom';

import { PageLayout } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import { ListAddresses } from './list-addresses';
import { CreateAddress } from './create-address';
import { EditAddress } from './edit-address';

export const Addresses: FC = () => {
  const { path } = useRouteMatch();
  const navigateUp = useNavigatePathUp();

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Meus Endereços</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <Switch>
          <Route exact path={path}>
            <ListAddresses />
          </Route>
          <Route path={`${path}/novo`}>
            <CreateAddress />
          </Route>
          <Route path={`${path}/:enderecoId`}>
            <EditAddress />
          </Route>
        </Switch>
      </PageLayout.Content>
    </PageLayout>
  );
};
