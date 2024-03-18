import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import WorkingWithUs from 'features/landing/features/working-with-us';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Faça Parte da Equipe da Bem Promotora | Trabalhe Conosco';
const description =
  'Quer Trabalhar na Bem Promotora? Candidate-se Agora para Nossas Oportunidades de Emprego. Faça Parte da Nossa Equipe de Colaboradores. Envie seu Currículo!';

const WorkinWithUsPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <WorkingWithUs />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default WorkinWithUsPage;
