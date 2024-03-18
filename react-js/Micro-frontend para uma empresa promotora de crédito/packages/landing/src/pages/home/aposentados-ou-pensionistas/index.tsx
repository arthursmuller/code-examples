import Head from 'next/head';

import { getStaticPropsDefault } from 'common/get-static-props-default';
import { AposentadosPensionistas } from 'features/landing/features/products/aposentados-pensionistas';
import { PublicTemplate } from 'app/routes/components';

const title =
  'Empréstimo Consignado: Aposentados e Pensionistas - Bem Promotora';
const description =
  'Somos Especialistas em Crédito Consignado p/ Aposentados e Pensionistas do INSS. Empréstimo c/ Desconto em Folha de Forma Fácil, Rápida e Segura. Conheça!';

const AposentadosPensionistasPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <AposentadosPensionistas />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default AposentadosPensionistasPage;
