import { Page } from '@playwright/test';

export const fillFacebookLoginForm = async (
  page: Page,
  name: string,
  password: string,
): Promise<void> => {
  await page.click('text=Entrar com redes sociais');
  // Click text=Entrar com Facebook
  const [page1] = await Promise.all([
    page.waitForEvent('popup'),
    page.click('text=Entrar com Facebook'),
  ]);
  // Fill input[name="email"]
  await page1.fill('input[name="email"]', name);
  // Click input[name="pass"]
  await page1.click('input[name="pass"]');
  // Fill input[name="pass"]
  await page1.fill('input[name="pass"]', password);
  // Click text=Entrar
  await Promise.all([
    page1.waitForNavigation(/* { url: 'https://web.facebook.com/v3.1/dialog/oauth?app_id=1414786278878237&cbt=1629292346219&channel_url=https%3A%2F%2Fstaticxx.facebook.com%2Fx%2Fconnect%2Fxd_arbiter%2F%3Fversion%3D46%23cb%3Df36063e7354b8e%26domain%3Dlocalhost%26is_canvas%3Dfalse%26origin%3Dhttps%253A%252F%252Flocalhost%253A3000%252Ff2e45f64e4684%26relation%3Dopener&client_id=1414786278878237&display=popup&domain=localhost&e2e=%7B%7D&fallback_redirect_uri=https%3A%2F%2Flocalhost%3A3000%2Flogin&locale=en_US&logger_id=f38a3dbd7ef313&origin=1&redirect_uri=https%3A%2F%2Fstaticxx.facebook.com%2Fx%2Fconnect%2Fxd_arbiter%2F%3Fversion%3D46%23cb%3Df63904b98b63c%26domain%3Dlocalhost%26is_canvas%3Dfalse%26origin%3Dhttps%253A%252F%252Flocalhost%253A3000%252Ff2e45f64e4684%26relation%3Dopener%26frame%3Df310b29126bc668&response_type=token%2Csigned_request%2Cgraph_domain&sdk=joey&version=v3.1&ret=login&fbapp_pres=0&tp=unspecified&ext=1629295988&hash=Aea-Li0A90AvJPfsJno' } */),
    page1.click('text=Entrar'),
  ]);
};
