import { useEffect, useState } from 'react';

interface CallbackArgs {
  mutationList: MutationRecord[];
  observer: MutationObserver;
  targetNode: Node;
}

interface UseMutationObserverArgs<T> {
  initialValue: T;
  targetNode: Node | null;
  config: MutationObserverInit;
  callback: (args: CallbackArgs) => Promise<T>;
}

export const useMutationObserver = <T>({
  initialValue,
  targetNode,
  config,
  callback,
}: UseMutationObserverArgs<T>): T => {
  const [value, setValue] = useState<T>(initialValue);

  // eslint-disable-next-line consistent-return
  useEffect(() => {
    if (targetNode) {
      const mutationObserver = new MutationObserver(
        async (mutationList: MutationRecord[], observer: MutationObserver) => {
          const result = await callback({ mutationList, observer, targetNode });

          setValue(result);
        },
      );

      mutationObserver.observe(targetNode, config);
      return () => {
        mutationObserver.disconnect();
      };
    }
  }, [targetNode, config]);

  return value;
};
