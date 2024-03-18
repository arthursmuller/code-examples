export interface UsuarioRecuperacaoSenhaRequisicao {
  email?: string;
  novaSenha?: string;
  senha?: string;
}

export interface UsuarioTrocaSenhaRequisicao {
  senhaAtual: string;
  novaSenha: string;
  senha?: string;
}
