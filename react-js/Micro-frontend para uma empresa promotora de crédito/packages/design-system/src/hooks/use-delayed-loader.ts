import { useState } from 'react';

import { useMount } from 'react-use';

export const useDelayedLoading = (
  isLoading: boolean,
  timeMS = 800,
): boolean => {
  const [timeElapsed, setTimeElapsed] = useState(false);

  useMount(() => {
    setTimeout(() => {
      setTimeElapsed(true);
    }, timeMS);
  });

  return !timeElapsed ? true : isLoading;
};
