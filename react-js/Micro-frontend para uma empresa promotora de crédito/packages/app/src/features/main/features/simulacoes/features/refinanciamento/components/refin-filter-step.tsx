import { FC } from 'react';

import { useForm } from 'react-hook-form';
import {
  Button,
  Text,
  Divider,
  Flex,
  useBreakpointValue,
} from '@chakra-ui/react';
import { useMount } from 'react-use';

import {
  FullLayoutCard,
  CustomHeading,
  BemCurrencyInput,
  FormItemControl,
  useStepsContainerContext,
} from '@pcf/design-system';

import {
  getNextPrazoOpts,
  SimulationPrazoMultiPicker,
  SimulationPrazoPickerData,
} from '../../../components/simulation-prazo-picker';
import { RefinFiltersData } from '../model/refin-form.model';
import { SimulationLayoutGrid } from '../../../components/simulation-layout-grid';

interface RefinFilterStepForm extends SimulationPrazoPickerData {
  value: number;
}

export const RefinFilterStep: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const { setData, data } = useStepsContainerContext<RefinFiltersData>();
  const {
    control,
    formState: { errors, isValid },
    handleSubmit,
    trigger,
  } = useForm<RefinFilterStepForm>({
    mode: 'onChange',
  });

  useMount(trigger);

  const submitData = (nextFilter: RefinFilterStepForm): void => {
    setData({
      ...data,
      prazo: getNextPrazoOpts(nextFilter),
      value: nextFilter.value,
      showFilters: false,
    });
  };

  return (
    <FullLayoutCard title="Qual o valor da prestação que você deseja pagar?">
      <Flex marginTop={4}>
        <FormItemControl
          label="Valor da prestação"
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

      <Divider borderColor="grey.300" />

      <CustomHeading
        pt="24px"
        pb="8px"
        as="h2"
        textStyle="bold32"
        color="secondary.mid-dark"
      >
        Dados opcionais
      </CustomHeading>

      <Text as="p" textStyle="regular16">
        Escolhendo continuar sem selecionar o prazo, os resultados serão
        referentes aos prazos preferenciais para as simulações existentes.
      </Text>

      {!isMobile && (
        <>
          <Divider borderColor="grey.300" marginTop={6} />

          <CustomHeading
            pt="24px"
            pb="8px"
            as="h2"
            textStyle="bold32"
            color="secondary.mid-dark"
          >
            Prazos preferenciais
          </CustomHeading>

          <Text as="p" textStyle="regular16">
            Escolha a quantidade de meses em que você gostaria de dividir as
            parcelas do seu consignado.
          </Text>
        </>
      )}

      <SimulationPrazoMultiPicker
        prazo={data.prazo}
        control={control as any}
        isValid={isValid}
        errors={errors}
      />

      <SimulationLayoutGrid mb="16px">
        <Button
          colorScheme="secondary"
          disabled={!isValid}
          onClick={handleSubmit(submitData)}
        >
          Filtrar prazo
        </Button>
      </SimulationLayoutGrid>
    </FullLayoutCard>
  );
};
