import { FC } from 'react';

import { Button, VStack, Icon } from '@chakra-ui/react';
import GoogleLogin, { GoogleLoginResponse } from 'react-google-login';
import AppleLogin from 'react-apple-login';

import {
  IconeFacebookIcon,
  LogoAppleIcon,
  LogoGoogleIcon,
} from '@pcf/design-system-icons';

import { useQuickToast } from '../../../toast';

export interface SocialMediaButtonProps {
  onGoogleLoginSuccess: (response: GoogleLoginResponse) => void;
  onFacebookLoginSuccess: () => void;
  onAppleLoginSuccess: (response: any) => void;
}

export const SocialMediaButtons: FC<SocialMediaButtonProps> = ({
  onGoogleLoginSuccess,
  onFacebookLoginSuccess,
  onAppleLoginSuccess,
}) => {
  const toast = useQuickToast();

  const handleGoogleLoginSuccess = (response): void => {
    onGoogleLoginSuccess(response);
  };

  const handleGoogleLoginError = (): void => {
    toast('A autenticação falhou.');
  };

  const handleAppleLoginError = (error: string): void => {
    toast('A autenticação falhou.', typeof error === 'string' ? error : '');
  };

  return (
    <VStack spacing="24px">
      <GoogleLogin
        clientId="1044056343286-lcp0epb8i025orn99ase975kgtbueked.apps.googleusercontent.com"
        onSuccess={handleGoogleLoginSuccess}
        onFailure={handleGoogleLoginError}
        cookiePolicy="single_host_origin"
        render={(renderProps) => (
          <Button
            width="100%"
            iconSpacing="8"
            justifyContent="flex-start"
            color="grey.600"
            leftIcon={<Icon as={LogoGoogleIcon} w="24px" h="24px" />}
            colorScheme="grey"
            {...renderProps}
          >
            Entrar com Google
          </Button>
        )}
      />

      <Button
        width="100%"
        iconSpacing="8"
        colorScheme="facebook"
        justifyContent="flex-start"
        color="white"
        boxShadow="soft"
        onClick={onFacebookLoginSuccess}
        leftIcon={<Icon as={IconeFacebookIcon} w="24px" h="24px" />}
      >
        Entrar com Facebook
      </Button>

      <AppleLogin
        clientId="cliente.bempromotora.com.br"
        scope="name email"
        usePopup
        callback={(response) => {
          if (response?.error) {
            if (response.error?.error !== 'popup_closed_by_user') {
              handleAppleLoginError(response.error.error);
            }
          } else {
            onAppleLoginSuccess(response);
          }
        }}
        redirectURI={window.location.href}
        render={(props) => {
          return (
            <Button
              width="100%"
              iconSpacing="8"
              color="grey.700"
              colorScheme="grey"
              justifyContent="flex-start"
              {...props}
              leftIcon={
                <Icon as={LogoAppleIcon} w="24px" h="24px" color="black" />
              }
            >
              Entrar com Apple
            </Button>
          );
        }}
      />
    </VStack>
  );
};
