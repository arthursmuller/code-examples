import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import {
  RadioCardsGroup,
  RadioCardVariant,
  getFormattedPhone,
} from '@pcf/design-system';
import { useTelefones } from '@pcf/core';

import { BaseLayoutStep } from './base-step-layout';

export const Telefones: FC = () => {
  const { data = [], isLoading } = useTelefones();

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <RadioCardsGroup name="telefones" onChange={() => {}} isRadio={false}>
        {data.map((phone) => (
          <RadioCardVariant
            key={phone.id}
            value={`${phone.id}`}
            header={<Text color="white">Telefone</Text>}
            content={
              <Text>
                {getFormattedPhone(phone)}{' '}
                {phone.principal ? '(Principal)' : ''}
              </Text>
            }
            footer={null}
          />
        ))}
      </RadioCardsGroup>

      <BaseLayoutStep.Footer />
    </BaseLayoutStep>
  );
};
