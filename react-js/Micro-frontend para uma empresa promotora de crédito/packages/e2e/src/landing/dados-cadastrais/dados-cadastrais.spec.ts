import { test, expect } from '@playwright/test';

import { data, fillForm } from './actions';

import { mockRouteResponse } from '../../utils/request';
import { assertRequestPayload, assertResponse } from '../../utils/asserts';

test.describe('Request personal data', () => {
  const ENDPOINT = '**/dados-pessoais/solicitacao-acesso';

  const RESPONSE = {
    retorno: null,
    alertas: [],
    erros: [],
  };

  test.beforeEach(async ({ page }) => {
    mockRouteResponse(page, ENDPOINT, RESPONSE);
  });

  test('successfully', async ({ page }) => {
    await page.goto(`${process.env.LANDING_URL}/meus-dados`);

    const [req] = await Promise.all([
      page.waitForRequest(ENDPOINT),
      fillForm(page),
    ]);

    assertRequestPayload(req, data);
    await assertResponse(req, RESPONSE);
    const visible = await page.isVisible(`text=Seus dados foram solicitados`);
    expect(visible).toBeTruthy();
  });

  test('with error', async ({ page }) => {
    const ERROR_MESSAGE = 'Erro teste!';

    page.unroute(ENDPOINT);
    mockRouteResponse(
      page,
      ENDPOINT,
      { erros: [{ mensagem: ERROR_MESSAGE, tipo: 1 }] },
      400,
    );

    await page.goto(`${process.env.LANDING_URL}/meus-dados`);

    await Promise.all([page.waitForRequest(ENDPOINT), fillForm(page)]);

    const visible = await page.isVisible(`text=${ERROR_MESSAGE}`);
    expect(visible).toBeTruthy();
  });
});
