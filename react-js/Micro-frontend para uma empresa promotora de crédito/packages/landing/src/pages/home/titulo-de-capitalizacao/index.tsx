import { FC } from 'react';

import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import { TituloCapitalizacao } from 'features/landing/features/products/titulo-capitalizacao';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title =
  'Título de Capitalização: Economize e Concorra a Prêmios Semanais';
const description =
  'Bem Promotora. Títulos de Capitalização para Aposentados, Pensionistas e Servidores Públicos INSS. Economize e Concorra a Prêmios c/ CAP Premiado. Confira!';

const TituloCapitalizacaoPage: FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <TituloCapitalizacao />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default TituloCapitalizacaoPage;
