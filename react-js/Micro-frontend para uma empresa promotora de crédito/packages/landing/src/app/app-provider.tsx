import { ComponentType, FC } from 'react';
import dynamic from 'next/dynamic';

import { ReactQueryDevtools } from 'react-query/devtools';
import { ChakraProvider } from '@chakra-ui/react';

const CookieBotLazy: ComponentType<any> = dynamic(() => import('react-cookiebot'))

import {
  TrackingUserConnection,
  BemErrorBoundary,
  ModalProvider,
  useQuickToast,
  bemTheme,
} from '@pcf/design-system';
import { MenuContextProvider } from 'features/components/menu-bar';
import { CoreProvider, queryCacheConfig } from '@pcf/core';

import { AppContextProvider } from './app.context';

const AppProvider: FC = ({ children }) => {
  const toast = useQuickToast();

  return (
    <div>
      <CoreProvider
        onError={(message) => toast('Houve um problema na requisição', message)}
      >
        {process.env.NODE_ENV === 'production' && process.env.NEXT_PUBLIC_DOMAIN_COOKIE_BOT_ID && (
          <CookieBotLazy
            domainGroupId={process.env.NEXT_PUBLIC_DOMAIN_COOKIE_BOT_ID}
          />
        )}

        <AppContextProvider>
          <ModalProvider>
            <MenuContextProvider>
              <BemErrorBoundary>{children}</BemErrorBoundary>

              <TrackingUserConnection />
              <ReactQueryDevtools position="bottom-right" />
            </MenuContextProvider>
          </ModalProvider>
        </AppContextProvider>
      </CoreProvider>
    </div>
  );
};

const AppProviderWithTheme: FC = ({ children }) => (
  <ChakraProvider resetCSS theme={bemTheme}>
    <AppProvider>{children}</AppProvider>
  </ChakraProvider>
);

export { AppProviderWithTheme as AppProvider, queryCacheConfig };
