import React, { FC } from 'react';

import { Flex, Button, Icon, BoxProps, Grid } from '@chakra-ui/react';
import { useHistory } from 'react-router-dom';

import { BoxIcon } from '@pcf/design-system';
import {
  TabAjudaInativaIcon,
  TabCartaoInativaIcon,
  TabNovoInativaIcon,
  TabPortabInativaIcon,
  TabRefinInativaIcon,
} from '@pcf/design-system-icons';
import { mainRoutePaths } from 'features/main/routes';
import { useFeatureFlags } from 'app/feature-toggle';
import { CanUseRefinanciamento } from 'features/main/features/simulacoes/features/refinanciamento';

export const QuickyButtons: FC<BoxProps> = (props: BoxProps) => {
  const history = useHistory();

  const { flags } = useFeatureFlags();

  function handleClick(route: string): void {
    history.push(route);
  }

  return (
    <Flex
      direction="column"
      alignItems="center"
      width="fit-content"
      margin="auto"
      {...props}
    >
      <Grid
        gridColumnGap={4}
        gridRowGap={6}
        gridTemplateColumns="auto auto"
        justifyContent="center"
      >
        <BoxIcon
          label="Simular Novo Consignado"
          onClick={() => handleClick(mainRoutePaths.SIMULAR_NOVO)}
          icon={TabNovoInativaIcon}
          disabled={!flags.SIMULAR_NOVO}
        />
        <BoxIcon
          label="Fazer Portabilidade"
          icon={TabPortabInativaIcon}
          onClick={() => handleClick(mainRoutePaths.PORTABILIDADE)}
          disabled={!flags.PORTABILIDADE}
        />
        <BoxIcon
          label="Solicitar Cartão"
          icon={TabCartaoInativaIcon}
          onClick={() => handleClick(mainRoutePaths.CARTAO)}
          disabled={!flags.CARTAO}
        />
        <CanUseRefinanciamento>
          <BoxIcon
            label="Refinanciar Consignado"
            icon={TabRefinInativaIcon}
            onClick={() => handleClick(mainRoutePaths.REFIN)}
            disabled={!flags.REFIN}
          />
        </CanUseRefinanciamento>
      </Grid>

      <Button
        h="64px"
        mt={6}
        leftIcon={
          <Icon
            color="primary.regular"
            as={TabAjudaInativaIcon}
            boxSize="28px"
            mr="6px"
          />
        }
        isFullWidth
        color="primary.regular"
        textStyle="bold14"
        colorScheme="grey"
        onClick={() => handleClick(mainRoutePaths.AJUDA)}
      >
        Preciso de ajuda
      </Button>
    </Flex>
  );
};
