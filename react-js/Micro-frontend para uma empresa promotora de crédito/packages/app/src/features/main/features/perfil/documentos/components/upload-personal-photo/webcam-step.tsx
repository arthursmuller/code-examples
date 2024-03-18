import React, { FC, useState } from 'react';

import {
  Button,
  Avatar,
  Text,
  useBreakpointValue,
  Flex,
} from '@chakra-ui/react';
import Webcam from 'react-webcam';

import { Loader, useStepsContainerContext, zIndexes } from '@pcf/design-system';

import { UploadPersonalPhotoModel } from './upload-personal-photo.model';

const videoConstraints = {
  width: 1280,
  height: 960,
  facingMode: 'user',
};

export const WebcamStep: FC = () => {
  const webcamRef = React.useRef(null);
  const [fotoBase64, setFotoBase64] = useState<string>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { nextStep, previousStep } =
    useStepsContainerContext<UploadPersonalPhotoModel>();
  const isMobile = useBreakpointValue({ base: true, md: false });

  const capture = React.useCallback(() => {
    const imageSrc = webcamRef.current.getScreenshot();
    setFotoBase64(imageSrc);
  }, [webcamRef]);

  return (
    <>
      {!fotoBase64 ? (
        <Flex
          flexDirection="column"
          alignItems="center"
          justifyContent="flex-start"
          position="fixed"
          width="100%"
          height="100%"
          top="0"
          left="0"
          right="0"
          bottom="0"
          zIndex={zIndexes.modal}
          backgroundColor="rgba(0, 0, 0, 0.2)"
          px={[6, 6, 16]}
          pb={[6, 6, 16]}
          pt={!isMobile ? [6, 6, 16] : 0}
        >
          <Flex
            h="600px"
            w="800px"
            position="absolute"
            justifyContent="center"
            alignItems="center"
          >
            <Webcam
              onUserMedia={() => setIsLoading(false)}
              style={{
                borderRadius: '8px',
                height: '100%',
                width: '100%',
                position: 'absolute',
                top: 0,
                left: 0,
                zIndex: 0,
              }}
              audio={false}
              ref={webcamRef}
              screenshotFormat="image/jpeg"
              videoConstraints={
                isMobile
                  ? {
                      height: 700,
                      width: 400,
                      facingMode: 'user',
                    }
                  : videoConstraints
              }
            />

            <Flex
              h="100%"
              w="100%"
              sx={{
                position: 'absolute',
                top: 0,
                left: 0,
                zIndex: 1,
                borderRadius: '8px',
                backgroundColor: 'rgba(0, 0, 0, 0.7)',
              }}
              position="absolute"
            />

            {isLoading ? (
              <>
                <Text color="white" mb={4} zIndex={2}>
                  Inicializando câmera ...
                </Text>
                <Loader />
              </>
            ) : (
              <>
                <Text
                  sx={{
                    position: 'absolute',
                    zIndex: 3,
                    top: '15%',
                    left: '50%',
                    transform: 'translate(-50%,-50%)',
                    color: 'white',
                    px: 4,
                    py: 2,
                    bg: 'primary.regular',
                    borderRadius: '8px',
                  }}
                >
                  Enquadre seu rosto
                </Text>

                <Flex
                  flexDirection="column"
                  sx={{
                    position: 'absolute',
                    zIndex: 3,
                    top: !isMobile ? '100%' : '93%',
                    left: '50%',
                    transform: 'translate(-50%,-50%)',
                  }}
                >
                  <Button mt={6} onClick={capture}>
                    Capturar
                  </Button>

                  <Button
                    variant="link"
                    fontWeight="normal"
                    color="white"
                    mt={6}
                    onClick={previousStep}
                  >
                    Cancelar
                  </Button>
                </Flex>
              </>
            )}

            <Flex
              h="50%"
              w="50%"
              sx={{
                position: 'absolute',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%,-50%)',
                overflow: 'hidden',
                borderRadius: '50%',
                border: '4px solid',
                borderColor: 'primary.regular',
                zIndex: 2,
                pt: '50%',
                h: 0,
              }}
              position="absolute"
            >
              <Webcam
                onUserMedia={() => setIsLoading(false)}
                style={{
                  borderRadius: '8px',
                  height: '200%',
                  width: '200%',
                  position: 'absolute',
                  maxWidth: 'none',
                  top: '-50%',
                  left: '-50%',
                  zIndex: 0,
                }}
                audio={false}
                screenshotFormat="image/jpeg"
              />
            </Flex>
          </Flex>
        </Flex>
      ) : (
        <>
          <Avatar
            alignSelf="center"
            w="100%"
            maxW="400px"
            maxWidth="400px"
            h="400px"
            src={`data:image/jpeg;charset=utf-8;${fotoBase64}`}
            alt="foto"
          />

          <Button mt={6} onClick={() => nextStep({ files: [fotoBase64] })}>
            Continuar
          </Button>

          <Button
            variant="link"
            colorScheme="primary"
            mt={6}
            onClick={() => {
              setIsLoading(true);
              setFotoBase64(null);
            }}
          >
            Tirar novamente
          </Button>
        </>
      )}
    </>
  );
};
