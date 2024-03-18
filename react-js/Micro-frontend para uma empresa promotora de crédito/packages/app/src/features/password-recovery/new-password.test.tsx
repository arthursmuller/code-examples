import { rest } from 'msw';
import { setupServer } from 'msw/node';
import {
  render,
  screen,
  waitFor,
  waitForElementToBeRemoved,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { BrowserRouter as Router, Route } from 'react-router-dom';

import { UsuarioRecuperacaoSenhaRequisicao } from '@pcf/core';
import { AuthContext } from 'app/auth/auth.context';
import { pcfApi } from 'setupTests';
import { App } from 'app';
import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';
import NewPassword from 'features/password-recovery/new-password';

const Mocks = {
  validToken: 'valid_token',
  invalidToken: 'not_valid',
};

jest.mock('containers/components/page', () => ({
  __esModule: true,
  default: () => null,
  Page: ({ children }) => <div>{children}</div>,
}));

const spyNewPasswordReqBody = jest.fn();
const onLoginSuccessMock = jest.fn();

const server = setupServer();

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

it('new password success flow', async () => {
  server.use(
    rest.get(
      pcfApi(`usuarios/recuperacao-senha/${Mocks.validToken}`),
      (req, res, ctx) => {
        return res(
          ctx.status(200),
          ctx.json({
            retorno: true,
            alertas: [],
            erros: [],
          }),
        );
      },
    ),
    rest.post<UsuarioRecuperacaoSenhaRequisicao>(
      pcfApi(`usuarios/recuperacao-senha/${Mocks.validToken}`),
      (req, res, ctx) => {
        spyNewPasswordReqBody(req.body);
        return res(
          ctx.json({
            retorno: {
              nome: 'Fagner',
              email: 'teste@gmail.com',
              token:
                'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c',
            },
            alertas: [],
            erros: [],
          }),
          ctx.status(200),
        );
      },
    ),
  );

  window.history.pushState({}, '', `/recuperacao-senha/${Mocks.validToken}`);
  render(
    <Router>
      <AuthContext.Provider
        value={{
          onLoginSuccess: onLoginSuccessMock,
          isAuthenticated: false,
          onLogout: jest.fn(),
          currentLoginEmail: '',
        }}
      >
        <Route path={PublicRoutesEnum.NewPassword} component={NewPassword} />
      </AuthContext.Provider>
    </Router>,
  );

  await waitForElementToBeRemoved(() => screen.queryByTestId('loader'));

  userEvent.type(screen.getByLabelText('Crie sua senha'), '123456789Ae');
  userEvent.type(screen.getByLabelText('Repita sua senha'), '123456789Ae');

  const nextButton = screen.getByRole('button', { name: /Redefinir senha/i });

  await waitFor(() => {
    expect(nextButton).not.toBeDisabled();
  });

  userEvent.click(nextButton);

  await waitFor(() => {
    expect(spyNewPasswordReqBody).toHaveBeenCalledWith({
      novaSenha: '123456789Ae',
    });
    expect(
      screen.getByRole('heading', {
        name: /pronto! sua senha foi redefinida\./i,
      }),
    ).toBeVisible();
  });

  userEvent.click(screen.getByRole('button', { name: /acessar minha conta/i }));

  await waitFor(() => {
    expect(onLoginSuccessMock).toHaveBeenCalled();
  });
});

it('redirect to /recuperao-senha if token is invalid', async () => {
  server.use(
    rest.get(
      pcfApi(`usuarios/recuperacao-senha/${Mocks.invalidToken}`),
      (req, res, ctx) => {
        return res(
          ctx.status(200),
          ctx.json({
            retorno: false,
            alertas: [],
            erros: [{ codigo: 0, mensagem: 'Invalid Token!', tipo: 3 }],
          }),
        );
      },
    ),
  );

  window.history.pushState({}, '', `/recuperacao-senha/${Mocks.invalidToken}`);
  render(<App />);

  await waitFor(() => {
    expect(screen.getByText(/recuperação de senha/i)).toBeVisible();
  });
});

it('redirect to /recuperao-senha if get an http error', async () => {
  server.use(
    rest.get(
      pcfApi(`usuarios/recuperacao-senha/${Mocks.invalidToken}`),
      (req, res, ctx) => {
        return res(ctx.status(400));
      },
    ),
  );

  window.history.pushState({}, '', `/recuperacao-senha/${Mocks.invalidToken}`);
  render(<App />);

  await waitFor(() => {
    expect(console.error).toHaveBeenCalledTimes(1);
    expect(screen.getByText(/recuperação de senha/i)).toBeVisible();
  });
});
