import { StrictMode } from 'react';
import ReactDOM from 'react-dom';

import { createFakeServer } from 'server';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import { App } from './app';

if (process.env.REACT_APP_START_MIRAGEJS === 'true') {
  createFakeServer();
}

ReactDOM.render(
  <StrictMode>
    <App />
  </StrictMode>,
  document.getElementById('root'),
);

serviceWorkerRegistration.register();