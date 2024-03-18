import { FC } from 'react';

import { Flex, Box } from '@chakra-ui/react';

import {
  CustomHeading,
  SwipeableContainer,
  ColorSchemes,
} from '@pcf/design-system';
import { RoutesEnum } from 'app/routes/routes.enum';

import ProductCard from './product-card';

export const products = [
  {
    title: 'Crédito Consignado',
    text: 'Somos especialistas em Crédito Consignado para <b>aposentados e pensionistas do INSS</b> e <b>funcionários públicos federais SIAPE.</b>',
    route: RoutesEnum.CreditoConsignado,
    isExternal: false,
  },
  {
    title: 'Cartão Consignado',
    text: 'Contrate o nosso Cartão Consignado e usufrua de mais <b>5% de crédito para utilizar como quiser</b>, além de ótimos benefícios!',
    route: RoutesEnum.CartaoConsignado,
    isExternal: false,
  },
  {
    title: 'Título de Capitalização',
    text: 'O <b>CAP premiado</b> é o nosso título de capitalização da Bem Promotora. Com ele você <b>economiza e concorre a prêmios</b> semanais!',
    route: RoutesEnum.TituloCapitalizao,
    isExternal: false,
  },
];

export const Produtos: FC = () => {
  return (
    <Flex as="section" p="68px 0px 80px 0px" flexDirection="column" w="100%">
      <Box mb="64px" px="24px">
        <CustomHeading
          as="h2"
          textStyle="bold40_48"
          color="secondary.mid-dark"
          textAlign="center"
        >
          Conheça nossa linha de produtos
        </CustomHeading>

        <CustomHeading
          as="h3"
          textStyle={['regular16', 'regular20']}
          textAlign="center"
          mt="24px"
        >
          A Bem Promotora te ajuda a realizar os seus sonhos!
        </CustomHeading>
      </Box>

      <Box p={['0 12px 0 12px', '0 24px 0 24px', '', '']}>
        <SwipeableContainer schemeColor={ColorSchemes.secondary}>
          {products.map((product) => (
            <ProductCard key={product.title} {...product} />
          ))}
        </SwipeableContainer>
      </Box>
    </Flex>
  );
};

export default Produtos;
