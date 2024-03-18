import { FC, useEffect, useMemo, useRef, useState } from 'react';

import {
  Flex,
  useBreakpointValue,
  Text,
  Divider,
  Button,
  Center,
} from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  BemCurrencyInput,
  ExpandableSection,
  FormItemControl,
  FullLayoutCard,
  Loader,
  useStepsContainerContext,
} from '@pcf/design-system';
import {
  PortabilidadeRetornoModel,
  usePortabilidadeSimulacaoQuery,
  useProdutoOperacaoPortabilidadeQuery,
} from '@pcf/core';
import { SimulationResultPicker } from 'features/main/features/simulacoes/components/simulation-result-picker';
import { SimulationResultNoData } from 'features/main/features/simulacoes/components/simulation-result-no-data';
import {
  getNextPrazoOpts,
  Prazo,
  SimulationPrazoMultiPicker,
  SimulationPrazoPickerData,
} from 'features/main/features/simulacoes/components/simulation-prazo-picker';
import { usePostIntencaoOperacao } from 'features/main/features/simulacoes/utils/use-post-intencao-operacao';

import { Summary } from './portabilidade-results-summary';

import {
  PortabilidadeFormData,
  PortabilidadeResultFormData,
  PortabilidadeType,
} from '../../models/portabilidade-form.model';

function usePreviousOrCurrent<T>(value: T): T {
  const ref = useRef<T>();

  useEffect(() => {
    if (value) ref.current = value;
  }, [value]);

  return value || ref.current;
}

interface FilterData extends SimulationPrazoPickerData {
  prestacaoIntencao: number;
}

interface FilterState {
  prazos: Prazo[];
  prestacaoIntencao: number;
}

