import { FC, lazy, Suspense } from 'react';

import { Redirect, Route, Switch } from 'react-router-dom';
import { useBreakpointValue } from '@chakra-ui/react';

import { Loader } from '@pcf/design-system';
import { useFeatureFlags } from 'app/feature-toggle';

import { mainRoutePaths } from './main-routes-paths';
import { Logout } from './logout';

const ImportarDados = lazy(() => import('../features/importar-dados'));
const Contracts = lazy(() => import('../features/contrats'));
const Dashboard = lazy(() => import('../features/dashboard'));
const Perfil = lazy(() => import('../features/perfil'));
const Ajuda = lazy(() => import('../features/ajuda'));
const Search = lazy(() => import('../features/search'));
const SimularConsignado = lazy(
  () => import('../features/simulacoes/features/simular-consignado'),
);
const Cartao = lazy(() => import('../features/cartao'));
const Refinanciamento = lazy(
  () => import('../features/simulacoes/features/refinanciamento'),
);
const Portabilidade = lazy(
  () => import('../features/simulacoes/features/portabilidade'),
);
const ConfiguracoesMobile = lazy(
  () => import('../features/configuracoes/configuracoes-mobile'),
);
const ResumoSolicitacao = lazy(() => import('../features/resumo-solicitacao'));
const ConsultaSiape = lazy(() => import('../features/consulta-siape'));
const ConsultaInss = lazy(() => import('../features/consulta-inss'));

export const MainRoutes: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');
  const { flags, isLoading } = useFeatureFlags();

  if (isLoading) {
    return <Loader fullHeight fullWidth />;
  }

  return (
    <Suspense fallback={<Loader fullHeight fullWidth />}>
      <Switch>
        <Route exact path={mainRoutePaths.INICIO} component={Dashboard} />

        <Route
          exact
          path={`${mainRoutePaths.RESUMO_SOLICITACAO}/:intencaoOperacaoId`}
          component={ResumoSolicitacao}
        />

        <Route path={mainRoutePaths.PERFIL} component={Perfil} />
        <Route path={mainRoutePaths.MEUS_CONTRATOS} component={Contracts} />
        {flags.SIMULAR_NOVO && (
          <Route
            path={mainRoutePaths.SIMULAR_NOVO}
            component={SimularConsignado}
          />
        )}
        {flags.PORTABILIDADE && (
          <Route
            path={mainRoutePaths.PORTABILIDADE}
            component={Portabilidade}
          />
        )}
        {flags.CARTAO && (
          <Route path={mainRoutePaths.CARTAO} component={Cartao} />
        )}
        {flags.REFIN && (
          <Route path={mainRoutePaths.REFIN} component={Refinanciamento} />
        )}
        <Route path={mainRoutePaths.AJUDA} component={Ajuda} />
        <Route path={mainRoutePaths.BUSCAR} component={Search} />
        <Route path={mainRoutePaths.IMPORTAR_DADOS} component={ImportarDados} />
        <Route
          path={mainRoutePaths.CONSULTAR_SIAPE}
          component={ConsultaSiape}
        />
        <Route path={mainRoutePaths.CONSULTAR_INSS} component={ConsultaInss} />

        {isMobile && (
          <Route
            path={mainRoutePaths.CONFIGURACOES}
            component={ConfiguracoesMobile}
          />
        )}
        <Route path={mainRoutePaths.LOGOUT} component={Logout} />

        <Redirect to={mainRoutePaths.INICIO} />
      </Switch>
    </Suspense>
  );
};

export default MainRoutes;
