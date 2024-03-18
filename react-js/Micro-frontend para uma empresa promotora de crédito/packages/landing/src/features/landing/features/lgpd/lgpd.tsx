import { FC } from 'react';

import { Button, Link, Divider, Text, Box, Center } from '@chakra-ui/react';
import NextLink from 'next/link';

import { RoutesEnum } from 'app/routes/routes.enum';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { InfoSection } from 'features/landing/components/info-section';
import { FaqList } from '@pcf/design-system';

import LGPDBanner from './assets/lgpd@2x.jpg';
import { DPOOfficerSection, PrivacySection, WhatIsLGPD } from './components';
import { lgpdQuestions } from './lgpd.consts';

const firstTwoQuestions = lgpdQuestions.slice(0, 2);
const otherQuestions = lgpdQuestions.slice(2, lgpdQuestions.length);

export const LGPD: FC = () => {
  return (
    <>
      <SubPageHeader
        backgroundImage={LGPDBanner}
        backgroundImageAlt="dados"
        position={['54%', '54%', 'right']}
      >
        <SubPageHeader.Title
          width={['90%', '603px', '603px']}
          title="LGPD - LEI GERAL DE PROTEÇÃO DE DADOS"
        />
        <SubPageHeader.Subtitle
          subtitle="O objetivo dessa lei é proteger os seus direitos de liberdade e privacidade."
          subtitleOrange=""
          width={['90%', '603px', '603px']}
        />
      </SubPageHeader>

      <InfoSection marginTop={['-70px', '-70px', '-20px', '-20px']}>
        <InfoSection.Description showSlogan={false} afterText="">
          <Text textStyle="regular20" color="grey.700" lineHeight="32px">
            A Bem Promotora sempre teve como prioridade a proteção dos dados
            pessoais de nossos clientes e colaboradores. A LGPD, que estabelece
            regras sobre como os dados pessoais devem ser tratados nos meios
            físicos ou digitais, consolida nosso compromisso e precauções
            tomadas.
          </Text>

          <Text textStyle="regular20" color="grey.700" lineHeight="32px" mt={6}>
            Para que que você possa compreender melhor do que trata a LGPD,
            consulte os principais conceitos que disponibilizamos a seguir.
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <WhatIsLGPD />

      <PrivacySection />

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '50px' }}
        questions={firstTwoQuestions}
      />

      <Divider
        borderBottomWidth="2px"
        justifySelf="center"
        alignSelf="center"
        w="70%"
        mb="95px"
      />

      <DPOOfficerSection />

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '85px' }}
        questions={otherQuestions}
      />

      <Text
        px={6}
        textAlign="center"
        textStyle="bold32"
        color="secondary.mid-dark"
      >
        Acesse seus dados pessoais
      </Text>

      <Text
        px={6}
        textAlign="center"
        color="grey.700"
        textStyle="regular20"
        mt={6}
      >
        Obtenha suas informações pessoais dentro da Bem. Clique no botão abaixo,
        preencha as informações necessárias e entraremos em contato.
      </Text>

      <Center px={6} mb="80px" mt="30px">
        <Box>
          <NextLink passHref href={RoutesEnum.PersonalData}>
            {/* eslint-disable-next-line */}
            <Button as={Link} variant="outline" w="auto">
              Meus dados pessoais
            </Button>
          </NextLink>
        </Box>
      </Center>
    </>
  );
};
