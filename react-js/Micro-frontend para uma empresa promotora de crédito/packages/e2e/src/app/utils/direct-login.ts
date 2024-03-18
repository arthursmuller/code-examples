import { Page } from '@playwright/test';

const endlessToken =
  'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkNvbnRyYXRvcyBUZXN0IiwibmFtZWlkIjoiMTYiLCJyb2xlIjoiIiwiVXN1YXJpb1RlbmFudCI6IiIsIm5iZiI6MTYyNzUwMTE4OCwiZXhwIjoyNjI3NTE1NTg4LCJpYXQiOjE2Mjc1MDExODh9.zK-2_xLBXElMpUfAoesvtqhMRx-X3Fdzho5hrzZO274';
const token =
  'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkZhZ25lciBTY2h3YWxtIiwibmFtZWlkIjoiMiIsInJvbGUiOiIiLCJVc3VhcmlvVGVuYW50IjoiIiwibmJmIjoxNjMxNjUxNDcxLCJleHAiOjE2MzE2NjU4NzEsImlhdCI6MTYzMTY1MTQ3MX0.rnlqc0BuUHJ62CV4CRgHstcvBzm4Ev1iVhf343jm96o';

const email = 'contratos%40test.com';

export const fillReauthDialog = async (page: Page): Promise<void> => {
  // Click input[name="password"]
  await page.click('input[name="password"]');
  // Fill input[name="password"]
  await page.fill('input[name="password"]', '1234ABcd');
  // Click text=Acessar minha conta
  await page.click('text=Acessar minha conta');

  await page.waitForRequest('**/login');
};

export const directLogin = async (
  page: Page,
  usingMocks?: boolean,
): Promise<void> => {
  await page.goto(
    `${process.env.APP_URL}/?re=${
      usingMocks ? endlessToken : token
    }&for=${email}`,
  );

  if (!usingMocks) {
    fillReauthDialog(page);
  }
};

export const setSession = async (
  page: Page,
  usingMocks?: boolean,
): Promise<void> => {
  await page.addInitScript(() => {
    sessionStorage.setItem('@plataformacliente/email', email);
    sessionStorage.setItem(
      '@plataformacliente/jwt',
      usingMocks ? endlessToken : token,
    );
  });
};
