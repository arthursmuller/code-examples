import React, { FC } from 'react';

import { Button, Divider, Flex, Icon, Text } from '@chakra-ui/react';
import { Link, useRouteMatch } from 'react-router-dom';

import { EnderecoClienteExibicaoModel } from '@pcf/core';
import { CustomHeading } from '@pcf/design-system';
import { ActionEditIcon } from '@pcf/design-system-icons';

import { getReadableEndereco } from '../utils/addresses.utils';

interface AddressListItemProps {
  endereco: EnderecoClienteExibicaoModel;
}

export const AddressListItem: FC<AddressListItemProps> = ({
  endereco,
}: AddressListItemProps) => {
  const { path } = useRouteMatch();

  return (
    <Flex
      h="auto"
      w="100%"
      flexDir="column"
      borderRadius="8px"
      overflow="hidden"
      boxShadow="card"
    >
      <Flex
        h="56px"
        px={6}
        justifyContent="space-between"
        bg={endereco.principal ? 'secondary.mid-dark' : 'grey.600'}
        alignItems="center"
        borderRadius="8px 8px 0px 0px"
      >
        <CustomHeading textStyle="bold16" color="white" overflow="hidden">
          {`
            ${
              endereco.principal ? 'Endereço principal' : 'Endereço alternativo'
            }
            ${endereco.titulo ? ` - ${endereco.titulo}` : ''}
          `}
        </CustomHeading>
      </Flex>

      <Flex
        direction="column"
        bg="grey.200"
        borderRadius="0 0 8px 8px"
        flex={1}
      >
        <Flex minH="80px" px={6} py={4} flex={1}>
          <Text textStyle="regular16" color="grey.800" whiteSpace="pre-line">
            {getReadableEndereco(endereco)}
          </Text>
        </Flex>

        <Divider borderColor="grey.600" />

        <Flex alignItems="center" justifyContent="center" paddingY={3}>
          <Link to={`${path}/${endereco.id}`}>
            <Button
              variant="link"
              color="secondary.regular"
              size="sm"
              fontWeight="700"
              rightIcon={<Icon as={ActionEditIcon} />}
            >
              Editar
            </Button>
          </Link>
        </Flex>
      </Flex>
    </Flex>
  );
};
