import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { Loader, CustomHeading, BemErrorBoundary } from '@pcf/design-system';
import {
  getTelefonesQueryConfig,
  TelefoneClienteExibicaoModel,
  TelefoneClienteModel,
  Resource,
} from '@pcf/core';

import { PhonesList } from './components/phones-list';

export const Phones: FC = () => {
  return (
    <Flex flexDir="column" mb={6} minH="270px">
      <CustomHeading
        textStyle="bold24_32"
        as="h2"
        color="secondary.regular"
        mt="17px"
      >
        Telefone
      </CustomHeading>

      <BemErrorBoundary>
        <Resource<TelefoneClienteExibicaoModel[], TelefoneClienteModel>
          path={getTelefonesQueryConfig().queryKey ?? ''}
          loaderComponent={<Loader />}
          render={({ data: phones }) => <PhonesList phones={phones} />}
        />
      </BemErrorBoundary>
    </Flex>
  );
};
