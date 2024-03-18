import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { RadioCardsGroup, RadioCardVariant } from '@pcf/design-system';
import { EnderecoClienteExibicaoModel, useEnderecos } from '@pcf/core';

import { BaseLayoutStep } from './base-step-layout';

import { getReadableEndereco } from '../../../../perfil/addresses/utils/addresses.utils';

export const Enderecos: FC = () => {
  const { data: enderecos = [], isLoading } = useEnderecos();

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <RadioCardsGroup name="enderecos" onChange={() => {}} isRadio={false}>
        {enderecos.map((endereco: EnderecoClienteExibicaoModel) => (
          <RadioCardVariant
            key={endereco.id}
            value={`${endereco.id}`}
            header={<Text color="white">Endereço</Text>}
            content={<Text>{getReadableEndereco(endereco)}</Text>}
            footer={null}
          />
        ))}
      </RadioCardsGroup>

      <BaseLayoutStep.Footer />
    </BaseLayoutStep>
  );
};
