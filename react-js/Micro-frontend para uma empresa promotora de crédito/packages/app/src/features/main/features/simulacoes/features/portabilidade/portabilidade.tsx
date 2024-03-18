import { FC, useState } from 'react';

import { Button, Flex, Icon } from '@chakra-ui/react';
import { useMount } from 'react-use';
import { useHistory } from 'react-router-dom';
import { AnimatePresence } from 'framer-motion';

import {
  StepsContainer,
  StepsContainerProvider,
  useStepsContainerContext,
} from '@pcf/design-system';
import { usePageContext } from 'components/page/page.context';
import { mainRoutePaths } from 'features/main/routes/main-routes-paths';
import { HelpIcon } from '@pcf/design-system-icons';

import { PortabilidadeFormData } from './models/portabilidade-form.model';
import { SimulationValueStep } from './components/portabilidade-type-step';
import { PortabilidadeForm } from './components/portabilidade-form';
import { PortabilidadeGuidelines } from './components/portabilidade-guidelines';
import { PortabilidadeResults } from './components/portabilidade-results';

import { SimulationMatriculaStep } from '../../components';

const title = 'Portabilidade';

const Portabilidade: FC = () => {
  const { stepNumber, previousStep } =
    useStepsContainerContext<PortabilidadeFormData>();
  const { setMenuBarConfig } = usePageContext();

  const [showHelp, setShowHelp] = useState<boolean>();

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
      justifyContent="center"
    >
      <Flex flex={1} maxWidth="1060px" marginX={4}>
        <StepsContainer
          onBack={previousStep}
          stepNumber={stepNumber}
          size="lg"
          containerHeight="full"
          colorScheme="white"
          title={title}
          titleTag="h2"
          customAction={
            <Flex
              alignItems="center"
              height="56px"
              marginX={[4, 4, 0]}
              marginTop={[-3, -3, 0]}
              position="absolute"
              top={0}
              right={0}
            >
              <Button
                variant="link"
                color="white"
                onClick={() => setShowHelp(true)}
              >
                Ajuda
              </Button>
              <Icon as={HelpIcon} marginLeft="12px" color="white" />
            </Flex>
          }
        >
          <SimulationValueStep />
          <SimulationMatriculaStep nextButtonLabel="Continuar" />
          <PortabilidadeForm />
          <PortabilidadeResults />
        </StepsContainer>
      </Flex>

      <AnimatePresence>
        {showHelp && (
          <PortabilidadeGuidelines onClose={() => setShowHelp(false)} />
        )}
      </AnimatePresence>
    </Flex>
  );
};

export const PortabilidadeProvided: FC = () => {
  const history = useHistory();

  return (
    <StepsContainerProvider
      onCloseCb={() => history.push(mainRoutePaths.INICIO)}
    >
      <Portabilidade />
    </StepsContainerProvider>
  );
};
