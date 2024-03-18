import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import FuncionarioPublico from 'features/landing/features/products/funcionario-publico';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Empréstimo para Funcionários Públicos | Crédito Consignado';
const description =
  'Bem Promotora. Especialistas em Crédito Consignado para Funcionários Públicos. Empréstimo Fácil com Desconto Em Folha. Seguro Exclusivo SIAPE. Saiba Mais!';

const FuncionarioPublicoPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <FuncionarioPublico />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default FuncionarioPublicoPage;
