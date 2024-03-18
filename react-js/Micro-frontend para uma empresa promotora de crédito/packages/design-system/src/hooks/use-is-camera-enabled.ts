import { useEffect, useState } from 'react';

import { useMediaDevices } from 'react-use';

export const useIsCameraEnabled = (): {
  hasCamera: boolean | undefined;
  hadCamera: boolean;
} => {
  const { devices } = useMediaDevices() as {
    devices: { kind: string; deviceId: string }[];
  };
  const [hasCamera, setHasCamera] = useState<boolean | undefined>();
  const [hadCamera, setHadCamera] = useState<boolean | undefined>();

  useEffect((): void => {
    if (devices) {
      const checkCamera =
        !!devices?.find((d) => d.kind === 'videoinput')?.deviceId || false;

      if (hadCamera === undefined && checkCamera !== undefined) {
        setHadCamera(checkCamera);
      }

      setHasCamera(checkCamera);
    }
  }, [devices]);

  return { hasCamera, hadCamera };
};
