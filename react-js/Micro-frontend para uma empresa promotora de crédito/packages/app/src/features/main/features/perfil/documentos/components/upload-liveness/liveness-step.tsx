import { FC, useEffect } from 'react';

import { Button, Center, Text } from '@chakra-ui/react';

import { useLiveness } from '@pcf/liveness';
import {
  BemErrorBoundary,
  Loader,
  useStepsContainerContext,
  useModal,
} from '@pcf/design-system';
import { getQueryFor, postQueryFor } from '@pcf/core';

import { UploadDocumentForm } from '../shared-steps/models/upload-document-form.model';

export const LivenessStepContent: FC = () => {
  const {
    previousStep,
    nextStep,
  } = useStepsContainerContext<UploadDocumentForm>();
  const { hideModal, showModal } = useModal();

  const handleNext = async (base64: string): Promise<void> => {
    const res: Response = await fetch(`data:image/jpeg;base64,${base64}`);
    const blob: Blob = await res.blob();

    nextStep({ files: [new File([blob], 'liveness', { type: 'image/png' })] });
  };

  const { loading, loaded, hasError, start, load } = useLiveness({
    deviceKeyIdentifier: process.env.REACT_APP_FACETEC_DEVICE_KEY_IDENTIFIER,
    publicFaceScanEncryptionKey:
      process.env.REACT_APP_FACETEC_PUBLIC_FACE_SCAN_ENCRYPTION_KEY,
    productionKey: process.env.REACT_APP_FACETEC_PRODUCTION_KEY,
    onComplete: (isSuccessful, result) => {
      if (isSuccessful) {
        handleNext(result.auditTrailImage);
      } else {
        previousStep();
      }
    },
    onCancel: (isLockedOut: boolean) => {
      if (isLockedOut) {
        hideModal();

        showModal({
          title: 'Erro no processo de captura',
          information:
            'Muitas tentativas sem sucesso ocorreram. Por favor, tente novamente em 5 minutos.',
          closeOnClickOverlay: false,
          type: 'error',
          closeText: 'Fechar',
        });
      } else previousStep();
    },
    sessionRequestFn: getQueryFor('validacoes-biometricas/token'),
    livenessRequestFn: postQueryFor('validacoes-biometricas/liveness'),
  });

  useEffect(() => {
    if (loaded && !hasError) {
      start();
    }
  }, [loaded, hasError]);

  if (!loaded && !hasError) {
    return (
      <Center flexDirection="column">
        <Loader />
        <Text>Carregando...</Text>
      </Center>
    );
  }

  if (!loaded && hasError) {
    return (
      <Center flexDirection="column">
        <Text color="error.primary">
          Um erro ocorreu ao carregar a funcionalidade de Liveness, por favor
          tente novamente, ou aguarde 5 minutos.
        </Text>
        <Button colorScheme="error" onClick={load} isLoading={loading}>
          Tentar novamente
        </Button>
      </Center>
    );
  }

  return <Text>Realizando Liveness...</Text>;
};

export const LivenessStep: FC = () => (
  <BemErrorBoundary>
    <LivenessStepContent />
  </BemErrorBoundary>
);
