import { createContext, FC, useContext, useState } from 'react';

import { useQueryClient } from 'react-query';
import { useMount, useSessionStorage, useUpdateEffect } from 'react-use';

import { api, setupInterceptors, RequestUtilsSingleton } from '@pcf/core';
import {
  FACEBOOK_DEFAULT_CONFIG,
  ModalProvider,
  useFacebookLogin,
  useModal,
} from '@pcf/design-system';
import { InactivityLogin } from 'features/login/inactivity-login';

import { useJwt } from './useJwt';

export interface LoginSuccessData {
  jwt: string;
  email: string;
}

type AuthContext = {
  isAuthenticated: boolean;
  onLoginSuccess(data: LoginSuccessData): void;
  onLogout(): void;
  currentLoginEmail: string | null;
};

export const AuthContext = createContext<AuthContext>({} as AuthContext);

const EMAIL_KEY = '@plataformacliente/email';

const resolvePendingRequests = (nextToken: string): void => {
  RequestUtilsSingleton.getAll().forEach((pendingRequest) => {
    pendingRequest(nextToken);
  });
};

const AuthContextProvider: FC = ({ children }) => {
  const { jwt } = useJwt();
  const [email, setEmail] = useSessionStorage<string | null>(EMAIL_KEY);
  const [cleanupInterceptorsCb, setCleanupInterceptorsCb] =
    useState<() => void>();
  const [sessionExpired, setSessionExpired] = useState<boolean>(false);
  const { showModal, hideModal } = useModal();
  const queryCache = useQueryClient();
  const isAuthenticated = !!jwt;

  const { logout, state } = useFacebookLogin(FACEBOOK_DEFAULT_CONFIG);

  function handleSetupInterceptors(): void {
    const cleanup = setupInterceptors(api, {
      onExpiredToken: () => setSessionExpired(true),
    });

    setCleanupInterceptorsCb(() => cleanup);
  }

  useMount(() => {
    if (isAuthenticated) {
      handleSetupInterceptors();
    }
  });

  function handleLoginSuccess({
    jwt: newJwt,
    email: newEmail,
  }: LoginSuccessData): void {
    const shouldReload = email && newEmail !== email;
    const isRefresh = isAuthenticated;

    RequestUtilsSingleton.setJwt(newJwt);
    setEmail(newEmail);

    if (isRefresh) {
      resolvePendingRequests(newJwt);
      RequestUtilsSingleton.clear();
      hideModal();
    } else {
      handleSetupInterceptors();
    }

    if (shouldReload) {
      window.location.reload();
    }
  }

  function handleLogout(): void {
    if (state.isLoggedIn) {
      logout();
    }
    RequestUtilsSingleton.removeJwt();
    queryCache.clear();
    cleanupInterceptorsCb && cleanupInterceptorsCb();
    setEmail(null);
  }

  useUpdateEffect(() => {
    if (sessionExpired) {
      showModal({
        closeOnClickOverlay: false,
        modal: (
          <InactivityLogin onSuccess={handleLoginSuccess} email={email || ''} />
        ),
        customOffset: {
          top: 5,
          right: 0,
        },
      });
    }
  }, [sessionExpired]);

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        onLoginSuccess: handleLoginSuccess,
        onLogout: handleLogout,
        currentLoginEmail: email,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

function useAuthContext(): AuthContext {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error('useAuthContext must be used within AuthContextProvider');
  }

  return context;
}

const AuthContextProviderWrapped: FC = ({ children }) => (
  <ModalProvider>
    <AuthContextProvider>{children}</AuthContextProvider>
  </ModalProvider>
);

export { AuthContextProviderWrapped as AuthContextProvider, useAuthContext };
