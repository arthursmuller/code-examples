import { FC } from 'react';
import { ChakraProvider } from '@chakra-ui/react';
import { ReactQueryDevtools } from 'react-query/devtools';

import { ModalProvider, bemTheme, useQuickToast } from '@pcf/design-system';
import { CoreProvider, queryCacheConfig } from '@pcf/core';
import { MenuContextProvider } from 'components/menu-bar';

import { AuthContextProvider } from './auth/auth.context';
import { FeatureToggleContextProvider } from './feature-toggle';

const AppProviders: FC = ({ children }) => {
  const toast = useQuickToast();

  return (
    <CoreProvider
      baseUrl={process.env.REACT_APP_PLATAFORMA_CLIENTE_API || ''}
      onError={(message) => toast(message)}
    >
      <ReactQueryDevtools position="bottom-right" />

      <AuthContextProvider>
        <ModalProvider>
          <FeatureToggleContextProvider>
            <MenuContextProvider>{children}</MenuContextProvider>
          </FeatureToggleContextProvider>
        </ModalProvider>
      </AuthContextProvider>
    </CoreProvider>
  );
};

const AppProvidersWithTheme: FC = ({ children }) => (
  <ChakraProvider resetCSS theme={bemTheme}>
    <AppProviders>{children}</AppProviders>
  </ChakraProvider>
);

export { AppProvidersWithTheme as default, queryCacheConfig };
