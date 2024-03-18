import { FC } from 'react';

import {
  StepsActionDialogComp,
  StepsContainerProvider,
  useStepsContainerContext,
  useModal,
  useIsCameraEnabled,
} from '@pcf/design-system';
import { DocumentTypeCode } from '@pcf/core';

import { UploadPersonalPhotoModel } from './upload-personal-photo.model';
import { PhotoTypeStep } from './photo-type-step';
import { WebcamStep } from './webcam-step';

import { UploadDocumentStep } from '../shared-steps/upload-document-step';
import { AllowCameraAccessStep } from '../shared-steps/allow-camera-access-step';
import { AttachFilesStep, RequestCameraStep } from '../shared-steps';

const UploadPersonalPhoto: FC = () => {
  const { data } = useStepsContainerContext<UploadPersonalPhotoModel>();
  const { hadCamera, hasCamera } = useIsCameraEnabled();
  const isWebcam = data.uploadType === 'webcam';

  return (
    <StepsActionDialogComp>
      <PhotoTypeStep />

      {isWebcam && !hadCamera && <AllowCameraAccessStep />}
      {isWebcam && !hadCamera && <RequestCameraStep />}
      {isWebcam && hasCamera && <WebcamStep />}

      {!isWebcam && (
        <AttachFilesStep
          maxFiles={1}
          info="Arraste um ÚNICO arquivo contendo a foto do seu rosto"
          onlyPictures
        />
      )}

      <UploadDocumentStep
        successMessage="Sua foto foi redefinida com sucesso!"
        documentTypeCode={DocumentTypeCode.FotoPessoal}
      />
    </StepsActionDialogComp>
  );
};

export const useUploadPersonalPhoto = (): (() => void) => {
  const { hideModal, showModal } = useModal();

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <StepsContainerProvider onCloseCb={hideModal}>
          <UploadPersonalPhoto />
        </StepsContainerProvider>
      ),
    });
  };
};
