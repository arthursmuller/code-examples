import { FC } from 'react';

import { Control, Controller } from 'react-hook-form';
import { Flex, Text } from '@chakra-ui/react';

import {
  RadioCardsGroup,
  RadioCard,
  CurrencyDisplay,
} from '@pcf/design-system';
import { SimulacaoNovoRetornoModel } from '@pcf/core';

import { SimulationDetailsDrawer } from './simulation-details-drawer';

import { SimulationResult } from '../models';

interface SimulationResultPickerProps {
  control: Control<{
    simulacao: SimulacaoNovoRetornoModel;
  }>;
  opts: SimulationResult[];
}

export const SimulationResultPicker: FC<SimulationResultPickerProps> = ({
  control,
  opts,
}) => (
  <Controller
    control={control}
    name="simulacao"
    defaultValue={undefined}
    rules={{ required: true }}
    render={({ field: { onChange } }) => (
      <RadioCardsGroup
        name="simulacao"
        onChange={(key: string) => onChange(opts[key])}
        minWidth="248px"
        fitMode="fill"
      >
        {opts.map((simulacao, index) => (
          <RadioCard
            key={`${index}`}
            value={`${index}`}
            subtitle={`Prazo ${
              simulacao.isCustomInterval ? 'personalizado' : ''
            } ${simulacao.prazo} meses`}
            customContent={
              <Flex direction="column" pl={[0, 0, '16px']} flexGrow={1}>
                <Text textStyle="bold16" color="inherit">
                  Valor da proposta:
                </Text>
                <Text textStyle="regular24" color="inherit">
                  <CurrencyDisplay value={simulacao.valorAF} />
                </Text>
              </Flex>
            }
            customFooter={
              <Flex>
                <Flex flexGrow={1}>
                  <Text textStyle="bold16" mr="8px" color="inherit">
                    Taxa:
                  </Text>
                  <Text textStyle="regular16" color="inherit">
                    {simulacao.taxaMes}%
                  </Text>
                </Flex>
                <SimulationDetailsDrawer data={simulacao} />
              </Flex>
            }
          />
        ))}
      </RadioCardsGroup>
    )}
  />
);
