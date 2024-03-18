import React, { FC } from 'react';

import {
  useBreakpointValue,
  Text,
  Box,
  OrderedList,
  ListItem,
} from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header';
import { InfoSection } from 'features/landing/components/info-section';

import PrivacyPolicyBanner from './assets/politica-privacidade@2x.jpg';

const paragraphs = [
  {
    id: 1,
    text: 'Quaisquer informações que os usuários passarem serão coletadas e guardadas de acordo com padrões rígidos de segurança e confidencialidade.',
  },
  {
    id: 2,
    text: 'As informações pessoais que forem passadas à Companhia pelos usuários serão coletadas por meios éticos e legais, podendo ter um ou mais propósitos, sobre os quais os usuários serão informados.',
  },
  {
    id: 3,
    text: 'Os usuários serão avisados de quais informações suas estarão sendo coletadas antes do instante dessa coleta, ficando a opção de escolha para fornecimento ou não dessas informações sob responsabilidade do usuário, o qual também terá ciência das conseqüências de sua decisão.',
  },
  {
    id: 4,
    text: 'A menos que a Bem Promotora receba determinação legal ou judicial, suas informações nunca serão transferidas a terceiros ou usadas para finalidades diferentes daquelas para as quais foram coletadas.',
  },
  {
    id: 5,
    text: 'O acesso às informações coletadas está restrito apenas a funcionários autorizados para o uso adequado dessas informações. Os funcionários que se utilizarem indevidamente dessas informações, ferindo essa Política de Privacidade, estarão sujeitos às penalidades do processo disciplinar da Bem Promotora.',
  },
  {
    id: 6,
    text: 'A Companhia manterá íntegras as informações que forem fornecidas pelos visitantes.',
  },
  {
    id: 7,
    text: 'Este website contem links ou frames de outros sites, que podem ou não ser parceiros da Companhia e aliados. Esses links e frames são disponibilizados buscando, tão somente, proporcionar mais um benefício para os usuários. Vale ressaltar que a inclusão desses links e frames não significa que a Bem Promotora tenha conhecimento, concorde ou seja responsável por eles ou por seus respectivos conteúdos. Portanto, a Bem Promotora não pode ser responsabilizada por eventuais perdas ou danos sofridos em razão de utilização dos referidos links ou frames.',
  },
  {
    id: 8,
    text: 'Sempre que outras organizações forem contratadas para prover serviços de apoio, será exigida a adequação aos padrões de privacidade da Bem Promotora.',
  },
  {
    id: 9,
    text: 'Para fins administrativos, eventualmente a Companhia poderá utilizar ‘cookies(*), sendo que o usuário pode, a qualquer instante, ativar em seu navegador mecanismos para informá-lo quando os mesmos estiverem acionados ou para evitar que sejam acionados.',
  },
  {
    id: 10,
    text: 'Outras importantes informações sobre os termos e condições de utilização deste website estão disponíveis em Termos e Condições.',
  },
  {
    id: 11,
    text: 'Informamos que a ACESSO DIGITAL TECNOLOGIA DA INFORMAÇÃO S.A. poderá receber e armazenar alguns dados pessoais e biométricos para fins de promover maior segurança no uso de sua identidade e prevenir o uso indevido dos seus dados. Se desejar mais informações, acesse: https://unico.io/politica-de-privacidade',
  },
];

export const PrivacyPolicy: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <>
      <SubPageHeader
        backgroundImage={PrivacyPolicyBanner}
        backgroundImageAlt="Mão digitando em um teclado com papel e caneta ao lado"
        position={['15%', '15%', 'right']}
      >
        <SubPageHeader.Title
          title="POLÍTICA DE PRIVACIDADE"
          width={['270px', '270px', '680px']}
        />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle="É Seguro! É Tranquilo!  É Bem Promotora!"
          width={['220px', '220px', '500px']}
        />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title
          subtitle={isMobile ? 'da Bem Promotora' : undefined}
          headingProps={{
            w: ['200px', '200px', '363px'],
            textStyle: isMobile ? 'bold32' : 'bold40',
          }}
        >
          Política de Privacidade {!isMobile ? 'da' : ''}
        </InfoSection.Title>
        <InfoSection.Description afterText="" showSlogan={false}>
          <Text
            textStyle="regular16_20"
            color="grey.700"
            lineHeight={[6, 6, 8]}
          >
            <Text as="span" textStyle="bold16_20" color="primary.regular">
              A Política de Privacidade da Bem Promotora
            </Text>{' '}
            foi criada para demonstrar o compromisso da Companhia com a
            segurança e a privacidade de informações de usuários de serviços
            interativos aqui disponíveis. Usuários podem visitar este website e
            conhecer os serviços que a Bem Promotora oferece, obter informações
            e notícias,{' '}
            <Text as="span" textStyle="bold16_20" color="primary.regular">
              sem fornecer informações pessoais.
            </Text>{' '}
            Caso você forneça alguma informação, essa política procura
            esclarecer como a Bem Promotora{' '}
            <Text as="span" textStyle="bold16_20" color="primary.regular">
              coleta e trata suas informações individuais.
            </Text>{' '}
            Recomenda-se a verificação temporária dessa política,{' '}
            <Text as="span" textStyle="bold16_20" color="primary.regular">
              que está sujeita a alterações sem prévio aviso.
            </Text>
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <Box p={['0 24px', '0 24px', '20px  87px 45px 150px']}>
        <OrderedList>
          {paragraphs.map(({ id, text }) => (
            <ListItem
              pl={[1, 1, 6]}
              key={id}
              textStyle="regular16_20"
              mt={[8, 8, 6]}
              color="grey.700"
              sx={{
                '::marker': {
                  color: 'primary.regular',
                  textStyle: !isMobile ? 'bold20' : 'regular16',
                },
              }}
            >
              {text}
            </ListItem>
          ))}
        </OrderedList>

        <Text
          textStyle="regular16_20"
          color="grey.700"
          mt={[8, 8, 6]}
          mb={6}
          ml={[-1, -1, -6]}
        >
          <Text as="span" textStyle="bold16_20" color="primary.regular">
            (*) Cookie:
          </Text>{' '}
          pequeno arquivo colocado em seu computador para rastrear movimentos
          dentro dos websites.
        </Text>
      </Box>
    </>
  );
};
