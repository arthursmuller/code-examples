import { FC } from 'react';

import { Flex, Icon, Text, Wrap, WrapItem, Link, Box } from '@chakra-ui/react';
import NextLink from 'next/link';

import { CustomHeading } from '@pcf/design-system';
import { ExternallinkIcon } from '@pcf/design-system-icons';
import { BemImage } from 'features/components/images';

import { WorkingWithUsSection } from './working-with-us-section';

import SerIcon from '../assets/Logo_SER_colorido.png';
import Capa1 from '../assets/Ser-Live-Mulher.png';
import Capa2 from '../assets/Ser-Live-Raca-e-Etnia.png';
import Capa3 from '../assets/SER-Live-LGBTQIAP.png';
import Capa4 from '../assets/SER-Live-Geracoes.png';

const YOUTUBE_ITEMS = [
  {
    id: 1,
    link: 'https://youtu.be/BT_2BYDKbwM',
    description: 'LIVE SER: Pilar Mulher',
    alt: 'LIVE SER: Pilar Mulher',
    icon: Capa1,
  },
  {
    id: 2,
    link: 'https://youtu.be/mCso6kHhU0I',
    description: 'LIVE SER: Pilar Raça e Etnia',
    alt: 'LIVE SER: Pilar Raça e Etnia',
    icon: Capa2,
  },
  {
    id: 3,
    link: 'https://youtu.be/dhyLbx_qbeU',
    description: 'LIVE SER: Pilar LGBTQIAP+',
    alt: 'LIVE SER: Pilar LGBTQIAP+',
    icon: Capa3,
  },
  {
    id: 4,
    link: 'https://youtu.be/goC-XP8zuwM',
    description: 'LIVE SER: Pilar Gerações',
    alt: 'LIVE SER: Pilar Gerações',
    icon: Capa4,
  },
];

export const Programs: FC = () => {
  return (
    <Flex flexDir="column" mt={4}>
      <CustomHeading
        textStyle="bold32_40"
        color="primary.regular"
        mt="45px"
        mb="31px"
        textAlign={['center', 'center', 'start']}
      >
        Nossos Programas
      </CustomHeading>

      <Flex
        layerStyle="card"
        flexDir="column"
        p={['32px', '32px', '32px 56px']}
      >
        <Box width="269px" height="106px" position="relative">
          <BemImage
            width="269px"
            height="106px"
            alt="logotipo programa SER"
            src={SerIcon}
          />
        </Box>

        <CustomHeading textStyle="bold24_32" color="secondary.regular" mt={6}>
          SER – Programa de Diversidade
        </CustomHeading>

        <Text textStyle="regular16_20" textAlign="justify" mt={6}>
          O Programa SER – Diversidade faz (a) Bem foi lançado em outubro de
          2019 com o intuito de promover ações afirmativas para cada um dos
          pilares de atuação:{' '}
          <Text as="span" textStyle="bold16_20">
            LGBTQIA+, Gerações, Mulher, PcD, Raça e Etnia.
          </Text>{' '}
          Com o apoio de um time de Guardiões da Diversidade, reforçamos a
          importância do respeito às diferenças e individualidades de cada
          pessoa!
        </Text>

        <Text mt={6} textStyle="bold16_24">
          Acreditamos que a diversidade é a chave para a inovação e a
          transformação. Aqui, todas as pessoas são bem-vindas para somar!{' '}
          <Text as="span" color="primary.regular">
            {' '}
            Confira algumas lives promovidas pelo nosso Programa SER:
          </Text>
        </Text>

        <Flex justifyContent="center">
          <Wrap
            maxW="1200px"
            justify="center"
            align="center"
            mt={[6, 6, '80px']}
            spacing={[6, 6, '40px']}
          >
            {YOUTUBE_ITEMS.map(({ id, description, icon, link, alt }) => (
              <WrapItem key={id}>
                <Flex flexDir="column" w={['211px', '211px', '487px']}>
                  <Flex
                    w={['211px', '211px', '487px']}
                    h={['89px', '89px', '205px']}
                    position="relative"
                    mb={3}
                  >
                    <BemImage alt={alt} src={icon} />
                  </Flex>
                  <Flex
                    justifyContent="space-between"
                    flexDir={['column', 'column', 'row']}
                    alignItems={['start', 'start', 'center']}
                  >
                    <Text color="primary.regular" textStyle="bold16_20">
                      {description}
                    </Text>
                    <Link
                      mt={['10px', '10px', 0]}
                      px={2}
                      py={1}
                      display="flex"
                      alignItems="center"
                      borderRadius={4}
                      bg="primary.washed"
                      textStyle="bold14"
                      color="primary.mid-dark"
                      href={link}
                      size="sm"
                      isExternal
                    >
                      Ver mais
                      <Icon
                        ml={2}
                        as={ExternallinkIcon}
                        boxSize={3}
                        color="primary.mid-dark"
                      />
                    </Link>
                  </Flex>
                </Flex>
              </WrapItem>
            ))}
          </Wrap>
        </Flex>
      </Flex>

      <Flex
        layerStyle="card"
        flexDir="column"
        mt="48px"
        p="32px 56px 48px 56px"
      >
        <CustomHeading textStyle="bold24_32" color="secondary.regular">
          Programa de Estágio
        </CustomHeading>

        <Text mt={6} textAlign="justify" textStyle="regular16_20">
          Com o objetivo de capacitar estudantes de diversas áreas, lançamos o
          nosso Programa de Estágio “Seu start para o futuro”. Nossa missão é
          desenvolver pessoas, em uma jornada de muito conhecimento, que serão
          apadrinhadas em suas devidas áreas. Buscamos estudantes de todas as
          idades. Nosso intuito é promover a diversidade e reter grandes
          talentos.
        </Text>

        <Text textStyle="bold16_20" mt={6}>
          Quer dar o seu start para o futuro? Fique ligado em{' '}
          <NextLink passHref href="https://bempromotora.pandape.com.br/">
            {/* eslint-disable-next-line */}
            <Link isExternal color="primary.regular">
              nossas vagas
            </Link>
          </NextLink>{' '}
          no Pandapé
        </Text>
      </Flex>

      <Flex
        layerStyle="card"
        flexDir="column"
        mt="48px"
        p="32px 56px 48px 56px"
        mb="48px"
      >
        <CustomHeading textStyle="bold24_32" color="secondary.regular">
          Programa Jovem Aprendiz
        </CustomHeading>

        <Text mt={6} textAlign="justify" textStyle="regular16_20">
          Recrutamos jovens para o nosso Programa de Aprendizagem. Em nosso
          Programa, oferecemos uma jornada de aprendizagem aliando a teoria e a
          prática, onde temos a oportunidade de contribuir com o desenvolvimento
          socioeducacional dos nossos jovens.
        </Text>

        <Text textStyle="bold16_20" mt={6}>
          Se você gosta de aprender e busca ter a oportunidade de conhecer o
          mundo do trabalho para colocar em prática seus aprendizados,{' '}
          <NextLink passHref href="https://bempromotora.pandape.com.br/">
            {/* eslint-disable-next-line */}
            <Link isExternal color="primary.regular">
              fique ligado em nossas vagas e inscreva-se.
            </Link>
          </NextLink>
        </Text>
      </Flex>

      <WorkingWithUsSection />
    </Flex>
  );
};
