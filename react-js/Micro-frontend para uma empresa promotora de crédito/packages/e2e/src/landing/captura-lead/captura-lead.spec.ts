import { expect, test } from '@playwright/test';

import MOCK_PRODUTOS from './mocks/produtos.json';
import { fillTipoProduto } from './actions';

import { mockRouteResponse } from '../../utils/request';
import { assertRequestPayload, assertResponse } from '../../utils/asserts';

test.describe('lead creation', () => {
  const LEAD_POST_PAYLOAD = {
    convenio: '',
    cpf: '',
    desejaContatoWhatsApp: false,
    email: '',
    idProduto: 1,
    latitude: null,
    longitude: null,
    origemRequisicaoCampanha: null,
    origemRequisicaoConteudo: null,
    origemRequisicaoMidia: null,
    origemRequisicaoPalavraChave: null,
    origemRequisicaoTermo: null,
    produto: '1',
    requerConvenio: true,
    telefone: '',
  };

  const LEAD_CREATED_RESPONSE = {
    retorno: {
      id: 552,
      latitude: 0,
      longitude: 0,
      linkContatoWhatsAppLoja: null,
    },
    alertas: [],
    erros: [],
  };

  test.beforeEach(async ({ page }) => {
    mockRouteResponse(page, '**/produtos', MOCK_PRODUTOS);
    mockRouteResponse(page, '**/leads', LEAD_CREATED_RESPONSE);
  });

  test('with geolocation', async ({ page, context }) => {
    const GEOLOCATION_MOCK = { longitude: 29.979097, latitude: 31.134256 };
    await context.grantPermissions(['geolocation']);
    await context.setGeolocation(GEOLOCATION_MOCK);

    await page.goto(process.env.LANDING_URL);

    const [req] = await Promise.all([
      page.waitForRequest('**/leads'),
      fillTipoProduto(page),
    ]);

    assertRequestPayload(req, { ...LEAD_POST_PAYLOAD, ...GEOLOCATION_MOCK });
    await assertResponse(req, LEAD_CREATED_RESPONSE);
  });

  test('without geolocation', async ({ page }) => {
    await page.goto(process.env.LANDING_URL);

    const [req] = await Promise.all([
      page.waitForRequest('**/leads'),
      fillTipoProduto(page),
    ]);

    assertRequestPayload(req, LEAD_POST_PAYLOAD);
    await assertResponse(req, LEAD_CREATED_RESPONSE);
  });

  test('with campanha params', async ({ page }) => {
    const CAMPANHA_PARAMS = {
      origemRequisicaoCampanha: 'webconversion',
      origemRequisicaoMidia: 'cpc',
      origemRequisicaoPalavraChave: 'abc',
      origemRequisicaoTermo: '123',
    };

    await page.goto(
      `${process.env.LANDING_URL}?utm_source=criteo&utm_medium=cpc&utm_campaign=webconversion&keyword=abc&utm_term=123`,
    );

    const [req] = await Promise.all([
      page.waitForRequest('**/leads'),
      fillTipoProduto(page),
    ]);

    assertRequestPayload(req, { ...LEAD_POST_PAYLOAD, ...CAMPANHA_PARAMS });
    await assertResponse(req, LEAD_CREATED_RESPONSE);
  });

  test('with error', async ({ page }) => {
    const ERROR_MESSAGE = 'Erro teste!';

    page.unroute('**/leads');
    mockRouteResponse(
      page,
      '**/leads',
      { erros: [{ mensagem: ERROR_MESSAGE, tipo: 1 }] },
      400,
    );

    await page.goto(process.env.LANDING_URL);

    await Promise.all([page.waitForRequest('**/leads'), fillTipoProduto(page)]);

    const visible = await page.isVisible(`text=${ERROR_MESSAGE}`);
    expect(visible).toBeTruthy();
  });
});
