import { FC } from 'react';

import { Flex, Icon, Text } from '@chakra-ui/react';
import { useMount } from 'react-use';

import { AttentionIcon } from '@pcf/design-system-icons';
import { useModal, useStepsContainerContext } from '@pcf/design-system';

interface SimulationResultNoDataProps {
  errorMessage?: string;
}

export const SimulationResultNoData: FC<SimulationResultNoDataProps> = ({
  errorMessage,
}) => {
  const { showModal } = useModal();
  const { previousStep } = useStepsContainerContext();

  useMount(
    () =>
      errorMessage &&
      showModal({
        title: 'A portabilidade não foi finalizada.',
        information: errorMessage,
        closeOnClickOverlay: true,
        type: 'error',
        closeText: 'Tente novamente',
        onClose: previousStep,
      }),
  );

  return (
    <Flex
      direction="column"
      textAlign="center"
      pt={4}
      align="center"
      flexGrow={[1, 1, 0]}
      paddingX={2}
    >
      <Icon
        marginBottom={4}
        as={AttentionIcon}
        height="64px"
        width="64px"
        color="grey.900"
        padding={2}
        border="3px solid"
        borderColor="grey.700"
        borderRadius="full"
        overflow="unset"
      />

      <Text textStyle="regular20" marginBottom={[0, 0, 10]}>
        No momento, não conseguimos encontrar um resultado válido para sua
        solicitação. Tente uma Nova Simulação com outros valores, ou então tente
        novamente mais tarde.
      </Text>
    </Flex>
  );
};
