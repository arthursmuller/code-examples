import { useQuery, UseQueryResult } from 'react-query';

import { getQueryFor } from 'common/client';

export const useRecuperarSenhaQuery = (
  token: string,
): UseQueryResult<boolean, Error> => {
  return useQuery(
    'usuarios/recuperacao-senha',
    getQueryFor<boolean>(`usuarios/recuperacao-senha/${token}`),
    { enabled: !!token, useErrorBoundary: false },
  );
};
