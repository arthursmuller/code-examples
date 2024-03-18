import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import AboutBem from 'features/landing/features/about';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Sobre a Bem Promotora | Conheça Nossa Empresa';
const description =
  'Bem Promotora. Somos Especialistas em Crédito Consignado desde 2008 e Estamos Presentes em Todo o Brasil. Conheça Nossa Empresa, Missão e Valores. Confira!';

const Sobre: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <AboutBem />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default Sobre;
