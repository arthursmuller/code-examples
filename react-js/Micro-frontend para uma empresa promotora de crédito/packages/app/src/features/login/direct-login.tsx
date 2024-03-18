import { FC } from 'react';

import qs from 'qs';
import { useHistory } from 'react-router-dom';
import { useMount } from 'react-use';

import { Loader } from '@pcf/design-system';
import { useAuthContext } from 'app/auth/auth.context';
import { PublicRoutes } from 'app/routes/public/public-routes.enum';

export const DirectLogin: FC = () => {
  const { onLoginSuccess } = useAuthContext();
  const { replace, location: { search }} = useHistory();

  useMount(() => {
    const queryParams = qs.parse(search, { ignoreQueryPrefix: true });

    if (queryParams && queryParams.re) {
      const jwt = queryParams.re as string;
      const email = queryParams.for as string;

      replace(PublicRoutes.Root);

      onLoginSuccess({ email, jwt });
    } else {
      replace(PublicRoutes.Login);
    }
  });

  return <Loader fullHeight fullWidth />;
};

export default DirectLogin;
