import { FC, useMemo, useCallback } from 'react';

import { useHistory } from 'react-router-dom';

import {
  FacebookLoginResponse,
  FacebookUser,
  GoogleLoginResponse,
  LoginStepper,
  useFacebookLogin,
  FACEBOOK_DEFAULT_CONFIG,
} from '@pcf/design-system';
import {
  useLogin,
  extractReadableErrorMessage,
  useLoginSocial,
  RedeSocialEnum,
} from '@pcf/core';
import { PublicRoutes } from 'app/routes/public/public-routes.enum';
import { useAuthContext } from 'app/auth/auth.context';
import { useFeatureFlags } from 'app';

export const Login: FC = () => {
  const history = useHistory();
  const { mutate, isLoading, error, reset } = useLogin({
    onError: () => {},
  });

  const { onLoginSuccess, currentLoginEmail } = useAuthContext();
  const featureFlags = useFeatureFlags();

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

  const handleFacebookLoginSuccess = useCallback(
    (response: FacebookLoginResponse, currentUser: FacebookUser) => {
      mutateFacebookLogin(
        { token: response?.authResponse.accessToken },
        {
          onSuccess(apiResponse) {
            if (apiResponse.usuarioCadastrado) {
              onLoginSuccess({
                email: apiResponse.email || '',
                jwt: apiResponse.token,
              });
            } else {
              history.push(
                `${PublicRoutes.SignUp}?socialMedia=${RedeSocialEnum.Facebook}&email=${currentUser?.email}&name=${currentUser?.name}&token=${response?.authResponse.accessToken}`,
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

  const { login } = useFacebookLogin(facebookConfig);

  function onSubmit({ password, email }): void {
    mutate(
      { email, senha: password },
      {
        onSuccess(data) {
          onLoginSuccess({ email: data.email || '', jwt: data.token });
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
            onLoginSuccess({
              email: apiResponse.email || '',
              jwt: apiResponse.token,
            });
          } else {
            history.push(
              `${PublicRoutes.SignUp}?socialMedia=${RedeSocialEnum.Google}&token=${response.tokenId}`,
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
            onLoginSuccess({
              email: apiResponse.email || '',
              jwt: apiResponse.token,
            });
          } else {
            history.push(
              `${PublicRoutes.SignUp}?socialMedia=${RedeSocialEnum.Apple}&token=${response?.authorization?.id_token}&email=${apiResponse?.email}`,
            );
          }
        },
      },
    );
  }

  function handleCreateAccountClick(): void {
    history.push(PublicRoutes.SignUp);
  }

  const navigateToRecoverPassword = (): void =>
    history.push(PublicRoutes.PasswordRecovery);

  const errorMessage = useMemo(
    () => extractReadableErrorMessage(error, { showFallbackMessage: false }),
    [error],
  );

  return (
    <LoginStepper
      onFacebookLoginSuccess={login}
      onCreateAccountClick={handleCreateAccountClick}
      showCreateAccountButton={featureFlags.flags.CRIAR_CONTA}
      showSocialMediaLoginButton={featureFlags.flags.LOGIN_SOCIAL}
      onGoogleLoginSuccess={handleGoogleLoginSuccess}
      onAppleLoginSuccess={handleAppleLoginSuccess}
      onSubmit={onSubmit}
      error={errorMessage}
      onResetError={reset}
      isSubmitting={isLoading}
      navigateToRecoverPassword={navigateToRecoverPassword}
      initialEmail={currentLoginEmail}
    />
  );
};
