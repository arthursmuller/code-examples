import { Page } from '@playwright/test';

export const fillSimularNovo = async (page: Page): Promise<void> => {
  // Click label[role="checkbox"]:has-text("Valor da parcelaVocê informa o valor que deseja pagar por mês.")
  await page.click(
    'label[role="checkbox"]:has-text("Valor da parcelaVocê informa o valor que deseja pagar por mês.")',
  );
  // Click input[name="value"]
  await page.click('input[name="value"]');
  // Fill input[name="value"]
  await page.fill('input[name="value"]', 'R$ 1.000,000');
  // Click text=Continuar
  await page.click('text=Continuar');
  // Click label[role="checkbox"]:has-text("Matrícula 1234567893 | INSSBanco:0041 - BANCO DO ESTADO DO RIO GRANDE DO SUL SAT")
  await page.click(
    'label[role="checkbox"]:has-text("Matrícula 1234567893 | INSSBanco:0041 - BANCO DO ESTADO DO RIO GRANDE DO SUL SAT")',
  );
  // Click text=Simular meu consignado
  await page.click('text=Simular meu consignado');
  // Click label[role="checkbox"]:has-text("Prazo 84 mesesValor da proposta:R$ 412.507,97Taxa:1.8%Ver mais")
  await page.click(
    'label[role="checkbox"]:has-text("Prazo 84 mesesValor da proposta:R$ 412.507,97Taxa:1.8%Ver mais")',
  );
  // Click text=Continuar
  await page.click('text=Continuar');
};
