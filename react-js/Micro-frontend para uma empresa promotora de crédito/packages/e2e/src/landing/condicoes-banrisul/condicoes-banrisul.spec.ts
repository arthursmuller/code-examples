import { expect, test } from '@playwright/test';
import { useNetworkRecordMocks } from 'playwright-request-mocker';

test.describe('Condicoes banrisul', () => {
  test('should load pdf', async ({ page }) => {
    await useNetworkRecordMocks(page, {
      recordRoute: `${process.env.LANDING_URL}/condicoes-gerais-banrisul`,
    });

    await page.goto(`${process.env.LANDING_URL}/condicoes-gerais-banrisul`);

    await page.waitForSelector('text=Condições gerais banrisul');
    await page.waitForSelector(
      'span:has-text("CONSIGNAÇÃO EM FOLHA DE PAGAMENTO")',
    );

    expect(await page.isVisible('text=Condições gerais banrisul')).toBeTruthy();
    expect(
      await page.isVisible(
        'span:has-text("CONSIGNAÇÃO EM FOLHA DE PAGAMENTO")',
      ),
    ).toBeTruthy();
    expect(await page.isVisible('text=1 / 17')).toBeTruthy();

    await page.click('text=>');
    expect(await page.isVisible('text=2 / 17')).toBeTruthy();

    await page.waitForSelector('text=Firmam');
    expect(await page.isVisible('text=Firmam')).toBeTruthy();

    await page.click('text=<');
    expect(await page.isVisible('text=1 / 17')).toBeTruthy();
  });
});
