import { FC } from 'react';

import {
  Flex,
  Text,
  Box,
  UnorderedList,
  ListItem,
  Stack,
} from '@chakra-ui/react';

import { BemImage } from 'features/components/images';
import { CustomHeading } from '@pcf/design-system';

import FrontViewWoman from '../assets/front-view-woman-working-laptop@2x.jpg';

export const WhatIsLGPD: FC = () => {
  return (
    <Stack
      direction={['column', 'column', 'row']}
      spacing={['20px', '30px', '30px']}
      position="relative"
      align={['center', 'center', 'flex-start']}
      minHeight="390px"
      mt={['66px', '66px', '77px']}
      mb={['65px', '65px', '125px']}
      px={['38px', '38px', '94px']}
    >
      <Box
        flexShrink={0}
        sx={{
          width: '285px',
          height: '384px',
          position: 'relative',
        }}
      >
        <BemImage
          width="285px"
          height="384px"
          src={FrontViewWoman}
          alt="Mulher digitando no notebook"
        />
      </Box>

      <Flex flexDirection="column">
        <CustomHeading textStyle="bold32" color="secondary.regular">
          O que é LGPD?
        </CustomHeading>

        <Text textStyle="regular16" color="grey.700" lineHeight="24px" mt={2}>
          A Lei Geral de Proteção de Dados - LGPD, nº 13.709 de 14 de agosto de
          2018, foi criada para regulamentar como as organizações devem tratar
          os dados pessoais, estabelecendo proteção, controle, transparência e
          direitos para os titulares desses dados.
        </Text>

        <Text textStyle="regular16" color="grey.700" lineHeight="24px" mt={6}>
          Exemplos de dados pessoais, que são amparados pela LGPD: Nome, RG,
          CPF, telefone, endereço, etc. Esta lei regula o uso de dados pessoais,
          tanto em meios digitais como na Internet, quanto em formatos
          analógicos como fichas e formulários impressos.
        </Text>

        <Text textStyle="regular16" color="grey.700" lineHeight="24px" mt={6}>
          Seguem alguns pontos abordados por ela:
        </Text>

        <UnorderedList
          ml={4}
          mt={6}
          sx={{
            li: {
              textStyle: 'regular16',
              color: 'grey.700',
              lineHeight: '24px',
            },
          }}
        >
          <ListItem>
            estabelece alguns princípios gerais que todos temos de respeitar;
          </ListItem>
          <ListItem>
            define as hipóteses em que é possível coletar dados pessoais;
          </ListItem>
          <ListItem>
            descreve os requisitos para o adequado tratamento dos dados
            coletados;
          </ListItem>
          <ListItem>
            cria direitos para as pessoas cujos dados são tratados e deveres
            para quem trata esses dados; e
          </ListItem>
          <ListItem>
            prevê sanções para o descumprimento dos itens anteriores.
          </ListItem>
        </UnorderedList>
      </Flex>
    </Stack>
  );
};
