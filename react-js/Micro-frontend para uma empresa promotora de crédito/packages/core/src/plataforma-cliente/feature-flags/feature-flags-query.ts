import { createQueryFor } from 'utils';

export interface FeatureFlagModel {
  chave: string;
  habilitado: boolean;
}

export const {
  getQueryConfig: getFeatureFlagsQueryConfig,
  useQueryOf: useFeatureFlagsQuery,
} = createQueryFor<FeatureFlagModel[]>('feature-flags');

export const flattenFlags = (flags: FeatureFlagModel[]) =>
  flags.reduce((map, flag) => {
    map[flag.chave] = flag.habilitado;
    return map;
  }, {});
