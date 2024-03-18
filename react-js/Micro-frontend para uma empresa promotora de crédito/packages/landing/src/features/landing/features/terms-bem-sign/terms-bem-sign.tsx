import React, { FC } from 'react';

import {
  useBreakpointValue,
  Text,
  Flex,
  Tabs,
  Tab,
  TabList,
  TabPanels,
  TabPanel,
} from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header';
import { InfoSection } from 'features/landing/components/info-section';

import TermsBemSignBanner from './assets/termos-bemsign@2x.jpg';
import { Glossary, Terms } from './components';

const tabStyle = {
  fontWeight: 'bold',
  borderColor: 'primary.regular',
  color: 'primary.regular',
};

export const TermsBemSign: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <>
      <SubPageHeader
        backgroundImage={TermsBemSignBanner}
        backgroundImageAlt="Mãos segurando uma caneta e assinando um documento"
        position={['57%', '57%', 'right top']}
      >
        <SubPageHeader.Title
          title="Termo de uso e privacidade
          para uso da Bemsign"
          width={['270px', '270px', '800px']}
        />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle={
            isMobile
              ? 'É Seguro! É Tranquilo! É Bem Promotora!'
              : 'O BEMSIGN é disponibilizado mediante acesso à internet e tem por objetivo a realização do processo de assinatura eletrônica de usuário na contratação de serviços e/ou produtos.'
          }
          width={['220px', '220px', '1100px']}
        />
      </SubPageHeader>

      {!isMobile && (
        <InfoSection>
          <InfoSection.Description afterText="" showSlogan={false}>
            <Text
              textStyle="regular16_20"
              color="grey.700"
              lineHeight={[6, 6, 8]}
            >
              A Solução/Ferramenta “BEMSIGN” é de propriedade da BEM PROMOTORA
              DE VENDAS E SERVIÇOS S/A, pessoa jurídica de direito privado,
              inscrita no CNPJ 10.397.031/0001-81, com sede Rua Siqueira Campos,
              1163 – 9° andar, bairro Centro Histórico, Porto Alegre/RS, neste
              ato denominada por seu nome fantasia BEM PROMOTORA.
            </Text>

            <Text color="secondary.regular" textStyle="bold20" mt={6}>
              O presente Termo de Uso e Privacidade regula a utilização da
              Solução BEMSIGN pelo usuário, bem como o tratamento de seus dados
              pessoais realizados pela BEM PROMOTORA, entendendo-se por BEMSIGN
              todas as páginas web consubstanciadas no domínio
              https://www.bempromotora.com.br e seus subdomínios, e por USUÁRIO
              toda pessoa natural que o utiliza ou acessa, estabelecendo seus
              direitos e obrigações. O uso da solução implica no reconhecimento,
              aceitação plena, irrevogável e integral de todos os termos a
              seguir:
            </Text>
          </InfoSection.Description>
        </InfoSection>
      )}

      <Flex p={['20px 16px', '32px 16px', '10px 133px 40px 133px']}>
        <Tabs variant="line" defaultIndex={0} maxW="100%">
          <TabList>
            <Tab fontSize={['14px', '14px', '16px']} _selected={tabStyle}>
              Termos de uso
            </Tab>
            <Tab fontSize={['14px', '14px', '16px']} _selected={tabStyle}>
              Glossário
            </Tab>
          </TabList>

          <TabPanels maxW="100%">
            <TabPanel maxW="100%">
              <Terms />
            </TabPanel>
            <TabPanel>
              <Glossary />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Flex>
    </>
  );
};
