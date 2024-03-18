import { FC } from 'react';

import { useForm } from 'react-hook-form';
import { Button } from '@chakra-ui/react';

import { FullLayoutCard, useStepsContainerContext } from '@pcf/design-system';
import { getConveniosQueryConfig } from '@pcf/core';
import { queryCacheConfig } from 'app/app-providers';
import { TipoConsignadoForm } from 'features/main/components/tipo-consignado';

import { ConsignadoTypeData } from '../models/consignado-form.model';
import { SimulationLayoutGrid } from '../../../components/simulation-layout-grid';

queryCacheConfig.prefetchQuery(getConveniosQueryConfig());

export const ConsignadoTypeStep: FC = () => {
  const { control, formState, handleSubmit } = useForm<ConsignadoTypeData>({
    mode: 'onChange',
  });
  const { nextStep, data } = useStepsContainerContext<ConsignadoTypeData>();

  const { isValid } = formState;

  return (
    <FullLayoutCard title="Escolha seu convênio">
      <TipoConsignadoForm control={control} initialData={data} />
      <SimulationLayoutGrid mb="16px">
        <Button disabled={!isValid} onClick={handleSubmit(nextStep)}>
          Simular meu consignado
        </Button>
      </SimulationLayoutGrid>
    </FullLayoutCard>
  );
};
