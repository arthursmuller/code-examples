import { FC } from 'react';

import { Flex, Text, Button, Grid } from '@chakra-ui/react';
import Link from 'next/link';

import { CustomHeading } from '@pcf/design-system';
import { SituacaoPropostaModel } from '@pcf/core';
import { RoutesEnum as PublicRoutesEnum } from 'app/routes/routes.enum';

interface ProposalSearchResultCardProps {
  situacaoProposta: SituacaoPropostaModel | null;
}

export const ProposalSearchResultCard: FC<ProposalSearchResultCardProps> = ({
  situacaoProposta,
}) => {
  return (
    <Flex
      layerStyle="card"
      flexDir="column"
      height="auto"
      alignItems="center"
      width="100%"
      maxWidth="822px"
      p={[
        '32px 24px 32px 24px',
        '32px 24px 32px 24px',
        '64px 108px 64px 108px',
      ]}
    >
      <CustomHeading
        as="h2"
        textStyle="bold24_32"
        color="secondary.mid-dark"
        textAlign="center"
        mb={[8, 8, 6]}
      >
        Consulta de Proposta
      </CustomHeading>

      {situacaoProposta?.descricaoSituacao && (
        <CustomHeading
          as="h2"
          textStyle="bold20_24"
          color="primary.regular"
          textAlign="center"
          mb={[8, 8, 6]}
        >
          {situacaoProposta?.descricaoSituacao}
        </CustomHeading>
      )}

      {situacaoProposta?.explicacaoSituacao && (
        <Text
          textStyle="regular20_24"
          textAlign="center"
          lineHeight="40px"
          fontWeight="400"
          mx={['auto', 'auto', '72px']}
        >
          {situacaoProposta?.explicacaoSituacao}
        </Text>
      )}

      <Grid
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
        gap={6}
        w={['100%', '100%', '600px']}
      >
        <Link href={PublicRoutesEnum.SignUp}>
          <Button>Criar uma conta</Button>
        </Link>

        <Link href={PublicRoutesEnum.Root}>
          <Button colorScheme="grey">Retonar ao Início</Button>
        </Link>
      </Grid>
    </Flex>
  );
};
