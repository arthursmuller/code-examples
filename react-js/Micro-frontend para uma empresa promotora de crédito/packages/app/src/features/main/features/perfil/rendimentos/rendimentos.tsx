import { FC } from 'react';

import { Redirect, Route, Switch, useRouteMatch } from 'react-router-dom';

import { EditMatricula } from './edit-matricula';
import { Matriculas } from './matriculas';

export const Rendimentos: FC = () => {
  const { path } = useRouteMatch();

  return (
    <Switch>
      <Route exact path={path}>
        <Matriculas />
      </Route>
      <Route path={`${path}/:matriculaId/edit`}>
        <EditMatricula />
      </Route>
      <Route exact path={`${path}/:matriculaId`}>
        <Redirect to={path} />
      </Route>
    </Switch>
  );
};
