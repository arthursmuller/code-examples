import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { DataDisplayCard, Loader } from '@pcf/design-system';
import { useClienteLogado } from '@pcf/core';

export const EmailList: FC = () => {
  const { data, isLoading } = useClienteLogado();

  if (isLoading) {
    return <Loader />;
  }

  return (
    <DataDisplayCard w="100%" py={6}>
      <DataDisplayCard.Header>
        <Text textStyle="bold16" color="white">
          Principal
        </Text>
      </DataDisplayCard.Header>
      <DataDisplayCard.Content>
        <Text>{data?.email}</Text>
      </DataDisplayCard.Content>
      <DataDisplayCard.Footer />
    </DataDisplayCard>
  );
};
