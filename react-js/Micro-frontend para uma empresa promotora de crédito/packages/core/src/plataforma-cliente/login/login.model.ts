export interface LoginModel {
  email: string;
  senha: string;
}

export interface AutenticacaoModel {
  nome?: string;
  token: string;
  email?: string;
}

export interface AutenticacaoLoginSocialModel {
  nome?: string;
  email?: string;
  token?: string;
  usuarioCadastrado?: string;
}

export enum RedeSocialEnum {
  Google = 1,
  Facebook = 2,
  Apple = 3,
}

export interface LoginSocialEnvioModel {
  token: string;
  code?: string;
  redirectURL?: string;
}
