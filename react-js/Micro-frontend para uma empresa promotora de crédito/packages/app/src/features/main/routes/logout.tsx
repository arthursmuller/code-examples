import { useEffect, FC } from 'react';

import { useAuthContext } from 'app/auth/auth.context';

export const Logout: FC = () => {
  const { onLogout } = useAuthContext();

  useEffect(() => {
    onLogout();
  }, [onLogout]);

  return null;
};
