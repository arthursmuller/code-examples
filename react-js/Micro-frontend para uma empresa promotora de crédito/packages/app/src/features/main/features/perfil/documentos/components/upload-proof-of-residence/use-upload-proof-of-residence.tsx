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

const UploadProofOfResidence: FC = () => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');
  const { hadCamera } = useIsCameraEnabled();

  return (
    <StepsActionDialog>
      {!isDesktop && <UploadGuidelinesStep />}
      {!isDesktop && !hadCamera && <AllowCameraAccessStep />}
      {!isDesktop && (
        <DocumentGuidelinesWithCameraStep
          cameraInputTitle="Tirar foto do Comprovante"
          title="Tire foto da frente do seu Comprovante de Residência"
          info="Conta de água, luz, gás, telefone ou cartão de crédito com no máximo 90 dias de emissão."
          icon={ContrachequeIcon}
        />
      )}
      {!!isDesktop && (
        <DocumentGuidelinesStep
          title="Comprovante de residência"
          subtitle="Anexe o seu comprovante de residência ao nosso sistema."
          info="Você pode usar como comprovante de residência uma conta de água, luz, gás, telefone ou cartão de crédito com no máximo 90 dias de emissão."
          icon={ContrachequeIcon}
        />
      )}
      {!!isDesktop && (
        <AttachFilesStep
          maxFiles={1}
          info="Arraste um ÚNICO arquivo contendo a FRENTE do seu documento para cá"
        />
      )}
      <UploadDocumentStep
        documentTypeCode={DocumentTypeCode.ComprovanteDeResidencia}
      />
    </StepsActionDialog>
  );
};

export const useUploadProofOfResidence = (): (() => void) => {
  const { showModal } = useModal();

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: <UploadProofOfResidence />,
    });
  };
};
