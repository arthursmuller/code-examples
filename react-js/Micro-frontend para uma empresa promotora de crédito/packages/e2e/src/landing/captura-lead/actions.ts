import { Page } from '@playwright/test';

export const fillTipoProduto = async (page: Page): Promise<void> => {
  // Click text=Simule sua PropostaQual produto você deseja?Tipo de produto >> input
  await page.click(
    'text=Simule sua PropostaQual produto você deseja?Tipo de produto >> input',
  );
  // Click :nth-match(button:has-text("Crédito Consignado"), 2)
  await page.click(':nth-match(button:has-text("Crédito Consignado"), 2)');
};
