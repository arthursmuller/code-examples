import {
  MutableRefObject,
  useCallback,
  useEffect,
  useRef,
  useState,
} from 'react';
import { use100vh } from 'react-div-100vh';

import { useLayoutEffectSSR } from '../../../../hooks';

export interface Offsets {
  height: number;
  bottom: number;
  width: number;
  top: number;
}

export const useCalculateOffset = (
  containerRef: MutableRefObject<HTMLDivElement>,
  isMenuOpen: boolean,
): Offsets => {
  const scrollEventCb = useRef<() => void>();
  const [menuPosOffset, setMenuPos] = useState<Offsets>({
    height: 0,
    bottom: 0,
    width: 0,
    top: 0,
  });
  const vhHeight = use100vh();

  const setMenuOffsets = useCallback((): void => {
    const refPos = containerRef.current.getBoundingClientRect();

    const nextBottom = (vhHeight || 0) - refPos.top;

    setMenuPos({
      height: refPos.height,
      width: refPos.width,
      bottom: nextBottom,
      top: refPos.top,
    });
  }, [vhHeight]);

  useLayoutEffectSSR(() => {
    setMenuOffsets && setMenuOffsets()
  }, [setMenuOffsets, isMenuOpen])

  useEffect(() => {
    if (isMenuOpen) {
      scrollEventCb.current = setMenuOffsets;
      document.addEventListener('scroll', setMenuOffsets, true);
    }

    return () =>
      scrollEventCb.current &&
      document.removeEventListener('scroll', scrollEventCb.current, true);
  }, [setMenuOffsets, isMenuOpen]);

  return menuPosOffset;
};
