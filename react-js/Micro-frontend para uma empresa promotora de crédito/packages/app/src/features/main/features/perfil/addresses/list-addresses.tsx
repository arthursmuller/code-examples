import { FC } from 'react';

import { Button, Flex, Grid } from '@chakra-ui/react';
import { Link, useRouteMatch } from 'react-router-dom';

import { EnderecoClienteExibicaoModel, useEnderecos } from '@pcf/core';
import { Loader, NoDataDisplay } from '@pcf/design-system';

import { AddressListItem } from './components/address-list-item';

const byEnderecoPrincipal = (endereco: EnderecoClienteExibicaoModel): number =>
  endereco.principal ? -1 : 1;

export const ListAddresses: FC = () => {
  const { data, isLoading } = useEnderecos();
  const { path } = useRouteMatch();

  if (isLoading) {
    return <Loader />;
  }

  const linkToNew = (
    <Link to={`${path}/novo`}>
      <Button variant="link" color="secondary.regular">
        Cadastrar novo endereço
      </Button>
    </Link>
  );

  return (
    <Flex flexDir="column" my={8}>
      {data?.length ? (
        <>
          <Grid gap={[6, 6, 8]} gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}>
            {data?.sort(byEnderecoPrincipal).map((endereco) => (
              <AddressListItem endereco={endereco} key={endereco.id} />
            ))}
          </Grid>

          <Flex
            width="100%"
            justifyContent={['center', 'center', 'flex-end']}
            mt={[6, 6, 12]}
          >
            {linkToNew}
          </Flex>
        </>
      ) : (
        <NoDataDisplay entityName="endereço">{linkToNew}</NoDataDisplay>
      )}
    </Flex>
  );
};
