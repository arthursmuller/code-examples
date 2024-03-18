import { cloneElement, FC } from 'react';

import { useFeatureFlags } from './feature-toggle.context';

export const isEnabledValidator =
  (flagKey: string): FC =>
  ({ children }) => {
    const { flags, isLoading } = useFeatureFlags();

    const hasAccess = flags && flags[flagKey];

    return (
      <>
        {children &&
          cloneElement(children as any, {
            disabled: isLoading || !hasAccess,
            isLoading,
          })}
      </>
    );
  };
