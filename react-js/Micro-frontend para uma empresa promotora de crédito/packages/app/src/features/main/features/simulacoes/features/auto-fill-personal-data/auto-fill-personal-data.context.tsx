import { createContext, FC, useContext } from 'react';

import { StepItemData, StepStatus } from '@pcf/design-system';

import { useSteps, UseStepsData } from './use-steps';

type AutoFillPersonalDataContext = UseStepsData;

export const AutoFillPersonalDataContext =
  createContext<AutoFillPersonalDataContext>({} as AutoFillPersonalDataContext);

export const STEPS_NAME = {
  DADOS_BASICOS: 'Dados Básicos',
  DOCUMENTO_PESSOAL: 'Documento Pessoal',
  TELEFONES: 'Telefones',
  EMAILS: 'Emails',
  ENDERECOS: 'Endereços',
  RENDIMENTOS: 'Rendimentos',
  DADOS_RECEBIMENTO_TED: 'Dados para recebimento TED',
  ANEXOS_OBRIGATORIOS: 'Anexos Obrigatórios',
};

const items: StepItemData[] = [
  {
    label: STEPS_NAME.DADOS_BASICOS,
    status: StepStatus.active,
  },
  {
    label: STEPS_NAME.DOCUMENTO_PESSOAL,
    status: StepStatus.inactive,
  },
  {
    label: STEPS_NAME.TELEFONES,
    status: StepStatus.inactive,
  },
  {
    label: STEPS_NAME.EMAILS,
    status: StepStatus.inactive,
  },
  {
    label: STEPS_NAME.ENDERECOS,
    status: StepStatus.inactive,
  },
  {
    label: STEPS_NAME.RENDIMENTOS,
    status: StepStatus.inactive,
  },
  // TODO: Waiting backend...
  // {
  //   label: STEPS.DADOS_RECEBIMENTO_TED,
  //   status: StepStatus.inactive,
  // },
  {
    label: STEPS_NAME.ANEXOS_OBRIGATORIOS,
    status: StepStatus.inactive,
  },
];

const AutoFillPersonalDataContextProvider: FC = ({ children }) => {
  const stepsData = useSteps({
    steps: items,
    currentStepName: 'Dados Básicos',
  });

  return (
    <AutoFillPersonalDataContext.Provider value={{ ...stepsData }}>
      {children}
    </AutoFillPersonalDataContext.Provider>
  );
};

function useAutoFillPersonalDataContext(): AutoFillPersonalDataContext {
  const context = useContext(AutoFillPersonalDataContext);

  if (!context) {
    throw new Error(
      'useAutoFillPersonalDataContext must be used within AutoFillPersonalDataContextProvider',
    );
  }

  return context;
}

const AutoFillPersonalDataContextProviderWrapped: FC = ({ children }) => (
  <AutoFillPersonalDataContextProvider>
    {children}
  </AutoFillPersonalDataContextProvider>
);

export {
  AutoFillPersonalDataContextProviderWrapped as AutoFillPersonalDataContextProvider,
  useAutoFillPersonalDataContext,
};
