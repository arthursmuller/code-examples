import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import Correspondent from 'features/landing/features/correspondent';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Seja Nosso Correspondente Bancário | Associe-se a Bem Promotora';
const description =
  'Seja Nosso Correspondente Bancário | Associe-se a Bem Promotora. Preencha os Seguintes Campos: Nome Completo, Email, Telefone, Cidade, Estado e Atividades.';

const CorrespondentePage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <Correspondent />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default CorrespondentePage;
