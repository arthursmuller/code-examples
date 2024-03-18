import { cloneElement, FC } from 'react';

import { Tooltip, useBreakpointValue } from '@chakra-ui/react';

import { useContratosQuery } from '@pcf/core';

const message = 'Infelizmente, você não possui contratos com a Bem.';

export const useRefinanciamentoEnabled = (): {
  canAccess: boolean;
  isLoading: boolean;
} => {
  const { isLoading, data } = useContratosQuery(undefined, {
    useErrorBoundary: false,
    retry: false,
    refetchOnMount: false,
  });

  return { canAccess: !!data?.length, isLoading };
};

export const CanUseRefinanciamento: FC = ({ children }) => {
  const { canAccess, isLoading } = useRefinanciamentoEnabled();
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <>
      <Tooltip
        hasArrow={!isMobile}
        label={message}
        isDisabled={canAccess || isLoading}
        shouldWrapChildren
      >
        <span>
          {children &&
            cloneElement(children as any, {
              disabled: isLoading || !canAccess,
              isLoading,
            })}
        </span>
      </Tooltip>
    </>
  );
};
