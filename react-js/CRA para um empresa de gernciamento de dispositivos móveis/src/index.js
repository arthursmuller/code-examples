import { ChakraProvider, CSSReset } from '@chakra-ui/react';
import React from 'react';
import ReactDOM from 'react-dom';
import { IntlProvider } from 'react-intl';

import App from './App';
import { Fonts } from './font';
import i18nConfig from './locales/pt-br';
import theme from './theme';

ReactDOM.render(
  <React.StrictMode>
    <IntlProvider
      locale={i18nConfig.locale}
      defaultLocale={i18nConfig.locale}
      messages={i18nConfig.messages}
    >
      <ChakraProvider theme={theme}>
        <CSSReset />
        <Fonts />
        <App />
      </ChakraProvider>
    </IntlProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
