import { FC, useEffect } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';
import * as acessoWebFrame from 'unico-webframe';

import {
  BemErrorBoundary,
  useStepsContainerContext,
  useModal,
} from '@pcf/design-system';
import { useFeatureFlags } from 'app';

import { UploadDocumentForm } from '../shared-steps/models/upload-document-form.model';

function getHostUrlBase(path: string): string {
  return `${window.location.protocol}//${window.location.host}/${path}`;
}

export const LivenessStepContent: FC = () => {
  const { nextStep } = useStepsContainerContext<UploadDocumentForm>();
  const { hideModal } = useModal();
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');
  const { flags } = useFeatureFlags();

  const handleNext = async (base64: string): Promise<void> => {
    const res: Response = await fetch(`${base64}`);
    const blob: Blob = await res.blob();

    nextStep({ files: [new File([blob], 'liveness', { type: 'image/jpeg' })] });
  };

  useEffect(() => {
    const callback = {
      on: {
        success(obj) {
          handleNext(obj.base64);
        },
        error(error) {
          hideModal();
          console.error(error);
        },
        support(error) {
          console.log(error);
        },
      },
    };

    const layout = {
      silhouette: {
        primaryColor: '#0bbd26',
        secondaryColor: '#bd0b0b',
        neutralColor: '#fff',
      },
      buttonCapture: {
        backgroundColor: '#2980ff',
        iconColor: '#fff',
      },
      popupLoadingHtml:
        '<div style="position: absolute; top: 45%; right: 50%; transform: translate(50%, -50%); z-index: 10; text-align: center;"><Inicializando câmera...></div>',
      boxMessage: {
        backgroundColor: '#2980ff',
        fontColor: '#fff',
      },
      boxDocument: {
        backgroundColor: '#2980ff',
        fontColor: '#fff',
      },
    };

    const configurations = {
      TYPE: flags.LIVENESS_UNICO_INTELIGENTE ? 2 : 1,
    };

    acessoWebFrame.webFrameModel
      .loadModelsCameraInteligence(getHostUrlBase('/models'))
      .then(() => {
        acessoWebFrame.initCamera(configurations, callback, layout);
      })
      .catch((error) => {
        console.error(error);
        // Confira em "Configurações" a lista de erros e demais informações
      });

    return () => {
      acessoWebFrame.closeCamera();
    };
  }, []);

  if (!isMobile) {
    return (
      <Flex
        sx={{
          '#box-camera': {
            w: '800px',
            h: '600px',
            left: '50%',
            top: '50%',
            marginLeft: '-400px',
            marginTop: '-300px',
          },
        }}
        className="container"
      >
        <Flex id="box-camera" top={0} left={0} />
      </Flex>
    );
  }

  return <Flex id="box-camera" top={0} left={0} />;
};

export const LivenessUnicoStep: FC = () => (
  <BemErrorBoundary>
    <LivenessStepContent />
  </BemErrorBoundary>
);
