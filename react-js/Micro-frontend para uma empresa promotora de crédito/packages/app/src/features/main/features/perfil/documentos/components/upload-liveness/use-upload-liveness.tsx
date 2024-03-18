import { FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import {
  StepsActionDialog,
  useIsCameraEnabled,
  useModal,
} from '@pcf/design-system';
import { DocumentTypeCode } from '@pcf/core';
import { FeatureToggleContextProvider, useFeatureFlags } from 'app';

import { LivenessStep } from './liveness-step';
import { LivenessUnicoStep } from './liveness-unico-step';

import { UploadGuidelinesStep, RequestCameraStep } from '../shared-steps';
import { UploadDocumentStep } from '../shared-steps/upload-document-step';

const UploadLiveness: FC = () => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');
  const { hasCamera, hadCamera } = useIsCameraEnabled();
  const { flags } = useFeatureFlags();

  return (
    isDesktop !== undefined &&
    hadCamera !== undefined && (
      <StepsActionDialog>
        {!isDesktop && <UploadGuidelinesStep />}
        {(!hasCamera || !hadCamera) && <RequestCameraStep />}
        {!flags.LIVENESS_UNICO ? <LivenessStep /> : <LivenessUnicoStep />}
        <UploadDocumentStep documentTypeCode={DocumentTypeCode.Selfie} />
      </StepsActionDialog>
    )
  );
};

export const useUploadLiveness = (): (() => void) => {
  const { showModal } = useModal();

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <FeatureToggleContextProvider>
          <UploadLiveness />
        </FeatureToggleContextProvider>
      ),
    });
  };
};
