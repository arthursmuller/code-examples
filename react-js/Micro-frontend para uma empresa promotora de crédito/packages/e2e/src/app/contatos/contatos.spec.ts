import { expect, test } from '@playwright/test';
import { useNetworkRecordMocks } from 'playwright-request-mocker';

import { directLogin } from '../utils';
import { mockRouteResponse } from '../../utils/request';
import { assertRequestPayload } from '../../utils';

const TELEFONES_ENDPOINT = '**/clientes/autenticado/telefones';

test.describe('Contatos', () => {
  test('add a new phone', async ({ page }) => {
    await useNetworkRecordMocks(page, {
      recordRoute: `${process.env.APP_URL}/telefones`,
      identifier: 'add-main-phone',
    });

    await directLogin(page, true);
    await page.pause();
    await page.goto(`${process.env.APP_URL}/perfil/contatos`);

    await page.click('button:has-text("Cadastre o seu telefone principal")');
    await page.fill('input[name="phone"]', '51996124054');

    await mockRouteResponse(
      page,
      TELEFONES_ENDPOINT,
      {
        retorno: [
          {
            id: 95,
            idCliente: 2,
            ddd: '51',
            fone: '996124054',
            principal: true,
            tipoTelefone: 2,
          },
          {
            id: 0,
            idCliente: 0,
            ddd: null,
            fone: null,
            principal: false,
            tipoTelefone: 0,
          },
        ],
        alertas: [],
        erros: [],
      },
      200,
    );

    const [postReq] = await Promise.all([
      // POST
      page.waitForRequest((req) => {
        return req.url().includes('/telefones') && req.method() === 'POST';
      }),

      // GET
      page.waitForRequest((req) => {
        return req.url().includes('/telefones') && req.method() === 'GET';
      }),

      await page.click('button:has-text("Salvar")'),
    ]);

    assertRequestPayload(postReq, {
      ddd: '51',
      fone: '996124054',
    });

    await page.pause();

    expect(await page.isVisible('text=(51) 99612-4054')).toBeTruthy();
    expect(
      await page.isVisible('text=Cadastre o seu telefone alternativo'),
    ).toBeTruthy();
  });

  // test('add a secondary phone', async ({ page }) => {});

  // test('edit a secondary phone', async ({ page }) => {});

  // test('delete a secondary phone', async ({ page }) => {});

  // test('not possible to delete a main phone', async ({ page }) => {});

  // test('show the error dialog when occur an error', async ({ page }) => {});
});
