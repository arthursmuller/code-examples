import { expect, test } from '@playwright/test';

import MOCK_AUTENTICADO from './mocks/autenticado.json';
import MOCK_FEATURE_FLAGS from './mocks/feature-flags.json';
import MOCK_INTENCOES_OPERACAO from './mocks/intencoes-operacao.json';
import MOCK_LOGIN from './mocks/login.json';
import MOCK_NOFICACOES from './mocks/notificacoes.json';
import MOCK_RENDIMENTOS from './mocks/rendimentos.json';

import { mockRouteResponse } from '../../../utils/request';

test.describe('Contratos section', () => {
  test.beforeEach(async ({ page }) => {
    mockRouteResponse(page, '**/autenticado', MOCK_AUTENTICADO);
    mockRouteResponse(page, '**/feature-flags', MOCK_FEATURE_FLAGS);
    mockRouteResponse(page, '**/intencoes-operacao', MOCK_INTENCOES_OPERACAO);
    mockRouteResponse(page, '**/login', MOCK_LOGIN);
    mockRouteResponse(page, '**/notificacoes', MOCK_NOFICACOES);
    mockRouteResponse(page, '**/rendimentos', MOCK_RENDIMENTOS);
    mockRouteResponse(
      page,
      '**/informacao-produto-operacao/refin',
      MOCK_RENDIMENTOS,
    );

    await page.goto(process.env.APP_URL);

    await page.click('text=Entrar com e-mail e senha');

    await page.click('input[name="email"]');
    await page.fill('input[name="email"]', 'fagnerschwalm@gmail.com');
    await page.fill('input[name="password"]', '');
    await page.fill('input[name="password"]', 'secret_password');

    await Promise.all([
      page.waitForNavigation({
        url: 'http://localhost:3000/inicio',
        waitUntil: 'networkidle',
      }),
      page.click('text=Acessar minha conta'),
    ]);
  });

  test('show the alert when exists', async ({ page }) => {
    const CONTRACTS_ALERT = {
      retorno: null,
      alertas: [
        {
          codigo: 0,
          mensagem:
            'Ainda não há registro de que o cliente tenha autorizado a importação de seus dados.',
          tipo: 1,
        },
      ],
      erros: [],
    };

    mockRouteResponse(page, '**/contratos', CONTRACTS_ALERT);

    await page.waitForSelector(`text=${CONTRACTS_ALERT.alertas[0].mensagem}`);
    const visible = await page.isVisible(
      `text=${CONTRACTS_ALERT.alertas[0].mensagem}`,
    );
    expect(visible).toBeTruthy();
  });

  test('show an error', async ({ page }) => {
    const ERROR_MESSAGE = 'Erro teste!';

    mockRouteResponse(
      page,
      '**/contratos',
      { erros: [{ mensagem: ERROR_MESSAGE, tipo: 1 }] },
      400,
    );

    await page.waitForSelector(`text=${ERROR_MESSAGE}`);
    const visible = await page.isVisible(`text=${ERROR_MESSAGE}`);
    expect(visible).toBeTruthy();
  });
});
