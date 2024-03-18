import { FC, useState } from 'react';

import { Center, Flex, Button, Text } from '@chakra-ui/react';
import { useHistory } from 'react-router-dom';
import { useMount } from 'react-use';

import {
  CustomHeading,
  FullLayoutCard,
  StepsContainer,
  StepsContainerProvider,
  useStepsContainerContext,
} from '@pcf/design-system';
import { mainRoutePaths } from 'features/main/routes/main-routes-paths';
import { usePageContext } from 'components/page/page.context';
import { useFeatureFlags } from 'app';

import { SimulationValueStep } from './components/consignado-value-step';
import { ConsignadoResultsStep } from './components/consignado-results-step';
import { ConsignadoFiltersStep } from './components/consignado-filters-step';
import { ConsignadoFormData } from './models/consignado-form.model';

import { SimulationMatriculaStep } from '../../components/simulation-matricula-step';
import AutoFillPersonalDataWrapped from '../auto-fill-personal-data';

const title = 'Simular Novo Consignado';

const SimularConsignado: FC = () => {
  const { stepNumber, previousStep, data } =
    useStepsContainerContext<ConsignadoFormData>();
  const { setMenuBarConfig } = usePageContext();
  const { flags } = useFeatureFlags();
  const [continueWithAutoFillFlow, setContinueWithAutoFillFlow] =
    useState(false);
  const history = useHistory();

  useMount(
    () =>
      setMenuBarConfig &&
      setMenuBarConfig({
        menuBarColor: 'secondary.regular',
        fixedMenuScrollOffset: 175,
        contextMenuBarColor: 'secondary.regular',
        contextMenuTitle: title,
        contextMenuCallback: previousStep,
      }),
  );

  return (
    <Flex
      h="100%"
      flex="1"
      backgroundColor="secondary.regular"
      pt={['0', '0', '24px', '24px']}
    >
      <StepsContainer
        onBack={previousStep}
        stepNumber={stepNumber}
        size="lg"
        containerHeight="full"
        colorScheme="white"
        title={title}
        titleTag="h2"
        showStepsIndicator={!data.showFilters}
      >
        <SimulationValueStep />
        {/* <SimulationTypeForm /> */}
        <SimulationMatriculaStep />

        {data.showFilters ? (
          <ConsignadoFiltersStep />
        ) : (
          <ConsignadoResultsStep
            onSetContinueWithAutoFillFlow={setContinueWithAutoFillFlow}
          />
        )}

        {flags.INCLUSAO_PROPOSTA_NOVA && (
          <FullLayoutCard>
            {continueWithAutoFillFlow ? (
              <AutoFillPersonalDataWrapped />
            ) : (
              <Center flexDir="column" flexGrow={1}>
                <CustomHeading color="secondary.regular" textStyle="bold40">
                  Obrigada!
                </CustomHeading>
                <Text mt={8} textStyle="bold24">
                  Um consultor entrará em contato com você para prosseguir com a
                  sua operação
                </Text>
                <Button
                  mt={4}
                  onClick={() => history.push(mainRoutePaths.INICIO)}
                >
                  Voltar
                </Button>
              </Center>
            )}
          </FullLayoutCard>
        )}
      </StepsContainer>
    </Flex>
  );
};

export const SimularConsignadoProvided: FC = () => {
  const history = useHistory();

  return (
    <StepsContainerProvider
      onCloseCb={() => history.push(mainRoutePaths.INICIO)}
    >
      <SimularConsignado />
    </StepsContainerProvider>
  );
};
