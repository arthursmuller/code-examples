import {
  GoogleLoginResponse,
  GoogleLoginResponseOffline,
} from 'react-google-login';

export * from './use-facebook-login';

export * from './login-stepper';
export * from './login-dialog';
export * from './login-popover';

export const FACEBOOK_DEFAULT_CONFIG = {
  appId: '1414786278878237',
  language: 'en_US',
  version: '3.1',
  fields: ['id', 'email', 'name'],
};

export type { GoogleLoginResponse, GoogleLoginResponseOffline };
