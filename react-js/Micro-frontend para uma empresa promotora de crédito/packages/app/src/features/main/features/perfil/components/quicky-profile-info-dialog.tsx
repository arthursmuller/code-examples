import { FC } from 'react';

import { formatCPF } from '@brazilian-utils/brazilian-utils';
import { Flex, Text, Button } from '@chakra-ui/react';

import { ActionDialog, useModal } from '@pcf/design-system';
import {
  TelefoneClienteExibicaoModel,
  ClienteExibicaoModel,
  EnderecoClienteExibicaoModel,
} from '@pcf/core';

import {
  getFormattedMainPhone,
  getReadableLoja,
} from '../contacts/utils/contacts.utils';
import { getReadableEndereco } from '../addresses/utils/addresses.utils';

interface QuickProfileInfoDialogProps {
  client?: ClienteExibicaoModel;
  address?: EnderecoClienteExibicaoModel;
  phones?: TelefoneClienteExibicaoModel[];
}

const InfoLine: FC<{ label: string; info: string }> = ({ label, info }) => (
  <>
    <Text color="secondary.mid-dark" textStyle="regular14">
      {label}
    </Text>

    <Text textStyle="regular14" marginBottom={6} whiteSpace="pre-line">
      {info}
    </Text>
  </>
);

export const QuickProfileInfoDialog: FC<QuickProfileInfoDialogProps> = ({
  client,
  address,
  phones,
}) => {
  const { showModal } = useModal();

  const showInfoModal = (): void =>
    showModal({
      modal: (
        <ActionDialog
          title="Resumo de dados"
          info={
            <Flex direction="column">
              {client && <InfoLine label="CPF" info={formatCPF(client.cpf)} />}
              {phones && (
                <InfoLine
                  label="Telefone principal"
                  info={getFormattedMainPhone(phones) || 'Não informado'}
                />
              )}
              {address && (
                <InfoLine
                  label="Endereço principal"
                  info={getReadableEndereco(address) || 'Não informado'}
                />
              )}
              {client?.loja && (
                <InfoLine
                  label="Relacionamento ponto de venda"
                  info={getReadableLoja(client.loja) || 'Não informado'}
                />
              )}
            </Flex>
          }
          hasCancel={false}
          confirmLabel="Fechar"
        />
      ),
    });

  return (
    <Button
      variant="link"
      color="white"
      textDecoration="underline"
      onClick={showInfoModal}
    >
      Visualizar resumo de dados
    </Button>
  );
};
