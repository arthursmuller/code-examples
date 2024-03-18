import { PlaywrightTestConfig } from '@playwright/test';

require('dotenv').config();

const config: PlaywrightTestConfig = {
  use: {
    ignoreHTTPSErrors: true,
    // Usar essa opção para finalidade de apresentação visto que cada ação será feita em 3s.
    // launchOptions: {
    //   slowMo: 3000,
    // },
  },
};
export default config;
