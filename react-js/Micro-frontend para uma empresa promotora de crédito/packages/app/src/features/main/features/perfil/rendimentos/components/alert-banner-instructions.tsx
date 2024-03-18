import { FC } from 'react';

import { Center, Flex, Icon, Text } from '@chakra-ui/react';

import { AttentionIcon } from '@pcf/design-system-icons';

export const AlertBannerInstructions: FC = () => {
  return (
    <Center
      minH="140px"
      flexDir="column"
      bg="warning.washed"
      borderRadius="8px"
      py={[4, 4, 6]}
      px={[4, 4, 6]}
    >
      <Flex
        justifyContent="center"
        alignItems="center"
        borderRadius="full"
        bg="warning.light"
        h="36px"
        w="36px"
      >
        <Icon w="21px" h="19px" as={AttentionIcon} />
      </Flex>

      <Text textAlign="center" textStyle="regular16_20" mt={4}>
        Lembrando que nossos produtos estão disponíveis apenas para os convênios
        INSS DATAPREV e SIAPE, ok?
      </Text>
    </Center>
  );
};
