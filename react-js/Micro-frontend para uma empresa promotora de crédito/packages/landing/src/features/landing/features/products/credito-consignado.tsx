import React, { FC } from 'react';

import { Text, useBreakpointValue, Box, Flex, Icon } from '@chakra-ui/react';

import { InfoSection } from 'features/landing/components/info-section';
import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { FaqList, FaqQuestion } from '@pcf/design-system';
import { LandingTemplate } from 'features/landing/landing-template';
import {
  LogoBemVerticalIcon,
  LogoPcfVerticalIcon,
} from '@pcf/design-system-icons';
import {
  BadgeProps,
  BadgesSection,
} from 'features/landing/components/badges-section';
import { CapturaLeadSection } from 'features/landing/components/captura-lead-section';

import { ContactsSection } from './components/contacts-section';
import Cartao from './assets/credito-consignado@2x.jpg';
import DinheiroBadge from './assets/badges/dinheiro.svg';
import CartaoBadge from './assets/badges/cartao.svg';
import JurosBadge from './assets/badges/juros.svg';
import CarteiraBadge from './assets/badges/carteira.svg';

const badges: BadgeProps[] = [
  {
    icon: DinheiroBadge,
    title: 'DINHEIRO FÁCIL E RÁPIDO',
    text: 'Juros baixos',
    altDescription: 'ilustração de cédulas de dinheiro',
  },
  {
    icon: CartaoBadge,
    title: 'EFETUE SAQUES',
    text: '5% mais crédito para você',
    altDescription: 'ilustração de um cartão de crédito',
  },
  {
    icon: JurosBadge,
    title: 'JUROS REDUZIDOS',
    text: 'Utilize para saque e compras',
    altDescription: 'ilustração de seta apontando para baixo',
  },
  {
    icon: CarteiraBadge,
    title: 'MAIS DINHEIRO PARA VOCÊ',
    text: 'Sem anuidade',
    altDescription: 'ilustração de uma carteira com dinheiro',
  },
];

const questions: FaqQuestion[] = [
  {
    question: 'O que é Crédito ou Empréstimo Consignado?',
    answer: `
      O crédito consignado surgiu através da Medida Provisória nº 130/2.003, com o objetivo de diminuir as dívidas de trabalhadores, aposentados, pensionistas e servidores públicos. A proposta do empréstimo consignado era possibilitar o financiamento com juros mais baixos e, desde então, a modalidade passou por diversas mudanças e se tornou mais popular, pois se apresenta como a forma de empréstimo menos burocrática e mais em conta que as demais, por ser descontado diretamente da folha de pagamento do cliente. Entao nao ha riscos de inadimplência.

      Além disso, possui diversas outras vantagens, como menores taxas de juros, facilidade para solicitar e prazos de pagamentos mais longos.
    `,
  },
  {
    question: 'Como funciona o Empréstimo Consignado?',
    answer:
      'Na contratação de empréstimos, o cliente pode solicitar até 30% do seu benefício, sem a necessidade de dizer qual será a finalidade. Basta solicitar uma simulação ou ir até uma das nossa lojas espalhadas pelo Brasil. Através do seu convênio, as parcelas serão descontadas automaticamente da conta do cliente todo mês. O cliente pode ainda agregar mais 5% de margem com o cartão consignado.',
  },
  {
    question: 'Qual é a taxa de juros do Empréstimo Consignado?',
    answer:
      'Dentre as vantagens do Crédito Consignado, está a de que os juros são os mais baixos dentro da modalidade de crédito pessoal, como cartão de crédito e cheque especial. Para saber mais, faça uma solicitação para um dos nossos consultores especializados.',
  },
  {
    question: 'Quem pode contratar o Crédito Consignado?',
    answer: `
      O crédito consignado pode ser contratado pelos seguintes públicos:

      • Aposentados e Pensionistas INSS (Instituto Nacional do Seguro Social);
      • Servidores Públicos (Federais, Estaduais e Municipais);
      • Militares das Forças Armadas; e
      • Trabalhadores de empresas privadas.

      A modalidade de crédito também é uma opção para pessoas que ficaram com o “nome sujo” e negativados, pois não é feita uma consulta ao SPC ou SERASA.
    `,
  },
  {
    question: 'Quais são os prazos para pagamento?',
    answer: `
      • Servidores públicos, até 96 meses;
      • Aposentados e Pensionistas do INSS, até 84 meses;
      • Trabalhadores de empresas privadas, em média até 48 meses.

      ** AS REGRAS MUDAM A TODA HORA. REGRAS DO DIA 02/06/2020 –
    `,
  },
  {
    question: 'Qual a documentação necessária para contratação?',
    answer: `
      • Documento de identificação com foto (RG ou CNH);
      • Cadastro de Pessoa Física (CPF);
      • Comprovante de residência;
      • Contracheque atualizado ou extrato do INSS.

      * CADA PROMOTORA DE VENDA SOLICITA DOCUMENTACOES DIFERENTES...
    `,
  },
];

