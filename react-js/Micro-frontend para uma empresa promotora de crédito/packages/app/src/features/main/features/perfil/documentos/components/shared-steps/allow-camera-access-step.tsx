import { FC } from 'react';

import { Button, Flex, Icon, Text } from '@chakra-ui/react';

import { CameraIcon } from '@pcf/design-system-icons';
import { useStepsContainerContext } from '@pcf/design-system';

export const AllowCameraAccessGuideline: FC = () => {
  return (
    <Flex direction="column" alignItems="center">
      <Icon
        as={CameraIcon}
        height="80px"
        width="80px"
        marginBottom="24px"
        color="secondary.mid-dark"
      />

      <Text as="p" textStyle="regular20" textAlign="center" marginBottom="32px">
        Para tirar a foto do seu documento, precisamos que você autorize o
        acesso à câmera do seu celular.
      </Text>

      <Text
        as="h3"
        textStyle="bold20"
        color="secondary.mid-dark"
        textAlign="center"
        marginBottom="32px"
      >
        Na tela seguinte, iremos pedir a sua autorização. Tudo bem?
      </Text>
    </Flex>
  );
};

export const AllowCameraAccessStep: FC = () => {
  const { nextStep, finish } = useStepsContainerContext();

  return (
    <Flex direction="column">
      <AllowCameraAccessGuideline />

      <Button onClick={() => nextStep()} marginBottom="24px">
        Entendi
      </Button>
      <Button variant="link" onClick={finish}>
        Terminar depois
      </Button>
    </Flex>
  );
};
