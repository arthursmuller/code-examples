import { PublicTemplate } from 'app/routes/components';
import { getStaticPropsDefault } from 'common/get-static-props-default';
import DischargeDocument from 'features/landing/features/discharge-document';

const DocumentoQuitacaoPage: React.FC = () => (
  <PublicTemplate>
    <DischargeDocument />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default DocumentoQuitacaoPage;
