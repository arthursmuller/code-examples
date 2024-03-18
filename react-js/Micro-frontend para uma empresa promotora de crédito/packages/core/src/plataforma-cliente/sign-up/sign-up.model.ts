import { RedeSocialEnum } from 'plataforma-cliente/login';

export interface UsuarioLoginSocialCriacaoModel {
  redeSocial: RedeSocialEnum;
  token: string;
}

export interface UsuarioCriacaoModel {
  nome: string;
  email: string;
  cpf: string;
  senha: string;
  loginSocial?: UsuarioLoginSocialCriacaoModel;
}

export interface UsuarioModel {
  id: number;
  nome: string;
  email: string;
  cpf: string;
}