export const PortabilidadeResults: FC = () => {
  const { data: formStepsData } =
    useStepsContainerContext<PortabilidadeFormData>();
  const [filterData, setFilterData] = useState<FilterState>({
    prazos: [96, 72],
    prestacaoIntencao: 0,
  });

  const isSimulacao = formStepsData.type === PortabilidadeType.Simular;
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const {
    formState: { isValid },
    control,
    handleSubmit,
    setValue,
    trigger,
  } = useForm<PortabilidadeResultFormData>({
    defaultValues: formStepsData,
    mode: 'onChange',
  });

  const { control: filterControl, handleSubmit: handleFilterSubmit } =
    useForm<FilterData>();

  const request = useMemo(
    () => ({
      idRendimentoCliente: formStepsData.matricula.id,
      prazoRestante: formStepsData.parcelas - formStepsData.parcelasPagas,
      prazoTotal: formStepsData.parcelas,
      saldoDevedor: formStepsData.saldoDevedor,
      valorPrestacaoPortabilidade: formStepsData.prestacao,

      retornarSomenteOperacoesViaveis: true,

      valorPrestacaoRefin:
        formStepsData.type === PortabilidadeType?.Simular
          ? filterData.prestacaoIntencao || formStepsData.prestacao
          : undefined,

      prazos: filterData.prazos || [96, 72],
    }),
    [filterData.prestacaoIntencao, filterData.prazos],
  );

  const {
    data: simulationResult,
    isLoading,
    isError,
    refetch,
  } = usePortabilidadeSimulacaoQuery(request, {
    useErrorBoundary: false,
    retry: 0,
  });
  const simulationResultOrPrevious =
    usePreviousOrCurrent<PortabilidadeRetornoModel>(simulationResult);

  useEffect(() => {
    if (!isLoading && !simulationResultOrPrevious && isSimulacao) {
      setValue('simulacao', undefined);
      trigger();
    }
  }, [simulationResult?.simulacoesIntencaoRefinanciamento]);

  const { data: produtoOp } = useProdutoOperacaoPortabilidadeQuery(undefined, {
    useErrorBoundary: false,
  });
  const { post, isPosting } = usePostIntencaoOperacao(
    produtoOp,
    formStepsData.matricula.id,
    {
      prestacao: simulationResult?.viabilidade.prestacao,
      valorAuxilioFinanceiro: 0,
      taxaMes: simulationResult?.viabilidade.taxaMes,
      taxaAno: simulationResult?.viabilidade.taxaAno,
      prazo: formStepsData.parcelas - formStepsData.parcelasPagas,
      valorFinanciado: simulationResult?.viabilidade.valorFinanciado,
      primeiroVencimento: simulationResult?.viabilidade.primeiroVcto,
      custoEfetivoTotalMes: simulationResult?.viabilidade?.cetMes,
      custoEfetivoTotalAno: simulationResult?.viabilidade?.cetAno,
      impostoOperacaoFinanceira: simulationResult?.viabilidade?.valorIOF,

      contratos: [{ contrato: formStepsData.contrato }],
      portabilidade: {
        idBancoOriginador: +formStepsData.origem,
        prazoRestante: formStepsData.parcelas - formStepsData.parcelasPagas,
        prazoTotal: +formStepsData.parcelas,
        saldoDevedor: formStepsData.saldoDevedor,
        planoRefinanciamento: (simulacao) => simulacao?.plano,
        prazoRefinanciamento: (simulacao) => +simulacao?.prazo,
        valorPrestacaoRefinanciamento: (simulacao) => simulacao?.prestacao,
      },
    },
    {
      title: 'Sua portabilidade foi enviada com sucesso!',
      information:
        'A instituição financeira de origem pode entrar em contato para propor retenção de contrato. Caso isso ocorra, nos informe.',
    },
  );

  return (
    <FullLayoutCard>
      {isLoading && !simulationResultOrPrevious ? (
        <Center flex={1}>
          <Loader size="lg" />
        </Center>
      ) : (
        ((isError ||
          (simulationResultOrPrevious?.viabilidade &&
            !simulationResultOrPrevious.viabilidade.portavel)) && (
          <Center flexDirection="column" height="100%">
            <SimulationResultNoData
              errorMessage={simulationResultOrPrevious?.viabilidade?.mensagem}
            />

            {isError && (
              <Button onClick={() => refetch()} maxWidth="250px">
                Tentar novamente
              </Button>
            )}
          </Center>
        )) || (
          <Flex direction="column" marginTop={3}>
            <Summary
              title="Detalhes da Portabilidade"
              extended={!isMobile && !isSimulacao}
              data={simulationResultOrPrevious.viabilidade}
            />

            {isSimulacao && (
              <>
                <Divider borderColor="grey.400" marginBottom={8} />

                <Summary
                  title="Portabilidade e Refinanciamento"
                  data={simulationResultOrPrevious.viabilidade}
                />

                <Flex direction="column">
                  <Text
                    color="secondary.regular"
                    textStyle="bold20"
                    marginBottom={2}
                  >
                    Prazos personalizados:
                  </Text>
                  <Text>
                    Escolha seu prazo a partir das possibilidades de
                    renegociação:
                  </Text>

                  {isLoading ? (
                    <Center flex={1} padding="81px">
                      <Loader size="lg" />
                    </Center>
                  ) : (
                    (simulationResult.simulacoesIntencaoRefinanciamento
                      .length && (
                      <SimulationResultPicker
                        control={control as any}
                        opts={
                          simulationResult.simulacoesIntencaoRefinanciamento
                        }
                      />
                    )) || (
                      <Flex
                        background="grey.300"
                        marginY={10}
                        borderRadius="20px"
                      >
                        <SimulationResultNoData />
                      </Flex>
                    )
                  )}

                  <Text
                    color="secondary.regular"
                    textStyle="bold20"
                    marginBottom={3}
                  >
                    Qual o valor da prestação que você deseja pagar?
                  </Text>

                  <Flex width={['100%', '100%', '288px']}>
                    <FormItemControl
                      label="Prestação"
                      as={BemCurrencyInput}
                      name="prestacaoIntencao"
                      defaultValue={
                        formStepsData.prestacaoIntencao ||
                        formStepsData.prestacao
                      }
                      control={filterControl}
                    />
                  </Flex>

                  <ExpandableSection title="Expandir Filtros">
                    <SimulationPrazoMultiPicker
                      prazo={[96, 72]}
                      control={filterControl as any}
                      errors={null}
                    />
                  </ExpandableSection>
                </Flex>
              </>
            )}
            <Flex
              justifyContent={isSimulacao ? 'space-between' : 'flex-end'}
              flexDirection={['column', 'row', 'row']}
            >
              {isSimulacao && (
                <Button
                  colorScheme="secondary"
                  variant="ghost"
                  width={['100%', '100%', '288px']}
                  marginBottom={8}
                  textDecoration="underline"
                  onClick={handleFilterSubmit(
                    ({ customOpt, selectedOpt, prestacaoIntencao }) => {
                      const prazos = (selectedOpt &&
                        getNextPrazoOpts({ customOpt, selectedOpt })) || [
                        72, 96,
                      ];

                      setFilterData({ prazos, prestacaoIntencao });
                    },
                  )}
                >
                  Filtrar
                </Button>
              )}

              <Button
                colorScheme="secondary"
                width={['100%', '100%', '288px']}
                marginBottom={8}
                disabled={!isValid}
                onClick={handleSubmit(post)}
                isLoading={isPosting}
              >
                Finalizar
              </Button>
            </Flex>
          </Flex>
        )
      )}
    </FullLayoutCard>
  );
};
