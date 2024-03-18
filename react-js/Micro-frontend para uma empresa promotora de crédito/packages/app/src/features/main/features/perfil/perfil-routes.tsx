import { FC } from 'react';

import { Route, Switch } from 'react-router-dom';

import { Contacts } from './contacts';
import { DadosPessoais } from './dados-pessoais';
import { Addresses } from './addresses';
import { Documentos } from './documentos';
import { Rendimentos } from './rendimentos';
import { PerfilRoutesPaths } from './perfil.routes.enum';
import { DadosCadastrais } from './dados-cadastrais';

export const PerfilRoutes: FC = ({ children }) => {
  return (
    <Switch>
      <Route exact path={PerfilRoutesPaths.perfil}>
        {children}
      </Route>
      <Route path={PerfilRoutesPaths.dadosPessoais}>
        <DadosPessoais />
      </Route>
      <Route path={PerfilRoutesPaths.dadosCadastrair}>
        <DadosCadastrais />
      </Route>
      <Route path={PerfilRoutesPaths.contatos}>
        <Contacts />
      </Route>
      <Route path={PerfilRoutesPaths.enderecos}>
        <Addresses />
      </Route>
      <Route path={PerfilRoutesPaths.documentos}>
        <Documentos />
      </Route>
      <Route path={PerfilRoutesPaths.rendimentos}>
        <Rendimentos />
      </Route>
    </Switch>
  );
};
