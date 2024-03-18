import { HomeProvided } from 'features/landing';
import { PublicTemplate } from 'app/routes/components';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const Home: React.FC = () => (
  <PublicTemplate>
    <HomeProvided />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default Home;
