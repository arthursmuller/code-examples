import { FC } from 'react';

import { Center, Divider, Flex, FlexProps, Text } from '@chakra-ui/react';
import { formatCPF } from '@brazilian-utils/brazilian-utils';

import { Loader, CustomHeading } from '@pcf/design-system';
import { useClienteLogado, useEnderecos, useTelefones } from '@pcf/core';
import { EditableAvatar } from 'features/main/components/editable-avatar';

import { QuickProfileInfoDialog } from './quicky-profile-info-dialog';

import { getReadableEndereco } from '../addresses/utils/addresses.utils';
import {
  getFormattedMainPhone,
  getReadableLoja,
} from '../contacts/utils/contacts.utils';

interface QuickyProfileProps extends FlexProps {
  asDialog?: boolean;
}

export const QuickyProfile: FC<QuickyProfileProps> = ({
  asDialog = false,
  ...rest
}) => {
  const { data: cliente, isLoading } = useClienteLogado();
  const { data: enderecos, isLoading: isLoadingEnderecos } = useEnderecos();
  const { data: phones, isLoading: isLoadingTelefones } = useTelefones();

  const enderecoPrincipal = enderecos?.find((endereco) => endereco.principal);

  return (
    <Flex
      flexDir="column"
      alignItems="center"
      px={6}
      bg="secondary.regular"
      h="100%"
      {...rest}
    >
      {isLoading || isLoadingEnderecos || isLoadingTelefones ? (
        <Flex minHeight="200px">
          <Loader theme="white" />
        </Flex>
      ) : (
        <>
          <CustomHeading
            as="h1"
            textStyle="bold32"
            color="white"
            my={[5, 5, 4]}
            lineHeight={['24px', '24px', '32px']}
          >
            Meu Perfil
          </CustomHeading>

          <Divider borderColor="secondary.washed" mb={[6, 6, 8]} />

          <EditableAvatar />

          <CustomHeading
            as="h3"
            textStyle="bold20"
            textAlign="center"
            textTransform="capitalize"
            color="white"
            mt={[3, 3, 6]}
            mb={[5, 5, 6]}
            mx={[12, 12, 0]}
          >
            {cliente?.nome}
          </CustomHeading>

          {asDialog ? (
            <Center w="100%" borderColor="secondary.washed" minH="64px">
              <QuickProfileInfoDialog
                client={cliente}
                address={enderecoPrincipal}
                phones={phones}
              />
            </Center>
          ) : (
            <>
              <Center
                w="100%"
                borderTopWidth="1px"
                borderColor="secondary.washed"
                minH="64px"
              >
                <Text
                  as="p"
                  color="white"
                  textAlign="center"
                  p={4}
                  textStyle="regular14"
                >
                  CPF: {cliente?.cpf && formatCPF(cliente?.cpf)}
                </Text>
              </Center>

              <Center
                w="100%"
                borderTopWidth="1px"
                borderColor="secondary.washed"
                minH="64px"
              >
                <Text
                  as="p"
                  color="white"
                  textAlign="center"
                  p={4}
                  textStyle="regular14"
                >
                  Telefone principal:
                  <br /> {getFormattedMainPhone(phones) || 'Não informado'}
                </Text>
              </Center>

              <Center
                w="100%"
                borderTopWidth="1px"
                borderColor="secondary.washed"
                minH="64px"
              >
                <Text
                  as="p"
                  color="white"
                  textAlign="center"
                  p={4}
                  textStyle="regular14"
                >
                  Endereço principal:
                  <br />
                  {enderecoPrincipal
                    ? getReadableEndereco(enderecoPrincipal)
                    : 'Não informado'}
                </Text>
              </Center>

              {cliente?.loja && (
                <Center
                  w="100%"
                  borderTopWidth="1px"
                  borderColor="secondary.washed"
                  minH="64px"
                >
                  <Text
                    as="p"
                    color="white"
                    textAlign="center"
                    paddingY={4}
                    textStyle="regular14"
                    whiteSpace="pre-line"
                  >
                    Relacionamento Ponto de Venda:
                    <br />
                    {enderecoPrincipal
                      ? getReadableLoja(cliente?.loja)
                      : 'Não informado'}
                  </Text>
                </Center>
              )}
            </>
          )}
        </>
      )}
    </Flex>
  );
};
