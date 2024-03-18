import { FC } from 'react';

import {
  Flex,
  Button,
  Divider,
  Text,
  useBreakpointValue,
} from '@chakra-ui/react';

import {
  Drawer,
  formatCurrency,
  formatDate,
  DefaultFormatStrings,
} from '@pcf/design-system';
import { ArrowLeftIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

import { SimulationResult } from '../models/simulation-result.model';

interface SimulationDetailsDrawer {
  data: SimulationResult;
}

const formatPercentage = (v: number): string => `${Math.round(v * 100) / 100}%`;

const MainInfo: FC<{ title: string; value: string }> = ({ title, value }) => (
  <Flex marginBottom={4}>
    <Text flex={1} textStyle="bold20" color="primary.regular">
      {title}:
    </Text>
    <Text textStyle="regular20" color="primary.regular">
      {value}
    </Text>
  </Flex>
);

const SecondaryInfo: FC<{ title: string; value: string }> = ({
  title,
  value,
}) => (
  <Flex marginBottom={4}>
    <Text flex={1}>{title}:</Text>
    <Text textStyle="bold16">{value}</Text>
  </Flex>
);

export const SimulationDetailsDrawer: FC<SimulationDetailsDrawer> = ({
  data,
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <Drawer
      buttonRender={({ onClick }) => (
        <Button
          variant="link"
          onClick={onClick}
          color="inherit"
          textDecoration="underline"
        >
          Ver mais
        </Button>
      )}
      content={({ onClose }) => (
        <Flex direction="column" height="100%">
          <Drawer.Title
            onClick={onClose}
            icon={isMobile ? ArrowLeftIcon : StatusCloseErrorIcon}
            iconProps={
              isMobile
                ? {
                    marginRight: '2px',
                    width: '10.67px',
                    height: '16px',
                    fill: 'white',
                  }
                : undefined
            }
            title="Resumo Proposta"
            color="secondary.mid-dark"
          />

          <Drawer.Body marginTop={4} flexDirection="column" display="flex">
            <Flex flex={1} direction="column">
              <MainInfo
                title="Valor da Proposta"
                value={formatCurrency(data.valorAF)}
              />
              <MainInfo
                title="Taxa"
                value={`${formatPercentage(data.taxaMes)} a.m`}
              />
              <MainInfo
                title="Prazo Personalizado"
                value={`${data.prazo} meses`}
              />

              <Divider
                borderColor="grey.500"
                borderBottomWidth="1.5px"
                marginBottom={6}
                marginTop={2}
              />

              <SecondaryInfo
                title="Primeiro Vencimento"
                value={
                  !!data.primeiroVcto
                    ? formatDate(
                        new Date(data.primeiroVcto),
                        DefaultFormatStrings.input,
                      )
                    : ''
                }
              />
              <SecondaryInfo
                title="Valor Operação"
                value={formatCurrency(data.valorOperacao)}
              />
              <SecondaryInfo
                title="Prestação"
                value={formatCurrency(data.prestacao)}
              />
              <SecondaryInfo
                title="Valor IOF"
                value={formatCurrency(data.valorIOF)}
              />
              <SecondaryInfo
                title="Valor Financiado"
                value={formatCurrency(data.valorFinanciado)}
              />
              <SecondaryInfo
                title="Valor AF"
                value={formatCurrency(data.valorAF)}
              />

              <SecondaryInfo
                title="Taxa Mês"
                value={formatPercentage(data.taxaMes)}
              />
              <SecondaryInfo
                title="Taxa Ano"
                value={formatPercentage(data.taxaAno)}
              />
              <SecondaryInfo
                title="Cet Mês"
                value={formatPercentage(data.cetMes)}
              />
              <SecondaryInfo
                title="Cet Ano"
                value={formatPercentage(data.cetAno)}
              />
            </Flex>

            <Flex justifyContent="flex-end">
              <Button
                minW="170px"
                width={['100%', '100%', 'auto']}
                onClick={onClose}
              >
                Fechar
              </Button>
            </Flex>
          </Drawer.Body>
        </Flex>
      )}
    />
  );
};
