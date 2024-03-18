import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import TermAndConditions from 'features/landing/features/terms-and-conditions';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Termos e Condições Gerais de Uso do Website da Bem Promotora';
const description =
  'Confira os Termos e Condições Gerais de Uso para Navegação e Consulta Pública do Website da Bem Promotora. Garantias, Responsabilidades e Danos. Leia mais.';

const TermsAndConditionsPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <TermAndConditions />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default TermsAndConditionsPage;
