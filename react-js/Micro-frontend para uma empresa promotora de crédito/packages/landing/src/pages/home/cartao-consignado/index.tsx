import Head from 'next/head';

import { PublicTemplate } from 'app/routes/components';
import CartaoConsignado from 'features/landing/features/products/cartao-consignado';
import { getStaticPropsDefault } from 'common/get-static-props-default';

const title = 'Cartão Consignado na Bem Promotora | Aposentados e Pensionistas';
const description =
  'Cartão Consignado c/ Juros Reduzidos. Faça Saques e Compras. Dinheiro Rápido e Fácil, Sem Anuidades. Conheça os Benefícios e Simule Sua Proposta! Confira!';

const CartaoConsignadoPage: React.FC = () => (
  <PublicTemplate>
    <Head>
      <title>{title}</title>
      <meta property="og:title" content={title} key="title" />
      <meta property="og:description" content={description} key="description" />
    </Head>
    <CartaoConsignado />
  </PublicTemplate>
);

export const getStaticProps = getStaticPropsDefault;

export default CartaoConsignadoPage;
