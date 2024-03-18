import { ConfigRoutesPaths } from '../features/configuracoes/configuracoes-routes';

export const mainRoutePaths = {
  INICIO: '/inicio',

  RESUMO_SOLICITACAO: '/resumo-solicitacao',

  PERFIL: '/perfil',
  MEUS_CONTRATOS: '/contratos',
  SIMULAR_NOVO: '/simular-novo-consignado',
  PORTABILIDADE: '/portabilidade',
  CARTAO: '/consultar-cartao',
  REFIN: '/refinanciar-consignado',
  AJUDA: '/ajuda',
  BUSCAR: '/buscar',
  IMPORTAR_DADOS: '/cadastrar-dados',
  CONSULTAR_SIAPE: '/consultar-margem-siape',
  CONSULTAR_INSS: '/consultar-beneficio-inss',

  CONFIGURACOES: ConfigRoutesPaths.configs,
  LOGOUT: '/logout',
};
