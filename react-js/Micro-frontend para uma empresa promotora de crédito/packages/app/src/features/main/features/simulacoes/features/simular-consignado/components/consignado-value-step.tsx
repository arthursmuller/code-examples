import { FC } from 'react';

import { Controller, useForm } from 'react-hook-form';
import { Box, Button, Divider, Flex } from '@chakra-ui/react';
import { useMount } from 'react-use';

import {
  CustomHeading,
  BemCurrencyInput,
  useStepsContainerContext,
  FullLayoutCard,
  RadioCard,
  RadioCardsGroup,
  fadeIn,
  FormItemControl,
} from '@pcf/design-system';

import { SimulationLayoutGrid } from '../../../components/simulation-layout-grid';
import { ConsignadoValueData } from '../models/consignado-form.model';
import { TiposEmprestimo } from '../models/tipos-emprestimo.enum';

export const SimulationValueStep: FC = () => {
  const { control, formState, trigger, handleSubmit, watch } =
    useForm<ConsignadoValueData>({ mode: 'onChange' });
  const { nextStep, data } = useStepsContainerContext<ConsignadoValueData>();

  useMount(trigger);

  const { isValid, errors } = formState;
  const tipoEmprestimoForm = watch('tipoEmprestimo');

  return (
    <FullLayoutCard title="Escolha como você deseja simular seu Consignado">
      <Controller
        control={control}
        name="tipoEmprestimo"
        defaultValue={data.tipoEmprestimo || ''}
        rules={{ required: true }}
        render={({ field: { onChange, value } }) => (
          <RadioCardsGroup
            name="loanType"
            onChange={onChange}
            defaultValue={value}
          >
            <RadioCard
              value={TiposEmprestimo.parcelamento}
              title="Valor da parcela"
              information="Você informa o valor que deseja pagar por mês."
            />
            <RadioCard
              value={TiposEmprestimo.valorTotal}
              title="Valor desejado"
              information="Você informa o quanto deseja receber no momento da contratação."
            />
          </RadioCardsGroup>
        )}
      />

      {tipoEmprestimoForm && (
        <Box sx={{ animation: `.5s ${fadeIn} ease-in` }}>
          <Divider borderColor="grey.300" />

          <SimulationLayoutGrid
            mt={['16px', '16px', '40px']}
            color={errors?.value || !isValid ? 'grey.600' : 'secondary.regular'}
            _focusWithin={{ color: 'secondary.regular' }}
          >
            <CustomHeading
              as="h2"
              textStyle="bold24"
              color="inherit"
              mb="16px"
              pr="24px"
            >
              {tipoEmprestimoForm === TiposEmprestimo.parcelamento
                ? 'Qual o valor da parcela que você deseja contratar?'
                : 'Qual o valor de empréstimo que você deseja contratar?'}
            </CustomHeading>

            <Flex flex={1} alignItems="center">
              <FormItemControl
                label="Valor"
                name="value"
                defaultValue={data.value || 0}
                required
                rules={{
                  min: { value: 1, message: 'Valor inválido' },
                }}
                control={control}
                as={BemCurrencyInput}
              />
            </Flex>
          </SimulationLayoutGrid>

          <SimulationLayoutGrid mt={['16px', '16px', '40px']} mb="16px">
            <Button disabled={!isValid} onClick={handleSubmit(nextStep)}>
              Continuar
            </Button>
          </SimulationLayoutGrid>
        </Box>
      )}
    </FullLayoutCard>
  );
};
