import { FC } from 'react';

import { Text, Flex, Icon, Center } from '@chakra-ui/react';

import {
  StatusCheckCircleIcon,
  ClockIcon,
  StatusCloseErrorIcon,
} from '@pcf/design-system-icons';
import { IntencaoOperacaoPassoModel } from '@pcf/core';

export enum SituationProposalColors {
  GREEN = 'success.regular',
  RED = 'error.regular',
  GREY = 'grey.800',
}

const IconByColor = {
  [SituationProposalColors.GREEN]: {
    component: (
      <Icon
        as={StatusCheckCircleIcon}
        w={4}
        h={4}
        color={SituationProposalColors.GREEN}
      />
    ),
  },
  [SituationProposalColors.RED]: {
    component: (
      <Center
        boxSize={4}
        borderRadius="full"
        border="solid 1px"
        borderColor={SituationProposalColors.RED}
      >
        <Icon
          as={StatusCloseErrorIcon}
          w="6.99px"
          h="6.99px"
          color={SituationProposalColors.RED}
        />
      </Center>
    ),
  },
  [SituationProposalColors.GREY]: {
    component: <Icon as={ClockIcon} w={4} h={4} color="grey.700" />,
  },
};

interface SituationProposalProps {
  passosProduto?: IntencaoOperacaoPassoModel[];
}

export const SituationProposalText: FC<SituationProposalProps> = ({
  passosProduto = [],
}) => {
  const lastStep = [...passosProduto].pop();
  const currentStep =
    [...passosProduto].reverse().find(({ completo }) => completo) || lastStep;

  let color: SituationProposalColors = SituationProposalColors.GREY;

  if (lastStep?.completo) {
    if (lastStep.excepcional) {
      color = SituationProposalColors.RED;
    } else {
      color = SituationProposalColors.GREEN;
    }
  }

  const { component } = IconByColor[color];

  return (
    <Flex alignItems="center">
      {component}

      <Text textStyle="regular14" color={color} ml={1}>
        {currentStep?.titulo}
      </Text>
    </Flex>
  );
};
