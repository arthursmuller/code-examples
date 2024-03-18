import { FC } from 'react';

import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import LGPD from 'features/landing/features/lgpd';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Lei Geral de Proteção a dados';
const description =
  'Entenda o que é a LGPD - Lei Geral de Proteção aos dados, suas motivações, o que ela representa, órgãos responsáveis e o que a Bem Promotora faz para cuidar de suas informações.';

const LGPDPage: FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <LGPD />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default LGPDPage;
