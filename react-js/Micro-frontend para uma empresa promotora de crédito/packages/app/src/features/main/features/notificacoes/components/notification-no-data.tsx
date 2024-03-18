import { FC } from 'react';

import { Flex, Text, Center, Icon, FlexProps } from '@chakra-ui/react';

import { CheckIcon } from '@pcf/design-system-icons';

export const NotificationNoData: FC<FlexProps> = (props) => (
  <Flex {...props} alignItems="center">
    <Center
      height={9}
      minWidth={9}
      borderRadius="full"
      border="1px solid"
      borderColor="grey.800"
      marginRight={6}
    >
      <Icon as={CheckIcon} />
    </Center>
    <Text textStyle="bold24">
      Tudo certo! No momento, você não possui novas notificações.
    </Text>
  </Flex>
);
