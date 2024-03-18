import { getQueryFor } from 'common/client';
import { createQueryFor } from 'utils';

import { UnidadeFederativaModel } from './unidades-federativas-query';

export interface MunicipioModel {
  id: number;
  descricao: string;
  uf: UnidadeFederativaModel;
}

interface MunicioQueryParams {
  idUF: string | undefined;
  municipio?: string | undefined;
}

export const citiesQueryEndpointGen = (idUF: string): string =>
  `localizacoes/unidades-federativas/${idUF}/municipios`;

const queryFn = async (
  queryString?: MunicioQueryParams,
): Promise<MunicipioModel[]> => {
  const { idUF, ...otherQueryProps } = queryString || {};

  const getQuery = getQueryFor<MunicipioModel[], Partial<MunicioQueryParams>>(
    citiesQueryEndpointGen(idUF as string),
    otherQueryProps,
  );

  return getQuery();
};

export const {
  getQueryConfig: getMunicipiosQueryConfig,
  useQueryOf: useMunicipios,
} = createQueryFor<MunicipioModel[], MunicioQueryParams>(
  'localizacoes/unidades-federativas/municipios',
  queryFn,
);
