const FEATURE_KEY = 'REACT_APP_FEATURE_';
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
  | 'SIMULAR_NOVO'
  | 'PORTABILIDADE'
  | 'CARTAO'
  | 'REFIN'
  | 'CRIAR_CONTA'
  | 'LOGIN_SOCIAL'
  | 'INCLUSAO_PROPOSTA_NOVA'
  | 'TELEFONE_CRIACAO_VALIDACAO'
  | 'TELEFONE_CRIACAO_VALIDACAO_TELEFONEMA'
  | 'TELEFONE_CRIACAO_VALIDACAO_SMS'
  | 'TELEFONE_CRIACAO_VALIDACAO_WHATSAPP'
  | 'LIVENESS_UNICO'
  | 'LIVENESS_UNICO_INTELIGENTE';

export type FeatureFlags = {
  [key in FEATURE_KEY]?: boolean;
};

export const FEATURES: FeatureFlags = keysFromProcess;
