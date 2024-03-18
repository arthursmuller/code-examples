import { CameraInput, CameraInputProps } from './camera-input';

import { useModal } from '../../modal';

export const useCameraInput = (configs: CameraInputProps): (() => void) => {
  const { showModal, hideModal } = useModal();

  const onSubmit = (picture: File): void => {
    configs.onSubmit && configs.onSubmit(picture);
    hideModal();
  };

  const onClose = (): void => {
    configs.onClose && configs.onClose();
    hideModal();
  };

  const showCameraInputDialog = (): void =>
    showModal({
      closeOnClickOverlay: false,
      modal: <CameraInput {...configs} onSubmit={onSubmit} onClose={onClose} />,
    });

  return showCameraInputDialog;
};
