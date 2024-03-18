import React, { FC, useCallback, useRef, useState } from 'react';

import {
  Flex,
  Text,
  Button,
  Icon,
  IconButton,
  useTimeout,
} from '@chakra-ui/react';
import Webcam from 'react-webcam';
import { useToggle } from 'react-use';

import { StatusCloseErrorIcon } from '@pcf/design-system-icons';

import { zIndexes } from '../../../consts/z-indexes.enum';
import { fadeIn } from '../../../animations';
import { ActionDialogHeader } from '../../action-dialog/action-dialog-header';

const backgroundColor = 'rgba(68, 68, 68, 0.8)';
const format = 'image/jpeg';

export interface CameraInputProps {
  onSubmit?: (picture: File) => void;
  onClose?: () => void;
  camera?: 'front' | 'back';
  title: string;
  guideline?: string;
}

export const CameraInput: FC<CameraInputProps> = ({
  title,
  onSubmit,
  onClose,
  camera = 'front',
  guideline,
}) => {
  const webcamRef = useRef<Webcam>(null);
  const [containerRef, setContainerRef] = useState<HTMLDivElement>();
  const [picture, setPicture] = useState<string>();
  const [showGuideline, toggleGuideline] = useToggle(true);

  const frameDimensions =
    containerRef && containerRef.offsetWidth > containerRef.offsetHeight
      ? { x: 100, y: 8 }
      : { x: 32, y: 16 };

  const capture = useCallback(() => {
    const imageSrc = webcamRef.current?.getScreenshot() as string;

    setPicture(imageSrc);
  }, [webcamRef, setPicture]);

  const cameraDirection = {
    facingMode: camera === 'front' ? 'user' : 'environment',
  };

  const handleSubmit = async (): Promise<void> => {
    if (onSubmit && picture) {
      const res: Response = await fetch(picture);
      const blob: Blob = await res.blob();
      const file = new File([blob], `${new Date().getTime()}-from-cam`, {
        type: format,
      });
      onSubmit(file);
    }
  };

  useTimeout(() => showGuideline && toggleGuideline(), 5000);

  return (
    <Flex
      direction="column"
      alignItems="center"
      id="camera-input"
      height="100%"
      width="100%"
      background="grey.900"
    >
      <Flex
        paddingTop="16px"
        background="secondary.regular"
        width="100%"
        zIndex={zIndexes.absoluteElements}
      >
        <ActionDialogHeader
          onClose={() => onClose && onClose()}
          title={title}
        />
      </Flex>

      <Flex
        id="camera-input-content"
        flex={1}
        position="relative"
        flexDirection="column"
        width="100%"
        ref={(ref) => setContainerRef(ref as HTMLDivElement)}
        overflow="hidden"
      >
        <Flex
          id="camera-input-overlay"
          position="absolute"
          direction="column"
          width="100%"
          height="100%"
          zIndex={zIndexes.absoluteElements}
        >
          {picture && (
            <Flex flex={1} alignItems="center" direction="column">
              <Text
                textStyle="regular14"
                color="grey.100"
                paddingX="26px"
                paddingY="12px"
                borderRadius="12px"
                background={backgroundColor}
                marginTop="16px"
              >
                A foto ficou boa?
              </Text>
            </Flex>
          )}

          {!picture && !!guideline && (
            <Flex width="100%" position="absolute" justify="center">
              <Flex
                background="primary.gradient"
                color="grey.100"
                marginX={`${frameDimensions.x + 16}px`}
                marginY={`${frameDimensions.y + 16}px`}
                borderRadius="8px"
                paddingX={2}
                paddingY={1}
                animation={`1000ms ${fadeIn} ease-in-out`}
                transition="opacity 1000ms"
                opacity={showGuideline ? 1 : 0}
                pointerEvents={showGuideline ? 'inherit' : 'none'}
                justify="center"
                align="center"
              >
                <Text>{guideline}</Text>

                <IconButton
                  marginLeft={2}
                  backgroundColor="primary.mid-dark"
                  aria-label="close guideline"
                  variant="ghost"
                  size="sm"
                  isRound
                  onClick={toggleGuideline}
                  icon={<Icon as={StatusCloseErrorIcon} fill="white" />}
                />
              </Flex>
            </Flex>
          )}

          {!picture && (
            <Flex
              id="camera-input-frame"
              flex={1}
              borderX={`${frameDimensions.x}px solid`}
              borderY={`${frameDimensions.y}px solid`}
              borderColor={backgroundColor}
            >
              <Flex flex={1} border="5px solid" borderColor="grey.100" />
            </Flex>
          )}

          <Flex
            id="camera-input-actions"
            paddingX="32px"
            paddingBottom="8px"
            background={!picture ? backgroundColor : 'transparent'}
          >
            {!picture ? (
              <Button onClick={capture} flex={1}>
                Tirar foto
              </Button>
            ) : (
              <>
                <Button
                  onClick={() => setPicture(undefined)}
                  marginRight="16px"
                  color="primary.regular"
                  background="primary.washed"
                  _hover={{
                    background: 'primary.washed',
                  }}
                >
                  Repetir
                </Button>

                <Button autoFocus flex={1} onClick={handleSubmit}>
                  Cadastrar
                </Button>
              </>
            )}
          </Flex>
        </Flex>

        <Flex flex={1} justifyContent="center" align="center">
          {!picture ? (
            <Webcam
              ref={webcamRef}
              audio={false}
              mirrored={camera === 'front'}
              videoConstraints={{ ...cameraDirection }}
              screenshotFormat={format}
              forceScreenshotSourceSize
            />
          ) : (
            <img src={picture} alt="camera results" />
          )}
        </Flex>
      </Flex>
    </Flex>
  );
};
