import { FC, useMemo } from 'react';

import { InactivityLoginDialog } from '@pcf/design-system';
import { useLogin, extractReadableErrorMessage } from '@pcf/core';

export interface InactivityLoginProps {
  onSuccess?: (data: { email: string; jwt: string }) => void;
  email?: string;
}

export const InactivityLogin: FC<InactivityLoginProps> = ({
  onSuccess,
  email: initialEmail,
}) => {
  const { mutate, isLoading, error } = useLogin({
    onError: () => {},
  });

  function onSubmit({ password, email }): void {
    mutate(
      { email, senha: password },
      {
        onSuccess(data) {
          onSuccess && onSuccess({ email: data.email || '', jwt: data.token });
        },
      },
    );
  }

  const errorMessage = useMemo(
    () => extractReadableErrorMessage(error, { showFallbackMessage: false }),
    [error],
  );

  return (
    <InactivityLoginDialog
      onSubmit={onSubmit}
      error={errorMessage}
      isSubmitting={isLoading}
      initialEmail={initialEmail}
      disableEmail
    />
  );
};
