import { FC } from 'react';

import { Text, Box, Grid } from '@chakra-ui/react';
import { formatCEP } from '@brazilian-utils/brazilian-utils';

import { RadioCardLogicProps, RadioCardVariant } from '@pcf/design-system';
import { CepModel } from '@pcf/core';

const mobileTemplateAreas = `
"logradouro"
"bairro"
"cidade"
"uf"
`;

const desktopTemplateAreas = `
"logradouro         cidade"
"bairro             uf"
`;

export interface CepSearchResultCardProps extends RadioCardLogicProps {
  cep: CepModel;
}

export const CepSearchResultCard: FC<CepSearchResultCardProps> = ({
  cep,
  ...props
}) => {
  return (
    <RadioCardVariant
      {...props}
      header={
        <>
          <Text textStyle="regular16" color="white" flex={1}>
            {cep.descricaoApoio && `${cep.descricaoApoio} | `}
            CEP {formatCEP(cep.cep || '')}
          </Text>
        </>
      }
      content={
        <Grid
          gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
          gridTemplateAreas={[
            mobileTemplateAreas,
            mobileTemplateAreas,
            desktopTemplateAreas,
          ]}
        >
          <Text textStyle="regular16" gridArea="logradouro">
            <Box as="span" color="secondary.mid-dark">
              Logradouro:
            </Box>{' '}
            {cep.logradouro || ''}
          </Text>

          <Text textStyle="regular16" gridArea="bairro">
            <Box as="span" color="secondary.mid-dark">
              Bairro:
            </Box>{' '}
            {cep.bairro || ''}
          </Text>

          <Text textStyle="regular16" gridArea="cidade">
            <Box as="span" color="secondary.mid-dark">
              Cidade:
            </Box>{' '}
            {cep.cidade?.descricao || ''}
          </Text>

          <Text textStyle="regular16" gridArea="uf">
            <Box as="span" color="secondary.mid-dark">
              UF:
            </Box>{' '}
            {cep.cidade?.uf?.nome || ''}
          </Text>
        </Grid>
      }
    />
  );
};
