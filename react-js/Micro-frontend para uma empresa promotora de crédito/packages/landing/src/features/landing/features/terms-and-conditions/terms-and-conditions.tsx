import React, { FC } from 'react';

import {
  useBreakpointValue,
  Text,
  Box,
  OrderedList,
  ListItem,
} from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header';
import { CustomHeading } from '@pcf/design-system';

import PrivacyPolicyBanner from './assets/privacy-policy-banner.jpg';
import PrivacyPolicyBannerMob from './assets/privacy-policy-banner-mob.jpg';

const paragraphs = [
  {
    id: 1,
    text: 'A Bem Promotora não garante que o conteúdo, os instrumentos e os materiais contidos, utilizados e oferecidos neste website estejam precisamente atualizados ou completos, e não se responsabiliza por danos causados por eventuais erros de conteúdo ou falhas de equipamento.',
  },
  {
    id: 2,
    text: 'A Bem Promotora não se responsabiliza, expressa ou tacitamente, pelo uso indevido das informações, dos instrumentos, dos materiais disponibilizados e/ou dos equipamentos utilizados por este website, para quaisquer que sejam os fins, feito por qualquer usuário, sendo de inteira responsabilidade desse as eventuais lesões a direito próprio ou de terceiros, causadas ou não por esse uso inadequado.',
  },
  {
    id: 3,
    text: 'Em nenhuma circunstância, a Bem Pomotora, seus diretores ou funcionários serão responsáveis por quaisquer danos diretos ou indiretos, especiais, incidentais ou de consequência, perdas ou despesas oriundos da conexão com este website ou uso da sua parte ou incapacidade de uso por qualquer parte, ou com relação a qualquer falha de desempenho, erro, omissão, interrupção, defeito ou demora na operação ou transmissão, vírus de computador ou falha da linha ou do sistema, mesmo se a Bem Promotora ou seus representantes estejam avisados da possibilidade de tais danos, perdas ou despesas.',
  },
  {
    id: 4,
    text: 'O adequado provimento de todos os recursos da Internet, sem exceção, é de inteira responsabilidade do usuário do website.',
  },
  {
    id: 5,
    text: 'A Bem Promotora não se responsabiliza pelo conteúdo de outros websites (a) cujos endereços estejam disponíveis nas páginas deste website, ou (b) cujo endereço deste website esteja neles disponível. A Bem Promotora não garante o ressarcimento de quaisquer danos causados pelos websites nesse item referidos.',
  },
  {
    id: 6,
    text: 'Sobre troca de mensagens entre o usuário e a Bem Promotora por meio da Internet favor consultar a Política de Privacidade.',
  },
];

export const PrivacyPolicy: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <>
      <SubPageHeader
        backgroundImage={
          isMobile !== undefined &&
          (isMobile ? PrivacyPolicyBannerMob : PrivacyPolicyBanner)
        }
        backgroundImageAlt="Mãos digitando em um teclado"
      >
        <SubPageHeader.Title
          title="Termos e Condições"
          width={['270px', '270px', '680px']}
        />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle="É Seguro! É Tranquilo!  É Bem Promotora!"
          width={['220px', '220px', '500px']}
        />
      </SubPageHeader>

      <Box p={[10, 10, '40px  87px 80px 150px']}>
        <Text textStyle="regular16_20" mt={2}>
          A navegação e consulta pública deste website se sujeita aos Termos e
          Condições abaixo:
        </Text>

        <CustomHeading color="secondary.regular" textStyle="bold32_40" my={8}>
          Garantias, Responsabilidades e Danos
        </CustomHeading>
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
      </Box>
    </>
  );
};