const Bold: FC<{ textStyle?: string; color?: string | string[] }> = ({
  textStyle = 'bold24',
  color = 'primary.regular',
  children,
}) => (
  <Box as="span" textStyle={textStyle} color={color}>
    {children}
  </Box>
);

export const CreditoConsignado: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={Cartao}
        backgroundImageAlt="Mão segurando um cartão de crédito"
        position={['33%', 'right', 'right']}
      >
        <SubPageHeader.Title title="Crédito Consignado" />
        <SubPageHeader.Subtitle
          subtitleOrange="Empréstimo consignado"
          subtitle="é na Bem Promotora!"
        />
      </SubPageHeader>
      <InfoSection>
        <Text textStyle="regular20" color="grey.700" lineHeight="28px">
          O empréstimo consignado (ou crédito consignado) é uma boa opção para
          quem precisa de dinheiro extra (ou dinheiro a mais). Para quem precisa
          de dinheiro de maneira rápida, fácil e sem burocracias, esta pode ser
          a melhor solução. Seja para reformar a casa, lidar com os gastos do
          dia a dia, pagar as contas, etc (para realizar alguns de seus sonhos.
          Reformar a casa ou apartamento, dar a entrada em um carro ou
          apartamento, pagar as dividas do cartão de crédito ou do cheque
          especial, pagar aquele presente, ....)..{' '}
          <Bold textStyle="bold20">
            A Bem Promotora te ajuda a realizar os seus sonhos!
          </Bold>
        </Text>
      </InfoSection>

      <Flex
        direction={['column', 'column', 'row']}
        alignItems={['center', 'center', 'center', 'center', 'start']}
        justifyContent="center"
        paddingX={[0, 0, '100px', '180px']}
        marginY={10}
      >
        <Icon
          as={
            isMobile !== undefined &&
            (isMobile ? LogoPcfVerticalIcon : LogoBemVerticalIcon)
          }
          width={['180px']}
          height="fit-content"
          marginRight={[0, 0, '100px']}
          zIndex={1}
        />
        <Flex
          direction="column"
          background={['primary.gradient', 'primary.gradient', 'none']}
          paddingX={[10, 10, 0]}
          paddingTop={['124px', '124px', 0]}
          paddingBottom={[5, 5, 0]}
          marginTop={['-100px', '-100px', 0]}
        >
          <Text
            textStyle="bold40"
            color="secondary.regular"
            marginBottom={6}
            display={['none', 'none', 'block']}
          >
            A Bem Promotora
          </Text>

          <Text textStyle="regular24" color={['white', 'white', 'grey.800']}>
            A Bem Promotora é{' '}
            <Bold color={['white', 'white', 'primary.regular']}>
              especialista em Crédito Consignado
            </Bold>{' '}
            para aposentados e pensionistas do INSS e funcionários públicos
            federais SIAPE. Somos referência quando o assunto é agilidade. E o
            melhor: oferecemos condições que cabe no seu bolso.{' '}
            <Bold color={['white', 'white', 'primary.regular']}>
              Você pode fazer sua simulação de empréstimo consignado agora
              mesmo!
            </Bold>
          </Text>
        </Flex>
      </Flex>

      <BadgesSection badges={badges} alignBadges="center" />

      <CapturaLeadSection />

      {/* is other form of instance */}
      {/* <SimulateBanner title="Cartão Consignado" product="CCC" /> */}

      <FaqList
        chakraProps={{ marginX: [5, 5, '100px'], marginY: '50px' }}
        questions={questions}
      />

      <ContactsSection
        contacts={[
          { number: '4002-0040', title: 'Capitais e regiões metropolitanas' },
          { number: '0800 285 3000', title: 'Demais localidades' },
          { number: '0800 286 0110', title: 'SAC' },
        ]}
      />
    </LandingTemplate>
  );
};

export { CreditoConsignado as default };
