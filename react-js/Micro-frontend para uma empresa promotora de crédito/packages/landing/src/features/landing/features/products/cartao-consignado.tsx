import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { FaqList } from '@pcf/design-system';
import { LandingTemplate } from 'features/landing/landing-template';

import { SimulateBanner } from './components/simulate-banner';
import { ContactsSection } from './components/contacts-section';
import { BanrisulCardBanner } from './components/banrisul-card-banner';
import { faqQuestions } from './faq-questions';
import Cartao from './assets/img_cartaoconsignado@2x.jpg';

import { AdvantagesSection } from '../../components/advantages-section/advantages-section';

const title = 'Cartão Consignado';

export const CartaoConsignado: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={Cartao}
        backgroundImageAlt="Mão segurando um cartão de crédito"
        position={['-834px', 'right', 'right']}
      >
        <SubPageHeader.Title title={title} />
        <SubPageHeader.Subtitle />
      </SubPageHeader>
      <InfoSection>
        <InfoSection.Title>Cartão Consignado é na</InfoSection.Title>
        <InfoSection.Description showSlogan={false}>
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            A Bem Promotora é especialista em Crédito Consignado para
            aposentados e pensionistas do INSS e Servidores Públicos Federais
            SIAPE.
          </Text>

          <Text
            textStyle="regular20"
            color="grey.700"
            lineHeight="28px"
            mt={[2, 2, 2, 8]}
          >
            Oferecemos o Cartão Consignado com ótimos benefícios para você. A
            Bem Promotora é referência em agilidade e atendimento, contando com
            os sistemas e processos mais ágeis do mercado.
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <BanrisulCardBanner />

      <AdvantagesSection />

      <SimulateBanner title="Cartão Consignado" product="CCC" />

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '50px' }}
        questions={faqQuestions}
      />

      <ContactsSection
        subLabel={false}
        contacts={[{ number: '0800 701 6888', title: ' (Cartão Consignado)' }]}
      />
    </LandingTemplate>
  );
};

export { CartaoConsignado as default };
