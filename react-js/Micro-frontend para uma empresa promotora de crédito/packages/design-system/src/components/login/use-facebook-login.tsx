// https://github.com/silver-xu/react-use-fb-login

import { useState, useEffect } from 'react';

export interface FacebookUser {
  id?: string;
  name?: string;
  email?: string;
}

export enum FacebookLoginStatus {
  Connected = 'connected',
  NotAuthorized = 'not_authorized',
  Unknown = 'unknown',
}

export type FacebookLoginResponse = {
  status: FacebookLoginStatus;
  authResponse?: {
    /** An access token for the person using the webpage. */
    accessToken: string;
    /**
     * A UNIX time stamp when the token expires. Once the token expires, the person will need to login again.
     */
    expiresIn: string;
    /**
     * The amount of time before the login expires, in seconds, and the person will need to login again.
     */

    /* eslint-disable camelcase */
    reauthorize_required_in: string;
    /**
     * A signed parameter that contains information about the person using your webpage.
     */
    signedRequest: string;
    /** The ID of the person using your webpage. */
    userID: string;
  };
};

export interface FaceBookLoginState {
  isSdkLoaded: boolean;
  isProcessing: boolean;
  isLoggedIn: boolean;
  currentUser?: FacebookUser;
  loaded: boolean;
}

type FacebookFields = 'name' | 'email' | 'gender' | 'id';

export interface FaceBookLoginConfig {
  appId: string;
  language: string;
  version: string;
  fields: FacebookFields[] | string[];
  onFailure?: (response: any) => void;
  onSuccess?: (
    response: FacebookLoginResponse,
    currentUser: FacebookUser,
  ) => void;
}

const getWindow = (): any => {
  return window as any;
};

const getUserInfo = (
  config: FaceBookLoginConfig,
  state: FaceBookLoginState,
  setState: (state: FaceBookLoginState) => void,
): void => {
  getWindow().FB.api(
    '/me',
    { locale: config.language, fields: config.fields.join(',') },
    (response: any) => {
      const currentUser: FacebookUser = response;
      setState({
        ...state,
        isLoggedIn: true,
        currentUser,
        isProcessing: false,
        loaded: true,
      });
    },
  );
};

const checkLoginCallback = (
  response: FacebookLoginResponse,
  config: FaceBookLoginConfig,
  state: FaceBookLoginState,
  setState: (state: FaceBookLoginState) => void,
): void => {
  if (response.status === 'connected') {
    getUserInfo(config, state, setState);
  } else {
    setState({
      ...state,
      isLoggedIn: false,
      currentUser: undefined,
      isProcessing: false,
    });
  }
};

const setFacekbookAsyncInit = (
  config: FaceBookLoginConfig,
  state: FaceBookLoginState,
  setState: (state: FaceBookLoginState) => void,
): void => {
  getWindow().fbAsyncInit = () => {
    getWindow().FB.init({
      version: `v${config.version}`,
      appId: `${config.appId}`,
      xfbml: false,
      cookie: false,
    });

    setState({
      ...state,
      isSdkLoaded: true,
    });

    getWindow().FB.getLoginStatus((response: any) =>
      checkLoginCallback(response, config, state, setState),
    );
  };
};

const loadSdkAsynchronously = (config: FaceBookLoginConfig): void => {
  ((doc: Document, script: string, sdkId: string) => {
    const newScriptElement = doc.createElement(script) as HTMLScriptElement;

    newScriptElement.id = sdkId;
    newScriptElement.src = `https://connect.facebook.net/${config.language}/sdk.js`;
    doc.head.appendChild(newScriptElement);

    let fbRoot = doc.getElementById('fb-root');
    if (!fbRoot) {
      fbRoot = doc.createElement('div');
      fbRoot.id = 'fb-root';
      doc.body.appendChild(fbRoot);
    }
  })(document, 'script', 'facebook-jssdk');
};

const loginCallback = (
  response: FacebookLoginResponse,
  config: FaceBookLoginConfig,
  state: FaceBookLoginState,
  setState: (state: FaceBookLoginState) => void,
): void => {
  if (response.status === 'connected') {
    getWindow().FB.api(
      '/me',
      { locale: config.language, fields: config.fields.join(',') },
      (response2: any) => {
        const currentUser: FacebookUser = response2;
        setState({
          ...state,
          isLoggedIn: true,
          currentUser,
          isProcessing: false,
          loaded: true,
        });

        if (config.onSuccess) {
          config.onSuccess(response, currentUser);
        }
      },
    );
  } else if (config.onFailure) {
    config.onFailure(response);
    setState({ ...state, isProcessing: false });
  }
};

const logoutCallback = (
  response: any,
  config: FaceBookLoginConfig,
  state: FaceBookLoginState,
  setState: (state: FaceBookLoginState) => void,
): void => {
  if (response.authResponse) {
    setState({
      ...state,
      isLoggedIn: false,
      currentUser: undefined,
      isProcessing: false,
    });
  } else if (config.onFailure) {
    config.onFailure(response);
  }
};

export const useFacebookLogin = (
  config: FaceBookLoginConfig,
): { state: FaceBookLoginState; login: () => void; logout: () => void } => {
  const [state, setState] = useState<FaceBookLoginState>({
    isSdkLoaded: false,
    isProcessing: false,
    isLoggedIn: false,
    loaded: false,
  });

  const login = (): void => {
    setState({ ...state, isProcessing: true });
    getWindow().FB.login(
      (response: any) => loginCallback(response, config, state, setState),
      { scope: 'public_profile,email' },
    );
  };

  const logout = (): void => {
    setState({ ...state, isProcessing: true });
    getWindow().FB.logout((response: any) =>
      logoutCallback(response, config, state, setState),
    );
  };

  useEffect(() => {
    setState({
      ...state,
      isProcessing: true,
    });

    if (!getWindow().FB) {
      setFacekbookAsyncInit(config, state, setState);
      loadSdkAsynchronously(config);
      setState({
        ...state,
        isProcessing: false,
      });
    } else {
      setState({
        isSdkLoaded: true,
        isLoggedIn: true,
        loaded: true,
        isProcessing: false,
      });
    }
  }, [config.appId, config.fields.join(','), config.language, config.version]);

  return { state, login, logout };
};
