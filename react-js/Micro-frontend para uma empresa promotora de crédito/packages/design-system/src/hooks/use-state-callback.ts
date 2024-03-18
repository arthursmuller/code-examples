import { useEffect, useRef, useState } from 'react';

export const useStateCallback = <T>(
  initialState: T,
): {
  state: T;
  setStateCallback: (
    nextState: T,
    cb?: ((nextData?: T | undefined) => void) | undefined,
  ) => void;
} => {
  const [state, setState] = useState(initialState);
  const cbRef = useRef<(nextData?: T) => void>();

  const setStateCallback = (
    nextState: T,
    cb?: (nextData?: T) => void,
  ): void => {
    cbRef.current = cb;
    setState(nextState);
  };

  useEffect(() => {
    if (cbRef.current) {
      cbRef.current(state);
      cbRef.current = undefined;
    }
  }, [state]);

  return { state, setStateCallback };
};
