import { FC } from 'react';

import { Flex, Text } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';

export const TermsAndConditions: FC = () => (
  <Flex direction="column">
    <CustomHeading
      as="h2"
      textStyle="bold24_32"
      color="secondary.mid-dark"
      marginBottom="24px"
    >
      Termos e condições
    </CustomHeading>

    <Flex
      marginBottom="24px"
      maxH="300px"
      flex={1}
      direction="column"
      overflow="auto"
      layerStyle="card"
    >
      <Text as="p" textStyle="bold16" mb="24px">
        Garantias, Responsabilidades e Danos
      </Text>

      <Text as="p" mb="16px" textStyle="regular12">
        1. A Bem Promotora não garante que o conteúdo, os instrumentos e os
        materiais contidos, utilizados e oferecidos neste website estejam
        precisamente atualizados ou completos, e não se responsabiliza por danos
        causados por eventuais erros de conteúdo ou falhas de equipamento.
      </Text>

      <Text as="p" mb="16px" textStyle="regular12">
        2. A Bem Promotora não se responsabiliza, expressa ou tacitamente, pelo
        uso indevido das informações, dos instrumentos, dos materiais
        disponibilizados e/ou dos equipamentos utilizados por este website, para
        quaisquer que sejam os fins, feito por qualquer usuário, sendo de
        inteira responsabilidade desse as eventuais lesões.
      </Text>

      <Text as="p" textStyle="regular12">
        3. Em nenhuma circunstância, a Bem Pomotora, seus diretores ou
        funcionários serão responsáveis por quaisquer danos diretos ou
        indiretos, especiais, incidentais ou de consequência, perdas ou despesas
        oriundos da conexão com este website ou uso da sua parte ou incapacidade
        de uso por qualquer parte, ou com relação a qualquer falha de
        desempenho, erro, omissão, interrupção, defeito ou demora na operação ou
        transmissão, vírus de computador ou falha da linha ou do sistema, mesmo
        se a Bem Promotora ou seus representantes estejam avisados da
        possibilidade de tais danos, perdas ou despesas.
      </Text>
    </Flex>
  </Flex>
);
