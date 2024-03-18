import { Box } from '@chakra-ui/react';

import {
  TabAjudaAtivaIcon,
  TabAjudaInativaIcon,
  TabCartaoAtivaIcon,
  TabCartaoInativaIcon,
  TabHomeAtivaIcon,
  TabHomeInativaIcon,
  TabNovoAtivaIcon,
  TabNovoInativaIcon,
  TabPortabAtivaIcon,
  TabPortabInativaIcon,
  TabProfileAtivaIcon,
  TabProfileInativaIcon,
  TabPropostasAtivaIcon,
  TabPropostasInativaIcon,
  TabRefinAtivaIcon,
  TabRefinInativaIcon,
  ConfiguracoesStrokeIcon,
  ConfiguracoesFillIcon,
  SearchIcon,
} from '@pcf/design-system-icons';
import { BemErrorBoundary, BemErrorFallback } from '@pcf/design-system';
import { AvatarContainer } from 'features/main/components/avatar-container';
import { QuickyButtons } from 'features/main/components/quicky-buttons';
import { SideMenuItemProps } from 'features/main/components/side-menu/side-menu-item';
import { isEnabledValidator } from 'app/feature-toggle';

import { QuickyProfile } from '../features/perfil/components';
import { mainRoutePaths } from '../routes/main-routes-paths';
import { CanUseRefinanciamento } from '../features/simulacoes/features/refinanciamento';

export const menuItems: SideMenuItemProps[] = [
  {
    label: 'Início',
    icon: TabHomeInativaIcon,
    iconActive: TabHomeAtivaIcon,
    route: mainRoutePaths.INICIO,
    content: (
      <BemErrorBoundary
        fallbackRender={(props) => (
          <BemErrorFallback
            {...props}
            chakraProps={{ margin: 0 }}
            schemeColor="white"
          />
        )}
      >
        <Box p="44px 24px">
          <AvatarContainer />
          <QuickyButtons mt="73px" />
        </Box>
      </BemErrorBoundary>
    ),
  },
  {
    label: 'Meu Perfil',
    route: mainRoutePaths.PERFIL,
    icon: TabProfileInativaIcon,
    iconActive: TabProfileAtivaIcon,
    content: (
      <BemErrorBoundary
        fallbackRender={(props) => (
          <BemErrorFallback
            {...props}
            chakraProps={{ margin: 0 }}
            schemeColor="white"
          />
        )}
      >
        <QuickyProfile pt="26px" />
      </BemErrorBoundary>
    ),
  },
  {
    label: 'Minhas solicitações',
    route: mainRoutePaths.MEUS_CONTRATOS,
    icon: TabPropostasInativaIcon,
    iconActive: TabPropostasAtivaIcon,
    flagKey: 'MEUS_CONTRATOS',
    validator: isEnabledValidator('MEUS_CONTRATOS'),
  },
  {
    label: 'Simular Novo Consignado',
    route: mainRoutePaths.SIMULAR_NOVO,
    icon: TabNovoInativaIcon,
    iconActive: TabNovoAtivaIcon,
    flagKey: 'SIMULAR_NOVO',
    validator: isEnabledValidator('SIMULAR_NOVO'),
  },
  {
    label: 'Fazer Portabilidade',
    route: mainRoutePaths.PORTABILIDADE,
    icon: TabPortabInativaIcon,
    iconActive: TabPortabAtivaIcon,
    flagKey: 'PORTABILIDADE',
    validator: isEnabledValidator('PORTABILIDADE'),
  },
  {
    label: 'Refinanciar Consignado',
    route: mainRoutePaths.REFIN,
    icon: TabRefinInativaIcon,
    iconActive: TabRefinAtivaIcon,
    validator: CanUseRefinanciamento,
  },
  {
    label: 'Solicitar Cartão',
    route: mainRoutePaths.CARTAO,
    icon: TabCartaoInativaIcon,
    iconActive: TabCartaoAtivaIcon,
    flagKey: 'CARTAO',
    validator: isEnabledValidator('CARTAO'),
  },
  {
    label: 'Preciso de Ajuda',
    route: mainRoutePaths.AJUDA,
    icon: TabAjudaInativaIcon,
    iconActive: TabAjudaAtivaIcon,
  },
];

export const menuItemsMobile: SideMenuItemProps[] = [
  ...menuItems,
  {
    label: 'Configurações',
    icon: ConfiguracoesStrokeIcon,
    iconActive: ConfiguracoesFillIcon,
    route: mainRoutePaths.CONFIGURACOES,
  },
];

export const tabBarMenuItems: SideMenuItemProps[] = [
  {
    label: 'Início',
    icon: TabHomeInativaIcon,
    iconActive: TabHomeAtivaIcon,
    route: mainRoutePaths.INICIO,
  },
  {
    label: 'Contratos',
    route: mainRoutePaths.MEUS_CONTRATOS,
    icon: TabPropostasInativaIcon,
    iconActive: TabPropostasAtivaIcon,
  },
  {
    label: 'Buscar',
    route: mainRoutePaths.BUSCAR,
    icon: SearchIcon,
    iconActive: SearchIcon,
  },
  {
    label: 'Perfil',
    route: mainRoutePaths.PERFIL,
    icon: TabProfileInativaIcon,
    iconActive: TabProfileAtivaIcon,
  },
];
