import { FC } from 'react';

import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import { Conditions } from 'features/landing/features/conditions';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Condições Gerais Empréstimo Consignado Banrisul - Bem Promotora';
const description =
  'CONDIÇÕES GERAIS DO CONTRATO DE CONCESSÃO DE EMPRÉSTIMO MEDIANTE CONSIGNAÇÃO EM FOLHA DE PAGAMENTO   Para fazer o download clique aqui  ';

const CondicoesGeraisPage: FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <Conditions />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default CondicoesGeraisPage;
