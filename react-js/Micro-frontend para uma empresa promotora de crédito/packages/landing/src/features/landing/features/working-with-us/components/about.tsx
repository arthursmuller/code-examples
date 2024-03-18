import { FC } from 'react';

import { Box, Text, Flex } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import { WorkingWithUsSection } from './working-with-us-section';

import MeetingIcon from '../assets/meeting.svg';
import MoneyIcon from '../assets/money.svg';

export const About: FC = () => {
  return (
    <Box>
      <CustomHeading
        mt={[7, 7, '58px']}
        textStyle="bold24_40"
        color="secondary.regular"
      >
        Nós somos a Bem Promotora
      </CustomHeading>

      <Text mt={[4, 4, 8]} textStyle="regular16_20">
        Somos uma Promotora de Vendas e Serviços que nasceu em 30 de setembro de
        2008, especializada em crédito consignado, e que tem como seu propósito
        Transformar Oportunidades em Realizações. A presença em todas as regiões
        do país acontece por meio de lojas próprias e de uma qualificada rede de
        correspondentes substabelecidos. Buscamos sempre trabalhar para fazer da
        experiência das pessoas colaboradoras e clientes a melhor possível.
        Somos um time vencedor e com vontade de apoiar nossos públicos a
        realizarem seus sonhos. A Bem é uma empresa GPTW! Estamos entre as
        melhores empresas para trabalhar no RS e RJ e entre as melhores para
        mulheres trabalharem no Brasil. Acreditamos que a diversidade é a chave
        para a inovação e a transformação. Aqui, todas as pessoas são bem-vindas
        para somar!
      </Text>

      <Flex
        justifyContent="center"
        my={['31px', '31px', '120px']}
        flexDir={['column', 'column', 'row']}
      >
        <Flex
          alignItems="center"
          justifyContent="center"
          flexDir="column"
          layerStyle="card"
          w={['303px', '303px', '392px']}
          h="298px"
          mr={[0, 0, 8]}
          mb={[8, 8, 0]}
        >
          <BemImage
            alt="ilustração reunião"
            width="90px"
            height="85px"
            src={MeetingIcon}
          />
          <Text
            textStyle="bold24"
            color="primary.regular"
            textAlign="center"
            mt="25px"
          >
            Mais de 380 pessoas transformando oportunidades em realizações
          </Text>
        </Flex>
        <Flex
          layerStyle="card"
          w={['303px', '303px', '392px']}
          h="298px"
          alignItems="center"
          justifyContent="center"
          flexDir="column"
        >
          <BemImage
            width="90px"
            height="85px"
            src={MoneyIcon}
            alt="ilustração dinheiro"
          />

          <Text
            textStyle="bold24"
            color="primary.regular"
            textAlign="center"
            mt="40px"
          >
            Mais de 234 correspondentes bancários parceiros
          </Text>
        </Flex>
      </Flex>

      <WorkingWithUsSection />
    </Box>
  );
};
