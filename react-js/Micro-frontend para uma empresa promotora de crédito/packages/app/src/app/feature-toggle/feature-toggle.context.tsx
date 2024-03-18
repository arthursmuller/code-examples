import { createContext, FC, useContext, useMemo } from 'react';

import { flattenFlags, useFeatureFlagsQuery } from '@pcf/core';

import { FeatureFlags, FEATURES } from './feature-toggle';

interface FeatureToggleContextData {
  flags: FeatureFlags;
  isLoading: boolean;
}

const FeatureToggleContext = createContext<FeatureToggleContextData>(
  {} as FeatureToggleContextData,
);

const FeatureToggleContextProvider: FC = ({ children }) => {
  const { isLoading, data: backendFlags = [] } = useFeatureFlagsQuery(
    undefined,
    {
      useErrorBoundary: false,
      retry: 1,
    },
  );

  const flags = useMemo(() => {
    let featureToggle: FeatureFlags = {
      ...flattenFlags(backendFlags || []),
    };

    if (process.env.NODE_ENV === 'development') {
      featureToggle = {
        ...featureToggle,
        ...FEATURES,
      };
    }

    return featureToggle;
  }, [backendFlags]);

  return (
    <FeatureToggleContext.Provider value={{ flags, isLoading }}>
      {children}
    </FeatureToggleContext.Provider>
  );
};

function useFeatureFlags(): FeatureToggleContextData {
  const context = useContext(FeatureToggleContext);

  if (!context) {
    throw new Error('useFeatureFlags must be used within FeatureToggleContext');
  }

  return context;
}

export { FeatureToggleContextProvider, useFeatureFlags };
