import { expect, test } from '@playwright/test';
import { useNetworkRecordMocks } from 'playwright-request-mocker';

import { data, dataRefin, fillPortForm } from './actions';

import { directLogin } from '../utils';
import { mockRouteResponse } from '../../utils/request';
import { assertRequestPayload } from '../../utils/asserts';

const INTENCAO_OPERACAO_ENDPOINT = '**/intencoes-operacao';

test.describe('Portabilidade', () => {
  test.beforeEach(async ({ page }) => {
    await useNetworkRecordMocks(page, {
      recordRoute: `${process.env.APP_URL}/portabilidade`,
    });

    await directLogin(page, true);
    await page.goto(`${process.env.APP_URL}/portabilidade`);
  });

  test('without refin', async ({ page }) => {
    const [req] = await Promise.all([
      page.waitForRequest(INTENCAO_OPERACAO_ENDPOINT),
      fillPortForm(page),
    ]);

    assertRequestPayload(req, data);
    const visible = await page.isVisible(
      `text=Sua portabilidade foi enviada com sucesso!`,
    );
    expect(visible).toBeTruthy();

    expect(await page.isVisible('text=1.52%')).toBeTruthy();
    expect(await page.isVisible('text=R$ 201,43')).toBeTruthy();
    expect(await page.isVisible('text=19.85%')).toBeTruthy();
    expect(await page.isVisible('text=1.5%')).toBeTruthy();
    expect(await page.isVisible('text=R$ 5.000,00')).toBeTruthy();
    expect(await page.isVisible('text=R$ 5.037,35')).toBeTruthy();
  });

  test('with refin', async ({ page }) => {
    const [req] = await Promise.all([
      page.waitForRequest(INTENCAO_OPERACAO_ENDPOINT),
      fillPortForm(page, true),
    ]);

    assertRequestPayload(req, dataRefin);
    const visible = await page.isVisible(
      `text=Sua portabilidade foi enviada com sucesso!`,
    );
    expect(visible).toBeTruthy();
  });

  test('show the error dialog when no results for simulation', async ({
    page,
  }) => {
    const errorMessage = 'Margem...';

    page.unroute(INTENCAO_OPERACAO_ENDPOINT);
    mockRouteResponse(
      page,
      INTENCAO_OPERACAO_ENDPOINT,
      { erros: [{ mensagem: errorMessage, tipo: 1 }] },
      400,
    );

    await Promise.all([
      page.waitForRequest(INTENCAO_OPERACAO_ENDPOINT),
      fillPortForm(page),
    ]);

    await page.waitForSelector(`text=${errorMessage}`);
    const visible = await page.isVisible(`text=${errorMessage}`);
    expect(visible).toBeTruthy();
  });
});
