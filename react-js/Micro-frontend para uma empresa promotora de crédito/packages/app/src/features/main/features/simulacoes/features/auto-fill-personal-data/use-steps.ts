import { useReducer } from 'react';

import { StepItemData, StepStatus } from '@pcf/design-system';

enum StepsActions {
  NEXT_STEP = 'NEXT_STEP',
  PREVIOUS_STEP = 'PREVIOUS_STEP',
}

interface StepAction {
  type: StepsActions;
}

interface StepsState {
  steps: StepItemData[];
  hasNext?: boolean;
  hasPrevious?: boolean;
  currentStepName: string;
}

function stepsReducer(state: StepsState, action: StepAction): StepsState {
  const { type } = action;

  const currentStepIndex = state.steps.findIndex(
    (step) => step.label === state.currentStepName,
  );

  switch (type) {
    case StepsActions.PREVIOUS_STEP:
      return {
        ...state,
        hasNext: state.steps.length !== currentStepIndex - 1,
        hasPrevious: currentStepIndex - 1 !== 0,
        currentStepName: state.steps[currentStepIndex - 1].label,
        steps: state.steps.map((step, currentIndex) => {
          if (currentIndex > currentStepIndex) {
            return {
              ...step,
              status: StepStatus.inactive,
            };
          }
          if (currentIndex === currentStepIndex) {
            return {
              ...step,
              status: StepStatus.inactive,
            };
          }
          if (currentIndex === currentStepIndex - 1) {
            return {
              ...step,
              status: StepStatus.active,
            };
          }
          return step;
        }),
      };

    case StepsActions.NEXT_STEP: {
      return {
        ...state,
        hasNext: state.steps.length - 1 > currentStepIndex + 1,
        hasPrevious: currentStepIndex + 1 !== 0,
        currentStepName: state.steps[currentStepIndex + 1].label,
        steps: state.steps.map((step, currentIndex) => {
          if (currentIndex < currentStepIndex) {
            return {
              ...step,
              status: StepStatus.success,
            };
          }
          if (currentIndex === currentStepIndex) {
            return {
              ...step,
              status: StepStatus.success,
            };
          }
          if (currentIndex === currentStepIndex + 1) {
            return {
              ...step,
              status: StepStatus.active,
            };
          }
          return step;
        }),
      };
    }
    default:
      return state;
  }
}

export interface UseStepsData {
  state: StepsState;
  onNext: () => void;
  onPrevious: () => void;
}

export function useSteps(initialState: StepsState): UseStepsData {
  const [state, dispatch] = useReducer(stepsReducer, {
    ...initialState,
    hasNext: true,
    hasPrevious: false,
  });

  const { hasNext, hasPrevious } = state;

  function onNext(): void {
    if (!hasNext) {
      throw new Error(
        'No nexStep step. Use hasNext to perform a validation before call this method.',
      );
    }

    dispatch({ type: StepsActions.NEXT_STEP });
  }

  function onPrevious(): void {
    if (!hasPrevious) {
      throw new Error(
        'No previous step. Use hasPrevious to perform a validation before call this method.',
      );
    }

    dispatch({ type: StepsActions.PREVIOUS_STEP });
  }

  return { state, onNext, onPrevious };
}
