import { createContext, useContext, useCallback, FC } from 'react';

import { useStepState } from './use-step-state';

import { useStateCallback } from '../../hooks/use-state-callback';

export interface GenericData {
  [key: string]: any; // eslint-disable-line
}

interface StepsContainerContextData<T extends GenericData = GenericData> {
  stepNumber: number;
  previousStep(): void;
  finish(): void;
  nextStep(data?: Partial<T>): void;
  data: T;
  setData(data?: T): void;
}

export const StepsContainerContext = createContext<StepsContainerContextData>(
  {} as StepsContainerContextData,
);

const StepsContainerProvider: FC<{ onCloseCb?: () => void }> = ({
  onCloseCb,
  children,
}) => {
  const { stepNumber, nextStep: goToNextStep, previousStep } = useStepState(
    undefined,
    undefined,
    onCloseCb,
  );
  const {
    state: data,
    setStateCallback: setData,
  } = useStateCallback<GenericData>({});

  const nextStep = useCallback(
    (nextData: GenericData = {}): void => {
      setData({ ...data, ...nextData }, goToNextStep);
    },
    [setData, data, goToNextStep],
  );

  return (
    <StepsContainerContext.Provider
      value={{
        previousStep,
        nextStep,
        finish: onCloseCb || (() => null),
        stepNumber,
        data,
        setData,
      }}
    >
      {children}
    </StepsContainerContext.Provider>
  );
};

function useStepsContainerContext<
  T extends GenericData = GenericData
>(): StepsContainerContextData<T> {
  const context = useContext(
    StepsContainerContext,
  ) as StepsContainerContextData<T>;

  if (!context) {
    throw new Error(
      'useStepsContainerContext must be used within an StepsContainerProvider',
    );
  }

  return context;
}

export { StepsContainerProvider, useStepsContainerContext };
