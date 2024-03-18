import { FC, useState } from 'react';

import { Flex, Icon, Text, Button, useBreakpointValue } from '@chakra-ui/react';

import { CustomHeading, useStepsContainerContext } from '@pcf/design-system';
import { CameraIcon } from '@pcf/design-system-icons';

interface ContentProps {
  title: string;
  info?: string;
  icon?: FC;
  buttonLabel: string;
  onButtonClick: () => void;
}

const Content: FC<ContentProps> = ({
  title,
  info,
  icon,
  buttonLabel,
  onButtonClick,
  children,
}) => (
  <Flex flex={1} direction="column" width="100%" justifyContent="center">
    <Flex alignItems="center" margin="auto" direction="column" width="100%">
      {icon && (
        <Icon
          as={icon}
          fill="secondary.mid-dark"
          height="92px"
          width="116px"
          marginBottom="24px"
        />
      )}

      <CustomHeading
        as="h2"
        textStyle="bold24_32"
        color="secondary.mid-dark"
        textAlign="center"
        marginBottom="16px"
      >
        {title}
      </CustomHeading>

      {children}

      {info && (
        <Text
          textStyle="regular16"
          color="secondary.mid-dark"
          textAlign="center"
          marginTop="16px"
        >
          {info}
        </Text>
      )}
    </Flex>

    <Button onClick={onButtonClick} marginTop={2}>
      {buttonLabel}
    </Button>
  </Flex>
);

export const RequestCameraStep: FC = () => {
  const [refusedCamera, setRefusedCamera] = useState(false);
  const { nextStep } = useStepsContainerContext();
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const requestCameraFn = (): void => {
    navigator.mediaDevices
      .getUserMedia({ video: true })
      .then(() => {
        // Workaround: https://github.com/streamich/react-use/issues/1318
        navigator.mediaDevices.dispatchEvent(new Event('devicechange'));

        nextStep();
      })
      .catch(() => setRefusedCamera(true));
  };

  return !refusedCamera ? (
    <Content
      icon={CameraIcon}
      title="Habilite sua câmera"
      info={`Por favor, habilite sua ${
        isMobile ? 'câmera frontal.' : 'webcam.'
      } `}
      buttonLabel="Habilitar câmera"
      onButtonClick={requestCameraFn}
    />
  ) : (
    <Content
      icon={CameraIcon}
      title="Habilite sua câmera"
      info={`Suas permissões de câmera estão desabilitadas. Por favor, verifique as configurações ${
        isMobile ? 'do seu celular.' : 'do seu navegador.'
      }`}
      buttonLabel="Habilitar câmera"
      onButtonClick={requestCameraFn}
    />
  );
};
