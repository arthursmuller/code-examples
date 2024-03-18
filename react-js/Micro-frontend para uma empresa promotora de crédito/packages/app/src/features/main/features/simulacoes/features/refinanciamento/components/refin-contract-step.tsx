import { FC, useMemo } from 'react';

import { Controller, useForm } from 'react-hook-form';
import { Button, Flex, Text } from '@chakra-ui/react';

import {
  CustomHeading,
  formatCurrency,
  NoDataDisplay,
  RadioCard,
  RadioCardsGroup,
  useStepsContainerContext,
  FloatingContainer,
  useModal,
  FullLayoutCard,
  BemErrorBoundary,
  ColorSchemes,
  Loader,
} from '@pcf/design-system';
import { ContratoModel, getContratosQueryConfig, Resource } from '@pcf/core';

import { SimulationLayoutGrid } from '../../../components/simulation-layout-grid';
import { RefinContractData, RefinFormData } from '../model/refin-form.model';

export const ContractStep: FC = () => {
  const { nextStep, data } = useStepsContainerContext<RefinFormData>();
  const {
    control,
    formState: { isValid },
    handleSubmit,
    watch,
    trigger,
  } = useForm<RefinContractData>({
    mode: 'onChange',
    defaultValues: data,
  });
  const { showModal } = useModal();

  const selectedContracts = watch('contratos');

  const totalValues = useMemo(() => {
    const totals =
      selectedContracts?.reduce(
        (sum, next) => ({
          installments: sum.installments + next.prestacao,
          total: sum.total + next.saldoTotal,
        }),
        { installments: 0, total: 0 },
      ) ?? 0;
    return {
      total: formatCurrency(totals.total || 0),
      installments: formatCurrency(totals.installments || 0),
    };
  }, [selectedContracts]);

  const infoColor = !isValid ? 'error' : 'success';

  const onSubmit = (submitData: RefinContractData): void => {
    showModal({
      title: 'Atenção',
      type: ColorSchemes.warning,
      information:
        'Você gostaria de manter o mesmo valor de prestação no seu novo Refinanciamento?',
      confirmText: 'Sim, continuar',
      closeText: 'Personalizar valor de parcela',
      onCancel: () => nextStep({ ...submitData, showFilters: true }),
      onConfirm: () => nextStep({ ...submitData, showFilters: false }),
      closeOnClickOverlay: false,
    });
  };

  return (
    <FullLayoutCard
      title={`Matrícula #${data.matricula.matricula} (${data.matricula.convenio.nome})`}
    >
      <BemErrorBoundary>
        <Resource<ContratoModel[]>
          path={
            getContratosQueryConfig({ matricula: data.matricula.matricula })
              .queryKey ?? ''
          }
          noDataComponent={<NoDataDisplay entityName="contrato" />}
          loadCallback={trigger}
          loaderComponent={<Loader />}
          render={({ data: contractOpts }) => (
            <>
              <Flex direction="column">
                <Controller
                  control={control}
                  name="contratos"
                  defaultValue={data?.contratos}
                  rules={{
                    required: true,
                    validate: {
                      maxThreeContracts(contracts) {
                        if (contracts && contracts.length > 3) {
                          return 'Máximo de 3 contratos';
                        }
                        return true;
                      },
                    },
                  }}
                  render={({ field: { onChange, value } }) => (
                    <RadioCardsGroup
                      name="matricula"
                      onChange={(nextContracts) => {
                        const selected = nextContracts.map((c) =>
                          contractOpts.find((opt) => opt.contrato === c),
                        );

                        onChange(selected);
                      }}
                      defaultValue={value?.map((v) => v.contrato) ?? []}
                      minWidth="100%"
                      fitMode="fill"
                      isRadio={false}
                    >
                      {contractOpts.map((contract) => (
                        <RadioCard
                          key={contract.contrato}
                          value={contract.contrato.toString()}
                          containerDirection="row-reverse"
                          customContent={({ isChecked }) => (
                            <Flex
                              direction={['column', 'column', 'row']}
                              flex={1}
                              paddingTop={1}
                              marginRight={6}
                            >
                              <CustomHeading
                                textStyle="bold20"
                                flex={[0, 0, 1]}
                                color={isChecked ? 'white' : 'grey.800'}
                              >
                                Contrato {contract.contrato}
                              </CustomHeading>
                              <CustomHeading
                                textStyle="regular16"
                                color={isChecked ? 'white' : 'grey.800'}
                              >
                                Parcela {contract.qtdParcelasPagas}/
                                {contract.qtdParcelas} |{' '}
                                {formatCurrency(contract.prestacao)}
                              </CustomHeading>
                            </Flex>
                          )}
                        />
                      ))}
                    </RadioCardsGroup>
                  )}
                />
              </Flex>

              <FloatingContainer mobileOnly>
                <SimulationLayoutGrid mb="16px">
                  <Flex
                    border="1px solid"
                    borderColor={`${infoColor}.dark`}
                    background={`${infoColor}.washed`}
                    borderRadius="8px"
                    boxShadow="soft"
                    alignItems="center"
                    justifyContent="center"
                    flex={[0, 0, 1]}
                    paddingY={2}
                  >
                    <Text
                      color={`${infoColor}.dark`}
                      textStyle="bold16"
                      marginRight={1}
                    >
                      {selectedContracts?.length || 0}/3
                    </Text>
                    <Text color={`${infoColor}.dark`} textStyle="regular16">
                      Contratos selecionados
                    </Text>
                  </Flex>

                  <Flex direction="column">
                    <Flex>
                      <Text textStyle="bold16" flex={1}>
                        Saldo Total
                      </Text>
                      <Text textStyle="regular16"> {totalValues.total}</Text>
                    </Flex>
                    <Flex>
                      <Text textStyle="bold16" flex={1}>
                        Prestação Total
                      </Text>
                      <Text textStyle="regular16">
                        {' '}
                        {totalValues.installments}
                      </Text>
                    </Flex>
                  </Flex>
                </SimulationLayoutGrid>

                <SimulationLayoutGrid>
                  <Button disabled={!isValid} onClick={handleSubmit(onSubmit)}>
                    Refinanciar
                  </Button>
                </SimulationLayoutGrid>
              </FloatingContainer>
            </>
          )}
        />
      </BemErrorBoundary>
    </FullLayoutCard>
  );
};
