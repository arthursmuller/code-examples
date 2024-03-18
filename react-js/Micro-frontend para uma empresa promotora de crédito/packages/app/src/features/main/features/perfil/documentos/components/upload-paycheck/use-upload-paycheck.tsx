import { FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import {
  StepsActionDialog,
  useModal,
  useIsCameraEnabled,
} from '@pcf/design-system';
import { ContrachequeIcon } from '@pcf/design-system-icons';
import { DocumentTypeCode } from '@pcf/core';

import { UploadDocumentStep } from '../shared-steps/upload-document-step';
import {
  AllowCameraAccessStep,
  AttachFilesStep,
  DocumentGuidelinesStep,
  DocumentGuidelinesWithCameraStep,
  UploadGuidelinesStep,
} from '../shared-steps';

const UploadIdentity: FC = () => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');
  const { hadCamera } = useIsCameraEnabled();

  return (
    <StepsActionDialog>
      {!isDesktop && <UploadGuidelinesStep />}
      {!isDesktop && !hadCamera && <AllowCameraAccessStep />}
      {!isDesktop && (
        <DocumentGuidelinesWithCameraStep
          cameraInputTitle="Tirar foto do Comprovante"
          title="Tire foto da frente do seu Contracheque (Holerite)"
          icon={ContrachequeIcon}
        />
      )}
      {!!isDesktop && (
        <DocumentGuidelinesStep
          title="Contracheque (Holerite)"
          subtitle="Anexe o seu Contracheque (Holerite) ao nosso sistema."
          info="Este documento demonstra e certifica o pagamento do seu salário, sendo depositado em sua conta bancária."
          icon={ContrachequeIcon}
        />
      )}
      {!!isDesktop && (
        <AttachFilesStep
          maxFiles={1}
          info="Arraste um ÚNICO arquivo contendo a FRENTE do seu documento para cá"
        />
      )}
      <UploadDocumentStep documentTypeCode={DocumentTypeCode.Holerite} />
    </StepsActionDialog>
  );
};

export const useUploadPaycheck = (): (() => void) => {
  const { showModal } = useModal();

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: <UploadIdentity />,
    });
  };
};
