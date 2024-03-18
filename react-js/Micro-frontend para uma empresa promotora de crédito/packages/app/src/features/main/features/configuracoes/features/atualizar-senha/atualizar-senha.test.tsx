import { rest } from 'msw';
import { setupServer } from 'msw/node';
import {
  render,
  screen,
  waitFor,
  waitForElementToBeRemoved,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';

import { trocarSenhaRoute } from '@pcf/core';
import { ModalProvider } from '@pcf/design-system';
import { pcfApi } from 'setupTests';
import { NavBar } from 'features/main/components';

import { configNavigation } from '../../configuracoes.test';

const spy = jest.fn();
const server = setupServer();

beforeAll(() => {
  server.listen({ onUnhandledRequest: 'warn' });
});

afterEach(() => {
  server.resetHandlers();
});

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

xtest('Update password success flow', async () => {
  server.use(
    rest.put(pcfApi(trocarSenhaRoute), (req, res, ctx) => {
      spy(req.body);
      return res(ctx.status(200));
    }),
  );

  jest.setTimeout(30000);

  render(
    <ModalProvider>
      <NavBar />
    </ModalProvider>,
  );
  configNavigation(/redefinir minha senha/i);

  await waitForElementToBeRemoved(() => screen.queryByTestId('loader'));

  userEvent.type(
    screen.getByLabelText('Insira sua senha atual'),
    '123456789Ae',
  );
  userEvent.type(screen.getByLabelText('Crie sua senha'), '123456789Ae');
  userEvent.type(screen.getByLabelText('Repita sua senha'), '123456789Ae');

  const saveButton = screen.getByRole('button', { name: /salvar nova senha/i });

  await waitFor(() => {
    expect(saveButton).not.toBeDisabled();
  });

  userEvent.click(saveButton);

  await waitFor(() => {
    expect(spy).toHaveBeenCalledWith({
      novaSenha: '123456789Ae',
      senhaAtual: '123456789Ae',
    });

    expect(
      screen.getByText(/Sua senha foi redefinida com sucesso!/i),
    ).toBeVisible();
  });

  const sectionTitle = screen.getByText(/Redefinição de senha/i);
  const closeButton = screen.getByRole('button', { name: /fechar/i });
  userEvent.click(closeButton);

  expect(sectionTitle).not.toBeInTheDocument();
});
