import { MutableRefObject, useState, useEffect } from 'react';

import { useKeyPress } from '../../hooks/use-key-press';

export interface useVirtuosoKeyboardNavigationData {
  currentItemIndex: number;
  setCurrentItemIndex: (value: number) => void;
}

export const useVirtuosoKeyboardNavigation = (
  virtuosoRef: MutableRefObject<any>,
  searchInputRef: MutableRefObject<any>,
): useVirtuosoKeyboardNavigationData => {
  const [currentItemIndex, setCurrentItemIndex] = useState(-1);

  const arrowUpPressed = useKeyPress(searchInputRef.current, 'ArrowUp');
  const arrowDownPressed = useKeyPress(searchInputRef.current, 'ArrowDown');

  useEffect(() => {
    if (arrowUpPressed) {
      const nextIndex = currentItemIndex - 1;

      virtuosoRef?.current?.scrollIntoView({
        index: nextIndex,
        behavior: 'auto',
        done: () => {
          setCurrentItemIndex(nextIndex);
        },
      });
    }
  }, [arrowUpPressed]);

  useEffect(() => {
    if (arrowDownPressed) {
      const nextIndex = currentItemIndex + 1;

      virtuosoRef?.current?.scrollIntoView({
        index: nextIndex,
        behavior: 'auto',
        done: () => {
          setCurrentItemIndex(nextIndex);
        },
      });
    }
  }, [arrowDownPressed]);

  return {
    currentItemIndex,
    setCurrentItemIndex,
  };
};
