// import { test } from '@playwright/test';
// import { useNetworkRecordMocks } from 'playwright-request-mocker';

// import { fillFacebookLoginForm } from './actions';

// test.describe('Login with facebook', () => {
//   test.beforeEach(async ({ page }) => {
//     await useNetworkRecordMocks(page, {
//       recordRoute: `${process.env.APP_URL_HTTPS}`,
//     });
//   });

//   test.only('success login', async ({ page }) => {
//     await page.goto(`${process.env.APP_URL_HTTPS}`);

//     await fillFacebookLoginForm(
//       page,
//       process.env.FACEBOOK_EMAIL,
//       process.env.FACEBOOK_PASSWORD,
//     );

//     await page.pause();
//   });
// });
