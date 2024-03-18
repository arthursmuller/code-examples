import { lazy, FC } from 'react';

import { Redirect, Route, Switch } from 'react-router-dom';

import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';
import { MenuBar } from 'components/menu-bar';
import { Flex } from '@chakra-ui/react';

const Login = lazy(() => import('features/login'));
const SignUp = lazy(() => import('features/sign-up'));
const ForgotPassword = lazy(() => import('features/password-recovery'));
const NewPassword = lazy(
  () => import('features/password-recovery/new-password'),
);
const DirectLogin = lazy(() => import('features/login/direct-login'));

export const PublicRoutes: FC = () => (
  <Switch>
    <Route exact path={PublicRoutesEnum.Root} component={DirectLogin} />

    <Route path={PublicRoutesEnum.Root}>
      <Flex direction="column">
        <MenuBar items={[]} background="primary.gradient" logoRoute={PublicRoutesEnum.Landing} />
        <Flex direction="column" marginTop={2}>
          <Switch>
            <Route
              exact
              path={`${PublicRoutesEnum.SignUp}`}
              component={SignUp}
            />
            <Route
              exact
              path={PublicRoutesEnum.PasswordRecovery}
              component={ForgotPassword}
            />
            <Route
              exact
              path={PublicRoutesEnum.NewPassword}
              component={NewPassword}
            />

            <Route exact path={PublicRoutesEnum.Login} component={Login} />

            <Route exact path={PublicRoutesEnum.Landing} component={() => {
              window.location.replace(process.env.REACT_APP_PLATAFORMA_CLIENTE_LANDING); 
              return null;
            }}/>

            <Redirect to={PublicRoutesEnum.Root} />
          </Switch>
        </Flex>
      </Flex>
    </Route>

    <Redirect to={PublicRoutesEnum.Root} />
  </Switch>
);
