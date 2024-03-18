import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import PrivacyPolicy from 'features/landing/features/privacy-policy';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title =
  'Política de Privacidade e Compromisso do Website da Bem Promotora';
const description =
  'Leia Sobre a Política de Privacidade e Compromisso do Website da Bem Promotora com a Segurança e a Privacidade de Informações de usuários.';

const WorkinWithUsPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <PrivacyPolicy />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default WorkinWithUsPage;
