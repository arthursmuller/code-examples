import { FC } from 'react';

import { Box, Text } from '@chakra-ui/react';

import { BadgesSection } from 'features/landing/components/badges-section';
import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { LandingTemplate } from 'features/landing/landing-template';

import { AboutBanner } from './components/about-banner';
import MesaTrabalho from './assets/sobre-a-bem@2x.jpg';

export const AboutBem: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={MesaTrabalho}
        backgroundImageAlt="Homem trabalhando em um notebook"
        position={['82%', '82%', 'right']}
      >
        <SubPageHeader.Title title="Sobre a BEM" />
        <SubPageHeader.Subtitle />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title subtitle="Transformar oportunidades em realizações.">
          Propósito
        </InfoSection.Title>
        <InfoSection.Description afterText="" showSlogan={false}>
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            Muito mais do que prestar serviços financeiros com excelência, ser
            Bem Promotora é promover o bem-estar das pessoas acima de tudo. E
            quer melhor jeito de fazer isso do que{' '}
            <Box as="span" textStyle="bold20" color="primary.regular">
              ajudando-as a realizarem suas conquistas
            </Box>
            ?
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <AboutBanner />

      <BadgesSection
        iconsSize="64px"
        widths={['212px', '212px', '335px']}
        chakraProps={{
          maxWidth: '1200px',
        }}
      />
    </LandingTemplate>
  );
};
