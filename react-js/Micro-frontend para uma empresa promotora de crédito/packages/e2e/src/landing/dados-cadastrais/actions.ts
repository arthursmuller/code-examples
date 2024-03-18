import { Page } from '@playwright/test';

export const fillForm = async (page: Page): Promise<void> => {
  // Click input[name="nome"]
  await page.click('input[name="nome"]');
  // Fill input[name="nome"]
  await page.fill('input[name="nome"]', 'My');
  // Press Tab
  await page.press('input[name="nome"]', 'Tab');
  // Fill input[name="sobrenome"]
  await page.fill('input[name="sobrenome"]', 'Test');
  // Press Tab
  await page.press('input[name="sobrenome"]', 'Tab');
  // Fill input[name="dataNascimento"]
  await page.fill('input[name="dataNascimento"]', '07/07/1994');
  // Press Tab
  await page.press('input[name="dataNascimento"]', 'Tab');
  // Fill input[name="nomeMae"]
  await page.fill('input[name="nomeMae"]', 'Test Mae');
  // Press Tab
  await page.press('input[name="nomeMae"]', 'Tab');
  // Fill input[name="email"]
  await page.fill('input[name="email"]', 'test@test.com');
  // Press Tab
  await page.press('input[name="email"]', 'Tab');
  // Fill :nth-match(input[type="text"], 5)
  await page.fill(':nth-match(input[type="text"], 5)', '(51) 2312-3122');
  // Press Tab
  await page.press(':nth-match(input[type="text"], 5)', 'Tab');
  // Fill textarea[name="motivo"]
  await page.fill('textarea[name="motivo"]', 'Qualquer um.');
  // Click text=Solicitar dados
  await page.click('text=Solicitar dados');
};

export const data = {
  nome: 'My',
  sobrenome: 'Test',
  dataNascimento: '1994-07-07T03:00:00.000Z',
  nomeMae: 'Test Mae',
  email: 'test@test.com',
  telefoneCompleto: {
    telefone: '23123122',
    ddd: '51',
  },
  motivo: 'Qualquer um.',
};
