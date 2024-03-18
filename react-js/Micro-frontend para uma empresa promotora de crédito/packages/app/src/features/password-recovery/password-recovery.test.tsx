import { rest } from 'msw';
import { setupServer } from 'msw/node';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

import { UsuarioRecuperacaoSenhaRequisicao } from '@pcf/core';
import { pcfApi } from 'setupTests';
import { App } from 'app';
import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';

const Mocks = {
  registeredEmail: 'registered_email@gmail.com',
  notRegisteredEmail: 'not_registered@gmail.com',
  notRegisteredEmailMessage: 'Usuário não encontrado',
};

jest.mock('containers/components/page', () => ({
  __esModule: true,
  default: () => null,
  Page: ({ children }) => <div>{children}</div>,
}));

const server = setupServer(
  rest.post<UsuarioRecuperacaoSenhaRequisicao>(
    pcfApi('/usuarios/recuperacao-senha'),
    (req, res, ctx) => {
      if (req?.body?.email === Mocks.registeredEmail) {
        return res(ctx.status(204));
      }

      return res(
        ctx.status(400),
        ctx.json({
          retorno: false,
          alertas: [],
          erros: [
            { codigo: 0, mensagem: Mocks.notRegisteredEmailMessage, tipo: 3 },
          ],
        }),
      );
    },
  ),
);

beforeAll(() => {
  server.listen();
  jest.spyOn(console, 'error').mockImplementation(() => {});
});

afterEach(() => {
  server.resetHandlers();
});

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

beforeEach(() => {
  window.history.pushState({}, '', PublicRoutesEnum.PasswordRecovery);

  render(<App />);
});

test('should be possible request password recovery when the user fills a registered e-mail', async () => {
  userEvent.type(await screen.findByLabelText('E-mail'), Mocks.registeredEmail);
  userEvent.click(await screen.findByRole('button', { name: /continuar/i }));

  await waitFor(() => {
    expect(screen.getByText(/sucesso/i)).toBeVisible();
  });
});

test('should be show an error message when the user fills with an unregistered e-mail', async () => {
  userEvent.type(
    await screen.findByLabelText('E-mail'),
    Mocks.notRegisteredEmail,
  );
  userEvent.click(await screen.findByRole('button', { name: /continuar/i }));

  await waitFor(() => {
    const [, alert] = screen.getAllByRole('alert');
    expect(alert).toHaveTextContent(Mocks.notRegisteredEmailMessage);
    expect(console.error).toHaveBeenCalledTimes(2);
  });
});

test('should display an error boundary when the api returns an unknow error', async () => {
  server.use(
    rest.post(pcfApi('/usuarios/recuperacao-senha'), (req, res, ctx) => {
      return res(ctx.status(500));
    }),
  );

  userEvent.type(
    await screen.findByLabelText('E-mail'),
    Mocks.notRegisteredEmail,
  );

  userEvent.click(await screen.findByRole('button', { name: /continuar/i }));

  await waitFor(() => {
    expect(
      screen.getByText(/Ops! Não conseguimos carregar esses dados/i),
    ).toBeVisible();
    expect(console.error).toHaveBeenCalledTimes(4);
  });

  userEvent.click(screen.getByRole('button', { name: /recarregar/i }));

  await waitFor(() => {
    expect(
      screen.getByRole('heading', { name: /Recuperação de senha/i }),
    ).toBeVisible();
  });
});
