import {
  createContext,
  FC,
  RefObject,
  useContext,
  useRef,
  useState,
} from 'react';

import { bemTheme } from '@pcf/design-system';

export interface MenuConfig {
  menuBarColor?: string;

  hasContextMenu?: boolean;
  fixedMenuScrollOffset: number;
  contextMenuBarColor?: string;
  contextMenuTitle?: string;
  contextMenuCallback?: () => void;
}

export interface PageContextData {
  menuBarConfig: MenuConfig;
  setMenuBarConfig: (config: Partial<MenuConfig>) => void;
  pageRef: RefObject<HTMLElement>;
  setPageRef: (ref: RefObject<HTMLElement>) => void;
}

const defaultConfig = {
  fixedMenuScrollOffset: parseInt(bemTheme.sizes.menu.height, 10),
  menuBarColor: 'transparent',
  contextMenuBarColor: 'primary.gradient',
  contextMenuTitle: 'title',
  hasContextMenu: true,
};

const PageContext = createContext<PageContextData>({} as PageContextData);

const PageContextProvider: FC = ({ children }) => {
  const [menuBarConfig, setMenuBarConfig] = useState<MenuConfig>(defaultConfig);
  const [pageRef, setPageRef] = useState<RefObject<HTMLElement>>(useRef(null));

  return (
    <PageContext.Provider
      value={{
        menuBarConfig,
        setMenuBarConfig: (configs: Partial<MenuConfig>) =>
          setMenuBarConfig({ ...defaultConfig, ...configs }),
        pageRef,
        setPageRef,
      }}
    >
      {children}
    </PageContext.Provider>
  );
};

function usePageContext(): PageContextData {
  const context = useContext(PageContext);

  return context;
}

export { PageContextProvider, usePageContext };
