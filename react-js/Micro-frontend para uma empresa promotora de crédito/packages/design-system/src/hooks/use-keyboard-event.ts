import { useMount } from 'react-use';

export function useKeyboardEvent(key, callback, element?: any): void {
  useMount(() => {
    const handler = (event): void => {
      if (event.key === key) {
        callback();
      }
    };

    const target = element || window;

    target.addEventListener('keydown', handler);

    return () => {
      target.removeEventListener('keydown', handler);
    };
  });
}
