import React, { FC } from 'react';

import { Controller, useForm } from 'react-hook-form';
import { Button, Flex } from '@chakra-ui/react';

import {
  FullLayoutCard,
  NoDataDisplay,
  RadioCardsGroup,
  useStepsContainerContext,
  BemErrorBoundary,
  Loader,
} from '@pcf/design-system';
import { queryCacheConfig } from 'app/app-providers';
import {
  getRendimentosQueryConfig,
  RendimentoResponseModel,
  Resource,
} from '@pcf/core';

import { SimulationLayoutGrid } from './simulation-layout-grid';

import { MatriculaCard } from '../../../components/matricula/matricula-card';
import { AlertBannerInstructions } from '../../perfil/rendimentos/components/alert-banner-instructions';
import { useCreateMatricula } from '../../perfil/rendimentos/use-create-matricula';

queryCacheConfig.prefetchQuery(getRendimentosQueryConfig());

export interface SimulationMatriculaData {
  matricula: RendimentoResponseModel;
}

interface SimulationMatriculastepProps {
  nextButtonLabel?: string;
}

export const SimulationMatriculaStep: FC<SimulationMatriculastepProps> = ({
  nextButtonLabel = 'Simular meu consignado',
}) => {
  const {
    control,
    formState: { isValid },
    handleSubmit,
    trigger,
  } = useForm<SimulationMatriculaData>({
    mode: 'onChange',
  });
  const { nextStep, data } =
    useStepsContainerContext<SimulationMatriculaData>();
  const showCreateDialog = useCreateMatricula();

  return (
    <FullLayoutCard title="Escolha sua matrícula">
      <AlertBannerInstructions />
      <BemErrorBoundary>
        <Resource<RendimentoResponseModel[]>
          path={getRendimentosQueryConfig().queryKey ?? ''}
          noDataComponent={
            <>
              <NoDataDisplay entityName="matrícula" />
              <Button
                gridArea="create"
                variant="link"
                color="secondary.regular"
                onClick={showCreateDialog}
              >
                Cadastrar novo Benefício
              </Button>
            </>
          }
          loadCallback={trigger}
          loaderComponent={<Loader />}
          render={({ data: matriculas }) => (
            <>
              <Flex direction="column" flexGrow={[1, 1, 0]}>
                <Controller
                  control={control}
                  name="matricula"
                  defaultValue={data?.matricula}
                  rules={{ required: true }}
                  render={({ field: { onChange, value } }) => (
                    <RadioCardsGroup
                      name="matricula"
                      onChange={(id) =>
                        onChange(matriculas.find((c) => c.id === +id))
                      }
                      defaultValue={value?.id?.toString() || ''}
                      minWidth="40%"
                      fitMode="fit"
                    >
                      {matriculas.map((matricula) => (
                        <MatriculaCard
                          key={matricula.id}
                          value={matricula.id.toString()}
                          matricula={matricula}
                          isSimulationFlow
                        />
                      ))}
                    </RadioCardsGroup>
                  )}
                />
              </Flex>
              <Button
                gridArea="create"
                variant="link"
                color="secondary.regular"
                onClick={showCreateDialog}
              >
                Cadastrar novo Benefício
              </Button>
              <SimulationLayoutGrid mb="16px">
                <Button
                  disabled={!isValid}
                  onClick={handleSubmit(nextStep)}
                  colorScheme="secondary"
                >
                  {nextButtonLabel}
                </Button>
              </SimulationLayoutGrid>
            </>
          )}
        />
      </BemErrorBoundary>
    </FullLayoutCard>
  );
};
