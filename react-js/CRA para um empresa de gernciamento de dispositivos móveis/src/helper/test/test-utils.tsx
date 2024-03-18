import { ChakraProvider } from '@chakra-ui/react';
import { render, RenderOptions } from '@testing-library/react';
import React, { FC, ReactElement } from 'react';
import { IntlProvider } from 'react-intl';
import { Route, Router, Switch } from 'react-router-dom';

import i18nConfig from '../../locales/pt-br';
import { history } from '../../store/history';
import theme from '../../theme';

const AllTheProviders: FC = ({ children }) => {
  return (
    <ChakraProvider theme={theme}>
      <IntlProvider
        locale={i18nConfig.locale}
        defaultLocale={i18nConfig.locale}
        messages={i18nConfig.messages}
      >
        <Router history={history}>
          <Switch>
            <Route path="/">{children}</Route>
          </Switch>
        </Router>
      </IntlProvider>
    </ChakraProvider>
  );
};

const customRender = (
  ui: ReactElement,
  options?: Omit<RenderOptions, 'wrapper'>
) => render(ui, { wrapper: AllTheProviders, ...options });

// re-export everything
export * from '@testing-library/react';

// override render method
export { customRender as render };
