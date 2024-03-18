import React, { FC } from 'react';

import { Box, Text } from '@chakra-ui/react';

import { FaqList, FaqQuestion } from '@pcf/design-system';
import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { LandingTemplate } from 'features/landing/landing-template';
import {
  BadgeProps,
  BadgesSection,
} from 'features/landing/components/badges-section';
import { CapturaLeadSection } from 'features/landing/components/captura-lead-section';

import { ContactsSection } from './components/contacts-section';
import CapitalizacaoBanner from './assets/capitalizacao_banner@2x.jpg';
import CofreBadge from './assets/badges/cofre.svg';
import ApulhetaBadge from './assets/badges/ampulheta.svg';
import DiamanteBadge from './assets/badges/diamanete.svg';
import CarteiraBadge from './assets/badges/carteira-dinheiro.svg';

const badges: BadgeProps[] = [
  {
    icon: CarteiraBadge,
    title: 'CABE NO SEU BOLSO',
    altDescription: 'ilustração carteira',
  },
  {
    icon: DiamanteBadge,
    title: 'CONCORRA A PRÊMIOS SEMANAIS',
    altDescription: 'ilustração diamante',
  },
  {
    icon: CofreBadge,
    title: 'ECONOMIZE E RESGATE',
    altDescription: 'ilustração cofre',
  },
  {
    icon: ApulhetaBadge,
    title: 'PRAZOS\nFLEXÍVEIS',
    altDescription: 'ilustração ampulheta',
  },
];

const questions: FaqQuestion[] = [
  {
    question: 'O que é empréstimo consignado?',
    answer: `
      É uma modalidade de empréstimo em que o desconto da prestação é feito diretamente na folha de pagamento ou benefício previdenciário do contratante. O dinheiro é depositado diretamente na sua conta e descontado automaticamente sem você ter que se preocupar em perder o pagamento.
    `,
  },
  {
    question: 'A taxa de juros do consignado é alta?',
    answer:
      'O empréstimo consignado possui uma das menores taxas de juros em relação as linhas de empréstimos do mercado. A Bem Promotora possui taxas competitivas e uma ampla rede de lojas que está a sua disposição para orientar e auxiliar você em busca de um bom negócio.',
  },
  {
    question: 'Quais são os convênios atendidos pela Bem Promotora?',
    answer:
      'A Bem Promotora realiza empréstimos para aposentados e pensionistas do INSS e servidores federais SIAPE. Procure uma loja perto de você! :)',
  },
  {
    question:
      'Quais tipos de empréstimos que a Bem Promotora pode me oferecer?',
    answer: `
      Trabalhamos com todas modalidades: Portabilidade, Operações de Margem Livre, e Refinanciamento. Portabilidade: Consiste em migrar o contrato do cliente de uma instituição para outra. A Portabilidade é a atual compra de dívida, regulamentada pela Resolução nº 4.292 do Banco Central.
    `,
  },
  {
    question: 'Em quanto tempo o dinheiro estará na minha conta?',
    answer: `
      A Bem Promotora conta com uma equipe treinada e os sistemas mais ágeis do mercado para liberar seu dinheiro com facilidade e rapidez. Após a análise de crédito aguardamos seu convênio para liberação da verba, que pode variar entre 2 e 12 dias.
    `,
  },
];

const title = 'Título de capitalização';

export const TituloCapitalizacao: FC = () => {
  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={CapitalizacaoBanner}
        backgroundImageAlt="Homem negro com camiseta branca olhando para a tela de um celular que está em suas mãos"
        position={['-672px 0', '-672px 0', 'right top']}
      >
        <SubPageHeader.Title title={title} />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle="Quem tem título de capitalização fica bem!"
        />
      </SubPageHeader>

      <InfoSection>
        <InfoSection.Title>Título de capitalização é na</InfoSection.Title>
        <InfoSection.Description showSlogan={false} afterText="">
          <Text textStyle="regular20" color="grey.700" lineHeight="28px">
            A Bem Promotora oferece o Título de Capitalização a todos seus
            clientes. Com o CAP Premiado você{' '}
            <Box as="span" textStyle="bold20" color="primary.regular">
              economiza e concorre a prêmios semanais
            </Box>
            ! Venha nos visitar ou ligue para agendar uma consulta com
            especialistas da Bem.
          </Text>
        </InfoSection.Description>
      </InfoSection>

      <BadgesSection badges={badges} alignBadges="center" />

      <CapturaLeadSection />

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '50px' }}
        questions={questions}
      />

      <ContactsSection
        subLabel="(CAP Premiado):"
        contacts={[
          { number: '4002-0040', title: '(capitais e regiões metropolitanas)' },
          { number: '0800 285 3000', title: '(demais localidades)' },
          { number: '0800 286 0110', title: 'SAC' },
          { number: '0800 286 0047', title: 'Ouvidoria' },
        ]}
      />
    </LandingTemplate>
  );
};

export { TituloCapitalizacao as default };
