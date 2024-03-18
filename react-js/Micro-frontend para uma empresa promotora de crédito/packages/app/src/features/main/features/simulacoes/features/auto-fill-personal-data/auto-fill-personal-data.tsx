import { FC } from 'react';

import { Box, Flex, Text, useBreakpointValue } from '@chakra-ui/react';

import {
  BemErrorBoundary,
  CustomHeading,
  StepsProgressBar,
} from '@pcf/design-system';

import {
  AutoFillPersonalDataContextProvider,
  STEPS_NAME,
  useAutoFillPersonalDataContext,
} from './auto-fill-personal-data.context';
import {
  DocumentoPessoal,
  AnexosObrigatorios,
  DadosBasicos,
  Telefones,
  Emails,
  Enderecos,
  Rendimentos,
} from './components';

const {
  DADOS_BASICOS,
  DOCUMENTO_PESSOAL,
  TELEFONES,
  EMAILS,
  ENDERECOS,
  RENDIMENTOS,
  ANEXOS_OBRIGATORIOS,
} = STEPS_NAME;

const AutoFillPersonalData: React.FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });
  const { state } = useAutoFillPersonalDataContext();
  const { currentStepName } = state;

  return (
    <Flex flexDir="column" flexGrow={1}>
      <Text mb="35px">
        Para a inclusão automática da simulação, você deve incluir os dados
        solicitados abaixo.
      </Text>

      <Box mb={6}>
        <StepsProgressBar
          isHorizontal
          showLabels={!isMobile}
          items={state.steps}
          size={isMobile ? 'xs' : 'md'}
        />
        <CustomHeading textStyle="bold24" color="secondary.regular">
          {currentStepName}
        </CustomHeading>
      </Box>

      <Flex justifyContent="center" flexGrow={1}>
        <BemErrorBoundary>
          {currentStepName === DADOS_BASICOS && <DadosBasicos />}
          {currentStepName === DOCUMENTO_PESSOAL && <DocumentoPessoal />}
          {currentStepName === TELEFONES && <Telefones />}
          {currentStepName === EMAILS && <Emails />}
          {currentStepName === ENDERECOS && <Enderecos />}
          {currentStepName === RENDIMENTOS && <Rendimentos />}
          {currentStepName === ANEXOS_OBRIGATORIOS && <AnexosObrigatorios />}
        </BemErrorBoundary>
      </Flex>
    </Flex>
  );
};

export const AutoFillPersonalDataWrapped: FC = () => {
  return (
    <AutoFillPersonalDataContextProvider>
      <AutoFillPersonalData />
    </AutoFillPersonalDataContextProvider>
  );
};
