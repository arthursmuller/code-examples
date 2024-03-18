import { rest } from 'msw';
import { setupServer } from 'msw/node';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { QueryClientProvider } from 'react-query';

import {
  Notificacao,
  NOTIFICACOES_QUERY_ENDPOINT,
  queryCacheConfig,
} from '@pcf/core';
import { pcfApi, pcfApiResponse } from 'setupTests';
import { mainRoutePaths } from 'features/main/routes';
import MainApp from 'features/main/main-mobile';

import ImportData from '../importar-dados';

const server = setupServer(
  rest.get<Notificacao[]>(
    pcfApi(NOTIFICACOES_QUERY_ENDPOINT),
    pcfApiResponse([
      {
        id: 1,
        titulo: 'Titulo',
        descricao: 'Descricao',
        urlReferencia: mainRoutePaths.IMPORTAR_DADOS,
        severidade: 1,
      },
    ]),
  ),
);

beforeAll(() => {
  server.listen({ onUnhandledRequest: 'warn' });
  jest.spyOn(console, 'error').mockImplementation(() => {});
});

afterEach(() => {
  server.resetHandlers();
});

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

it('Receive notifications and redirect - mobile', async () => {
  window.history.pushState({}, '', mainRoutePaths.INICIO);
  render(
    <QueryClientProvider client={queryCacheConfig}>
      <Router>
        <Route path={mainRoutePaths.INICIO} component={MainApp} />
        <Route path={mainRoutePaths.IMPORTAR_DADOS} component={ImportData} />
      </Router>
    </QueryClientProvider>,
  );

  const notificationsButton = screen.getByText(/Notificações/i);
  userEvent.click(notificationsButton);

  await waitFor(async () => {
    const accessButton = await screen.queryByText(/acessar/i);
    if (accessButton) userEvent.click(accessButton);

    expect(screen.getByText(/Importação de Dados Cadastrais/i)).toBeVisible();
  });
});
