import { createContext, useContext, useState, FC } from 'react';

type LandingContextData = {
  onShowSimulador(): void;
  showSimulador: boolean;
};

const LandingContext = createContext<LandingContextData>(
  {} as LandingContextData,
);

const LandingContextProvider: FC = ({ children }) => {
  const [showSimulador, setShowSimulador] = useState<boolean>(false);

  const handleShowSimulador = (): void => {
    setShowSimulador(true);
    setTimeout(() => {
      document
        .getElementById('simuladorContainer')
        ?.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }, 300);
  };

  return (
    <LandingContext.Provider
      value={{
        onShowSimulador: handleShowSimulador,
        showSimulador,
      }}
    >
      {children}
    </LandingContext.Provider>
  );
};

function useLandingContext(): LandingContextData {
  const context = useContext(LandingContext);

  if (!context) {
    throw new Error(
      'useLandingContext must be used within LandingContextProvider',
    );
  }

  return context;
}

export { LandingContextProvider, useLandingContext };
