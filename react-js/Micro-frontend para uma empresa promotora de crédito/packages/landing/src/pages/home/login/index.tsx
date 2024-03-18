import Login from 'features/login';

import { getStaticPropsDefault } from 'common/get-static-props-default';

const LoginPage: React.FC = () => <Login />;

export const getStaticProps = getStaticPropsDefault;

export default LoginPage;
