import { ReactElement, FC } from 'react';

import { Button, Flex, Icon, Text } from '@chakra-ui/react';

import { ActionEditIcon, ActionTrashIcon } from '@pcf/design-system-icons';

import { DataDisplayCard } from '.';

export default {
  title: 'Data Display Card',
  component: DataDisplayCard,
  decorators: [
    (Story: FC): ReactElement => (
      <Flex flexDir="column" h="90vh">
        <Story />
      </Flex>
    ),
  ],
};

export const Default: FC = () => (
  <DataDisplayCard>
    <DataDisplayCard.Header>
      <Text color="white">Telefone</Text>
    </DataDisplayCard.Header>
    <DataDisplayCard.Content>(51) 9999-8888</DataDisplayCard.Content>
    <DataDisplayCard.Footer>
      <Button
        variant="link"
        size="sm"
        mr={6}
        colorScheme="secondary"
        rightIcon={<Icon as={ActionEditIcon} w="12.47px" h="12.47px" />}
      >
        Editar
      </Button>
      <Button
        variant="link"
        size="sm"
        colorScheme="grey"
        rightIcon={
          <Icon as={ActionTrashIcon} w="12.09px" h="13.33px" color="grey.900" />
        }
      >
        Excluir
      </Button>
    </DataDisplayCard.Footer>
  </DataDisplayCard>
);

export const SomeVariant: FC = () => (
  <DataDisplayCard>
    <DataDisplayCard.Header bg="secondary.mid-light">
      <Text color="white">Telefone Alternativo</Text>
    </DataDisplayCard.Header>
    <DataDisplayCard.Content bg="gray.100">
      (51) 9999-8888
    </DataDisplayCard.Content>
  </DataDisplayCard>
);
