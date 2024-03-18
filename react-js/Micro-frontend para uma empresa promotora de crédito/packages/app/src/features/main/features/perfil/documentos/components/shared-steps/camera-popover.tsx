import { FC } from 'react';

import { Button } from '@chakra-ui/react';

import {
  CameraInputProps,
  useCameraInput,
  ModalProvider,
} from '@pcf/design-system';

const CameraPopover: FC<CameraInputProps> = (config) => {
  const showCameraInput = useCameraInput(config);

  return (
    <Button onClick={showCameraInput} marginBottom="24px">
      Tirar foto
    </Button>
  );
};

const CameraWrapper: FC<CameraInputProps> = (config) => (
  <ModalProvider>
    <CameraPopover {...config} />
  </ModalProvider>
);

export { CameraWrapper as CameraPopover };
