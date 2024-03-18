import { FC, useEffect } from 'react';

import { Button, Center, Grid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { UseQueryResult } from 'react-query';

import { Loader, useStepsContainerContext } from '@pcf/design-system';
import { SimulacaoNovoRetornoModel } from '@pcf/core';

import { Prazo } from './simulation-prazo-picker';
import { SimulationResultPicker } from './simulation-result-picker';
import { SimulationResultNoData } from './simulation-result-no-data';

import { SimulationResult } from '../models';

interface ContextData {
  prazo: Prazo;
  showFilters: boolean;
}

export interface ResultsDisplayProps {
  queryProps: UseQueryResult<SimulationResult[], Error>;
  onSubmit: ({ simulacao }: { simulacao: SimulacaoNovoRetornoModel }) => void;
  isSubmitting: boolean;
}

export const ResultsDisplay: FC<ResultsDisplayProps> = ({
  queryProps,
  onSubmit,
  isSubmitting,
}) => {
  const { data, setData } = useStepsContainerContext<ContextData>();

  const {
    control,
    formState: { isValid },
    handleSubmit,
    trigger,
  } = useForm<{
    simulacao: SimulacaoNovoRetornoModel;
  }>({
    mode: 'onChange',
  });

  const {
    isLoading,
    isFetching,
    refetch,
    data: simulationResults,
  } = queryProps;

  useEffect(() => {
    trigger();
  }, [isLoading]);

  return (
    <>
      {isLoading ? (
        <Center flex={1}>
          <Loader size="lg" />
        </Center>
      ) : (
        (simulationResults?.length && (
          <SimulationResultPicker control={control} opts={simulationResults} />
        )) || <SimulationResultNoData />
      )}

      {!isLoading && (
        <Grid
          my="16px"
          gridTemplateColumns={['1fr', '1fr', 'repeat(3, 1fr)']}
          gridTemplateAreas={[
            "'link' 'submit'",
            "'link' 'submit'",
            "'. link submit'",
          ]}
          gap="24px"
        >
          {simulationResults?.length ? (
            <>
              <Button
                gridArea="link"
                variant="link"
                onClick={() => setData({ ...data, showFilters: true })}
                height="48px"
                color="primary.regular"
                textDecoration="underline"
              >
                Mostrar prazos preferenciais
              </Button>

              <Button
                gridArea="submit"
                disabled={!isValid || !simulationResults.length}
                paddingX={0}
                onClick={handleSubmit(onSubmit)}
                isLoading={isSubmitting}
                loadingText="Requisitando"
              >
                Continuar
              </Button>
            </>
          ) : (
            <Button
              paddingX={0}
              onClick={() => refetch()}
              gridArea="link"
              isLoading={isFetching}
            >
              Tentar novamente
            </Button>
          )}
        </Grid>
      )}
    </>
  );
};
