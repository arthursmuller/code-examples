import { expect, test } from '@playwright/test';
import { useNetworkRecordMocks } from 'playwright-request-mocker';

import { fillSimularNovo } from './actions';

import { directLogin } from '../utils';

test.describe('Simulacação novo - ', () => {
  test.beforeEach(async ({ page }) => {
    await useNetworkRecordMocks(page, {
      recordRoute: `${process.env.APP_URL}/simular-novo-consignado`,
    });
    await directLogin(page, true);
    await page.goto(`${process.env.APP_URL}/simular-novo-consignado`);
  });

  test('preenchimento da proposta', async ({ page }) => {
    await fillSimularNovo(page);

    const successMessage = await page.isVisible(
      `text=A sua Solicitação de Proposta foi enviada com sucesso. Agora, é so aguardar`,
    );
    expect(successMessage).toBeTruthy();

    await page.click('text=Fechar');
    expect(page.url().includes('inicio')).toBeTruthy();
  });
});

test.describe('Simulacação novo (INCLUSAO_PROPOSTA_NOVA) -', () => {
  const SUCCESS_MODAL_MESSAGE =
    'Deseja prosseguir com a sua operação de crédito?';

  test.beforeEach(async ({ page }) => {
    await useNetworkRecordMocks(page, {
      recordRoute: `${process.env.APP_URL}/simular-novo-consignado`,
      identifier: 'INCLUSAO_PROPOSTA_NOVA',
      overrideResponses: {
        '/feature-flags': {
          retorno: [
            { habilitado: true, chave: 'TEST' },
            { habilitado: true, chave: 'NEXT_PUBLIC_FEATURE_ACESSAR_CONTA' },
            { habilitado: true, chave: 'ACESSAR_CONTA' },
            { habilitado: true, chave: 'SIMULAR_NOVO' },
            { habilitado: true, chave: 'WHATSAPP' },
            { habilitado: true, chave: 'CRIAR_CONTA' },
            { habilitado: true, chave: 'LOGIN_SOCIAL' },
            { habilitado: true, chave: 'PORTABILIDADE' },
            { habilitado: true, chave: 'CARTAO' },
            { habilitado: true, chave: 'REFIN' },
            { habilitado: true, chave: 'INCLUSAO_PROPOSTA_NOVA' },
          ],
          alertas: [],
          erros: [],
        },
      },
    });
    await directLogin(page, true);
    await page.goto(`${process.env.APP_URL}/simular-novo-consignado`);
  });

  test('NÃO prosseguir inclusão automática', async ({ page }) => {
    await fillSimularNovo(page);

    expect(await page.isVisible(`text=${SUCCESS_MODAL_MESSAGE}`)).toBeTruthy();

    await page.click('text=Não, quero orientação');
    expect(await page.isVisible('text=Obrigada!')).toBeTruthy();

    await page.click('#full-layout-card >> text=Voltar');
    expect(page.url().includes('inicio')).toBeTruthy();
  });

  test('prosseguir inclusão automática', async ({ page }) => {
    await fillSimularNovo(page);

    expect(await page.isVisible(`text=${SUCCESS_MODAL_MESSAGE}`)).toBeTruthy();

    await page.click('text=Sim, quero continuar');
    expect(await page.isVisible('text=Dados Básicos')).toBeTruthy();
  });
});
