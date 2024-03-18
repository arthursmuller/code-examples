import { PublicTemplate } from 'app/routes/components';
import { getStaticPropsDefault } from 'common/get-static-props-default';
import { CreditoConsignado } from 'features/landing/features/products/credito-consignado';
import { FC } from 'react';

const CreditoConsignadoPage: FC = () => (
  <PublicTemplate>
    <CreditoConsignado />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default CreditoConsignadoPage;
