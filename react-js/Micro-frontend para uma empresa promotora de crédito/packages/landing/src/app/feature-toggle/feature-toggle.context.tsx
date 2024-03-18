import { createContext, FC, useContext, useState } from 'react';

import { FeatureFlags } from './feature-toggle';

type FeatureToggleContextData = FeatureFlags;

const FeatureToggleContext = createContext<FeatureFlags>({} as FeatureFlags);

const FeatureToggleContextProvider: FC<{ flags: FeatureFlags }> = ({
  flags,
  children,
}) => {
  const [stateFlags] = useState<FeatureFlags>(flags || {});

  return (
    <FeatureToggleContext.Provider value={stateFlags}>
      {children}
    </FeatureToggleContext.Provider>
  );
};

function useFeatureFlag(): FeatureToggleContextData {
  const context = useContext(FeatureToggleContext);

  if (!context) {
    throw new Error(
      'useFeatureFlag must be used within FeatureToggleContextProvider',
    );
  }

  return context;
}

export { FeatureToggleContextProvider, useFeatureFlag };
