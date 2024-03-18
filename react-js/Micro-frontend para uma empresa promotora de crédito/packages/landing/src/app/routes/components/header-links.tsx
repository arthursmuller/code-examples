import { MenuItem } from 'features/components';

import { RoutesEnum } from '../routes.enum';

export const headerLinks: MenuItem[] = [
  {
    label: 'Início',
    route: '/',
    isExternal: false,
  },
  {
    label: 'Nossos Produtos',
    items: [
      {
        label: 'Aposentados e Pensionistas',
        route: RoutesEnum.AposentadosPensionistas,
        isExternal: false,
      },
      {
        label: 'Funcionário Público',
        route: RoutesEnum.FuncionarioPublico,
        isExternal: false,
      },
      {
        label: 'Cartão Consignado',
        route: RoutesEnum.CartaoConsignado,
        isExternal: false,
      },
      {
        label: 'Crédito Consignado',
        route: RoutesEnum.CreditoConsignado,
        isExternal: false,
      },
      {
        label: 'Título Capitalização',
        route: RoutesEnum.TituloCapitalizao,
        isExternal: false,
      }
    ],
  },
  {
    label: 'Sobre a Bem',
    route: RoutesEnum.About,
    isExternal: false,
  },
  {
    label: 'Atendimento',
    route: RoutesEnum.Stores,
    isExternal: false,
  },
  {
    label: 'Consultar Proposta',
    route: RoutesEnum.ProposalSearch,
    isExternal: false,
  },
  {
    label: 'Blog',
    route: 'https://www.bempromotora.com.br/blog-bem',
    isExternal: true,
  },
];
