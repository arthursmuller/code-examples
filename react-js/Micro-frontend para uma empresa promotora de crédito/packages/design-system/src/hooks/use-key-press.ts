import { useEffect, useState } from 'react';

export const useKeyPress = (element, targetKey: any): boolean => {
  const [keyPressed, setKeyPressed] = useState(false);

  useEffect(() => {
    const target = element || window;

    const downHandler = ({ key }): void => {
      if (key === targetKey) {
        setKeyPressed(true);
      }
    };
    const upHandler = ({ key }): void => {
      if (key === targetKey) {
        setKeyPressed(false);
      }
    };

    target.addEventListener('keydown', downHandler);
    target.addEventListener('keyup', upHandler);

    return () => {
      target.removeEventListener('keydown', downHandler);
      target.removeEventListener('keyup', upHandler);
    };
  }, [targetKey]);

  return keyPressed;
};
