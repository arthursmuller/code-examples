import { FC } from 'react';

import { Button, Divider, Text } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { useMount } from 'react-use';

import {
  FullLayoutCard,
  useStepsContainerContext,
  CustomHeading,
} from '@pcf/design-system';

import { ConsignadoFiltersData } from '../models/consignado-form.model';
import {
  getNextPrazoOpt,
  SimulationPrazoPicker,
  SimulationPrazoPickerData,
} from '../../../components/simulation-prazo-picker';
import { SimulationLayoutGrid } from '../../../components/simulation-layout-grid';

export const ConsignadoFiltersStep: FC = () => {
  const { data, setData } = useStepsContainerContext<ConsignadoFiltersData>();

  const {
    control,
    formState: { errors, isValid },
    handleSubmit,
    trigger,
  } = useForm<SimulationPrazoPickerData>({
    mode: 'onChange',
  });

  useMount(trigger);

  const submitData = (nextFilter: SimulationPrazoPickerData): void => {
    setData({
      ...data,
      prazo: getNextPrazoOpt(nextFilter),
      showFilters: false,
    });
  };

  return (
    <FullLayoutCard title="Dados opcionais">
      <Text as="p" textStyle="regular16" pb="24px">
        Escolhendo continuar sem selecionar plano e prazo, os resultados serão
        referentes aos prazos preferenciais para a simulação.
      </Text>

      <Divider borderColor="grey.300" />

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

      <SimulationPrazoPicker
        prazo={data.prazo}
        control={control}
        errors={errors}
      />

      <SimulationLayoutGrid mb="16px">
        <Button
          colorScheme="secondary"
          disabled={!isValid}
          onClick={handleSubmit(submitData)}
        >
          Continuar
        </Button>
      </SimulationLayoutGrid>
    </FullLayoutCard>
  );
};
