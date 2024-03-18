import { FC, useMemo, useCallback } from 'react';

import { useRouter } from 'next/router';

import {
  LoginStepper,
  LoginPopover,
  GoogleLoginResponse,
  FacebookLoginResponse,
  useFacebookLogin,
  FACEBOOK_DEFAULT_CONFIG,
  FacebookUser,
} from '@pcf/design-system';
import {
  useLogin,
  extractReadableErrorMessage,
  useLoginSocial,
  RedeSocialEnum,
} from '@pcf/core';
import { ProxyRoutesEnum, RoutesEnum } from 'app/routes/routes.enum';

import { useFeatureFlag } from '../../app';

interface LoginProps {
  isPopover?: boolean;
  onPrevious?: () => void;
  onSuccess?: () => void;
}

export const Login: FC<LoginProps> = ({ isPopover, onPrevious, onSuccess }) => {
  const router = useRouter();
  const flags = useFeatureFlag();

  const { mutate, isLoading, error } = useLogin({
    onError: () => {},
  });
  const { mutate: mutateGoogleLogin } = useLoginSocial(RedeSocialEnum.Google, {
    onError: () => {},
  });
  const { mutate: mutateAppleLogin } = useLoginSocial(RedeSocialEnum.Apple, {
    onError: () => {},
  });
  const { mutate: mutateFacebookLogin } = useLoginSocial(
    RedeSocialEnum.Facebook,
    {
      onError: () => {},
    },
  );

  function onSubmit({ password, email }): void {
    mutate(
      { email, senha: password },
      {
        onSuccess(data) {
          onSuccess && onSuccess();

          const { token } = data;

          router.push({
            pathname: ProxyRoutesEnum.App,
            query: { re: token, for: email },
          });
        },
      },
    );
  }

  function handleGoogleLoginSuccess(response: GoogleLoginResponse): void {
    mutateGoogleLogin(
      { token: response.tokenId },
      {
        onSuccess(apiResponse) {
          if (apiResponse.usuarioCadastrado) {
            router.push({
              pathname: ProxyRoutesEnum.App,
              query: { re: apiResponse.token, for: apiResponse.email },
            });
          } else {
            router.push(
              `${ProxyRoutesEnum.SignUp}?socialMedia=${RedeSocialEnum.Google}&token=${response.tokenId}`,
            );
          }
        },
      },
    );
  }

  function handleAppleLoginSuccess(response: any): void {
    mutateAppleLogin(
      {
        token: response?.authorization?.id_token,
        code: response?.authorization?.code,
        redirectURL: window.location.href,
      },
      {
        onSuccess(apiResponse) {
          if (apiResponse.usuarioCadastrado) {
            router.push({
              pathname: ProxyRoutesEnum.App,
              query: { re: apiResponse.token, for: apiResponse.email },
            });
          } else {
            router.push(
              `${ProxyRoutesEnum.SignUp}?socialMedia=${RedeSocialEnum.Apple}&token=${response?.authorization?.id_token}&email=${apiResponse?.email}`,
            );
          }
        },
      },
    );
  }

  const handleFacebookLoginSuccess = useCallback(
    (response: FacebookLoginResponse, currentUser: FacebookUser) => {
      mutateFacebookLogin(
        { token: response?.authResponse.accessToken },
        {
          onSuccess(apiResponse) {
            if (apiResponse.usuarioCadastrado) {
              router.push({
                pathname: ProxyRoutesEnum.App,
                query: { re: apiResponse.token, for: apiResponse.email },
              });
            } else {
              router.push(
                `${ProxyRoutesEnum.SignUp}?socialMedia=${RedeSocialEnum.Facebook}&email=${currentUser?.email}&name=${currentUser?.name}&token=${response?.authResponse.accessToken}`,
              );
            }
          },
        },
      );
    },
    [],
  );

  const facebookConfig = useMemo(
    () => ({
      ...FACEBOOK_DEFAULT_CONFIG,
      onFailure: (error) => {
        console.log(error);
      },
      onSuccess: handleFacebookLoginSuccess,
    }),
    [],
  );

  const { login: onFacebookLogin } = useFacebookLogin(facebookConfig);
  function handleCreateAccountClick(): void {
    router.push(`${ProxyRoutesEnum.SignUp}`);
  }

  const navigateToRecoverPassword = (): void => {
    router.push({
      pathname: ProxyRoutesEnum.PasswordRecovery,
    });
  };

  const errorMessage = useMemo(
    () => extractReadableErrorMessage(error, { showFallbackMessage: false }),
    [error],
  );

  return isPopover ? (
    <LoginPopover
      onAppleLoginSuccess={handleAppleLoginSuccess}
      onCreateAccountClick={handleCreateAccountClick}
      showCreateAccountButton={flags.CRIAR_CONTA}
      showSocialMediaLoginButton={flags.LOGIN_SOCIAL}
      onFacebookLoginSuccess={onFacebookLogin}
      onSubmit={onSubmit}
      error={errorMessage}
      isSubmitting={isLoading}
      navigateToRecoverPassword={navigateToRecoverPassword}
      onGoogleLoginSuccess={handleGoogleLoginSuccess}
    />
  ) : (
    <LoginStepper
      onAppleLoginSuccess={handleAppleLoginSuccess}
      onCreateAccountClick={handleCreateAccountClick}
      showCreateAccountButton={flags.CRIAR_CONTA}
      showSocialMediaLoginButton={flags.LOGIN_SOCIAL}
      onFacebookLoginSuccess={onFacebookLogin}
      onPrevious={() =>
        onPrevious ? onPrevious() : router.push(RoutesEnum.Root)
      }
      onGoogleLoginSuccess={handleGoogleLoginSuccess}
      onSubmit={onSubmit}
      error={errorMessage}
      isSubmitting={isLoading}
      navigateToRecoverPassword={navigateToRecoverPassword}
    />
  );
};
