import { lazy, FC } from 'react';

import { LazyByDevice } from '@pcf/design-system';

const AppMobile = lazy(() => import('./main-mobile'));
const AppDesktop = lazy(() => import('./main-desktop'));

const App: FC = () => <LazyByDevice mobile={AppMobile} desktop={AppDesktop} />;

export default App;
