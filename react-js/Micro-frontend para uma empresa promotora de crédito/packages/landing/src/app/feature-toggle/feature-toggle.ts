const FEATURE_KEY = 'NEXT_PUBLIC_FEATURE_';
const FEATURE_KEY_ALT = 'FEATURE_';

const keysFromProcess = Object.keys(process.env)
  .filter((key) => key.includes(FEATURE_KEY) || key.includes(FEATURE_KEY_ALT))
  .reduce(
    (keys, key) => ({
      ...keys,
      [key.includes(FEATURE_KEY)
        ? key.substring(FEATURE_KEY.length)
        : key.substring(FEATURE_KEY_ALT.length)]: process.env[key] === 'true',
    }),
    {},
  );

type FEATURE_KEY =
  | 'ACESSAR_CONTA'
  | 'WHATSAPP'
  | 'CRIAR_CONTA'
  | 'LOGIN_SOCIAL';

export type FeatureFlags = {
  [key in FEATURE_KEY]?: boolean;
};

export const FEATURES: FeatureFlags = keysFromProcess;
