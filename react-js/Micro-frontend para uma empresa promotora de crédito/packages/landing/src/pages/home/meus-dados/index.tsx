import { FC } from 'react';

import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import PersonalData from 'features/landing/features/personal-data-request';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Acesso de Dados Pessoais - Bem Promotora';
const description = 'Solicite seus dados cadastrais junto a Bem Promotora';

const DadosPessoaisPage: FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <PersonalData />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default DadosPessoaisPage;
