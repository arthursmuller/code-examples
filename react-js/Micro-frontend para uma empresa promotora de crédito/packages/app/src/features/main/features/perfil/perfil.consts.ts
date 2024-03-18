import { RouteOpt } from 'features/main/components/use-sub-route-menu';

import { PerfilRoutesPaths } from './perfil.routes.enum';

interface PerfilOpt extends RouteOpt {
  description: string;
}

export const perfilOptions: PerfilOpt[] = [
  {
    title: 'Minhas Mensagens',
    description: 'Confira aqui as mensagens que a Bem mandou para você',
    route: PerfilRoutesPaths.mensagens,
    disabled: true,
  },
  {
    title: 'Meus Dados Pessoais',
    description:
      'Cadastre ou edite seu nome, estado civil, grau de instrução...',
    route: PerfilRoutesPaths.dadosPessoais,
  },
  {
    title: 'Meus Dados Cadastrais',
    description: 'Solicite  seus dados cadastrais, através da plataforma',
    route: PerfilRoutesPaths.dadosCadastrair,
  },
  {
    title: 'Meus Rendimentos',
    description: 'Cadastre ou edite as matrículas das suas contas',
    route: PerfilRoutesPaths.rendimentos,
  },
  {
    title: 'Meus Contatos',
    description: 'Cadastre ou edite seus telefones e e-mails de contato',
    route: PerfilRoutesPaths.contatos,
  },
  {
    title: 'Meus Endereços',
    description: 'Cadastre ou edite seus endereços',
    route: PerfilRoutesPaths.enderecos,
  },
  {
    title: 'Meus Documentos',
    description:
      'Cadastre ou atualize seus documentos de identificação, comprovantes de rendimentos de residência...',
    route: PerfilRoutesPaths.documentos,
  },
];
