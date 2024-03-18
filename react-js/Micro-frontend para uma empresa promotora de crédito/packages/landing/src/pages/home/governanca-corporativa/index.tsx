import Head from 'next/head';

import { FullHeightTemplate, PublicTemplate } from 'app/routes/components';
import CooperativeGovernance from 'features/landing/features/cooperative-governance';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Governança Corporativa: Administração e mais - Bem Promotora';
const description =
  'Acesse e Conheça a Visão Geral, Estatuto e Políticas, Administração, Diretoria Executiva, Conselho Fiscal, Gestão, Assembleias, Atas e mais. Confira!';

const CooperativeGovernancePage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <FullHeightTemplate>
      <CooperativeGovernance />
    </FullHeightTemplate>
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default CooperativeGovernancePage;
