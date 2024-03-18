import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { FaqList } from '@pcf/design-system';
import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { LandingTemplate } from 'features/landing/landing-template';

import { faqQuestions } from './faq-questions';
import { ContactsSection } from './components/contacts-section';
import { SimulateBanner } from './components/simulate-banner';
import Funcionarios from './assets/img_funcionarios@2x.jpg';

import { AdvantagesSection } from '../../components/advantages-section/advantages-section';

const title = 'Funcionários Públicos';

export const FuncionarioPublico: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={Funcionarios}
        backgroundImageAlt="Mãos digitando em um teclado de notebook"
        position={['-608px 0', 'right', 'right']}
      >
        <SubPageHeader.Title title={title} />
        <SubPageHeader.Subtitle />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title>Empréstimo Consignado é na</InfoSection.Title>
        <InfoSection.Description>
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            A Bem Promotora é especialista em Crédito Consignado para
            funcionários públicos. Oferecemos empréstimo com desconto em folha
            de maneira fácil, segura e barata para você utilizá-lo do melhor
            jeito que desejar.
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <Text
        textAlign={['start', 'start', 'center']}
        textStyle="regular20"
        lineHeight="36px"
        color="grey.700"
        mx={['42px', '42px', '42px', '190px', '300px']}
        mb={['74px', '74px', '84px']}
      >
        Aproveite e contrate aqui o{' '}
        <Text as="b" color="primary.regular">
          seguro exclusivo para os conveniados SIAPE.{' '}
        </Text>
        A pessoa segurada contará com a tranquilidade de ter a sua dívida
        quitada, caso aconteça algum imprevisto. Além disso, com a contratação
        deste seguro você concorre a prêmios mensais! Confira!
      </Text>

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

export { FuncionarioPublico as default };
