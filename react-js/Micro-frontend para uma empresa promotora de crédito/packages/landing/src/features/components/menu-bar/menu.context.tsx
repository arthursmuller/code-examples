import { createContext, useContext, FC, useEffect } from 'react';

import { useDisclosure, UseDisclosureReturn } from '@chakra-ui/react';
import { useToggle } from 'react-use';

interface MenuContextData extends UseDisclosureReturn {
  onToggleMobileExtraContent(): void;
  showMobileExtraContent: boolean;
}

const MenuContext = createContext<MenuContextData>({} as MenuContextData);

const MenuContextProvider: FC = ({ children }) => {
  const disclosureProps = useDisclosure();
  const [showMobileExtraContent, toggleMobileExtraContent] = useToggle(false);

  useEffect(() => {
    if (!disclosureProps.isOpen) {
      toggleMobileExtraContent(false);
    }
  }, [disclosureProps.isOpen]);

  return (
    <MenuContext.Provider
      value={{
        ...disclosureProps,
        showMobileExtraContent,
        onToggleMobileExtraContent: toggleMobileExtraContent,
      }}
    >
      {children}
    </MenuContext.Provider>
  );
};

function useMenuContext(): MenuContextData {
  const context = useContext(MenuContext);

  if (!context) {
    throw new Error('useMenuContext must be used within MenuContextProvider');
  }

  return context;
}

export { MenuContextProvider, useMenuContext };
