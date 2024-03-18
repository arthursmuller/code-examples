import { createContext, FC, useContext, useState } from 'react';

import { useGeolocation } from 'react-use';

type AppContextData = {
  latitude: number | null;
  longitude: number | null;
  currentCpf: string;
  setCurrentCpf: (e) => void;
};

const AppContext = createContext<AppContextData>({} as AppContextData);

const AppContextProvider: FC = ({ children }) => {
  const { latitude, longitude } = useGeolocation();
  const [currentCpf, setCurrentCpf] = useState<string>('');

  return (
    <AppContext.Provider
      value={{
        latitude,
        longitude,
        currentCpf,
        setCurrentCpf,
      }}
    >
      {children}
    </AppContext.Provider>
  );
};

function useAppContext(): AppContextData {
  const context = useContext(AppContext);

  if (!context) {
    throw new Error('useAppContext must be used within AppContextProvider');
  }

  return context;
}

export { AppContextProvider, useAppContext };
