import React, {
  createContext,
  FC,
  MutableRefObject,
  useContext,
  useRef,
  useState,
  // useEffect,
} from 'react';

import {
  useVirtuosoKeyboardNavigation,
  useVirtuosoKeyboardNavigationData,
} from './use-virtuoso-keyboard-navigation';

// import { useKeyPress } from '../../hooks/use-key-press';

interface SearchInputContextProviderProps {
  onPressEnter: (item: any) => void;
}
interface SearchInputContextData
  extends useVirtuosoKeyboardNavigationData,
    SearchInputContextProviderProps {
  searchInputRef: MutableRefObject<any>;
  virtuosoRef: MutableRefObject<any>;
  isSearchInputFocused: boolean;
  setIsSearchInputFocused: (value: boolean) => void;
}

export const SearchInputContext = createContext<SearchInputContextData>(
  {} as SearchInputContextData,
);

const SearchInputContextProvider: FC<SearchInputContextProviderProps> = ({
  children,
  onPressEnter,
}) => {
  const searchInputRef = useRef(null);
  const virtuosoRef = useRef(null);
  const [isSearchInputFocused, setIsSearchInputFocused] = useState(false);
  const { currentItemIndex, setCurrentItemIndex } =
    useVirtuosoKeyboardNavigation(virtuosoRef, searchInputRef);

  // const enterPressed = useKeyPress(searchInputRef.current, 'Enter');

  // useEffect(() => {
  //   if (enterPressed) {
  //     onPressEnter(currentItemIndex);
  //   }
  // }, [enterPressed, currentItemIndex, onPressEnter]);

  return (
    <SearchInputContext.Provider
      value={{
        virtuosoRef,
        currentItemIndex,
        setCurrentItemIndex,
        isSearchInputFocused,
        setIsSearchInputFocused,
        searchInputRef,
        onPressEnter,
      }}
    >
      {children}
    </SearchInputContext.Provider>
  );
};

function useSearchInputContext(): SearchInputContextData {
  const context = useContext(SearchInputContext);

  if (!context) {
    throw new Error(
      'useSearchInputContext must be used within SearchInputContextProvider',
    );
  }

  return context;
}

export { SearchInputContextProvider, useSearchInputContext };
