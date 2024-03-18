import { FC } from 'react';

import { Box, Text } from '@chakra-ui/react';

import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { LandingTemplate } from 'features/landing/landing-template';

import { CorrespondentForm } from './components/correspondent-form';
import NotebookImg from './assets/correspondente@2x.jpg';

const Strong: FC = ({ children }) => (
  <Box as="span" textStyle="bold20" color="primary.regular">
    {children}
  </Box>
);

export const Correspondent: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={NotebookImg}
        backgroundImageAlt="Notebook"
        position={['68%', '68%', 'right']}
      >
        <SubPageHeader.Title
          title="Seja nosso correspondente"
          width={['100%', '345px', '465px', '465px']}
        />
        <SubPageHeader.Subtitle
          subtitle="Ofereça conveniência e segurança para clientes"
          subtitleOrange=""
        />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title subtitle="">
          Associe-se à Bem Promotora e veja o seu negócio crescer.
        </InfoSection.Title>
        <InfoSection.Description afterText="" showSlogan={false}>
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            Conte com nosso <Strong>portfólio de produtos</Strong> e o{' '}
            <Strong>melhor e mais ágil</Strong> suporte para correspondentes do
            mercado. Aqui nós ajudamos você a{' '}
            <Strong>
              aumentar suas vendas e tornar seu estabelecimento ainda mais
              completo
            </Strong>
            . Venha ser nosso parceiro de negócios!
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <CorrespondentForm />
    </LandingTemplate>
  );
};
