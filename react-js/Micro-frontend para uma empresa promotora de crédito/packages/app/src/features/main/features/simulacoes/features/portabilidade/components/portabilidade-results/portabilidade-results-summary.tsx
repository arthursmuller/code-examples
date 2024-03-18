import { FC } from 'react';

import {
  Box,
  Divider,
  Flex,
  FlexProps,
  Grid,
  Icon,
  Text,
  useBreakpointValue,
} from '@chakra-ui/react';

import {
  CustomHeading,
  formatCurrency,
  StatusCheckCircleIcon,
} from '@pcf/design-system';
import { PortabilidadeData } from '@pcf/core';

interface LineProps extends FlexProps {
  label: string;
  value: string;
  small?: boolean;
}

const Line: FC<LineProps> = ({ label, value, small, ...flexProps }) => (
  <Flex marginBottom={4} {...flexProps}>
    <Text textStyle={small ? 'bold16' : 'regular20'}>
      {label}:{' '}
      <Box
        as="span"
        color="primary.regular"
        textStyle={small ? 'bold16' : 'bold20'}
      >
        {value}
      </Box>
    </Text>
  </Flex>
);

const formatPercentage = (v: number): string => `${Math.round(v * 100) / 100}%`;

interface SummaryProps {
  extended?: boolean;
  title: string;
  data: PortabilidadeData;
}

export const Summary: FC<SummaryProps> = ({ extended, title, data }) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <Flex direction="column">
      <Flex color="secondary.regular">
        <CustomHeading
          textStyle={['bold24', 'bold24', 'bold32']}
          marginBottom={10}
        >
          {title}
        </CustomHeading>
      </Flex>

      <Flex
        direction={['column', 'column', 'row']}
        justifyContent={['center', 'center', 'flex-start']}
        alignItems="center"
        marginBottom={[6, 6, 8]}
      >
        <Text textStyle={extended ? 'bold24' : 'bold20'} whiteSpace="pre">
          Valor Financiado:{'   '}
          <Box as="span" color="primary.regular">
            {formatCurrency(data.valorFinanciado)}
          </Box>
        </Text>

        <Flex
          marginLeft={[0, 0, 8]}
          marginTop={[3, 3, 0]}
          color="success.regular"
          alignItems="center"
        >
          <Icon marginRight={2} as={StatusCheckCircleIcon} />
          <Text textStyle="bold14">Válido</Text>
        </Flex>
      </Flex>

      {(isMobile || extended) && (
        <Divider borderColor="grey.400" marginBottom={8} />
      )}

      <Grid
        gridTemplateColumns={extended ? '1fr' : ['1fr', '1fr', '1fr 1fr 1fr']}
        grid-template-rows="1fr auto"
      >
        <Line
          small={!extended}
          label="CET Mês"
          value={formatPercentage(data.cetMes)}
        />
        <Line
          small={!extended}
          label="RCO"
          value={formatCurrency(data.valorRCOBruto)}
        />
        <Line
          small={!extended}
          label="CET Ano"
          value={formatPercentage(data.cetAno)}
        />
        <Line
          small={!extended}
          label="Taxa"
          value={formatPercentage(data.taxaMes)}
        />
        <Line
          small={!extended}
          label="Saldo Devedor Corrigido"
          value={formatCurrency(data.saldoDevedorCorrigido)}
          gridColumnEnd={!isMobile && 'span 2'}
        />
      </Grid>
    </Flex>
  );
};
