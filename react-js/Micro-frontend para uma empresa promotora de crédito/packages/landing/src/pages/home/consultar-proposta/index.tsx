import Head from 'next/head';
import { PublicTemplate } from 'app/routes/components';
import ProposalSearch from 'features/landing/features/proposal-search';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Consulte sua proposta na Bem Promotora';
const description =
  'Aqui você verifica o andamento da sua proposta de Crédito Consignado junto a Bem Promotora.';

const ProposalSearchPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <ProposalSearch />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default ProposalSearchPage;
