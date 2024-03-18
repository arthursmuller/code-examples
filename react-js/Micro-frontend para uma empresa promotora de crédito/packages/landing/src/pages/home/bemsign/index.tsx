import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import TermsBemSign from 'features/landing/features/terms-bem-sign';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Termo de uso e privacidade para uso da Bemsign - Bem Promotora';
const description =
  'Última alteração: 18/11/2020 Versão: 1.00.02   A Solução/Ferramenta “BEMSIGN” é de propriedade da BEM PROMOTORA DE VENDAS E SERVIÇOS S/A, pessoa jurídica de direito privado, inscrita no […]';

const TermsBemSignPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <TermsBemSign />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default TermsBemSignPage;
