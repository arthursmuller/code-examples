import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { FaqList } from '@pcf/design-system';
import { LandingTemplate } from 'features/landing/landing-template';

import { faqQuestions } from './faq-questions';
import { ContactsSection } from './components/contacts-section';
import { SimulateBanner } from './components/simulate-banner';
import Aposentados from './assets/img_aposentados@2x.jpg';

import { AdvantagesSection } from '../../components/advantages-section/advantages-section';

const title = 'Aposentados e Pensionistas';

export const AposentadosPensionistas: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={Aposentados}
        backgroundImageAlt="Uma mulher de cabelos escuros e presos, encostada em um homem de barba branca e óculos de grau"
        position={['-672px 0', '-672px 0', 'right top']}
      >
        <SubPageHeader.Title title={title} />
        <SubPageHeader.Subtitle />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title>Empréstimo Consignado é na</InfoSection.Title>
        <InfoSection.Description>
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            A Bem Promotora é especialista em Crédito Consignado para
            aposentados e pensionistas do INSS. Oferecemos empréstimo com
            desconto em folha de maneira fácil, rápida e segura para você
            utilizá-lo do melhor jeito que desejar.
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <AdvantagesSection />

      <SimulateBanner product="CC" />

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '50px' }}
        questions={faqQuestions}
      />

      <ContactsSection
        contacts={[
          { number: '4002-0040', title: '(capitais e regiões metropolitanas)' },
          { number: '0800 285 3000', title: '(demais localidades)' },
          { number: '0800 286 0110', title: 'SAC' },
        ]}
      />
    </LandingTemplate>
  );
};

export { AposentadosPensionistas as default };
