import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import Stores from 'features/landing/features/stores';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Ouvidoria | SAC | Fale Conosco | Atendimento - Bem Promotora';
const description =
  'Encontre nossos Números de Contato e Fale com a Nossa Equipe de Atendimento. Telefones para Capitais e Regiões Metropolitanas e Demais Localidades.';

const StoresPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <Stores />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default StoresPage;
