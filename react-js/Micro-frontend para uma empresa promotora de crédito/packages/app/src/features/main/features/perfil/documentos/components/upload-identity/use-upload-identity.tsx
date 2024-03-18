import { FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import {
  StepsActionDialogComp,
  StepsContainerProvider,
  useStepsContainerContext,
  useModal,
  useIsCameraEnabled,
} from '@pcf/design-system';
import { DocumentTypeCode } from '@pcf/core';

import { IdentityTypeStep } from './identity-type-step';
import {
  IdentityGuidelineExtraStep,
  IdentityGuidelinesStep,
} from './identity-guidelines-step';
import { IdentityType } from './identity-types.enum';
import { AttachIdentityStep } from './attach-identity-step';
import { UploadIdentityForm } from './models/upload-identity-form.model';

import { UploadDocumentStep } from '../shared-steps/upload-document-step';
import { AllowCameraAccessStep } from '../shared-steps/allow-camera-access-step';
import { UploadGuidelinesStep } from '../shared-steps/upload-guidelines-step';

const UploadIdentity: FC = () => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');
  const { data } = useStepsContainerContext<UploadIdentityForm>();
  const { hadCamera } = useIsCameraEnabled();

  let documentTypeCode = DocumentTypeCode.PassaporteBrasileiro;
  if (data.documentType === IdentityType.RG)
    documentTypeCode = DocumentTypeCode.RegistroIdentidadeCivil;
  if (data.documentType === IdentityType.CNH)
    documentTypeCode = DocumentTypeCode.CarteiraNacionalDeHabilitacao;

  return (
    <StepsActionDialogComp>
      <IdentityTypeStep />
      {!isDesktop && <UploadGuidelinesStep />}
      {!isDesktop && !hadCamera && <AllowCameraAccessStep />}
      {!isDesktop && <IdentityGuidelinesStep />}
      {!isDesktop && data.documentType === IdentityType.RG && (
        <IdentityGuidelineExtraStep />
      )}
      {!!isDesktop && <AttachIdentityStep />}
      <UploadDocumentStep documentTypeCode={documentTypeCode} />
    </StepsActionDialogComp>
  );
};

export const useUploadIdentity = (): (() => void) => {
  const { hideModal, showModal } = useModal();

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <StepsContainerProvider onCloseCb={hideModal}>
          <UploadIdentity />
        </StepsContainerProvider>
      ),
    });
  };
};
