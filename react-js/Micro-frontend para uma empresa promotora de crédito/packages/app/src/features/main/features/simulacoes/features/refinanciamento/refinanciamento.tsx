import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { useHistory } from 'react-router-dom';
import { useMount } from 'react-use';

import {
  StepsContainer,
  StepsContainerProvider,
  useStepsContainerContext,
} from '@pcf/design-system';
import { usePageContext } from 'components/page/page.context';
import { mainRoutePaths } from 'features/main/routes/main-routes-paths';

import { ContractStep } from './components/refin-contract-step';
import { RefinFilterStep } from './components/refin-filter-step';
import { RefinResultsStep } from './components/refin-results-step';

import { SimulationMatriculaStep } from '../../components';

const title = 'Renegociação de Contrato';

const Refinanciamento: FC = () => {
  const { stepNumber, previousStep, data } = useStepsContainerContext();
  const { setMenuBarConfig } = usePageContext();

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
        <SimulationMatriculaStep nextButtonLabel="Continuar" />
        <ContractStep />
        {data.showFilters ? <RefinFilterStep /> : <RefinResultsStep />}
      </StepsContainer>
    </Flex>
  );
};

export const RefinanciamentoProvided: FC = () => {
  const history = useHistory();

  return (
    <StepsContainerProvider
      onCloseCb={() => history.push(mainRoutePaths.INICIO)}
    >
      <Refinanciamento />
    </StepsContainerProvider>
  );
};
