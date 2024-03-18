export enum RoutesEnum {
  Root = '/',

  SignUp = '/cadastro',

  AposentadosPensionistas = '/aposentados-ou-pensionistas',
  CartaoConsignado = '/cartao-consignado',
  CreditoConsignado = '/credito-consignado',
  FuncionarioPublico = '/funcionario-publico',
  TituloCapitalizao = '/titulo-de-capitalizacao',
  About = '/sobre-a-bem',

  Stores = '/atendimento',
  CooperativeGovernance = '/governanca-corporativa',
  ProposalSearch = '/consultar-proposta',
  Correspondent = '/seja-um-correspondente-bancario',
  WorkingWithUs = '/trabalhe-conosco',
  PrivacyPolicy = '/politica-de-privacidade',
  TermsAndConditions = '/termos-e-condicoes',
  BemSign = '/bemsign',
  PersonalData = '/meus-dados',
  LGPD = '/lgpd',
}

export enum ProxyRoutesEnum {
  PasswordRecovery = '/recuperacao-senha',
  NewPassword = '/recuperacao-senha/:token',
  App = '/app',
  SignUp = '/signup',
}
