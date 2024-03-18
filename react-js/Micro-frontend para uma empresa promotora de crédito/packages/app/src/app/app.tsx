import { FC } from 'react';

import { TrackingUserConnection } from '@pcf/design-system';

import { AppRoutes } from './routes';
import AppProviders from './app-providers';

import './app-bootstrap';

const App: FC = () => {
  return (
    <AppProviders>
      <AppRoutes />

      <TrackingUserConnection />
    </AppProviders>
  );
};

export default App;
